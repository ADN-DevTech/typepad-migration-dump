---
layout: "post"
title: "Maya 2023 API Update guide"
date: "2022-03-30 17:12:18"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2022/03/maya-2023-api-update-guide.html "
typepad_basename: "maya-2023-api-update-guide"
typepad_status: "Publish"
---

<p><a id="top"></a>The guide is based on What’s New in the Maya Devkit in Maya 2023 with some extra info. For more details including commands and scripts changes, please checkout the <a href="http://help.autodesk.com/view/MAYAUL/2023/ENU/">documentation</a> for more details.</p>
<p>Our Maya documentation team is also interested to know how impactful Developer documentation improvements have been over the past few releases. If you have a moment, <a href="https://autodeskfeedback.az1.qualtrics.com/jfe/form/SV_ai4bX5owgz9nihU">please take this short survey</a> to help us improve and know what is most important for you.</p>
<h2 id="building-environments"><a name="building-environments" href="#building-environments"></a>Building environments</h2>
<p>The building environment remains unchanged.</p>
<table>
<thead>
<tr>
<th style="text-align:center">Platform</th>
<th style="text-align:center">OS</th>
<th style="text-align:center">Compiler</th>
</tr>
</thead>
<tbody>
<tr>
<td style="text-align:center">Windows</td>
<td style="text-align:center">Windows 10 x64</td>
<td style="text-align:center">Visual Studio 2019.7.8 <br/> Windows SDK Version 10.0.10586.0</td>
</tr>
<tr>
<td style="text-align:center">Mac</td>
<td style="text-align:center">Mojave 10.14.x</td>
<td style="text-align:center">XCode 10.2.1</td>
</tr>
<tr>
<td style="text-align:center">Linux</td>
<td style="text-align:center">CentOS/RHEL 7.x(x&gt;=6)</td>
<td style="text-align:center">gcc 9.3.1 (DTS 9.1)</td>
</tr>
</tbody>
</table>
<p><a href="#top">Back to top</a></p>
<h2 id="devkit-changes"><a name="devkit-changes" href="#devkit-changes"></a>Devkit changes</h2>
<ul>
<li>Alembic libraries have moved, using the find_alembic macro in your CMakeLists.txt files.</li><li>Devkit examples now require cmake version <strong>3.13</strong> or later to compile.</li><li>The <strong>py1ArrayAttrBlenderNode.py</strong> example has been added to demonstrate how to implement array attributes in a straightforward way.</li><li>The <strong>py1MoveTool.py</strong> example has been updated from using VP1 to using VP2, and updated to use Python API 2.0. It has been renamed to <strong>py2MoveTool.py</strong>.</li><li>The <strong>customImagePlane</strong> example has been updated to fix an issue where images were not animated when scrubbing.</li><li>The <strong>gpuCache</strong> example has been updated to use the new <strong>kSelectionHighlighting</strong> enum.</li><li>The <strong>rockingTransform</strong> example has been updated to use the new <strong>preRotation()</strong> method in <strong>MPxTransformationMatrix</strong>.</li><li>The <strong>offsetNode</strong> example has been updated to use the new <a href="#enhanced-gpu-deformer-apis">GPU deformer API</a>.</li><li>The <strong>apiMeshShape</strong> example has been updated to remove deprecated code and to fix a bug in the example.</li></ul>
<p><a href="#top">Back to top</a></p>
<h2 id="python-changes"><a name="python-changes" href="#python-changes"></a>Python Changes</h2>
<h3 id="python-updated-to-version-3.9.7"><a name="python-updated-to-version-3.9.7" href="#python-updated-to-version-3.9.7"></a>Python updated to version 3.9.7</h3>
<p>Python has been updated to 3.9 from 3.7. Code compatibility is not guaranteed between these two version and you may encounter issues when porting your scripts from Python 3.7 to Python 3.9. There are several deprecated methods removed in Python 3.9. Consult <a href="https://docs.python.org/3/whatsnew/3.8.html">What’s New in Python 3.8</a> and <a href="https://docs.python.org/3/whatsnew/3.9.html">What’s New in Python 3.9</a> for details.</p>
<p>The ABI is not compatible between these 2 versions neither. Depending on which method is used (pybind11, swig, boost.python or CPython), it is necessary to review and recompile Python extensions.</p>
<p>PySide is now also built with Python 3.9.<br><a href="#top">Back to top</a></p>
<h3 id="new-python-site-packages-directory"><a name="new-python-site-packages-directory" href="#new-python-site-packages-directory"></a>New Python site-packages directory</h3>
<p>Maya will use a site-packages directory specific to its release version.</p>
<table>
<thead>
<tr>
<th>Operating system</th>
<th>Path to new site-packages location</th>
</tr>
</thead>
<tbody>
<tr>
<td>macOS</td>
<td>Preferences/Autodesk/maya/2023/scripts/site-packages</td>
</tr>
<tr>
<td>linux</td>
<td>/local/users/<username>/maya/2023/scripts/site-packages</td>
</tr>
<tr>
<td>Windows</td>
<td>C:/Users/<username>/Documents/maya/2023/scripts/site-packages</td>
</tr>
</tbody>
</table>
<p>This is an added directory. Packages will not be moved to this directory, and this directory will not replace any existing directories. However, packages in this directory will be prioritized over Maya’s own site-packages directory.</p>
<p>Use the <strong>pip —target</strong> option to install packages to this location:</p>
<pre><code>mayapy -m pip install &lt;package_name&gt; --target &lt;full_path_to_maya_2023_site-packages&gt;
</code></pre><p><a href="#top">Back to top</a></p>
<h3 id="generating-python-code-from-ui-files-using-pyside2-uic"><a name="generating-python-code-from-ui-files-using-pyside2-uic" href="#generating-python-code-from-ui-files-using-pyside2-uic"></a>Generating Python code from ui files using pyside2-uic</h3>
<p><strong>pyside2-uic</strong> and <strong>pyside2-rcc</strong> are now included with Maya.</p>
<p>Users can now use <strong>pyside2-uic</strong> to generate Python code from <strong>.ui</strong> files.</p>
<h3 id="maya.api.openmaya.mfnmesh()-handles-empty-meshes-same-as-python-api-1.0-now."><a name="maya.api.openmaya.mfnmesh()-handles-empty-meshes-same-as-python-api-1.0-now." href="#maya.api.openmaya.mfnmesh()-handles-empty-meshes-same-as-python-api-1.0-now."></a>maya.api.OpenMaya.MFnMesh() handles empty meshes same as Python API 1.0 now.</h3>
<h3 id="methods-added-to-the-python-api-2.0"><a name="methods-added-to-the-python-api-2.0" href="#methods-added-to-the-python-api-2.0"></a>Methods added to the Python API 2.0</h3>
<p>The Python counterparts of the following C++ methods have been added to the Python API 2.0:</p>
<pre><code>virtual void MPxNode::getCacheSetup(const MEvaluationNode&amp;, MNodeCacheDisablingInfo&amp;, MNodeCacheSetupInfo&amp;, MObjectArray&amp;) const

