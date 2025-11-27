---
layout: "post"
title: "Finding out about changes to AutoCAD layers via the Bindable Object Layer using .NET"
date: "2012-07-04 06:39:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Autodesk"
  - "Drawing structure"
  - "Notification / Events"
original_url: "https://www.keanw.com/2012/07/finding-out-about-changes-to-autocad-layers-via-the-bindable-object-layer-using-net.html "
typepad_basename: "finding-out-about-changes-to-autocad-layers-via-the-bindable-object-layer-using-net"
typepad_status: "Publish"
---

<p>In the last few posts on this topic, we saw some examples of <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/dumping-data-from-autocads-bindable-object-layer-using-net.html" target="_blank">getting information from</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/using-autocads-bindable-object-layer-to-change-the-current-view-using-net.html" target="_blank">controlling</a> AutoCAD via its Bindable Object Layer.</p>
<p>In this post, we’re going to look at a way to find out when changes are made to AutoCAD’s layer table: when layers are added, changed or removed.</p>
<p>There are certainly other ways to do this: you can use Database.ObjectAppended(), ObjectModified() and ObjectErased() to find out about changes to LayerTableRecords, for instance, but this is an alternative approach that may be interesting to some people.</p>
<p>In this implementation, we attach some event handlers to keep an eye on what’s happening in the BOL collection bound to the layer table. We also maintain a list of layer names so we have good information on what has changed (in terms of what layers have been removed, at least – if we wanted information on specific changes we’d need to cache more information than that). From this starting point, it should be possible for developers to get a more comprehensive mechanism that provides information on when commands create or remove multiple layers (something this particular implementation hasn’t specifically been designed to deal with or tested against).</p>
<p>Here’s the C# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Windows.Data;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Specialized;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.ComponentModel;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BolInfo</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">&gt; _layerNames = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;LAYMODS&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> GetNotifiedOnLayerChange()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get our layer list and extract the initial layer names</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> layers = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.UIBindings.Collections.Layers;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; UpdateStoredLayerNames();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Attach event handlers to the layer list...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Find out when items are added or removed from the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// collection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; layers.CollectionChanged +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (s, e) =&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nCollection changed: {0}&quot;</span><span style="line-height: 140%;">, e.Action);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; e.Action == </span><span style="line-height: 140%; color: #2b91af;">NotifyCollectionChangedAction</span><span style="line-height: 140%;">.Add &amp;&amp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; e.NewStartingIndex == -1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// What happens for commands that create &gt;1 layers?</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> newLays = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.UIBindings.Collections.Layers;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nNew item: \&quot;{0}\&quot;&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetItemValue(newLays[newLays.Count - 1])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; e.Action == </span><span style="line-height: 140%; color: #2b91af;">NotifyCollectionChangedAction</span><span style="line-height: 140%;">.Remove</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nRemoved item: \&quot;{0}\&quot;&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _layerNames[e.OldStartingIndex]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// As we can&#39;t access data in e.NewItems or e.OldItems</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (they contain NewDataItem objects - a class that isn&#39;t</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// exposed) get the collection again and list it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nUpdated collection: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> item </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.UIBindings.Collections.Layers</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot; \&quot;{0}\&quot;&quot;</span><span style="line-height: 140%;">, GetItemValue(item));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UpdateStoredLayerNames();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; };</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Find out when items have been changed in the collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (although not what specifically has changed)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; layers.ItemsChanged +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (s, e) =&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nItem(s) changed.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UpdateStoredLayerNames();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; };</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Find out when properties of the collection (typically</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the Count, for instance) have changed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; layers.PropertyChanged +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (s, e) =&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCollection property changed: {0}&quot;</span><span style="line-height: 140%;">, e.PropertyName</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; };</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Store a cache of the layer names</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> UpdateStoredLayerNames()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> layers = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.UIBindings.Collections.Layers;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; _layerNames = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">&gt;(layers.Count);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> layer </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> layers)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; _layerNames.Add(GetItemValue(layer));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Extract the name of an item from the item descriptor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> GetItemValue(</span><span style="line-height: 140%; color: #2b91af;">ICustomTypeDescriptor</span><span style="line-height: 140%;"> item)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">)item.GetProperties()[</span><span style="line-height: 140%; color: #a31515;">&quot;Name&quot;</span><span style="line-height: 140%;">].GetValue(item);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>When we run the custom LAYMODS command and then use the standard LAYER command to add, change and remove some layers, we can see the level of information provided:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">LAYMODS</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">LAYER</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer1&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer2&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer3&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer3&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer4&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer3&quot; &quot;Layer4&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer5&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer3&quot; &quot;Layer4&quot; &quot;Layer5&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer7&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer3&quot; &quot;Layer4&quot; &quot;Layer5&quot; &quot;Layer6&quot; &quot;Layer7&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer8&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer3&quot; &quot;Layer4&quot; &quot;Layer5&quot; &quot;Layer6&quot; &quot;Layer7&quot; &quot;Layer8&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Remove</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Removed item: &quot;Layer3&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer4&quot; &quot;Layer5&quot; &quot;Layer6&quot; &quot;Layer7&quot; &quot;Layer8&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Remove</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Removed item: &quot;Layer4&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer5&quot; &quot;Layer6&quot; &quot;Layer7&quot; &quot;Layer8&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Remove</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Removed item: &quot;Layer5&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer6&quot; &quot;Layer7&quot; &quot;Layer8&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Remove</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Removed item: &quot;Layer6&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer7&quot; &quot;Layer8&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Item(s) changed.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Item(s) changed.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer4&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer7&quot; &quot;Layer8&quot; &quot;Layer3&quot; &quot;Layer4&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer5&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer7&quot; &quot;Layer8&quot; &quot;Layer3&quot; &quot;Layer4&quot; &quot;Layer5&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection changed: Add</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">New item: &quot;Layer6&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Updated collection:&#0160; &quot;0&quot; &quot;Layer1&quot; &quot;Layer2&quot; &quot;Layer7&quot; &quot;Layer8&quot; &quot;Layer3&quot; &quot;Layer4&quot; &quot;Layer5&quot; &quot;Layer6&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Collection property changed: Count</span></p>
</div>
