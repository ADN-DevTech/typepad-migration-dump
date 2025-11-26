---
layout: "post"
title: "Maya 2019 API Updates guide"
date: "2019-01-15 00:28:03"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2019/01/maya-2019-api-updates-guide.html "
typepad_basename: "maya-2019-api-updates-guide"
typepad_status: "Publish"
---

<div style="font-family: &#39;Roboto Condensed&#39;, Tauri, &#39;Hiragino Sans GB&#39;, &#39;Microsoft YaHei&#39;, STHeiti, SimSun, &#39;Lucida Grande&#39;, &#39;Lucida Sans Unicode&#39;, &#39;Lucida Sans&#39;, &#39;Segoe UI&#39;, AppleSDGothicNeo-Medium, &#39;Malgun Gothic&#39;, Verdana, Tahoma, sans-serif; font-size: 15px; overflow-x: hidden; overflow-y: auto; margin: 0px !important; padding: 20px; background-color: #ffffff; color: #222222; line-height: 1.6; -webkit-font-smoothing: antialiased; background: #ffffff;">
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Maya 2019 release has been released now. Here is a guide based on What&#39;s New in API in Maya 2019 with some extra info.</p>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">For more details, please check our documents <a href="https://help.autodesk.com/view/MAYAUL/2019/ENU/?guid=__developer_What_s_New_What_s_Changed__What_s_New_in_API_in_Maya_2019_html">here</a>.</p>
<h2 id="build-environment:" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#build-environment:" name="build-environment:" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Build environment:</h2>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Windows and MacOSX remain unchanged. Linux is updated to CentOS/RHEL 7.x(x&gt;=2) and using gcc 6.3.1 from DTS 6.1</p>
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
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Windows</td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Windows 7 x64</td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Visual Studio 2015 Update 3 <br style="clear: both; margin-top: 0px; margin-bottom: 0px;" />Windows SDK Version 10.0.10586.0</td>
</tr>
<tr style="background-color: #f2f2f2;">
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">Mac</td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">El Capitan 10.11.6</td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;">XCode 7.3.1</td>
</tr>
<tr>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;"> Linux </strong></td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;">CentOS/RHEL 7.x(x&gt;=2) </strong></td>
<td style="text-align: center; border: 1px solid #e6e6e6; margin: 0px; padding: 6px 13px;"><strong style="margin-top: 0px; margin-bottom: 0px;"> gcc 6.3.1(DTS 6.1)</strong></td>
</tr>
</tbody>
</table>
<p><strong>Important:</strong>&#0160;Maya sets the&#0160;<span class="code">SSL_CERT_FILE</span>&#0160;environment variable to point to the&#0160;<span class="code">cert.pem</span>&#0160;file within its embedded Python Framework if&#0160;<span class="code">SSL_CERT_FILE</span>&#0160;is unset.</p>
<p>If you need&#0160;<span class="code">SSL_CERT_FILE</span>&#0160;to remain unset because you are using other means of controlling Python&#39;s certificate usage, set&#0160;<span class="code">MAYA_DO_NOT_SET_SSL_CERT_FILE</span>&#0160;<strong>before</strong>&#0160;launching Maya:</p>
<div class="codeBlock">
<pre class="prettyprint prettyprinted">  <span class="kwd">export</span><span class="pln"> MAYA_DO_NOT_SET_SSL_CERT_FILE</span><span class="pun">=</span><span class="lit">1</span></pre>
</div>
<h2 id="devkit-updates" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#devkit-updates" name="devkit-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Devkit updates</h2>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Devkit samples of Maya 2019 use CMake to generate makefiles/solutions.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new geometryOverrideExample1/geometryOverrideExample1.cpp example plug-in, which demonstrates how to render geometry with a stock shader.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new geometryOverrideExample2/geometryOverrideExample2.cpp example plug-in, which demonstrates how render geometry with a Maya shader network.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new geometryOverrideHighPerformance/geometryOverrideHighPerformance example, which demonstrates how to use MPxGeometryOverride to render geometry with the best performance as possible.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new footPrintNode_GeometryOverride/footPrintNode_GeometryOverride.cpp example plug-in to make it fully compatible with Evaluation Caching.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new footPrintNode_GeometryOverride_AnimatedMaterial/footPrintNode_GeometryOverride_AnimatedMaterial.cpp example, which demonstrates how to draw a simple mesh like foot Print in an efficient way.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new nameFilter/NameFilter.cpp example, which demonstrates how to create custom configuration rule filters with new API</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new testMTopologyEvaluator/testMTopologyEvaluator.cpp example, which demonstrates the features of topology evaluators.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new topologyTrackingNode/topologyTrackingNode.cpp example, which demonstrates the use of the new version of the attributeAffects() relationship which can take a parameter specifying whether or not the relationship affects topology.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">A number of devkit examples have been updated to remove deprecated methods.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">An updated captureViewRenderCmd/captureViewRenderCmd.cpp devkit example now shows how to disable the output transform in Viewport 2.0 using the MRenderer::render() API.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Add a guide for cmake in readme.md</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">libOpenMayalib.a is removed in Linux.</p>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">Added a new tessellatedQuad example plug-in to the Maya devkit. The example demonstrates how to implement custom selection shader for kPatch render items in a MPxGeometryOverride. It also shows how to share geometry/index streams among several render items to avoid expensive recalculation.</p>
</li>
</ul>
<h5 id="samples-removed-from-devkit/plug-ins" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#samples-removed-from-devkit/plug-ins" name="samples-removed-from-devkit/plug-ins" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Samples removed from devkit/plug-ins</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">Cg(moved to headers)<br style="clear: both;" />blastCmd<br style="clear: both;" />curvedArrowsNode<br style="clear: both;" />D3DViewportRender<br style="clear: both;" />D3DViewportRenderer<br style="clear: both;" />DX11ViewportRenderer<br style="clear: both;" />GL<br style="clear: both;" />hwAnisotropicShader_NV20<br style="clear: both;" />hwColorPerVertexShader<br style="clear: both;" />hwDecalBumpShader_NV20<br style="clear: both;" />hwReflectBumpShader_NV20<br style="clear: both;" />hwRefractReflectShader_NV20<br style="clear: both;" />hwToonShader_NV20<br style="clear: both;" />hwUnlitShader<br style="clear: both;" />OpenGLViewportRender<br style="clear: both;" />pnTriangles<br style="clear: both;" />quadricShapes<br style="clear: both;" />selectClosestPointLocator<br style="clear: both;" />ShapeMonitor</p>
</blockquote>
<h5 id="headers-removed-from-devkit" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#headers-removed-from-devkit" name="headers-removed-from-devkit" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Headers removed from devkit</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">d3dx11effect.h<br style="clear: both;" />d3dxGlobal.h</p>
</blockquote>
<h5 id="new-headers-included" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-headers-included" name="new-headers-included" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New headers included</h5>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px;">libxml<br style="clear: both;" />Universal Front End</p>
<ul style="margin-bottom: 0px;">
<li>Universal Front End is to create a DCC-agnostic component that will allow a DCC to browse and edit data in multiple data models.<br style="clear: both;" />The initial data model targets to be supported are the Maya DG, Bifrost, and USD.</li>
</ul>
</blockquote>
<h3 id="api-updates-in-maya-2019" style="clear: both; font-size: 1.6em; font-weight: bold; margin: 1.125em 0px 0.75em;"><a href="#api-updates-in-maya-2019" name="api-updates-in-maya-2019" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>API Updates in Maya 2019</h3>
<h3 id="rendering-and-lighting" style="clear: both; font-size: 1.6em; font-weight: bold; margin: 1.125em 0px 0.75em;"><a href="#rendering-and-lighting" name="rendering-and-lighting" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Rendering and Lighting</h3>
<h5 id="mrenderitem-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mrenderitem-class-updates" name="mrenderitem-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MRenderItem class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">When a 3D model view activates Isolate Select for components, Viewport 2.0 creates and maintains necessary render items to represent the drawing of the isolate selected components specifically for that view. These render items are copies of their original items and so they have the same properties including name, type, primitive type, and draw mode. However, their shading components are filtered from the view selected set of that view.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">New methods have been added to allow custom render items created in a MPxGeometryOverride implementation to have their copies created for the drawing of isolate selected components. They also provide access of the view-selected shading components so that the MPxGeometryOverride implementation can fill in geometries properly.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">These new methods include:</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MRenderItem::shadingComponent()<br style="clear: both;" />MRenderItem::setAllowIsolateSelectCopy()<br style="clear: both;" />MRenderItem::allowIsolateSelectCopy()<br style="clear: both;" />MRenderItem::isIsolateSelectCopy()</p>
</blockquote>
<p style="margin: 1em 0px; word-wrap: break-word;">The apiMeshShape example plug-in has been updated to use these new methods, and the Python API also includes these changes.</p>
<h5 id="mselectioninfo-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mselectioninfo-class-updates" name="mselectioninfo-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MSelectionInfo class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">The mask argument has changed from a non-const reference to const reference for the following methods:</p>
<ul>
<li>MSelectionInfo::selectable()</li>
<li>MSelectionInfo::selectableComponent()</li>
</ul>
<h5 id="mpxgeometryoverride-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mpxgeometryoverride-class-updates" name="mpxgeometryoverride-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MPxGeometryOverride class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Updates to the both C++/Python MPxGeometryOverride class include the following new methods:</p>
<ul>
<li>MPxGeometryOverride::requiresGeometryUpdate(), which determines if other MPxGeometryOverride methods get called for the associated DAG object during the draw preparation phase.</li>
<li>MPxGeometryOverride::requiresUpdateRenderItems(), which gets called when an instance of a DAG object changes to determine whether MPxGeometryOverride::updateRenderItems() needs to be called.</li>
<li>MPxGeometryOverride::supportsEvaluationManagerParallelUpdate(), which gets called to determine if the scene is supported by the Evaluation Manager Parallel Update.</li>
<li>MPxGeometryOverride::supportsVP2CustomCaching(), which gets called during evaluation to determine if VP2 Custom Caching is supported.</li>
</ul>
<h5 id="mpxdrawoverride-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mpxdrawoverride-class-updates" name="mpxdrawoverride-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MPxDrawOverride class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">A new MPxDrawOverride::pointSnappingActive() method allows a draw override to query whether snapping to points is active.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">A new MPxDrawOverride::userSelect() interface. It allows for adding a list of DAG paths/components and world space hit points.</p>
<h5 id="mshadermanager-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mshadermanager-class-updates" name="mshadermanager-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MShaderManager class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">A new MShaderManager::getArraySize method. It returns array size of the given parameter.</p>
<h5 id="mrenderitem-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mrenderitem-updates" name="mrenderitem-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MRenderItem Updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">You can now control consolidation per-object to MPxGeometryOverride. The following new methods have been added to let you control whether render items participate in consolidation:</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MRenderItem::setWantConsolidation()<br style="clear: both;" />MRenderItem::wantConsolidation()</p>
</blockquote>
<p style="margin: 1em 0px; word-wrap: break-word;">This change also deprecates the following methods:</p>
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MRenderItem::setWantSubSceneConsolidation()<br style="clear: both;" />MRenderItem::wantSubSceneConsolidation()</p>
</blockquote>
<p style="margin: 1em 0px; word-wrap: break-word;">The footPrintNode_SubSceneOverride example plug-in has been updated to use these new methods.</p>
<h5 id="new-mcolormanagementnodes-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-mcolormanagementnodes-class-updates" name="new-mcolormanagementnodes-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New MColorManagementNodes class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">The MColorManagementNodes C++ API class has been added that allows you to manipulate color managed nodes; for example: you can color manage one or all input nodes, and query whether an object is color manageable or color managed. A colorManageAllNodes flag has also been added to the colorManagementPrefs MEL/Python command.</p>
<h2 id="evaluation-and-performance" style="clear: both; font-size: 1.8em; font-weight: bold; margin: 1.275em 0px 0.85em; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #e6e6e6;"><a href="#evaluation-and-performance" name="evaluation-and-performance" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Evaluation and Performance</h2>
<h5 id="new-class-mpxtopologyevaluator" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-class-mpxtopologyevaluator" name="new-class-mpxtopologyevaluator" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New class MPxTopologyEvaluator</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">The new class allows topology override of evaluation graph. This makes possible to have total control over granularity, leverage already existing scheduling constraints and scheduling graph. Supports partial evaluation for minimal evaluation of cluster content. Finally, allows better resource utilization since execution of cluster content doesn’t have to wait for entire upstream to start and downstream can start as soon as dependencies are ready (sort of transparent clusters, but much more configurable and with no constraints on granularity).</p>
<h5 id="mfndependencynode-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mfndependencynode-class-updates" name="mfndependencynode-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MFnDependencyNode class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">New methods added to the MFnDependencyNode class let you set and manage which nodes have permission to be scheduled for evaluation by the evaluation graph. Nodes that do not have permission for evaluation can be created and destroyed without invalidating the evaluation graph.The Python API also includes these new methods.</p>
<ul>
<li>MFnDependencyNode::setAllowedToAnimate(), sets whether or not a node has permission to be added to the evaluation graph for scheduling during playback or manipulation. Nodes without permission, cannot be added to the evaluation graph.</li>
<li>MFnDependencyNode::allowedToAnimate() indicates whether or not the node has permission for evaluation based on MFnDependencyNode::setAllowedToAnimate().</li>
</ul>
<h5 id="mgrapheditorinfo-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mgrapheditorinfo-class-updates" name="mgrapheditorinfo-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MGraphEditorInfo class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Added the following new methods to the MGraphEditorInfo class:</p>
<ul>
<li>MGraphEditorInfo::isStackedViewportMode(), which returns whether or not the Graph Editor is in Stacked View mode.</li>
<li>MGraphEditorInfo::isNormalizedViewportMode(), which returns whether or not the Graph Editor is in Normalized View mode.</li>
</ul>
<h5 id="mprofiler-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mprofiler-class-updates" name="mprofiler-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MProfiler class updates</h5>
<ul>
<li>New versions of the MProfiler::addCategory() and MProfiler::getAllCategories() methods now use a categoryInfo parameter in addition to the previous accepted parameters. Previous versions of these methods have been deprecated.</li>
<li>A new MProfiler::getCategoryInfo() method returns the profiling category description.</li>
</ul>
<h5 id="mpxnode-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mpxnode-class-updates" name="mpxnode-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MPxNode class updates</h5>
<ul>
<li>Add two methods for controlling topology dirtying
<blockquote>
<p style="margin: 1em 0px; word-wrap: break-word; margin-top: 0px; margin-bottom: 0px;">MPxNode::isTrackingTopology<br style="clear: both;" />MPxNode::attributeAffects</p>
</blockquote>
</li>
</ul>
<h5 id="new-mpxcacheconfigrulefilter-and-mcacheconfigruleregistry-classes" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-mpxcacheconfigrulefilter-and-mcacheconfigruleregistry-classes" name="new-mpxcacheconfigrulefilter-and-mcacheconfigruleregistry-classes" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New MPxCacheConfigRuleFilter and MCacheConfigRuleRegistry classes</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">A new MPxCacheConfigRuleFilter class lets you define custom configuration rule filters for background evaluation caching. The new class includes the following methods:</p>
<ul>
<li>MPxCacheConfigRuleFilter::preRulesExecution(), which is called when the cache configuration rule application starts.</li>
<li>MPxCacheConfigRuleFilter::postRulesExecution(), which is called when the cache configuration rule application stops.</li>
<li>MPxCacheConfigRuleFilter::isMatch(), which gets called for each evaluation node when filter rules are applied for the cache configuration.</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">A new MCacheConfigRuleRegistry class includes a static method for registering and deregistering MPxCacheConfigRuleFilter custom classes.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">A new nameFilter/NameFilter.cpp example demonstrates how to create custom configuration rule filters.</p>
<h3 id="general-and-modeling" style="clear: both; font-size: 1.6em; font-weight: bold; margin: 1.125em 0px 0.75em;"><a href="#general-and-modeling" name="general-and-modeling" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>General and Modeling</h3>
<h5 id="muidrawmanager-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#muidrawmanager-class-updates" name="muidrawmanager-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MUIDrawManager class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Updates to the MUIDrawManager include the following:</p>
<ul>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New cylinder and capsule primitive extension methods:</p>
<ul>
<li>MUIDrawManager::cylinder()</li>
<li>MUIDrawManager::capsule()</li>
</ul>
</li>
<li>
<p style="margin: 1em 0px; word-wrap: break-word;">New parametric primitive methods now let you define the number of subdivisions for primitives:</p>
<ul>
<li>MUIDrawManager::sphere()</li>
<li>MUIDrawManager::circle()</li>
<li>MUIDrawManager::circle2d()</li>
<li>MUIDrawManager::arc()</li>
<li>MUIDrawManager::arc2d()</li>
<li>MUIDrawManager::cone()</li>
</ul>
</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">The Python API also includes these methods. Updates to the uiDrawManager example plug-in in the Maya devkit include the new methods.</p>
<h5 id="new-mcameramessage-class" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#new-mcameramessage-class" name="new-mcameramessage-class" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>New MCameraMessage class</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">A new MCameraMessage class lets you register callbacks for interactive camera manipulation messages. To remove a callback use MMessage::removeCallback(). Note that all callbacks registered by a plug-in must be removed by that plug-in when it is unloaded. Failure to do so will result in a fatal error. The Python API has also been updated with these new methods.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">A new cameraMessageCmd/cameraMessageCmd.cpp plug-in example demonstrates how to use each of the new camera manipulation callbacks.</p>
<h5 id="mfnmesh-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mfnmesh-class-updates" name="mfnmesh-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MFnMesh class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">A new version of the MFnMesh::create() method now stores vertices in doubles and allows edge connections.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">The OpenMaya.MFnMesh.create () method in the Python API has also been updated with this improvement.</p>
<h5 id="mfn-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mfn-class-updates" name="mfn-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MFn class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Added new kPinToGeometryUV, kPinToGeometryProx and kUfeProxyTransform enums.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">Note, kPinToGeometryUV and kPinToGeometryProx are fater kClosestPointOnMesh. If you are using numbers directly instead of enum, it may cause problem.</p>
<h5 id="mpolymessage-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mpolymessage-class-updates" name="mpolymessage-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MPolyMessage class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Added the MPolyMessage::addColorSetChangedCallback() method to register a callback when ColorSetChanged is modified.</p>
<p style="margin: 1em 0px; word-wrap: break-word;">Added the following new enums to specify the type of color set change message:</p>
<ul>
<li>kColorSetAdded when a new color set gets added.</li>
<li>kColorSetDeleted when a color set gets deleted.</li>
<li>kCurrentColorSetChanged when the current color set gets changed.</li>
</ul>
<p style="margin: 1em 0px; word-wrap: break-word;">The Python API has also been updated to include these changes.</p>
<h5 id="mglobal-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mglobal-class-updates" name="mglobal-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MGlobal class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Updates to the MGlobal class include the addition of the MGlobal::executeTaskOnIdle() method. Use this method to execute a customized task to execute on the next idle event. This is a thread safe way to schedule a task for the main thread to execute. <strong>This method is not available in Python.</strong></p>
<p style="margin: 1em 0px; word-wrap: break-word;">Add a new method MGlobal::customVersion.</p>
<h5 id="mmessage-class-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#mmessage-class-updates" name="mmessage-class-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>MMessage class updates</h5>
<p style="margin-top: 0px; margin: 1em 0px; word-wrap: break-word;">Added the MMessage::stopRegisteringCallableScript() method. Use this method to stop the MMessage object from being passed.</p>
<h5 id="other-api-updates" style="clear: both; font-size: 1.2em; font-weight: bold; margin: 0.855em 0px 0.57em;"><a href="#other-api-updates" name="other-api-updates" style="text-decoration: none; vertical-align: baseline; color: #3269a0;"></a>Other API updates</h5>
<ul>
<li>Added a new kAfterCreateReference enum to the MSceneMessage class for Python. The enum provides support for MSceneMessage.addReferenceCallback().</li>
<li>Added a new MColorPickerUtilities class, which lets you create a color picker that can grab colors from third-party plugins.</li>
<li>Added a new callback MPxIkSolverNode::isAttributeCreatedBySolver. It returns whether a certain attribute on the ikHandle was created by the solver (and affecting the result of the solve).</li>
<li>Added a new class MArrayIteratorTemplate. It enables iterators for Maya array classes.</li>
<li>Added a new method MFnAnimCurve::insertKey</li>
<li>The MnCloth::setAddCrossLinks() method now uses a boolean instead of a float value.</li>
<li>doubleBuffered is renamed to softwareStaged in MVertexBuffer and MIndexBuffer. When using MVertexBuffer/MIndexBuffer in conjunction with Evaluation Manager Parallel Update in MPxGeometryOverride the buffer used must be software staged.</li>
<li>In Maya 2019, we changed the deprecation mechanism for several methods. These methods have already been marked as being obsolete in the documentation for several years but now they will generate warnings at the compilation time. The documentation usually explains which alternate method to use. In the meantime the warning/error can be disabled by using the _OPENMAYA_DEPRECATION_PUSH_AND_DISABLE_WARNING and _OPENMAYA_POP_WARNING macros.</li>
</ul>
</div>
