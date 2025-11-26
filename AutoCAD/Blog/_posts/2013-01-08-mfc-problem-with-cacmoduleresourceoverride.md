---
layout: "post"
title: "MFC: Problem with CAcModuleResourceOverride"
date: "2013-01-08 17:34:13"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "MFC"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/mfc-problem-with-cacmoduleresourceoverride.html "
typepad_basename: "mfc-problem-with-cacmoduleresourceoverride"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might find that when you use the CAcModuleResourceOverride object to switch the default resource handle (AutoCAD resource handle) to your application resource file, you cannot call AutoCAD or ARX function such as acedGetFileD(). </p>  <p>This is because you switched the default resource file (the AutoCAD resource file) to your application resource file using the CAcModuleResourceOverride object. Then, inside a function (where the resource handle still points to your application resource file), you call an AutoCAD function that needs to access the AutoCAD resources (not yours). As you switched it, the acedGetFileD() tries to find the dialog template it needs, and fails. To solve this problem, restore the AutoCAD resource handle as the default one. See the following sample code:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">HINSTANCE hOld =AfxGetResourceHandle () ; </font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#008000">// Optional</font></span></font></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">AfxSetResourceHandle (acedGetAcadResourceInstance ()) ;</font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">acedGetFileD(...) ;</font></font></span></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">AfxSetResourceHandle (hOld) ; </font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#008000">// Optional</font></span></font></p> </div>
