---
layout: "post"
title: "Resolving references when using tlbimp "
date: "2012-05-11 20:15:36"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/resolving-references-when-using-tlbimp-.html "
typepad_basename: "resolving-references-when-using-tlbimp-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To use a COM component in a .Net project, you will need an inter-op assembly created from the type-library which can be referenced in your .Net project. This is done automatically when a reference is added in a .Net project using the “Add Reference” and selecting a component from the COM tab. But it sometimes becomes important to generate these assemblies by using specific versions of the type library.</p>
<p>Type Library importer (tlbimp.exe) is a tool that can create the inter-op assembly from a type-library. In its simplest form, you can use “tlbimp” as follow:</p>
<p>For example if you are creating an inter-op assembly for the E-Transmit type-libary (C:\ObjectARX 2012\inc-x64\AcETransmit18.tlb)</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">tlbimp </span><span style="color: #a31515; line-height: 140%;">&quot;C:\ObjectARX 2012\inc-x64\AcETransmit18.tlb&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;">:AcETransmit18.Interop.dll </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;">:AcETransmit </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/machine:x64</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">tlbimp </span><span style="color: #a31515; line-height: 140%;">&quot;C:\ObjectARX 2012\inc-win32\AcETransmit18.tlb&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;">:AcETransmit18.Interop.dll </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;">:AcETransmit </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/machine:x86</span></p>
</div>
<p></p>
<p>If you are using tlbimp tool to create an inter-op assembly for one of your own COM components that imports other AutoCAD type-library such as “axdb18enu.tlb”, then you will additionally need to resolve the references. By default, if no additional reference paths are specified, the type-library importer will use the type libraries registered in the registry.</p>
<p>Here is an example usage to resolve the references explicitly:</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">tlbImp.exe </span><span style="color: #a31515; line-height: 140%;">&quot;C:\Test\LineCOMWrapper\x64\MyLineCOMWrapper.arx&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;">:</span><span style="color: #a31515; line-height: 140%;">&quot; MyLineCOMWrapper.dll&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;">:MyLine </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/nologo </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/reference:</span><span style="color: #a31515; line-height: 140%;">&quot;C:\\ObjectARX 2012\\inc-x64\\Autodesk.AutoCAD.Interop.Common.dll&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/tlbreference:</span><span style="color: #a31515; line-height: 140%;">&quot;C:\\ObjectARX 2012\\inc-x64\\axdb18enu.tlb&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/machine:x64&nbsp; </span></p>
<p></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tlbImp.exe </span><span style="color: #a31515; line-height: 140%;">&quot;C:\Test\LineCOMWrapper\Win32\MyLineCOMWrapper.arx&quot;</span><span style="line-height: 140%;"> /</span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;">:</span><span style="color: #a31515; line-height: 140%;">&quot;MyLineCOMWrapper.dll&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/</span><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;">:MyLine </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/nologo </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/reference:</span><span style="color: #a31515; line-height: 140%;">&quot;C:\\ObjectARX 2012\\inc-win32\\Autodesk.AutoCAD.Interop.Common.dll&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/tlbreference:</span><span style="color: #a31515; line-height: 140%;">&quot;C:\\ObjectARX 2012\\inc-win32\\axdb18enu.tlb&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/machine:x86</span></p>
</div>
<p></p>
<p>For a complete list of command line switches that can be used with tlbimp.exe, please refer to the MSDN documentation 
<a href = "http://msdn.microsoft.com/en-us/library/tt0cf3sx(v=vs.80).aspx">http://msdn.microsoft.com/en-us/library/tt0cf3sx(v=vs.80).aspx</a></p>
