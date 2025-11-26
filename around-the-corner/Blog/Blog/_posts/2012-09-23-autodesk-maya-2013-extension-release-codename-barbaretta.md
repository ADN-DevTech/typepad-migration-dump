---
layout: "post"
title: "Autodesk Maya 2013 Extension Release (codename “Barbaretta”)"
date: "2012-09-23 23:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/autodesk-maya-2013-extension-release-codename-barbaretta.html "
typepad_basename: "autodesk-maya-2013-extension-release-codename-barbaretta"
typepad_status: "Publish"
---

<p>With the <a href="http://area.autodesk.com/assets/img/siggraph2012/2012-08-06_2013_dec_extension_releases.pdf" target="_blank">announcement</a>&#0160;made at
SIGGRAPH 2012, there is something important to note about the&#0160;<a href="http://usa.autodesk.com/adsk/servlet/pc/item?siteID=123112&amp;id=15500357" target="_blank">Extension release</a>!
Unlike past releases, this one <strong>will not be binary compatible</strong> with its
major release. In the past, a hotfix, Service Pack, and Extension release were
binary compatible with their respective major release; <strong>that&#39;s not the case this time!</strong></p>
<p><strong>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3bfcc0f0970c-pi" style="display: inline;"><img alt="Breaking-chain" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3bfcc0f0970c" src="/assets/image_e8503a.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Breaking-chain" /></a><br /></strong><span style="text-decoration: underline;">Here is an example to illustrate
what this means:</span></p>
<p>The Maya 2012 Extension release is
binary compatible with the Maya 2012 major release, which means C++ plug-ins
compiled using the Maya 2012 devkit can be loaded in the Maya 2012 Extension
release. Developers do not need to recompile their plug-ins to work in the Extension
release.</p>
<p>Unfortunately, the internal
changes required for the Maya 2013 Extension release made that compatibility
impossible to maintain. This means, Maya&#0160;C++ plug-in developers <strong>will need to
recompile their plug-ins</strong> to work in Maya 2013 Extension release using the
new devkit from the Extension release.</p>
<p>Python<sup>®</sup>
plug-ins aren&#39;t affected by the changes, but developers
may have to do code changes depending on the API they are using.</p>
<p><span style="text-decoration: underline;">New API and API changes:</span></p>
NOTE: This list is not exhaustive. It only covers those changes and additions which are likely to be of most interest to API users.
<h2>New API for reference edits</h2>
<ul>
<li>MItEdits&#0160;allows you to interate through a scene&#39;s reference edits.</li>
MEdit,&#0160;MAddRemoveAttrEdit,&#0160;MConnectDisconnectAttrEdit,&#0160;MFcurveEdit,&#0160;MParentingEdit&#0160;and&#0160;MSetAttrEdit&#0160;provide methods for querying information about specific types of edits.
<li>MFnReference&#0160;has been added to allow you to query individual file references.</li>
<li>MFnDependencyNode::isTrackingEdits()&#0160;can be used to determine if a reference node or assembly is tracking edits.</li>
<li>New edit-related message types have been added to&#0160;MSceneMessage: kBeforeLoadReferenceAndRecordEdits, kAfterLoadReferenceAndRecordEdits, kBeforeCreateReferenceAndRecordEdits, kAfterCreateReferenceAndRecordEdits.</li>
</ul>
<h2>New API for scene assemblies</h2>
<ul>
<li>MFnAssembly&#0160;can be used to query and modify assembly nodes, which are used to manage the various representations of a scene asset.</li>
<li>MPxAssembly&#0160;can be used to create custom assembly types</li>
<li>MPxRepresentation&#0160;can be used to create custom representations.</li>
</ul>
<h2>New API for generic metadata (blind data)</h2>
<ul>
<li>A number of classes have been added to create and operate on streams of generic data. Look in the API Reference for classes in the &quot;adsk::Data&quot; namespace (e.g.&#0160;adsk::Data::Stream, adsk::Data::Channel).</li>
<li>setMetadata()&#0160;and&#0160;deleteMetadata()&#0160;methods have been added to&#0160;MDGModifier.</li>
<li>setMetadata(),&#0160;deleteMetadata(),&#0160;metadata()&#0160;and&#0160;validateMetadata()&#0160;methods have been added to&#0160;MFnDependencyNode.</li>
<li>The&#0160;writesMetadata()&#0160;virtual method has been added to&#0160;MPxMayaAsciiFilter.</li>
</ul>
<h2>New API for URIs (Uniform Resource Identifiers)</h2>
<ul>
<li>The&#0160;MURI&#0160;class can be used to create URIs as well as query and modify the various components of a URI.</li>
<li>MPxFileResolver&#0160;can be used to create custom handlers to resolve specific URI schemes.</li>
</ul>
<h2>Viewport Rendering API Changes</h2>
<ul>
<li>DirectX11 support: kDirectX11 has been added to the DrawAPI enum.&#0160;MRenderer::drawAPI()&#0160;will return kDirectX11 if the current renderer supports DirectX11. If the drawing API is kDirectX11 then&#0160;MRenderer::GPUDeviceHandle()&#0160;will return a point to the DirectX device. Custom overrides derived from&#0160;MPxDrawOverride,&#0160;MPxGeometryOverride,&#0160;MPxShaderOverride&#0160;andMRenderOverride&#0160;can indicate that they support DirectX11 by including kDirectX11 in the bitmap returned by their&#0160;supportedDrawAPIs()&#0160;methods.</li>
<li>MRenderItem&#0160;consolidation: When multiple objects are compatible, their geometry can be consolidated into a single&#0160;MRenderItem, to provide better performance by concatening their index and vertex buffers.&#0160;isConsolidated()&#0160;returns true if the&#0160;MRenderItem&#0160;contains multiple objects and&#0160;sourceIndexMapping()&#0160;will return an&#0160;MGeometryIndexMapping&#0160;instance which describes how they have been consolidated into the buffers.</li>
<li>Custom display filters:&#0160;MFnPlugin::registerDisplayFilter()&#0160;has been added to allow the creation of new object filters which will show up on the viewport&#39;s &#39;Show&#39; menu.&#0160;setPluginDisplay()&#0160;andpluginObjectDisplay()&#0160;have been added to&#0160;M3dView&#0160;to control and query the state of these filters.&#0160;MDrawInfo::pluginObjectDisplayStatus()&#0160;has been added to provide that information to plugin shapes.</li>
<li>GPU-based gamma correction (aka &quot;sRGB write&quot;):&#0160;MRenderTargetManager::formatSupportsSRGBWrite()&#0160;has been added to determine if a given raster format supports GPU-based gamma correction and&#0160;MRenderOperation::enableSRGBWrite()&#0160;has been added to enable it for a given operation.</li>
<li>Explicit binding of shader effects.&#0160;MShaderInstance&#0160;now allows explicit control over the binding of shader effects (e.g. cgfx or fx files) via its new&#0160;bind()&#0160;and&#0160;unbind()&#0160;methods.&#0160;getPassCount()&#0160;andactivatePass()&#0160;can be used to determine which passes are enabled in the effect and&#0160;updateParameters()&#0160;can be used to update parameters with the overhead of rebinding.</li>
<li>MPxPrimitiveGenerator&#0160;has been added to allow the creation of new primitive types which can be used by&#0160;MPxShaderOverride&#0160;or by a custom renderer.</li>
<li>MPxVertexBufferMutator&#0160;has been added to allow the manipulation of custom vertex streams.</li>
<li>MPxIndexBufferMutator&#0160;has been added to allow the manipulation of custom index buffers.</li>
<li>MRenderUtilities&#0160;has been added to provide various utility functions for viewport rendering, such as setting the swatch background color, retrieving the draw contexts for swatches and the texture editor, and blitting a target to OpenGL or an&#0160;MImage.</li>
<li>setAllowsUnorderedAccess()&#0160;and&#0160;allowsUnorderedAccess()&#0160;have been added to&#0160;MRenderTargetDescription&#0160;to set and query whether a target supports simultaneous read/write access to its data by multiple threads.</li>
<li>MDrawContext::getPassContext()&#0160;has been added to return information about the current render pass. It returns an instance of the new&#0160;MPassContext&#0160;class.</li>
<li>lightType()&#0160;and&#0160;lightPath()&#0160;methods haved been added to&#0160;MLightParameterInformation, and several other methods have been added to work with StockParameterSemantics.</li>
<li>MGeometryUtilities::acquireReferenceGeometry()&#0160;has been added to return geometry for default reference primitives such as sphere, cube and plane. The caller is responsible for usingMGeometryUtilities::releaseReferenceGeometry()&#0160;to release the geometry when done with it.</li>
<li>MVertexBufferArray&#0160;has been added to allow arrays of&#0160;MVertexBuffers&#0160;to be passed between API methods.</li>
<li>EXT_frame_buffer_object support has been added to&#0160;MGLdefinitions</li>
<li>map()&#0160;and&#0160;unmap()&#0160;have been added to&#0160;MIndexBuffer&#0160;and&#0160;MVertexBuffer&#0160;to provide read-only access to their buffers&#39; contents.</li>
<li>resourceHandle()&#0160;methods have been added to&#0160;MIndexBuffer&#0160;and&#0160;MVertexBuffer&#0160;to set and retrieve graphics device dependent handles to hardware buffers.&#0160;hasCustomResourceHandle()&#0160;can be used to determine if a custom resource handle has been set.</li>
<li>castsShadows()&#0160;methods have been added to&#0160;MRenderItem&#0160;to query and modify whether the item casts shadows.&#0160;receivesShadows()&#0160;methods have been added to do the same for receiving shadows.</li>
<li>getEffectsBufferShader()&#0160;has been added to&#0160;MShaderManager. It performs the same function as&#0160;getEffectsFileShader()&#0160;except that the source code for the effect is contained in a memory buffer rather than a file.</li>
<li>rawData()&#0160;has been added to&#0160;MRenderTarget&#0160;and&#0160;MTexture&#0160;to return a copy of their currently mapped data.</li>
<li>Instances of the following classes can no longer be deleted directly but must instead be released using the appropriate method from the corresponding manager class:
<table border="1" cellpadding="4" cellspacing="0">
<tbody>
<tr>
<th>Instance Type</th><th>Release Method</th>
</tr>
<tr>
<td>MBlendState</td>
<td>MStateManager::releaseBlendState().</td>
</tr>
<tr>
<td>MDepthStencilState</td>
<td>MStateManager::releaseDepthStencilState().</td>
</tr>
<tr>
<td>MRasterizerState</td>
<td>MStateManager::releaseRasterizerState().</td>
</tr>
<tr>
<td>MRenderTarget</td>
<td>MRenderTargetManager::releaseRenderTarget().</td>
</tr>
<tr>
<td>MSamplerState</td>
<td>MStateManager::releaseSamplerState().</td>
</tr>
<tr>
<td>MShaderInstance</td>
<td>MShaderManager::releaseShader().</td>
</tr>
<tr>
<td>MTexture</td>
<td>MTextureManager::releaseTexture().</td>
</tr>
</tbody>
</table>
</li>
</ul>
<h2>Miscellaneous</h2>
<ul>
<li>MFnFloatArrayData&#0160;has been added to complement the set of array data types which can be passed through a connection.</li>
<li>MLibrary::cleanup()&#0160;now takes an optional &#39;exitWhenDone&#39; parameter. Setting this to false allows control to return to the caller rather than forcing the application to exit.</li>
</ul>
<p>&#0160;</p>
