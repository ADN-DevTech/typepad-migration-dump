---
layout: "post"
title: "Creating extensions for the Autodesk viewer"
date: "2016-05-03 17:00:18"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "JavaScript"
  - "PaaS"
original_url: "https://www.keanw.com/2016/05/creating-extensions-for-the-autodesk-viewer.html "
typepad_basename: "creating-extensions-for-the-autodesk-viewer"
typepad_status: "Publish"
---

<p>I’ve been working on a prototype implementation of a research project that makes use of the <a href="https://developer.autodesk.com/api/view-and-data-api/" target="_blank">View &amp; Data AP</a>I for its visualization. It’s interesting to get back into using this API, especially as it’s a fundamental piece of the Forge platform.</p>
<p>As we expect this particular application to grow, over time, we’re using extensions to house logically separate parts of the UI implementation. Extensions are a great mechanism for encapsulating functionality: they’re basically JavaScript objects that have load() and unload() methods that are called when the viewer loads/unloads them.</p>
<p>A number of samples in the Autodesk samples repository make use of extensions, particularly the <a href="http://viewer.autodesk.io/node/gallery/" target="_blank">View &amp; Data Gallery</a> (<a href="https://github.com/Developer-Autodesk/ng-gallery" target="_blank">source on GitHub</a>) and the <a href="http://examples.developer.autodesk.com/lmv-extensions/" target="_blank">Viewer Extensions</a> site (<a href="https://github.com/Developer-Autodesk/lmv-extensions" target="_blank">source on GitHub</a>).</p>
<p>Here’s the implementation of a basic extension that you’ll find when you open the extension editor on the View &amp; Data Gallery:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: green;">///////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="color: green;">// Basic viewer extension</span></p>
<p style="margin: 0px;"><span style="color: green;">//</span></p>
<p style="margin: 0px;"><span style="color: green;">///////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;">AutodeskNamespace(<span style="color: #a31515;">&quot;Autodesk.ADN.Viewing.Extension&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.ADN.Viewing.Extension.Basic = <span style="color: blue;">function</span> (viewer, options) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; Autodesk.Viewing.Extension.call(<span style="color: blue;">this</span>, viewer, options);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> _this = <span style="color: blue;">this</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; _this.load = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; alert(<span style="color: #a31515;">&quot;Autodesk.ADN.Viewing.Extension.Basic loaded&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; viewer.setBackgroundColor(255, 0, 0, 255, 255, 255);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; _this.unload = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; viewer.setBackgroundColor(160, 176, 184, 190, 207, 216);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; alert(<span style="color: #a31515;">&quot;Autodesk.ADN.Viewing.Extension.Basic unloaded&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Autodesk.Viewing.theExtensionManager.unregisterExtension(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Autodesk.ADN.Viewing.Extension.Basic&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; };</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.ADN.Viewing.Extension.Basic.prototype =</p>
<p style="margin: 0px;">&#0160;&#0160; Object.create(Autodesk.Viewing.Extension.prototype);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.ADN.Viewing.Extension.Basic.prototype.constructor =</p>
<p style="margin: 0px;">&#0160;&#0160; Autodesk.ADN.Viewing.Extension.Basic;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Viewing.theExtensionManager.registerExtension(</p>
<p style="margin: 0px;">&#0160;&#0160; <span style="color: #a31515;">&quot;Autodesk.ADN.Viewing.Extension.Basic&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160; Autodesk.ADN.Viewing.Extension.Basic);</p>
</div>
<p>&#0160;</p>
<p>The extension is about as simple as it gets: all it does is set the viewer’s background colour to a nice pink gradient. Here’s the view of a model before loading the extension:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1db2024970c-pi" target="_blank"><img alt="The View &amp; Data Gallery with the extension editor" border="0" height="322" src="/assets/image_533095.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="The View &amp; Data Gallery with the extension editor" width="500" /></a></p>
<p>And here’s how it looks with it loaded:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb08f4f4de970d-pi" target="_blank"><img alt="Now with the extension loaded" border="0" height="320" src="/assets/image_281021.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="Now with the extension loaded" width="500" /></a></p>
<p>Over the next couple of posts we’ll look at some sample extensions I’ve created for my current project. We’ll start with one that defines a toolbar that centers on the left of the screen, and look at how we can have extensions for features that get loaded by that toolbar. We’ll also compare and contrast the style used to implement extensions, which is likely to depend on whether you need a customized interface to the extension that can be called from “outside”.</p>
