---
layout: "post"
title: "WallCrossSection Renaming in the Revit 2022.1 API"
date: "2021-11-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2022"
  - "Migration"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/11/wallcrosssection-renaming-in-the-revit-20221-api.html "
typepad_basename: "wallcrosssection-renaming-in-the-revit-20221-api"
typepad_status: "Publish"
---

<p>Breaking news from the Revit development team:</p>

<h4><a name="2"></a> WallCrossSection versus WallCrossSectionDefinition</h4>

<p>Last week, we mentioned the unfortunate breaking change inadvertently introduced with the Revit 2022.1 API update
by <a href="https://thebuildingcoder.typepad.com/blog/2021/11/revit-20221-sdk-revitlookup-build-and-install.html#3">renaming <code>WallCrossSection</code> to <code>WallCrossSectionDefinition</code></a> and
suggested a fix for the <code>BuiltInParameterGroup</code> enumeration value.</p>

<p>Here is the workaround suggested by the development team to also address the <code>ForgeTypeId</code> modification to support both versions of the API:</p>

<p>As you know from
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revitapi-2022-update-change-wallcrosssection-to/td-p/10720345">Revit API 2022.1 update change <code>WallCrossSection</code> to <code>WallCrossSectionDefinition</code></a>,
there was a breaking change introduced in Revit 2022.1:</p>

<ul>
<li><code>BuiltInParameterGroup.PG_WALL_CROSS_SECTION</code></li>
<li><code>ForgeTypeId.WallCrossSection</code></li>
</ul>

<p>were renamed to </p>

<ul>
<li><code>BuiltInParameterGroup.PG_WALL_CROSS_SECTION_DEFINITION</code></li>
<li><code>ForgeTypeId.WallCrossSectionDefinition</code></li>
</ul>

<p>respectively.</p>

<p>A solution for the first name change in the enum value was already suggested in the forum discussion thread:</p>

<p>The actual integer value can be used instead to define a constant like this:</p>

<pre class="code">
  <span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">PG_WALL_CROSS_SECTION</span>&nbsp;=&nbsp;(BuiltInParameterGroup)&nbsp;(-5000228);
</pre>

<p>This value be used in both Revit 2022.0 and Revit 2022.1 without causing the problem.</p>

<p>A workaround for the second rename, the <code>WallCrossSection</code> property of the <code>ForgeTypeId</code> class, can be implemented using Reflection in all .NET languages.</p>

<p>Here is a sample code snippet in C#:</p>

<pre class="code">
<span style="color:blue;">using</span>&nbsp;System.Reflection;

. . .

  ForgeTypeId&nbsp;<span style="color:#1f377f;">id</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ForgeTypeId();
  Type&nbsp;<span style="color:#1f377f;">type</span>&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(GroupTypeId);

  PropertyInfo&nbsp;<span style="color:#1f377f;">propOld</span>&nbsp;=&nbsp;type.GetProperty(<span style="color:#a31515;">&quot;WallCrossSection&quot;</span>,
    BindingFlags.Public&nbsp;|&nbsp;BindingFlags.Static);

  PropertyInfo&nbsp;<span style="color:#1f377f;">propOld</span>&nbsp;=&nbsp;type.GetProperty(<span style="color:#a31515;">&quot;WallCrossSection&quot;</span>,
    BindingFlags.Public&nbsp;|&nbsp;BindingFlags.Static);

  <span style="color:#8f08c4;">if</span>&nbsp;(<span style="color:blue;">null</span>&nbsp;!=&nbsp;propOld)
  {
    id&nbsp;=&nbsp;(ForgeTypeId)&nbsp;propOld.GetValue(<span style="color:blue;">null</span>,&nbsp;<span style="color:blue;">null</span>);
  }
  <span style="color:#8f08c4;">else</span>
  {
    PropertyInfo&nbsp;<span style="color:#1f377f;">propNew</span>&nbsp;=&nbsp;type.GetProperty(<span style="color:#a31515;">&quot;WallCrossSectionDefinition&quot;</span>,
      BindingFlags.Public&nbsp;|&nbsp;BindingFlags.Static);

    id&nbsp;=&nbsp;(ForgeTypeId)&nbsp;propNew.GetValue(<span style="color:blue;">null</span>,&nbsp;<span style="color:blue;">null</span>);
  }
</pre>

<p>Or, if you prefer a more succinct version, use this:</p>

<pre class="code">
&nbsp;&nbsp;Type&nbsp;<span style="color:#1f377f;">type</span>&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(GroupTypeId);

&nbsp;&nbsp;PropertyInfo&nbsp;<span style="color:#1f377f;">prop</span>&nbsp;=&nbsp;type.GetProperty(<span style="color:#a31515;">&quot;WallCrossSection&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;BindingFlags.Public&nbsp;|&nbsp;BindingFlags.Static)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;??&nbsp;type.GetProperty(<span style="color:#a31515;">&quot;WallCrossSectionDefinition&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BindingFlags.Public&nbsp;|&nbsp;BindingFlags.Static);

&nbsp;&nbsp;ForgeTypeId&nbsp;<span style="color:#1f377f;">id</span>&nbsp;=&nbsp;(ForgeTypeId)&nbsp;prop.GetValue(<span style="color:blue;">null</span>,&nbsp;<span style="color:blue;">null</span>);
</pre>

<p>We tested it here, and it works for both Revit 2022.0 and Revit 2022.1.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdeff112a200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdeff112a200c img-responsive" alt="Non-breaking change" title="Non-breaking change" src="/assets/image_446ef7.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Non-breaking change &ndash; &#xA9; <a href="https://www.datamation.com">Datamation</a></p>

<p></center></p>
