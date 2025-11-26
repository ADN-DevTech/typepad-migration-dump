---
layout: "post"
title: "AutoCAD Map 3D Industry Data Modeling Plug-ins and their execution targets"
date: "2013-11-13 23:45:40"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2013"
  - "AutoCAD Map 3D 2014"
  - "Map 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/autocad-map-3d-industry-data-modeling-plug-ins-and-their-execution-targets.html "
typepad_basename: "autocad-map-3d-industry-data-modeling-plug-ins-and-their-execution-targets"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>You have developed a custom Industry Data Modeling Plug-in in AutoCAD Map 3D and if you are wondering how to restrict the usage to only in AutoCAD Map 3D, here is the answer :&#0160;</p>
<p>A typical tbp file looks like the following where in you define the plugins execution target.</p>
<p style="padding-left: 30px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&lt;PlugIn&gt;&#0160;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&lt;Default</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">AssemblyName=&quot;MyCompany.Mapping.MyTest.dll&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">Namespace=&quot;MyCompany.Mapping.MyTest&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">Priority=&quot;100&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt; background-color: #ffff00;">ExecutionTargetDesktop=&quot;True&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">ExecutionTargetWeb=&quot;False&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">ExecutionTargetAdmin=&quot;False&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">DMCode=&quot;&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">Company=&quot;MyCompany&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">Author=&quot;Partha Sarkar&quot;</span></p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">/&gt;</span>&#0160;</p>
<p style="padding-left: 60px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&lt;ApplicationPlugIn ClassName=&quot;MyTestApplicationPlugin&quot;/&gt;/&gt;&#0160;</span></p>
<p style="padding-left: 30px;"><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&lt;/PlugIn&gt;</span>&#0160;</p>
<p>&#0160;</p>
<p>In this post let me explain you the various options of the execution targets and when we should use them -</p>
<p>&#0160;</p>
<p><strong>ExecutionTargetDesktop</strong>&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : used for “AutoCAD Map3D Client” (Map3D + Industry Model Data Editor + Infrastructure Administrator + IMBatch.exe)&#0160;</p>
<p><strong>ExecutionTargetWeb&#0160;&#0160;</strong>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : used for Industry Data Model Extensions (Web)&#0160;</p>
<p>&#0160;</p>
<p>Most plug-ins only use these two, but some need the more specific properties like the following:</p>
<p>&#0160;</p>
<p><strong>ExecutionTargetClient *</strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; : used specifically for AutoCAD Map 3D</p>
<p><strong>ExecutionTargetStandalone&#0160;</strong>&#0160; &#0160;: used specifically for Industry Model Data Editor</p>
<p><strong>ExecutionTargetAdmin&#0160;</strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;: used&#0160; specifically for Industry Data Model Administrator / Infrastructure Administrator.</p>
<p><strong>ExecutionTargetBatch</strong>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : used specifically for IMBatch.exe / Industry Data Model Batch applications.&#0160;&#0160;&#0160;&#0160;</p>
<p>&#0160;&#0160;</p>
<p><strong>*</strong> You can use ‘<em><strong>ExecutionTargetClient=”true”</strong></em>’ for plugins that require graphical data interaction.</p>