virtual void MPxNode::configCache(const MEvaluationNode&amp;, MCacheSchema&amp;) const

virtual MTimeRange MPxNode::transformInvalidationRange(const MPlug&amp; source, const MTimeRange&amp; input) const

bool MPxNode::hasInvalidationRangeTransformation() const

virtual void MPxGeometryOverride::configCache(const MEvaluationNode&amp;, MCacheSchema&amp;) const
</code></pre><p><a href="#top">Back to top</a></p>
<h2 id="api-changes"><a name="api-changes" href="#api-changes"></a>API Changes</h2>
<h4 id="enhanced-gpu-deformer-apis"><a name="enhanced-gpu-deformer-apis" href="#enhanced-gpu-deformer-apis"></a>Enhanced GPU deformer APIs</h4>
<p>The <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_open_c_l_utils.html">MOpenCLUtils</a>, <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_open_c_l_kernel_info.html">MOpenCLKernelInfo</a>, and <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_px_g_p_u_standard_deformer.html">MPxGPUStandardDeformer</a> classes have been added.</p>
<p>One new method has been added to <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_g_p_u_deformer_registration_info.html">MGPUDeformerRegistrationInfo</a>:</p>
<pre><code>virtual bool isGeometryFilter() const
</code></pre><p>And four new methods have been added to <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_px_g_p_u_deformer.html">MPxGPUDeformer</a>:</p>
<pre><code>static bool isBufferUpdateNeeded(const MOpenCLBuffer&amp; buffer, const MEvaluationNode&amp; evaluationNode, const MObject&amp; attribute)

