---
layout: "post"
title: "GetSimilarTypes, SnappingService and Title Labels"
date: "2019-09-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Content"
  - "Data Access"
  - "Element Relationships"
  - "Family"
  - "Filters"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/09/getsimilartypes-snappingservice-and-title-block-labels.html "
typepad_basename: "getsimilartypes-snappingservice-and-title-block-labels"
typepad_status: "Publish"
---

<p>So many interesting discussions and inspiring solutions in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>!</p>

<p>Here are two of them, plus one non-forum beginner case:</p>

<ul>
<li><a href="#2"><code>GetSimilarTypes</code> filters for curtain wall door symbols</a></li>
<li><a href="#3"><code>SnappingService</code> &ndash; what does it actually do?</a></li>
<li><a href="#4">Get title block label parameters</a></li>
</ul>

<h4><a name="2"></a> GetSimilarTypes Filters for Curtain Wall Door Symbols</h4>

<p>Yet once again,
Frank <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518">@Fair59</a> Aarssen
comes to the rescue, answering
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/builtincategory-of-doors-and-curtain-wall-doors/m-p/9002988"><code>BuiltInCategory</code> of doors and curtain wall doors</a>:</p>

<p><strong>Question:</strong> I want to build a plugin that changes the curtain wall panel types.</p>

<p>When I select a curtain wall door, how can I filter for only curtain wall panel door symbols?</p>

<p>Is there any specific value for curtain panel door?</p>

<p>Snooping a curtain wall panel door in RevitLookup:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4cce3d7200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4cce3d7200b image-full img-responsive" alt="Snoop door in curtain wall" title="Snoop door in curtain wall" src="/assets/image_516c6b.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>A door in a standard wall looks similar:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a47f0170200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a47f0170200c image-full img-responsive" alt="Snoop door in standard wall" title="Snoop door in standard wall" src="/assets/image_81d132.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Since the category is the same for all doors, that cannot be used to tell them apart.</p>

<p>Another possible criterion might be to query the door for its host using
its <a href="https://www.revitapidocs.com/2020/69f30141-bd3b-8bdd-7a63-6353d4d495f9.htm"><code>Host</code> property</a>.
Using the host element properties, one ought to be able to differentiate curtain walls from others.
However, I am looking at family symbols that have not yet been placed, not instances, so the host is not defined yet.</p>

<p>The <code>Family</code> class provides an <a href="https://www.revitapidocs.com/2020/da0becae-cb65-fffd-1e97-b4aab5533004.htm">IsCurtainPanelFamily property</a>,
so I also tried checking</p>

<pre>
  FamilyInstance.Symbol.Family.IsCurtainPanelFamily
</pre>

<p>Unfortunately, <code>IsCurtainPanelFamily</code> always returns false, so that does not work either.</p>

<p><strong>Answer:</strong> From the <code>FamilySymbol</code>, you can find similar types using <code>GetSimilarTypes</code>.</p>

<p>This returns all curtain wall panels that can be placed in a curtain wall.</p>

<p>Filter that list for the door category:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;&nbsp;CW_doors
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;symbol.GetSimilarTypes()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Doors&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;();
</pre>

<p>Many thanks to Frank for solving this!</p>

<p>I added his suggestion 
to <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> in
a new method <code>GetDoorSymbolsForCurtainWall</code>
in <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2020.0.147.7...2020.0.147.7">release 2020.0.147.7</a>.</p>

<h4><a name="3"></a> SnappingService &ndash; What Does it Actually Do?</h4>

<p>From
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/snappingservice-what-it-actually-does/m-p/8986801"><code>SnappingService</code> &ndash; What it actually does?</a></p>

<p><strong>Question:</strong> Does anyone have any insight or experience? The API documentation doesn't say much &nbsp; :-(</p>

<p><strong>Answer:</strong> According to the development team, <code>SnappingService</code> provides snapping points and lines and is used for point clouds.
They also expressed an intent to add some documentation for it, and maybe for a couple of other undocumented services as well.</p>

<h4><a name="4"></a> Get Title Block Label Parameters</h4>

<p>Let's wrap up for today with a beginner's question:</p>

<p><strong>Question:</strong> I am trying to access these title block label parameters marked in yellow through the Revit API:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a47f013f200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a47f013f200c image-full img-responsive" alt="Title block label parameters" title="Title block label parameters" src="/assets/image_45f128.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I could not find a proper way to access those values in the Revit API documentation.</p>

<p>I'd like to know if this actually possible to achieve or not.</p>

<p><strong>Answer:</strong> Yes, this is easy to achieve.</p>

<p>In fact, you can probably find out for yourself how to access this data by
installing <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> and
using that to explore the title block properties.</p>

<p>Are you aware of RevitLookup?
It is a really invaluable tool for Revit add-in development and understanding the structure and contents of the Revit database.</p>

<p>You may also find it out easily for yourself by searching the Internet for an existing solution, e.g., by searching for something
like <a href="https://duckduckgo.com/?q=revit+api+title+block+label+parameters">revit api title block label parameters</a>.</p>

<p>The Building Coder took a look at
the <a href="https://thebuildingcoder.typepad.com/blog/2009/11/title-block-of-sheet.html">title block of a sheet</a> and
its parameters a long time ago, without providing a method to achieve what you are asking for.</p>

<p>Since I think it is time to take another look at this and it may be useful for others in future as well, I put together the following function that may or may not achieve what you are after:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Read&nbsp;the&nbsp;title&nbsp;block&nbsp;parameters&nbsp;to&nbsp;retrieve&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;label&nbsp;parameters&nbsp;Sheet&nbsp;Number,&nbsp;Author&nbsp;and&nbsp;Client&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Name</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ReadTitleBlockLabelParameters(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;title_block_instances
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_TitleBlocks&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;Title&nbsp;block&nbsp;instances:&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;tb&nbsp;<span style="color:blue;">in</span>&nbsp;title_block_instances&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;typeId&nbsp;=&nbsp;tb.GetTypeId();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;type&nbsp;=&nbsp;doc.GetElement(&nbsp;typeId&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&nbsp;tb.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SHEET_NUMBER&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;sheet&nbsp;number&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s_sheet_number&nbsp;=&nbsp;p.AsString();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&nbsp;tb.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.PROJECT_AUTHOR&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;project&nbsp;author&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s_project_author&nbsp;=&nbsp;p.AsValueString();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&nbsp;tb.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.CLIENT_NAME&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;client&nbsp;name&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s_client_name&nbsp;=&nbsp;p.AsValueString();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Title&nbsp;block&nbsp;{0}&nbsp;&lt;{1}&gt;&nbsp;of&nbsp;type&nbsp;{2}&nbsp;&lt;{3}&gt;:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;{4}&nbsp;project&nbsp;author&nbsp;{5}&nbsp;for&nbsp;client&nbsp;{6}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tb.Name,&nbsp;tb.Id.IntegerValue,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;type.Name,&nbsp;typeId.IntegerValue,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s_sheet_number,&nbsp;s_project_author,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s_client_name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>I also added this code
to <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a>
in <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2020.0.147.5...2020.0.147.6">release 2020.0.147.6</a>.</p>

<p>I hope this helps.</p>

<p>Please confirm whether this method does what you are after.</p>

<p>If not, please use RevitLookup to find out exactly what you need and fix it accordingly.</p>
