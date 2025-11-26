---
layout: "post"
title: "Revit model viewer for iOS - part 3"
date: "2012-06-28 17:56:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Cloud"
  - "Mobile"
original_url: "https://adndevblog.typepad.com/aec/2012/06/revit-model-viewer-for-ios-part-3.html "
typepad_basename: "revit-model-viewer-for-ios-part-3"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is a continuation of the previous post&#0160;<a href="http://adndevblog.typepad.com/aec/2012/06/revit-model-viewer-for-ios-part-2.html" target="_blank" title="Revit model viewer for iOS - part 2">Revit model viewer for iOS - part 2</a></p>
<p><strong>The iOS App part - view manipulation</strong></p>
<p>We&#39;ll now make the application more interactive by responding to gestures to modify the view orientation. I&#39;ve seen samples where the rotation, translation, etc were stored separately and then used to modify the view or called OpenGL functions directly to modify the view orientation. Since the GLK effect object (in our case GLKBaseEffect) already keeps track of the transformation matrix, I tought I would modify that directly.</p>
<p>I was a bit confused about the transformation matrix at first as I haven&#39;t used such things for a while, and since everyone understands things faster in different ways, I created my own drawing to help me with this.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017615a9c17a970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="OpenGL CS" class="asset  asset-image at-xid-6a0167607c2431970b017615a9c17a970c" src="/assets/image_453024.jpg" title="OpenGL CS" /></a>&#0160;</p>
<p>Note: the area behind the UIToolbar that is placed at the bottom of the screen is actually part of the GLKView no matter if the toolbar is transparent or not. That&#39;s why the house does not seem to be vertically in the middle if you ignore the toolbar area.</p>
<p>The&#0160;modelviewMatrix describes the model axes (X, Y, Z) and translation to the model origin (T) using the OpenGL coordinate system (x, y, z):</p>
<table border="0" style="height: 200px; width: 200px;">
<tbody>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">&#0160;</td>
<td align="center" style="border: 1px solid black;" valign="middle">x</td>
<td align="center" style="border: 1px solid black;" valign="middle">y</td>
<td align="center" style="border: 1px solid black;" valign="middle">z</td>
<td align="center" style="border: 1px solid black;" valign="middle">t</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">X</td>
<td align="center" style="border: 1px solid black;" valign="middle">m00</td>
<td align="center" style="border: 1px solid black;" valign="middle">m01</td>
<td align="center" style="border: 1px solid black;" valign="middle">m02</td>
<td align="center" style="border: 1px solid black;" valign="middle">m03</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">Y</td>
<td align="center" style="border: 1px solid black;" valign="middle">m10</td>
<td align="center" style="border: 1px solid black;" valign="middle">m11</td>
<td align="center" style="border: 1px solid black;" valign="middle">m12</td>
<td align="center" style="border: 1px solid black;" valign="middle">m13</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">Z</td>
<td align="center" style="border: 1px solid black;" valign="middle">m20</td>
<td align="center" style="border: 1px solid black;" valign="middle">m21</td>
<td align="center" style="border: 1px solid black;" valign="middle">m22</td>
<td align="center" style="border: 1px solid black;" valign="middle">m23</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">T</td>
<td align="center" style="border: 1px solid black;" valign="middle">m30</td>
<td align="center" style="border: 1px solid black;" valign="middle">m31</td>
<td align="center" style="border: 1px solid black;" valign="middle">m32</td>
<td align="center" style="border: 1px solid black;" valign="middle">m33</td>
</tr>
</tbody>
</table>
<p>The above house model&#39;s origin is at the bottom left corner of the front of the house, the X is pointing from the left to the right, the Y is pointing towards the back of the house and Z is pointing upwards. If the house&#39;s width is 12, depth is 8 and height is 10, then the center point will be at (6, 4, 5). When you are using the&#0160;GLKMatrix4MakeLookAt() function to set the model view matrix in a way that you&#39;ll be looking at the center of the house from e.g. a distance of 20 ...</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;"><span style="color: #008423;">// All below coordinates are defined in </span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;"><span style="color: #008423;">// Model Coordinate S</span><span style="color: #008423;">ystem (X, Y, Z)</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;"><span style="color: #008423;"><br /></span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;"><span style="color: #733ea4;">GLKVector3</span> center = <span style="color: #401f7d;">GLKVector3Make</span>(<span style="color: #2f2fd1;">6</span>, <span style="color: #2f2fd1;">4</span>, <span style="color: #2f2fd1;">5</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;"><span style="color: #733ea4;">GLKVector3</span> centerToEye = <span style="color: #401f7d;">GLKVector3Make</span>(<span style="color: #2f2fd1;">0</span>, -<span style="color: #4d8186;">20</span>, <span style="color: #2f2fd1;">0</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;"><span style="color: #733ea4;">GLKVector3</span> eye = <span style="color: #401f7d;">GLKVector3Add</span>(center, centerToEye);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #4d8186;">baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;"> =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;">&#0160; GLKMatrix4MakeLookAt<span style="color: #000000;">(</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; eye.<span style="color: #733ea4;">x</span>, eye.<span style="color: #733ea4;">y</span>, eye.<span style="color: #733ea4;">z</span>,</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; center.<span style="color: #733ea4;">x</span>, center.<span style="color: #733ea4;">y</span>, center.<span style="color: #733ea4;">z</span>,</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160;&#0160;</span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, </span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, </span><span style="color: #2f2fd1;">1</span><span style="color: #000000;"> </span>/* upVector = OpenGL y axis = Model Z axis */<span style="color: #000000;">); &#0160;</span></p>
<p>... then you&#39;ll get a matrix like this:</p>
<table border="0" style="height: 200px; width: 200px;">
<tbody>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">&#0160;</td>
<td align="center" style="border: 1px solid black;" valign="middle">x</td>
<td align="center" style="border: 1px solid black;" valign="middle">y</td>
<td align="center" style="border: 1px solid black;" valign="middle">z</td>
<td align="center" style="border: 1px solid black;" valign="middle">t</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">X</td>
<td align="center" style="border: 1px solid black;" valign="middle">1</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">Y</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
<td align="center" style="border: 1px solid black;" valign="middle">-1</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">Z</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
<td align="center" style="border: 1px solid black;" valign="middle">1</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
<td align="center" style="border: 1px solid black;" valign="middle">0</td>
</tr>
<tr>
<td align="center" style="border: 1px solid black;" valign="middle">T</td>
<td align="center" style="border: 1px solid black;" valign="middle">-6</td>
<td align="center" style="border: 1px solid black;" valign="middle">-5</td>
<td align="center" style="border: 1px solid black;" valign="middle">-16</td>
<td align="center" style="border: 1px solid black;" valign="middle">1</td>
</tr>
</tbody>
</table>
<p>You get -16 for the z part of T because the translation vector only points at the model origin and does not go all the way back to the model center (-16 = -20 + (depth / 2)).</p>
<p>When you want to rotate around the center of the model, then you have to move/translate the model view matrix from the center position to the eye position, do the rotation, and then move the matrix back. In our case the distance between the eye and model center position is held by the &#39;distance&#39; global variable:<span style="font-family: Menlo; font-size: 11px;">&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)rotateMatrix:(<span style="color: #be299d;">float</span> *)rotX&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rotY:(<span style="color: #be299d;">float</span> *)rotY&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rotZ:(<span style="color: #be299d;">float</span> *)rotZ</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>GLKMatrix4<span style="color: #000000;"> mx = </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px; color: #008423;">&#0160; // In this function we are providing the coordinates in</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px; color: #008423;">&#0160; // OpenGL coordinate system (x, y, z)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px; color: #008423;">&#0160; // So the below&#0160;toOrigin translation is acting along the z axis,</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px; color: #008423;">&#0160; // which&#0160;is perpendicular to the screen</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px; color: #008423;">&#0160; // Here &#39;toOrigin&#39; means translation to the origin of the</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px; color: #008423;">&#0160; // OpenGL coordinate system (screen/view center)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #733ea4;">GLKMatrix4</span><span style="color: #000000;"> toOrigin = </span>GLKMatrix4MakeTranslation<span style="color: #000000;">(</span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, </span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, </span><span style="color: #4d8186;">distance</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; mx = <span style="color: #401f7d;">GLKMatrix4Multiply</span>(toOrigin, mx);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">if</span> (rotX != <span style="color: #be299d;">nil</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; mx = </span>GLKMatrix4Multiply<span style="color: #000000;">(</span>GLKMatrix4MakeXRotation<span style="color: #000000;">(*rotX), mx);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">if</span> (rotY != <span style="color: #be299d;">nil</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; mx = </span>GLKMatrix4Multiply<span style="color: #000000;">(</span>GLKMatrix4MakeYRotation<span style="color: #000000;">(*rotY), mx);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">if</span> (rotZ != <span style="color: #be299d;">nil</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; mx = </span>GLKMatrix4Multiply<span style="color: #000000;">(</span>GLKMatrix4MakeZRotation<span style="color: #000000;">(*rotZ), mx);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #733ea4;">GLKMatrix4</span><span style="color: #000000;"> fromOrigin = </span>GLKMatrix4MakeTranslation<span style="color: #000000;">(</span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, </span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, -</span><span style="color: #4d8186;">distance</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; mx = <span style="color: #401f7d;">GLKMatrix4Multiply</span>(fromOrigin, mx);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;"> = mx;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p>Here are the other transformation functions:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)panMatrix:(<span style="color: #be299d;">float</span>)x y:(<span style="color: #be299d;">float</span>)y&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>GLKMatrix4<span style="color: #000000;"> mx = </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>GLKMatrix4<span style="color: #000000;"> pan =&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; </span>GLKMatrix4MakeTranslation<span style="color: #000000;">(</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; x * <span style="color: #4d8186;">distance</span> * <span style="color: #2f2fd1;">.005</span>, y * <span style="color: #4d8186;">distance</span> * <span style="color: #2f2fd1;">.005</span>, <span style="color: #2f2fd1;">0</span>);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; mx = </span>GLKMatrix4Multiply<span style="color: #000000;">(pan, mx);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;"> = mx;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">- (<span style="color: #be299d;">void</span>)scaleMatrix:(<span style="color: #be299d;">float</span>)scale&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span>GLKMatrix4<span style="color: #000000;"> mx = </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;">;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">if</span> (scale &lt; <span style="color: #2f2fd1;">1</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; scale = -<span style="color: #2f2fd1;">1</span> / scale;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; </span>// We just move along the view Z to show scale like effect</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #733ea4;">GLKMatrix4</span> scaler =&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; &#0160; </span>GLKMatrix4MakeTranslation<span style="color: #000000;">(</span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, </span><span style="color: #2f2fd1;">0</span><span style="color: #000000;">, scale * </span><span style="color: #4d8186;">distance</span><span style="color: #000000;"> * </span><span style="color: #2f2fd1;">.03</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; mx = <span style="color: #401f7d;">GLKMatrix4Multiply</span>(scaler, mx);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">&#0160; </span><span style="color: #4d8186;">_baseEffect</span><span style="color: #000000;">.</span>transform<span style="color: #000000;">.</span>modelviewMatrix<span style="color: #000000;"> = mx;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">}</span></p>
<p>Note: the .005 and .03 values used above are just experimental and not based on any calculation.</p>
<p>We need to catch the gestures in order to respond to them. You can just go into the storyboard editor and drag&amp;drop gestures onto the GLKView, then set up the sent actions for the control that can be handled by our functions. Inside these functions we can use the previously created view matrix manipulation functions:</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">- (</span><span style="color: #be299d;">IBAction</span><span style="color: #000000;">)onPinch:(</span>UIPinchGestureRecognizer<span style="color: #000000;"> *)sender</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">static</span> <span style="color: #be299d;">double</span> _prevScale;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">if</span><span style="color: #000000;"> ([sender </span>state<span style="color: #000000;">] == </span>UIGestureRecognizerStateBegan<span style="color: #000000;">)</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _prevScale = <span style="color: #2f2fd1;">1</span>;&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">else</span><span style="color: #000000;"> </span><span style="color: #be299d;">if</span><span style="color: #000000;"> ([sender </span>state<span style="color: #000000;">] == </span>UIGestureRecognizerStateChanged<span style="color: #000000;">)</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; [<span style="color: #be299d;">self</span> <span style="color: #30595d;">scaleMatrix</span>:(sender.<span style="color: #733ea4;">scale</span> / _prevScale)];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _prevScale = sender.<span style="color: #733ea4;">scale</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #d42722;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #401f7d;">NSLog</span><span style="color: #000000;">(</span>@&quot;onPinch with scale %f&quot;<span style="color: #000000;">, sender.</span><span style="color: #733ea4;">scale</span><span style="color: #000000;">);</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>updateGLView<span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; } &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">- (</span><span style="color: #be299d;">IBAction</span><span style="color: #000000;">)onPan:(</span>UIPanGestureRecognizer<span style="color: #000000;"> *)sender&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">static</span> <span style="color: #733ea4;">CGPoint</span> _prevPoint;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">static</span> <span style="color: #733ea4;">NSUInteger</span> _numOfTouches = <span style="color: #2f2fd1;">0</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">if</span><span style="color: #000000;"> ([sender </span>state<span style="color: #000000;">] == </span>UIGestureRecognizerStateBegan<span style="color: #000000;">)</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _prevPoint = [sender <span style="color: #401f7d;">locationInView</span>:<span style="color: #be299d;">self</span>.<span style="color: #733ea4;">view</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// Number of touches can change, so better stick to the one that</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// is started</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _numOfTouches = sender.<span style="color: #733ea4;">numberOfTouches</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">else</span><span style="color: #000000;"> </span><span style="color: #be299d;">if</span><span style="color: #000000;"> ([sender </span>state<span style="color: #000000;">] == </span>UIGestureRecognizerStateChanged<span style="color: #000000;"> &amp;&amp;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; _numOfTouches == sender.<span style="color: #733ea4;">numberOfTouches</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #733ea4;">CGPoint</span> pt = [sender <span style="color: #401f7d;">locationInView</span>:<span style="color: #be299d;">self</span>.<span style="color: #733ea4;">view</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// Two touches is panning</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #be299d;">if</span> (_numOfTouches == <span style="color: #2f2fd1;">2</span>)</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; [<span style="color: #be299d;">self</span> <span style="color: #30595d;">panMatrix</span>:(pt.<span style="color: #733ea4;">x</span> - _prevPoint.<span style="color: #733ea4;">x</span>) <span style="color: #30595d;">y</span>:-(pt.<span style="color: #733ea4;">y</span> - _prevPoint.<span style="color: #733ea4;">y</span>)];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #008423;"><span style="color: #000000;">&#0160; &#0160; </span>// One touch is rotation</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #be299d;">else</span>&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; <span style="color: #be299d;">float</span> rotX = (pt.<span style="color: #733ea4;">y</span> - _prevPoint.<span style="color: #733ea4;">y</span>) / <span style="color: #2f2fd1;">180</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; <span style="color: #be299d;">float</span> rotY = (pt.<span style="color: #733ea4;">x</span> - _prevPoint.<span style="color: #733ea4;">x</span>) / <span style="color: #2f2fd1;">180</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; &#0160; [<span style="color: #be299d;">self</span> <span style="color: #30595d;">rotateMatrix</span>:&amp;rotX <span style="color: #30595d;">rotY</span>:&amp;rotY <span style="color: #30595d;">rotZ</span>:<span style="color: #be299d;">nil</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _prevPoint = pt;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>updateGLView<span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #d42722;"><span style="color: #000000;">&#0160; </span><span style="color: #401f7d;">NSLog</span><span style="color: #000000;">(</span>@&quot;onPan with state %d and number %d&quot;<span style="color: #000000;">, [sender </span><span style="color: #401f7d;">state</span><span style="color: #000000;">],&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; [sender <span style="color: #401f7d;">numberOfTouches</span>]); &#0160; &#0160; &#0160; &#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #733ea4;"><span style="color: #000000;">- (</span><span style="color: #be299d;">IBAction</span><span style="color: #000000;">)onRotate:(</span>UIRotationGestureRecognizer<span style="color: #000000;"> *)sender&#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">{</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; <span style="color: #be299d;">static</span> <span style="color: #be299d;">double</span> _prevRotation;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">if</span><span style="color: #000000;"> ([sender </span>state<span style="color: #000000;">] == </span>UIGestureRecognizerStateBegan<span style="color: #000000;">)</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _prevRotation = <span style="color: #2f2fd1;">0</span>;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #401f7d;"><span style="color: #000000;">&#0160; </span><span style="color: #be299d;">else</span><span style="color: #000000;"> </span><span style="color: #be299d;">if</span><span style="color: #000000;"> ([sender </span>state<span style="color: #000000;">] == </span>UIGestureRecognizerStateChanged<span style="color: #000000;">)</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; {</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; <span style="color: #be299d;">float</span> rotZ = -([sender <span style="color: #401f7d;">rotation</span>] - _prevRotation);</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>rotateMatrix<span style="color: #000000;">:</span><span style="color: #be299d;">nil</span><span style="color: #000000;"> </span>rotY<span style="color: #000000;">:</span><span style="color: #be299d;">nil</span><span style="color: #000000;"> </span>rotZ<span style="color: #000000;">:&amp;rotZ];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; &#0160; _prevRotation = [sender <span style="color: #401f7d;">rotation</span>];</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #d42722;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #401f7d;">NSLog</span><span style="color: #000000;">(</span>@&quot;onRotate with angle %f&quot;<span style="color: #000000;">, rotZ); &#0160; &#0160; &#0160; &#0160;</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; min-height: 13.0px;">&#0160;</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo; color: #30595d;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #be299d;">self</span><span style="color: #000000;"> </span>updateGLView<span style="color: #000000;">];</span></p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">&#0160; }</p>
<p style="margin: 0.0px 0.0px 0.0px 0.0px; font: 11.0px Menlo;">}<span style="font-family: Arial, sans-serif; font-size: small;">&#0160;</span></p>
<p>I hope you found these posts useful. Here is the finished project:&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b0177428fdd89970d"><a href="http://adndevblog.typepad.com/files/revitmodelviewer---ios-app---2012-06-20.zip">Download RevitModelViewer - iOS App - 2012-06-20</a></span></p>
<p>Note: this is just a sample and not a fine tuned project ready to be used in production ;)</p>
