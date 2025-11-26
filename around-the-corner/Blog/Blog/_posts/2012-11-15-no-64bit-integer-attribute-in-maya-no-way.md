---
layout: "post"
title: "No 64bit integer attribute in Maya? no way!"
date: "2012-11-15 01:30:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
  - "Plug-in"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/11/no-64bit-integer-attribute-in-maya-no-way.html "
typepad_basename: "no-64bit-integer-attribute-in-maya-no-way"
typepad_status: "Publish"
---

<p>Ok, but where is it?</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3d700355970c-pi" style="display: inline;"><img alt="Where-is-it" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3d700355970c" src="/assets/image_409040.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Where-is-it" /></a><br />If you take a look to the Maya API documentation you will find a lot of things about 64b support, but the only classes specific to 64b will be:</p>
<ul>
<li>the&#0160;MUint64Array which is an array of MUint64 data type,</li>
<li>the MFnUInt64ArrayData class which allows the creation and manipulation of the MUint64Array objects for use in the dependency graph when the MDataHandle::type() method returns kUInt64Array.</li>
</ul>
<p>The&#0160;MFnUInt64ArrayData /&#0160;MUint64Array /&#0160;MUint64 classes are present in the API to support Subdivision surface&#0160;(I.e.&#0160;MFnSubd /&#0160;MFnSubdNames /&#0160;MFnUint64SingleIndexedComponent /&#0160;MItSubdEdge /&#0160;MItSubdFace /&#0160;MItSubdVertex)</p>
<p>Now there are some other classes in the devkit like the Mint64 class, but you would not find it documented anywhere since there isn&#39;t any direct use of it anywhere in Maya or the Maya DG :(</p>
<p>There is currently no support of single signed / unsigned 64b integer attribute in Maya, but fortunately for us, Maya is versatile in a way that it can handle any data type (simple or complex). Anyone can eventually expose a custom attribute type. The example provided along this post demonstrate how to do this. But before we go into details there is one important thing to know before using 64b attributes in Maya. While the Maya API (C++ or Python) can work easily with 64b integer. MEL and the Python commands will not! This is because MEL is not 64b capable. The MEL engine is a 32b engine only, so you would not be able to query a 64b integer attribute value and store it into a MEL variable for example.</p>
<p><span style="text-decoration: underline;">Example:</span> assuming&#0160;myInt64 is a 64b integer attribute</p>
<pre class="brush: cpp; toolbar: false;">$val64 = 0xFFFFFFFF64; // that will fail since MEL cast it to 0x7FFFFFFF which is a signed 32b integer
setAttr sphere1.myInt64 $val64; // already wrong on the line above
setAttr sphere1.myInt64 0xFFFFFFFF64; // that will work, see below why
getAttr&#0160;sphere1.myInt64; // that will fail with an error, MEL will never return a value
</pre>
<p>getAttr fails because the MEL interpreter cannot handle 64b integer since MEL is a 32b engine. But then why setAttr is working fine to assign a 64b integer? Simply because the value is passed as a string and evaluated in by the C++ API vs MEL interpreter. However we can&#39;t read it back :(<br />Python setAttr/getAttr commands will behave exactly the same since the Python commands are wrapper of the MEL equivalent commands. So even if the Python interpreter can handle 64b integer variable, when it goes to the MEL interpreter, you&#39;ll get the same results. At the time of this post, only the API (C++ and Python) can work with 64b integer attribute.</p>
<h2>An 64b attribute implementation</h2>
<p>First we need to define and create the class which Maya will use to handle our 64b integer data. For this we will create and register a class in Maya using the MPxData&#0160;base class.</p>
<pre class="brush: cpp; toolbar: false;">class MInt64Data : public MPxData {};

MFnPlugin.registerData (MInt64Data::typeName, MInt64Data::id, MInt64Data::creator, MPxData::kData)
</pre>
<p>This is really the only thing you need to do to add 64b attribute support in Maya. The complete implementation for the&#0160;MInt64Data can be found in the sample <a href="http://around-the-corner.typepad.com/files/Int64AttributeData.zip" target="_self">here</a>&#0160;(<strong>Int64AttributeData</strong> sample). From there, when the plug-in is loaded in Maya, Maya can handle static, dynamic, and&#0160;extension attributes with 64b integer data type.</p>
<p>The sample also expose and exports a MFn class to handle the attribute data type from an API standpoint. The class is class&#0160;MFnInt64Data derived from MFnPluginData.</p>
<pre class="brush: cpp; toolbar: false;">class DLLIMPEXP MFnInt64Data : public MFnPluginData {};</pre>
<p>The&#0160;<strong>Int64AttributeDataNodes</strong> sample is there to supply a node which can connect to our 64b attributes and convert data back and force to string, so it they can be used to read/write 64b integer from MEL indirectly as string. It also use an unsigned int64 data type vs signed unlike the previous example.</p>
<p>The&#0160;<strong>Int64AttributeDataWC</strong> is another sample which tries to solve the MEL limitation another way by splitting a 64b attribute data in low and high word numbers into a MEL array.</p>
<p>&#0160;</p>
