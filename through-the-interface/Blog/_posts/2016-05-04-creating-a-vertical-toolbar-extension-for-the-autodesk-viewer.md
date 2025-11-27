---
layout: "post"
title: "Creating a vertical toolbar extension for the Autodesk viewer"
date: "2016-05-04 22:43:45"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "JavaScript"
  - "PaaS"
original_url: "https://www.keanw.com/2016/05/creating-a-vertical-toolbar-extension-for-the-autodesk-viewer.html "
typepad_basename: "creating-a-vertical-toolbar-extension-for-the-autodesk-viewer"
typepad_status: "Publish"
---

<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2016/05/creating-extensions-for-the-autodesk-viewer.html" target="_blank">the last post</a>, we’re now going to take a closer look at writing extensions for the Autodesk View &amp; Data API. To start with, we’re going to create an extension which displays a vertical toolbar docked to the left of the Autodesk viewer. This toolbar will be centred on the viewer area and only contain three buttons: two will be toggles – with events assigned to when they’re both clicked and unclicked – while the third will simply launch an action.</p>
<p>Here’s the JavaScript for this extension:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: green;">///////////////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;"><span style="color: green;">// Autodesk.Research.TtIf.Extension.Toolbar</span></p>
<p style="margin: 0px;"><span style="color: green;">//</span></p>
<p style="margin: 0px;"><span style="color: green;">///////////////////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px;">AutodeskNamespace(<span style="color: #a31515;">&#39;Autodesk.Research.TtIf.Extension&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Research.TtIf.Extension.Toolbar = <span style="color: blue;">function</span> (viewer, options) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; Autodesk.Viewing.Extension.call(<span style="color: blue;">this</span>, viewer, options);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> _viewer = viewer;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> _this = <span style="color: blue;">this</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; _this.load = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; createToolbar();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; console.log(<span style="color: #a31515;">&#39;Autodesk.Research.TtIf.Extension.Toolbar loaded&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; _this.unload = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; deleteToolbar();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; console.log(<span style="color: #a31515;">&#39;Autodesk.Research.TtIf.Extension.Toolbar unloaded&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">function</span> createToolbar() {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> toolbar = <span style="color: blue;">new</span> Autodesk.Viewing.UI.ToolBar(<span style="color: #a31515;">&#39;toolbar-TtIf&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ctrlGroup = <span style="color: blue;">new</span> Autodesk.Viewing.UI.ControlGroup(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;Autodesk.Research.TtIf.Extension.Toolbar.ControlGroup&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ctrlGroup.addClass(<span style="color: #a31515;">&#39;toolbar-vertical-group&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Names, icons and tooltips for our toolbar buttons</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> names = [<span style="color: #a31515;">&#39;CGB1&#39;</span>, <span style="color: #a31515;">&#39;CGB2&#39;</span>, <span style="color: #a31515;">&#39;CGB3&#39;</span>];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> icons = [<span style="color: #a31515;">&#39;dashboard&#39;</span>, <span style="color: #a31515;">&#39;fire&#39;</span>, <span style="color: #a31515;">&#39;flash&#39;</span>];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> tips = [<span style="color: #a31515;">&#39;Dashboard&#39;</span>, <span style="color: #a31515;">&#39;Temperature&#39;</span>, <span style="color: #a31515;">&#39;Power&#39;</span>];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Operations for when the buttons are clicked</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> clicks =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> () { console.log(<span style="color: #a31515;">&#39;Dashboard clicked&#39;</span>); },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> () { console.log(<span style="color: #a31515;">&#39;Temperature clicked&#39;</span>); },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> () { console.log(<span style="color: #a31515;">&#39;Power clicked&#39;</span>); }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ]</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Operations for when buttons are unclicked (i.e. toggled off)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// If false, then the button won&#39;t have any &#39;state&#39;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> unclicks =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> () { console.log(<span style="color: #a31515;">&#39;Dashboard clicked&#39;</span>); },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> () { console.log(<span style="color: #a31515;">&#39;Temperature clicked&#39;</span>); }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ]</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// The loop to create our buttons</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> button;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> i = 0; i &lt; names.length; i++) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Start by creating the button</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; button = <span style="color: blue;">new</span> Autodesk.Viewing.UI.Button(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;Autodesk.Research.TtIf.Extension.Toolbar.&#39;</span> + names[i]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Assign an icon</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (icons[i] &amp;&amp; icons[i] !== <span style="color: #a31515;">&#39;&#39;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.icon.classList.add(<span style="color: #a31515;">&#39;myicon&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.icon.classList.add(<span style="color: #a31515;">&#39;glyphicon&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.icon.classList.add(<span style="color: #a31515;">&#39;glyphicon-&#39;</span> + icons[i]);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Set the tooltip</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; button.setToolTip(tips[i]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Only create a toggler for our button if it has an unclick operation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (unclicks[i]) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.onClick = createToggler(button, clicks[i], unclicks[i]);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.onClick = clicks[i];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ctrlGroup.addControl(button);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; toolbar.addControl(ctrlGroup);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> toolbarDivHtml = <span style="color: #a31515;">&#39;&lt;div id=&quot;divToolbar&quot;&gt; &lt;/div&gt;&#39;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; $(_viewer.container).append(toolbarDivHtml);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// We want our toolbar to be centered vertically on the page</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; toolbar.centerToolBar = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; $(<span style="color: #a31515;">&#39;#divToolbar&#39;</span>).css({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;top&#39;</span>: <span style="color: #a31515;">&#39;calc(50% + &#39;</span> + toolbar.getDimensions().height / 2 + <span style="color: #a31515;">&#39;px)&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; toolbar.addEventListener(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.UI.ToolBar.Event.SIZE_CHANGED,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; toolbar.centerToolBar</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Start by placing our toolbar off-screen (top: 0%)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; $(<span style="color: #a31515;">&#39;#divToolbar&#39;</span>).css({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;top&#39;</span>: <span style="color: #a31515;">&#39;0%&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;left&#39;</span>: <span style="color: #a31515;">&#39;0%&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;z-index&#39;</span>: <span style="color: #a31515;">&#39;100&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;position&#39;</span>: <span style="color: #a31515;">&#39;absolute&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; $(<span style="color: #a31515;">&#39;#divToolbar&#39;</span>)[0].appendChild(toolbar.container);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// After a delay we&#39;ll center it on screen</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span> () { toolbar.centerToolBar(); }, 100);</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">function</span> deleteToolbar() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; $(<span style="color: #a31515;">&#39;#divToolbar&#39;</span>).remove();</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">function</span> createToggler(button, click, unclick) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> state = button.getState();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (state === Autodesk.Viewing.UI.Button.State.INACTIVE) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.setState(Autodesk.Viewing.UI.Button.State.ACTIVE);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; click();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (state === Autodesk.Viewing.UI.Button.State.ACTIVE) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; button.setState(Autodesk.Viewing.UI.Button.State.INACTIVE);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; unclick();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">function</span> setVisibility(panel, flag) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (panel)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; panel.setVisible(flag);</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> css = [</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;.myicon {&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;font-size: 20px;&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;padding-top: 1px !important;&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;}&#39;</span>,</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;.toolbar-vertical-group &gt; .adsk-button &gt; .adsk-control-tooltip {&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;left: 120%;&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;bottom: 25%;&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;}&#39;</span></p>
<p style="margin: 0px;">&#0160; ].join(<span style="color: #a31515;">&#39;\n&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; $(<span style="color: #a31515;">&#39;&lt;style type=&quot;text/css&quot;&gt;&#39;</span> + css + <span style="color: #a31515;">&#39;&lt;/style&gt;&#39;</span>).appendTo(<span style="color: #a31515;">&#39;head&#39;</span>);</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Research.TtIf.Extension.Toolbar.prototype =</p>
<p style="margin: 0px;">&#0160; Object.create(Autodesk.Viewing.Extension.prototype);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Research.TtIf.Extension.Toolbar.prototype.constructor =</p>
<p style="margin: 0px;">&#0160; Autodesk.Research.TtIf.Extension.Toolbar;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Viewing.theExtensionManager.registerExtension(</p>
<p style="margin: 0px;">&#0160; <span style="color: #a31515;">&#39;Autodesk.Research.TtIf.Extension.Toolbar&#39;</span>,</p>
<p style="margin: 0px;">&#0160; Autodesk.Research.TtIf.Extension.Toolbar</p>
<p style="margin: 0px;">);</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>To give it a try, copy and paste the code into the <a href="http://viewer.autodesk.io/node/gallery/#/extension-editor" target="_blank">extension editor in the View &amp; Data Gallery</a>.</p>
<p>Here’s how it looks for me – note the two items toggled on, as well as the right-positioned tooltip for the top icon (which the invisible cursor is hovering over):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c852ccb0970b-pi" target="_blank"><img alt="Left-docked toolbar in the Autodesk viewer" border="0" height="330" src="/assets/image_414285.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border: 0px;" title="Left-docked toolbar in the Autodesk viewer" width="500" /></a></p>
<p>I’m happy that I once again used the tie-fighter model, as it happens to be the unofficial Star Wars day. May the 4th be with you, and all that.</p>
<p>In response to the last post, an old friend and colleague in Autodesk Consulting, Jan Liska, pinged me to point me to some great samples he’d written to show using the View &amp; Data API from TypeScript. It’s something I’ve been meaning to do for some time, and when I see the TypeScript samples when compared to JavaScript (and think about the hoops I jump through to implement and extend interfaces, etc.) I wish I’d covered it sooner. It’s now been added to the list, though, rest assured. :-)</p>