cl_int uploadFixedSetupData(const MString&amp; name, MOpenCLBuffer&amp; buffer, cl_int&amp; errorCode, unsigned int* arrayLength)

static MFnGeometryData::SubsetState getSubsetState(MDataBlock&amp; block, unsigned int multiIndex, MStatus* ReturnStatus = NULL)

static const char* className()
</code></pre><p>The <strong>offsetNode</strong> example has been updated to use these new classes and methods.</p>
<h4 id="two-new-methods,-[mcolorpickerutilities::applyviewtransform()](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_picker_utilities.html#a1de9fd7aa122ca60308005e760342ff0),-and-[mcolorpickerutilities::grabcolor()](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_picker_utilities.html#ae78601c9aff49c253c60975440875564)-and-a-new-class-[mcolormixingspacehelper](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_mixing_space_helper.html),-have-been-added-for-creating-a-color-managed-color-picker-using-the-api."><a name="two-new-methods,-[mcolorpickerutilities::applyviewtransform()](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_picker_utilities.html#a1de9fd7aa122ca60308005e760342ff0),-and-[mcolorpickerutilities::grabcolor()](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_picker_utilities.html#ae78601c9aff49c253c60975440875564)-and-a-new-class-[mcolormixingspacehelper](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_mixing_space_helper.html),-have-been-added-for-creating-a-color-managed-color-picker-using-the-api." href="#two-new-methods,-[mcolorpickerutilities::applyviewtransform()](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_picker_utilities.html#a1de9fd7aa122ca60308005e760342ff0),-and-[mcolorpickerutilities::grabcolor()](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_picker_utilities.html#ae78601c9aff49c253c60975440875564)-and-a-new-class-[mcolormixingspacehelper](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_color_mixing_space_helper.html),-have-been-added-for-creating-a-color-managed-color-picker-using-the-api."></a>Two new methods,  <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_color_picker_utilities.html#a1de9fd7aa122ca60308005e760342ff0">MColorPickerUtilities::applyViewTransform()</a>, and <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_color_picker_utilities.html#ae78601c9aff49c253c60975440875564">MColorPickerUtilities::grabColor()</a> and a new class <a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_color_mixing_space_helper.html">MColorMixingSpaceHelper</a>, have been added for creating a color managed color picker using the API.</h4>
<pre><code>MColorPickerUtilities::applyViewTransform (const MColor &amp;inputColor, Direction direction)

