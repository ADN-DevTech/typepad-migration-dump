---
layout: "post"
title: "CACHEMAXTOTALSIZE – new system variable from AutoCAD 2013."
date: "2012-10-15 04:50:35"
author: "Virupaksha Aithal"
categories:
  - "2013"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/cachemaxtotalsize-new-system-variable-from-autocad-2013.html "
typepad_basename: "cachemaxtotalsize-new-system-variable-from-autocad-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>AutoCAD has introduced a new system variable called CACHEMAXTOTALSIZE in 2013 version. This variable sets the maximum total size of all graphics cache files saved in the local configured temporary folder for AutoCAD. The cached graphics are stored in folder C:\Users\&lt;username&gt;\AppData\Local\Autodesk\AutoCAD 2013 - English\R19.0\enu\GraphicsCache.</p>
<p>When the total size of the graphics cache files reaches the maximum, the oldest files in the cache are automatically deleted. Set this system variable to 0 to disable the graphics cache.</p>
