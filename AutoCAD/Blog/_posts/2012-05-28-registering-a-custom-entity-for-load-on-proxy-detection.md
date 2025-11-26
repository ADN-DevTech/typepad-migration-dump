---
layout: "post"
title: "Registering a custom entity for load on proxy detection"
date: "2012-05-28 03:45:13"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/registering-a-custom-entity-for-load-on-proxy-detection.html "
typepad_basename: "registering-a-custom-entity-for-load-on-proxy-detection"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have my custom entity which I registered for load on proxy detection, but AutoCAD does not load it automatically when a drawing containing my entity is opened. What could be the problem? <br />Here is my code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> RegisterDbxApp()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcadAppInfo appInfo;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; appInfo.setAppName(L</span><span style="line-height: 140%; color: #a31515;">"MYAPP"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; HMODULE hModule=GetModuleHandle(L</span><span style="line-height: 140%; color: #a31515;">"MyApp.dll"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; TCHAR modulePath[512];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; DWORD pathLength = GetModuleFileName(hModule, modulePath, 512);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pathLength)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; appInfo.setModuleName(modulePath); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; appInfo.setAppDesc(L</span><span style="line-height: 140%; color: #a31515;">"My DBX module"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; appInfo.setLoadReason(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcadApp::LoadReasons(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcadApp::kOnProxyDetection |</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcadApp::kOnLoadRequest));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; appInfo.writeToRegistry(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">,</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p><strong>Solution</strong></p>
<p>When AutoCAD tries to find your object enabler application then it is using the application name you defined in the ACRX_DXF_DEFINE_MEMBERS macro, which in your case is:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">ACRX_DXF_DEFINE_MEMBERS (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; MyEnt, AcDbEntity,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbProxyEntity::kNoOperation, MyEnt,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #a31515;">"MYAPP"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #a31515;">"|Product Desc:&nbsp;&nbsp;&nbsp;&nbsp; My App"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #a31515;">"|Company:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; My Company"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #a31515;">"|WEB Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; www.mycompany.com"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
<p>Since the macro already converts the parameters to string, therefore the application name will get extra " characters: "MYAPP" instead of MYAPP, so AutoCAD won’t find it in the registry:</p>


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016305e979ad970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016305e979ad970d image-full" alt="Myapp" title="Myapp" src="/assets/image_155871.jpg" border="0" /></a><br />

<p>You just have to remove the extra <strong>" </strong>characters. This way AutoCAD can find your application in the registry and can load it the next time a drawing containing your entity is opened:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ACRX_DXF_DEFINE_MEMBERS (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; MyEnt, AcDbEntity,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbProxyEntity::kNoOperation, MyEnt,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; MYAPP</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; |Product Desc:&nbsp; &nbsp;&nbsp; My App</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; |Company:&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; My Company</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; |WEB Address:&nbsp; &nbsp; &nbsp; www.mycompany.com</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
