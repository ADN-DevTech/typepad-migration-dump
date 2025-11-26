---
layout: "post"
title: "Reading/writing DWG files from a standalone application"
date: "2012-03-28 11:21:29"
author: "Madhukar Moogala"
categories:
  - "AutoCAD OEM"
  - "RealDWG"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/03/readingwriting-dwg-files-from-a-standalone-application.html "
typepad_basename: "readingwriting-dwg-files-from-a-standalone-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html" target="_self">Stephen Preston</a></p>
<p>A commonly asked question via <a href="www.autodesk.com/joinadn" target="_blank">ADN support</a> and on our public forums is how to read or write DWG files from a standalone executable without having to install AutoCAD on the same machine.</p>
<p>This can be done by licensing the Autodesk <strong>RealDWG</strong> SDK. This SDK allows you to build DWG capability into your own application without having to install AutoCAD on the same machine and automate it from your executable. RealDWG is essentially the DatabaseServices part of the AutoCAD .NET API (or AcDb part of ObjectARX), along with supporting namespaces.</p>
<p>RealDWG doesn’t include AutoCAD ‘editor’ APIs, and so you can’t easily use it for viewing and plotting DWG files (unless you do a lot of work implementing your own graphics/plotting engine). If your customer won’t buy AutoCAD for that, but they need viewing and plotting with the same fidelity that AutoCAD provides, then consider <strong>AutoCAD OEM</strong>. AutoCAD OEM is a customizable AutoCAD that you can ‘brand’ as your own application, and from which you can expose a subset of the full AutoCAD functionality, and also add your own additional functionality.&nbsp; AutoCAD LT and DWG TrueView are examples of Autodesk products built using AutoCAD OEM.</p>
<p>Both RealDWG and AutoCAD OEM are licensed technologies. You can find out more from the <a href="http://www.techsoft3d.com/" target="_blank">Tech Soft 3D website</a>. (Tech Soft 3D are&nbsp; our global distributor for RealDWG and AutoCAD OEM).</p>
<p>Here’s a video on <a href="http://download.autodesk.com/media/adn/DevTV-Introduction-to-RealDWG-Programming/" target="_blank">RealDWG programming basics</a>, recorded by DevTech’s Adam Nagy.</p>
