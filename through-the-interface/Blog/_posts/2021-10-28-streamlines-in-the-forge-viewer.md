---
layout: "post"
title: "Streamlines in the Forge viewer"
date: "2021-10-28 17:58:08"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "VASA"
original_url: "https://www.keanw.com/2021/10/streamlines-in-the-forge-viewer.html "
typepad_basename: "streamlines-in-the-forge-viewer"
typepad_status: "Publish"
---

<p>One more feature inspired by <a href="https://dasher360.com" rel="noopener" target="_blank">Project Dasher</a> has made its way into the Forge viewer’s <a href="https://www.keanw.com/2021/05/its-officially-available-data-visualization-extension-for-the-forge-viewer.html" rel="noopener" target="_blank">Data Visualization Extension</a> (Project Hyperion): streamlines. This is something I’ve talked about a few times <a href="https://www.keanw.com/2018/09/displaying-streamlines-in-the-forge-viewer.html" rel="noopener" target="_blank">in</a> <a href="https://www.keanw.com/2018/10/an-update-on-my-au2018-classes.html" rel="noopener" target="_blank">the</a> <a href="https://www.keanw.com/2019/06/this-years-forge-accelerator-in-barcelona.html" rel="noopener" target="_blank">past</a>, and I’m happy to see it make its way into the Forge platform. Streamlines are a great mechanism for indicating movement – of people or assets – through an architectural space, or perhaps something like a toolpath in a manufacturing context.</p>
<p>The way we implemented them originally inside Dasher wasn’t ideal, in all fairness: I used the <a href="https://github.com/elifer5000/THREE.MeshLine" rel="noopener" target="_blank">MeshLine.js library</a> to be able to display lines with thickness in the Forge viewer. The Hyperion team implemented their own shader to do the work for this, which is much better.</p>
<p>The new StreamLine object is quite easy to use – it adds itself to an appropriate overlay scene when you create it via the StreamLineBuilder object, and then removes itself from said scene when you destroy it. I had a particular use for this shiny new feature, which was to display the paths created by <a href="https://www.keanw.com/2021/09/introducing-vasa-for-voxel-based-architectural-space-analysis.html" rel="noopener" target="_blank">VASA</a> inside a Forge scene: I’ve created Emscripten bindings for the underlying C++ VASA library so that we could build a WebAssembly (WASM) package, and use it with triangles from the loaded scene in the Forge viewer to find paths between the entrance of a building to various exits. The fact the below workflow is shown inside Dasher is merely a convenience: it was an easy way for me to prototype the use of VASA with Forge, and also the use of the new StreamLine object to display the resulting path(s).</p>
<p>By default the StreamLines get occluded by the building itself:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdefc1a42200c-pi" rel="noopener" target="_blank"><img alt="VASA&#39;s path inside Dasher using a Hyperion StreamLine" border="0" height="312" src="/assets/image_956628.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="VASA&#39;s path inside Dasher using a Hyperion StreamLine" width="500" /></a></p>
<p>But this easy to change by going in and toggling the depthWrite property on the associated StreamLineMaterial. Here’s the same shot with “X-ray mode” enabled:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202788053f25f200d-pi" rel="noopener" target="_blank"><img alt="VASA&#39;s path inside Dasher using a Hyperion StreamLine - in X-ray mode" border="0" height="312" src="/assets/image_573601.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="VASA&#39;s path inside Dasher using a Hyperion StreamLine - in X-ray mode" width="500" /></a></p>
<p>Here’s a side view – of the same path – still in X-ray mode:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202788053f263200d-pi" rel="noopener" target="_blank"><img alt="Now from another angle" border="0" height="312" src="/assets/image_894765.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Now from another angle" width="500" /></a></p>
<p>If we go back to the previous view, it’s interesting to see that selecting a destination point close-by leads to a very different path being followed through the building, as it’s marginally shorter to take that path than the previous one.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdefc1a4a200c-pi" rel="noopener" target="_blank"><img alt="A nearby destination leads to a very different path through the building" border="0" height="312" src="/assets/image_105045.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="A nearby destination leads to a very different path through the building" width="500" /></a></p>
<p>But this post isn’t really about VASA – it’s about the new StreamLine object that’s available in the Forge viewer (as of v7.50 or so, I believe). Here’s a snippet of JavaScript code provided by the Hyperion team that will create a simple StreamLine and add it to your Forge viewer scene.</p>
<div class="vscode" style="white-space: pre;">
<div><span style="color: #569cd6;">const</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4fc1ff;">points</span><span style="color: #d4d4d4;"> = [ </span><span style="color: #b5cea8;">10</span><span style="color: #d4d4d4;">, </span><span style="color: #b5cea8;">10</span><span style="color: #d4d4d4;">, </span><span style="color: #b5cea8;">10</span><span style="color: #d4d4d4;">, </span><span style="color: #b5cea8;">20</span><span style="color: #d4d4d4;">, </span><span style="color: #b5cea8;">20</span><span style="color: #d4d4d4;">, </span><span style="color: #b5cea8;">20</span><span style="color: #d4d4d4;">, ... ];</span></div>
<div>&#0160;</div>
<div><span style="color: #569cd6;">const</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4fc1ff;">streamLineData</span><span style="color: #d4d4d4;"> = {</span></div>
<div><span style="color: #d4d4d4;">&#0160;&#0160;&#0160; </span><span style="color: #9cdcfe;">points:</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #569cd6;">new</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4ec9b0;">Float32Array</span><span style="color: #d4d4d4;">(</span><span style="color: #9cdcfe;">points</span><span style="color: #d4d4d4;">)</span></div>
<div><span style="color: #d4d4d4;">};</span></div>
<div>&#0160;</div>
<div><span style="color: #569cd6;">const</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4fc1ff;">streamLineSpecs</span><span style="color: #d4d4d4;"> = {</span></div>
<div><span style="color: #d4d4d4;">&#0160;&#0160;&#0160; </span><span style="color: #9cdcfe;">lineWidth:</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #b5cea8;">8.0</span><span style="color: #d4d4d4;">,</span></div>
<div><span style="color: #d4d4d4;">&#0160;&#0160;&#0160; </span><span style="color: #9cdcfe;">lineColor:</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #569cd6;">new</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4fc1ff;">THREE</span><span style="color: #d4d4d4;">.</span><span style="color: #dcdcaa;">Color</span><span style="color: #d4d4d4;">(</span><span style="color: #b5cea8;">0xff0080</span><span style="color: #d4d4d4;">),</span></div>
<div><span style="color: #d4d4d4;">&#0160;&#0160;&#0160; </span><span style="color: #9cdcfe;">opacity:</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #b5cea8;">1.0</span><span style="color: #d4d4d4;">,</span></div>
<div><span style="color: #d4d4d4;">&#0160;&#0160;&#0160; </span><span style="color: #9cdcfe;">lineData:</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #9cdcfe;">streamLineData</span></div>
<div><span style="color: #d4d4d4;">};</span></div>
<div>&#0160;</div>
<div><span style="color: #569cd6;">const</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4fc1ff;">streamLineBuilder</span><span style="color: #d4d4d4;"> = </span><span style="color: #9cdcfe;">dataVizExtension</span><span style="color: #d4d4d4;">.</span><span style="color: #9cdcfe;">streamLineBuilder</span><span style="color: #d4d4d4;">;</span></div>
<div><span style="color: #569cd6;">const</span><span style="color: #d4d4d4;">&#0160;</span><span style="color: #4fc1ff;">streamLine</span><span style="color: #d4d4d4;"> = </span><span style="color: #9cdcfe;">streamLineBuilder</span><span style="color: #d4d4d4;">.</span><span style="color: #dcdcaa;">createStreamLine</span><span style="color: #d4d4d4;">(</span><span style="color: #9cdcfe;">streamLineSpecs</span><span style="color: #d4d4d4;">);</span></div>
</div>
<p>The main thing to note are the points passed in are in a flattened array: three items in the array contribute to an individual vertex of the StreamLine, of course. Otherwise it should be fairly obvious how it works.</p>
<p>Destroying the StreamLine – which, as I said before, removes it from the scene – is very simple:</p>
<div class="vscode" style="white-space: pre;">
<div><span style="color: #9cdcfe;">streamLineBuilder</span><span style="color: #d4d4d4;">.</span><span style="color: #dcdcaa;">destroyStreamLine</span><span style="color: #d4d4d4;">(</span><span style="color: #9cdcfe;">streamLine</span><span style="color: #d4d4d4;">);</span></div>
</div>
<p>I’ll dig into more advanced capabilities of the StreamLine object in future posts. I hope you find interesting uses for them in the meantime!</p>
