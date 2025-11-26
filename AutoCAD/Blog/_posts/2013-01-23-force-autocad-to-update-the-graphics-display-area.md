---
layout: "post"
title: "Force AutoCAD to update the graphics display area"
date: "2013-01-23 10:38:55"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/force-autocad-to-update-the-graphics-display-area.html "
typepad_basename: "force-autocad-to-update-the-graphics-display-area"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>You can force the graphics update inserting the following lines in the code:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">actrTransactionManager-&gt;flushGraphics();</font></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">acedUpdateDisplay();</font></font></p> </div>  <p>In the case where you use transactions to open entities and you have transaction(s) started, you then need to also call:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">actrTransactionManager-&gt;queueForGraphicsFlush();</font></font></p> </div>  <p>before calling the preceding functions.</p>
