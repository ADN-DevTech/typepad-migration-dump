---
layout: "post"
title: "Applying the current Layer Color to a new Layer using LISP"
date: "2012-09-18 16:44:26"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/applying-the-current-layer-color-to-a-new-layer-using-lisp.html "
typepad_basename: "applying-the-current-layer-color-to-a-new-layer-using-lisp"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>To apply the current Layer’s colour to a newly created Layer using LISP can be done very simply like this: </p>  <p><font face="Consolas"><font color="#a5a5a5">; get the current layer name       <br /></font>(setq currentLayer (getvar &quot;CLAYER&quot;))      <br /><font color="#666666">; get the ename of the current layer from the Layer table</font>       <br />(setq currentLayerEname (tblobjname &quot;LAYER&quot; currentLayer))      <br /><font color="#666666">; extract the group code 62 (colour) from the ename using entget       <br /></font>(setq currentLayerColour (cdr (assoc 62 (entget currentLayerEname))))      <br /><font color="#666666">; finally create the new layer using (command &quot;LAYER&quot;)</font>      <br />(command &quot;_.-LAYER&quot; &quot;_N&quot; &quot;TEST2&quot; &quot;_C&quot; currentLayerColour &quot;TEST2&quot; &quot;&quot;)      <br /></font></p>
