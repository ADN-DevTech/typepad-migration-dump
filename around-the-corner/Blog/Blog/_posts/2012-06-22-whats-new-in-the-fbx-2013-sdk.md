---
layout: "post"
title: "What’s New in the FBX 2013 SDK?"
date: "2012-06-22 02:26:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "FBX"
  - "Kristine Middlemiss"
  - "Linux"
  - "Mac"
  - "Python"
  - "Visual Studio"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/whats-new-in-the-fbx-2013-sdk.html "
typepad_basename: "whats-new-in-the-fbx-2013-sdk"
typepad_status: "Publish"
---

<ul type="disc">
<li>The FBX SDK animation system has been completely redesigned.</li>
<li>Take nodes, take node containers, current takes, and FCurves have all been replaced by animation stacks (FbxAnimStack), animation layers (FbxAnimLayer) animation curve nodes (FbxAnimCurveNode), and animation curves (FbxAnimCurve).</li>
<li>The classes in the FBX SDK have all been renamed using the &quot;Fbx&quot; prefix, instead of &quot;KFbx&quot; or &quot;K&quot;. All structures have also been renamed with the same prefix, including global functions and enumerations. We did this to make the SDK simpler and more consistent.</li>
<li>The FbxFile class has been exposed in fbxfile.h. This class will be a basic utility class used by the FBX SDK to open/read/write/close files.</li>
<li>The FbxFileUtils class will expose all static functions related to file handling, such as remove, rename</li>
<li>The FbxPathUtils class will expose all static functions related to file path handling, such as IsRelative, Clean</li>
<li>Many FBX SDK functions which required file paths as inputs have been updated to support UTF-8. The parameter names were updated to reflect this change.</li>
<li>Removed the KFbxMemoryAllocator class. Instead, use handler setter functions: FbxSetMallocHandler located in fbxalloc.h</li>
<li>Removed all KFCurveFilter classes. Please use FbxAnimCurveFilter instead. It will be part of the animation refactoring that started in the 2011 version.</li>
<li>The FbxLight class has been augmented to support area lights and barn doors.</li>
<li>The FbxGeometryBase class has been augmented to support render options: PrimaryVisibility, CastShadow and ReceiveShadow.</li>
<li>It will now be possible to retrieve the &quot;long&quot; version string of the FBX SDK via the function FbxManager::GetVersion(true). This will allow developers to get the version string of this library along with the name and the revision number.</li>
<li>The FBX SDK will use secure versions of CRT calls on the Windows platform. It will no longer be necessary to disable these warnings when including the FBX SDK headers.</li>
<li>Changed how objects are stored in the manager to help increase performance and reduce memory.</li>
</ul>
