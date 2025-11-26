---
layout: "post"
title: "Unloading my ARX application for the second time crashes AutoCAD"
date: "2012-05-28 09:49:01"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/unloading-my-arx-application-for-the-second-time-crashes-autocad.html "
typepad_basename: "unloading-my-arx-application-for-the-second-time-crashes-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>1. appload command and load TestDll.ARX   <br />2. close appload dialog   <br />3. appload command and unload TestDll.ARX   <br />4. close appload dialog   <br />5. appload command and load TestDll.ARX   <br />6. close appload dialog   <br />7. appload command and unload TestDll.ARX -&gt; AutoCAD crashes</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>I can see in your project's settings that you are using CLR (i.e. Common Language Runtime Support (/clr)).</p>  <p>This makes your project managed, which means that its extension should be *.dll and it should be loaded using NETLOAD, not APPLOAD.</p>  <p>Note that managed AddIns cannot be unloaded/reloaded in the same AutoCAD session.</p>
