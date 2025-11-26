---
layout: "post"
title: "Using AcDbRasterImageDefFileAccessReactor"
date: "2012-10-02 01:54:49"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/using-acdbrasterimagedeffileaccessreactor.html "
typepad_basename: "using-acdbrasterimagedeffileaccessreactor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I want to use an AcDbRasterImageDefFileAccessReactor to receive notifications when images are attached, detached, reloaded etc. using the Image Command. Is there an example that shows how to implement and use the AcDbRasterImageDefFileAccessReactor class?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>The attached ObjectARX example shows how to implement Image dictionary notifications using the AcDbRasterImageDefFileAccessReactor class. The reactor is attached when the ARX application is loaded, and removed when the application is unloaded.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:4ead1857-3025-4f62-a52f-3d5546959feb" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/arxrasterdefreactor.zip" target="_blank">ArxRasterDefReactor.zip</a></p></div>
