---
layout: "post"
title: "Evaluating Node Attribute"
date: "2015-03-10 08:13:14"
author: "Naiqi Weng"
categories:
  - "C++"
  - "Maya"
  - "Naiqi Weng"
  - "Plug-in"
original_url: "https://around-the-corner.typepad.com/adn/2015/03/evaluating-node-attribute.html "
typepad_basename: "evaluating-node-attribute"
typepad_status: "Publish"
---

<p>Often in times, when people are grabbing attribute values working with Dependency Graph (DG) Nodes, people stumble across the situation what is the best way to access attribute values.&#0160; At a first glance, it looks both MPlug and MDataHandle can access attribute values and both of them would work.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb07fe49d7970d-pi" style="display: inline;"><img alt="Mplug-vs-handle" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01bb07fe49d7970d img-responsive" src="/assets/image_eff0af.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Mplug-vs-handle" /></a></p>
<p>However there are some caveats that you need to be aware when you choose to use one instead of the other:</p>
<p>One distinct difference between MPlug and MDataHandle way to access attribute, which is also the general rule is: During evaluation of a custom node (e.g. inside of MPxNode::compute()), you should use an MDataHandle from the MDatablock which was passed to compute(). Outside of evaluation you should use MPlug.</p>
<p>There may be exceptions where you need to manipulate DG evaluation in unintended way ( e.g. force dirtying during evaluation or avoid forcing an evaluation outside of evaluation) or to improve performance in time-critical code, but since you are taking risks of DG not evaluating correctly, you are basically on your own if you break DG evaluation. Therefore unless you are pretty sure what you are doing, we don’t recommend it.</p>
<p>In a situation of getting attribute value outside of compute() using MDataHandle, you may wonder how you would actually go and implement it. If you look closely into MPxNode class, you will find this function: MPxNode::forCache(). This function call allows you to get a handle of MDataBlock outside of compute() with a timed context and access attribute value. This is useful in situations where the data can be accessed more quickly through the MDatablock. For example, MPlug access might end up copying an array while MDataHandle access will let you access it directly.&#0160; But you need to be aware MPxNode::forceCache() doesn’t guarantee the values are updated, which means when you use forceCache(), a plug could be dirty, but not evaluated yet.</p>
<p>Also another tip to pay attention is when you use MDataBlock::inputValue(), the data handle you retrieved from this call will guaranteed to be valid for reading. If the data is from a dirty connection, then the connection will be evaluated. MDataBlock::outputValue(), on the other hand, does not guarantee the data to be valid. You may get some invalid or not up-to-date values. That’s why MDataBlock::inputValue() is suggested to be used to get attribute value, while MDataBlock::outputValue() is recommended to be used to set attribute value.</p>
<p>If you really want to compare MPlug with MDataHandle, here is a short summary:</p>
<p>Some advantages of MPlug are:</p>
<ol start="1" type="1">
<li>It will handle all of the dirty propagation for you.</li>
<li>It will automatically do any required evaluations.</li>
<li>It will automatically create multi elements when&#0160;needed.</li>
</ol>
<p>Some advantages of MDataBlock/MDataHandle are:</p>
<ol start="1" type="1">
<li>Usually faster than MPlug.</li>
<li>Let you access a dirty plug&#39;s current value without&#0160;forcing it to evaluate.</li>
</ol>
