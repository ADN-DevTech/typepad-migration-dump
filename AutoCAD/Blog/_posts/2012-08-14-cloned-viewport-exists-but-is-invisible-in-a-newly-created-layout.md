---
layout: "post"
title: "Cloned viewport exists but is invisible in a newly created layout"
date: "2012-08-14 05:28:11"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/cloned-viewport-exists-but-is-invisible-in-a-newly-created-layout.html "
typepad_basename: "cloned-viewport-exists-but-is-invisible-in-a-newly-created-layout"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>Starting from a layout &quot;layout1&quot; with a viewport &quot;vp&quot; in it, I wish to copy this viewport with ObjectARX (deepCloneObjects) into an existing but not yet opened layout &quot;layout2&quot; of the same dwg. But changing to layout2 the copied viewport is invisible and cannot be selected nor can it be found with the _list command or anything else.</p>  <p>If layout2 was already opened once before copying vp (without filling in the page setup dialog), everything works fine i.e. vp is perfectly visible after changing to layout2.</p>  <p>What is wrong?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>Because the paperspace viewport is not created until the first time a layout is activated, it’s very likely that there’s no paperspace viewport for the layout that the viewport is being copied into, or that the paperspace viewport ends up *<b>NOT</b>* being the first AcDbViewport object in the layout’s BlockTableRecord. It is not supposed to matter whether the paperspace viewport is the first viewport or not, but a lot of the layout handling code (and other code as well) assumes that the paperspace viewport is the first viewport in the BlockTableRecord. So, if the paperspace viewport is not first, problems can occur.</p>  <p>The solution is to switch to the Layout first (making it active) before cloning the viewport.</p>
