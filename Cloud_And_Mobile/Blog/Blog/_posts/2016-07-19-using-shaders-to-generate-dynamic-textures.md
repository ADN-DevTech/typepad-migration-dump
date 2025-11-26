---
layout: "post"
title: "Using Shaders to Generate Dynamic Textures in the Viewer API"
date: "2016-07-19 11:53:00"
author: "Michael Ge"
categories: []
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/using-shaders-to-generate-dynamic-textures.html "
typepad_basename: "using-shaders-to-generate-dynamic-textures"
typepad_status: "Publish"
---

<p>By Michael Ge (<a href="https://twitter.com/hahakumquat">@hahakumquat</a>)</p>
<p>A week ago, I wrote a <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/07/projecting-dynamic-textures-onto-flat-surfaces-with-threejs.html">post</a> on injecting dynamic THREE.js textures into a viewer scene by creating a plane mesh with a canvas-based material. While the solution works for simple surfaces, it fails for two major reasons:</p>
<ol>
<li>Simply applying a texture to a previously uncolored fragment usually yields an incorrect projection (since the&#0160;<a href="https://en.wikipedia.org/wiki/UV_mapping">UV mapping</a>&#0160;coordinates have most likely not been defined).</li>
<li>A&#0160;model&#39;s fragment often has complex geometry that cannot be imitated by built-in THREE.js shapes.&#0160;</li>
</ol>
<p>With WebGL shaders, however, we can solve both problems by using pre-existing geometry and defining how each pixel should be rendered.</p>
<p><strong>A Brief Introduction&#0160;to&#0160;Shaders</strong></p>
<p>Coming from more of a 3D animation background, shaders seemed like scary, mystical things that always &quot;just worked.&quot; But in fact, the concept behind writing shader code is simple. There are only two types of shaders: vertex shaders and fragment shaders. Vertex shaders are programs that define how every point along a surface should be positioned, while fragment shader programs define how every point is colored. Note that a &quot;point&quot; is different from a &quot;vertex&quot; as points span the entire surface while vertices define the boundaries. These programs output vectors based on parameters such as position, the current UV coordinate, etc.&#0160;<a href="https://aerotwist.com/tutorials/an-introduction-to-shaders-part-1/">Aerotwist&#39;s intro to shaders</a>&#0160;is a great way to get started relatively painlessly using built-in THREE.js functionality.</p>
<p><strong>Generating the Material</strong></p>
<script src="https://gist.github.com/hahakumquat/7d028f0391feea9de3ca71f17eaab9a6.js"></script>
<p>&#0160;First, we&#39;ll use THREE.js&#39;s native ShaderMaterial instead of a preset mesh material to texture a fragment. Here, we pass in the vertex shader, fragment shader, and uniform variables. The uniforms include the width and height of the fragment I want to project on, the canvas texture, and a&#0160;<em>corner&#0160;</em>variable that is (er, initially was) used for offsetting every point so have nonnegative coordinates. The calculations for&#0160;<em>_bounds</em>&#0160;were determined by the genBounds function here:</p>
<script src="https://gist.github.com/hahakumquat/f17845683f248a751c141a4e1a917d42.js"></script>
<p>The color and displacement of every point is then determined by the shaders.&#0160;</p>
<p><strong>The Shaders</strong></p>
<p>For my heat map, I wanted every point on the canvas to translate to its scaled position on the actual rooftop, but with a bit of math, shaders offer much more complex and interesting visualizations.&#0160;</p>
<p>The final position of any point is determined by the&#0160;<em>gl_Position</em> variable in the vertex shader. THREE.js magically passes in the position of the current point, <em>position</em>, as well as&#0160;projection and model-view matrices required to transform the position to the correct perspective. During these calculations, I also determine a vec2 called&#0160;<em>vUv&#0160;</em>to pass into the fragment shader.</p>
<script src="https://gist.github.com/hahakumquat/7de7af61b44a1fb16379390582a88a44.js"></script>
<p>Now, my original intent when calculating <em>vUv&#0160;</em>was&#0160;to project the point onto the xy-plane (bird&#39;s eye view), shift it by an offset defined by the minimum corner of the roof, and divide the result by the roof width and height. In theory, this should return a value between 0 and 1, which is the format in which the fragment shader can understand how to apply the texture. The calculation looked something like:</p>
<p>vUv = vec2(abs(projection.x - corner.x) / width, abs(projection.y - corner.y) / height))</p>
<p>But for reasons unknown, it doesn&#39;t perform as expected. The code in the gist seems to work, but if anyone can figure out how to sensibly calculate the UV coordinates, I&#39;d love to know.</p>
<p>In either case, a UV&#0160;vec2 of scaled&#0160;values between 0 and 1 is passed to the fragment shader. Here, the&#0160;<em>gl_FragColor </em>simply&#0160;applies the texture at the UV coordinate specified by <em>vUv</em>.</p>
<p><strong>Setting the Material</strong></p>
<script src="https://gist.github.com/hahakumquat/ebb0811d6020b313e5dd533157601655.js"></script>
<p>The fragment proxy gives us access to a mesh&#39;s material, so we simply update the model with our newly created shader material. Here, I access the roof mesh&#39;s fragment proxy and recolor the roof to the appropriate material.</p>
<p><strong>Implications</strong></p>
<p>We now have a way to display textures onto meshes that weren&#39;t intended to have textures! The possibilities really are endless. Videos can be projected onto any surface, data can be pulled into a scene to create an IoT viewer, even entire webpages can be placed in a scene. The specific use case is up to you, but the idea of taking a texturable asset, importing it into THREE.js, and projecting it via either shaders or standard materials makes the viewer an especially powerful tool.</p>
