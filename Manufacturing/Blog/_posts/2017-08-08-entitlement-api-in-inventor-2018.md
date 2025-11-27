---
layout: "post"
title: "Entitlement API in Inventor 2018"
date: "2017-08-08 09:58:00"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/08/entitlement-api-in-inventor-2018.html "
typepad_basename: "entitlement-api-in-inventor-2018"
typepad_status: "Publish"
---

<p><span style="font-size: medium;"><span style="font-family: Arial;">By <a href="http://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener noreferrer" target="_blank">Chandra shekar Gopal</a></span></span></p>
<p><span style="font-size: medium;"><span style="font-family: Arial;"><strong>Entitlement API</strong> are used in getting <strong>Autodesk 360</strong> users account information. It also helps in determining genuinely of users. Whether the user is downloaded from <strong>App Store</strong> or copied from somewhere else. </span></span></p>
<p><span style="font-family: Arial; font-size: medium;">In previous version of Inventor (2016 &amp; 2017), <strong>Entitlement API</strong> is provided by <strong>AdWebServices.dll </strong>and is in <strong>C++. </strong>It is explained in <a href="http://adndevblog.typepad.com/manufacturing/2015/03/entitlement-api-in-inventor.html" rel="noopener noreferrer" target="_blank">this post</a></span><span style="font-family: Arial; font-size: medium;">. </span></p>
<p><span style="font-family: Arial; font-size: medium;">In Inventor 2018, <strong>Entitlement API </strong>is provided by a <strong>.net Wrapper</strong> called &quot;<strong>AddinNETFramework.AdWebServicesWrapper.dll</strong>&quot; </span></p>
<p><span style="font-family: Arial; font-size: medium;">The <strong>.net Wrapper </strong>is available at installed location <strong>(C:\Program Files\Autodesk\Inventor 2018\Bin)</strong> </span></p>
<p><span style="font-family: Arial; font-size: medium;">So, in order to access <strong>Entitlement API</strong>, AddReference &quot;<strong>AddinNETFramework.AdWebServicesWrapper.dll</strong>&quot; from installed location to the application. </span></p>
<p><span style="font-size: medium;"><span style="font-family: Arial;">Sample C# code.</span> </span></p>
<blockquote>
<p><span style="font-size: 8pt;">using Autodesk.WebServices;</span></p>
<p><br /><span style="font-size: 8pt;">CWebServicesManager mgr = new CWebServicesManager();</span></p>
<p><span style="font-size: 8pt;">bool isInitialize = mgr.Initialize();</span></p>
<p><span style="font-size: 8pt;">if (isInitialize == true)</span><br /><span style="font-size: 8pt;">{</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160; string userId = &quot;&quot;;</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160; mgr.GetUserId(ref userId);</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160; string username = &quot;&quot;;</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160; mgr.GetLoginUserName(ref username);</span></p>
<p><span style="font-size: 8pt;">}</span></p>
</blockquote>
<p><span style="font-family: Arial; font-size: medium;">Sample VB.net code.</span></p>
<blockquote>
<p><span style="font-size: 8pt;">Imports Autodesk.WebServices</span></p>
<p><br /><span style="font-size: 8pt;">Dim webServiceMgr As CWebServicesManager</span></p>
<p><span style="font-size: 8pt;">webServiceMgr = New CWebServicesManager()</span></p>
<p><span style="font-size: 8pt;">Dim isWebServiceInitialized As Boolean</span></p>
<p><span style="font-size: 8pt;">isWebServiceInitialized = webServiceMgr.Initialize()</span></p>
<p><span style="font-size: 8pt;">If isWebServiceInitialized = True Then</span></p>
<p><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160; Dim userId As String = &quot;&quot; webService</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160;&#0160; Mgr.GetUserId(userId)</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160;&#0160; Dim userName As String = &quot;&quot; webService</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160;&#0160; Mgr.GetLoginUserName(userName)</span></p>
<p><br /><span style="font-size: 8pt;">End If</span></p>
</blockquote>
<p><span style="font-size: medium;"><span style="font-family: Arial;"><span style="text-decoration: underline;"><strong>Note:</strong></span> Changes in entitlement API for <strong>Inventor 2020</strong> is blogged in <a href="https://modthemachine.typepad.com/my_weblog/2019/09/entitlement-api-changes-in-inventor-2020.html">this post</a>.</span></span></p>
