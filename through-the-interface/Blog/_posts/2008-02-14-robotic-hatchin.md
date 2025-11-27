---
layout: "post"
title: "Robotic hatching inside AutoCAD using .NET"
date: "2008-02-14 07:50:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Hatches"
original_url: "https://www.keanw.com/2008/02/robotic-hatchin.html "
typepad_basename: "robotic-hatchin"
typepad_status: "Publish"
---

<p>This may strike you as a fairly bizarre title for a post, but I was inspired to develop the below code by <a href="http://international.husqvarna.com/node1555.aspx?cid=118">a robotic lawnmower</a> we bought about a year ago. This fantastic tool bounces around our garden, changing direction randomly when it hits the lawn's boundary. I got to thinking how to implement a similar technique to hatch a boundary with a polyline. While this is mostly for fun, I can see a few interesting potential uses: you might use the technique to test randomly generated paths of, for example, a robot or you might simply want to supplement traditional hatching with something more random in nature.</p>

<p>The principle is actually very simple: we're going to take a point on the edge of the boundary, and fire a ray (an infinite line) in a random direction - but planar to the boundary - and find out where it intersects the boundary. The tricky piece is that - depending on how &quot;jagged&quot; your boundary is - the ray may actually intersect multiple (i.e. &gt; 2) times. If we want to stay inside the boundary - a requirement for my little robotic hatcher - we need to exclude the segments that would lead us to exiting the boundary to reach the other end.</p>

<p>Excluding the &quot;bad&quot; segments actually proved to be the tricky part. I ended up taking a DevNote from the ADN site (<a href="http://adn.autodesk.com/adn/servlet/devnote?siteID=4814862&amp;id=5408893&amp;linkID=4900509">Testing Whether A Point Lies Inside A Curve</a>, for those of you who are ADN members), and converting the C++ code to C#. I won't comment much on the implementation - it's using a similar technique to the one I have in my own code, firing rays to determine intersections - but to be frank I'm only vaguely aware of how it works (ah, the joys of copy &amp; paste development :-).</p>

