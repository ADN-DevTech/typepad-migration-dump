---
layout: "post"
title: "Displaying an AutoCAD ribbon tab contextually using .NET"
date: "2011-10-07 05:53:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Notification / Events"
  - "Selection"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2011/10/displaying-an-autocad-ribbon-tab-contextually-using-net.html "
typepad_basename: "displaying-an-autocad-ribbon-tab-contextually-using-net"
typepad_status: "Publish"
---

<p><em>This very interesting feature came to my attention via an internal discussion. Thanks, once again, to George Varghese for providing the base sample used for this post.</em></p>
<p>At various times inside AutoCAD – such as when a block is selected, for instance – a specific ribbon tab is displayed “contextually”. As an example, when you select a Hatch object inside the AutoCAD editor, you should see a hatch-related contextual tab displayed:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2015435c58def970c-pi" target="_blank"><img alt="Hatch editor contextual tab" border="0" height="118" src="/assets/image_809591.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Hatch editor contextual tab" width="450" /></a></p>
<p>It’s possible to implement your own, comparable behaviour inside the AutoCAD editor using a combination of a simple .NET module, a XAML file and some CUI editing (or a partial CUI file).</p>
<p>Let’s start with the .NET module. Here’s some code that implements a sample rule that can be hooked into AutoCAD to tell it when a certain condition has been satisfied. In our case, the condition we want is that exactly two circles are selected in the drawing editor.</p>
<p>Here’s the C# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> Rules</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SampleRule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The flag stating whether the contextual tab is enabled</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> _enableCtxtTab = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// A static reference to the rule itself</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SampleRule</span><span style="line-height: 140%;"> _rule = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// A public property referenced from the XAML, getting</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// whether the contextual tab should be enabled</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> TwoCirclesSelected</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">get</span><span style="line-height: 140%;"> { </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> _enableCtxtTab; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// A public static property providing access to the rule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (also referenced from the XAML)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SampleRule</span><span style="line-height: 140%;"> TheRule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">get</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (_rule == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _rule = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SampleRule</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> _rule;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Constructor for the rule, where we attach various</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// even handlers</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; SampleRule()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DocumentCollection</span><span style="line-height: 140%;"> dm =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.ApplicationServices.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DocumentManager;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; dm.DocumentCreated +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentCollectionEventHandler</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OnDocumentCreated</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; dm.DocumentToBeDestroyed +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentCollectionEventHandler</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OnDocumentToBeDestroyed</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; dm.MdiActiveDocument.ImpliedSelectionChanged +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">EventHandler</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OnImpliedSelectionChanged</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// When the pickfirst selection is changed, check</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the selection to see whether to enable the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// contextual tab (if exactly two circles are selected)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OnImpliedSelectionChanged(</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptSelectionResult</span><span style="line-height: 140%;"> res = ed.SelectImplied();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; EnableContextualTab(res.Value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// When a document is created, add our handler to it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OnDocumentCreated(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span><span style="line-height: 140%; color: #2b91af;">DocumentCollectionEventArgs</span><span style="line-height: 140%;"> e</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; e.Document.ImpliedSelectionChanged +=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">EventHandler</span><span style="line-height: 140%;">(OnImpliedSelectionChanged);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// When a document is destroyed, remove our handler from it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OnDocumentToBeDestroyed(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span><span style="line-height: 140%; color: #2b91af;">DocumentCollectionEventArgs</span><span style="line-height: 140%;"> e</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; e.Document.ImpliedSelectionChanged -=</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">EventHandler</span><span style="line-height: 140%;">(OnImpliedSelectionChanged);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Check whether we should enable the contextual tab,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// based on the selection passed in</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> EnableContextualTab(</span><span style="line-height: 140%; color: #2b91af;">SelectionSet</span><span style="line-height: 140%;"> ss)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The default assumption is &quot;no&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; _enableCtxtTab = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We need to have exactly two objects selected</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ss == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ss.Count == 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">ObjectId</span><span style="line-height: 140%;">[] ids = ss.GetObjectIds();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ids != </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Check whether both objects are Circles</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DBObject</span><span style="line-height: 140%;"> obj =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(ids[0], </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DBObject</span><span style="line-height: 140%;"> obj2 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(ids[1], </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _enableCtxtTab = (obj </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;"> &amp;&amp; obj2 </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Circle</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Commit, as it&#39;s quicker than aborting</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The above code is reasonably simple – it detects changes to the pickfirst selection set and checks whether it contains exactly two objects, both of which are circles. If so, it sets a Boolean property that can be checked by AutoCAD.</p>
<p>To tell AutoCAD what to do with the module, we need to include a small XAML file which AutoCAD will parse on startup:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;?</span><span style="line-height: 140%; color: #a31515;">xml</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">version</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">1.0</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;"> </span><span style="line-height: 140%; color: red;">encoding</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">utf-8</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">?&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">TabSelectorRules</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; </span><span style="line-height: 140%; color: red;">xmlns</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">clr-namespace:Autodesk.AutoCAD.Ribbon;assembly=AcWindows</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; </span><span style="line-height: 140%; color: red;">Ordering</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">0</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">TabSelectorRules.References</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">AssemblyReference</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">Namespace</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">Rules</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">Assembly</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">ADNPlugin-SampleRule</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">TabSelectorRules.References</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;</span><span style="line-height: 140%; color: #a31515;">Rule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">Uid</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">ADNPluginSampleRuleId</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">DisplayName</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">Sample: Two circles selected rule</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">Theme</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">Green</span><span style="line-height: 140%;">&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">Trigger</span><span style="line-height: 140%; color: blue;">=</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">Selection</span><span style="line-height: 140%;">&quot;</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; &lt;![CDATA[</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">&#0160;&#0160;&#0160;&#0160;&#0160; SampleRule.TheRule.TwoCirclesSelected</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">]]&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; &lt;/</span><span style="line-height: 140%; color: #a31515;">Rule</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">TabSelectorRules</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
</div>
<p>The DLL and XAML files are to be placed in the AutoCAD executable folder (on my system this is <em>“C:\Program Files\Autodesk\AutoCAD 2012 - English”</em>), which means it’s imperative you prefix both with your Registered Developer Symbol (RDS), obtainable from <a href="http://autodesk.com/symbolreg">http://autodesk.com/symbolreg</a>.</p>
<p>In my case – as I have the ADNP symbol registered – I’ve named the files <em>ADNPlugin-SampleRule.dll</em> and&#0160; <em>ADNPlugin-SampleContextualTabSelectorRules.xaml</em>. You can find them <a href="http://through-the-interface.typepad.com/files/SampleRule.zip" target="_blank">here, along with the source project</a>.</p>
<p>Now on to actually using the rule inside AutoCAD.</p>
<p>Launch AutoCAD: if all is well you should see nothing out of the ordinary, otherwise you’ll see lots of additional (and often very helpful) text describing the problem with your rule implementation (or its XAML description).</p>
<p>Run the CUI command. From here you should see a new node named “Sample: Two circles selected rule” under <em>Ribbon</em> –&gt; <em>Contextual Tab States</em>.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2015391f21ae8970b-pi" target="_blank"><img alt="Our new Contextual Tab State in the CUI dialog" border="0" height="235" src="/assets/image_529269.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our new Contextual Tab State in the CUI dialog" width="270" /></a></p>
<p>To have a custom tab displayed when this condition is true, simply drag and drop the tab from the <em>Ribbon</em> –&gt; <em>Tabs</em> list to our new node. You’ll see a number of tabs named “… Contextual Tab”, for just this purpose. Just for fun – and because we can – let’s show the “Solid Modeling” tab when two circles are selected in the editor.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2015391f21af4970b-pi" target="_blank"><img alt="Let&#39;s show the solid modelling tab when two circles are selected" border="0" height="252" src="/assets/image_783576.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Let&#39;s show the solid modelling tab when two circles are selected" width="270" /></a></p>
<p>Sure enough, when we close the CUI dialog, saving changes, and select two circles in the AutoCAD editor, we see the Solid Modeling tab gets displayed and highlighted with our chosen theme (a green tint to it):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2015391f21b0b970b-pi" target="_blank"><img alt="The Solid Modeling tab displayed contextually when two circles are selected" border="0" height="153" src="/assets/image_363404.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The Solid Modeling tab displayed contextually when two circles are selected" width="450" /></a></p>
<p>Now of course it’s unlikely you’ll want to do something quite like this, but this sample does show how you might implement your own contextual tabs, and not just for objects of a certain – you can perform quite deep analysis of the selection set, should you so wish (keeping an eye on interactive performance, of course).</p>
