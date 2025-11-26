---
layout: "post"
title: "What are the &lsquo;Load Controls&rsquo; in AutoCAD OEM"
date: "2013-07-16 16:06:59"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD OEM"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/what-are-the-load-controls-in-autocad-oem.html "
typepad_basename: "what-are-the-load-controls-in-autocad-oem"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>The Load Controls section of the AutoCAD OEMMakeWizard define the demand loading required for your module. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e4c052b970b-pi"><img title="image" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="image" src="/assets/image_507599.jpg" width="540" height="343" /></a></p>  <p>It relates directly to the ObjectARX AcadApp::LoadReasons enum, e.g.</p>  <p><strong>struct AcadApp {     <br />&#160; enum LoadReasons {      <br />&#160;&#160;&#160; kOnProxyDetection = 0x01,      <br />&#160;&#160;&#160; kOnAutoCADStartup = 0x02,      <br />&#160;&#160;&#160; kOnCommandInvocation = 0x04,      <br />&#160;&#160;&#160; kOnLoadRequest = 0x08,      <br />&#160;&#160;&#160; kLoadDisabled = 0x10,      <br />&#160;&#160;&#160; kTransparentlyLoadable = 0x20,      <br />&#160;&#160;&#160; kOnIdleLoad = 0x40      <br />&#160; };      <br /></strong></p>  <p>The Load Controls are used for all ObjectARX, ObjectDBX, .NET and Mixed Mode DLLs.</p>
