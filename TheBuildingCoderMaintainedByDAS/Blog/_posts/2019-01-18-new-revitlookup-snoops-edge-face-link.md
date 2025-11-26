---
layout: "post"
title: "New RevitLookup Snoops Edges, Faces and Links"
date: "2019-01-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
  - "Family"
  - "Geometry"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/01/new-revitlookup-snoops-edge-face-link.html "
typepad_basename: "new-revitlookup-snoops-edge-face-link"
typepad_status: "Publish"
---

<p>Before leaving for the weekend, let me highlight some recent additions
to <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> by 
Håvard Leding of <a href="https://www.symetri.com">Symetri</a>:</p>

<ul>
<li><a href="#3">Three new RevitLookup commands</a> </li>
<li><a href="#4">About "Snoop Pick Face..."</a> </li>
<li><a href="#5">About "Pick Linked Element..."</a> </li>
<li><a href="#6">Running in a family document</a> </li>
</ul>

<p>I added and tested the new commands
in <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.6">RevitLookup release 2019.0.0.6</a>.</p>

<p>Below is the description and some additional background information in Håvard's own words.</p>

<p>Many thanks to Håvard for implementing and sharing this!</p>

<h4><a name="3"></a> Three New RevitLookup Commands</h4>

<p>This is perhaps something of interest to someone.</p>

<p>Three very simple additions:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3b20425200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3b20425200d img-responsive" style="width: 268px; display: block; margin-left: auto; margin-right: auto;" alt="Three new commands" title="Three new commands" src="/assets/image_4c6027.jpg" /></a><br /></p>

<p></center></p>

<p>The first one really helped when debugging stable references on joined solid geometry in families.
Just the picked <code>Reference</code> passed into the <code>Object</code> form.
If I pass the <code>GeometryObject</code> (the face), it will not retrieve a reference, presumably because <code>GeometryObjectFromReference</code> doesn't calculate references.</p>

<p>If of any use, the <a href="https://thebuildingcoder.typepad.com/files/hl_revitlookup_commands.txt">commands are attached here</a>.</p>

<p>Here is the code to generate the additional ribbon entries:</p>

<pre class="code">
  optionsBtn.AddPushButton(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;<span style="color:#a31515;">&quot;Snoop&nbsp;Pick&nbsp;Face...&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Snoop&nbsp;Pick&nbsp;Face...&quot;</span>,&nbsp;ExecutingAssemblyPath,&nbsp;<span style="color:#a31515;">&quot;RevitLookup.CmdSnoopModScopePickSurface&quot;</span>&nbsp;)&nbsp;);

  optionsBtn.AddPushButton(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;<span style="color:#a31515;">&quot;Snoop&nbsp;Pick&nbsp;Edge...&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Snoop&nbsp;Pick&nbsp;Edge...&quot;</span>,&nbsp;ExecutingAssemblyPath,&nbsp;<span style="color:#a31515;">&quot;RevitLookup.CmdSnoopModScopePickEdge&quot;</span>&nbsp;)&nbsp;);

  optionsBtn.AddPushButton(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;<span style="color:#a31515;">&quot;Snoop&nbsp;Pick&nbsp;Linked&nbsp;Element...&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Snoop&nbsp;Linked&nbsp;Element...&quot;</span>,&nbsp;ExecutingAssemblyPath,&nbsp;<span style="color:#a31515;">&quot;RevitLookup.CmdSnoopModScopeLinkedElement&quot;</span>&nbsp;)&nbsp;);
</pre>

<h4><a name="4"></a> About "Snoop Pick Face..."</h4>

<p>Using <code>Autodesk.Revit.UI.Selection.ObjectType.Face...</code> gets you a reference to a face.</p>

