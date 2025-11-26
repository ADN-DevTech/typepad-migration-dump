---
layout: "post"
title: "Change the selection color in MapGuide Ajax viewer and Fusion viewer"
date: "2012-03-20 22:11:22"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/03/change-the-selection-color-in-mapguide-ajax-viewer-and-fusion-viewer.html "
typepad_basename: "change-the-selection-color-in-mapguide-ajax-viewer-and-fusion-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html" target="_self">Daniel Du</a></p>
<p>As you know, if a feature is selected, it will be highlighted as blue by default, but is it possible to change to other color for selection? It is possible to change the selection color when plotting as well? Yes, it is. In this article, I would like to introduce how to do that.</p>
<p>To change the selection color of select features in Ajax viewer, you can open the ajaxmappane.temp file in text editor, change the value to your favorite color. The value is 0xRRGGBBaa where each is a hex value from 0-255 representing your RGB and transparency (alpha) values. Currently the alpha value does not have any impact as either FF or 00, it is hardcoded in source code.</p>
<p>C:\Program Files\Autodesk\MapGuideEnterprise2011\WebServerExtensions\www\viewerfiles\ajaxmappane.templ</p>
<p>around line 335:</p>
<pre class="csharpcode"><span class="rem">//var selectionColor = '0x0000FFFF'; // Blue</span>
<span class="kwrd">var</span> selectionColor = <span class="str">'0xFF5300FF'</span>;</pre>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e90b01bd970c-pi"><img style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image002[4]" src="/assets/image_9104ba.jpg" border="0" alt="clip_image002[4]" width="358" height="216" /></a></p>
<p>If you want to change the selection color of print as well, you will have to modify printablepage.templ file. Please pay attention to the words highlighted.</p>
<p>C:\Program Files\Autodesk\MapGuideEnterprise2011\WebServerExtensions\www\viewerfiles\printablepage.templ</p>
<p>Line 72:</p>
<pre class="csharpcode"><span class="kwrd">if</span>(requester.responseXML)
{
    <span class="kwrd">if</span>(ValidateMapResponse(<br />         requester.responseXML.documentElement))
        <span class="rem">//document.getElementById("mapImage").src = </span>
        <span class="rem">//webAgent + "?OPERATION=GETMAPIMAGE</span>
        <span class="rem">//&amp;FORMAT=PNG</span>
        <span class="rem">//&amp;VERSION=1.0.0&amp;SELECTION=</span>
        <span class="rem">//&amp;MAPNAME=" + encodeURIComponent(mapName) </span>
        <span class="rem">//+ "&amp;SESSION=" + sessionId </span>
        <span class="rem">//+ "&amp;CLIENTAGENT=" + encodeURIComponent(clientAgent) ;</span>

        document.getElementById(<span class="str">"mapImage"</span>).src = 
         webAgent <br />         + <span class="str">"OPERATION=<span style="background-color: #ffff00;">GETDYNAMICMAPOVERLAYIMAGE</span>
         &amp;FORMAT=PNG
         &amp;VERSION=<span style="background-color: #ffff00;">2.0.0<br /></span>         &amp;SELECTION=
         &amp;MAPNAME="</span> + encodeURIComponent(mapName) 
        + <span class="str">"&amp;SESSION="</span> + sessionId <br />        + <span class="str">"&amp;CLIENTAGENT="</span> 
        + encodeURIComponent(clientAgent) +
        <span class="str">"&amp;<span style="background-color: #ffff00;">BEHAVIOR=7&amp;SELECTIONCOLOR=FF5300FF</span>"</span> ;
}</pre>
<p>With this modification, we use GETDYNAMICMAPOVERLAYIMAGE as operation and pass the BEHAVIOR and SELECTIONCOLOR as additional parameters. You may be curious about the BEHAVIOR parameter. MapGuideRfc38( <a href="http://trac.osgeo.org/mapguide/wiki/MapGuideRfc38">http://trac.osgeo.org/mapguide/wiki/MapGuideRfc38</a> ) gives us an explanation. In old release, MapGuide rendered the selection and the overlay image as a single image. This required all untiled layers to be redrawn whenever the selection is changed. To make it more efficient, an improvement is implemented to add the parameter BEHAVIOR to the existing GETDYNAMICMAPOVERLAYIMAGE and increase the VERSION to 2.0.0. BEHAVIOR is a bitmask with the following values:</p>
<pre class="csharpcode">RenderSelection = 1 <span class="rem">// Renders the selected feature(s)</span>
RenderLayers = 2    <span class="rem">// Renders the features on the map</span>
KeepSelection = 4   <span class="rem">// Renders the selected feature(s) </span>
          //even <span class="kwrd">if</span> they are outside the current scale</pre>
<p>With this change, the color of selected features is also changed while plotting.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e90b01d4970c-pi"><img style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image004[4]" src="/assets/image_416f0e.jpg" border="0" alt="clip_image004[4]" width="250" height="298" /></a></p>
<p>If you are using Flexible Web Layout (Fusion Viewer), you can change the selection color from MapGuide Studio. Open the flexible web layout in MapGuide Studio, and switch to Map tab. By clicking “Edit Map Groups” button, you will get the xml configuration of map groups as below, and you can change the value of &lt;SelectionColor&gt; to other color value:</p>
<pre class="csharpcode"><span class="kwrd">&lt;?</span><span class="html">xml</span> <span class="attr">version</span><span class="kwrd">="1.0"</span> <span class="attr">encoding</span><span class="kwrd">="utf-8"</span>?<span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">MapSet</span> <span class="attr">xmlns:xsi</span><span class="kwrd">=<a href="http://www.w3.org/2001/XMLSchema-instance">http://www.w3.org/2001/XMLSchema-instance</a></span> <br /><span class="attr">        xmlns:xsd</span><span class="kwrd">="http://www.w3.org/2001/XMLSchema"</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">MapGroupType</span> <span class="attr">id</span><span class="kwrd">="Sheboygan"</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">Map</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;</span><span class="html">Type</span><span class="kwrd">&gt;</span>MapGuide<span class="kwrd">&lt;/</span><span class="html">Type</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;</span><span class="html">SingleTile</span><span class="kwrd">&gt;</span>true<span class="kwrd">&lt;/</span><span class="html">SingleTile</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;</span><span class="html">Extension</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">ResourceId</span><span class="kwrd">&gt;<br /></span>          Library://Samples/Sheboygan/Maps/Sheboygan.MapDefinition<br />        <span class="kwrd">&lt;/</span><span class="html">ResourceId</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">SelectionAsOverlay</span><span class="kwrd">&gt;</span>true<span class="kwrd">&lt;/</span><span class="html">SelectionAsOverlay</span><span class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">SelectionColor</span><span class="kwrd">&gt;</span><span style="background-color: #ffff00;">0xFF5300FF</span> <span class="kwrd">&lt;/</span><span class="html">SelectionColor</span><span class="kwrd">&gt;</span>
      <span class="kwrd">&lt;/</span><span class="html">Extension</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">Map</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">Extension</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;/</span><span class="html">MapGroupType</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">MapSet</span><span class="kwrd">&gt;</span></pre>
<p>Here is the screen-shot after modification:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016303157caf970d-pi"><img style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image006[4]" src="/assets/image_cee99f.jpg" border="0" alt="clip_image006[4]" width="341" height="274" /></a></p>
<p>To change the selection color of printing in Fusion viewer, you have to modify the source code of printablepage.templ. Here is the code snippet:</p>
<p>C:\Program Files\Autodesk\MapGuideEnterprise2011\WebServerExtensions\www\fusion\widgets\Print\printablepage.templ</p>
<p>Line 53:</p>
<pre class="csharpcode">        <span class="rem">//var imgReq = webAgent + "?OPERATION=GETMAPIMAGE</span>
        <span class="rem">//&amp;VERSION=1.0.0&amp;FORMAT=PNG&amp;LOCALE="+locale</span>
        <span class="rem">//+"&amp;MAPNAME=" + encodeURIComponent(mapName) </span>
        <span class="rem">//+ "&amp;SESSION=" + sessionId + "&amp;SETDISPLAYWIDTH=" </span>
        <span class="rem">//+ mapWidth + "&amp;SETDISPLAYHEIGHT=" + mapHeight </span>
        <span class="rem">//+ "&amp;SETDISPLAYDPI=" + dpi + "&amp;SETVIEWSCALE=" </span>
        <span class="rem">//+ scale + "&amp;SETVIEWCENTERX=" + centerX </span>
        <span class="rem">//+ "&amp;SETVIEWCENTERY=" + centerY + "&amp;SEQ=" </span>
        <span class="rem">//+ Math.random() + "&amp;CLIENTAGENT=" </span>
        <span class="rem">//+ encodeURIComponent(clientAgent);</span>

        <span class="kwrd">var</span> imgReq = webAgent <br />        + OPERATION=<span style="background-color: #ffff00;">GETDYNAMICMAPOVERLAYIMAGE</span>
        &amp;VERSION=<span style="background-color: #ffff00;">2.0.0<br /></span>        &amp;FORMAT=PNG&amp;LOCALE=<span class="str">"+locale
        +"</span>&amp;MAPNAME=<span class="str">" + encodeURIComponent(mapName) 
        + "</span>&amp;SESSION=<span class="str">" + sessionId <br />        + "</span>&amp;SETDISPLAYWIDTH=<span class="str">" 
        + mapWidth + "</span>&amp;SETDISPLAYHEIHT=<span class="str">" + mapHeight
        + "</span>&amp;SETDISPLAYDPI=<span class="str">" + dpi + "</span>&amp;SETVIEWSCALE=<span class="str">" 
        + scale + "</span>&amp;SETVIEWCENTERX=<span class="str">" + centerX 
        + "</span>&amp;SETVIEWCENTERY=<span class="str">" + centerY + "</span>&amp;SEQ=<span class="str">" 
        + Math.random() + "</span>&amp;CLIENTAGENT=<span class="str">" 
        + encodeURIComponent(clientAgent) 
        + <span style="background-color: #ffff00;">"</span></span><span style="background-color: #ffff00;">&amp;BEHAVIOR=7&amp;SELECTIONCOLOR=0xFF5300FF</span>";</pre>
<p>And here is the screen-shot after modification:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e90b01f6970c-pi"><img style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image008[4]" src="/assets/image_fba95e.jpg" border="0" alt="clip_image008[4]" width="301" height="343" /></a></p>
<p>OK, with that you can change the selection color to your favorite ones for both Ajax viewer and fusion viewer when viewing and printing. Enjoy !</p>
<p><strong>&nbsp;</strong></p>
<p>&nbsp;</p>
