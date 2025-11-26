---
layout: "post"
title: "Removing buttons from QuickAccessToolbar crashes AutoCAD"
date: "2014-10-03 14:28:44"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/10/removing-buttons-from-quickaccesstoolbar-crashes-autocad.html "
typepad_basename: "removing-buttons-from-quickaccesstoolbar-crashes-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you are adding ribbon buttons to the AutoCAD QuickAccessToobar (QAT), please ensure that you have provided a unique Id to the ribbon button. If the ribbon button is not provided an Id, AutoCAD can crash when hiding / removing any other standard QAT button.</p>
<p>Here is a code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Autodesk.Windows.ToolBars.<span style="color:#2b91af">QuickAccessToolBarSource</span><span style="color:#000000">  qat </pre>
<pre style="margin:0em;">     = Autodesk.Windows.<span style="color:#2b91af">ComponentManager</span><span style="color:#000000"> .QuickAccessToolBar;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (qat != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#2b91af">RibbonButton</span><span style="color:#000000">  rbButton = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">RibbonButton</span><span style="color:#000000"> ();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Important to provide a unique id</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// to avoid the crash</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     rbButton.Id = <span style="color:#a31515">&quot;MYBUTTON&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     rbButton.Text = <span style="color:#a31515">&quot;Circle&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     rbButton.Description = <span style="color:#a31515">&quot;Circle&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     rbButton.Image = GetIcon(<span style="color:#a31515">&quot;Circle_16.ico&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     rbButton.LargeImage = GetIcon(<span style="color:#a31515">&quot;Circle_32.ico&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Attach the handler to fire out command</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     rbButton.CommandHandler</pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">AutoCADCommandHandler</span><span style="color:#000000"> (<span style="color:#a31515">&quot;_.Circle&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Add it to the Quick Access Toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     qat.AddStandardItem(rbButton);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
