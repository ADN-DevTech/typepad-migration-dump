---
layout: "post"
title: "Adding a global keyword menu to AutoCAD using WPF &ndash; Part 2"
date: "2015-02-25 16:51:55"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Notification / Events"
  - "Runtime"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2015/02/adding-a-global-keyword-menu-to-autocad-using-wpf-part-2.html "
typepad_basename: "adding-a-global-keyword-menu-to-autocad-using-wpf-part-2"
typepad_status: "Publish"
---

<p>After introducing this project in <a href="http://through-the-interface.typepad.com/through_the_interface/2015/02/adding-a-global-keyword-menu-to-autocad-using-wpf-part-1.html" target="_blank">the last post</a>, now it’s time to share some code. The project, as it currently stands, contains three source files: the first one relates to AutoCAD – it implements the various commands we’ll use to attach event handlers to tell us when to display (or hide) keywords and the other two files relate to the UI we’ll use to display them. We’re going to use an invisible window which has a child popup containing a listbox of our keywords.</p>
<p>Here’s the application in action – for now in English AutoCAD, as that’s what I have installed – helping us with the keywords during the PLINE and HATCH commands:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb07f815cf970d-pi"><img alt="KeywordHelper" height="399" src="/assets/image_799869.jpg" style="float: none; margin-left: auto; display: block; margin-right: auto;" title="KeywordHelper" width="450" /></a> <br /> </p>
<p>Now for the source. Let’s start with the AutoCAD-related C# file, which I’ve called <em>keyword-helper.cs</em>:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Specialized;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> KeywordHelper</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// The keyword display window</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">KeywordWindow</span> _window = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// List of &quot;special&quot; commands that need a timer to reset</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// the keyword list</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">readonly</span> <span style="color: blue;">string</span>[] specialCmds = { <span style="color: #a31515;">&quot;MTEXT&quot;</span> };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;KWS&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> KeywordTranslation()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (_window == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _window = <span style="color: blue;">new</span> <span style="color: #2b91af;">KeywordWindow</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _window.Show();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Application</span>.MainWindow.Focus();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Add our various event handlers</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// For displaying the keyword list...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForAngle += OnPromptingForAngle;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForCorner += OnPromptingForCorner;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForDistance += OnPromptingForDistance;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForDouble += OnPromptingForDouble;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForEntity += OnPromptingForEntity;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForInteger += OnPromptingForInteger;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForKeyword += OnPromptingForKeyword;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForNestedEntity += OnPromptingForNestedEntity;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForPoint += OnPromptingForPoint;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForSelection += OnPromptingForSelection;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForString += OnPromptingForString;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// ... and removing it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandWillStart += OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandEnded += OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandCancelled += OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandFailed += OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.EnteringQuiescentState += OnEnteringQuiescentState;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;KWSX&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> StopKeywordTranslation()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (_window == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _window.Hide();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _window = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Remove our various event handlers</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// For displaying the keyword list...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForAngle -= OnPromptingForAngle;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForCorner -= OnPromptingForCorner;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForDistance -= OnPromptingForDistance;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForDouble -= OnPromptingForDouble;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForEntity -= OnPromptingForEntity;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForInteger -= OnPromptingForInteger;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForKeyword -= OnPromptingForKeyword;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForNestedEntity -= OnPromptingForNestedEntity;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForPoint -= OnPromptingForPoint;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForSelection -= OnPromptingForSelection;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.PromptingForString -= OnPromptingForString;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// ... and removing it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandWillStart -= OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandEnded -= OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandCancelled -= OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.CommandFailed -= OnCommandEnded;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.EnteringQuiescentState -= OnEnteringQuiescentState;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Event handlers to display the keyword list</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// (each of these handlers needs a separate function due to the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// signature, but they all do the same thing)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForAngle(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptAngleOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForCorner(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptPointOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForDistance(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptDistanceOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForDouble(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptDoubleOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForEntity(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptEntityOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForInteger(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptIntegerOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForKeyword(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptKeywordOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForNestedEntity(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptNestedEntityOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForPoint(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptPointOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForSelection(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptSelectionOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Nested selection sometimes happens (e.g. the HATCH command)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// so only display keywords when there are some to display</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (e.Options.Keywords.Count &gt; 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnPromptingForString(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> sender, <span style="color: #2b91af;">PromptStringOptionsEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; DisplayKeywords(e.Options.Keywords);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnCommandEnded(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">CommandEventArgs</span> e)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _window.ClearKeywords(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Event handlers to clear &amp; hide the keyword list</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">void</span> OnEnteringQuiescentState(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _window.ClearKeywords(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Helper to display our keyword list</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span> DisplayKeywords(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">KeywordCollection</span> kws</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// First we step through the keywords, collecting those</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// we want to display in a collection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sc = <span style="color: blue;">new</span> <span style="color: #2b91af;">StringCollection</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (kws != <span style="color: blue;">null</span> &amp;&amp; kws.Count &gt; 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Keyword</span> kw <span style="color: blue;">in</span> kws)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (kw.Enabled &amp;&amp; kw.Visible &amp;&amp; kw.GlobalName != <span style="color: #a31515;">&quot;dummy&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sc.Add(kw.LocalName); <span style="color: green;">// Expected this to be GlobalName</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If we don&#39;t have keywords to display, make sure the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// current list is cleared/hidden</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (sc.Count == 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _window.ClearKeywords(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Otherwise we pass the keywords - as a string array -</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// to the display function along with a flag indicating</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// whether the current command is considered &quot;special&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sa = <span style="color: blue;">new</span> <span style="color: blue;">string</span>[kws.Count];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sc.CopyTo(sa, 0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We should probably check for transparent/nested</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// command invocation...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cmd =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">string</span>)<span style="color: #2b91af;">Application</span>.GetSystemVariable(<span style="color: #a31515;">&quot;CMDNAMES&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _window.ShowKeywords(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sa, <span style="color: #2b91af;">Array</span>.IndexOf(specialCmds, cmd) &gt;= 0 <span style="color: green;">//, append</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> launchCommand(<span style="color: blue;">string</span> cmd)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.SendStringToExecute(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;_&quot;</span> + cmd + <span style="color: #a31515;">&quot; &quot;</span>, <span style="color: blue;">true</span>, <span style="color: blue;">false</span>, <span style="color: blue;">true</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>I was surprised that the English version of keywords on localized versions were accessible via the LocalName – rather than GlobalName – property. But apparently that’s how it works.</p>
<p>Next we have the XAML file for our KeywordWindow which, while invisible, contains the popup we’ll use to display the keywords. The file is called <em>KeywordWindow.xaml</em>.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: #a31515;">Window</span></p>
<p style="margin: 0px;">&#0160;<span style="color: red;"> x</span><span style="color: blue;">:</span><span style="color: red;">Class</span><span style="color: blue;">=&quot;KeywordHelper.KeywordWindow&quot;</span></p>
<p style="margin: 0px;">&#0160;<span style="color: red;"> xmlns</span><span style="color: blue;">=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span></p>
<p style="margin: 0px;">&#0160;<span style="color: red;"> xmlns</span><span style="color: blue;">:</span><span style="color: red;">x</span><span style="color: blue;">=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span></p>
<p style="margin: 0px;">&#0160;<span style="color: red;"> Title</span><span style="color: blue;">=&quot;KeywordWindow&quot;</span><span style="color: red;"> Height</span><span style="color: blue;">=&quot;0&quot;</span><span style="color: red;"> Width</span><span style="color: blue;">=&quot;0&quot;</span></p>
<p style="margin: 0px;">&#0160;<span style="color: red;"> WindowStyle</span><span style="color: blue;">=&quot;None&quot;</span><span style="color: red;"> ShowInTaskbar</span><span style="color: blue;">=&quot;False&quot;</span><span style="color: red;"> AllowsTransparency</span><span style="color: blue;">=&quot;True&quot;</span></p>
<p style="margin: 0px;">&#0160;<span style="color: red;"> Loaded</span><span style="color: blue;">=&quot;Window_Loaded&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">Window.Background</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">SolidColorBrush</span><span style="color: red;"> Opacity</span><span style="color: blue;">=&quot;0&quot;</span><span style="color: red;"> Color</span><span style="color: blue;">=&quot;White&quot;/&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Window.Background</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">Grid</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">Popup</span><span style="color: red;"> Name</span><span style="color: blue;">=&quot;KeywordPopup&quot;</span><span style="color: red;"> Placement</span><span style="color: blue;">=&quot;Custom&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">ListBox</span><span style="color: red;"> x</span><span style="color: blue;">:</span><span style="color: red;">Name</span><span style="color: blue;">=&quot;Keywords&quot;</span><span style="color: red;"> Width</span><span style="color: blue;">=&quot;100&quot;</span><span style="color: red;"> Height</span><span style="color: blue;">=&quot;auto&quot;&gt;</span>Keywords</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">ListBox.ItemContainerStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">Style</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">TargetType</span><span style="color: blue;">=&quot;{</span><span style="color: #a31515;">x</span><span style="color: blue;">:</span><span style="color: #a31515;">Type</span><span style="color: red;"> ListBoxItem</span><span style="color: blue;">}&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">BasedOn</span><span style="color: blue;">=&quot;{</span><span style="color: #a31515;">StaticResource</span><span style="color: blue;"> {</span><span style="color: #a31515;">x</span><span style="color: blue;">:</span><span style="color: #a31515;">Type</span><span style="color: red;"> ListBoxItem</span><span style="color: blue;">}}&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: #a31515;">EventSetter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">Event</span><span style="color: blue;">=&quot;MouseDoubleClick&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">Handler</span><span style="color: blue;">=&quot;ListBoxItem_MouseDoubleClick&quot;/&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: #a31515;">ListBox.ItemContainerStyle</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: #a31515;">ListBox</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Popup</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Grid</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: #a31515;">Window</span><span style="color: blue;">&gt;</span></p>
</div>
<p>And finally the C# code-behind, <em>KeywordWindow.xaml.cs</em>:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows.Controls;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows.Controls.Primitives;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows.Threading;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> KeywordHelper</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: gray;">///</span><span style="color: green;"> Interaction logic for KeywordWindow.xaml</span></p>
<p style="margin: 0px;">&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">partial</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">KeywordWindow</span> : <span style="color: #2b91af;">Window</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">DispatcherTimer</span> _t;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">DateTime</span> _lastOpened;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">bool</span> _special;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> KeywordWindow()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; InitializeComponent();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; KeywordPopup.CustomPopupPlacementCallback =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">CustomPopupPlacementCallback</span>(PlacePopup);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _t = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ShowKeywords(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span>[] keywords, <span style="color: blue;">bool</span> special = <span style="color: blue;">false</span>, <span style="color: blue;">bool</span> append = <span style="color: blue;">false</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Store the flag in a member variable so we can access it</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// from a lambda event handler</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _special = special;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the listbox contents</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> items = ((<span style="color: #2b91af;">ListBox</span>)KeywordPopup.Child).Items;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// The first test of difference is whether the number of items</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// is different</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (append)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> kw <span style="color: blue;">in</span> keywords)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; items.Add(kw);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">bool</span> different = keywords.Length != items.Count;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (!different)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If lists are the same length, check the contents</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// item by item</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; items.Count; i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> kw = keywords[i];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> item = (<span style="color: blue;">string</span>)items[i];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #2b91af;">String</span>.Compare(kw, item) != 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; different = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If the items are different, let&#39;s clear the list and</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// rebuild it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (different)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; items.Clear();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> kw <span style="color: blue;">in</span> keywords)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; items.Add(kw);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; KeywordPopup.IsOpen = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;re going to use a timer to close the popup in case</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// it isn&#39;t closed by one of the various callbacks we have</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// in place</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (_t == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Choose an interval of 2 seconds</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ts = <span style="color: blue;">new</span> <span style="color: #2b91af;">TimeSpan</span>(<span style="color: #2b91af;">TimeSpan</span>.TicksPerSecond * 2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _t = <span style="color: blue;">new</span> <span style="color: #2b91af;">DispatcherTimer</span> { Interval = ts };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _t.Tick += (s, e) =&gt;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If 2s or more has elapsed since the last popup</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// was displayed, close it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (_special &amp;&amp; (<span style="color: #2b91af;">DateTime</span>.Now - _lastOpened &gt;= ts))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; KeywordPopup.IsOpen = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _t.Start();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Record when the latest popup was displayed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _lastOpened = <span style="color: #2b91af;">DateTime</span>.Now;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ClearKeywords(<span style="color: blue;">bool</span> hide)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Optionally hide the popup</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; KeywordPopup.IsOpen = !hide;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Clear the keyword contents</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ((<span style="color: #2b91af;">ListBox</span>)KeywordPopup.Child).Items.Clear();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span> ListBoxItem_MouseDoubleClick(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">object</span> s, System.Windows.Input.<span style="color: #2b91af;">MouseButtonEventArgs</span> e</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// When an item is double-clicked, simply send it to the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// command-line with an underscore prefix</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> item = (<span style="color: #2b91af;">ListBoxItem</span>)s;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Commands</span>.launchCommand((<span style="color: blue;">string</span>)item.Content);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">CustomPopupPlacement</span>[] PlacePopup(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Size</span> popupSize, <span style="color: #2b91af;">Size</span> targetSize, <span style="color: #2b91af;">Point</span> offset</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We want to place the popup relative to the AutoCAD</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// main window</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> win =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.MainWindow;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Calculate the bottom-right of the popup - both x and y -</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// relative to the location of the parent window (this)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> x =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; win.DeviceIndependentLocation.X +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; win.DeviceIndependentSize.Width - <span style="color: blue;">this</span>.Left;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// 33 is the height of the bottom window border/status bar</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> y =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; win.DeviceIndependentLocation.Y +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; win.DeviceIndependentSize.Height - <span style="color: blue;">this</span>.Top - 33;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// The above values need scaling for DPI</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> s =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.Windows.<span style="color: #2b91af;">Window</span>.GetDeviceIndependentScale(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">IntPtr</span>.Zero</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get our scaled position, taking into account the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// size of the popip</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> p =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> System.Windows.<span style="color: #2b91af;">Point</span>(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">int</span>)(x * s.X - popupSize.Width),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">int</span>)(y * s.Y - popupSize.Height)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Return that position as our custom placement</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">new</span> <span style="color: #2b91af;">CustomPopupPlacement</span>[] {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">CustomPopupPlacement</span>(p, <span style="color: #2b91af;">PopupPrimaryAxis</span>.Vertical)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>So far I’ve had to code a few caveats for command behaviour: the HATCH command displays a selection prompt – without keywords – within a point prompt (which does have keywords). So I make sure we don’t clear the menu, in this case. Then there’s the MTEXT command, which performs a point selection for the window area – with keywords – before displaying its IPE (in-place editor). We use a timer to close the popup in the case that 500ms elapses without a request for keywords to be displayed. I have no doubt other commands will present other quirks, but we’ll address those as they crop up.</p>
<p>There’s still some work to do to hide the popup when AutoCAD is minimized – as well as <a href="http://stackoverflow.com/questions/357076/best-way-to-hide-a-window-from-the-alt-tab-program-switcher" target="_blank">to make sure our invisible window doesn’t appear in the Alt-Tab program switcher</a> – but that’s left for the reader (or for another day, we’ll see).</p>
<p>One additional requirement I do want to address – probably in the next post – is to automatically prefix underscores on unknown commands, to see if someone has entered an English command by mistake on a localization version of AutoCAD.</p>
