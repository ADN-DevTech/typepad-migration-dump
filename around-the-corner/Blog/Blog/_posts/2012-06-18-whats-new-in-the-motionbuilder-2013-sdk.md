---
layout: "post"
title: "What’s New in the MotionBuilder 2013 SDK?"
date: "2012-06-18 01:21:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "MotionBuilder"
  - "Python"
  - "Qt"
  - "Visual Studio"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/whats-new-in-the-motionbuilder-2013-sdk.html "
typepad_basename: "whats-new-in-the-motionbuilder-2013-sdk"
typepad_status: "Publish"
---

<ul type="disc">
  <li>Programming Environment and Code Refactoring Changes</li>
  <ul type="circle">
    <li>MotionBuilder and its distributed plug-ins are now compiled using Visual Studio 2010 (Service Pack 1).</li>
    <li>FBContainer has been renamed to FBVisualContainer for uniformity of cross-product naming schemes. </li>
    <li>const correctness improvements. </li>
    <li>Although not enforced in this release, we recommend that the &quot;H&quot; prefix in classes such as HFBPlug be removed and replaced by the pointer suffix &quot;*&quot;, for example: FBPlug*. This change is intended to facilitate the use of the documentation system in the near future. </li>
    <li>FBShader has been heavily refactored for performance.</li>
  </ul>
  <li>New Classes</li>
  <ul type="circle">
    <li>FBSyncReference - Will allow the MotionBuilder application to sync its time with a device's time. For more information, consult the ordevicesync_device plugin. </li>
    <li>FBTimeCode - A timecode contains the time and frame information used to identify a specific moment in a timeline, often used in video and audio production. This class can help in the conversion of a point in time from one framerate domain to another. </li>
    <li>FBHUD, FBHUDElement - These classes represent the new Heads-Up Display (HUD) feature in MotionBuilder. A HUD will be connected to a camera, and appears as a 2D overlay. This overlay can be used to display information about the take: the current time and frame number </li>
    <li>FBFileMonitoringManager - This class can be used to help monitor operating system file events. By contrast, the event properties in FBApplication will only monitor events triggered within the MotionBuilder application.</li>
    <li>FBNamespace - This class has been exposed to help facilitate the use of namespaces among scene elements. Functions: FBFindModelByLabelName(), FBFbxOptions::NamespaceList, FBFbxOptions::SetNamespaceList(), FBFbxOptions::GetNamespaceList(), and the FBScene's namespace related functions (FBScene::NamespaceImport()) can filter, select, import and export scene elements using namespaces.</li>
  </ul>
  <li>New Methods and Added Functionality </li>
  <ul type="circle">
    <li>FBAnimationNode::KeyCandidate(), FBPropertyAnimatable::KeyAt() - These functions &nbsp;provide the ability to key at a given time without moving time. </li>
    <li>FBSystem.ConfigPath, FBSystem.UserConfigPath, FBSystem.TempPath - These properties respectively will define MotionBuilder software&rsquo;s directory paths for config files, user config files, and temporary files. </li>
    <li>FBAudioClip - A substantial amount of functionality has been added to this class. Consult the header files and class documentation for more information. </li>
    <li>FBMenuManager - Functionality has been added to this class to help manipulate and remove MotionBuilder software&rsquo;s UI top menu bar. </li>
    <li>FBComponent::GetFullName() - Retrieves an object's unique name. </li>
    <li>FBStory, FBStoryFolder, FBStoryTrack - Added the capability to record stories on disk with properties: FBStory::RecordToDisk and FBStoryFolder::RecordClipPath. </li>
    <li>FBDevice::ModelBindingCreate, FBDevice::ModelBindingRootsList - These functions allow you to create a model binding from a device, or obtain a list of possible root models which can be bound to the device. </li>
    <li>FBModel - Exposed additional properties to facilitate custom rendering. Viewport dimensions can be set in the model, which will only be updated and valid during custom renderer callbacks (FBModel::CameraViewportX, FBModel::CameraViewportY, FBModel::CameraViewportWidth, FBModel::CameraViewportHeight). Motion blur properties have also been added: FBModel::MotionBlurIntensity, FBModel::UseMotionBlur, FBModel::UseRealTimeMotionBlur. </li>
    <li>FBModelNull, FBModelMarker - Overloaded several FBModel base class functions in these subclasses to help increase user customization. These functions are: FBModelMarker/FBModelNull::FbxStore(), FBModelMarker/FBModelNull::FbxRetrieve(), FBModelMarker/FBModelNull::FbxGetObject(), and FBModelMarker/FBModelNull::FbxGetObjectSubType(). </li>
    <li>FBMaterial - Custom material support, and added sample project (OpenRealitySDK/Samples/miscellaneous/material_template) </li>
    <li>FBRenderer - Added new functionality: FBRenderer::GetViewingCamera() to obtain the current camera, FBRenderer::InPicking() to determine if the renderer will be in the picking phase, and a variety of certain other functions and properties related to picking. </li>
    <li>FBAudioRenderOptions() - This class has been added to help specify audio rendering options, which can be passed as a parameter to FBApplication::AudioRender(). Consult the Scripts/Samples/Audio/AudioRendering.py sample for more details. </li>
    <li>FBEvaluateManager - Added functionality will allow for computationally intensive tasks to be registered and run as callbacks in a background thread (FBEvaluateManager::RegisterEvaluationGlobalFunction(), FBEvaluateManager::UnregisterEvaluationGlobalFunction())</li>
  </ul>
  <li>Misc and Optimizations</li>
  <ul type="circle">
    <li>Optimized functions FBFindObjectByFullName() and FBDeleteObjectsByNames(). </li>
    <li>Optimized the merging of multiple files transaction workflow: FBMergeTransactionBegin(), FBMergeTransactionEnd().</li>
  </ul>
</ul>