<p>Here's the converted code. I have it saved in a file called <em>point-in-curve.cs</em>, but clearly the specific name doesn't matter, as long as it is included in your project.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> PointInCurve</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Fns</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">enum</span> <span style="COLOR: teal">IncidenceType</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ToLeft = 0,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ToRight = 1,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ToFront = 2,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Unknown</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; };</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">IncidenceType</span> CurveIncidence(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Curve</span> cur,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">double</span> param,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector3d</span> dir,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector3d</span> normal</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector3d</span> deriv1 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cur.GetFirstDerivative(param);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (deriv1.IsParallelTo(dir))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Need second degree analysis</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Vector3d</span> deriv2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cur.GetSecondDerivative(param);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (deriv2.IsZeroLength() ||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;deriv2.IsParallelTo(dir))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: teal">IncidenceType</span>.ToFront;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (deriv2.CrossProduct(dir).</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; DotProduct(normal) &lt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: teal">IncidenceType</span>.ToRight;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: teal">IncidenceType</span>.ToLeft;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (deriv1.CrossProduct(dir).</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; DotProduct(normal) &lt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: teal">IncidenceType</span>.ToLeft;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: teal">IncidenceType</span>.ToRight;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> IsInsideCurve(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Curve</span> cur,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> testPt</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (!cur.Closed)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Cannot be inside</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Polyline2d</span> poly2d = cur <span style="COLOR: blue">as</span> <span style="COLOR: teal">Polyline2d</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (poly2d != <span style="COLOR: blue">null</span> &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; poly2d.PolyType != <span style="COLOR: teal">Poly2dType</span>.SimplePoly)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Not supported</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> ptOnCurve =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cur.GetClosestPointTo(testPt, <span style="COLOR: blue">false</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (<span style="COLOR: teal">Tolerance</span>.Equals(testPt, ptOnCurve))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">true</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Check it's planar</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Plane</span> plane = cur.GetPlane();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (!cur.IsPlanar)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Make the test ray from the plane</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector3d</span> normal = plane.Normal;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector3d</span> testVector =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; normal.GetPerpendicularVector();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Ray</span> ray = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Ray</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.BasePoint = testPt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.UnitDir = testVector;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3dCollection</span> intersectionPoints =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">Point3dCollection</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Fire the ray at the curve</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cur.IntersectWith(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ray,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Intersect</span>.OnBothOperands,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; intersectionPoints,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0, 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &nbsp; <span style="COLOR: blue">int</span> numberOfInters =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; intersectionPoints.Count;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (numberOfInters == 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Must be outside</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> nGlancingHits = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">double</span> epsilon = 2e-6; <span style="COLOR: green">// (trust me on this)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; numberOfInters; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Get the first point, and get its parameter</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point3d</span> hitPt = intersectionPoints[i];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> hitParam =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cur.GetParameterAtPoint(hitPt);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> inParam = hitParam - epsilon;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> outParam = hitParam + epsilon;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">IncidenceType</span> inIncidence =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; CurveIncidence(cur, inParam, testVector, normal);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">IncidenceType</span> outIncidence =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; CurveIncidence(cur, outParam, testVector, normal);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> ((inIncidence == <span style="COLOR: teal">IncidenceType</span>.ToRight &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;outIncidence == <span style="COLOR: teal">IncidenceType</span>.ToLeft) ||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(inIncidence == <span style="COLOR: teal">IncidenceType</span>.ToLeft &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;outIncidence == <span style="COLOR: teal">IncidenceType</span>.ToRight))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; nGlancingHits++;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> ((numberOfInters + nGlancingHits) % 2 == 1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>I then implemented my own code to make use of this library (in this case saved in <em>bounce-hatch.cs</em>):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> PointInCurve;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> BounceHatch</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Get a vector in a random direction</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: teal">Vector3d</span> randomUnitVector(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PlanarEntity</span> pl</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Create our random number generator</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.<span style="COLOR: teal">Random</span> ran =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> System.<span style="COLOR: teal">Random</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// First we get the absolute value</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// of our x and y coordinates</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">double</span> x = ran.NextDouble();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">double</span> y = ran.NextDouble();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Then we negate them, half of the time</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; x = -x;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; y = -y;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Create a 2D vector and return it as</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; 3D on our plane</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector2d</span> v2 = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Vector2d</span>(x, y);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: blue">new</span> <span style="COLOR: teal">Vector3d</span>(pl, v2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Allow tracing in 3 colours, depending on</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// whether the vector was accepted, rejected,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// or superseded by a better one</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">enum</span> <span style="COLOR: teal">TraceType</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Accepted = 0,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Rejected = 1,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Superseded = 2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; };</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">void</span> TraceSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> end,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">TraceType</span> type</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> vecCol = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">switch</span> (type)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">case</span> <span style="COLOR: teal">TraceType</span>.Accepted:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vecCol = 3;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">case</span> <span style="COLOR: teal">TraceType</span>.Rejected:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vecCol = 1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">case</span> <span style="COLOR: teal">TraceType</span>.Superseded:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vecCol = 2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Matrix3d</span> trans =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.CurrentUserCoordinateSystem.Inverse();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.DrawVector(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; start.TransformBy(trans),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; end.TransformBy(trans),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vecCol,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">false</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Test whether a segment goes outside our boundary</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">bool</span> TestSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Curve</span> cur,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Vector3d</span> vec</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Test 10 points along the segment...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (This is inefficient, but it's not a problem for</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; this application. Some of the redundant overhead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; of firing rays for each iteration could be factored</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; out, among other enhancements, I expect.)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">bool</span> result = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 1; i &lt; 10; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point3d</span> test = start + (vec * 0.1 * i);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Call into our IsInsideCurve library function,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// &quot;and&quot;-ing the results</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; result &amp;=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; PointInCurve.<span style="COLOR: teal">Fns</span>.IsInsideCurve(cur, test);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (!result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> result;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// For a particular boundary, get the next vertex on the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// curve, found by firing a ray in a random direction</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> nextBoundaryPoint(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Curve</span> cur,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">bool</span> trace</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Create and define our ray</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Ray</span> ray = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Ray</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.BasePoint = start;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.UnitDir =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; randomUnitVector(cur.GetPlane());</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get the intersection points until we</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// have at least 2 returned</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (will usually happen straightaway)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3dCollection</span> pts =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">Point3dCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cur.IntersectWith(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ray,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Intersect</span>.OnBothOperands,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pts,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 0, 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (pts.Count &lt; 2)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ray.UnitDir =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;randomUnitVector(cur.GetPlane());</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">while</span> (pts.Count &lt; 2);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &nbsp; <span style="COLOR: green">// For each of the intersection points - which</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// are points elsewhere on the boundary - let's</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// check to make sure we don't have to leave the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// area to reach them</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">bool</span> first = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">double</span> nextLen = 0.0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> nextPt = start;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">Point3d</span> pt <span style="COLOR: blue">in</span> pts)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Get the distance between this intersection</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and the last accepted point - both points</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// are on our ray</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Vector3d</span> vec = pt - start;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> len = vec.Length;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// If the vector length is positive and either</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the first to be a candidate or closer than</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the previous one (we generally select the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// closest non-zero option) then check it out</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// further</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (len &gt; <span style="COLOR: teal">Tolerance</span>.Global.EqualVector &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(first || len &lt; nextLen))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Run our tests to make sure the segment is</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// inside our boundary</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (TestSegment(cur, start, vec))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Draw the previous segment before overwriting</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (trace)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; TraceSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; nextPt,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">TraceType</span>.Superseded</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;nextLen = len;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;nextPt = pt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;first = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// This segment has been rejected,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// as it goes outside</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (trace)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; TraceSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pt,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">TraceType</span>.Rejected</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Draw our accepted segment and return it</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (nextLen &gt; <span style="COLOR: teal">Tolerance</span>.Global.EqualVector)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (trace)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; TraceSegment(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;nextPt,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">TraceType</span>.Accepted</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> nextPt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// If we didn't find a good segment, throw an</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// exception to be handled by the calling function</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">throw</span> <span style="COLOR: blue">new</span> <span style="COLOR: teal">Exception</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ErrorStatus</span>.PointNotOnEntity,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;Could not find another intersection point.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;BOUNCE&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> BounceHatch()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">bool</span> doTrace = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get various bits of user input</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptEntityOptions</span> peo =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptEntityOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nSelect point on closed loop: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptEntityResult</span> per =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetEntity(peo);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (per.Status != <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptIntegerOptions</span> pio =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptIntegerOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nEnter number of segments: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pio.DefaultValue = 500;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptIntegerResult</span> pir =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetInteger(pio);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pir.Status != <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptKeywordOptions</span> pko =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptKeywordOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nDisplay segment trace: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pko.Keywords.Add(<span style="COLOR: maroon">&quot;Yes&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pko.Keywords.Add(<span style="COLOR: maroon">&quot;No&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pko.Keywords.Default = <span style="COLOR: maroon">&quot;Yes&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptResult</span> pkr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetKeywords(pko);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pkr.Status != <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Check the selected object - make sure it's</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// a closed loop (could do some more checks here)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(per.ObjectId, <span style="COLOR: teal">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Curve</span> cur = obj <span style="COLOR: blue">as</span> <span style="COLOR: teal">Curve</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (cur == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nThis is not a curve.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (!cur.Closed)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nLoop is not closed.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Extract parameters from our user-input...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// A flag for our vector tracing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;doTrace = (pkr.StringResult == <span style="COLOR: maroon">&quot;Yes&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// The number of segments</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> numBounces = pir.Value;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// The first vertex of our path</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Point3d</span> latest =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; per.PickedPoint.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; TransformBy(ed.CurrentUserCoordinateSystem).</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;OrthoProject(cur.GetPlane());</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Create our polyline path, adding the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// initial vertex</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Polyline</span> path = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Polyline</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;path.Normal = cur.GetPlane().Normal;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;path.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; latest.Convert2d(cur.GetPlane()),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0.0, 0.0, 0.0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// For each segment, get the next vertex</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// and add it to the path</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> i = 1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">while</span> (i &lt;= numBounces)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> next =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;nextBoundaryPoint(cur, latest, doTrace);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; path.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;i++,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;next.Convert2d(cur.GetPlane()),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0, 0.0, 0.0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; latest = next;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">catch</span> (<span style="COLOR: teal">Exception</span> ex)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// If there's an exception we know about</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// then ignore it and allow the loop to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// continue (we probably did not increment</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// i in this case, as it will fail on</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// nextBoundaryPoint)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (ex.ErrorStatus !=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">ErrorStatus</span>.PointNotOnEntity)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">throw</span> ex;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Open the modelspace</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTable</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTableRecord</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// We need to transform the path polyline so</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// that it's over our boundary</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;path.TransformBy(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Matrix3d</span>.Displacement(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cur.StartPoint - <span style="COLOR: teal">Point3d</span>.Origin</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add our path to the modelspace</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;btr.AppendEntity(path);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.AddNewlyCreatedDBObject(path, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Commit, whether we added a path or not.</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// If we're tracing, pause for user input</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// before regenerating the graphics</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (doTrace)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pko =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptKeywordOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nPress return to clear trace vectors: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pko.AllowNone = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pko.AllowArbitraryInput = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pkr = ed.GetKeywords(pko);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.Regen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>As I've mentioned in the above code, this is not the most efficient possible implementation: we do several (currently 10) checks per potential vertex, to see whether it needs to be excluded. This number might well be reduced, or the library could be updated to provide a more efficient implementation. But for our purposes it works fine, so I'm not going to worry too much. Optimization is left as an exercise for the reader. :-)</p>

<p>Here's what happens when we run the BOUNCE command on a couple of boundaries. The first one I just created for testing - it's a lightweight polyline with three downward prongs that makes it likely that rays fired from the boundary will intersect it multiple (&gt;2) times. For each of these examples I've composed three views: the boundary (pre-hatching), the trace vectors displayed to show the successful vectors (in green) and the excluded vectors (in red), and then the final hatch.</p>

<p>This first loop has been hatched with 100 segments, to clearly show the way the pattern works (or can work, as it's randomly generated).</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=199,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2008/02/13/basic_loop.png"><img title="Basic_loop" height="111" alt="Basic_loop" src="/assets/basic_loop.png" width="450" border="0" /></a> </p>

<p>The second loop I started drawing with straight segments and then switched to arcs. Somehow I ended up creating something that looks like it's out of <a href="http://en.wikipedia.org/wiki/Xenomorph_%28Alien%29">Alien</a>, although I wasn't (consciously, at least) in any way inspired by <a href="http://en.wikipedia.org/wiki/HR_Giger">Giger</a> (even though he's Swiss and has a <a href="http://www.hrgigermuseum.com/">museum</a> in nearby <a href="http://en.wikipedia.org/wiki/Gruy%C3%A8res">Gruyères</a>). Anyway, this one I hatched with 1000 segments, selecting the initial point on the boundary of the left-most leg: you can see that even with that many segments, not every piece of the boundary was reached using a random algorithm.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=209,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2008/02/13/strange_loop.png"><img title="Strange_loop" height="117" alt="Strange_loop" src="/assets/strange_loop.png" width="450" border="0" /></a></p>

<p><strong><em>Update:</em></strong></p>

<p>While working with this a little more, I realised I was not disposing the ray objects we were using for intersection calculations. I've now fixed the above code by inserting calls to ray.Dispose() in the appropriate places.</p>
