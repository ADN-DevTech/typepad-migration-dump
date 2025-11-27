---
layout: "post"
title: "Get the &quot;correct&quot; StartAngle and SweepAngle of EllipticalArc and EllipticalArc2d"
date: "2012-08-09 14:28:51"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/get-the-correct-startangle-and-sweepangle-of-ellipticalarc-and-ellipticalarc2d.html "
typepad_basename: "get-the-correct-startangle-and-sweepangle-of-ellipticalarc-and-ellipticalarc2d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>If you are working with elliptical objects and the StartAngle and SweepAngle do not seem correct it could be because Inventor transient geometry elliptical arcs use elliptical angle. This can be referred to as &quot;skew angle&quot;. This can be obtained using the major and minor radiuses. Sketch entities use circular angles.</p>
<p>The zip file contains a part document with some sample ellipses. You can use this document with the example code to see this conversion between circular and elliptical angles.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0167692bc334970b"><a href="http://adndevblog.typepad.com/files/sample1.zip">Download Sample1</a></span></p>
<p>Note: To test this code select an ellipse in the sample document and then run the testellipse procedure.</p>
<p><strong>VBA Example</strong></p>
<p>Sub testellipse() <br />&#0160;&#0160;&#0160; Dim kPI As Double <br />&#0160;&#0160;&#0160; kPI = Atn(1) * 4 <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim kRadToDeg As Double <br />&#0160;&#0160;&#0160; kRadToDeg = 180 / kPI <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim oSEA As SketchEllipticalArc <br />&#0160;&#0160;&#0160; Set oSEA = ThisApplication. _ <br />&#0160;&#0160;&#0160;&#0160;&#0160; ActiveDocument.SelectSet.Item(1) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Debug.Print &quot; *SketchEllipticalArc*&quot; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; circular angle <br /></span>&#0160;&#0160;&#0160; Dim sweepAng As Double <br />&#0160;&#0160;&#0160; sweepAng = kRadToDeg * oSEA.SweepAngle <br />&#0160;&#0160;&#0160; Debug.Print &quot;SweepAngle [Circular] =&quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; sweepAng <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; elliptic angle <br /></span>&#0160;&#0160;&#0160; Dim convertedAng As Double <br />&#0160;&#0160;&#0160; convertedAng = kRadToDeg * _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oSEA.SweepAngle, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MajorRadius, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MinorRadius, True) <br />&#0160;&#0160;&#0160; Debug.Print &quot;SweepAngle [Elliptic] = &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedAng <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; circular angle <br /></span>&#0160;&#0160;&#0160; Dim startAng As Double <br />&#0160;&#0160;&#0160; startAng = kRadToDeg * oSEA.StartAngle <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Debug.Print &quot;StartAngle [Circular] = &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; startAng <br />&#0160; <span style="color: #0000ff;">&#0160; &#39; elliptic angle <br /></span>&#0160;&#0160;&#0160; Dim convertedStartAng As Double <br />&#0160;&#0160;&#0160; convertedStartAng = kRadToDeg * _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oSEA.StartAngle, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MajorRadius, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MinorRadius, True) <br />&#0160;&#0160;&#0160; Debug.Print &quot;StartAngle [Elliptic] = &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedStartAng <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim oEA As EllipticalArc2d <br />&#0160;&#0160;&#0160; Set oEA = oSEA.Geometry <br />&#0160;&#0160;&#0160; Debug.Print &quot; **** EllipticalArc2d ****&quot; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; elliptic angle <br /></span>&#0160;&#0160;&#0160; Debug.Print &quot;SweepAngle [Elliptic] = &quot; _ <br />&#0160;&#0160;&#0160; &amp; kRadToDeg * oEA.SweepAngle <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; circular angle <br /></span>&#0160;&#0160;&#0160; Dim convertedEllArcAng As Double <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; convertedEllArcAng = kRadToDeg * _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oEA.SweepAngle, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MajorRadius, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MinorRadius, False) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Debug.Print &quot;SweepAngle [Circular] = &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedEllArcAng <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; elliptic angle <br /></span>&#0160;&#0160;&#0160; Debug.Print &quot;StartAngle [Elliptic] = &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; kRadToDeg * oEA.StartAngle <br />&#0160;&#0160; <span style="color: #0000ff;">&#39; circular angle <br /></span>&#0160;&#0160;&#0160; Dim convertedEllArcStartAng As Double <br />&#0160;&#0160;&#0160; convertedEllArcStartAng = kRadToDeg * _ <br />&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oEA.StartAngle, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MajorRadius, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MinorRadius, False) <br />&#0160;&#0160;&#0160; Debug.Print &quot;StartAngle [Circular] = &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedEllArcStartAng <br />End Sub</p>
<p><br />Function getConvertedAngle(dAngle As Double, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dMajorRadius As Double, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dMinorRadius As Double, _ <br />&#0160;&#0160; bCircularToElliptical As Boolean) As Double <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim kPI As Double <br />&#0160;&#0160;&#0160; kPI = Atn(1) * 4 <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim dTan As Double <br />&#0160;&#0160;&#0160; dTan = Tan(dAngle) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; If Abs(dTan) &lt; 0.0000000001 _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Or Abs(dTan) &gt; 10000000000# Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = dAngle <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; ElseIf bCircularToElliptical Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = Atn _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (dTan * dMajorRadius / dMinorRadius) <br />&#0160;&#0160;&#0160; Else <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = Atn _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (dTan * dMinorRadius / dMajorRadius) <br />&#0160;&#0160;&#0160; End If <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; If getConvertedAngle &lt; 0 Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle + 2 * kPI <br />&#0160;&#0160;&#0160; End If <br />End Function</p>
<p><strong>VB.NET</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">Form1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_inventorApp <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> Button1_Click(<span style="color: blue;">ByVal</span> sender <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Object</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ByVal</span> e <span style="color: blue;">As</span> System.<span style="color: #2b91af;">EventArgs</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Handles</span> Button1.Click</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get an active instance of Inventor</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.Runtime. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteropServices.<span style="color: #2b91af;">Marshal</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> <span style="color: green;">&#39;Inventor not started</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">MessageBox</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Show(<span style="color: #a31515;">&quot;Start an Inventor session&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Call the Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; testellipse()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Sub</span> testellipse()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> kPI <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kPI = <span style="color: #2b91af;">Math</span>.Atan(1) * 4</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> kRadToDeg <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kRadToDeg = 180 / kPI</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oSEA <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchEllipticalArc</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA = m_inventorApp. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ActiveDocument.SelectSet.Item(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot; *SketchEllipticalArc*&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; circular angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> sweepAng <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sweepAng = kRadToDeg * oSEA.SweepAngle</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;SweepAngle [Circular] =&quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; sweepAng)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; elliptic angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> convertedAng <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; convertedAng = kRadToDeg * _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oSEA.SweepAngle, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MajorRadius, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MinorRadius, <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;SweepAngle [Elliptic] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedAng)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; circular angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> startAng <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; startAng = kRadToDeg * oSEA.StartAngle</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;StartAngle [Circular] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; startAng)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; elliptic angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> convertedStartAng <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; convertedStartAng = kRadToDeg * _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oSEA.StartAngle, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MajorRadius, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSEA.MinorRadius, <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;StartAngle [Elliptic] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedStartAng)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oEA <span style="color: blue;">As</span> <span style="color: #2b91af;">EllipticalArc2d</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA = oSEA.Geometry</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot; **** EllipticalArc2d ****&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; elliptic angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;SweepAngle [Elliptic] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; kRadToDeg * oEA.SweepAngle)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; circular angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> convertedEllArcAng <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; convertedEllArcAng = kRadToDeg * _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oEA.SweepAngle, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MajorRadius, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MinorRadius, <span style="color: blue;">False</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;SweepAngle [Circular] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedEllArcAng)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; elliptic angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;StartAngle [Elliptic] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; kRadToDeg * oEA.StartAngle)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; circular angle</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> convertedEllArcStartAng <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; convertedEllArcStartAng = kRadToDeg * _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle(oEA.StartAngle, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MajorRadius, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEA.MinorRadius, <span style="color: blue;">False</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(<span style="color: #a31515;">&quot;StartAngle [Circular] = &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; convertedEllArcStartAng)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Function</span> getConvertedAngle(dAngle <span style="color: blue;">As</span> <span style="color: blue;">Double</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dMajorRadius <span style="color: blue;">As</span> <span style="color: blue;">Double</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dMinorRadius <span style="color: blue;">As</span> <span style="color: blue;">Double</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bCircularToElliptical <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span>) <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> kPI <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kPI = <span style="color: #2b91af;">Math</span>.Atan(1) * 4</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> dTan <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dTan = <span style="color: #2b91af;">Math</span>.Tan(dAngle)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: #2b91af;">Math</span>.Abs(dTan) &lt; 0.0000000001 _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Or</span> <span style="color: #2b91af;">Math</span>.Abs(dTan) &gt; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 10000000000.0# <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = dAngle</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ElseIf</span> bCircularToElliptical <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = <span style="color: #2b91af;">Math</span>.Atan _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (dTan * dMajorRadius / dMinorRadius)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = <span style="color: #2b91af;">Math</span>.Atan _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (dTan * dMinorRadius / dMajorRadius)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> getConvertedAngle &lt; 0 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getConvertedAngle + 2 * kPI</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>
</div>
