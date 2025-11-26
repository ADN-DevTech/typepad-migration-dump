---
layout: "post"
title: "Include or precompiled"
date: "2013-01-17 00:15:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Maya"
  - "Visual Studio"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2013/01/include-or-precompiled.html "
typepad_basename: "include-or-precompiled"
typepad_status: "Publish"
---

<p>Usage of precompiled headers may significantly reduce compilation time, especially when applied to big header files or header files that include many other header files or header files that are included in many translation units. And when you consider template files, that will save you even more time.</p>
<p>The beauty of precompiled headers is that even if most of the time they are made per project, they do not have to. You could create a precompiled header (pch for Windows, or gch for GCC/Linux) with all your library includes once, and use/consume it from multiple project without to have to pre-recompile them.</p>
<p>However, many people do not take advantage of this features because they somewhat misuse it by including their own project headers into the collection, and each time they modify something, the precompiled header has to be recompiled. So no benefit... Really, someone should only put static header files in the collection, and almost never a project header unless they never change. The Maya, MotionBuilder, 3ds Max, SoftImage SDK/API headers are good candidates for that feature as once shipped with the product, they aren't supposed to change until the next version. Some other good guys are MFC, Boost, ATL, Qt, etc...</p>
<p>While working on a new version of the Maya C++/Python/.Net wizards, I started developing a master pre-compiled header for Maya API including some pragma directives to automate some of the settings such as an automatic library inclusion as we can see in Boost, ATL, MFC, ... The advantage of these pragma is that porting the plug-in to the next version of the devkit becomes very easy. But I'll come back to that another time.</p>
<p>The new C++ project wizard will create a file like the one below with some pre-processor directives as well to include/exclude some of the not always used API.</p>
<pre class="brush: cpp; toolbar: false;">//
//  Copyright 2012 Autodesk, Inc.  All rights reserved.
//
//  Use of this software is subject to the terms of the Autodesk license 
//  agreement provided at the time of installation or download, or which 
//  otherwise accompanies this software in either electronic or hard copy form.   
//

//-----------------------------------------------------------------------------
//- StdAfx.h : include file for standard system include files,
//-      or project specific include files that are used frequently,
//-      but are changed infrequently
//-----------------------------------------------------------------------------
#pragma once

//-----------------------------------------------------------------------------
//----- This file is preprocessor symbol driven.
//----- Define:
//----- _PYTHON_MODULE_ to include and import Python headers and libs in your project.
//----- _PYTHON_MODULE_ to include and import Python headers and libs in your project.
//----- _MAYA_QT_ to include and import Maya QT headers and libs in your project.
//----- _MAYA_VP2_ to include and import Maya Viewport 2.0 headers and libs in your project.

#include "mayaHeaders.h"

//-----------------------------------------------------------------------------
//----- If you need MFnPlugin definition more than once
//- MNoVersionString is needed on MacOS and Linux to avoid multiple MApiVersion definition
//#define MNoVersionString
//- MNoPluginEntry is needed on Windows platform to avoid multiple DllMain definition
//#define MNoPluginEntry
//#include &lt;maya/MFnPlugin.h&gt;

// Add your other lib headers here</pre>
<p>The "mayaHeaders.h" which have the Maya headers and pragma will be dependent of the Maya devkit version, and might be included in the Maya devkit in future.</p>
<pre class="brush: cpp; toolbar: false;">//
//  Copyright 2012 Autodesk, Inc.  All rights reserved.
//
//  Use of this software is subject to the terms of the Autodesk license 
//  agreement provided at the time of installation or download, or which 
//  otherwise accompanies this software in either electronic or hard copy form.   
//

//-----------------------------------------------------------------------------
#pragma once

#if defined(_MANAGED)
#pragma unmanaged
#pragma message(__FILE__ " Turning down managed code")
#endif

#if defined(_MANAGED) &amp;&amp; !defined(MNoMSTypedef)
//- The MS typedef can cause errors when linking with .NET libraries on Windows.
//- Use the MNoMSTypedef define to force all status codes to be prefixed by MStatus:: instead of MS::.
#define MNoMSTypedef
#pragma message(__FILE__ " Turning MNoMSTypedef on - MS typedef can cause errors when linking with .NET libraries on Windows.")
#endif

//-----------------------------------------------------------------------------
#ifdef NT_PLUGIN
#define EXPORT comment(linker, "/EXPORT:"__FUNCTION__"="__FUNCDNAME__)
#else
#define EXPORT 
#endif

//-----------------------------------------------------------------------------
#define MayaOk(a,b) \
	if ( !a ) { \
	a.perror (b) ; \
	return (a) ; \
	}
#define NodeRegisterOk(a) MayaOk(a,_T("registerNode"))
#define NodeUnregisterOk(a) MayaOk(a,_T("deregisterNode"))

//-----------------------------------------------------------------------------
//----- This file is preprocessor symbol driven.
//----- Define:
//----- _PYTHON_MODULE_ to include and import Python headers and libs in your project.

//-----------------------------------------------------------------------------
#ifdef NT_PLUGIN
#pragma comment (lib, "cg.lib")
#pragma comment (lib, "cgGL.lib")
#pragma comment (lib, "Cloth.lib")
#pragma comment (lib, "Foundation.lib")
#pragma comment (lib, "Image.lib")
#pragma comment (lib, "IMFbase.lib")
#pragma comment (lib, "libawxml2.lib")
#pragma comment (lib, "libHalf.lib")
#pragma comment (lib, "libIex.lib") // Link this always (2013 change). Otherwise: 4 missing symbols when linking
#if !defined(_MANAGED)
#pragma comment (lib, "libIlmImf.lib")
#endif
#pragma comment (lib, "libImath.lib")
#pragma comment (lib, "libmocap.lib")
#pragma comment (lib, "libzlib.lib")
#pragma comment (lib, "OpenMaya.lib")
#pragma comment (lib, "OpenMayaAnim.lib")
#pragma comment (lib, "OpenMayaFX.lib")
#pragma comment (lib, "OpenMayaRender.lib")
#pragma comment (lib, "OpenMayaUI.lib")
#ifdef _PYTHON_MODULE_
#pragma comment (lib, "python26.lib")
#endif
#pragma comment (lib, "tbb.lib")
#pragma comment (lib, "tbbmalloc.lib")
#endif

//-----------------------------------------------------------------------------
#include &lt;maya/MTypeId.h&gt;
#include &lt;maya/MTypes.h&gt;
#include &lt;maya/flib.h&gt;
#include &lt;maya/ilib.h&gt;
#include &lt;maya/M3dView.h&gt;
#include &lt;maya/MAngle.h&gt;
#include &lt;maya/MAnimControl.h&gt;
#include &lt;maya/MAnimCurveChange.h&gt;
#include &lt;maya/MAnimCurveClipboard.h&gt;
#include &lt;maya/MAnimCurveClipboardItem.h&gt;
#include &lt;maya/MAnimCurveClipboardItemArray.h&gt;
#include &lt;maya/MAnimMessage.h&gt;
#include &lt;maya/MAnimUtil.h&gt;
//#include &lt;maya/MApiVersion.h&gt;
#include &lt;maya/MArgDatabase.h&gt;
#include &lt;maya/MArgList.h&gt;
#include &lt;maya/MArgParser.h&gt;
#include &lt;maya/MArrayDataBuilder.h&gt;

// etc...
</pre>
<p>This current version (Maya 2013 / 2013 Extension 1&amp;2) full source is located <a href="http://around-the-corner.typepad.com/files/mayaHeaders.h" target="_self">here</a>,</p>
</pre>
