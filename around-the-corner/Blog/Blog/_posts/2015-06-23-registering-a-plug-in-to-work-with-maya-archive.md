---
layout: "post"
title: "Registering a plug-in to work with Maya archive"
date: "2015-06-23 18:04:48"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
  - "References"
original_url: "https://around-the-corner.typepad.com/adn/2015/06/registering-a-plug-in-to-work-with-maya-archive.html "
typepad_basename: "registering-a-plug-in-to-work-with-maya-archive"
typepad_status: "Publish"
---

<p>To add external dependencies to the archive created by Maya with "File" --&gt; "Archive Scene", a plug-in developer needs to implement the MPxNode::getFilesToArchive() method.</p>

<p>The documentation is <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_class_m_px_node_html">here</a>. This will add the plug-in node's external dependencies to the archive. While you are at it, you can add any file paths to Maya’s File Path Editor UI using this command:</p>

<pre class="brush: shell; toolbar: false;">filePathEditor –registerType "myNode1.filePath" –typeLabel "Custom"
</pre>

<p>If you wish to add the external dependencies to metadata in the Maya file itself, use the getExternalContent() and setExternalContent() entry points. Unfortunately these did not get properly documented and are limited in scope (they do not support file sequences). There are shipped usage examples in our scene assembly nodes (look for getExternalContent and setExternalContent in these):</p>

<ul>
<li><a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_scene_assembly_2assembly_reference_8h_example_html">sceneAssembly/assemblyReference.h</a></li>
<li><a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_scene_assembly_2adsk_representations_8h_example_html">sceneAssembly/adskRepresentations.h</a></li>
<li><a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_scene_assembly_2adsk_representations_8cpp_example_html">sceneAssembly/adskRepresentations.cpp</a></li>
<li><a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_scene_assembly_2assembly_definition_8h_example_html">sceneAssembly/assemblyDefinition.h</a></li>
<li><a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_scene_assembly_2assembly_definition_8cpp_example_html">sceneAssembly/assemblyDefinition.cpp</a></li>
</ul>

<p>A search for getExternalContent or setExternalContent in the search field of the online doc will reveal <a href="http://help.autodesk.com/view/MAYAUL/2015/ENU/?guid=__cpp_ref_index_html">more</a>. The Python API does have the expected documentation:</p>

<ul>
<li><a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-SDK/py_ref/class_open_maya_1_1_m_px_node.html">MPxNode</a></li>
</ul>

<p>Another issue is that getFilesToArchive() will not be called on any MPxLocatorNode proxy derived class since the method was not implemented until the latest Maya 2016 service pack (SP1)</p>
