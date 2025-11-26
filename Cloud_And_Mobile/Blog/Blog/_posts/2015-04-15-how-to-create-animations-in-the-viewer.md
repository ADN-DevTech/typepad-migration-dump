---
layout: "post"
title: "How to create animations in the viewer?"
date: "2015-04-15 01:49:21"
author: "Philippe Leefsma"
categories:
  - "Browser"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/04/how-to-create-animations-in-the-viewer.html "
typepad_basename: "how-to-create-animations-in-the-viewer"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Last time I wrote about how&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/03/creating-tools-for-the-view-data-api.html" target="_self">to create a basic tool</a>&nbsp;for the LMV viewer, today we will see how to use a more complex tool which creates a camera motion loop.</p>
<p>But first, let's quickly review the few classical JavaScript functions that can help us achieve animations. Here is pretty good description I'm borrowing from&nbsp;<a title="" href="https://developer.mozilla.org/en-US/Add-ons/Code_snippets/Timers" target="_self">the Mozzila Developer Network:</a></p>
<p><em>A block of JavaScript code is generally executed synchronously. But there are some JavaScript native functions (timers) which let us to delay the execution of arbitrary instructions:</em></p>
<ul>
<li><em><a title="The documentation about this has not yet been written; please consider contributing!" href="https://developer.mozilla.org/en-US/docs/Web/API/window.setTimeout"><code>setTimeout()</code></a></em></li>
<li><em><a title="The documentation about this has not yet been written; please consider contributing!" href="https://developer.mozilla.org/en-US/docs/Web/API/window.setInterval"><code>setInterval()</code></a></em></li>
<li><em><a title="This method is used to break up long running operations and run a callback function immediately after the browser has completed other operations such as events and display updates." href="https://developer.mozilla.org/en-US/docs/Web/API/window.setImmediate"><code>setImmediate()</code></a></em></li>
<li><em><a title="You should call this method whenever you're ready to update your animation onscreen. This will request that your animation function be called before the browser performs the next repaint. The number of callbacks is usually 60 times per second, but will generally match the display refresh rate in most web browsers as per W3C recommendation. The callback rate may be reduced to a lower rate when running in background tabs." href="https://developer.mozilla.org/en-US/docs/Web/API/window.requestAnimationFrame"><code>requestAnimationFrame()</code></a></em></li>
</ul>
<p><em>The&nbsp;<code>setTimeout()</code>&nbsp;function is commonly used if you wish to have your function called&nbsp;once&nbsp;after the specified delay. The<code>setInterval()</code>&nbsp;function is commonly used to set a delay for functions that are executed again and again, such as animations. The<code>setImmediate()</code>&nbsp;function can be used instead of the&nbsp;<code>setTimeout(fn,&nbsp;<strong>0</strong>)</code>&nbsp;method to execute heavy operations. The<code>requestAnimationFrame()</code>&nbsp;function tells the browser that you wish to perform an animation and requests that the browser schedule a repaint of the window for the next animation frame.</em></p>
<p>An example of an animation using <em>requestAnimationFrame</em> in LMV is testable in my&nbsp;<a title="" href="http://viewer.autodesk.io/node/physics" target="_blank">Physics extension demo</a>.&nbsp;</p>
<p>Another approach I wanted to illustrate today is using the update callback of a custom tool. Let's take a look at an empty implementation:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// update is called by the framework
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// t: time elapsed since tool activated in ms
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.update = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(t) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//run time based animation...
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">};</span></pre>


<p>Update will be invoked repeatedly by the viewer framework with the time t since the tool has been activated as argument.&nbsp;This can be used to create animations such as moving the camera on a predefined or computed path, move viewer components, animate three.js third party geometry, 2d graphics, visual cues when creating advanced commands, or anything cool we could think about ...</p>
<p>In order to play with that tool update function, I wrote the viewer extension that you can test below. I named it Explorer: it will animate the camera around the model using a circular trajectory computed on the fly. You can test the extension by opening the blog entry <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/04/how-to-create-animations-in-the-viewer.html" target="_blank">in it's own tab</a> or click on the picture below:</p>



<div id="dynamic-content-explorer-div-id">
<iframe width="500" height="480" frameborder="0" allowFullScreen webkitallowfullscreen mozallowfullscreen src="http://viewer.autodesk.io/node/gallery/embed?id=54dd0bb7725ef3180fc4ab9b&extIds=Autodesk.ADN.Viewing.Extension.Explorer&viewerConfig=%27{%22viewerType%22:%22Viewer3D%22}%27"></iframe>
</div>

<div id="img-content-explorer-div-id">
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/cloud_and_mobile/2015/04/how-to-create-animations-in-the-viewer.html" target="_blank"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb081d938e970d image-full img-responsive" alt="Screen Shot 2015-04-14 at 7.10.26 PM" title="Explorer Extension Demo" src="/assets/image_e74173.jpg" border="0" /></a><br />
</div>



<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>

<script >
             
if(window.location.toString().indexOf('how-to-create-animations-in-the-viewer.html') > 0) {
                    
$("#img-content-explorer-div-id").remove();

}
else {

$("#dynamic-content-explorer-div-id").remove();
}

</script>
 
<p>Here is the full source of the Explorer Extension:</p>

<script src="https://gist.github.com/leefsmp/ecceed055473003ab5b1.js"></script>
