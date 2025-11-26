---
layout: "post"
title: "MFn::kFluid and MFn::KFluidData attribute connection and limitation"
date: "2016-06-29 19:05:21"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Maya"
  - "Plug-in"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/06/mfnkfluid-and-mfnkfluiddata-attribute-connection-and-limitation.html "
typepad_basename: "mfnkfluid-and-mfnkfluiddata-attribute-connection-and-limitation"
typepad_status: "Publish"
---

<p><br /> <br /> We can create an attribute(typed attribute) using the following Maya C++ API <br /> MFnTypedAttribute has 2 overload methods for creating attribute.</p>
<p><br /> MObject create (const MString &amp;fullName, const MString &amp;briefName, <strong>const MTypeId &amp;id</strong>, MObject defaultData=MObject::kNullObj, MStatus *ReturnStatus=NULL)<br /> MObject create (const MString &amp;fullName, const MString &amp;briefName, <strong>MFnData::Type type</strong>, MObject defaultData=MObject::kNullObj, MStatus *ReturnStatus=NULL)<br /> <br /> Here is the sample code</p>
<pre>    MObject              fluidType::outGrid;<br />    MStatus              stat;<br />    MFnTypedAttribute    tAttr;<br />    <br />    outGrid = tAttr.create(&quot;outGrid&quot;, &quot;ogd&quot;, MFn::kFluid, MObject::kNullObj, NULL);<br />    if (outGrid.isNull()) cerr &lt;&lt; &quot;object is null&quot; &lt;&lt; endl;<br /><br />    tAttr.setWritable(false);<br />    tAttr.setStorable(false);</pre>
<p>If we create an attribute with MFn::kFluid or MFn::kFluidData, because it is an enumerator, the create() function is not able to type cast or map it to MFnData::Type enumerator. Hence, if we try to create an attribute with the above method, you will get MObject as NULL.<br /> <br /> In short, the MFn::kFluid and MFn::kFluidData attributes are fluid attributes that can not be created explicitly using either C++ API or using MEL script. But, if you assign any fluid shape to object, you can check whether the object has MFn::kFluidData or MFn::kFluid. For example, node.hasFn(MFn::kFluid) where node is MObject type.<br /> We can connect fluid type of attributes only when if both the attributes are same type. For example, I created “fluid shape” node and “simpleFluidEmitter” node from devkit sample. I am able to connect “fluidShape.outGrid” to “simpleFluidEmitter.geometry” attributes.<br /> <br /> Please check the below output:<br /> <br /> getAttr -type simpleFluidEmitter.geometry;<br /> // Result: fluid // <br /> <br /> getAttr -type fluidShape1.outGrid;<br /> // Result: fluid //<br /> <br /> <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c87557a4970b-pi" style="display: inline;"><img alt="Fluid_outGrid_geometry" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01b7c87557a4970b image-full img-responsive" src="/assets/image_044491.jpg" title="Fluid_outGrid_geometry" /></a><br /><br /></p>
