---
layout: "post"
title: "Maya 2025 API Update guide"
date: "2024-03-26 18:21:06"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2024/03/maya-2025-api-update-guide.html "
typepad_basename: "maya-2025-api-update-guide"
typepad_status: "Publish"
---

<p>The guide is based on What’s New in the Maya Devkit in Maya 2025 with some extra info. The details could be found <a href="https://help.autodesk.com/view/MAYADEV/2025/ENU/?guid=2025-Whats-New-in-API">here in the document</a>.</p>

<h2>Building environment</h2>

<p>In Maya 2025, the building environment changes on Mac.</p>

<p>Operating System   Requirements </p>

<p>Windows            Visual Studio 2022 (17.8.3 or higher) |
macOS              Xcode version 14.3 or higher |
Linux              RHEL or Rocky Linux 8.6 or higher, DTS-11 with gcc 11.2.1 |</p>

<p>The minimum version of CMake required is 3.27.3.</p>

<h2>Migration to Qt6 and PySide6</h2>

<p>Maya has moved from Qt5 and PySide2 to Qt6 and PySide6.</p>

<p>There are official migration guides for <a href="https://doc.qt.io/qt-6/portingguide.html">C++</a> and <a href="https://doc.qt.io/qtforpython-6/gettingstarted/porting_from2.html">Python</a>.</p>

<p>For more details, please check out our <a href="https://help.autodesk.com/view/MAYADEV/2025/ENU/?guid=Qt6Migration">Maya Qt6 Migration</a> page.</p>

<p>If you want to use PyQt6, please install <em>PyQt6==6.5.3</em> with pip.</p>

<h2>Maya is moving to .NET 8</h2>

<p>Maya 2025 is moving to .NET 8, and .NET 8 is now the default version of .NET used by Maya.</p>

<p>For more details, please check out our <a href="https://help.autodesk.com/view/MAYADEV/2025/ENU/?guid=DotNet8">Maya is moving to .NET 8</a> section.</p>

<h2>SDK Changes for Maya 2025</h2>

<p>Here are some API changes in the SDK for Maya 2025:</p>

<ul>
<li>Three new methods have been added to <strong>MFnAttribute</strong>.</li>
<li>Two new methods have been added to <strong>MFnGeometryFilter</strong>.</li>
<li>Two new color management methods have been added.</li>
<li><strong>addPostDuplicateNodeListCallback()</strong> added to <strong>MModelMessage</strong>.</li>
<li><strong>extendSelectionFromComponents()</strong> has been added to <strong>MPxSurfaceShape</strong>.</li>
<li><strong>MQtUtil::resourceGLContext()</strong> now returns <strong>QOpenGLContext</strong>.</li>
<li><strong>MFnWeightGeometryFilter</strong> has been added to Python API 2.0.</li>
<li>Some samples in the devkit have been updated.</li>
</ul>

<p>For the full details about devkit changes, please refer to the <a href="https://help.autodesk.com/view/MAYADEV/2025/ENU/?guid=NewInAPI">SDK changes for Maya 2025</a>.</p>

<h2>Bonustools</h2>

<p>Bonustools is shipping with Maya 2025 now. You don't need to get it separately.</p>
