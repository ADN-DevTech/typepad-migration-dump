---
layout: "post"
title: "Sphere packing in AutoCAD: creating an Apollonian packing using F# &ndash; Part 1"
date: "2012-02-16 06:24:00"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Fractals"
  - "Geometry"
  - "Solid modeling"
original_url: "https://www.keanw.com/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-1.html "
typepad_basename: "sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-1"
typepad_status: "Publish"
---

<p>So far in this series, we’ve looked at Apollonian circle packing <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-net.html" target="_blank">using C#</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-1.html" target="_blank">also</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-2.html" target="_blank">F#</a>. The next few posts will look at solving this problem in 3D: performing Apollonian sphere packing. I’ve decided to stay in F# for the algorithmic side of things: it just feels a much cleaner environment for dealing with this kind of problem, and, besides, I’ve been having too much fun with it. :-)</p>
<p>One thing I should mention, as this series is nominally about 3D printing fill algorithms: printing hollow spheres isn’t at all straightforward with today’s 3D printing technology, as support material is needed to keep the internal arches from collapsing. This material can be water soluble, but then the material around the spherical hollows needs to be porous (or at least have some holes) to allow water to pass through it. So this series is still largely theoretical, in many ways, until such a time as hollow spheres can effectively be printed. But anyway – onwards and upwards. I’ve never been one to let feasibility to get in the way of pursuing something interesting.</p>
<p>Before we get onto the technical details, there’s something else I’d like to reiterate: I have been utterly dependent on the goodwill of others for the underlying mathematics/algorithms allowing creation of this geometry. I’ve begged and borrowed (I don’t like to use the term “stolen” ;-) code from a few different of places, and have even reached out to academia for help. More on that later.</p>
<p>When looking into the 3D side of this problem, I found relatively few websites that were ultimately of help.</p>
<p>Paul Bourke has <a href="http://paulbourke.net/fractals/apollony" target="_blank">a nice site</a>, although the posted code dealt with 2D rather than 3D. <a href="http://www.hiddendimension.com/FractalMath/CircleInversionFractals.html" target="_blank">This site</a> provided some useful information – and a simple algorithm – although ultimately didn’t lead to me being able to create working code.</p>
<p>There were also a couple of <a href="http://graphics.ethz.ch/~peikert/papers/apollonian.pdf" target="_blank">academic</a> <a href="http://home.pf.jcu.cz/~sbml/wp-content/uploads/gergel1.pdf" target="_blank">papers</a> that provided some helpful insights.</p>
<p>My first “breakthrough” in terms of getting actual code working was the discovery of <a href="http://shiny3d.de/mandelbrot-dazibao/Apollo/Apollo.htm" target="_blank">The Mandelbot Dazibao</a>, which included some BASIC code creating 3D Apollonian fills. It’s this code that we’ll be looking at, today.</p>
<p>A few words on what’s coming in the coming posts…</p>
<p>Before getting today’s post working, I had also reached out to Thomas Bonner, the author of <a href="http://thomasbonner.heliohost.org/apolfrac.htm" target="_blank">ApolFrac</a>. ApolFrac is a very cool tool to visualize – but unfortunately not export – Apollonian packings. Thomas was very helpful, pointing me at the algorithm he had used for ApolFrac, which I’d actually already seen (but obviously hadn’t understood) in one of the aforementioned academic papers.</p>
<p>My second breakthrough came from reaching out to <a href="http://www.scivis.ethz.ch/people/rpeikert/" target="_blank">Professor Ronald Peikert</a> from <a href="http://www.ethz.ch/index_EN" target="_blank">ETH Zurich</a>, who very kindly sent some C++ code that essentially implements a version of the algorithm Thomas had pointed me to. We’ll look at that in the next post.</p>
<p>So yes, back to breakthrough #1. Here’s the F# code I generated from The Mandelbrot Dazibao, which I believe uses <a href="http://mathworld.wolfram.com/TangentSpheres.html" target="_blank">the Soddy-Gosset theorem</a> as an alternative to (or really an extension of) Descartes’ theorem.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">module</span><span style="line-height: 140%;"> SpherePackingFs</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> maxSpheres = 8000</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> spheres = Array.create(maxSpheres)(0.,0.,0.,0.,0,0,0,0,0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">mutable</span><span style="line-height: 140%;"> idc = 0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> createSphere (x,y,z,r) id1 id2 id3 id4 rank =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; spheres.[idc] &lt;- x,y,z,r,id1,id2,id3,id4,rank</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idc &lt;- idc + 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idc - 1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> getSpheres() =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [</span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> 0..(idc-1) </span><span style="line-height: 140%; color: blue;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (x,y,z,r,id1,id2,id3,id4,rank) = spheres.[i]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">yield</span><span style="line-height: 140%;"> ((x,y,z,r),rank)]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> generateMatrix</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; (x1:double, y1:double, z1:double, r1:double)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; (x2:double, y2:double, z2:double, r2:double)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; (x3:double, y3:double, z3:double, r3:double)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; (x4:double, y4:double, z4:double, r4:double) =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> a = Matrix.create 5 5 0.0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(1,1) &lt;- 2. * (x1 - x2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(2,1) &lt;- 2. * (x2 - x3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(3,1) &lt;- 2. * (x3 - x4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(4,1) &lt;- 2. * (x4 - x1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(1,2) &lt;- 2. * (y1 - y2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(2,2) &lt;- 2. * (y2 - y3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(3,2) &lt;- 2. * (y3 - y4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(4,2) &lt;- 2. * (y4 - y1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(1,3) &lt;- 2. * (z1 - z2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(2,3) &lt;- 2. * (z2 - z3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(3,3) &lt;- 2. * (z3 - z4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(4,3) &lt;- 2. * (z4 - z1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(1,4) &lt;- 2. * (r1 - r2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(2,4) &lt;- 2. * (r2 - r3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(3,4) &lt;- 2. * (r3 - r4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(4,4) &lt;- 2. * (r4 - r1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(1,0) &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; -(x1**2.-x2**2.+y1**2.-y2**2.+z1**2.-z2**2.+r2**2.-r1**2.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(2,0) &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; -(x2**2.-x3**2.+y2**2.-y3**2.+z2**2.-z3**2.+r3**2.-r2**2.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(3,0) &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; -(x3**2.-x4**2.+y3**2.-y4**2.+z3**2.-z4**2.+r4**2.-r3**2.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a.Item(4,0) &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; -(x4**2.-x1**2.+y4**2.-y1**2.+z4**2.-z1**2.+r1**2.-r4**2.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; a</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> apollonianSphere id1 id2 id3 id4 offset =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (x1,y1,z1,r1,id11,id12,id13,id14,rank1) = spheres.[id1]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (x2,y2,z2,r2,id21,id22,id23,id24,rank2) = spheres.[id2]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (x3,y3,z3,r3,id31,id32,id33,id34,rank3) = spheres.[id3]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (x4,y4,z4,r4,id41,id42,id43,id44,rank4) = spheres.[id4]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rank = List.max [rank1;rank2;rank3;rank4]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> r1 = r2 &amp;&amp; r1 = r3 &amp;&amp; r1= r4 </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; createSphere</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (0.,0.,(-r1 * sqrt(1./6.) + offset),(r1 * (sqrt(3./2.)-1.)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; id1 id2 id3 id4 (rank+1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; |&gt; ignore</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">(*</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; The four equations to be solved are:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; (x-x1)^2+(y-y1)^2+(z-z1)^2 = (r+r1)^2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; (x-x2)^2+(y-y2)^2+(z-z2)^2 = (r+r2)^2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; (x-x3)^2+(y-y3)^2+(z-z3)^2 = (r+r3)^2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; (x-x4)^2+(y-y4)^2+(z-z4)^2 = (r+r4)^2</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; Subtract them 2 by 2:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; x*2*(x1-x2) + y*2*(y1-y2) + z*2*(z1-z2) + r*2*(r1-r2) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160; x1^2-x2^2 + y1^2-y2^2 + z1^2-z2^2 + r2^2-r1^2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; x*2*(x2-x3) + y*2*(y2-y3) + z*2*(z2-z3) + r*2*(r2-r3) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160; x2^2-x3^2 + y2^2-y3^2 + z2^2-z3^2 + r3^2-r2^2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; x*2*(x3-x4) + y*2*(y3-y4) + z*2*(z3-z4) + r*2*(r3-r4) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160; x3^2-x4^2 + y3^2-y4^2 + z3^2-z4^2 + r4^2-r3^2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; x*2*(x4-x1) + y*2*(y4-y1) + z*2*(z4-z1) + r*2*(r4-r1) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160;&#0160; x4^2-x1^2 + y4^2-y1^2 + z4^2-z1^2 + r1^2-r4^2</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; Matrix writing:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(1,1)*x+a(1,2)*y+a(1,3)*z+a(1,4)*r+a(1,0) = 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(2,1)*x+a(2,2)*y+a(2,3)*z+a(2,4)*r+a(2,0) = 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(3,1)*x+a(3,2)*y+a(3,3)*z+a(3,4)*r+a(3,0) = 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(4,1)*x+a(4,2)*y+a(4,3)*z+a(4,4)*r+a(4,0) = 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; *)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">mutable</span><span style="line-height: 140%;"> a =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; generateMatrix</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (x1,y1,z1,r1) (x2,y2,z2,r2) (x3,y3,z3,r3) (x4,y4,z4,r4)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> a.Item(3,3) = 0. </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; a &lt;-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; generateMatrix</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (x4,y4,z4,r4) (x2,y2,z2,r2) (x3,y3,z3,r3) (x1,y1,z1,r1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">(*</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; Get x, y and z as functions of r</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(1,1)*x+a(1,2)*y+a(1,3)*z=-a(1,4)*r-a(1,0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(2,1)*x+a(2,2)*y+a(2,3)*z=-a(2,4)*r-a(2,0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; a(3,1)*x+a(3,2)*y+a(3,3)*z=-a(3,4)*r-a(3,0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; *)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> det (a : matrix) m n p q =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; a.Item(m,p) * a.Item(n,q) - a.Item(n,p) * a.Item(m,q)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> D1313 = det a 1 3 1 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> D2323 = det a 2 3 2 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> D1323 = det a 1 3 2 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> D2313 = det a 2 3 1 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> DetMajor = D2313 * D1323 - D1313 * D2323</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> RX =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (a.Item(1,4) * a.Item(3,3) * D2323 - a.Item(2,4) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; a.Item(3,3) * D1323 - a.Item(3,4) * a.Item(1,3) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; D2323 + a.Item(3,4) * a.Item(2,3) * D1323) / DetMajor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> RY =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (-a.Item(1,4) * a.Item(3,3) * D2313 + a.Item(2,4) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; a.Item(3,3) * D1313 - a.Item(3,4) * a.Item(2,3) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; D1313 + a.Item(3,4) * a.Item(1,3) * D2313) / DetMajor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> RX0 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (a.Item(1,0) * a.Item(3,3) * D2323 - a.Item(2,0) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; a.Item(3,3) * D1323 - a.Item(3,0) * a.Item(1,3) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; D2323 + a.Item(3,0) * a.Item(2,3) * D1323) / DetMajor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> RY0 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (-a.Item(1,0) * a.Item(3,3) * D2313 + a.Item(2,0) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; a.Item(3,3) * D1313 - a.Item(3,0) * a.Item(2,3) *</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; D1313 + a.Item(3,0) * a.Item(1,3) * D2313) / DetMajor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> RZ =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; -a.Item(3,4) / a.Item(3,3) - RX * a.Item(3,1) / a.Item(3,3) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; a.Item(3,2) / a.Item(3,3) * RY</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> RZ0 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; -a.Item(3,0) / a.Item(3,3) - RX0 * a.Item(3,1) / a.Item(3,3) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; RY0 * a.Item(3,2) / a.Item(3,3)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> Pp = RX**2. + RY**2. + RZ**2. - 1.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> Pq =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 2. * (RX * (RX0-x4) + RY * (RY0-y4) + RZ * (RZ0-z4) - r4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> Pr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (RX0-x4)**2. + (RY0-y4)**2. + (RZ0-z4)**2. - r4**2.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> Delta = Pq**2. - 4. * Pp * Pr</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> Radius = (-Pq - sqrt(Delta)) / 2. / Pp</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> x = RX * Radius + RX0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> y = RY * Radius + RY0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> z = RZ * Radius + RZ0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; createSphere (x,y,z,Radius) id1 id2 id3 id4 (rank+1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; |&gt; ignore</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> fleshOut maxRank offset =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> curRank </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> 1..maxRank </span><span style="line-height: 140%; color: blue;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> id </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> 1..(idc-1) </span><span style="line-height: 140%; color: blue;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> (x,y,z,r,id1,id2,id3,id4,rank) = spheres.[id]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> rank = curRank </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; apollonianSphere id id1 id2 id3 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; apollonianSphere id id1 id2 id4 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; apollonianSphere id id1 id3 id4 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; apollonianSphere id id2 id3 id4 offset</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> exteriorGasket c0 c1 c2 c3 c4 offset steps =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idc &lt;- 0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c0 = createSphere c0 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c1 = createSphere c1 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c2 = createSphere c2 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c3 = createSphere c3 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c4 = createSphere c4 0 0 0 0 0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; apollonianSphere c0 c1 c2 c3 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; apollonianSphere c0 c1 c2 c4 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; apollonianSphere c0 c1 c3 c4 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; apollonianSphere c0 c2 c3 c4 offset</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; apollonianSphere c1 c2 c3 c4 offset</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; fleshOut steps offset</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; getSpheres()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> interiorGasket c0 c1 c2 c3 offset steps =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; idc &lt;- 0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c0 = createSphere c0 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c1 = createSphere c1 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c2 = createSphere c2 0 0 0 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c3 = createSphere c3 0 0 0 0 0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; apollonianSphere c0 c1 c2 c3 offset</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; fleshOut steps offset</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; getSpheres()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">type</span><span style="line-height: 140%;"> Packer() =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">member</span><span style="line-height: 140%;"> ExteriorApollonianGasket steps =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> size = 500.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt1o6 = sqrt(1./6.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt3 = sqrt(3.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt2o3 = sqrt(2./3.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> offset = size * rt1o6</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> outerRad = size * (2. + (1. / (2. * rt1o6)))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; exteriorGasket</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (0., 0., (offset - (size * rt1o6)), outerRad)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (((2. * size) / rt3), 0., offset, size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (0., 0., (offset - (2. * size * rt2o3)), size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ((-size / rt3), size, offset, size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ((-size / rt3), -size, offset, size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; offset steps</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">member</span><span style="line-height: 140%;"> InteriorApollonianGasket steps =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> size = 500.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt3 = sqrt(3.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt2o3 = sqrt(2./3.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> offset = size * 0.5</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; interiorGasket</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ((2. * size / rt3), 0., offset, size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (0., 0., (offset - (2. * size * rt2o3)), size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ((-size / rt3), size, offset, size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ((-size / rt3), -size, offset, size)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; offset steps</span></p>
</div>
<p>As the original code was in BASIC, I’ve found it simplest to keep certain approaches in place: rather than building – and returning – a list of spheres to create, the code builds an fixed-size array and populates it with the results. Which means there are fundamental limits to the number of spheres that can be generated (and if you increase the number of levels to generate, you also need to be aware you may need to increase the size of the array). This isn’t ideal, by any means, but I’ve left the implementation fairly similar to the original, especially as it’s ultimately being superseded by the code in the next post.</p>
<p>To make use of the F# code, I’ve added this C# loader to the project (which I’ll post next time):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> SpherePackingLoader</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFSE&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ExteriorApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ApollonianGasket(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFSI&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> InteriorApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ApollonianGasket(</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CreateLayers(</span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db, </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">LayerTable</span><span style="line-height: 140%;"> lt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">LayerTable</span><span style="line-height: 140%;">)tr.GetObject(db.LayerTableId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">short</span><span style="line-height: 140%;"> i = 1; i &lt;= 20; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> name = i.ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!lt.Has(name))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">LayerTableRecord</span><span style="line-height: 140%;"> ltr = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LayerTableRecord</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ltr.Color =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.Colors.</span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;">.FromColorIndex(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.Colors.</span><span style="line-height: 140%; color: #2b91af;">ColorMethod</span><span style="line-height: 140%;">.ByAci, i</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ltr.Name = name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lt.Add(ltr);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(ltr, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ApollonianGasket(</span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> exterior)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateLayers(db, tr);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.BlockTableId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bt[</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace], </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> res =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; exterior ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SpherePackingFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.ExteriorApollonianGasket(5)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SpherePackingFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.InteriorApollonianGasket(5);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.Length &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We get an F# list of tuples containing our circle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// definitions (no need for a special class)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> tup </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> res)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Our circles are defined in terms of position (x,y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// and curvature (the 3rd item in the nested tuple)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> radius = System.</span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Abs(tup.Item1.Item4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (radius &gt; 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// x, y &amp; z are items 1, 2 &amp; 3 (in the nested tuple)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// respectively</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Solid3d</span><span style="line-height: 140%;"> s = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Solid3d</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.CreateSphere(radius);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> center =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tup.Item1.Item1, tup.Item1.Item2, tup.Item1.Item3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> disp = center - </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.TransformBy(</span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;">.Displacement(disp));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The Layer (and therefore the colour) will be based</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// on the &quot;level&quot; of each sphere</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (item 2 in the top-level tuple)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.Layer = tup.Item2.ToString();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(s);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(s, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>It defines two commands, AGFSI and AGFSE, creating interior and exterior gaskets, respectively. To aid with visualizing the various levels, the C# code places the resultant spheres on corresponding layers.</p>
<p>Running the AGFSI command – and turning off layer 0, after changing the current later, of course – generates a nice interior Apollonian gasket:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201630172d08e970d-pi" target="_blank"><img alt="An interior 3D Apollonian packing" border="0" height="536" src="/assets/image_145562.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="An interior 3D Apollonian packing" width="475" /></a></p>
<p>Running the AGFSE command creates an external packing, although I’m somewhat loathe to call it Apollonian. I suspect through some transcription error on my part it’s not a perfect packing – you can see some intersecting spheres, for instance. I also had to manually delete some outer spheres to get to this geometry.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201676268108f970b-pi" target="_blank"><img alt="Some form of 3D packing (which isn&#39;t quite Apollonian)" border="0" height="481" src="/assets/image_629515.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Some form of 3D packing (which isn&#39;t quite Apollonian)" width="475" /></a></p>
<p>Anyway – rather than spend time perfecting the irrelevant, I’m leaving this implementation as it stands. After the weekend I’ll post a different – and more elegant – implementation that does a much better job of generating an Apollonian packing in 3D.</p>
