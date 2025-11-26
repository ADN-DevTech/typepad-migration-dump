---
layout: "post"
title: "Creating a gradient hatch using VLisp"
date: "2012-09-16 11:32:14"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/creating-a-gradient-hatch-using-vlisp.html "
typepad_basename: "creating-a-gradient-hatch-using-vlisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a Visual Lisp example that adds a circle to model space and fill it with a gradient hatch.</p>
<p></p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:addGHat&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">/</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;acadObject&nbsp;&nbsp;acaddocument<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mspace&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mycircle&nbsp;&nbsp;&nbsp;myloop&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;myhatch<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;col1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;col2<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;acver&nbsp;<span style="color:#ff0000">(</span>substr&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">getvar</span>&nbsp;<span style="color:#ff00ff">"acadver"</span><span style="color:#ff0000">)</span>1&nbsp;<span style="color:#008000">2</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>SetQ&nbsp;acadobject&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>VLAX-Get-ACAD-Object<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;acaddocument&nbsp;<span style="color:#ff0000">(</span>VLA-Get-ActiveDocument&nbsp;acadobject<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;mspace&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>VLA-Get-ModelSpace&nbsp;acaddocument<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>SetQ&nbsp;mycircle&nbsp;<span style="color:#ff0000">(</span>VLA-AddCircle<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mspace<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>VLAX-3D-Point&nbsp;'<span style="color:#ff0000">(</span>50.0&nbsp;50.0&nbsp;0.0<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;150.0<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>SetQ&nbsp;myloop&nbsp;<span style="color:#ff0000">(</span>VLAX-Make-SafeArray&nbsp;vlax-vbObject&nbsp;'<span style="color:#ff0000">(</span><span style="color:#008000">0</span>&nbsp;.&nbsp;<span style="color:#008000">0</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>VLAX-SafeArray-Put-Element<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;myloop<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mycircle<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span>SetQ&nbsp;myhatch&nbsp;<span style="color:#ff0000">(</span>VLA-AddHatch<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mspace<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;acPreDefinedGradient<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff00ff">"LINEAR"</span>&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:vlax-true<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;acGradientObject<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;col1&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-GetInterfaceObject</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">strcat</span>&nbsp;<span style="color:#ff00ff">"AutoCAD.AcCmColor."</span>&nbsp;acver<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;col2&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-GetInterfaceObject</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">strcat</span>&nbsp;<span style="color:#ff00ff">"AutoCAD.AcCmColor."</span>&nbsp;acver<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-SetRGB</span>&nbsp;col1&nbsp;<span style="color:#008000">255</span>&nbsp;<span style="color:#008000">0</span>&nbsp;<span style="color:#008000">0</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-setrgb</span>&nbsp;col2&nbsp;<span style="color:#008000">0</span>&nbsp;<span style="color:#008000">0</span>&nbsp;<span style="color:#008000">255</span><span style="color:#ff0000">)</span><br />
&nbsp;<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-put-gradientcolor</span>1&nbsp;myhatch&nbsp;col1<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-put-gradientcolor</span>2&nbsp;myhatch&nbsp;col2<span style="color:#ff0000">)</span><br />
&nbsp;<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span>VLA-AppendOuterLoop&nbsp;myhatch&nbsp;myloop<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span>VLA-Evaluate&nbsp;myhatch<span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">)</span></span></p>
<p></p>
<p>Update : Thanks to Oleg for suggesting the use of acadVer instead of using "AutoCAD.AcCmColor.19" to get the color object.</p>