<p>But if you use this instead...</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;face&nbsp;=&nbsp;cmdData.Application.ActiveUIDocument
&nbsp;&nbsp;&nbsp;&nbsp;.Document.GetElement(&nbsp;refElem&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;refElem&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Face</span>;
</pre>

<p>...then <code>Face.Reference</code> will be null.</p>

<p>Such a reference is needed when placing face-based families or dimensions, for example.</p>

<p>But it does get you a stable reference to the face:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;stableRef&nbsp;=&nbsp;refElem
&nbsp;&nbsp;&nbsp;&nbsp;.ConvertToStableRepresentation(&nbsp;uidoc.Document&nbsp;);
</pre>

<p>Which you can use to find the same face using <code>Element.get_Geometry</code>.
Where <code>Options</code> calculate the references.
And that face (really the same face) will have a usable reference.</p>

<p>Using Lookup, I found this <code>stableRef</code> inside a <code>GeomCombination</code>.
And so, I knew I had to include <code>GeomCombination</code> in my <code>FilteredElementCollector</code>.</p>

<p>Perhaps this could be an improvement on <code>GetGeometryObjectFromReference</code>?
An overload to calcuate references if possible.
<code>GetGeometryObjectFromReference(</code> <code>Reference,</code> <code>bool</code> <code>CalculatedReference )</code>.</p>

<p>The new pick options helped guide me to my target face.</p>

<h4><a name="4"></a> About "Pick Linked Element..."</h4>

<p>"Snoop Pick Linked Element..." I haven't had use for yet.</p>

<p>I suspect I will use it quite a bit when debugging interaction with linked elements.</p>

<p>Seems to me you could have used "Pick Linked Element..." yourself in your latest discussion
on <a href="https://thebuildingcoder.typepad.com/blog/2019/01/retrieving-linked-ifczone-elements-using-python.html">retrieving linked <code>IfcZone</code> elements using Python</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad38bee86200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad38bee86200c image-full img-responsive" alt="Snooping linked elements" title="Snooping linked elements" src="/assets/image_c68a26.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="5"></a> Running in a Family Document</h4>

<p>In my case, the code is running in a family document, not a project.</p>

<p>I get all the solids I need first, knowing that somewhere inside, there is the face I first picked.</p>

<pre class="code">
&nbsp;&nbsp;List&lt;<span style="color:#2b91af;">Solid</span>&gt;&nbsp;solidsInFamily&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;<span style="color:#2b91af;">Solid</span>&gt;();
&nbsp;&nbsp;IList&lt;<span style="color:#2b91af;">Type</span>&gt;&nbsp;geomTypes&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;<span style="color:#2b91af;">Type</span>&gt;()&nbsp;{&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">GenericForm</span>&nbsp;),&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">GeomCombination</span>&nbsp;)&nbsp;};
&nbsp;&nbsp;<span style="color:#2b91af;">ElementMulticlassFilter</span>&nbsp;emcf&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticlassFilter</span>(&nbsp;geomTypes&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;colForms&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;.WherePasses(&nbsp;emcf&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Options</span>&nbsp;opt&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>();
&nbsp;&nbsp;opt.ComputeReferences&nbsp;=&nbsp;<span style="color:blue;">true</span>;

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">CombinableElement</span>&nbsp;combinable&nbsp;<span style="color:blue;">in</span>&nbsp;colForms&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;combinable&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">GenericForm</span>&nbsp;&amp;&amp;&nbsp;!(&nbsp;combinable&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">GenericForm</span>&nbsp;).IsSolid&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">continue</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GeometryElement</span>&nbsp;geomElem&nbsp;=&nbsp;combinable.get_Geometry(&nbsp;opt&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;<span style="color:#2b91af;">Solid</span>&gt;&nbsp;solids&nbsp;=&nbsp;Utils.GetElementSolids(&nbsp;geomElem&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;solidsInFamily.AddRange(&nbsp;solids&nbsp;);
&nbsp;&nbsp;}
</pre>

<p>Then, using the stable reference from "Pick Face", iterate the solids until I find the face I'm looking for:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;solid&nbsp;<span style="color:blue;">in</span>&nbsp;solids&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;face&nbsp;<span style="color:blue;">in</span>&nbsp;solid.Faces&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;stable&nbsp;=&nbsp;face.Reference.ConvertToStableRepresentation(&nbsp;doc&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;stable&nbsp;==&nbsp;stableRef&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;face&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>Perhaps there is another, simpler, way of getting (picking) a face which has a reference?</p>

<p>But if I do this, passing the picked face, not the reference:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3b20447200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3b20447200d img-responsive" alt="Snooping Face object" title="Snooping Face object" src="/assets/image_95d548.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Then the <code>Face</code> has no reference:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad38beea9200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad38beea9200c img-responsive" alt="Snooping Face object" title="Snooping Face object" src="/assets/image_faee08.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Which more or less prevents any interaction with it, such as placing dimensions, alignments or face-based families.</p>

<p>Enjoy!</p>
