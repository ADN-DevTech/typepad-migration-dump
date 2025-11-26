---
layout: "post"
title: "Paintable Attributes on Custom Deformer Node "
date: "2013-04-04 07:52:31"
author: "Cyrille Fauvel"
categories:
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2013/04/paintable-attributes-on-custom-deformer-node-.html "
typepad_basename: "paintable-attributes-on-custom-deformer-node-"
typepad_status: "Publish"
---

<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c38574009970b-pi" style="display: inline;"><img alt="Paintable" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c38574009970b" src="/assets/image_79cc06.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Paintable" /></a></p>
<p>To create a custom deformer node in Maya, you would need to derive from MPxDeformerNode, and the documentation says to not override the compute() method unless required since the deform() method is called by the MPxDeformerNode&#39;s compute() method.</p>
<p>There is few exception to that recommendation. Exceptions are namely:</p>
<ul>
<li>Your node&#39;s deformation algorithm depends on the geometry type, which is not available in the deform() method,</li>
<li>Your node&#39;s deformation algorithm requires computing all of the output geometries simultaneously,</li>
</ul>
<p>So if you node deformation algorithm depends on the input geometry type, which is not available in the deform() method, or your node&#39;s deformation algorithm requires computing all of the output geometries simultaneously, you have to implement the compute() method as well. </p>
<p>The devkit splatDeformer sample is a good example as its deformation algorithm requires to know the geometry type.</p>
<p>Note that in case you implement your own compute() method, you will tell Maya not to call deform anymore - so remember to write your node code accordingly.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d428657e3970c-pi" style="display: inline;"><img alt="Yellow-painting" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d428657e3970c" src="/assets/image_23e7e9.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Yellow-painting" /></a><br />If you already have an attribute in the deformer’s implementation, which you want to make paintable, you can call MEL command &quot;makePaintable&quot; like this:</p>
<p>&#0160; &#0160; &#0160;makePaintable -attrType &quot;multiFloat&quot; -sm &quot;deformer&quot; &quot;fStretch&quot; &quot;paintStretchGrowShrink&quot;;
</p>
<p>&quot;paintStretchGrowShrink&quot; being the attribute name in that example. </p>
<p>One thing to emphasize is in order to make an attribute paintable, it has to be a multi of multis. The multi attribute has to have a parent which is also a multi. The first level multi attribute is to record per vertex values, and the parent multi attribute is necessary to store values for multiple shapes.</p>
<p>&#0160;</p>
