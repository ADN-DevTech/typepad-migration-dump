---
layout: "post"
title: "Circle packing in AutoCAD: creating an Apollonian gasket using F# &ndash; Part 2"
date: "2012-02-13 10:24:54"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Fractals"
  - "Geometry"
original_url: "https://www.keanw.com/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-2.html "
typepad_basename: "circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-2"
typepad_status: "Publish"
---

<p>Following on from <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-1.html" target="_blank">the previous</a> post in this series, today’s post completes the implementation to create a full Apollonian gasket in AutoCAD using F#.</p>
<p>As a comment on <a href="http://langexplr.blogspot.com/2007/10/drawing-apollonian-gasket-with-common.html" target="_blank">the original Common LISP implementation</a>, someone had contributed <a href="http://fraktal.republika.pl/doc/apollonian_3.lisp" target="_blank">a more complete version</a> which allowed me to complete today’s F# version.</p>
<p>Here’s the additional F# file for the project (which I’ll be providing in full at the end of the series):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">module</span><span style="line-height: 140%;"> CirclePackingFullFs</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">open</span><span style="line-height: 140%;"> System.Numerics;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Use Descartes&#39; theorem to calculate the radius/position</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// of the 4th circle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// k4 = k1 + k2 + k3 +/- sqrt(k1k2 + k2k3 + k3k1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> solve a b c =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [(+);(-)] |&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; List.map</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> f </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> (f (a + b + c) (2. * (sqrt (a*b + b*c + a*c)))))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> multComplex a (c : Complex) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Complex(a * c.Real, a * c.Imaginary)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> solveComplex (a : Complex) (b : Complex) (c : Complex) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [(+);(-)] |&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; List.map</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> f </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (f (a + b + c)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (multComplex 2.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Complex.Sqrt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Complex.Multiply(a,b) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Complex.Multiply(b,c) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Complex.Multiply(a,c))))))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Return a list of all solution circles (positive and negative)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> apollonianCircle (x1,y1,k1) (x2,y2,k2) (x3,y3,k3) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> p1 = Complex(x1,y1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> p2 = Complex(x2,y2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> p3 = Complex(x3,y3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> kz1 = multComplex k1 p1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> kz2 = multComplex k2 p2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> kz3 = multComplex k3 p3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> curSols = solve k1 k2 k3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> posSols = solveComplex kz1 kz2 kz3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; List.map2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> cur pos </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> r4 = 1. / cur</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> pos4 = multComplex r4 pos</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pos4.Real, pos4.Imaginary, cur)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; curSols posSols</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Only return certain circles - for use with &quot;hard&quot; gaps</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> apollonianCircle2 (x1,y1,k1) (x2,y2,k2) (x3,y3,k3) =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> p1 = Complex(x1,y1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> p2 = Complex(x2,y2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> p3 = Complex(x3,y3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> kz1 = multComplex k1 p1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> kz2 = multComplex k2 p2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> kz3 = multComplex k3 p3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> cur = List.head (solve k1 k2 k3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> pos = List.nth (solveComplex kz1 kz2 kz3) 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> r4 = 1. / cur</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> pos4 = multComplex r4 pos</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pos4.Real, pos4.Imaginary, cur</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">rec</span><span style="line-height: 140%;"> easyGap c1 c2 c3 steps =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">match</span><span style="line-height: 140%;"> steps </span><span style="line-height: 140%; color: blue;">with</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | 0 </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | _ </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c4 = List.head(apollonianCircle c1 c2 c3)&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [(c4,steps-1)] @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c1 c2 c4 (steps-1) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c2 c3 c4 (steps-1) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c3 c1 c4 (steps-1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> hardGap c1 c2 c3 steps =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">match</span><span style="line-height: 140%;"> steps </span><span style="line-height: 140%; color: blue;">with</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | 0 </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | _ </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> g1 = apollonianCircle2 c1 c2 c3&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [(g1,steps-1)] @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c2 c3 g1 (steps-1) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c2 c1 g1 (steps-1) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> steps = 1 </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> g2 = apollonianCircle2 c1 c3 g1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; [(g2,steps-2)] @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; easyGap c3 g1 g2 (steps-2) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; easyGap c1 g1 g2 (steps-2) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> steps = 2 </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> g3 = apollonianCircle2 c1 c3 g2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [(g3,steps-3)] @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; easyGap c1 g2 g3 (steps-3) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; easyGap c1 c3 g3 (steps-3) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> steps = 3 </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> g4 = apollonianCircle2 c3 g2 g3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [(g4,steps-4)] @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; easyGap c3 g2 g4 (steps-4) @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; easyGap g2 g3 g4 (steps-4)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">type</span><span style="line-height: 140%;"> Packer() =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">member</span><span style="line-height: 140%;"> ApollonianGasket outerRad steps =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt3 = sqrt(3.)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> size = outerRad * 2.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> a = 1. + 2. / rt3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> innerRad = outerRad / a</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> innerCur = 1. / innerRad</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> h = innerRad * rt3</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c0 = outerRad, outerRad, -1. / outerRad</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c1 = outerRad, size - innerRad, innerCur</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c2 = outerRad + innerRad, size - (h + innerRad), innerCur</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c3 = outerRad - innerRad, size - (h + innerRad), innerCur</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c4 = List.head(apollonianCircle c1 c2 c3)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> nextStep = steps-1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Main circles</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (Assumption is that the outer circle already exists)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [(c1,steps);(c2,steps);(c3,steps);(c4,nextStep)] @</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Inner gaps</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c1 c2 c4 nextStep @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c2 c3 c4 nextStep @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c3 c1 c4 nextStep @</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Peripheral gaps</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c1 c2 c0 nextStep @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; easyGap c2 c3 c0 nextStep @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; hardGap c3 c1 c0 nextStep</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>The hardGap function does a fair amount of manual filling of the top-left gap… I suspect this could also have been addressed by mirroring or rotating the results from one of the easyGap calls, but there you go. Ultimately the code performs well enough.</p>
<p>The corresponding C# loader file clearly also needs updating to define an additional command calling the F# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> CirclePackingLoader</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFC&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; PackApollonianCircles(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFCI&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> InnerApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; PackApollonianCircles(</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> PackApollonianCircles(</span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> full)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptEntityOptions</span><span style="line-height: 140%;"> peo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;\nSelect circle: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AllowNone = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nMust be a circle.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(</span><span style="line-height: 140%; color: blue;">typeof</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;">), </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(peo);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK &amp;&amp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; per.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.None</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.CurrentSpaceId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If the user selected a circle, use it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;"> cir;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.None &amp;&amp; per.ObjectId != </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(per.ObjectId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Otherwise create a new one at the default location</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">.Origin, </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">.ZAxis, 500);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(cir);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(cir, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> numSteps = 11;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> res =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; full ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">CirclePackingFullFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.ApollonianGasket(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.Radius, numSteps</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">CirclePackingFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.ApollonianGasket(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.Radius, numSteps</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> offset =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.Center - </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(cir.Radius, cir.Radius, 0.0);</span></p>
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
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> curvature = System.</span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Abs(tup.Item1.Item3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (1.0 / curvature &gt; 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// x and y are Items 1 and 2 (in the nested tuple)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// respectively</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;"> c =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tup.Item1.Item1, tup.Item1.Item2, 0.0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) + offset,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;">.ZAxis,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 1.0 / curvature</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Color index will be based on the &quot;level&quot; of each</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// circle (item 2 in the top-level tuple)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; c.ColorIndex = (numSteps - tup.Item2) + 1;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(c);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(c, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>When we run our new AGFC command, we now see a full Apollonian gasket generated:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20168e742fc62970c-pi" target="_blank"><img alt="A full F#-generated 2D Apollonian gasket" border="0" height="458" src="/assets/image_630916.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="A full F#-generated 2D Apollonian gasket" width="475" /></a></p>
<p>Over the next few posts we’ll make the transition across to 3D, looking at sphere packing.</p>
