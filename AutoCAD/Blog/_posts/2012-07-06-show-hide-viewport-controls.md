---
layout: "post"
title: "Show / Hide Viewport controls"
date: "2012-07-06 04:23:07"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/show-hide-viewport-controls.html "
typepad_basename: "show-hide-viewport-controls"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Viewport controls were introduced in AutoCAD 2012 and lets you easily change the view or the visual style.</p>
<p>It is displayed at the top left corner of the viewport and here is a screenshot :</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176162e9ce7970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0176162e9ce7970c" title="1" src="/assets/image_74751.jpg" border="0" alt="1" /></a><br />To control the visibility of this control, launch the&nbsp;options dialog using the "Options" command, switch to "3d Modeling" tab and check / uncheck the "Display the Viewport controls" checkbox.</p>
<p>To control it programmatically, set the "VPCONTROL" system variable to 1&nbsp;or 0.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Application.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;VPCONTROL&quot;</span><span style="line-height: 140%;">, 1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//OR</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Application.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;VPCONTROL&quot;</span><span style="line-height: 140%;">, 0);</span></p>
</div>
