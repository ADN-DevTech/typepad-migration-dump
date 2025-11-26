---
layout: "post"
title: "DCL dialog that displays slides not showing slides if path contains parenthesis"
date: "2012-05-25 11:56:05"
author: "Wayne Brill"
categories:
  - "AutoCAD"
  - "LISP"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/dcl-dialog-that-displays-slides-not-showing-slides-if-path-contains-parenthesis.html "
typepad_basename: "dcl-dialog-that-displays-slides-not-showing-slides-if-path-contains-parenthesis"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>I have&#160; a DCL dialog that has slides. The slides are not being displayed. I use the &quot;VSLIDE&quot; command and select a sld and I get&#160; an AutoCAD Message that states &quot;Program Files.slb&quot; &quot;Can't find file in search path&quot;. How can I get the slides to display?&#160; </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>If the directory that contains a SLB file contains parenthesis, such as &quot;Program Files (x86)\&quot;, the slides will not display. The DCL call into the slide library is being confused. It uses the parenthesis as a pointer into the library. </p>  <p>The solution is to modify the AutoLISP code not to use the path and move the slide library into support search path that does not contain parenthesis.</p>
