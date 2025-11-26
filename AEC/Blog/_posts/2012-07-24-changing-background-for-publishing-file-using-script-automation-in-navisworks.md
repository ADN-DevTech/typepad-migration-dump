---
layout: "post"
title: "Changing Background for publishing file using Script Automation in Navisworks"
date: "2012-07-24 08:00:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/07/changing-background-for-publishing-file-using-script-automation-in-navisworks.html "
typepad_basename: "changing-background-for-publishing-file-using-script-automation-in-navisworks"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>
<p>One of the SDK samples in Navisworks called <em>AutoSimpleScriptExample</em> shows how to use the Navisworks API with the VB script file. If there is a requirement to change the background color from black to another color, say, red during this automation process, how can we achieve this?</p>
<p>All available attributes for publishing are listed in InwOaPublishAttribute. There is no attribute to control the background color. However, you can use state.BackgroundColor before publishing. Performing the following changes (highlighted in grey) to the VB script file included in the AutoSimpleScriptExample, helps change the background color to Red.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#0160;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39; Navisworks API - AutoSimpleScriptExample - Script Automation</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39; Example of using the Navisworks API with VB script file</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39;------------------------------------------------------------</span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39;TODO: The filename needs setting to a suitable .nwd file.</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#0160;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39;create new document</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">set navis_doc=CreateObject(</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;Navisworks.Document&quot;</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">)&#0160; </span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #a31515;"><span style="font-size: 8pt;">&#39;make sure it&#39;</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">s visible&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">navis_doc.visible=</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">true</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">&#0160; </span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39;open document</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">navis_doc.OpenFile(</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;C:\Program Files\Autodesk\Navisworks Manage 2013\API\COM\examples\gatehouse.nwd&quot;</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">)</span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39;set current view to first saved view</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000; font-size: 8pt;">navis_doc.state.currentview=navis_doc.state.savedviews(1).anonview</span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="background-color: #cccccc; color: #000000; font-size: 8pt;"><strong>dim ndx</strong></span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><strong><span style="background-color: #cccccc;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">ndx = navis_doc.state.getenum(</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;eObjectType_nwLVec3f&quot;</span></span></span><span style="line-height: 11pt;"><span style="color: #000000; font-size: 8pt;">)</span></span></span></strong></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="background-color: #cccccc; color: #000000; font-size: 8pt;"><strong>set color =navis_doc.state.objectFactory(ndx)</strong></span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="background-color: #cccccc; color: #000000; font-size: 8pt;"><strong>color.data1 = 1</strong></span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="background-color: #cccccc; color: #000000; font-size: 8pt;"><strong>color.data2 = 0</strong></span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="background-color: #cccccc; color: #000000; font-size: 8pt;"><strong>color.data3 = 0</strong></span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="background-color: #cccccc; color: #000000; font-size: 8pt;"><strong>navis_doc.state.BackgroundColor = color</strong></span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #a31515; font-size: 8pt;">&#39;make sure app stays open with no refs</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000; font-size: 8pt;">navis_doc.stayopen</span></span></span></p>
</div>
<p>Using the SDK sample as it is:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177438db781970d-pi"><img alt="image" border="0" height="154" src="/assets/image_467903.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border-width: 0px;" title="image" width="244" /></a></p>
<p>After modifying the code to change the color: <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616a7899b970c-pi"><img alt="image" border="0" height="153" src="/assets/image_926961.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border-width: 0px;" title="image" width="244" /></a></p>
