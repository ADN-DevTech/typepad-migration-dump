---
layout: "post"
title: "AutoCAD &ldquo;Font file doesn&rsquo;t Exist&rdquo; when it does!?!"
date: "2013-09-11 10:11:41"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "ActiveX"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Fenton Webb"
  - "LISP"
  - "Mac"
  - "MFC"
  - "ObjectARX"
  - "Plant3D"
  - "PnID"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2013/09/autocad-font-file-doesnt-exist-when-it-does.html "
typepad_basename: "autocad-font-file-doesnt-exist-when-it-does"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>One of my developers was trying to create a toolbar button which automatically setup some text styles/parameters. Everything was fine except that on some machines, the button would fail – the error was “Font file doesn’t exist”. He was using a very commonly used font file “arial.ttf”, indeed arial.ttf is the default font for the “Standard” AutoCAD style.</p>  <p>It seems there are very important updates the Font packs that you need to include (update) with your Windows installation – check out <a title="http://www.microsoft.com/typography/fonts/" href="http://www.microsoft.com/typography/fonts/">http://www.microsoft.com/typography/fonts/</a></p>  <p>I’m guessing Windows update also includes these Font updates as ‘Optional’ – best install them for an easy life!</p>
