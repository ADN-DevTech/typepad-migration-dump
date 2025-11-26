---
layout: "post"
title: "Beam Bottom Endpoint"
date: "2012-10-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Data Access"
  - "Element Relationships"
  - "Family"
  - "Geometry"
  - "Parameters"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/10/beam-bottom-endpoint.html "
typepad_basename: "beam-bottom-endpoint"
typepad_status: "Publish"
---

<p>Here is a nice little example illustrating different approaches to find the end point of the bottom centre line of an inclined beam.


<p><strong>Question:</strong> I am trying to find the coordinates of the end points of the bottom centre line of an inclined beam. 
Depending on the beam angle, these coordinates will change.</p>


<p>Here is a 3D view of the kind of situation I am looking at:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee4405e1b970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee4405e1b970d" alt="Inclined beams 3D view" title="Inclined beams 3D view" src="/assets/image_a3ceae.jpg" border="0" /></a><br />

</center>

<p>The front view looks like this with the beam end points marked in orange.
The desired bottom line end points are marked by red points within the circled areas:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d3ccb0781970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d3ccb0781970c" alt="Inclined beams front view" title="Inclined beams front view" src="/assets/image_6bafa9.jpg" border="0" /></a><br />

</center>

<p>I am currently using the following code to retrieve the beam location line start and end points: 

<pre class="code">
&nbsp; <span class="teal">LocationCurve</span> location = beam.Location <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp; <span class="teal">Curve</span> c = location.Curve;
&nbsp; <span class="teal">XYZ</span> p = c.get_EndPoint( 0 );
&nbsp; <span class="teal">XYZ</span> q = c.get_EndPoint( 1 );
</pre>


<p><strong>Answer:</strong> There are an infinite number of ways to achieve this.
Here are three to start with, in order of increasing purely personal preference:

<ul>
<li><a href="#2">Face iteration and project</a>
<li><a href="#3">Parameter values and trigonometry</a>
<li><a href="#4">Perpendicular vector</a>
</ul>


<a name="2"></a>

<h4>Face Iteration and Project</h4> One way to approach this is to use the beam solid faces and projection methods provided by the Revit geometry API to calculate the desired points.

<p>You can determine the bottom face of the beam by querying the beam for its solid, iterating over its faces, and searching for the face whose normal vector has a minimal Z value, i.e. points downward the most.
I provided source code samples implementing this for the

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/bottom-face-of-a-wall.html">
bottom</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2011/12/top-faces-of-sloped-wall-update.html">
top faces of a wall</a>.

<p>You can then project the beam location line end points onto that face using the Face.Project method to obtain the desired bottom line end points.



<a name="3"></a>

<h4>Parameter Values and Trigonometry</h4>

<p>A second solution is based on querying some of the beam parameter values and using a little bit of trigonometry. 

<p>It calculates the sides of the right angle triangle using the beam direction vector, vertical delta and insertion value and returns the beam bottom coordinate as follows in C#:

<pre class="code">
<span class="blue">void</span> GetBeamInsertionAndVertDelta(
&nbsp; <span class="teal">Element</span> beam,
&nbsp; <span class="blue">double</span> sideLength,
&nbsp; <span class="blue">out</span> <span class="blue">double</span> beamInsertion,
&nbsp; <span class="blue">out</span> <span class="blue">double</span> beamVertDelta )
{
&nbsp; <span class="green">// sideLength is beam height. </span>
&nbsp; <span class="green">// If there is any other element like plate to </span>
&nbsp; <span class="green">// be attached to beam bottom then </span>
&nbsp; <span class="green">// sideLength = beam height + plate thickness</span>
&nbsp;
&nbsp; beamInsertion = 0.0;
&nbsp; beamVertDelta = 0.0;
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != beam &amp;&amp; _eps &lt; <span class="teal">Math</span>.Abs( sideLength ) )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Parameter</span> cutLength = beam.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_FRAME_CUT_LENGTH );
&nbsp;
&nbsp; &nbsp; <span class="teal">Parameter</span> stOffset = beam.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END0_ELEVATION );
&nbsp;
&nbsp; &nbsp; <span class="teal">Parameter</span> endOffset = beam.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END1_ELEVATION );
&nbsp;
&nbsp; &nbsp; <span class="blue">double</span> hypotenuse = cutLength.AsDouble();
&nbsp;
&nbsp; &nbsp; <span class="blue">double</span> side = <span class="teal">Math</span>.Abs( stOffset.AsDouble() 
&nbsp; &nbsp; &nbsp; - endOffset.AsDouble() );
&nbsp;
&nbsp; &nbsp; <span class="blue">double</span> angle = <span class="teal">Math</span>.Acos( side / hypotenuse );
&nbsp;
&nbsp; &nbsp; beamInsertion = ( sideLength / <span class="teal">Math</span>.Tan( angle ) );
&nbsp; &nbsp; beamVertDelta = ( sideLength / <span class="teal">Math</span>.Sin( angle ) );
&nbsp; }
}
</pre>


<a name="4"></a>

<h4>Perpendicular Vector</h4>

<p>The simplest solution may possibly be to avoid using the beam geometry as well as the tan and sin functions and just calculate straight away based on the beam location line.

<p>You can determine the vector perpendicular to the beam location line and pointing downward as much as possible, i.e. lying in the plane P defined by the (non-vertical!) location line and the global Z axis.

<p>Let's say the beam location line top two endpoints are stored in 'p' and 'q'. 
To determine the bottom points, we just need to know the height 'h' of the beam cross section and calculate a vector 'v' of length 'h' pointing downward and perpendicular to the beam location line. 
The desired points are given by p + v and q + v.

<p>I used a similar calculation to determine the wall width or thickness vector 'w' to display the 

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-compound-layers.html">
wall compound layers</a>,

except that it is horizontal, i.e. has a zero Z component, instead of lying in the plane P, with a minimal (negative) Z component.

<p>Something like this untested snippet should do the job, given the beam location curve start point 'p', end point 'q' and height 'h' as defined above:

<pre class="code">
&nbsp; <span class="teal">XYZ</span> u = q - p;
&nbsp; <span class="teal">XYZ</span> w = <span class="teal">XYZ</span>.BasisZ.Cross( u );
&nbsp; <span class="teal">XYZ</span> v = h * w.Cross( u ).Normalized;
</pre>

<p>As said, the desired points are then given by p + v and q + v.


<a name="5"></a>

<h4>Determine Hosted Family Host Type</h4>

<p>On a different topic, in his latest AEC DevBlog post, Saikat Bhattacharya explains how you can 

<a href="http://adndevblog.typepad.com/aec/2012/10/determining-if-a-family-was-created-using-wall-floor-face-ceiling-or-roof-based-templates.html">
determine the base host type of a hosted family</a> via 

the Host parameter on the family document owner family property, which stores the following integer values corresponding to the FamilyHostingBehavior enumeration:

<ol start="0">
<li>None  
<li>Wall  
<li>Floor  
<li>Ceiling  
<li>Roof  
<li>Face 
</ol>

<p>The parameter value is zero, i.e. None, for families created using line and pattern based templates, so their host type cannot be determined using this method. 

<p>Note that if you have a face-based family <b>instance</b>, you can query its Host and HostFace properties to retrieve the host element and a reference to the hosting face, respectively.

<p>Two more interesting methods to explore in this context are the FamilyCanConvertToFaceHostBased and ConvertFamilyToFaceHostBased methods provided by the FamilyUtils class.
