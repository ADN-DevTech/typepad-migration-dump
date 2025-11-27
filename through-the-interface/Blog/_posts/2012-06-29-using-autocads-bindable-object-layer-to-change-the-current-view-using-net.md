---
layout: "post"
title: "Using AutoCAD&rsquo;s Bindable Object Layer to change the current view using .NET"
date: "2012-06-29 06:27:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "Mac"
  - "Notification / Events"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2012/06/using-autocads-bindable-object-layer-to-change-the-current-view-using-net.html "
typepad_basename: "using-autocads-bindable-object-layer-to-change-the-current-view-using-net"
typepad_status: "Publish"
---

<p>After looking at <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/dumping-data-from-autocads-bindable-object-layer-using-net.html" target="_blank">how the Bindable Object Layer (BOL) in AutoCAD might be used to get information about the current drawing</a>, in today’s post we’re going to see how it can also be used to manipulate that data (in a fairly limited, albeit useful, way).</p>
<p>But first, let’s talk a bit about the origins of the BOL. It was first introduced as an architectural feature of AutoCAD when we were looking at delivering <a href="http://autodesk.com/autocadformac" target="_blank">AutoCAD for Mac</a>. It’s common, these days, for programming frameworks to provide some kind of data-binding facility to simplify the creation of UIs: both WPF and Cocoa have data-binding capabilities, for instance, that allow programmers to point collection-friendly UI widgets – such as list- and combo-boxes – at core data collections containing the information they wish to present to the user.</p>
<p>The BOL was our (primarily internal) effort to create a cross-platform, abstract information layer that could be bound via WPF on Windows and Cocoa on OS X to present the UI most appropriate to the respective platforms. Some work was needed to expose the core layer on each platform, but the underlying C++ implementation – contained within AcCore – is shared across platforms.</p>
<p>For instance, much of the information regarding the state of the current drawing – such as the current layer, linetype, dimension/table/text/visual (etc.) style – is providing to AutoCAD’s ribbon by the BOL. The BOL provides the most recent information – it gets updated as the drawing changes and even as the active drawing changes – so that the UI can simply update whenever changes propagate through the BOL.</p>
<p>Which actually makes the BOL quite a powerful abstraction for use programmatically, too. While it’s not directly possible to add items to the BOL and have them appear in the drawing, it is possible to use the BOL to change the “current” item in a collection, and also to use the BOL to find out when modifications have been made to its various collections.</p>
<p>The BOL can therefore be used to change the current Visual Style, the current layer, or – as we show in the post – the current named view.</p>
<p>And here’s the C# code that does so:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Windows.Data;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BolAccess</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;CHGVW&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ChangeViewViaBol()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get our set of named views</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> views = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.UIBindings.Collections.NamedViews;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (views.Count == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nNo named views available.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nNamed views available:&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Create a list in which to store the names of our views</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">&gt; viewNames = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">&gt;(views.Count);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Loop through the view list, populating our list of names</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// and printing each one for later selection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i=0; i &lt; views.Count; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Add a dummy item - will be replaced via the index later</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewNames.Add(</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> desc = views[i];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get the collection&#39;s property descriptors</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> props = desc.GetProperties();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Set our view name as the value of each &quot;Name&quot; property</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (do so by index, just to make sure we&#39;re keeping in sync</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// with the values in the views collection)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewNames[i] = (</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">)props[</span><span style="line-height: 140%; color: #a31515;">&quot;Name&quot;</span><span style="line-height: 140%;">].GetValue(desc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Print the name along with the 1-based index</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n {0} {1}&quot;</span><span style="line-height: 140%;">, i+1, viewNames[i]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Ask the user to select the view based on the index</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptIntegerOptions</span><span style="line-height: 140%;"> opts =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptIntegerOptions</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;\nEnter number of view: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; opts.LowerLimit = 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; opts.UpperLimit = views.Count;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptIntegerResult</span><span style="line-height: 140%;"> pir = ed.GetInteger(opts);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pir.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Simply set our current item to the one selected</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; views.CurrentItem = views[pir.Value - 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>When we run the CHGVW command inside <a href="http://download.autodesk.com/us/samplefiles/acad/visualization_-_condominium_with_skylight.dwg" target="_blank">the “<em>visualization_-_condominium_with_skylight.dwg</em>” sample drawing</a>, we see we can use it to navigate easily between the various named views:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017615dd1027970c-pi" target="_blank"><img alt="Initial view" border="0" height="305" src="/assets/image_272299.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Initial view" width="394" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017615dd1094970c-pi" target="_blank"><img alt="X-ray view" border="0" height="305" src="/assets/image_964291.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="X-ray view" width="394" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016767e7dd22970b-pi" target="_blank"><img alt="Sink view" border="0" height="305" src="/assets/image_388617.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Sink view" width="394" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017742c2d84a970d-pi" target="_blank"><img alt="Render view" border="0" height="305" src="/assets/image_141142.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Render view" width="394" /></a></p>