MColor MColorPickerUtilities::grabColor(MStatus * returnedStatus = nullptr)
</code></pre><h4 id="improvement-to-the-preference-system"><a name="improvement-to-the-preference-system" href="#improvement-to-the-preference-system"></a>Improvement to the preference system</h4>
<p>Three new methods have been added to <strong>MGlobal</strong>:</p>
<pre><code>static bool initOptionVar(const MString&amp; name, int value, const MString&amp; category)
static bool initOptionVar(const MString&amp; name, double value, const MString&amp; category)
static bool initOptionVar(const MString&amp; name, MString value, const MString&amp; category)
</code></pre><h4 id="[mtimeslidercustomdrawmanager](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_time_slider_custom_draw_manager.html)-class-has-been-added-to-the-maya-api.-this-class-provides-a-way-to-draw-custom-items-on-the-timeline."><a name="[mtimeslidercustomdrawmanager](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_time_slider_custom_draw_manager.html)-class-has-been-added-to-the-maya-api.-this-class-provides-a-way-to-draw-custom-items-on-the-timeline." href="#[mtimeslidercustomdrawmanager](http://help.autodesk.com/cloudhelp/2023/enu/maya-sdk/cpp_ref/class_m_time_slider_custom_draw_manager.html)-class-has-been-added-to-the-maya-api.-this-class-provides-a-way-to-draw-custom-items-on-the-timeline."></a><a href="http://help.autodesk.com/cloudhelp/2023/ENU/Maya-SDK/cpp_ref/class_m_time_slider_custom_draw_manager.html">MTimeSliderCustomDrawManager</a> class has been added to the Maya API. This class provides a way to draw custom items on the timeline.</h4>
<p>Three callback methods were added:</p>
<pre><code>void setStopPrimitiveEditFunction(MCustomDrawID id, MSharedPtr&lt; MStopPrimitiveEditingFct &gt; fct)
void setStartPrimitiveEditFunction(MCustomDrawID id, MSharedPtr&lt; MStartPrimitiveEditingFct &gt; fct)
void setEditPrimitiveFunction(MCustomDrawID id, MSharedPtr&lt; MEditPrimitiveFct &gt; fct)
</code></pre><p>The three abstract classes, <strong>MStopPrimitiveEditingFct</strong>, <strong>MStartPrimitiveEditingFct</strong>, and <strong>MEditPrimitiveFct</strong>, were added to support these callbacks.</p>
<h4 id="three-new-selection-levels-have-been-added-to-the-mintersection::selectionlevel()-and-mselectioncontext::selectionlevel-in-openmayarender"><a name="three-new-selection-levels-have-been-added-to-the-mintersection::selectionlevel()-and-mselectioncontext::selectionlevel-in-openmayarender" href="#three-new-selection-levels-have-been-added-to-the-mintersection::selectionlevel()-and-mselectioncontext::selectionlevel-in-openmayarender"></a>Three new selection levels have been added to the MIntersection::SelectionLevel() and MSelectionContext::SelectionLevel in OpenMayaRender</h4>
<p><strong>kFace</strong>, <strong>kEdge</strong>, <strong>kVertex</strong> were added. Plug-ins that test the selection level of a selection hit against the <strong>kComponent</strong> level should be updated to test against the three new levels instead.</p>
<h4 id="reset()-methods-in-mitmeshpolygon,-mitmeshedge,-and-mitmeshvertex-have-been-modified-to-remove-invalid-component-ids-automatically"><a name="reset()-methods-in-mitmeshpolygon,-mitmeshedge,-and-mitmeshvertex-have-been-modified-to-remove-invalid-component-ids-automatically" href="#reset()-methods-in-mitmeshpolygon,-mitmeshedge,-and-mitmeshvertex-have-been-modified-to-remove-invalid-component-ids-automatically"></a>reset() methods in MItMeshPolygon, MItMeshEdge, and MItMeshVertex have been modified to remove invalid component IDs automatically</h4>
<h4 id="**getprerotation()**-and-**prerotation()**-have-been-added-to-mpxtransform-and-mpxtransformationmatrix"><a name="**getprerotation()**-and-**prerotation()**-have-been-added-to-mpxtransform-and-mpxtransformationmatrix" href="#**getprerotation()**-and-**prerotation()**-have-been-added-to-mpxtransform-and-mpxtransformationmatrix"></a><strong>getPreRotation()</strong> and <strong>preRotation()</strong> have been added to MPxTransform and MPxTransformationMatrix</h4>
<p>The <strong>rockingTransform</strong> example has been modified to use this new functionality.</p>
<h4 id="**isproxy**-and-**proxied**-have-been-added-to-mplug"><a name="**isproxy**-and-**proxied**-have-been-added-to-mplug" href="#**isproxy**-and-**proxied**-have-been-added-to-mplug"></a><strong>isProxy</strong> and <strong>proxied</strong> have been added to MPlug</h4>
<h4 id="snaptoactive()-added-to-mselectioninfo"><a name="snaptoactive()-added-to-mselectioninfo" href="#snaptoactive()-added-to-mselectioninfo"></a>snapToActive() added to MSelectionInfo</h4>
<h4 id="kselectionhighlighting-added-to-mgeometry::drawmode"><a name="kselectionhighlighting-added-to-mgeometry::drawmode" href="#kselectionhighlighting-added-to-mgeometry::drawmode"></a>kSelectionHighlighting added to MGeometry::DrawMode</h4>
<p>The <strong>gpuCache</strong> devkit example has been updated to use this new enum.</p>
<h4 id="a-drawpass-enum,-**begindrawpass()**-and-**enddrawpass()**-have-been-added-to-muidrawmanager"><a name="a-drawpass-enum,-**begindrawpass()**-and-**enddrawpass()**-have-been-added-to-muidrawmanager" href="#a-drawpass-enum,-**begindrawpass()**-and-**enddrawpass()**-have-been-added-to-muidrawmanager"></a>A DrawPass enum, <strong>beginDrawPass()</strong> and <strong>endDrawPass()</strong> have been added to MUIDrawManager</h4>
<h4 id="two-new-parameters,-**inputcolorspace**-and-**alphadiscardthreshold**-have-been-added-to-muidrawmanager::settexture()"><a name="two-new-parameters,-**inputcolorspace**-and-**alphadiscardthreshold**-have-been-added-to-muidrawmanager::settexture()" href="#two-new-parameters,-**inputcolorspace**-and-**alphadiscardthreshold**-have-been-added-to-muidrawmanager::settexture()"></a>Two new parameters, <strong>inputColorSpace</strong> and <strong>alphaDiscardThreshold</strong> have been added to MUIDrawManager::setTexture()</h4>
<p><strong>inputColorSpace</strong> is the color space of the texture that will be used to create a shader override when converting texture color to render space color.</p>
<p><strong>alphaDiscardThreshold</strong> controls the shader that draws textured UI objects. Any parts of the texture that have an alpha value lower than the threshold will not be drawn. A threshold of 0.0 means everything will be drawn while 1.0 means nothing will be. </p>
<h4 id="**defaultmaterialfiltering**,-**setdefaultmaterialhandling()**,-and-**getdefaultmaterialhandling()**-were-added-so-that-a-**mrenderitem**-can-be-filtered-based-on-the-default-material-setting-of-the-viewport."><a name="**defaultmaterialfiltering**,-**setdefaultmaterialhandling()**,-and-**getdefaultmaterialhandling()**-were-added-so-that-a-**mrenderitem**-can-be-filtered-based-on-the-default-material-setting-of-the-viewport." href="#**defaultmaterialfiltering**,-**setdefaultmaterialhandling()**,-and-**getdefaultmaterialhandling()**-were-added-so-that-a-**mrenderitem**-can-be-filtered-based-on-the-default-material-setting-of-the-viewport."></a><strong>DefaultMaterialFiltering</strong>, <strong>setDefaultMaterialHandling()</strong>, and <strong>getDefaultMaterialHandling()</strong> were added so that a <strong>MRenderItem</strong> can be filtered based on the default material setting of the viewport.</h4>
<p>A new enum and three new methods have been added to <strong>MHWRender::MRenderItem</strong>.</p>
<h4 id="**muint64-mrenderitem::internalobjectid()-const**-returns-a-unique-identifier-for-a-render-item."><a name="**muint64-mrenderitem::internalobjectid()-const**-returns-a-unique-identifier-for-a-render-item." href="#**muint64-mrenderitem::internalobjectid()-const**-returns-a-unique-identifier-for-a-render-item."></a><strong>MUint64 MRenderItem::InternalObjectId() const</strong> returns a unique identifier for a render item.</h4>
<h4 id="the-default-tolerance-of-**mfnmesh::getpointsatuv()**-was-changed-from-0-to-0.001.-if-no-points-are-found,-the-method-will-no-longer-fail.-instead-its-arrays-of-points-polygon-ids-will-be-empty."><a name="the-default-tolerance-of-**mfnmesh::getpointsatuv()**-was-changed-from-0-to-0.001.-if-no-points-are-found,-the-method-will-no-longer-fail.-instead-its-arrays-of-points-polygon-ids-will-be-empty." href="#the-default-tolerance-of-**mfnmesh::getpointsatuv()**-was-changed-from-0-to-0.001.-if-no-points-are-found,-the-method-will-no-longer-fail.-instead-its-arrays-of-points-polygon-ids-will-be-empty."></a>The default tolerance of <strong>MFnMesh::getPointsAtUV()</strong> was changed from 0 to 0.001. If no points are found, the method will no longer fail. Instead its arrays of points polygon IDs will be empty.</h4>
<h4 id="the-methods-**edgeborderinfo()**,-**getuvborderedges()**,-and-**getmeshshellids()**,-**getrawuvs()**,-as-well-as-the-enum,-**borderinfo**,-have-been-added-to-**mfnmesh**."><a name="the-methods-**edgeborderinfo()**,-**getuvborderedges()**,-and-**getmeshshellids()**,-**getrawuvs()**,-as-well-as-the-enum,-**borderinfo**,-have-been-added-to-**mfnmesh**." href="#the-methods-**edgeborderinfo()**,-**getuvborderedges()**,-and-**getmeshshellids()**,-**getrawuvs()**,-as-well-as-the-enum,-**borderinfo**,-have-been-added-to-**mfnmesh**."></a>The methods <strong>edgeBorderInfo()</strong>, <strong>getUVBorderEdges()</strong>, and <strong>getMeshShellIds()</strong>, <strong>getRawUVs()</strong>, as well as the enum, <strong>BorderInfo</strong>, have been added to <strong>MFnMesh</strong>.</h4>
<h4 id="the-boolclassification-enum-indicates-whether-to-use-edge-classification-or-normal-classification-when-computing-booleans-between-meshes-using-the-booleanops()-method."><a name="the-boolclassification-enum-indicates-whether-to-use-edge-classification-or-normal-classification-when-computing-booleans-between-meshes-using-the-booleanops()-method." href="#the-boolclassification-enum-indicates-whether-to-use-edge-classification-or-normal-classification-when-computing-booleans-between-meshes-using-the-booleanops()-method."></a>The BoolClassification enum indicates whether to use edge classification or normal classification when computing booleans between meshes using the booleanOps() method.</h4>
<h4 id="four-new-overloaded-**create()**-and-two-new-overloaded-**createinplace()**-methods-have-been-added-to-**mfnmesh**.-the-new-methods-mirror-the-mesh-creation-used-by-mayaascii-and-mayabinary-file-readers."><a name="four-new-overloaded-**create()**-and-two-new-overloaded-**createinplace()**-methods-have-been-added-to-**mfnmesh**.-the-new-methods-mirror-the-mesh-creation-used-by-mayaascii-and-mayabinary-file-readers." href="#four-new-overloaded-**create()**-and-two-new-overloaded-**createinplace()**-methods-have-been-added-to-**mfnmesh**.-the-new-methods-mirror-the-mesh-creation-used-by-mayaascii-and-mayabinary-file-readers."></a>Four new overloaded <strong>create()</strong> and two new overloaded <strong>createInPlace()</strong> methods have been added to <strong>MFnMesh</strong>. The new methods mirror the mesh creation used by mayaAscii and mayaBinary file readers.</h4>
<h4 id="getmemberpaths()-has-been-added-to-mfnsets-to-get-an-array-of-dagpaths-that-are-members-of-the-set."><a name="getmemberpaths()-has-been-added-to-mfnsets-to-get-an-array-of-dagpaths-that-are-members-of-the-set." href="#getmemberpaths()-has-been-added-to-mfnsets-to-get-an-array-of-dagpaths-that-are-members-of-the-set."></a>getMemberPaths() has been added to MFnSets to get an array of dagPaths that are members of the set.</h4>
<h4 id="isincrashhandler()-has-been-added-to-mglobal-to-query-where-or-not-maya-has-crashed."><a name="isincrashhandler()-has-been-added-to-mglobal-to-query-where-or-not-maya-has-crashed." href="#isincrashhandler()-has-been-added-to-mglobal-to-query-where-or-not-maya-has-crashed."></a>isInCrashHandler() has been added to MGlobal to query where or not Maya has crashed.</h4>
<h4 id="isvalidreference()-added-to-mfnreference-to-validate-a-file-reference-before-passing-it-to-other-methods,-such-as-**mfnreferemce::isloaded()**,-to-avoid-triggering-an-exception."><a name="isvalidreference()-added-to-mfnreference-to-validate-a-file-reference-before-passing-it-to-other-methods,-such-as-**mfnreferemce::isloaded()**,-to-avoid-triggering-an-exception." href="#isvalidreference()-added-to-mfnreference-to-validate-a-file-reference-before-passing-it-to-other-methods,-such-as-**mfnreferemce::isloaded()**,-to-avoid-triggering-an-exception."></a>isValidReference() added to MFnReference to validate a file reference before passing it to other methods, such as <strong>MFnReferemce::isLoaded()</strong>, to avoid triggering an exception.</h4>
<h4 id="uniquename()-added-to-mfndependencynode"><a name="uniquename()-added-to-mfndependencynode" href="#uniquename()-added-to-mfndependencynode"></a>uniqueName() added to MFnDependencyNode</h4>
<h4 id="devicepixelratio()-has-been-added-to-m3dview-to-return-ratio-of-qt-logical-pixels-to-viewport-rendering-pixels-for-the-device."><a name="devicepixelratio()-has-been-added-to-m3dview-to-return-ratio-of-qt-logical-pixels-to-viewport-rendering-pixels-for-the-device." href="#devicepixelratio()-has-been-added-to-m3dview-to-return-ratio-of-qt-logical-pixels-to-viewport-rendering-pixels-for-the-device."></a>devicePixelRatio() has been added to M3dView to return ratio of Qt logical pixels to viewport rendering pixels for the device.</h4>
<h4 id="isempty()-added-to-mstring"><a name="isempty()-added-to-mstring" href="#isempty()-added-to-mstring"></a>isEmpty() added to MString</h4>
<h4 id="mstatus-mfileio::getreferencenodes(const-mstring-&-filename,-mstringarray-&-nodes,-bool-dagpath)-has-been-added-to-mfileio"><a name="mstatus-mfileio::getreferencenodes(const-mstring-&-filename,-mstringarray-&-nodes,-bool-dagpath)-has-been-added-to-mfileio" href="#mstatus-mfileio::getreferencenodes(const-mstring-&-filename,-mstringarray-&-nodes,-bool-dagpath)-has-been-added-to-mfileio"></a>MStatus MFileIO::getReferenceNodes(const MString &amp; fileName, MStringArray &amp; nodes, bool dagPath) has been added to MFileIO</h4>
<h4 id="the-return-type-of-**mindexmapper::affectmap()**-has-changed-from-**mintarray**-to-**muintarray**"><a name="the-return-type-of-**mindexmapper::affectmap()**-has-changed-from-**mintarray**-to-**muintarray**" href="#the-return-type-of-**mindexmapper::affectmap()**-has-changed-from-**mintarray**-to-**muintarray**"></a>The return type of <strong>MIndexMapper::affectMap()</strong> has changed from <strong>MIntArray</strong> to <strong>MUintArray</strong></h4>
<p><a href="#top">Back to top</a></p>
