---
layout: "post"
title: "Changing the format of measure result of fusion viewer"
date: "2012-06-03 20:49:47"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/changing-the-format-of-measure-result-of-fusion-viewer.html "
typepad_basename: "changing-the-format-of-measure-result-of-fusion-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>If you are using measure widget of Fusion viewer, you will notice that the measure result if in exponential format, like 4.91e+5m. Is it possible to the change the format? </p>  <p>Measure widget of fusion viewer uses following JavaScript code snippet to handle the output result:</p>  <p>C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\fusion\widgets\Measure.js , around line 583:</p>  <p><font style="background-color: #ffff00">value = value.toPrecision(this.areaPrecision);</font></p>  <p>You can change this code to show the result as your preferred format. To make your changes take effect, you need to change the template file, for example, it is C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\fusion\templates\mapguide\slate\index.html for Slate template: </p>  <p>Change from:</p>  <p>&lt;script type=&quot;text/javascript&quot; src=&quot;../../../lib/fusionSF-compressed.js&quot;&gt;&lt;/script&gt;</p>  <p>To:</p>  <p>&lt;script type=&quot;text/javascript&quot; src=&quot;../../../lib/fusion.js&quot;&gt;&lt;/script&gt;</p>  <p>You may also want to check out this blog - <a href="http://adndevblog.typepad.com/infrastructure/2012/04/debugging-fusion-viewer-or-mobile-viewer-of-aims-in-firebug.html" target="_blank">Debugging Fusion Viewer or Mobile Viewer of AIMS in Firebug</a></p>
