---
layout: "post"
title: "How to know if an entity's properties are modified by the OPM?"
date: "2012-08-22 02:12:50"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/how-to-know-if-an-entitys-properties-are-modified-by-the-opm.html "
typepad_basename: "how-to-know-if-an-entitys-properties-are-modified-by-the-opm"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>How would one know if an entity's properties are modified by the OPM?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>OPM brackets the property modification calls with modeless operation start/end notifications using the string <em>&quot;OPM_CHGPROP&quot;.</em></p>  <p>Applications should receive these notifications through the <em>AcEditorReactor::modelessOperationWillStart()</em> and <em>AcEditoReactor::modelessOperationEnded()</em></p>
