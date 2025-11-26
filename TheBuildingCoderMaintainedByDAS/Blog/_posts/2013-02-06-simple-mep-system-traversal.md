---
layout: "post"
title: "Simple MEP System Traversal"
date: "2013-02-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/02/simple-mep-system-traversal.html "
typepad_basename: "simple-mep-system-traversal"
typepad_status: "Publish"
---

<p>Here is a simple MEP system traversal implementation that especially addresses the issue of determining what equipment is connected to the systems.

<p>The following read-only external command traverses all MEP systems in the document, using the MEPSystem.Elements property to retrieve most of the desired elements with very little effort.

<p>More than half the code is actually fussing about with formatting the result:

<pre class="code">
[<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.ReadOnly )]
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">static</span> <span class="blue">string</span> PluralSuffix( <span class="blue">int</span> n )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> 1 == n ? <span class="maroon">&quot;&quot;</span> : <span class="maroon">&quot;s&quot;</span>;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">void</span> TraverseSystems( <span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> systems
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">MEPSystem</span> ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> i, n;
&nbsp; &nbsp; <span class="blue">string</span> s;
&nbsp; &nbsp; <span class="blue">string</span>[] a;
&nbsp;
&nbsp; &nbsp; <span class="teal">StringBuilder</span> message = <span class="blue">new</span> <span class="teal">StringBuilder</span>();
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">MEPSystem</span> system <span class="blue">in</span> systems )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; message.AppendLine( <span class="maroon">&quot;System Name: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + system.Name );
&nbsp;
&nbsp; &nbsp; &nbsp; message.AppendLine( <span class="maroon">&quot;Base Equipment: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + system.BaseEquipment );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">ConnectorSet</span> cs = system.ConnectorManager
&nbsp; &nbsp; &nbsp; &nbsp; .Connectors;
&nbsp;
&nbsp; &nbsp; &nbsp; i = 0;
&nbsp; &nbsp; &nbsp; n = cs.Size;
&nbsp; &nbsp; &nbsp; a = <span class="blue">new</span> <span class="blue">string</span>[n];
&nbsp;
&nbsp; &nbsp; &nbsp; s = <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{0} element{1} in ConnectorManager: &quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; n, PluralSuffix( n ) );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Connector</span> c <span class="blue">in</span> cs )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Element</span> e = c.Owner;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != e )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; a[i++] = e.GetType().Name
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; &quot;</span> + e.Id.ToString();
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; message.AppendLine( s
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="blue">string</span>.Join( <span class="maroon">&quot;, &quot;</span>, a ) );
&nbsp;
&nbsp; &nbsp; &nbsp; i = 0;
&nbsp; &nbsp; &nbsp; n = system.Elements.Size;
&nbsp; &nbsp; &nbsp; a = <span class="blue">new</span> <span class="blue">string</span>[n];
&nbsp;
&nbsp; &nbsp; &nbsp; s = <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{0} element{1} in System: &quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; n, PluralSuffix( n ) );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> system.Elements )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; a[i++] = e.GetType().Name
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; &quot;</span> + e.Id.ToString();
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; message.AppendLine( s
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="blue">string</span>.Join( <span class="maroon">&quot;, &quot;</span>, a ) );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; n = systems.Count&lt;<span class="teal">Element</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> caption =
&nbsp; &nbsp; &nbsp; <span class="blue">string</span>.Format( <span class="maroon">&quot;Traverse {0} MEP System{1}&quot;</span>,
&nbsp; &nbsp; &nbsp; n, (1 == n ? <span class="maroon">&quot;&quot;</span> : <span class="maroon">&quot;s&quot;</span>) );
&nbsp;
&nbsp; &nbsp; <span class="teal">TaskDialog</span> dlg = <span class="blue">new</span> <span class="teal">TaskDialog</span>( caption );
&nbsp; &nbsp; dlg.MainContent = message.ToString();
&nbsp; &nbsp; dlg.Show();
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; TraverseSystems( doc );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<p>I executed this on an absolutely trivial system:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c36a25bf1970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c36a25bf1970b image-full" alt="Simple system" title="Simple system" src="/assets/image_2373db.jpg" border="0" /></a><br />

</center>

<p>That generates the following message box as a result:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d40d0df71970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d40d0df71970c" alt="Resulting message" title="Resulting message" src="/assets/image_ffa3a2.jpg" border="0" /></a><br />

</center>

<p>Obviously, you will want to clean up the reporting significantly to suit your needs.
Currently, the owner elements in the ConnectorSet are listed, and the same elements also appear in the system elements list.
Here is another result of running this on two systems, an electrical and a duct system, with the elements highlighted in yellow:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d40d0e09e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d40d0e09e970c image-full" alt="Duplicated elements" title="Duplicated elements" src="/assets/image_2f4f19.jpg" border="0" /></a><br />

</center>

<p>Also, the API reports four elements in the system including one duct segment, whereas the UI reports three, which is what one would expect, so some identification of duplicate elements needs to be added to make this useful.

<p>Furthermore, reporting on these ‘logical’ systems may have the limitation that in-line equipment such as duct dampers and valves are not reported as part of the system.
The other system traversal samples based on physical connectivity provide this info, however.

<p>For now, the main point is to demonstrate that this simple access exists at all.

<p>For your convenience, here is

<span class="asset  asset-generic at-xid-6a00e553e168978833017d40d0e1af970c"><a href="http://thebuildingcoder.typepad.com/files/mepsystemtraversal.zip">MepSystemTraversal.zip</a></span> containing

the source code, Visual Studio solution and add-in manifest for this command.</p>

<p>For more advanced traversal algorithms and determining the correct order of the individual system elements in the direction of the flow, you can look at the

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/revit-mep-api.html">
TraverseSystem SDK sample</a>

(<a href="http://thebuildingcoder.typepad.com/blog/2009/09/the-revit-mep-api.html#6">2010</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2010/05/the-revit-mep-2011-api.html#samples">2011</a>)

for mechanical systems and the

<a href="http://thebuildingcoder.typepad.com/blog/2012/05/the-adn-mep-sample-adnrme-for-revit-mep-2013.html">
AdnRme</a> sample

for electrical ones.</p>
