---
layout: "post"
title: "Rotate Components Control for the Viewer"
date: "2016-07-12 07:29:41"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/rotate-component-control-for-the-viewer.html "
typepad_basename: "rotate-component-control-for-the-viewer"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" target="_blank">(@F3lipek)</a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;The viewer API provides access to individual components in a model and the programmer has the possibility to transform them at will by applaying matrices, translations are rather straightforward, however rotations can get a bit tricky. There is also no built-in tools directly provided by the API to transform components from the UI.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;I worked in the past on a first&#0160;version of the <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/08/moving-visually-your-components-in-the-viewer-using-the-transformtool.html">translate tool</a> and finally got the opportunity to deal with the rotation. If the translate tool is using a transform control provided by three.js API, I wasn&#39;t really satisfied by the rotate feature, so I decided to write my own and learn a couple of things at the same time.&#0160;</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;Here are the two main highlights of that sample:</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">1/ The <em><strong>pointerToRaycaster</strong></em> method that converts a 2d screenpoint from the mouse pointer into a three.js Raycaster object that lets you perform object picking selection on screen, this is used to handle user interaction with our rotate gizmo control</span></p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 12pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">// Creates Raycaster object from mouse or touch pointer
</span><span style="color: #800000; background-color: #f0f0f0;"> 3 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 </span><span style="background-color: #ffffff;">pointerToRaycaster (pointer) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 
 7 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> pointerVector = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Vector3()
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> pointerDir = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Vector3()
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> ray = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Raycaster()
</span><span style="color: #800000; background-color: #f0f0f0;">10 
11 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> rect = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.domElement.getBoundingClientRect()
</span><span style="color: #800000; background-color: #f0f0f0;">12 
13 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> x = ((pointer.clientX - rect.left) / rect.width) * </span><span style="color: #0000ff; background-color: #ffffff;">2</span><span style="background-color: #ffffff;"> - </span><span style="color: #0000ff; background-color: #ffffff;">1
</span><span style="color: #800000; background-color: #f0f0f0;">14 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> y = -((pointer.clientY - rect.top) / rect.height) * </span><span style="color: #0000ff; background-color: #ffffff;">2</span><span style="background-color: #ffffff;"> + </span><span style="color: #0000ff; background-color: #ffffff;">1
</span><span style="color: #800000; background-color: #f0f0f0;">15 
16 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">if</span><span style="background-color: #ffffff;"> (</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.camera.isPerspective) {
</span><span style="color: #800000; background-color: #f0f0f0;">17 
18 </span><span style="background-color: #ffffff;">    pointerVector.set(x, y, </span><span style="color: #0000ff; background-color: #ffffff;">0.5</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">19 
20 </span><span style="background-color: #ffffff;">    pointerVector.unproject(</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.camera)
</span><span style="color: #800000; background-color: #f0f0f0;">21 
22 </span><span style="background-color: #ffffff;">    ray.set(</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.camera.position,
</span><span style="color: #800000; background-color: #f0f0f0;">23 </span><span style="background-color: #ffffff;">      pointerVector.sub(
</span><span style="color: #800000; background-color: #f0f0f0;">24 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.camera.position).normalize())
</span><span style="color: #800000; background-color: #f0f0f0;">25 
26 </span><span style="background-color: #ffffff;">  } </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">else</span><span style="background-color: #ffffff;"> {
</span><span style="color: #800000; background-color: #f0f0f0;">27 
28 </span><span style="background-color: #ffffff;">    pointerVector.set(x, y, -</span><span style="color: #0000ff; background-color: #ffffff;">1</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">29 
30 </span><span style="background-color: #ffffff;">    pointerVector.unproject(</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.camera)
</span><span style="color: #800000; background-color: #f0f0f0;">31 
32 </span><span style="background-color: #ffffff;">    pointerDir.set(</span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">, </span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">, -</span><span style="color: #0000ff; background-color: #ffffff;">1</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">33 
34 </span><span style="background-color: #ffffff;">    ray.set(pointerVector,
</span><span style="color: #800000; background-color: #f0f0f0;">35 </span><span style="background-color: #ffffff;">      pointerDir.transformDirection(
</span><span style="color: #800000; background-color: #f0f0f0;">36 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.camera.matrixWorld))
</span><span style="color: #800000; background-color: #f0f0f0;">37 </span><span style="background-color: #ffffff;">  }
</span><span style="color: #800000; background-color: #f0f0f0;">38 
39 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">return</span><span style="background-color: #ffffff;"> ray
</span><span style="color: #800000; background-color: #f0f0f0;">40 </span><span style="background-color: #ffffff;">}</span></pre>
<p><span style="font-family: arial, helvetica, sans-serif;">This can later be used as follow:</span></p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 12pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="background-color: #ffffff;">onPointerDown (event) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 
 3 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> pointer = event.pointers ? event.pointers[ </span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;"> ] : event
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 
 5 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">if</span><span style="background-color: #ffffff;"> (pointer.button === </span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 
 7 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> ray = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.pointerToRaycaster(pointer)
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 
 9 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> intersectResults = ray.intersectObjects(
</span><span style="color: #800000; background-color: #f0f0f0;">10 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.gizmos, </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">true</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">11 
12 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">if</span><span style="background-color: #ffffff;"> (intersectResults.length) {
</span><span style="color: #800000; background-color: #f0f0f0;">13 
14 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.gizmos.forEach((gizmo) =&gt; {
</span><span style="color: #800000; background-color: #f0f0f0;">15 
16 </span><span style="background-color: #ffffff;">        gizmo.visible = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">false
</span><span style="color: #800000; background-color: #f0f0f0;">17 </span><span style="background-color: #ffffff;">      })
</span><span style="color: #800000; background-color: #f0f0f0;">18 
19 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.selectedGizmo = intersectResults[</span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">].object</span></pre>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif;">2/ The <em><strong>rotateFragments</strong></em> method that perform rotation of the selected meshes (fragments) according to the specific input parameters &#0160;</span></p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 12pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">// Rotate selected fragIds of specific model around based on specified
</span><span style="color: #800000; background-color: #f0f0f0;"> 3 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">// axis, angle and center
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 </span><span style="background-color: #ffffff;">rotateFragments (model, fragIdsArray, axis, angle, center) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 7 
 8 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> quaternion = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Quaternion()
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 
10 </span><span style="background-color: #ffffff;">  quaternion.setFromAxisAngle(axis, angle)
</span><span style="color: #800000; background-color: #f0f0f0;">11 
12 </span><span style="background-color: #ffffff;">  fragIdsArray.forEach((fragId, idx) =&gt; {
</span><span style="color: #800000; background-color: #f0f0f0;">13 
14 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> fragProxy = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.viewer.impl.getFragmentProxy(
</span><span style="color: #800000; background-color: #f0f0f0;">15 </span><span style="background-color: #ffffff;">      model, fragId)
</span><span style="color: #800000; background-color: #f0f0f0;">16 
17 </span><span style="background-color: #ffffff;">    fragProxy.getAnimTransform()
</span><span style="color: #800000; background-color: #f0f0f0;">18 
19 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> position = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Vector3(
</span><span style="color: #800000; background-color: #f0f0f0;">20 </span><span style="background-color: #ffffff;">      fragProxy.position.x - center.x,
</span><span style="color: #800000; background-color: #f0f0f0;">21 </span><span style="background-color: #ffffff;">      fragProxy.position.y - center.y,
</span><span style="color: #800000; background-color: #f0f0f0;">22 </span><span style="background-color: #ffffff;">      fragProxy.position.z - center.z)
</span><span style="color: #800000; background-color: #f0f0f0;">23 
24 </span><span style="background-color: #ffffff;">    position.applyQuaternion(quaternion)
</span><span style="color: #800000; background-color: #f0f0f0;">25 
26 </span><span style="background-color: #ffffff;">    position.add(center)
</span><span style="color: #800000; background-color: #f0f0f0;">27 
28 </span><span style="background-color: #ffffff;">    fragProxy.position = position
</span><span style="color: #800000; background-color: #f0f0f0;">29 
30 </span><span style="background-color: #ffffff;">    fragProxy.quaternion.multiplyQuaternions(
</span><span style="color: #800000; background-color: #f0f0f0;">31 </span><span style="background-color: #ffffff;">      quaternion, fragProxy.quaternion)
</span><span style="color: #800000; background-color: #f0f0f0;">32 
33 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">if</span><span style="background-color: #ffffff;"> (idx === </span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">) {
</span><span style="color: #800000; background-color: #f0f0f0;">34 
35 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> euler = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> THREE.Euler()
</span><span style="color: #800000; background-color: #f0f0f0;">36 
37 </span><span style="background-color: #ffffff;">      euler.setFromQuaternion(
</span><span style="color: #800000; background-color: #f0f0f0;">38 </span><span style="background-color: #ffffff;">        fragProxy.quaternion, </span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">39 
40 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.emit(</span><span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;transform.rotate&#39;</span><span style="background-color: #ffffff;">, {
</span><span style="color: #800000; background-color: #f0f0f0;">41 </span><span style="background-color: #ffffff;">        rotation: euler,
</span><span style="color: #800000; background-color: #f0f0f0;">42 </span><span style="background-color: #ffffff;">        model
</span><span style="color: #800000; background-color: #f0f0f0;">43 </span><span style="background-color: #ffffff;">      })
</span><span style="color: #800000; background-color: #f0f0f0;">44 </span><span style="background-color: #ffffff;">    }
</span><span style="color: #800000; background-color: #f0f0f0;">45 
46 </span><span style="background-color: #ffffff;">    fragProxy.updateAnimTransform()
</span><span style="color: #800000; background-color: #f0f0f0;">47 </span><span style="background-color: #ffffff;">  })
</span><span style="color: #800000; background-color: #f0f0f0;">48 </span><span style="background-color: #ffffff;">}</span></pre>
<p>&#0160;&#0160;&#0160;&#0160;</p>
<p>&#0160;&#0160;&#0160;&#0160;The rest of the code is pretty much self-explanatory so you can refer to the sample below. The full source code of the extension is part of my extensions library at <a href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/src/Viewing.Extension.Transform" target="_blank">Viewing.Extension.Transform</a>. Like most of my latest samples, the extension requires a build step with webpack, sorry for people who prefer to deal with straigh ES5 JavaScript code to include in their html but we are in 2016 now :)</p>
<p><a href="https://react-gallery.autodesk.io/embed?id=57609f6b177a241809da305e&amp;extIds=Viewing.Extension.Transform" target="_blank">Here</a> is a live demo where you can play with those transform tools.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c87a837f970b-pi" style="display: inline;"><img alt="Screen Shot 2016-07-12 at 16.04.29" class="asset  asset-image at-xid-6a0167607c2431970b01b7c87a837f970b img-responsive" src="/assets/image_3ccbfb.jpg" title="Screen Shot 2016-07-12 at 16.04.29" /></a></p>
<script src="https://gist.github.com/leefsmp/286ee567c4376bbecaa96f69e61506f1.js"></script>
