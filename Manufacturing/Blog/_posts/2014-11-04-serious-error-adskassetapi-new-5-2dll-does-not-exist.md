---
layout: "post"
title: "Serious error: adskassetapi_new-5_2.dll does not exist"
date: "2014-11-04 04:17:59"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/serious-error-adskassetapi_new-5_2dll-does-not-exist.html "
typepad_basename: "serious-error-adskassetapi_new-5_2dll-does-not-exist"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>After installing <strong>Inventor 2015</strong> and running your program that uses <strong>apprentice</strong> to access e.g. the <strong>ActiveRenderStyle</strong> property of the document, you might get this message: &quot;<strong>Serious error</strong>&quot; / &quot;<strong>Library C:\Program Files\Autodesk\Inventor 2015\Bin\bin32\adskassetapi_new-5_2.dll does not exist</strong>&quot;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7001d93970b-pi" style="display: inline;"><img alt="Seriouserror" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7001d93970b img-responsive" src="/assets/image_22f9f1.jpg" title="Seriouserror" /></a></p>
<p>This problem only seems to occur after installing <strong>Inventor 2015</strong> if:<br />- you are trying to use <strong>apprentice</strong> from a <strong>32 bit process</strong> on a <strong>64 bit OS</strong>, and<br />- the document you have opened in apprentice has not been migrated to <strong>2015</strong> yet: i.e. <strong>ApprenticeServerDocument.NeedsMigrating =</strong> True, and<br />- you are calling functions which are affected by the issue, or you are using the viewer component</p>
<p>If you have only <strong>Inventor View 2015&#0160;</strong>installed, that should be working fine.</p>
<p>It seems that the issue is caused by not having 2 of the 32 bit dll&#39;s installed in the appropriate folder:<br /><strong>libeay32_Ad_1.dll</strong> and&#0160;<strong>ssleay32_Ad_1.dll </strong>- attached here:&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7001d89970b img-responsive"><a href="http://adndevblog.typepad.com/files/modules_x32.zip">Download Modules_x32</a></span></p>
<p>If you place the two <strong>32 bit</strong> dlls inside the <strong>C:\Program Files\Autodesk\Inventor 2015\Bin\Bin32</strong>&#0160;folder then this should solve the problem. &#0160;</p>
