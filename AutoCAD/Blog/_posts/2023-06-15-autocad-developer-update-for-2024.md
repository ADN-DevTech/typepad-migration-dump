---
layout: "post"
title: "AutoCAD developer update for 2024"
date: "2023-06-15 06:50:00"
author: "Sreeparna Mandal"
categories:
  - "Sreeparna Mandal"
original_url: "https://adndevblog.typepad.com/autocad/2023/06/autocad-developer-update-for-2024.html "
typepad_basename: "autocad-developer-update-for-2024"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/autocad/sreeparna-mandal.html" target="_self">Sreeparna Mandal</a></p>

<div style="text-align: center;"><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cb0ce8200b-pi"><img width="298" height="200" title="Picture1" style="display: inline; background-image: none;" alt="Picture1" src="/assets/image_610694.jpg" border="0"></a></div>

<p>
The <a href="https://www.autodesk.com/developer-network/platform-technologies/autocad">AutoCAD Developer Center</a> has been updated with the following:
<br>
<ol>
<li>Wizards</li>

<ul>
<li>ObjectARX 2024 Wizard</li>
</ul>
<ul>
<li>AutoCAD 2024 .NET Wizard</li>
</ul>

<li>Training Material</li>

<ul>
<li>ObjectARX 2024 Training Labs</li>
</ul>
<ul>
<li>AutoCAD 2024 .NET Training Labs</li>
</ul>

<li>My First AutoCAD Plug-In tutorial for AutoCAD 2024</li>
</ol>


<br>

<p>
<h3>Important updates for AutoCAD 2024</h3>
<ol>
<li>DWG file format compatibility</li>
<ul>
<li>No Change</li>
</ul><br>

<li>API binary compatibility</li>
<ul>
<li>Maintained for AutoCAD 2024</li>
AutoCAD 2024 is a binary compatibility release. ObjectARX applications developed for AutoCAD 2021, AutoCAD 2022, or AutoCAD 2023 shouldn't need to be recompiled. Applications developed for AutoCAD 2020 and earlier releases will need to be recompiled.
</ul><br>

<li>Development Environment</li>
<ul>
<li>Visual Studio 2022 v17.2.6</li>
<li>New ObjectARX applications built for AutoCAD 2024 will use VC143 toolset, however applications built with previous toolset VC142 is also compatible with AutoCAD 2024</li>
<li>.NET Framework v4.8</li>
</ul><br>

<li>LISP Support for AutoCAD LT</li>
<ul>
<li>We are enabling end-user LISP support for AutoCAD LT</li>
</ul>

</ol>


<br>

<p>
<h3>API changes post ObjectARX 2024 migration</h3>
<br>

<h4><u>Moved Files in ObjectARX API</u></h4>
<ul>
<li>class AcCmColor</li>
</ul>
<br>

<h4><u>Deprecated ObjectARX APIs</u></h4>
<ul>
<li>AcDbGripData::AcDbGripData () Constructor</li>
<li>AcDbGripData::AcDbGripData (AcGePoint3d&amp;, void*, AcRxClass*, GripOperationPtr, GripOperationPtr, GripRtClkHandler, GripWorldDrawPtr, GripViewportDrawPtr, GripOpStatusPtr, GripToolTipPtr, GripDimensionPtr, GripDimensionPtr, unsigned int, AcGePoint3d*, GripInputPointPtr) Constructor</li>

<br>
The alternate <strong>API to be used</strong> instead of the above deprecated API is: <br>
AcDbGripData(const AcGePoint3d&amp;, void* AppData); 
</ul>
<br>

<h4><u>Modified structures/API in ObjectARX</u></h4>
<ul>
<li>AcadApp structure declaration</li>
<li>AcDbAssocTransInfo structure declaration</li>
<li>AcDbMultiModesGripPE::GripMode structure declaration</li>
<li>AcDbMultiModesGripPE::GripMode::ActionType Data Member </li>
<li>AcDbMultiModesGripPE::GripMode::CursorType Data Member </li>
<li>AcDbMultiModesGripPE::GripMode::GripMode Constructor </li>
<li>AcGsKernelDescriptor::hasRequirement Method </li>
</ul>
<br>

<em>For detailed information on Moved/Deprecated/Modified API, refer to the documentations(arxref.chm) in the <a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-download">ObjectARX 2024 SDK</a></em>
