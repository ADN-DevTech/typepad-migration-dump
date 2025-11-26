---
layout: "post"
title: "Understanding group code 68 of AcDbViewport"
date: "2013-01-04 16:03:32"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/understanding-group-code-68-of-acdbviewport.html "
typepad_basename: "understanding-group-code-68-of-acdbviewport"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Group code 68 represents the viewport status field, and the value is an integer with the following meaning:</p>  <p>0: the viewport is off so that the view within it is not displayed on screen.</p>  <p>Non-zero value: The viewport is on so that the view within it is displayed on screen as long as this viewport is active (only certain number of viewports can be active at any time. This number is reported by the MAXACTVP system variable). And the value indicates the order of stacking for the viewports, where 1 is the active viewport, 2 is the next, etc..</p>
