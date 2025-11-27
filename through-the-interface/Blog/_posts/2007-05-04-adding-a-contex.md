---
layout: "post"
title: "Adding a context menu to AutoCAD objects using .NET"
date: "2007-05-04 17:34:16"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2007/05/adding_a_contex.html "
typepad_basename: "adding_a_contex"
typepad_status: "Publish"
---

<p>It&#39;s been quite a week - between interviews for a DevTech position we&#39;re working to fill in Beijing and AU proposals (my team managed to pull together and submit nearly 60 API class proposals at the beginning of the week) life has been extremely hectic.</p>
<p>Thankfully we&#39;re now in the middle of the &quot;May Day Golden Week&quot; here in China. The Chinese government schedules three <a href="http://en.wikipedia.org/wiki/Golden_Week_(China)">Golden Weeks</a> every year - one for Spring Festival, one for Labour Day (also known as May Day) and one for the National Day. They&#39;re basically week-long holidays formed by a few standard holidays and mandating that people work through an adjacent weekend, grouping together the holidays with the days that are freed up into a contiguous week-long break. These weeks are designed to promote domestic tourism, by allowing people to plan vacations well in advance, and they seem to be working, apparently 25% of domestic tourism in China is due to these three Golden Weeks.</p>
<p>Anyway - I&#39;ve been working, on and off, as my team isn&#39;t all based in China, but I did get to spend some quality time with my family. All this to say it&#39;s been a week since my last post, so I&#39;m up late on Friday night to assuage my guilt. :-)</p>
<p>I threw some simple code together to show how to add your own custom context menu to a particular type of AutoCAD object using .NET. The below code adds a new context menu at the &quot;Entity&quot; level, which means that as long as only entities are selected in the editor, the context menu will appear. As objects have to be of type Entity to be selectable, I think it&#39;s safe to say the context menu will always be accessible. :-)</p>
<p>You could very easily modify the code to only show the menu for a concrete class of object (Lines, Circles, etc.), of course.</p>
<p>So what does this new context menu do? In this case it simply fires off a command, which then selects our entities by accessing the pickfirst selection set, and does something with the selected entities. The actual command I implemented was very simple, indeed: it simply counts the entities selected and writes a message to the command-line.</p>
<p>Here&#39;s the C# code:</p>
<p>&#0160;</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Windows;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> System;</p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">namespace</span> ContextMenuApplication</p>
<p style="font-size: 8pt; margin: 0px;">{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: teal;">Commands</span> : <span style="color: teal;">IExtensionApplication</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Initialize()</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">CountMenu</span>.Attach();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Terminate()</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">CountMenu</span>.Detach();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; [<span style="color: teal;">CommandMethod</span>(<span style="color: maroon;">&quot;COUNT&quot;</span>, <span style="color: teal;">CommandFlags</span>.UsePickSet)]</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> CountSelection()</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">Editor</span> ed =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: teal;">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">PromptSelectionResult</span> psr = ed.GetSelection();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (psr.Status == <span style="color: teal;">PromptStatus</span>.OK)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: maroon;">&quot;\nSelected {0} entities.&quot;</span>,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; psr.Value.Count</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: teal;">CountMenu</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: teal;">ContextMenuExtension</span> cme;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> Attach()</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;cme = <span style="color: blue;">new</span> <span style="color: teal;">ContextMenuExtension</span>();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">MenuItem</span> mi = <span style="color: blue;">new</span> <span style="color: teal;">MenuItem</span>(<span style="color: maroon;">&quot;Count&quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;mi.Click += <span style="color: blue;">new</span> <span style="color: teal;">EventHandler</span>(OnCount);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;cme.MenuItems.Add(mi);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">RXClass</span> rxc = <span style="color: teal;">Entity</span>.GetClass(<span style="color: blue;">typeof</span>(<span style="color: teal;">Entity</span>));</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">Application</span>.AddObjectContextMenuExtension(rxc, cme);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> Detach()</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">RXClass</span> rxc = <span style="color: teal;">Entity</span>.GetClass(<span style="color: blue;">typeof</span>(<span style="color: teal;">Entity</span>));</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">Application</span>.RemoveObjectContextMenuExtension(rxc, cme);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> OnCount(<span style="color: teal;">Object</span> o, <span style="color: teal;">EventArgs</span> e)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: teal;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: teal;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;doc.SendStringToExecute(<span style="color: maroon;">&quot;_.COUNT &quot;</span>, <span style="color: blue;">true</span>, <span style="color: blue;">false</span>, <span style="color: blue;">false</span>);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">}</p>
</div>
<p>And here&#39;s what we see when we have our application loaded, right-clicking after selecting some AutoCAD objects:</p>
<p><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/05/04/object_context_menu.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=331,height=502,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Object_context_menu" border="0" height="454" src="/assets/object_context_menu.png" title="Object_context_menu" width="300" /></a></p>
<p>Followed by the somewhat mundane result:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;">Selected 10 entities.</p>
</div>
<p><em><strong>Update:</strong></em></p>
<p>As an alternative, it&#39;s worth looking at <a href="http://through-the-interface.typepad.com/through_the_interface/2014/02/adding-a-context-menu-item-with-an-icon-in-autocad-using-net.html" target="_blank">these</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2014/02/adding-a-context-menu-item-with-an-icon-for-a-specific-autocad-object-type-using-net.html" target="_blank">posts</a>&#0160;which show how to add context menus for AutoCAD objects using CUI.</p>
