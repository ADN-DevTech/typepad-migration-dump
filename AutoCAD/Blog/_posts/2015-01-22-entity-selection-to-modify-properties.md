---
layout: "post"
title: "Entity selection to modify properties"
date: "2015-01-22 02:24:28"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/entity-selection-to-modify-properties.html "
typepad_basename: "entity-selection-to-modify-properties"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To select entities and to have their properties displayed in the property palette, it is necessary for your command to use CommandFlags.Redraw. This ensures that AutoCAD highlights the selection. You can then modify their common properties if you wish.</p>
<p></p>
<p>Here is a small code snippet to select all entities :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;SelectAll&quot;</span><span style="color:#000000"> , CommandFlags.Redraw)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SelectMethod()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Editor ed </pre>
<pre style="margin:0em;"> 	= Application.DocumentManager.MdiActiveDocument.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     PromptSelectionResult psr = ed.SelectAll();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (SelectionSet ss = psr.Value)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ed.SetImpliedSelection(ss.GetObjectIds());</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is a screenshot in AutoCAD :</p>

<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07dffd1c970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07dffd1c970d img-responsive" alt="1" title="1" src="/assets/image_576931.jpg" style="margin: 0px 5px 5px 0px;" /></a>
