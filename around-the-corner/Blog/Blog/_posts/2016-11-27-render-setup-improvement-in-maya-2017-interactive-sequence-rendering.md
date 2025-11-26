---
layout: "post"
title: "Render Setup improvement in Maya 2017: Interactive Sequence Rendering"
date: "2016-11-27 20:01:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "Maya"
  - "Plug-in"
  - "Python"
  - "Rendering"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/render-setup-improvement-in-maya-2017-interactive-sequence-rendering.html "
typepad_basename: "render-setup-improvement-in-maya-2017-interactive-sequence-rendering"
typepad_status: "Publish"
---

<p>The last main improvement for Render Setup feature is actually a new feature to render a sequence directly from Maya’s UI without executing a batch render (including support for loading the sequence in the Renderview for review), thanks to engineering team’s contribution, here is the detail if you are interested:</p>  <h3><font style="font-weight: bold" size="3">Introduction</font></h3>  <p>Rendering of image sequences in Maya has traditionally been an offline process for the end-user via command-line or the batch render feature in Maya. This can be limiting for artists attempting to generate sequences locally, and imposed additional workflow to review and evaluate renders over time. </p>  <p>Interactive sequence rendering is a new rendering mode introduced in Maya 2017 that allows rendering of a sequence of frames into the Render View. It’s a mix between Batch rendering and ordinary Render View rendering. Like Batch rendering it will iterate over render layers, cameras and frames and save the results to disc, but like Render View rendering it’s done interactively, displaying the progress in the Render View. There is also an option to keep each frame in the Render View to allow scrolling through and review the animation results afterwards.</p>  <p>This blog describes how to add support for this feature to a 3<sup>rd</sup> party renderer.</p>  <h3><font style="font-weight: bold" size="3">Sequence Rendering Procedure </font></h3>  <p>To add support for sequence rendering a procedure for this needs to be created and registered with Maya. This is a wrapper procedure written in MEL or Python, and it should in turn call the plugin render command to start the rendering. The following signature should be used for the procedure:</p>  <pre class="brush:python;toolbar: false;">int mySequenceRenderProc(int width, int height, string camera,
string saveToRenderView);</pre>

<ul>
  <li><i>width </i>- The width of the images produced. </li>

  <li><i>height</i> - The height of the images produced. </li>

  <li><i>camera -</i> Sets the name of a specific camera to render. If set to empty string the renderer should iterate over all cameras that have been set as renderable. </li>

  <li><i>saveToRenderView -</i> Sets the name of a camera for which the rendered images should be saved to the Render View. If set to empty string no images should be saved. If set to “all” images for all cameras should be saved. See below on how to save images to the render view. </li>
</ul>

<p>The options sent in are set by Maya depending on user preferences. These options, and any custom options needed by the renderer, should be sent to the render command when it’s called from inside this wrapper procedure.</p>

<p>Maya will call this procedure to start the rendering for each render layer that has been selected for rendering. Maya will also handle execution of Pre/Post Render MEL scripts and Pre/Post Render Layer MEL scripts. The plugin render command should handle iteration over frames and cameras and also execute Pre/Post Render Frame scripts. </p>

<p>The frame range can be read from the common render globals:</p>

<pre class="brush:python;toolbar: false;">MCommonRenderSettingsData renderGlobals;
MRenderUtil::getCommonRenderSettings(renderGlobals);

renderGlobals.frameStart
renderGlobals.frameEnd
renderGlobals.frameBy</pre>

<p>Region rendering is supported. If this is requested by the user Maya will set the attribute defaultRenderGlobals.useRenderRegion to true.</p>

<p>Like with ordinary Render View rendering the execution should abort if the user presses the Esc key. If this happens the wrapper procedure should return a non-zero value to notify the render layer iteration on Maya side to abort.</p>

<h3><font style="font-weight: bold" size="3">Registration</font></h3>

<p>The wrapper procedure is registered like all other renderer procedures using the MEL/Python command <i>“renderer”</i>. A new flag “-<i>renderSequenceProcedure</i>” has been added for this purpose.</p>

<p><b>Example - Register a new renderer supporting single frame rendering and sequence rendering:</b></p>

<pre class="brush:python;toolbar: false;">renderer	
	-rendererUIName &quot;My Renderer&quot;
	-renderProcedure &quot;myRenderProc&quot; 	
	-renderSequenceProcedure &quot;mySequenceRenderProc&quot;
	myRenderer;</pre>

<p><b>Example – Add sequence rendering support to an already registered renderer:</b></p>

<pre class="brush:python;toolbar: false;">renderer 
	-edit 
	-renderSequenceProcedure &quot;mySequenceRenderProc&quot;
	myRenderer;</pre>

<h3><font style="font-weight: bold" size="3">Save to Render View</font></h3>

<p>The following MEL code can be used by the plugin command to save the result in the Render View.</p>

<pre class="brush:python;toolbar: false;">string $allPanels[0] = `getPanel -scriptType renderWindowPanel`;
if (size($allPanels) &gt; 0)
{
    renderWindowMenuCommand &quot;keepImageInRenderView&quot; $allPanels[0];
}</pre>
