---
layout: "post"
title: "Where is AutoCAD MEP SDK?"
date: "2012-07-22 19:12:29"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD MEP"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/07/where-is-autocad-mep-sdk.html "
typepad_basename: "where-is-autocad-mep-sdk"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada/" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>We have been using AutoCAD MEP contents and want to further customize some of the work we have been doing.&#0160;&#0160;I&#0160;searched&#0160;public ADN site. I see AutoCAD and AutoCAD Architecture, but&#0160;I cannot find anything about AutoCAD MEP.&#0160; What kind of API does MEP have?&#0160; Where can I download the SDK?&#0160;&#0160;</p>
<p><strong>Solusion</strong></p>
<p>API for AutoCAD MEP (AME)&#0160;is .NET.&#0160;&#0160;There is no C++ API like ObjectARX for AutoCAD.&#0160;Managed modules, which you need to reference in the .NET project, are all in the product install. You don&#39;t need anything&#0160;additional to customize AME using .NET API.&#0160;The&#0160;install location&#0160;for 2013 release typically looks like this.</p>
<p>&#0160;&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD 2013\MEP</p>
<p>The managed modules have a naming of *mgd.dll.</p>
<p>Help files are found under Help folder:<br /><br />&#0160;&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD 2013\Help\amemgd.chm</p>
<p>Samples are under Sample folder:<br /><br />&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD 2013\Sample\CS.NET<br />&#0160;&#0160;&#0160;C:\Program Files\Autodesk\AutoCAD 2013\Sample\VB.NET</p>
<p>Note: products install location has been changed in 2013 release. For 2012 releases and earlier, they are as follows:&#0160; <br /><br />&#0160;&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD MEP 2012\<br />&#0160;&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD MEP 2012\Help<br />&#0160;&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD MEP 2012\Sample\CS.NET<br />&#0160;&#0160;&#0160; C:\Program Files\Autodesk\AutoCAD MEP 2012\Sample\VB.NET</p>
<p>You can find learning materials from the webcast archives. Go to here: <br /><a href="http://www.adskconsulting.com/adn/cs/api_course_webcast_archive.php">http://www.adskconsulting.com/adn/cs/api_course_webcast_archive.php</a></p>
<p>Look for on “AutoCAD MEP .NET”.</p>
<p>You can also find materials from the 2008 AEC DevCamp in AutoCAD/ACA/AME track, which you can also download from the above archive page.&#0160;</p>
<p>The following are discussion forum for AutoCAD, Architecture and MEP: <br /><br /><a href="http://forums.autodesk.com/t5/AutoCAD-MEP/bd-p/61">http://forums.autodesk.com/t5/AutoCAD-MEP/bd-p/61</a><br /><a href="http://forums.autodesk.com/t5/AutoCAD-Architecture/bd-p/54">http://forums.autodesk.com/t5/AutoCAD-Architecture/bd-p/54</a><br /><a href="http://forums.autodesk.com/t5/NET/bd-p/152">http://forums.autodesk.com/t5/NET/bd-p/152</a></p>
<p>Since AutoCAD MEP is built on top of AutoCAD and AutoCAD Architecture, depending on the type of question, you may use those discussion forums.</p>
