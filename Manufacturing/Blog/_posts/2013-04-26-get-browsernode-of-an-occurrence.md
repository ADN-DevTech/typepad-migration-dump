---
layout: "post"
title: "Get BrowserNode of an occurrence"
date: "2013-04-26 08:42:19"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/get-browsernode-of-an-occurrence.html "
typepad_basename: "get-browsernode-of-an-occurrence"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is a function for exactly this purpose called&#0160;<strong>GetBrowserNodeFromObject</strong>, but unfortunately, it seems that it does not always return the correct <strong>BrowserNode</strong> object. E.g. in case of an occurrence which is part of a pattern feature, it would return a <strong>BrowserNode</strong> object which seems like some internal object. And so if you try to call <strong>EnsureVisible()</strong> on it, then Inventor will throw an error.&#0160;</p>
<p>If you run into such a situation then you could find the correct node using the following code:&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">public</span> <span style="color: #2b91af;">BrowserNode</span> findNode(<br /><span style="color: #2b91af;">&#0160; BrowserNode</span> node, <span style="color: #2b91af;">ComponentOccurrence</span> occ)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Use try/catch because the node may not have a NativeObject <br />&#0160; // and would&#0160;</span><span style="color: green;">throw an exception here</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (node.NativeObject == occ)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">return</span> node;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">catch</span> { }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">BrowserNode</span> subNode <span style="color: blue;">in</span> node.BrowserNodes)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: #2b91af;">BrowserNode</span> returned = findNode(subNode, occ);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">if</span> (returned != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">return</span> returned;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px; line-height: 120%;">}</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">private</span> <span style="color: blue;">void</span> SelectNode(<br /><span style="color: #2b91af;">&#0160; AssemblyDocument</span> oDoc, <span style="color: #2b91af;">ComponentOccurrence</span> occ)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Get The Browser Pane by BrowserInternalName</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">BrowserPane</span> browserPane = <br />&#0160; &#0160; oDoc.BrowserPanes[<span style="color: #a31515;">&quot;AmBrowserArrangement&quot;</span>];</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: #2b91af;">BrowserNode</span> occNode = findNode(browserPane.TopNode, occ);&#0160;&#0160; </p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// Select node</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; occNode.EnsureVisible();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// EnsureVisible seems to highlight it</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">// so this is probably not needed</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; occNode.DoSelect();</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901b9a5de3970b-pi" style="display: inline;"><img alt="Bolts" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01901b9a5de3970b image-full" src="/assets/image_efd186.jpg" title="Bolts" /></a><br /><br /></p>
<p>&#0160;</p>
