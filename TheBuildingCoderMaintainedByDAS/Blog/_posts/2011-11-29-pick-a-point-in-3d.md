---
layout: "post"
title: "Pick a Point in 3D"
date: "2011-11-29 14:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Transaction"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/11/pick-a-point-in-3d.html "
typepad_basename: "pick-a-point-in-3d"
typepad_status: "Publish"
---

<p>Today is the second day of AU for me, the first 'real' day of the conference.
I woke up at two in the morning, got back to sleep again eventually, but gave up at five and thus had plenty of time for a final private dress rehearsal for my lecture on the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/extensible-storage.html">
extensible</a>

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/extensible-storage-of-a-map.html">
storage</a> API

later today.
I needed that, since it is a couple of weeks since I submitted the material and all but forgot what it is about at all.
Now I will be busy all the rest of the day running back and forth between the DevLab and my lecture.

<p>On a completely different topic, here is a question on picking points in space that has already come up a few times:

<p><strong>Question:</strong> The Selection.PickPoint method is limited to selecting points on the active work plane. 
Is there any way to pick an arbitrary 3D point, regardless of work plane? 

<p><strong>Answer:</strong> Just as you say, the PickPoint method only allows point selection in the current work plane. 
As stated by the Revit API help file RevitAPI.chm, the various overloads of PickPoint prompt the user to pick a point on the active sketch plane using optionally specified 
snap settings. 
The Revit API does not offer any other method to pick a point arbitrarily in 3D space.

<p>So what if you need to pick a point somewhere in space that is not on the active work plane?

<p>Well, if you know a plane on which the point needs to be selected, you can temporarily set the active work plane to that plane.
This approach was actually already mentioned briefly in the discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2011/07/point-cloud-snap-and-freeze.html">
snapping to individual points in a point cloud</a>.
You need a sketch plane in your document in order to set the active work plane, so you may want to encapsulate the entire operation in a dummy transaction in which to create a temporary sketch plane for that purpose.

<p>I implemented a new external command CmdPickPoint3d in The Building Coder samples to demonstrate this by prompting the user to first select a face on an element and then pick a point on that face. 
The element face picked first is used to temporarily redefine the active work plane, on which the second point can be picked.

<p>This is implemented by the method PickFaceSetWorkPlaneAndPickPoint:

<pre class="code">
<span class="blue">bool</span> PickFaceSetWorkPlaneAndPickPoint( 
&nbsp; <span class="teal">UIDocument</span> uidoc,
&nbsp; <span class="blue">out</span> <span class="teal">XYZ</span> point_in_3d )
{
&nbsp; point_in_3d = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; <span class="teal">Reference</span> r = uidoc.Selection.PickObject( 
&nbsp; &nbsp; <span class="teal">ObjectType</span>.Face,
&nbsp; &nbsp; <span class="maroon">&quot;Please select a planar face to define work plane&quot;</span> );
&nbsp;
&nbsp; <span class="teal">Element</span> e = doc.get_Element( r.ElementId );
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != e )
&nbsp; {
&nbsp; &nbsp; <span class="teal">PlanarFace</span> face 
&nbsp; &nbsp; &nbsp; = e.GetGeometryObjectFromReference( r )
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">PlanarFace</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( face != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Plane</span> plane = <span class="blue">new</span> <span class="teal">Plane</span>(
&nbsp; &nbsp; &nbsp; &nbsp; face.Normal, face.Origin );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Transaction</span> t = <span class="blue">new</span> <span class="teal">Transaction</span>( doc );
&nbsp;
&nbsp; &nbsp; &nbsp; t.Start( <span class="maroon">&quot;Temporarily set work plane&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; to pick point in 3D&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">SketchPlane</span> sp = doc.Create.NewSketchPlane(
&nbsp; &nbsp; &nbsp; &nbsp; plane );
&nbsp;
&nbsp; &nbsp; &nbsp; uidoc.ActiveView.SketchPlane = sp;
&nbsp; &nbsp; &nbsp; uidoc.ActiveView.ShowActiveWorkPlane();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; point_in_3d = uidoc.Selection.PickPoint(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Please pick a point on the plane&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; defined by the selected face&quot;</span> );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">OperationCanceledException</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; t.RollBack();
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="blue">null</span> != point_in_3d;
}
</pre>

<p>Note that as always, we need to encapsulate the call to PickPoint in an exception handler in order to gracefully take care of the use cancelling the selection operation.

<p>PickFaceSetWorkPlaneAndPickPoint is driven by the following trivial Execute mainline:

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp;
&nbsp; <span class="teal">XYZ</span> point_in_3d;
&nbsp;
&nbsp; <span class="blue">if</span>( PickFaceSetWorkPlaneAndPickPoint( 
&nbsp; &nbsp; uidoc, <span class="blue">out</span> point_in_3d ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;3D Point Selected&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;3D point picked on the plane&quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; defined by the selected face: &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="teal">Util</span>.PointString( point_in_3d ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
&nbsp; <span class="blue">else</span>
&nbsp; {
&nbsp; &nbsp; message = <span class="maroon">&quot;3D point selection failed&quot;</span>;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; }
}
</pre>

<p>Here are a couple of screen snapshots showing this command in action:

<ul>
<li>Prompt the user to select a face on an element:

<p/>
<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330162fd160762970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330162fd160762970d" alt="Select face" title="Select face" src="/assets/image_d2c4ed.jpg" border="0" /></a><br />


<li>Set the active work plane and prompt the user to pick a point on it:

<p/>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330162fd16083a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330162fd16083a970d" alt="Pick point on plane" title="Pick point on plane" src="/assets/image_a87f20.jpg" border="0" /></a><br />

<li>Report the result:
</ul>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015393c0c4b4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015393c0c4b4970b" alt="Pick point result" title="Pick point result" src="/assets/image_160dc6.jpg" border="0" /></a><br />

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e16897883301543794188e970c"><a href="http://thebuildingcoder.typepad.com/files/bc_12_95-1.zip">version 2012.0.95.0</a></span> of

The Building Coder samples including the new CmdPickPoint3d command.
