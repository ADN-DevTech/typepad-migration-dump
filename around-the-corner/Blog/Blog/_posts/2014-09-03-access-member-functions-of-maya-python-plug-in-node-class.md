---
layout: "post"
title: "Access Member Functions of Maya Python Plug-in Node Class"
date: "2014-09-03 01:14:22"
author: "Cyrille Fauvel"
categories:
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
  - "Plug-in"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2014/09/access-member-functions-of-maya-python-plug-in-node-class.html "
typepad_basename: "access-member-functions-of-maya-python-plug-in-node-class"
typepad_status: "Draft"
---

<p>In a C++ implementation, someone can get a pointer on a node instance after its creation by using the MSelectionList class. The MSelectionList class is used to get&#0160;a MObject of a node instance and MFnDependencyNode::userNode() returns a pointer to an instance of MPxNode, which can be typecast to this subclass instance and the member function can be retrieved. But there is no notion of pointer in Python. How to get to custom node class member functions?</p>
<p>If you don&#39;t have control over the source code of the plug-in, there&#39;s no way that you will be able to get its member function after the instance of the node is created.</p>
<p>If you do have control over the source of the plugin, then you can work around this problem by creating a dictionary and taking advantage of the properties of the OpenMayaMPx.asHashable() function. OpenMayaMPx.asHashable() takes an MPx object and returns a hash value which uniquely identifies the underlying Maya object. It doesn&#39;t matter whether the object is an instance of the base MPx class or your derived class: so long as they both refer to the same underlying Maya object they will return the same hash value. This allows you to set up a dictionary which can translate a base instance into its corresponding derived instance.</p>
<p>You can do it by placing the tracking dictionary onto some global Python module such as &#39;sys&#39;. E.g:</p>
<p>import sys</p>
<p>class sineNode(OpenMayaMPx.MPxNode):<br /> def __init__(self):<br /> OpenMayaMPx.MPxNode.__init__(self)<br /> sys.sineNode_nodes[OpenMayaMPx.asHashable(self)] = self</p>
<p>def specialFunc(self):<br /> print(&#39;special func defined on sineNode classe&#39;)</p>
<p>...</p>
<p>Then, outside of the plugin, if you have a sineNode in an MObject called &#39;nodeObj&#39;, you can get at its Python object as follows:</p>
<p>fn = OpenMaya.MFnDependencyNode(nodeObj)<br />nodePtr = fn.userNode()<br />node = sys.sineNode_nodes[OpenMayaMPx.asHashable(nodePtr)]<br />node.specialFunc()</p>
<p>In the Python API 2.0 you won&#39;t need the tracking dictionary at all and will be able to directly use the result from userNode(). E.g:</p>
<p>fn = OpenMaya.MFnDependencyNode(nodeObj)<br />node = fn.userNode()<br />node.specialFunc()</p>
