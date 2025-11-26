---
layout: "post"
title: "Convert Maya World Coordinates to Screen Coordinates"
date: "2012-08-15 00:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/convert-maya-world-coordinates-to-screen-coordinates.html "
typepad_basename: "convert-maya-world-coordinates-to-screen-coordinates"
typepad_status: "Publish"
---

<p>Usually you can use M3dView::worldToView() to convert world coordinates to screen space coordinates, however there may be situations where you need to convert them manually, for example, you want to know what the screen coordinates would be even if they are outside the viewport. In this case, you need to convert them manually, vis using the M3dView class method.</p>
<p>1. Let’s say we have the world space point “vertex_corner”, you need to multiply it by the modelView and projection matrices,</p>
<pre class="brush: cpp; toolbar: false;">MPoint corner = vertext_corner * (modelViewMatrix * projectionMatrix);
</pre>
<p>you ends up with 'corner', which is a point in homogenous coordinates</p>
<p>2. Get the size of the viewport</p>
<pre class="brush: cpp; toolbar: false;">unsigned int viewport_x, viewport_y, viewport_width, viewport_height;
maya_view.viewport(viewport_x, viewport_y, viewport_width, viewport_height);
</pre>
<p>3. In a homogenous coordinate system there is an additional coordinate, called 'w', by which all of the other coordinates have been multiplied. <a href="http://en.wikipedia.org/wiki/Homogeneous_coordinates" target="_self">Homogenous coordinates</a> which are multiples of each other all refer to the same point in space. So (-1.5, 0.6, 0.0 3.0) is the same point in space as (-0.5, 0.2, 0.0, 1.0)</p>
<p>The first thing you need to do is to divide the x and y coords by w to get them into normalized form:</p>
<pre class="brush: cpp; toolbar: false;">corner.x / corner.w;  (-1.5 / 3.0 -&gt; -0.5)
corner.y / corner.w;  ( 0.6 / 3.0 -&gt;&nbsp; 0.2)
</pre>
<p>That leaves you with a point in which x ranges from -1.0 to +1.0 across the width of the viewport, and y ranges from -1.0 to +1.0 across its height.</p>
<p>You wants values which range from (0, 0) to (viewWidth, viewHeight), so the first thing you need to do is to get rid of the negative values by offsetting the coordinates by 1.0 so that they range from 0.0 to 2.0:</p>
<pre class="brush: cpp; toolbar: false;">
corner.x/corner.w + 1.0;  (-0.5 + 1.0 -&gt; 0.5)
corner.y/corner.w + 1.0;  ( 0.2 + 1.0 -&gt; 1.2)
</pre>
<p>Then you divide them by 2 so that they range from 0.0 to 1.0:</p>
<pre class="brush: cpp; toolbar: false;">
(corner.x / corner.w + 1.0) / 2.0;  (0.5 / 2.0 -&gt; 0.25)
(corner.y / corner.w + 1.0) / 2.0;  (1.2 / 2.0 -&gt; 0.60)
</pre>
<p>Finally, you multiplies the x coord by the width and the y coord by the height to get the final pixel positions:</p>
<pre class="brush: cpp; toolbar: false;">
(viewport_width)(corner.x / corner.w + 1.0) / 2.0;  (0.25 * 1024 -&gt; 256)
(viewport_height)(corner.y / corner.w + 1.0) / 2.0; (0.60 * 768&nbsp; -&gt; 460)
</pre>
<p>The final formula is something like this:</p>
<pre class="brush: cpp; toolbar: false;">
CPoint pointWin;
pointWin.x =static_cast&lt;int&gt;(static_cast&lt;double&gt;(viewport_width) * (corner.x / corner.w + 1.0) / 2.0);
pointWin.y =static_cast&lt;int&gt;(static_cast&lt;double&gt;(viewport_height) * (corner.y / corner.w + 1.0) / 2.0);
</pre>
