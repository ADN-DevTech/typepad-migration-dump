---
layout: "post"
title: "Adding custom meta-properties to the viewer property panel"
date: "2015-05-29 08:41:38"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/adding-custom-meta-properties-to-the-viewer-property-panel.html "
typepad_basename: "adding-custom-meta-properties-to-the-viewer-property-panel"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>This week sample adds up to my series about customising the default user interface in the viewer. Here are the links if you missed the previous posts:</p>
<p><a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/05/building-self-contained-ui-components-for-the-viewer.html" target="_self">Building self-contained UI components for the viewer</a></p>
<p><a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/05/screenshot-extension-manager-for-the-viewer.html" target="_self">Screenshot Extension Manager for the viewer</a></p>
<p>The purpose of that sample is to illustrates how to insert your own properties to the native viewer property panel. I'm calling it meta-property because it shows not only how to add text property with a value and label, but also url links, images and file links.</p>
<p>In that sample, for sake of simplicity, I am hardcoding four properties, one of each type (text, link, file, image), that will be dynamically added to any component property panel. However in a real world scenario, those meta-properties would typically be fetched from your backend, database, or third party web-service, based on the nodeId of the selected component if you need to display component-specific properties.</p>
<p>Here are the properties that I'm adding to the property panel:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> textProp = {
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="background-color:#ffffff;">  name: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Text Property'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="background-color:#ffffff;">  value: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"I'm just a text!"</span><span style="background-color:#ffffff;"> ,
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="background-color:#ffffff;">  category: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Meta Properties'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">  dataType: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'text'
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> linkProp = {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">  name: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Link Property'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">  value: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Visit our API portal...'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">  category: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Meta Properties'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">  dataType: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'link'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">  href: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'http://developer.autodesk.com'
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;">15 
16 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fileProp = {
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">  name: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'File Property'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">  value: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Click to download ...'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">  category: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Meta Properties'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">  dataType: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'file'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">  href: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'img/favicon.ico'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">  filename: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'favicon.ico'
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> imgProp = {
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">  name: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Image Property'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">  category: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Meta Properties'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">  dataType: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'img'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">29 </span><span style="background-color:#ffffff;">  href: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'img/favicon.ico'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">30 </span><span style="background-color:#ffffff;">  filename: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'favicon.ico'
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">};</span></pre>

The result in the viewer panel looks as follow, the link can be clicked and will open in a new browser tab, the image and the file link can be clicked and it will download the resource locally:

<br/>
<br/>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7952faf970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7952faf970b image-full img-responsive" alt="Meta-Properties Extension demo" title="Meta-Properties Extension demo" src="/assets/image_2aceb0.jpg" border="0" /></a><br />

The full code sample is contained in a single viewer extension and can be easily re-used in your custom application:
<br/>
<br/>
<script src="https://gist.github.com/leefsmp/aac09cdd13d750ed9de0.js"></script>
