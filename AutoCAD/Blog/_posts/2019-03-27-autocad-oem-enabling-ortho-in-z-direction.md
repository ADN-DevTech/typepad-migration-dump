---
layout: "post"
title: "AutoCAD OEM: Enabling Ortho in Z Direction"
date: "2019-03-27 00:17:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2019/03/autocad-oem-enabling-ortho-in-z-direction.html "
typepad_basename: "autocad-oem-enabling-ortho-in-z-direction"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p><br></p><p>I have received a query from an OEM developer, in his Survey product Ortho snapping functionality doesn’t work in +Z/-Z direction.</p><p>Here is screen GIF.</p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4995ffb200b-pi"><img width="362" height="234" title="NoOrthoInZ" style="display: inline;" alt="NoOrthoInZ" src="/assets/image_976732.jpg"></a></p><p><br></p><p>AutoCAD, AutoCAD OEM, AutoCAD LT and TrueView are different manifestations of same code base, this has been singlehandedly maintained by our beloved Engineer Patti [Patricia Harris] for last 30 years if I’m not wrong, she recently passed away in tragic <a href="https://www.pressdemocrat.com/news/8826002-181/a-rare-week-in-sonoma">accident</a>. <img class="wlEmoticon wlEmoticon-sadsmile" alt="Sad smile" src="/assets/image_87027.jpg"></p><p><br></p><p>By design AutoCAD LT doesn’t enable Ortho in Z direction as LT is not for 3D designs.</p><p>But AutoCAD OEM has a setting that needs to be enabled to have this functionality work.</p><p><br></p><p>Tracking down the setting variable was an exhilarating experience, I would like to thank my esteem colleague Markus Kraus, one of the most talented programmer, I would call him second generation programmer after John Walker and his gang left.</p><p><br></p><p>So the what’s the secret setting that is hidden for AutoCAD LT, and TrueView but not for AutoCAD and AutoCAD OEM.</p><p><br></p><p>You need to enable ‘<strong>UCSDETECT’</strong> command in your OEM Program .XML<p><br><p><br></p>

<pre style="background: rgb(0, 0, 0); color: rgb(209, 209, 209);"><span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">OemCmd</span><span style="color: rgb(255, 137, 6);">&gt;</span>

<span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Name</span><span style="color: rgb(255, 137, 6);">&gt;</span>ucsdetect<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Name</span><span style="color: rgb(251, 132, 0);">&gt;</span>

<span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Status</span><span style="color: rgb(255, 137, 6);">&gt;</span>Full<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">Status</span><span style="color: rgb(251, 132, 0);">&gt;</span>

<span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(246, 193, 208);">OemCmd</span><span style="color: rgb(251, 132, 0);">&gt;</span>
</pre>
<p>After enabling the Command </p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44b9489200c-pi"><img width="326" height="329" title="OrthoInZ" style="display: inline; background-image: none;" alt="OrthoInZ" src="/assets/image_275997.jpg" border="0"></a></p>
