---
layout: "post"
title: "How can you show AModeler hidden line as a dashed line"
date: "2013-02-08 18:13:18"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/how-can-you-show-amodeler-hidden-line-as-a-dashed-line.html "
typepad_basename: "how-can-you-show-amodeler-hidden-line-as-a-dashed-line"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Basically, you'll need to add a class that is derived from OutputPolylineCallback and override outputPolyline(). Next, instantiate a callback object in the subViewportDraw() of the custom entity class so it is called when it is HIDE, SHADE or RENDER. Since the callback will give you the hidden edges of a body, if you figure out the correct projection plane, it is just a matter of loading a dashed linetype and displaying the hidden edges in an appropriate LTSCALE. </p>  <p>There is also a trick to force AutoCAD to display the hidden edges. All of the above is fully demonstrated in this <a href="http://adndevblog.typepad.com/autocad/Downloads/Hidden.zip">sample</a>. Pay special attention to AsdkBody.cpp and .h files, and please be sure to load the sample drawing to test.</p>
