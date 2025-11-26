---
layout: "post"
title: "Maya 2026 API Update guide"
date: "2025-03-25 22:21:46"
author: "Cheng Xi Li"
categories:
  - ".Net"
  - "C++"
  - "Cheng Xi Li"
  - "Linux"
  - "Mac"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2025/03/maya-2026-api-update-guide.html "
typepad_basename: "maya-2026-api-update-guide"
typepad_status: "Publish"
---

<p>The guide is based on What’s New in the Maya Devkit in Maya 2026 with some extra info. The details could be found <a href="https://help.autodesk.com/view/MAYADEV/2026/ENU/">here</a>.</p>

<h2>Building environment</h2>

<p>On the macOS, Xcode has been updated to 14.3 or higher. Other platforms remain the same as Maya 2025.</p>

<ul>
<li>Windows - Visual Studio 2022 (17.8.3 or higher)      </li>
<li><strong>macOS</strong> - <strong>Xcode version 14.3 or higher</strong>       </li>
<li>Linux - RHEL 8.6 or higher, DTS-11 with gcc 11.2.1 </li>
</ul>

<h2>The minimum version of CMake required is 3.27.3.</h2>

<h2>SDK/Devkit Changes</h2>

<p>SDK Changes for Maya 2026</p>

<p>Here are some API changes in the SDK for Maya 2026:</p>

<ul>
<li>Added TBB to the Devkit</li>
<li>Python has been updated from 3.11.4 to 3.11.9</li>
<li>Alembic has been updated</li>
<li>Updated the usage of <strong>canMakeLive</strong> method in <strong>gpuCacheShapeNode.cpp</strong></li>
<li>Updated the dx11Shader to make it reload the shader after loading the file the for security reasons.</li>
<li>The stock standard material has been replaced with stock OpenPBR in several samples.</li>
<li>Added a method to get matrix for matching local matrix in <strong>MDagPath</strong>.</li>
<li>Added a method for locking/unlocking the plug in <strong>MDGModifier</strong>.</li>
<li>Added two methods to get color and values at positions in <strong>MRampAttribute</strong>.</li>
<li>Added a method for extracting geometry in <strong>MGeometryExtractor</strong>.</li>
<li>Using MObject instead of MDagPath in the <strong>MGeometryExtractorConstructHelper</strong></li>
<li>Added enums for supporting boolean operations and a method for boolean operations in <strong>MFnMesh</strong>.</li>
<li>Added the three methods for selected objects in <strong>M3dView</strong>. Fixed a typo of parameter in the <strong>getM3dViewFromModelPanel</strong> method.</li>
<li>Added an enum and a method for setting axis of faces in <strong>MFnVolumeLight</strong>.</li>
<li>Added an enumeration and methods for setting and getting culling modes <em>MHWGeometry</em>*.</li>
<li>Added two stock shaders for <strong>MShaderManager</strong>.</li>
<li>Added a pointer to a callback function which takes a string and a boolean in <strong>MMessage</strong>.</li>
<li>Added a callback to be notified when the view selected state or view selected objects for any 3D view changes in <strong>MUiMessage</strong> (not available in .NET). </li>
<li>Added flip the image from left to right or upside down <strong>MPxImagePlane</strong>.</li>
<li>Added a function that returns if the shading node can handle consolidated geometry and methods to get fragment name or update shader based if a fragment or shader is textured in <strong>MPxShadingNodeOverride</strong>.</li>
</ul>

<p>For the full details about devkit changes, please refer to the <a href="https://help.autodesk.com/view/MAYADEV/2026/ENU/?guid=2026-Whats-New-in-API">What's new in the Maya 2026 devkit</a>.</p>
