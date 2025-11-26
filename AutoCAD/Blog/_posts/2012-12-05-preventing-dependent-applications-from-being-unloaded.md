---
layout: "post"
title: "Preventing dependent applications from being unloaded"
date: "2012-12-05 16:44:19"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/preventing-dependent-applications-from-being-unloaded.html "
typepad_basename: "preventing-dependent-applications-from-being-unloaded"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>Consider this scenario:&#0160; Your custom object is contained within a DBX module and its user interface is in a separate ARX module. How do you ensure that the DBX module can&#39;t be unloaded before   <br />the ARX module unloads.</p>
<p>The solution is to load one ARX/DBX module from another. You can use AcRxDynamicLinker::loadModule() or AcRxDynamicLinker::loadApp() to respond to the AcRx::kInitAppMsg message in the acrxEntryPoint() function of your module.</p>
<p>This should be paired with AcRxDynamicLinker::unloadModule or   <br />AcRxDynamicLinker::unloadApp respectively to unload the module when you have finished it (usually in response to AcRx::kUnloadAppMsg). The choice of function (loadApp or loadModule) depends on your application configuration. Lets look at both cases:</p>
<p><strong>The loadApp/unloadApp approach</strong>:</p>
<p>loadApp can be used when the module you want to use has a registry entry (You should refer to the ObjectARX help files for detailed information about this function). The trick is to set the asCmd parameter to false. This means that if the module is already loaded, its reference counter will be incremented. Such a call would look something like this:</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">acrxDynamicLinker-&gt;loadApp(</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;APPNAME&quot;</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">, </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160; AcadApp::LoadReasons::kOnLoadRequest,</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160; Adesk::kTrue, Adesk::kFalse);</span></span></span></p>
</div>
<p>To unload the module you must call unloadApp with its asCmd parameter also set to false. The corresponding unloadApp call to the loadApp call shown above is:</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">acrxDynamicLinker-&gt;unloadApp(</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;APPNAME&quot;</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">, </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160; Adesk::kFalse);</span></span></span></p>
</div>
<p>This will decrement the module&#39;s reference counter. The module will only unload if its reference counter reduces to zero. In this way, you can ensure that this module will only unload once every application that has loaded it has unloaded it.</p>
<p><strong>The loadModule/unloadModule approach</strong>:</p>
<p>loadModule works in exactly the same way as loadApp (refer to the helpfiles for full details), but it does not require the loaded module to have a registry entry. Instead, it requires the module&#39;s pathname. Unfortunately, loadModule does not search the AutoCAD search path, so you have to provide the full absolute pathname. If your module is in the AutoCAD search path, you can use the following function to find and load it:</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #008000;">// Parameter &quot;name&quot; is name of dbx application</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">bool</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> loadDependentModules(</span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">const</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> ACHAR * name)&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;"> {&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; ACHAR full_app_file_name[512] = L</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;&quot;</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">if</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> (RTNORM != acedFindFile(name, full_app_file_name))&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160; {&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160; acutPrintf(L</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;\nCouldn&#39;t find path for %s&quot;</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">,name);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">return</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">false</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160; }&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">if</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> (!acrxDynamicLinker-&gt;loadModule(</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160; full_app_file_name, </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160; Adesk::kTrue,Adesk::kFalse)</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160; )&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160; {&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160; acutPrintf(L</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;\nloadModule %s failed&quot;</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">,</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; full_app_file_name);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">return</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">false</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160; }&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">return</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">true</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;"> }</span></span></span></p>
</div>
<p>Unfortunately, using the acedFindFile() means that the above code will only work from an ARX application (not a DBX module). Therefore, DBX modules should either use loadApp/unloadApp, or use a full pathname in loadModule.</p>
