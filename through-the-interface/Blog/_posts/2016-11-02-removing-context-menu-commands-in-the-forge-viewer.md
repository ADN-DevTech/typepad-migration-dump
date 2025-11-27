---
layout: "post"
title: "Removing context menu commands in the Forge Viewer"
date: "2016-11-02 18:53:12"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "PaaS"
original_url: "https://www.keanw.com/2016/11/removing-context-menu-commands-in-the-forge-viewer.html "
typepad_basename: "removing-context-menu-commands-in-the-forge-viewer"
typepad_status: "Publish"
---

<p>During&#0160;<a href="http://through-the-interface.typepad.com/through_the_interface/2016/10/forge-accelerator-in-munich.html" target="_blank">last week’s Forge Accelerator</a>, a developer wanted to strip the standard items from the context menu in his Forge Viewer application. We both searched for a while, until we found the answer: he’d been using <a href="http://through-the-interface.typepad.com/through_the_interface/2016/08/adding-a-context-menu-command-to-objects-in-the-forge-viewer.html" target="_blank">this approach to add his own menu items</a> – of course – but it turns out the exact same approach can be used to strip out unwanted items, too. The “context menu callback” receives a menu object that contains its various items: you can inspect them and remove the ones you don’t want, or even adopt a more brute-force approach as we’ve done below and remove everything.</p>
<p>Here’s the updated TypeScript extension from the original post with a few additional lines of code to clean the menu before we add our custom (still hugely theoretical, by the way) “Send to HoloLens” command.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: green;">/// &lt;reference path=&#39;../../../../../typings/lmv-client/lmv-client.d.ts&#39; /&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">export</span> <span style="color: blue;">default</span> <span style="color: blue;">class</span> ContextMenuExtension <span style="color: blue;">extends</span> Autodesk.Viewing.Extension {</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">constructor</span>(viewer: Autodesk.Viewing.Private.GuiViewer3D, options: <span style="color: blue;">any</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">super</span>(viewer, options);</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; load(): <span style="color: blue;">boolean</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; console.log(<span style="color: #a31515;">&#39;ContextMenuExtension loaded&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">let</span> self = <span style="color: blue;">this</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">this</span>.viewer.registerContextMenuCallback(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;Autodesk.Dasher.ContextMenuExtension&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (menu, status) =&gt; {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Remove all existing menu items</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">while</span> (menu.length &gt; 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; menu.pop();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Add our new item if an object is selected</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (status.hasSelected) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; menu.push({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; title: <span style="color: #a31515;">&#39;Send to HoloLens&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; target: <span style="color: blue;">function</span>(): <span style="color: blue;">void</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">let</span> messageSpecs = {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;msgTitleKey&#39;</span>: <span style="color: #a31515;">&#39;Sent to HoloLens&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;messageKey&#39;</span>: <span style="color: #a31515;">&#39;Sent to HoloLens&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;messageDefaultValue&#39;</span>: <span style="color: #a31515;">&#39;This object has been sent to HoloLens for viewing.&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.Private.HudMessage.displayMessage(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; self.viewer.container, messageSpecs</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; () =&gt; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.Private.HudMessage.dismiss();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }, 10000</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; unload(): <span style="color: blue;">boolean</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; console.log(<span style="color: #a31515;">&#39;ContextMenuExtension unloaded&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">this</span>.viewer.unregisterContextMenuCallback(<span style="color: #a31515;">&#39;Autodesk.Dasher.ContextMenuExtension&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Here’s what the new context menu looks like when you right-click on an object in the Forge Viewer, now:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c8ab6e86970b-pi" target="_blank"><img alt="Our custom menu item in the Forge Viewer" border="0" height="321" src="/assets/image_583082.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="Our custom menu item in the Forge Viewer" width="500" /></a></p>
