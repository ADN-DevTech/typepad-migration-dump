---
layout: "post"
title: "Offline support with View & Data API"
date: "2015-10-30 07:40:13"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/10/offline-support-with-view-data-api.html "
typepad_basename: "offline-support-with-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Since the launch of our View &amp; Data API technology, developers have been asking if it is possible to use the viewer offline.&nbsp;</p>
<p>For that reason <a href="http://around-the-corner.typepad.com/adn/cyrille-fauvel.html" target="_self">Cyrille</a> created the <a href="http://extract.autodesk.io/" target="_self">extract</a> sample that allows you to upload your model to his website and it will let you download a package that contains all the files required for offline viewing.</p>
<p>I grabbed some of Cyrille's code and added it to the <a href="https://github.com/Developer-Autodesk/view-and-data-npm" target="_self">View &amp; Data NPM Package</a>, so it offers more flexibility to developers. It is so far exposing just a single method that allows you to download to a target directory on your server the whole directory structure and files required to load the model offline.</p>
<p>Here is what your node.js code may look like if you want to download a model knowing it's URN. You obviously have to use the same credentials that were used to upload and translate that model:</p>
<script src="https://gist.github.com/leefsmp/8fb342f5176e1fc8383c.js"></script>
<p>For offline viewing you need to run a local html server, then unzip the lmv-local/version.zip &nbsp;in your client lib folder and reference the css and viewer3d.js (or viewer3d.min.js for production):</p>
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">link</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">rel=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"stylesheet"</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">type=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"text/css"</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">href=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"lib/lmv-local/v1.2.21/style.css?v=v1.2.21"</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">script</span><span style="background-color:#efefef;"> </span><span style="color:#0000ff;background-color:#efefef;font-weight:bold;">src=</span><span style="color:#008000;background-color:#efefef;font-weight:bold;">"lib/lmv-local/v1.2.21/viewer3D.min.js?v=v1.2.21"</span><span style="background-color:#efefef;">&gt;&lt;/</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">script</span><span style="background-color:#efefef;">&gt;</span></pre>
<br>
<p>In your JavaScript, load an available viewable path obtained as output of the download:</p>



<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:11pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//&lt;div id="viewer-local"&gt;&lt;/div&gt; in your html
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> viewer = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.Viewing.Private.GuiViewer3D(
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="background-color:#ffffff;">  document.getElementById(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'viewer-local'</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> options = {
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="background-color:#ffffff;">  docid: viewablePath[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">].path,
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">  env: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Local'
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">Autodesk.Viewing.Initializer (options, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">  viewer.initialize();
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">  viewer.load(options.docid);
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="background-color:#ffffff;">});</span></pre>

Here is an example of directory structure and files downloaded for a simple model. If you deal with more complex models, the amount of files can be considerably larger:

<br>
<br>

<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0888fb22970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0888fb22970d img-responsive"
style="height:550px" alt="Screen Shot 2015-10-30 at 15.48.08" title="Screen Shot 2015-10-30 at 15.48.08" src="/assets/image_9afecb.jpg" style="margin: 0px 5px 5px 0px;" /></a>
