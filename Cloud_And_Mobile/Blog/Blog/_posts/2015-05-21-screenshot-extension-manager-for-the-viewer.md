---
layout: "post"
title: "Screenshot Extension Manager for the viewer"
date: "2015-05-21 05:48:13"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/screenshot-extension-manager-for-the-viewer.html "
typepad_basename: "screenshot-extension-manager-for-the-viewer"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>This week post illustrates the concept I exposed in my previous post about&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/05/building-self-contained-ui-components-for-the-viewer.html" target="_self">building self-contained UI components for the viewer</a>.&nbsp;</p>
<p>I built a concrete example of such a component making use of the screenshot feature introduced recently in the View &amp; Data API. The <strong><em>viewer.getScreenShot</em></strong> method allows you to programmatically get a screenshot of the current model with specified dimensions and retrieve it as a <a href="https://developer.mozilla.org/en/docs/Web/API/Blob" target="_self">blob</a>.</p>
<p>Here is the method prototype:</p>



<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/**
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * Captures the current screen image as Blob URL
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * Blob URL can be used like a regular image url
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * (e.g., window.open, img.src, etc)
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * If no parameters are given, returns an image as
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * Blob URL, with dimensions equal to current canvas dimensions
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * If width and height are given,
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * returns asynchronously and calls the callback
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * with the resized image as Blob URL
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * If no callback is given, displays the image in a new window
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * </span><span style="text-decoration:underline;color:#808080"><span style="color:#808080;background-color:#ffffff;font-weight:bold;font-style:italic;">@param</span></span><span style="color:#808080;background-color:#ffffff;font-style:italic;">  {int}      [w]  width of the requested image
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * </span><span style="text-decoration:underline;color:#808080"><span style="color:#808080;background-color:#ffffff;font-weight:bold;font-style:italic;">@param</span></span><span style="color:#808080;background-color:#ffffff;font-style:italic;">  {int}      [h]  height of the requested image
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * </span><span style="text-decoration:underline;color:#808080"><span style="color:#808080;background-color:#ffffff;font-weight:bold;font-style:italic;">@param</span></span><span style="color:#808080;background-color:#ffffff;font-style:italic;">  {Function} [cb] callback
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> * </span><span style="text-decoration:underline;color:#808080"><span style="color:#808080;background-color:#ffffff;font-weight:bold;font-style:italic;">@return</span></span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> {DOMString}     screenshot image Blob URL,
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> *     if no parameters are given
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;"> */
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">Autodesk.Viewing.Viewer3D.prototype.getScreenShot =
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(w, h, cb)</span></pre>


<p>Pretty straightforward to use. However I noticed that I had to provide width and height arguments otherwise it doesn't to work as expected. If you want to use the current canvas size, you can simply invoke the method as follow using jQuery API:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> $container = $(viewer.container);
</span><span style="color:#800000;background-color:#f0f0f0;">2 
3 </span><span style="background-color:#ffffff;">viewer.getScreenShot(
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#ffffff;">    $container.width(),
</span><span style="color:#800000;background-color:#f0f0f0;">5 </span><span style="background-color:#ffffff;">    $container.height(),
</span><span style="color:#800000;background-color:#f0f0f0;">6 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(newBlobURL){
</span><span style="color:#800000;background-color:#f0f0f0;">7 
8 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// use blobUrl ...
</span><span style="color:#800000;background-color:#f0f0f0;">9 </span><span style="background-color:#ffffff;">    });</span></pre>



<p>The rest of the code is pretty self-explanatory. The ScreenShot Manager will display a panel that allows you to take screenshots of the current model and shows a preview image. You can either delete it or download it by clicking the image link. For that I used an html5 feature that allows to use a <a href="http://updates.html5rocks.com/2011/08/Downloading-resources-in-HTML5-a-download" target="_self">download tag</a> on &lt;a&gt; elements to pass the predefined name of the downloaded resource. Also the &lt;a href&gt; tag can just be set to the blobUrl, not further data manipulation is needed, which I found out after an hour of trials and errors :)</p>
<p>Below is how the result looks like and the complete, self-contained, code for the extension. To use it, simply reference the extension file in your html and invoke it as follow, passing the optional argument <em>createControls</em> if you want to add a toolbar icon, otherwise the panel will just be displayed directly:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="background-color:#ffffff;">viewer.loadExtension(
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#ffffff;">  </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.ScreenShotManager'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#ffffff;">  {
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#ffffff;">      createControls: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true
</span><span style="color:#800000;background-color:#f0f0f0;">5 </span><span style="background-color:#ffffff;">  });</span></pre>

<br>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78e1db4970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c78e1db4970b image-full img-responsive" alt="ScreenShot Manager" title="ScreenShot Manager" src="/assets/image_6c1f78.jpg" border="0" /></a><br />

You can also test a live version <a href="http://viewer.autodesk.io/node/gallery/embed?id=5476efc783bdd31804754db5&extIds=Autodesk.ADN.Viewing.Extension.ScreenShotManager&viewerConfig=%27{%22viewerType%22:%22Viewer3D%22}%27" target="_blank">here</a> directly. 
<br>
<br>
<script src="https://gist.github.com/leefsmp/6cb958bb40ec27715ee4.js"></script>
