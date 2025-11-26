---
layout: "post"
title: "Top Faces of Sloped Wall Update"
date: "2011-12-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Data Access"
  - "Geometry"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/12/top-faces-of-sloped-wall-update.html "
typepad_basename: "top-faces-of-sloped-wall-update"
typepad_status: "Publish"
---

<p>On Saturday we left Paris and arrived in Göteborg, or Go:teborg as the local tourist office appears to like spelling it.

<p>On Sunday we went for a trip on one of the public transportation ferry boats out to Vr&aring;ng&ouml;.
From right to left you see Jim, Partha, Philippe, and me, and Adam is taking the picture:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301675ea126b8970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301675ea126b8970b image-full" alt="DevTech team on Vr&aring;ng&ouml;" title="DevTech team on Vr&aring;ng&ouml;" src="/assets/image_38b8da.jpg" border="0" /></a><br />

</center>

<p>Fittingly enough, I also had some correspondence on a Revit API issue with a Swedish developer on the same day:
Henrik Bengtsson of

<a href="http://www.lindab.se">
Lindab</a> submitted a

<a href="http://thebuildingcoder.typepad.com/blog/2011/07/top-faces-of-wall.html?cid=6a00e553e1689788330162fda970a9970d#comment-6a00e553e1689788330162fda970a9970d">
comment</a> on 

the retrieval of the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/07/top-faces-of-wall.html">
top faces of a sloped wall</a>:

<blockquote>
<em>
<p>When I tried parts of this code, I discovered that the .Normal function of the PlanarFace object sometimes shows incorrect values for faces with a positive or negative z-direction.

<p>This especially affects the code part:
</em>

<pre class="code">
if( f is PlanarFace && PointsUpwards( ((PlanarFace)f).Normal ) )
</pre>

<em>
<p>A solution that so far has given me correct values is to replace the .Normal function of the PlanarFace object with the .ComputeNormal function.

<p>I found problems when analyzing faces that had a positive or negative z-value of its normal. So, not only the boundary faces of the wall but also top and bottom faces of its openings...
</em>
</blockquote>

<p>Henrik also sent me a simple model with two simple quadrilateral sloped walls in which the existing 

<a href="http://thebuildingcoder.typepad.com/blog/2011/07/top-faces-of-wall.html">
CmdWallTopFaces</a> command 

implementation fails:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154382b4c42970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154382b4c42970c" alt="Two sloped walls" title="Two sloped walls" src="/assets/image_34e025.jpg" border="0" /></a><br />

</center>

<p>The command reports zero top faces:</p>

<pre>
0 top faces found on Walls <194639 Lindab FR E120/120 202 M0 c450>
0 top faces found on Walls <195080 Lindab FR E120/120 202 M0 c450>
</pre>

<p>This is obviously false:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154382b4a2c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154382b4a2c970c" alt="Wall top faces" title="Wall top faces" src="/assets/image_c87701.jpg" border="0" /></a><br />

</center>

<p>Henrik implemented the following improved solution in VB calling ComputeNormal:

<pre class="code">
&nbsp; <span class="blue">For</span> <span class="blue">Each</span> f <span class="blue">As</span> DB.<span class="teal">Face</span> <span class="blue">In</span> <span class="teal">Solid</span>.Faces
&nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; <span class="blue">If</span> <span class="blue">TypeOf</span> f <span class="blue">Is</span> DB.<span class="teal">PlanarFace</span> <span class="blue">Then</span>
&nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; <span class="blue">Dim</span> pf <span class="blue">As</span> DB.<span class="teal">PlanarFace</span> = f

&nbsp; &nbsp; &nbsp; <span class="blue">Dim</span> p <span class="blue">As</span> <span class="teal">XYZ</span> = pf.Origin

&nbsp; &nbsp; &nbsp; <span class="blue">If</span> pf.ComputeNormal(<span class="blue">New</span> DB.<span class="teal">UV</span>(p.X, p.Y)).Z &gt; 0 <span class="blue">Then</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">Dim</span> faceVertices <span class="blue">As</span> <span class="teal">IList</span>(<span class="blue">Of</span> DB.<span class="teal">XYZ</span>) _
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = pf.Triangulate().Vertices

&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">For</span> <span class="blue">Each</span> v <span class="blue">As</span> DB.<span class="teal">XYZ</span> <span class="blue">In</span> faceVertices
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">If</span> sideVertices.Contains(v, comparer) <span class="blue">Then</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">If</span> <span class="blue">Not</span> ret.Contains(f) <span class="blue">Then</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ret.Add(f)
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">End</span> <span class="blue">If</span>

