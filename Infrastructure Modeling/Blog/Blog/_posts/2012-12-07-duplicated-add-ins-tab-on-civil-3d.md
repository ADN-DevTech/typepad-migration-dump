---
layout: "post"
title: "Duplicated Add-ins tab on Civil 3D"
date: "2012-12-07 09:13:37"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2013"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/12/duplicated-add-ins-tab-on-civil-3d.html "
typepad_basename: "duplicated-add-ins-tab-on-civil-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>With the <a href="http://apps.exchange.autodesk.com/CIV3D/Home/Index">Autodesk Exchange Store for AutoCAD Civil 3D 2013</a> we (and the users of our apps) can download and quickly use several apps! Nice! But you may notice that the ‘Add-ins’ tab is appearing duplicated, like below.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6054124970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="duplicated" border="0" alt="duplicated" src="/assets/image_155a2c.jpg" width="467" height="95" /></a></p>  <p>To fix it, run CUI command, (1) select ‘C3D.cuix’, then (2) select ‘Civil 3D Default’ and finally (3) click on ‘Customize Workspace’. See below.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3461b224970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="setps1" border="0" alt="setps1" src="/assets/image_e91f97.jpg" width="471" height="150" /></a></p>  <p>Now (4) expand the ‘Ribbon’ list and select ‘Civil 3D Add-ins’ and finally (5) click ‘Done’. Click OK to confirm the changes.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3461b259970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="steps2" border="0" alt="steps2" src="/assets/image_2ab31b.jpg" width="471" height="253" /></a></p>  <p>That should do it.</p>  <p>If still having problems, it may be required uninstall the apps and install again to force AutoCAD Civil 3D to rebuild the CUIx.</p>
