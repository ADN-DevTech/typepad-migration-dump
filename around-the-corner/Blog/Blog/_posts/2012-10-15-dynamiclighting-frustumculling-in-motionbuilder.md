---
layout: "post"
title: "DynamicLighting & FrustumCulling in MotionBuilder"
date: "2012-10-15 00:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Rendering"
  - "Shader"
original_url: "https://around-the-corner.typepad.com/adn/2012/10/dynamiclighting-frustumculling-in-motionbuilder.html "
typepad_basename: "dynamiclighting-frustumculling-in-motionbuilder"
typepad_status: "Publish"
---

<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c53d5ca970c-pi" style="display: inline;"><img alt="Shadow_caster_culling" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c53d5ca970c" src="/assets/image_96eaba.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Shadow_caster_culling" /></a><br />If you want to manually specifying
Shadow Caster Culling during render pass, you could use the following functions on FBModel. This will give
you control per model instead of a global control. &#0160;</p>
<pre class="brush: cpp; toolbar: false;">/** Acquire no frustum culling request.
*&#0160;&#0160;\return Current no frustum culling request count after function call.
*/
int NoFrustumCullingRequire();

/** Release no frustum culling request.
*&#0160;&#0160;\return Current no frustum culling request count after function call.
*/
int NoFrustumCullingRelease();
</pre>
You can also use the ShaderPassTypeBegin()/End() to turn on / off frustum culling globally
&amp; temporarily.