&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">Exit For</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">End</span> <span class="blue">If</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">Next</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; &nbsp; <span class="blue">End</span> <span class="blue">If</span>
&nbsp; &nbsp; &nbsp; <span class="green">'</span>
&nbsp; &nbsp; <span class="blue">End</span> <span class="blue">If</span>
&nbsp; &nbsp; <span class="green">'</span>
&nbsp; <span class="blue">Next</span>
</pre>

<p>He says:

<blockquote>
<em>
<p>I tried the .ComputeNormal function and it helps solving the problem. The .Normal function shows an incorrect x as well as z value for the walls that was in the project file.

<p>Depending on the vector of the top face, I am sure that x, y and z-directions will be incorrect. 

<p>The function that this routine is implemented in looks like this (more or less a VB copy of your blog code):

<p>I have tried a couple of walls and sometimes a wall works correct, then if I run a mirror command on it, the new one becomes incorrect. 

<p>A positive or negative top face slope has showed to have an impact as well.

<p>If there are openings inside the wall they can have an incorrect .Normal value as well. That was actually how I discovered it. The top face of a wall was pointing upwards
and ended up in my collection as well.
</em>
</blockquote>

<p>I decided I might as well clean up this command a bit more to ensure that top faces are really found.

<p>If we make use of the Face.ComputeNormal method instead of the planar face normal vector, we might as well remove the restriction to planar faces at the same time.

<p>To handle all kinds of faces, I can use the following simple method to determine whether a given face is facing upwards:

<ul>
<li>Determine the face UV bounding box.
<li>Pick a UV point in the middle.
<li>Call ComputeNormal to determine the face normal vector at that point.
<li>Check whether it points upwards.
</ul>

<p>This is still an extremely simplistic test, so there is no guarantee that it will handle the general case.
For instance, if the face has holes or a complex boundary, the middle point calculated may not belong to it at all.
It is up to you to test it for the cases you need.

<p>The command already implements a bunch of other stuff to eliminate faces that belong to openings, and thus are not top faces of the wall, but just faces into the wall openings.
I left that part completely untouched.

<p>I first replaced the line of code that I was previously using which was only checking for planar faces by a call to this new method which does the same thing:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Super-simple test whether a face is planar </span>
&nbsp; <span class="gray">///</span><span class="green"> and its normal vector points upwards.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">bool</span> IsTopPlanarFace( <span class="teal">Face</span> f )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> f <span class="blue">is</span> <span class="teal">PlanarFace</span>
&nbsp; &nbsp; &nbsp; &amp;&amp; PointsUpwards( ( (<span class="teal">PlanarFace</span>) f ).Normal );
&nbsp; }
</pre>

<p>I then went and implemented the new more general approach described above like this to replace it:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Simple test whether a given face normal vector </span>
&nbsp; <span class="gray">///</span><span class="green"> points upwards in the middle of the face.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">bool</span> IsTopFace( <span class="teal">Face</span> f )
&nbsp; {
&nbsp; &nbsp; <span class="teal">BoundingBoxUV</span> b = f.GetBoundingBox();
&nbsp; &nbsp; <span class="teal">UV</span> p = b.Min;
&nbsp; &nbsp; <span class="teal">UV</span> q = b.Max;
&nbsp; &nbsp; <span class="teal">UV</span> midpoint = p + 0.5 * ( q - p );
&nbsp; &nbsp; <span class="teal">XYZ</span> normal = f.ComputeNormal( midpoint );
&nbsp; &nbsp; <span class="blue">return</span> PointsUpwards( normal );
&nbsp; }
</pre>

<p>Lo and behold! 
Running this on the simple model provided by Henrik returned the two faces, just as expected:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154382b4787970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154382b4787970c" alt="Wall top face edges" title="Wall top face edges" src="/assets/image_473b4a.jpg" border="0" /></a><br />

</center>

<p>Problem apparently fixed, a least for this simple case.

<p>I also ran the command on rac_basic_sample_project.rvt again, like I did the first implementation.
It now reports 119 walls selected with 34 top faces when I select all walls in the Level 1 plan view, and 605 walls selected with 47 top faces when I do it in 3D view.
These results are both different from what I found in the simplified model last time, but I am not going to worry about that.

<p>Anyway, here is 

<span class="asset  asset-generic at-xid-6a00e553e16897883301675ea11d7c970b"><a href="http://thebuildingcoder.typepad.com/files/bc_12_96_2.zip">version 2012.0.96.2</a></span> of

The Building Coder samples including the updated command.
I am looking forward to hearing what new issues you run into with it.

<p>Many thanks to Henrik for prompting this improvement!
