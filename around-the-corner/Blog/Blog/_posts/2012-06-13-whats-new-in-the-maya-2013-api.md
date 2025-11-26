---
layout: "post"
title: "What’s New in the Maya 2013 API?"
date: "2012-06-13 04:14:52"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "Linux"
  - "Mac"
  - "Maya"
  - "Python"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/whats-new-in-the-maya-2013-api.html "
typepad_basename: "whats-new-in-the-maya-2013-api"
typepad_status: "Publish"
---

<ul type="disc">
  <li>The &ldquo;devkit/plug-ins&rdquo; folder has been reorganized so       that the plug-in associated files are encapsulated under one folder and the       plug-in project will be parented under a single parent solution file       called &ldquo;Plug-ins.sln&rdquo;</li>
  <li>The module support for plug-in distribution has been       improved so that you will be able to more easily create a distributable       deployment of your plug-in. </li>
  <li>New attribute pattern API classes </li>
  <ul type="circle">
    <li>MAttributePattern: class that will provide a pattern of attributes to be applied to nodes as dynamic attributes or to node classes as extension attributes.</li>
    <li>MAttributePatternArray: class that will provide methods for manipulating arrays of attribute patterns.</li>
    <li>MPxAttributeFactory: base class added for user-defined attribute pattern factories.</li>
  </ul>
  <li>New threaded device node classes added for creating threaded Maya device nodes</li>
  <ul type="circle">
    <li>MPxThreadedDeviceNode: base class added for creating threaded Maya device nodes.</li>
    <li>MPxClientDeviceNode: extension of MPxThreadedDeviceNode, intended for creating Maya devices that act as clients.</li>
    <li>MCharBuffer (MPxThreadedDeviceNode.h): utility class added to deal with reference to a char* type.</li>
  </ul>
  <li>A group of new classes added under namespace MHWRender       to work with index buffer and vertex buffer in Viewport 2.0 </li>
  <ul type="circle">
    <li>MComponentDataIndexing: added class for storing index mapping when vertices are shared</li>
    <li>MComponentDataIndexingList: added class that defines a list of MComponentDataIndexing objects&nbsp;&nbsp;&nbsp; </li>
    <li>MHWGeometryUtilities: added a utility class for rendering geometry in Viewport 2.0, its wireframeColor() function will get the final wireframe color the draw can use.</li>
    <li>MGeometryExtractor: added base class for extracting renderable geometry, constructed an instance of this class to populate buffers with vertex and indexing data.</li>
    <li>MIndexBufferDescriptor (MHWGeometry.h): added class represents a description of an indexing scheme, indexing type, primitive type, primitive stride and component information.</li>
    <li>MIndexBufferDescriptorList (MHWGeometry.h): added a list of MIndexBufferDescriptor objects.</li>
    <li>MLightParameterInformation (MDrawContext.h): added a class for providing lighting information, which will allow for access to various per-light information accessible via the MDrawContext class in Viewport 2.0.</li>
    <li>MPxVertexBufferGenerator: added a base class for user defined vertex buffer generators.</li>
  </ul>
  <li>New classes added to provide callback hook into Maya software&rsquo;s       drag-and-drop mechanism. </li>
  <ul type="circle">
    <li>MExternalDropCallback: This class will be used to register callbacks to gain access to Maya software&rsquo;s drag-and-drop information during dropping an external object to Maya. You can replace or augment Maya software&rsquo;s drop behavior for external drag-and-drop operations. </li>
    <li>MExternalDropData: a class that represents data that a drag-and-drop operation carries if dragging from an external application and dropping onto Maya. It typically will arrive from a MExternalDropCallback callback method.</li>
  </ul>
</ul>
