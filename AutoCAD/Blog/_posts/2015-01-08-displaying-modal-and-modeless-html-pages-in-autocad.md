---
layout: "post"
title: "Displaying Modal and Modeless HTML Pages in AutoCAD"
date: "2015-01-08 23:48:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/displaying-modal-and-modeless-html-pages-in-autocad.html "
typepad_basename: "displaying-modal-and-modeless-html-pages-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>In AutoCAD 2014 , two new APIs are introduced&#0160; to display HTML webpages in AutoCAD , most of you are aware of displaying forms these&#0160; API s are little addition to the existing ones</p>
<p>Application.ShowModalWindow</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> ShowModalWindow(<span style="color: #2b91af;">Uri</span> htmlPage);</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> ShowModalWindow(<span style="color: #2b91af;">IntPtr</span> owner, <span style="color: #2b91af;">Uri</span> htmlPage);</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> ShowModalWindow(<span style="color: #2b91af;">IntPtr</span> owner, <span style="color: #2b91af;">Uri</span> htmlPage, <span style="color: blue;">bool</span> persistSizeAndPosition);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="font-family: Arial; font-size: small;">Application.ShowModelessWindow.</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ShowModelessWindow(<span style="color: #2b91af;">Uri</span> htmlPage);</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ShowModelessWindow(<span style="color: #2b91af;">IntPtr</span> owner, <span style="color: #2b91af;">Uri</span> htmlPage);</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ShowModelessWindow(<span style="color: #2b91af;">IntPtr</span> owner, <span style="color: #2b91af;">Uri</span> htmlPage, <span style="color: blue;">bool</span> persistSizeAndPosition);</p>
</div>
<p>Sample Code:</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> test()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Uri</span> uri = <span style="color: blue;">new</span> <span style="color: #2b91af;">Uri</span>(<span style="color: #a31515;">&quot;http://adndevblog.typepad.com/autocad/&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">IntPtr</span> owner = Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.MainWindow.Handle;</p>
<p style="margin: 0px;"><span style="color: green;">/*Modeless */</span></p>
<p style="margin: 0px;"><span style="color: green;">//Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessWindow(owner, uri, true);</span></p>
<p style="margin: 0px;"><span style="color: green;">/*Modal Window*/</span></p>
<p style="margin: 0px;"><span style="color: blue;">bool</span> rc = Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.ShowModalWindow(owner, uri, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Snapshot :</p>
<p style="margin: 0px;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0bba334970c-pi"><img alt="HTMLPage" border="0" height="200" src="/assets/image_646219.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="HTMLPage" width="244" /></a></p>
</div>
