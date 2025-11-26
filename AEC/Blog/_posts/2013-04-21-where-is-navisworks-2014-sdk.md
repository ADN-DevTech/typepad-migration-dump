---
layout: "post"
title: "Where is Navisworks 2014 SDK ?"
date: "2013-04-21 19:56:07"
author: "Xiaodong Liang"
categories:
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/04/where-is-navisworks-2014-sdk.html "
typepad_basename: "where-is-navisworks-2014-sdk"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>We’d firstly say sorry to anybody who were struggling to look for the SDK of Navisworks 2014 in the past few weeks. The SDK was not packaged with product . It is an independent installer which is now available at <a href="http://www.autodesk.com/developnavisworks ">ADN Open</a> . You can also download it directly from the link below:</p>
<p><img alt="" height="16" src="/assets/exe.gif" width="16" /> <a href="http://images.autodesk.com/adsk/files/Navisworks_API_SDK.exe">Navisworks 2014 SDK</a> <em>(exe - 217 Mb)</em></p>
<p>In default, if you have installed Navisworks 2014, the installer will be deployed &lt;Navisworks Installation Path&gt;\api\, same as previous. If you have not installed Navisworks 2014, it will be deployed to &lt;My Documents&gt;\Autodesk\Navisworks 2014\api\. You can also put&#0160; the SDK to any folder you like.</p>
<p>What’s new with the SDK?</p>
<p>1. New samples are provided. </p>
<p>&#0160; NET:&#0160;&#0160; <strong>InputAndRenderHandling</strong> is a comprehensive demo on the three new types of plugins: RenderPlugin, ToolPlugin and InputPlugin.&#0160;&#0160; </p>
<p> COM:&#0160; <strong>ActiveXWebpageExample</strong> is a sample similar to the previous demo <strong>ACTX_01</strong>. It was missed in Navisworks 2013 SDK. Now it comes back. It uses the Redistributable x86 ActiveX control. You can also test it with other types of ActiveX controls.&#0160; </p>
<p>2. The API reference NET API.chm adds the descriptions on the new features. And the chapter [<strong>Takeoff</strong>] encloses some demos on how to use Quantification.&#0160; </p>
<p>3. Both X86 and X64 installers and merge modules for ActiveX Redistributable control are provided at \api\COM\bin.</p>
