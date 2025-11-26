---
layout: "post"
title: "Maya Fluid Shader"
date: "2012-08-02 01:11:50"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Dynamics"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/maya-fluid-shader.html "
typepad_basename: "maya-fluid-shader"
typepad_status: "Publish"
---

<p>Today, we will talk about the dedicated Maya fluid shader workflow. Usually people who has their own fluid shader wants to mimic the Maya one, so we review in details what one needs to do.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017743d9f80a970d-pi" style="display: inline;"><img alt="Smoke" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017743d9f80a970d" src="/assets/image_9d5034.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Smoke" /></a><br /><br /></p>
<p><strong><span style="text-decoration: underline;">The general flow of the fluid shading computation is:</span></strong></p>
<ol>
<li>Compute the desired grid values for a point within the volume           
<ul>
<li>for example density or temperature</li>
<li>the value at the point is determined by the grid values interpolated using the desired interpolation method (described in the attachment )&#0160;</li>
</ul>
</li>
<li>Multiply the resulting value by the scale for that value, for example densityScale.&#0160;</li>
<li>Modulate the value based on any texturing for that attribute (for example opacity texture)&#0160;</li>
<li>Clip to result 0-1&#0160;</li>
<li>Apply the bias to the resulting value... the bias shifts the range 0-1 so that the values tend to be pushed more towards 1 or zero depending on the bias value.&#0160;</li>
<li>Use the result to look up the ramp value.</li>
</ol>
<p><strong>Find here some pseudo code which describes the overall fluids chain for shading:</strong></p>
<pre class="brush: python; toolbar: false;"># For a given point in space get the interpolated grid or
# gradient value (which grid is determined by attributes 
# such as opacityInput)
# If renderInterpolator is smooth then quadratic interpolation
# is used otherwise it linearly interpolates grid values.
# For example the opacity grid times the opacity scale value)

inputValue =interpolatedGridValue * gridValueScale

# Apply the built in noise texturing (they would need to 
# determine how to best simulate our noise methods if they 
# want to match those)
# Note that if one is getting an opacity value then the 
# texture gain would be opacityTexGain.

inputValue *=1 - textureGain + textureGain * noise_factor
# Apply the bias
if ( inputBias != 0.0 )
  if ( inputBias &lt; -0.99 )
    inputBias =-0.99 # avoid divide by zero
  float pval =(inputBias - 1) / (-inputBias - 1)
  if ( inputValue &lt; 0 )
    inputValue =0 # protect power function from negative
  inputValue =powf (inputValue, pval)
</pre>
<p><strong><span style="text-decoration: underline;">Bias computation: <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017743d9f425970d-pi" style="display: inline;"><img alt="Bias" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017743d9f425970d" src="/assets/image_4b37e3.jpg" title="Bias" width="150" /></a><br /></span></strong></p>
<p>The bias is applied to the value after all the other calculations (not simply added in). It basically shifts where the center (0.5) value will be while leaving 0 and 1 the same. Generally for users it is important that their grid*scale value ends up with values between 0-1, as values outside this range simply get clipped. Then we apply the bias to the clipped 0-1 values to adjust the distribution (avoids needing ramps with all indices near 0 or 1). The setup is as follows:</p>
<pre class="brush: python; toolbar: false;">Ramp Indice =bias (textureFunction (inputGrid * gridScale))
</pre>
<p>Here is the math to apply the bias. InputVal is the grid value after scaling and texturing.</p>
<pre class="brush: python; toolbar: false;">float pval =(inputBias - 1.0) / (-inputBias - 1.0)
if ( pval &lt; 0)
  indice =0
else
  indice =powf (inputVal, pval)
</pre>
<p><strong><span style="text-decoration: underline;">The Density values remapping for rendering:</span></strong></p>
<p>The default settings map a 0-2 density range to 0-1 opacity values on the fluid (the densityScale defaults to 0.5, a value of 1 would map 1-1 density and opacity), which is then scaled by the transparency (which is a 3 channel rgb transparency).</p>
<p>(Other renderers might instead map 0-inf grid densities to 0-1 opacity)</p>
<p><strong><span style="text-decoration: underline;">Implementation of texture type:</span></strong></p>
<p>It contains perlin noise, billow, volume wave, wispy, spacetime. Except for perlin noise, how another noise types were computed?</p>
<p>Here is an overview:</p>
<p><strong>Perlin noise</strong>: standard noise, although animating time will offset the different iteration frequencies in different directions. <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017743d9fb0d970d-pi" style="float: right;"><img alt="Perlin_noise" class="asset  asset-image at-xid-6a0163057a21c8970d017743d9fb0d970d" src="/assets/image_0d9159.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Perlin_noise" /></a></p>
<p><strong>SpaceTime</strong>: perlin noise, but with a added dimension that is mapped to the time parameter (this has the time animation with the least bias)</p>
<p><strong>Wispy</strong>: perlin noise where the texture coordinates are offset by a second perlin noise. Time animation animates the offset of the different frequencies in the texture offset noise.</p>
<p><strong>Volume wave</strong>: each frequency is a cluster of sine waves along random vectors. Time offsets the input coordinate to the sin function.</p>
<p><strong>Billow</strong>: this is a grid where each grid contains a point with a radius. The point position within the grid cell is randomized and the radius randomized. The time animation involves orbiting each point about the center of the voxel.</p>
<p>Maya 2D noise texture uses the same underlying code.</p>
<p><strong><span style="text-decoration: underline;">The formula for texGain is:</span></strong>&#0160;</p>
<pre class="brush: python; toolbar: false;">val =inputgrid * gridScale
if ( gain &lt; 1 )
  val *=(1 - gain + gain * textureValue)
