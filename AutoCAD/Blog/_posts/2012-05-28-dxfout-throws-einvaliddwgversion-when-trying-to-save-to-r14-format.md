---
layout: "post"
title: "DxfOut throws eInvalidDwgVersion when trying to save to R14 format"
date: "2012-05-28 05:03:30"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/dxfout-throws-einvaliddwgversion-when-trying-to-save-to-r14-format.html "
typepad_basename: "dxfout-throws-einvaliddwgversion-when-trying-to-save-to-r14-format"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>I'm using Database.DxfOut() to save a file and it works fine except when trying to save to R14 format. Then it throws eInvalidDwgVersion exception. Why?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The reason is that saving to R14 DXF format has not been supported since AutoCAD 2006. But you can save to other DXF formats all the way back to R12.</p>
