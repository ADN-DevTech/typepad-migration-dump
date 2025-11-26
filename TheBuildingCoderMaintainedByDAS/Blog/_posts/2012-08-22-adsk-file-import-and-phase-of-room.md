---
layout: "post"
title: "ADSK File Import and Phase of Room"
date: "2012-08-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "External"
  - "Geometry"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/08/adsk-file-import-and-phase-of-room.html "
typepad_basename: "adsk-file-import-and-phase-of-room"
typepad_status: "Publish"
---

<p>I already had one look at 

<a href="http://thebuildingcoder.typepad.com/blog/2011/12/loading-an-inventor-adsk-component.html">
loading an Inventor ADSK component</a> using 

the OpenBuildingComponentDocument method.

<p>Ishwar Nagwani of Autodesk Consulting now revisited this topic from a different perspective and explains:

<p>I came across another tricky issue while developing Revit add-in to import an ADSK file.
I looked at your previous post on this when I started looking for way to open ADSK file in Revit and it helped me lot.

<p>The ADSK file is created in Inventor and then processed in Revit to create an RFA file. 
This is part of a bigger project which does lots of other stuff like adding shared parameters etc. 

<p>The ADSK file after importing contains two ImportInstances.
One of these is the bounding box, the other is the main solid itself. 
The outer bounding box hides the geometry in elevation views.
Here the bounding box which would be deleted is displayed in red:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330176175fb8c3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330176175fb8c3970c image-full" alt="ADSK component with bounding box in red" title="ADSK component with bounding box in red" src="/assets/image_681d6e.jpg" border="0" /></a><br />

</center>

<p>These are the selected bounding box details displayed using RevitLookup:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017744463c0f970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017744463c0f970d image-full" alt="ADSK component bounding box properties" title="ADSK component bounding box properties" src="/assets/image_07a0ef.jpg" border="0" /></a><br />

</center>

<p>I open the ADSK file using the OpenBuildingComponentDocument method and then delete the bounding box in a separate transaction.

<p>The following method retrieves the ImportInstances and deletes the one representing the bounding box, identified by the fact that it only has exactly six faces:

<pre class="code">
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> This method deletes the outer bounding box </span>
<span class="gray">///</span><span class="green"> of an imported ADSK file in Revit. If this </span>
<span class="gray">///</span><span class="green"> bounding box is not deleted, the elevation </span>
<span class="gray">///</span><span class="green"> views cannot be seen. After importing an </span>
<span class="gray">///</span><span class="green"> ADSK file the two ImportInstance objects </span>
<span class="gray">///</span><span class="green"> are created in Revit, one for outer </span>
<span class="gray">///</span><span class="green"> bounding box and the other for main solid.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">void</span> DeleteOuterBox( <span class="teal">Document</span> doc )
{
&nbsp; <span class="green">// Instantiate this once only:</span>
&nbsp;
&nbsp; <span class="teal">Options</span> opt = doc.Application.Create
&nbsp; &nbsp; .NewGeometryOptions();
&nbsp;
&nbsp; <span class="green">// This is probably not needed:</span>
&nbsp;
&nbsp; <span class="green">//opt.ComputeReferences = true;</span>
&nbsp;
&nbsp; <span class="green">// Search the elements of type ImportInstance;</span>
&nbsp; <span class="green">// there will be two in case of ADSK import:</span>
&nbsp;
&nbsp; <span class="teal">FilteredElementCollector</span> collector 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">ImportInstance</span> ) );
&nbsp;
&nbsp; <span class="teal">ElementId</span> idToDelete = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> collector )
&nbsp; {
&nbsp; &nbsp; <span class="teal">ImportInstance</span> inst = e <span class="blue">as</span> <span class="teal">ImportInstance</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( inst != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Get the Solid from ImportInstance</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> geomElem 
&nbsp; &nbsp; &nbsp; &nbsp; = inst.get_Geometry( opt );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// GeometryElement.Objects is obsolete,</span>
&nbsp; &nbsp; &nbsp; <span class="green">// so use LINQ extension methods instead.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">//GeometryObjectArray geomObjArray </span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; = geomElem.Objects;</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">int</span> n = geomElem.Count&lt;<span class="teal">GeometryObject</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 1 == n )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solid = geomElem
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .First&lt;<span class="teal">GeometryObject</span>&gt;() <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// The outer bounding box</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// has just 6 faces.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( solid.Faces.Size == 6 )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Do not delete during iteration;<out/span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// remember the id instead.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//doc.Delete( e.Id );</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; idToDelete = e.Id;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != idToDelete )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; doc.Delete( idToDelete );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>Many thanks to Ishwar for this nice example!


<a name="2"></a>

<h4>Determining the Phase of a Room</h4>

<p>Here is another issue brought up and solved by 

<a href="http://www.hclausen.net">H&aring;kon Clausen</a>, 

whose 

<a href="http://thebuildingcoder.typepad.com/blog/2012/07/revitrubyshell-implementation-and-installer.html">
RevitRubyShell</a> I

recently waxed so enthusiastic about:

<p><strong>Question:</strong> How do I find which phase a room is in?

<p>From the UI, the phase is uneditable and is the same phase as the view you created it in. 
Unfortunately, the room CreatedPhaseId and DemolishedPhaseId properties are not valid (-1, InvalidElementId) and the Room class provides no explicit phase access.

I looked through your posts regarding phases, on

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/phase-dependent-room-properties.html">
phase dependent room properties</a> and 

the

<a href="http://thebuildingcoder.typepad.com/blog/2012/02/family-instance-room-phase.html">
family instance room phase</a>.

They do not help, as they only describe how to determine the phases on family instances, not on the room itself.

<p><strong>Answer:</strong> I solved it by reading the built-in parameter value for ROOM_PHASE_ID on the room element.

<p>On an interesting side note, I needed the same information for Mechanical.Space objects, and this also worked by accessing the same built-in parameter ROOM_PHASE_ID, which is a little obscure, I think...

<p>Thank you, H&aring;kon, for raising and clarifying this.

<p>The same credit also goes to Martino, who posted a 

<a href="http://thebuildingcoder.typepad.com/blog/2010/09/filter-for-view-and-phase.html?cid=6a00e553e1689788330176168faca8970c#comment-6a00e553e1689788330176168faca8970c">
comment</a> to 

the same effect on the

<a href="http://thebuildingcoder.typepad.com/blog/2010/09/filter-for-view-and-phase.html">
view and phase filtering</a> discussion.
