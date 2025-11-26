---
layout: "post"
title: "Apparent *extended* Intersection OSnap not working with my custom entity"
date: "2012-07-09 05:37:21"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/apparent-extended-intersection-osnap-not-working-with-my-custom-entity.html "
typepad_basename: "apparent-extended-intersection-osnap-not-working-with-my-custom-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>My custom entity explodes itself into AcDbPolylines in the intersectWith() functions, I then call the intersectWith() on each of these AcDbPolylines. The problem is that AppInt (apparerent *extended* Intersection) doesn't work... Why?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>This should work fine, as long as the AcDbPolyline that you custom entity explodes into isn't *Closed*... If it is then the Apparent *extended* Intersect OSnap will not work with it...</p>  <p>The solution is to explode the AcDbPolyline once more into normal AcDbLines.</p>
