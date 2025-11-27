---
layout: "post"
title: "F1 help for Ribbon buttons"
date: "2014-08-27 03:56:31"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/08/f1-help-for-ribbon-buttons.html "
typepad_basename: "f1-help-for-ribbon-buttons"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When the user hovers over a Ribbon button, then if the tooltip is progressive (i.e. provides an expanded description that shows only after a couple of seconds) then a message at the bottom of that notifies the user that the help information for that button can be envoked by pressing <strong>F1&#0160;</strong>while the tooltip is showing. You can set this up through the API for your own buttons as well.<br />Note: this <strong>F1</strong> functionality works for the tooltip even if that is not progressive.</p>
<p>You just need to handle the <strong>ButtonDefinition</strong>&#39;s <strong>OnHelp</strong> event. I modified the <strong>SimpleAddIn</strong> sample project&#39;s <strong>DrawSlotButton.cs</strong> file with the following code which also adds extended help:&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">public</span> DrawSlotButton(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">string</span> displayName, <span style="color: blue;">string</span> internalName,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">CommandTypesEnum</span> commandType, <span style="color: blue;">string</span> clientId, <span style="color: blue;">string</span> description,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">string</span> tooltip, <span style="color: #2b91af;">Icon</span> standardIcon, <span style="color: #2b91af;">Icon</span> largeIcon,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">ButtonDisplayEnum</span> buttonDisplayType)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; : <span style="color: blue;">base</span>(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; displayName, internalName, commandType, clientId,</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; description, tooltip, standardIcon, largeIcon, buttonDisplayType)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">base</span>.ButtonDefinition.OnHelp +=</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">ButtonDefinitionSink_OnHelpEventHandler</span>(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ButtonDefinition_OnHelp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">base</span>.ButtonDefinition.ProgressiveToolTip.Title =</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #a31515;">&quot;Additional Help&quot;</span>;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">base</span>.ButtonDefinition.ProgressiveToolTip.ExpandedDescription =</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #a31515;">&quot;Additional description. Progressive tooltip is &quot;</span> +</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #a31515;">&quot;not needed for the F1 help to work&quot;</span>;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">void</span> ButtonDefinition_OnHelp(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">NameValueMap</span> Context, <span style="color: blue;">out</span> <span style="color: #2b91af;">HandlingCodeEnum</span> HandlingCode)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; HandlingCode = <span style="color: #2b91af;">HandlingCodeEnum</span>.kEventHandled;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Show the webpage with the help of the current </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Ribbon button</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// System.Diagnostics.Process is being used here</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">Process</span>.Start(<span style="color: #a31515;">&quot;http://www.autodesk.com&quot;</span>);</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6d3fea2970b-pi" style="display: inline;"><img alt="Progressive" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6d3fea2970b image-full img-responsive" src="/assets/image_fd497f.jpg" title="Progressive" /></a></p>
<p>&#0160;</p>
