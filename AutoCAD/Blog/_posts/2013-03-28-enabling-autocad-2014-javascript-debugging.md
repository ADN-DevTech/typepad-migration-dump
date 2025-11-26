---
layout: "post"
title: "Enabling AutoCAD 2014 JavaScript Debugging"
date: "2013-03-28 09:58:36"
author: "Fenton Webb"
categories:
  - "2014"
  - "AutoCAD"
  - "Fenton Webb"
  - "JavaScript"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/enabling-autocad-2014-javascript-debugging.html "
typepad_basename: "enabling-autocad-2014-javascript-debugging"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>New in AutoCAD 2014 is the JavaScript API. Without going into too much detail here (this will be covered in much more detail elsewhere) this basically allows developers host their Apps via a URL rather than a download/install model – very cool.</p>  <p>If you have tried using the JavaScript API, I’m sure you have started to get frustrated about not being able to debug your code… Well, I have a debugging solution for you…</p>  <p>To enable debugging our JavaScript APIs we use the <a href="http://trac.webkit.org/wiki/WebInspector">WebInspector</a> application which is part of the <a href="http://www.webkit.org/">WebKit Open Source</a>. This is a web application written in JavaScript that allows the user to examine things like HTML tags, debug JavaScript code and perform profiling. </p>  <p>You can access these tools by turning on this registry entry…</p>  <p><strong>[HKEY_LOCAL_MACHINE/SOFTWARE/Wow6432Node/Autodesk/WebInspector]</strong></p>  <p><strong>&quot;DevToolsURL&quot;=</strong><span lang="EN-US" style="font-size: 11pt; font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; color: #1f497d; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-ansi-language: en-us; mso-fareast-language: en-gb; mso-bidi-language: ar-sa"><a href="http://drawingfeed.visualtao.net/DevTools/inspector/devtools.html"><font color="#0000ff">http://drawingfeed.visualtao.net/DevTools/inspector/devtools.html</font></a></span></p>  <p>Once enabled, you will see a new Right Click menu in the AcWebKit control context menu called “Developer Tools”…</p>  <p>Enjoy!</p>