else
  val *=gain * textureValue
</pre>
<p><strong>If texture coordinate is fixed, the texture input P is just point of each center position of voxel in object space? How does coordinate speed affect P.&#0160;</strong>&#0160;</p>
<p>Coordinates are scaled relative to the size attribute on the fluid. I believe zero is the center of the fluid. Coordinate speed is only when the coordinates are defined as a dynamic simulating grid, in which case the simulation advection scales the grid velocity by the coordinate speed. (In terms of shaders, you simply need to export the coordinate grid and use that if it is dynamic)&#0160;</p>
<p><strong>In noise parameter: It contains ratio, threshold, depth max, frequency ratio. I have known that the meaning of these parameters, but how the frequency ratio be computed to affect ratio?</strong>&#0160;</p>
<p>The frequency ratio determines the change in space (coordinate) scaling for each iteration of the noise, while the ratio scales the magnitude of the noise each iteration.&#0160; They do not affect each other directly and in general if one lowers the frequency ratio one needs to increase the ratio (as well as max iterations) to preserve the character of the noise.</p>
<p><strong><span style="text-decoration: underline;">COMPUTING SPEED input for fluid ramps:</span></strong></p>
<p>Starting with the raw grid velocity the formula to compute the ramp lookup index:</p>
<pre class="brush: python; toolbar: false;">inputVal =1.0 - 1.0 / (1.0 + magnitude (velocity * velocityScale) * 0.1)
</pre>
<p>The inputVal then get modified by the bias function.</p>
<p><strong><span style="text-decoration: underline;">Computing EdgeDropoff:</span></strong></p>
<p>The edge dropoff affects the transparency used for the final fluid render (as opposed to the opacity input ). The dropoff can optionally be a grid.&#0160;&#0160;</p>
<p><strong>In the grid case:</strong></p>
<pre class="brush: python; toolbar: false;">falloff =interpolatedFalloffGridValue (point)
if ( edgeDropoff &gt; 0.9999 )
  opacityMult =0
else
  opacityMult =powf (falloff, edgeDropoff / (1 - edgeDropoff))
</pre>
<p><strong>in the linear gradient case(xyz axis):</strong></p>
<pre class="brush: python; toolbar: false;"># Determine a distance from the center to the boundary of the 
# dropoff shape (cube, sphere, cone) or the simple linear gradient 
# along a particular axis:
dist =gradientDistance # this is the 0-1 distance along an axis
if( edgeDropoff&#0160; &gt; 0.9999 )
  opacityMult =0
else
  opacityMult =powf (dist, edgeDropoff / (1 - edgeDropoff))
</pre>
<p><strong>in the shape case (cube, sphere, cone)</strong></p>
<pre class="brush: python; toolbar: false;">dist =distanceFromCenter (point, dropoffShape) # this is a 0-1 distance from center to boundary of shape
dist =(1 - dist) / edgeDropoff
opacityMult =float(sin ((dist - 0.5) * PI) * 0.5 + 0.5)
</pre>
<p>The cube case is a little more complex than what is described above… basically we&#0160; do the dropoff defined above( with the sin function) for each axis then the resulting density mult is the factor for each of the axis values multiplied together.</p>
<p><strong><span style="text-decoration: underline;">How the ramps are indexed:</span></strong></p>
<p>u is the interpolant between the indices, ranging between 0-1. L1 and L2 are the luminosity of the two indices being interpolated.&#0160;</p>
<p><strong>exponential up:</strong></p>
<pre class="brush: python; toolbar: false;">u =u * u
</pre>
<p><strong>exponential down:</strong></p>
<pre class="brush: python; toolbar: false;">u =1 - (1 - u * u)
</pre>
<p><strong>smooth:</strong></p>
<pre class="brush: python; toolbar: false;">u =(cos ((u + 1) * PI) + 1) * 0.5
</pre>
<p><strong>bump:</strong></p>
<pre class="brush: python; toolbar: false;">if ( l1 &gt; l2 )
  u =sin (u * PI / 2)
else
  u =sin ((u - 1) * PI / 2) + 1
</pre>
<p><strong>spike:</strong></p>
<pre class="brush: python; toolbar: false;">if ( l1 &lt; l2 )
  u =sin (u * PI / 2)
else
  u =sin ((u - 1) * PI / 2 ) + 1
</pre>
<p><strong><span style="text-decoration: underline;">How Inflection is computed:</span></strong></p>
<pre class="brush: python; toolbar: false;">if ( inflection )
  if ( nval &gt; 0 )
    nval =nval * 2 - 1
  else
    nval =-nval * 2 - 1

</pre>
<p>Where nval is noise value.</p>
