---
layout: "post"
title: "Maya 2020 API update guide"
date: "2019-12-10 05:08:12"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Plug-in"
original_url: "https://around-the-corner.typepad.com/adn/2019/12/maya-2020-api-update-guide.html "
typepad_basename: "maya-2020-api-update-guide"
typepad_status: "Publish"
---

<div style="font-family: 'Roboto Condensed', Tauri, 'Hiragino Sans GB', 'Microsoft YaHei', STHeiti, SimSun, 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', 'Segoe UI', AppleSDGothicNeo-Medium, 'Malgun Gothic', Verdana, Tahoma, sans-serif; font-size: 15px; overflow-x: hidden; overflow-y: auto; margin: 0px !important; padding: 20px; background-color: #ffffff; color: #222222; line-height: 1.6; -webkit-font-smoothing: antialiased; background: #ffffff;">
<p style="margin: 1em 0px; word-wrap: break-word;">The guide is based on What's New in the Maya Devkit in Maya 2020 with some extra info. Check the Maya documentation for the most up to date information.</p>
<p style="margin: 1em 0px; word-wrap: break-word;"><strong>For Mac users, Apple has introduced notarization since OS X 10.14.5 and it is going to be enforced for Applications after 10.15. It requires the host application to declare entitlements for the plug-ins. Although Maya is the parent application, you should be aware that it may cause your plug-in to fail in these Mac releases if Maya didn't declare the entitlement. Please test your plug-in in both 10.14.5 and 10.15 if possible. For more details please check <a href="https://developer.apple.com/documentation/security/notarizing_your_app_before_distribution" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Notarizing Your App Before Distribution</a> from Apple.</strong></p>
<h2 id="build-environment:" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#build-environment:" name="build-environment:" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Build environment:</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">The build environments have been upgraded. Mac has been updated to Mojave 10.14.x with Xcode 10.2.1, and Windows has been updated to Windows 10 with VS2017. Linux remains unchanged.</p>
<table style="padding: 0px; border-collapse: collapse; border-spacing: 0px; margin-bottom: 16px; background-color: #fafafa;">
<thead>
<tr>
<th style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px; font-weight: bold;">Platform</th>
<th style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px; font-weight: bold;">OS</th>
<th style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px; font-weight: bold;">Compiler</th>
</tr>
</thead>
<tbody>
<tr>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;">Windows</strong></td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;">Windows 10 x64</strong></td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px;">Visual Studio 2017</strong> <br style="clear: both; margin-bottom: 0px;" />Windows SDK Version 10.0.10586.0</td>
</tr>
<tr style="background-color: #f2f2f2;">
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;">Mac</strong></td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;">Mojave 10.14.x</strong></td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;">XCode 10.2.1</strong></td>
</tr>
<tr>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Linux</td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">CentOS/RHEL 7.3(x&gt;=3)</td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">gcc 6.3.1(DTS 6.1)</td>
</tr>
</tbody>
</table>
<h2 id="developer-resources" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#developer-resources" name="developer-resources" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Developer resources</h2>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">The Maya docs are being updated with API changes. You can find the most up to date information for the API in the <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_What_s_New_What_s_Changed_2020_Whats_New_in_API_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">What's New / What's Changed</a> section.</p>
</li>
</ul>
<h2 id="autodesk-standard-surface-shader" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#autodesk-standard-surface-shader" name="autodesk-standard-surface-shader" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Autodesk Standard Surface Shader</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;"><strong>Introduction</strong></p>
<p style="margin: 1em 0px; word-wrap: break-word;">In Maya 2020, a new Maya node called <em>standardSurface</em> will be shipped as a new surface shading node.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">Based on the <a href="https://autodesk.github.io/standard-surface/" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Autodesk Standard Surface specification</a>, it is a renderer agnostic uber surface shader that aims to provide a material representation capable of accurately modeling the vast majority of materials used in practical visual effects and feature animation productions. The next release of Maya will have an implementation of the Autodesk Standard Surface based on version v1.0.1 of the specification.</p>
<p style="margin: 1em 0px; word-wrap: break-word;"><strong>Purpose and Scope</strong></p>
<p style="margin: 1em 0px; word-wrap: break-word;">Using the Standard Surface node will allow more flexibility for rendering and for data interchange between applications.</p>
<ul>
<li>Allows multiple renderers to get similar rendering results by using the same native Maya surface node.</li>
<li>Allows scene importer/exporter plugins to keep as much shading information.</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;"><strong>Compatibility</strong></p>
<p style="margin: 1em 0px; word-wrap: break-word;">The Standard Surface specification is a common base that can be extended (or reduced) by render engines that support it. For example, the Arnold Standard Surface shader has some additional parameters like <em>subsurface_type</em> or <em>exittobackground</em> not defined in the specification. The Arnold renderer plugin for Maya (MtoA) is responsible for extending the Maya Standard Surface node with those specific parameters. On the other side of the spectrum, some real-time renderers might not support some advanced parameters. In this scenario, some simplification can be done by zeroing the combination weights of the unsupported layers like <em>transmission</em> or <em>subsurface</em> for example. See the <a href="https://autodesk.github.io/standard-surface/#layeredmixturemodel/compatibilitymode" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Compatibility section</a> in the Autodesk Standard Surface specification.</p>
<p style="margin: 1em 0px; word-wrap: break-word;"><strong>Roadmap</strong></p>
<p style="margin: 1em 0px; word-wrap: break-word;">Deprecating the Lambert shader in favor of the Standard Surface as the de facto default surface shader in Maya is the final goal. However since the Lambert shader has been used since the very first version of Maya, we decided to keep it as the default surface shader for now. You can experiment using the standardSurface as the default surface shader in the next release of Maya by setting the environment variable "MAYA_DEFAULT_SURFACE_SHADER" to "standardSurface".</p>
<p style="margin: 1em 0px; word-wrap: break-word;"><strong>API Updates</strong></p>
<p style="margin: 1em 0px; word-wrap: break-word;">The following classes, methods, and types were added to the Viewport 2.0 API to support the new shader.</p>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New classes</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MFnStandardSurfaceShader</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New types</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MFn.kStandardSurface<br style="clear: both;" />k3dStandardSurfaceShader in MStockShader</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New methods</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MShaderInstance::addInputFragmentForMultiParams()</p>
</blockquote>
</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">Refer to <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_What_s_New_What_s_Changed_2020_Whats_New_in_API_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">What's New / What's Changed</a> and <a href="https://autodesk.github.io/standard-surface/" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">Autodesk Standard Surface specification</a> for details.</p>
<h2 id="devkit-updates" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#devkit-updates" name="devkit-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Devkit updates</h2>
<h5 id="new-samples" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-samples" name="new-samples" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New samples</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">basicBlendShapeDeformer - Deformer Subsetting: MPxBlendShape plugins<br style="clear: both;" />simpleSimulationNode - Cached playback support<br style="clear: both;" />pyPanelCanvasInfo.py - Graph Editor overlay drawing support<br style="clear: both;" />pyPanelCanvas.py - Graph Editor overlay drawing support</p>
</blockquote>
<h5 id="samples-updated" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#samples-updated" name="samples-updated" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Samples updated</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">apiMeshShape - Added component selection support to the example and Light plugin enhancements following internal changes to vizualization of objectSets<br style="clear: both;" />apiMeshSubSceneOverride - Selection update<br style="clear: both;" />viewObjectSetOverride - Light plugin enhancements following internal changes to vizualization of objectSets<br style="clear: both;" />footPrintNode_GeometryOverride - rewrite for better caching support<br style="clear: both;" />footPrintNode_GeometryOverride_AnimatedMaterial - requireRenderItemUpdate default value to false<br style="clear: both;" />geometryOverrideExample1 - requireRenderItemUpdate default value to false<br style="clear: both;" />geometryOverrideExample2 - requireRenderItemUpdate default value to false<br style="clear: both;" />customImagePlane - Revisited plugin after deprecation of MPxImagePlaneOverride</p>
</blockquote>
<h5 id="samples-removed-from-devkit/plug-ins" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#samples-removed-from-devkit/plug-ins" name="samples-removed-from-devkit/plug-ins" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Samples removed from devkit/plug-ins</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">geometryOverrideHighPerformance<br style="clear: both;" />AnimEngine - A similar AnimX example is available from <a href="https://github.com/Autodesk/animx" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">https://github.com/Autodesk/animx</a></p>
</blockquote>
<h5 id="headers-removed-from-devkit" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#headers-removed-from-devkit" name="headers-removed-from-devkit" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Headers removed from devkit</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MRenderSetupPrivate.h</p>
</blockquote>
<h5 id="new-headers-included" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-headers-included" name="new-headers-included" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New headers included</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MCacheMode.h<br style="clear: both;" />MCacheSchema.h<br style="clear: both;" />MPlugin.h<br style="clear: both;" />MTimeRange.h</p>
</blockquote>
<h3 id="api-updates-in-maya-2020" style="clear: both; font-size: 1.6em; font-weight: bold; margin: 1.125em 0px 0.75em;"><a href="#api-updates-in-maya-2020" name="api-updates-in-maya-2020" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>API Updates in Maya 2020</h3>
<h5 id="graph-editor-overlay-drawing" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#graph-editor-overlay-drawing" name="graph-editor-overlay-drawing" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Graph Editor overlay drawing</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">This feature allow users to draw primitives over the Graph Editor. Several new classes, methods, and examples were added to the API to support Graph Editor overlay drawing.</p>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New C++ and Python classes</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPanelCanvas<br style="clear: both;" />MPanelCanvasInfo</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New Python samples</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">pyPanelCanvasInfo.py<br style="clear: both;" />pyPanelCanvas.py</p>
</blockquote>
</li>
</ul>
<h5 id="cached-playback-support" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#cached-playback-support" name="cached-playback-support" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Cached Playback Support</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Your plug-in can now support Cached Playback. These new classes and methods have been added to the API to make it easier for you to define and configure your node for Cached Playback.</p>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New classes</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MNodeCacheDisablingInfo<br style="clear: both;" />MNodeCacheDisablingInfoHelper<br style="clear: both;" />MNodeCacheSetupInfo<br style="clear: both;" />MCacheMode<br style="clear: both;" />MCacheSchema<br style="clear: both;" />MTimeRange</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New methods</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPxGeometryOverride::configCache()<br style="clear: both;" />MPxNode::configCache()<br style="clear: both;" />MPxNode::transformInvalidRange()<br style="clear: both;" />MPxNode::hasInvalidationRangeTransformation()<br style="clear: both;" />MPxNode::getCacheSetup()<br style="clear: both;" />MPxLocatorNode::getCacheSetup()<br style="clear: both;" />MNodeCacheDisablingInfo::setUnsafeNode()</p>
</blockquote>
</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">simpleSimulationNode example have been added to the devkit to showcase the new cached playback APIs.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">Refer to <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_cpp_ref_index_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">C++ API reference</a> for details.</p>
<h5 id="other-new-classes-and-methods" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#other-new-classes-and-methods" name="other-new-classes-and-methods" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Other New Classes and Methods</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Here are a few more classes and methods that have been added to the API. They're new classes and methods to help with your deformer, selection pass rendering, and iterating over geometry data.</p>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New C++ classes</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MGPUEventList<br style="clear: both;" />MIndexMapper</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New methods for selection pass rendering</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPxSubSceneOverride::enableUpdateForSelection()<br style="clear: both;" />MFrameContext::getSelectionInfo()</p>
</blockquote>
<p style="margin: 1em 0px; word-wrap: break-word;">apiMeshSubSceneOverride example have been updated to use the new methods.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New method added to MItGeometry</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MItGeometry::positionIndex()</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New Python 2.0 classes</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MItGeometry<br style="clear: both;" />MPyIterObject</p>
</blockquote>
</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">Refer to <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_What_s_New_What_s_Changed_2020_Whats_New_in_API_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">What's New / What's Changed</a> and the <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_cpp_ref_index_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">C++ API reference</a> for details.</p>
<h5 id="fixes-and-changes" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#fixes-and-changes" name="fixes-and-changes" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Fixes and Changes</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Several third party libraries have been updated. Some methods which have been changed or fixed as well. If your plug-in uses the following methods, check the docs to see what's been fixed or changed.</p>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Third party libraries</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px;">Boost has been updated to 1.61. Maya is replacing boost with std, some headers have been removed from the boost library shipped with Maya.</p>
<p style="margin: 1em 0px; word-wrap: break-word; margin-bottom: 0px;">Qt has been updated to Qt 5.12.5.</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">OSX Plugin export symbols<br style="clear: both;" />If you wanted to update your existing plugin makefile for Mac, here are the export symbols.</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">__Z16initializePluginN8Autodesk4Maya16OpenMaya202000007MObjectE<br style="clear: both;" />__Z18uninitializePluginN8Autodesk4Maya16OpenMaya202000007MObjectE</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Fixed feature</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPxTransform::boundingBox() and MPxTransform::isBounded()</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Change in signature</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MFnPlugin::registerEvaluator()<br style="clear: both;" />MPxNode::configCache()<br style="clear: both;" />MFnPlugin::registerImageFile()<br style="clear: both;" />MeshIntersector::create()</p>
</blockquote>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Change in default value</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPxGeometryOverride::requireRenderItemUpdate</p>
</blockquote>
<p style="margin: 1em 0px; word-wrap: break-word;">footPrintNode_GeometryOverride_AnimatedMaterial, geometryOverrideExample1, and geometryOverrideExample2 examples have been updated to reflect this change.</p>
</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">Refer to <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_What_s_New_What_s_Changed_2020_Whats_New_in_API_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">What's New / What's Changed</a> for details.</p>
<h5 id="deprecated-classes" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#deprecated-classes" name="deprecated-classes" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Deprecated classes</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Creating custom image plane has been simplified. If you’re creating a custom image plane, you may want to update your plug-in because the following class have been deprecated.</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPxImagePlaneOverride</p>
</blockquote>
<p style="margin: 1em 0px; word-wrap: break-word;">Refer to <a href="http://help.autodesk.com/view/MAYAUL/2020/ENU/?guid=Maya_SDK_MERGED_What_s_New_What_s_Changed_2020_Whats_New_in_API_html" style="text-decoration: none; vertical-align: baseline; color: #3269a0;">What's New / What's Changed</a> for details.<br style="clear: both;" />`</p>
</div>
