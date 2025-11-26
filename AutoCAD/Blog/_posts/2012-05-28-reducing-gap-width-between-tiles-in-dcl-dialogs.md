---
layout: "post"
title: "Reducing gap width between tiles in DCL dialogs"
date: "2012-05-28 08:01:20"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/reducing-gap-width-between-tiles-in-dcl-dialogs.html "
typepad_basename: "reducing-gap-width-between-tiles-in-dcl-dialogs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have a DCL dialog with controls organized into columns. My problem is that the gap distance between the columns is too big and I cannot make them smaller.</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The problem is that the longer the label of the tile is, the bigger the gap following the tile is:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebe09d06970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168ebe09d06970c image-full" alt="_dcl_gap_problem" title="_dcl_gap_problem" src="/assets/image_352242.jpg" border="0" /></a><br />
<p>You can work round this by breaking the text into multiple lines:</p>


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016305eb5370970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016305eb5370970d" alt="_dcl_gap_problem3" title="_dcl_gap_problem3" src="/assets/image_400733.jpg" border="0" /></a><br />
