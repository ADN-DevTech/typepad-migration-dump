---
layout: "post"
title: "Creating your own AutoCAD progress meter using HTML5 and JavaScript"
date: "2015-02-19 15:07:22"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "HTML"
  - "JavaScript"
  - "Runtime"
  - "User interface"
original_url: "https://www.keanw.com/2015/02/creating-your-own-autocad-progress-meter-using-html5-and-javascript.html "
typepad_basename: "creating-your-own-autocad-progress-meter-using-html5-and-javascript"
typepad_status: "Publish"
---

<p>This week I’ve spent quite a bit of time looking into future API features. For one of them I needed to create a progress meter, and thought to myself “why not create one in HTML5?” And as it’s nothing specific to a future product release, I decided to go ahead and post it now.</p>
<p>For context, here’s the way AutoCAD’s standard progress meter currently looks, displayed using the code from <a href="http://through-the-interface.typepad.com/through_the_interface/2007/05/displaying_a_pr.html" target="_blank">this previous post</a>:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb07f3ba7b970d-pi"><img alt="Standard progress meter" height="24" src="/assets/image_445696.jpg" style="float: none; margin-left: auto; display: block; margin-right: auto;" title="Standard progress meter" width="240" /></a></p>
<p>So why would you go head and create your own progress meter? A few different reasons come to mind… yes, AutoCAD has its own, but perhaps you want something more visible (not tucked away in the bottom right corner of the application frame), pausable or more explicitly cancellable. Or perhaps you just want to style it differently – something we’ll take a look at in tomorrow’s post.</p>
<p>Even if you don’t want to create your own progress meter, the techniques shown in today’s post will be valuable if you want to create an HTML UI that’s tightly integrated with AutoCAD.</p>
<p>Overall the code is fairly straightforward: as with most HTML5 projects I’ve embarked upon, I ended up spending more time than expected to get the vertical alignment on the page looking good (mainly because the “old” approach of using tables with the valign attribute no longer works in HTML5… apart from understanding <a href="http://www.impressivewebs.com/css-vertical-align/" target="_blank">how vertical-align now works</a>, there are still <a href="http://www.vanseodesign.com/css/vertical-centering/" target="_blank">a number of approaches for managing vertical space</a>).</p>
<p>The other big sticking point was around getting the various page elements to display consistently. For instance, very often the caption wouldn’t display the first time the dialog was shown in a session… I hit my head against this for ages. In the end I found that having the HTML page call back into our .NET app to say “the page has loaded, we’re ready to roll” was the cleanest approach.</p>
<p>Here’s the progress meter in action, running to completion. You’ll notice the dialog is quite big… that’s the minimum size of a modeless dialog. We could also use another modeless container, of course – such as an HTML palette or even a non-DWG document window – but for this scenario a modeless window made most sense.</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb07f3ba81970d-pi"><img alt="Progress meter - completed" height="388" src="/assets/image_507957.jpg" style="display: inline;" title="Progress meter - completed" width="470" /></a> <br /> </p>
<p>And here it is when it’s cancelled partway through:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7501185970b-pi"><img alt="Progress meter - cancelled" height="388" src="/assets/image_882339.jpg" style="display: inline;" title="Progress meter - cancelled" width="470" /></a> <br /> </p>
<p>Here’s the HTML code:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">!doctype</span> <span style="color: red;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">title</span><span style="color: blue;">&gt;</span>Progress<span style="color: blue;">&lt;/</span><span style="color: maroon;">title</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">98%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">height</span>: <span style="color: blue;">98%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">hidden</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">display</span>: <span style="color: blue;">none</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">.td-center</span>&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-align</span>: <span style="color: blue;">center</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">.td-right</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-align</span>: <span style="color: blue;">right</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">.center-div</span>&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">padding</span>: <span style="color: blue;">25%</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">div</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">font-family</span>: <span style="color: blue;">&#39;Segoe UI&#39;</span>, <span style="color: blue;">Tahoma</span>, <span style="color: blue;">Geneva</span>, <span style="color: blue;">Verdana</span>, <span style="color: blue;">sans-serif</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">font-size</span>: <span style="color: blue;">large</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">font-weight</span>: <span style="color: blue;">bold</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">src</span><span style="color: blue;">=&quot;http://app.autocad360.com/jsapi/v2/Autodesk.AutoCAD.js&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">type</span><span style="color: blue;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> progbar, limit, loaded = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> updateProgress(value) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; progbar.max = limit;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; progbar.value = value;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; progbar.getElementsByTagName(<span style="color: #a31515;">&#39;span&#39;</span>)[0].innerHTML =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Math.floor((100 / limit) * value);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> displayValue(prop, val) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (prop == <span style="color: #a31515;">&quot;progress&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; updateProgress(val);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (prop == <span style="color: #a31515;">&quot;limit&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; limit = val;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Display the specified value in our div for the specified</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// property</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> div = document.getElementById(prop);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (div != <span style="color: blue;">null</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">typeof</span> val === <span style="color: #a31515;">&quot;string&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; div.innerHTML = val;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; div.innerHTML = val.toFixed(2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> showControls(show) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> prog = document.getElementById(<span style="color: #a31515;">&quot;progress&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> butt = document.getElementById(<span style="color: #a31515;">&quot;cancel&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (show) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prog.classList.remove(<span style="color: #a31515;">&quot;hidden&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; butt.classList.remove(<span style="color: #a31515;">&quot;hidden&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prog.classList.add(<span style="color: #a31515;">&quot;hidden&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; butt.classList.add(<span style="color: #a31515;">&quot;hidden&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> start() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; showControls(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> ready() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> loaded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> stop() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; showControls(<span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; self.close();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> updateControls(args) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> obj = JSON.parse(args);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> propName = obj.propName;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> propVal = obj.propValue;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If the string represents a double (we test using</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// a RegExp), round it to 2 decimal places</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> val = 0.0;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> found = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">typeof</span> propVal === <span style="color: #a31515;">&quot;number&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; val = propVal;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; found = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (<span style="color: blue;">typeof</span> propVal === <span style="color: #a31515;">&quot;string&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> re = <span style="color: maroon;">/^[+-] ?[0-9]{0,99}(?:\.[0-9]{1,99})?$/</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (propVal.match(re)) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; val = parseFloat(propVal);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Otherwise just display the string</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; displayValue(propName, propVal);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (found) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; displayValue(propName, val);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Shaping layer extensions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> pageLoaded() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> jsonResponse =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; exec(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; JSON.stringify({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; functionName: <span style="color: #a31515;">&#39;Ready&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; invokeAsCommand: <span style="color: blue;">false</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; functionParams: undefined</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; })</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> jsonObj = JSON.parse(jsonResponse);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (jsonObj.retCode !== Acad.ErrorStatus.eJsOk) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span> Error(jsonObj.retErrorString);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> jsonObj.result;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">function</span> cancelOperation() {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> jsonResponse =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; exec(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; JSON.stringify({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; functionName: <span style="color: #a31515;">&#39;CanOp&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; invokeAsCommand: <span style="color: blue;">false</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; functionParams: undefined</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; })</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> jsonObj = JSON.parse(jsonResponse);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (jsonObj.retCode !== Acad.ErrorStatus.eJsOk) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span> Error(jsonObj.retErrorString);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> jsonObj.result;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">table</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;center-div&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">td</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;td-center&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;caption&quot;&gt;</span><span style="color: red;">&amp;nbsp;</span><span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">td</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">td</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;td-right&quot;</span> <span style="color: red;">width</span><span style="color: blue;">=&quot;100%&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">progress</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;progress&quot;</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;hidden&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">value</span><span style="color: blue;">=&quot;0&quot;</span> <span style="color: red;">max</span><span style="color: blue;">=&quot;100&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">span</span><span style="color: blue;">&gt;</span>0<span style="color: blue;">&lt;/</span><span style="color: maroon;">span</span><span style="color: blue;">&gt;</span>%</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">progress</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">td</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">td</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;td-right&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">button</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;cancel&quot;</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;hidden&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">onclick</span><span style="color: blue;">=&quot;</span>cancelOperation();<span style="color: blue;">&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Cancel</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">button</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">td</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">td</span> <span style="color: red;">class</span><span style="color: blue;">=&quot;td-center&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;extra&quot;&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">td</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">table</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">type</span><span style="color: blue;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; registerCallback(<span style="color: #a31515;">&quot;updval&quot;</span>, updateControls);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; registerCallback(<span style="color: #a31515;">&quot;start&quot;</span>, start);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; registerCallback(<span style="color: #a31515;">&quot;stop&quot;</span>, stop);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; progbar = document.getElementById(<span style="color: #a31515;">&#39;progress&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; document.onkeydown = <span style="color: blue;">function</span> (evt) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; evt = evt || window.event;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (evt.keyCode == 27) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cancelOperation();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; window.onload = pageLoaded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; })();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
</div>
<p>I created a C# class that mimics the ProgressMeter protocol – in fact it derives from the standard ProgressMeter class, adding a few additional capabilities – to make it easier to switch between the two, as needed. You won’t want to put yours in the Autodesk.AutoCAD.Runtime namespace – I simply did so for my own convenience.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Reflection;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Runtime.InteropServices;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> Autodesk.AutoCAD.Runtime</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Use the standard ProgressMeter protocol</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">ProgressMeterHtml</span> : <span style="color: #2b91af;">ProgressMeter</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> _ready;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> _cancelled;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">int</span> _pos;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">DllImport</span>(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;AcJsCoreStub.crx&quot;</span>, CharSet = <span style="color: #2b91af;">CharSet</span>.Auto,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; CallingConvention = <span style="color: #2b91af;">CallingConvention</span>.Cdecl,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; EntryPoint = <span style="color: #a31515;">&quot;acjsInvokeAsync&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">extern</span> <span style="color: blue;">static</span> <span style="color: blue;">private</span> <span style="color: blue;">int</span> acjsInvokeAsync(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span> name, <span style="color: blue;">string</span> jsonArgs</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Called by Progress.html when the page has loaded</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">JavaScriptCallback</span>(<span style="color: #a31515;">&quot;Ready&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">string</span> ReadyToStart(<span style="color: blue;">string</span> jsonArgs)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _ready = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: #a31515;">&quot;{\&quot;retCode\&quot;:0}&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Called by Progress.html to cancel the operation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">JavaScriptCallback</span>(<span style="color: #a31515;">&quot;CanOp&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">string</span> CancelOperation(<span style="color: blue;">string</span> jsonArgs)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _cancelled = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: #a31515;">&quot;{\&quot;retCode\&quot;:0}&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Constructor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> ProgressMeterHtml()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Initialize static members</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _ready = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _cancelled = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Load Progress.html from this module&#39;s folder</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> asm = <span style="color: #2b91af;">Assembly</span>.GetExecutingAssembly();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> loc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Path</span>.GetDirectoryName(asm.Location) + <span style="color: #a31515;">&quot;\\progress.html&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Application</span>.ShowModelessWindow(<span style="color: blue;">new</span> System.<span style="color: #2b91af;">Uri</span>(loc));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Wait for the page to load fully to avoid refresh issues</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">while</span> (!_ready)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Threading.<span style="color: #2b91af;">Thread</span>.Sleep(500);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.DoEvents();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Initialize our progress counter</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _pos = 0;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Start the progress meter without a caption</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> Start()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; acjsInvokeAsync(<span style="color: #a31515;">&quot;start&quot;</span>, <span style="color: #a31515;">&quot;{}&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Start the progress meter with a caption</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> Start(<span style="color: blue;">string</span> displayString)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Start();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Caption(displayString);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set the limit</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> SetLimit(<span style="color: blue;">int</span> max)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; SendProperty(<span style="color: #a31515;">&quot;limit&quot;</span>, max);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Advance the progress meter</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> MeterProgress()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; SendProperty(<span style="color: #a31515;">&quot;progress&quot;</span>, ++_pos);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Stop the progess meter, whether it&#39;s finished or the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// operation has been cancelled</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> Stop()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Caption(_cancelled ? <span style="color: #a31515;">&quot;Cancelled&quot;</span> : <span style="color: #a31515;">&quot;Completed&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; AdditionalInfo(<span style="color: #a31515;">&quot; &quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;ll wait for a second and then close the dialog</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; System.Threading.<span style="color: #2b91af;">Thread</span>.Sleep(1000);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; acjsInvokeAsync(<span style="color: #a31515;">&quot;stop&quot;</span>, <span style="color: #a31515;">&quot;{}&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Cancels the current operation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Cancel()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _cancelled = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Returns whether the operation has been cancelled</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> Cancelled</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _cancelled; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Sets the dialog&#39;s caption</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Caption(<span style="color: blue;">string</span> displayString)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; SendProperty(<span style="color: #a31515;">&quot;caption&quot;</span>, displayString);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Sets the additional information text</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> AdditionalInfo(<span style="color: blue;">string</span> displayString)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; SendProperty(<span style="color: #a31515;">&quot;extra&quot;</span>, displayString);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Helper function to set a property in the HTML page</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span> SendProperty(<span style="color: blue;">string</span> name, <span style="color: blue;">object</span> val)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">bool</span> enclose = val.GetType() == <span style="color: blue;">typeof</span>(<span style="color: #2b91af;">String</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> args =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;{\&quot;propName\&quot;:\&quot;&quot;</span> + name + <span style="color: #a31515;">&quot;\&quot;,\&quot;propValue\&quot;:&quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (enclose ? <span style="color: #a31515;">&quot;\&quot;&quot;</span> : <span style="color: #a31515;">&quot;&quot;</span>) + val.ToString() +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (enclose ? <span style="color: #a31515;">&quot;\&quot;&quot;</span> : <span style="color: #a31515;">&quot;&quot;</span>) + <span style="color: #a31515;">&quot;}&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; acjsInvokeAsync(<span style="color: #a31515;">&quot;updval&quot;</span>, args);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>The calling code is almost identical to what we saw in the original ProgressMeter post:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows.Forms;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> ProgressMeterTest</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Cmds</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;PB&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ProgressBarHtml()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">int</span> ticks = 50;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pm = <span style="color: blue;">new</span> <span style="color: #2b91af;">ProgressMeterHtml</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pm.Start(<span style="color: #a31515;">&quot;Testing Progress Bar&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pm.AdditionalInfo(<span style="color: #a31515;">&quot;Show something extra&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pm.SetLimit(ticks);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Now our lengthy operation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; ticks; i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Threading.<span style="color: #2b91af;">Thread</span>.Sleep(50);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Increment progress meter...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pm.MeterProgress();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Application</span>.DoEvents();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pm.Cancelled)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pm.Stop();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>That’s it for today’s post. Tomorrow we’ll take a look at styling the HTML to see what we can do with it.</p>
