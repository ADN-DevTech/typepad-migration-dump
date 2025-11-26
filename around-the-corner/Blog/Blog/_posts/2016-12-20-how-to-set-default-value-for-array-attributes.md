---
layout: "post"
title: "How to set default value for array attributes"
date: "2016-12-20 17:22:17"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2016/12/how-to-set-default-value-for-array-attributes.html "
typepad_basename: "how-to-set-default-value-for-array-attributes"
typepad_status: "Publish"
---

<p>We were asked some array questions recently, and I found that there isn&#39;t a good sample in Maya devkit. This article will demonstrate how to initialize an array with MArrayDataBuilder. &#0160;<br /><br /> MPxNode::Compute isn&#39;t a good choice to give your array a default value. The reasons are that the compute will be called after a plug is dirty, or it will be connected on demand. The proper way to give your array a default value is to use the MPxNode::postConstructor method. For example, I can create a default value for the weightListNode sample from the Maya devkit with following code:</p>
<pre id="dNSACAV4wfn">void weightList::postConstructor()<br />{<br />&#0160; &#0160;&#0160;MStatus status = MStatus::kSuccess;<br />&#0160; &#0160; // Get the datablock outside the compute with MPxNode::forceCache<br />&#0160;&#0160; &#0160;MDataBlock block = this-&gt;forceCache();<br /><br />&#0160; &#0160; // The remaining part is the same as compute<br />&#0160;&#0160; &#0160;unsigned i, j;<br />&#0160;&#0160; &#0160;MObject thisNode = thisMObject();<br />&#0160;&#0160; &#0160;MPlug wPlug(thisNode, aWeights); <br /><br />&#0160;&#0160; &#0160;// Write into aWeightList<br />&#0160;&#0160; &#0160;for( i = 0; i &lt; 3; i++) {<br />&#0160;&#0160; &#0160; &#0160; &#0160;status = wPlug.selectAncestorLogicalIndex( i, aWeightsList );<br />&#0160;&#0160; &#0160; &#0160; &#0160;MDataHandle wHandle = wPlug.constructHandle(block);<br />&#0160;&#0160; &#0160; &#0160; &#0160;MArrayDataHandle arrayHandle(wHandle, &amp;status);<br />&#0160;&#0160; &#0160; &#0160; &#0160;//McheckErr(status, &quot;arrayHandle construction failed\n&quot;);<br />&#0160;&#0160; &#0160; &#0160; &#0160;MArrayDataBuilder arrayBuilder = arrayHandle.builder(&amp;status);<br />&#0160;&#0160; &#0160; &#0160; &#0160;<br />&#0160;&#0160; &#0160; &#0160; &#0160;for( j = 0; j &lt; i+2; j++) {<br />&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;MDataHandle handle = arrayBuilder.addElement(j,&amp;status);&#0160; &#0160; &#0160; &#0160;&#0160;<br />&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;float val = (float)(1.0f*(i+j)); <br />&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;handle.set(val);<br />&#0160;&#0160; &#0160; &#0160; &#0160;}<br />&#0160;&#0160; &#0160; &#0160; &#0160;status = arrayHandle.set(arrayBuilder);<br />&#0160;&#0160; &#0160; &#0160; &#0160;<br />&#0160;&#0160; &#0160; &#0160; &#0160;wPlug.setValue(wHandle);<br />&#0160;&#0160; &#0160; &#0160; &#0160;wPlug.destructHandle(wHandle);<br />&#0160;&#0160; &#0160;}<br />}</pre>
<p>This will be called after the node is created and will insert three elements into the weightList attribute. That&#39;s it :)&#0160;</p>
