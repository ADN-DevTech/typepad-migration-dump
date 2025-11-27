---
layout: "post"
title: "Rolling back the effect of AutoCAD commands using .NET"
date: "2008-08-13 07:02:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Custom objects"
  - "Notification / Events"
original_url: "https://www.keanw.com/2008/08/rolling-back-th.html "
typepad_basename: "rolling-back-th"
typepad_status: "Publish"
---

<p><em>Another big thank you to Jeremy Tammik, from our DevTech team in Europe, for providing this elegant sample. This is another one Jeremy presented at the recent advanced custom entity workshop in Prague. I have added some initial commentary as well as some steps to see the code working. Jeremy also provided the code for <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/anchoring-autoc.html">the last post</a>.</em></p>

<p>We sometimes want to stop entities from being modified in certain ways, and there are a few different approaches possible, for instance: at the simplest - and least granular - level, we can place entities on locked layers or veto certain commands using an editor reactor.&nbsp; Or we can go all-out and implement custom objects that have complete control over their behaviour. The below technique provides a nice balance between control and simplicity: it makes use of a Document event to check when a particular command is being called, a Database event to cache the information we wish to restore and finally another Document event to restore it. In this case it's all about location (or should I say &quot;location, location, location&quot; ? :-). We're caching an object's state before the MOVE command (which changes an object's position in the model), but if we wanted to roll back the effect of other commands, we would probably want to cache other properties.</p>

<p>Here's the C# code:</p>

<div style="background: white none repeat scroll 0%; font-size: 8pt; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; color: black; font-family: courier new;"><p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> System.Diagnostics;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p><br /><p style="margin: 0px; font-size: 8pt;"><span style="color: blue;">namespace</span> Reactor</p>

<p style="margin: 0px; font-size: 8pt;">{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;">&nbsp;</span><span style="color: gray;">&lt;summary&gt;</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Reactor command.</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;">&nbsp;</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Demonstrate a simple object reactor, as well as</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> cascaded event handling.</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;">&nbsp;</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> In this sample, the MOVE command is cancelled for</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> all red circles. This is achieved by attaching an</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> editor reactor and watching for the MOVE command begin.</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> When triggered, the reactor attaches an object reactor</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> to the database and watches for red circles. If any are </span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> detected, their object id and original position are</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> stored. When the command ends, the positions are</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> restored and the object reactor removed again.</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;">&nbsp;</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> Reactors create overhead, so we should add them only</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> when needed and remove them as soon as possible</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;"> afterwards.</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: gray;">///</span><span style="color: green;">&nbsp;</span><span style="color: gray;">&lt;/summary&gt;</span></p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">CmdReactor</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Document</span> _doc;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">ObjectIdCollection</span> _ids =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Point3dCollection</span> _pts =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">new</span> <span style="color: #2b91af;">Point3dCollection</span>();</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;REACTOR&quot;</span>)]</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> Reactor()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;_doc =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;_doc.CommandWillStart +=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(doc_CommandWillStart);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">void</span> doc_CommandWillStart(</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">object</span> sender,</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">CommandEventArgs</span> e</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; )</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">if</span> (e.GlobalCommandName == <span style="color: #a31515;">&quot;MOVE&quot;</span>)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _ids.Clear();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _pts.Clear();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _doc.Database.ObjectOpenedForModify +=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectEventHandler</span>(_db_ObjectOpenedForModify);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _doc.CommandCancelled +=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(_doc_CommandEnded);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _doc.CommandEnded +=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(_doc_CommandEnded);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _doc.CommandFailed +=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(_doc_CommandEnded);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">void</span> removeEventHandlers()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;_doc.CommandCancelled -=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(_doc_CommandEnded);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;_doc.CommandEnded -=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(_doc_CommandEnded);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;_doc.CommandFailed -=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CommandEventHandler</span>(_doc_CommandEnded);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;_doc.Database.ObjectOpenedForModify -=</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectEventHandler</span>(_db_ObjectOpenedForModify);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">void</span> _doc_CommandEnded(</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">object</span> sender,</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">CommandEventArgs</span> e</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; )</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: green;">// Remove database reactor before restoring positions</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;removeEventHandlers();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;rollbackLocations();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">void</span> _db_ObjectOpenedForModify(</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">object</span> sender,</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectEventArgs</span> e</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; )</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Circle</span> circle = e.DBObject <span style="color: blue;">as</span> <span style="color: #2b91af;">Circle</span>;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">if</span> (<span style="color: blue;">null</span> != circle &amp;&amp; 1 == circle.ColorIndex)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// In AutoCAD 2007, OpenedForModify is called only</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// once by MOVE.</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// In 2008, OpenedForModify is called multiple</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// times by the MOVE command ... we are only</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// interested in the first call, because </span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// in the second one, the object location</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: green;">// has already been changed:</span></p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">if</span> (!_ids.Contains(circle.ObjectId))</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; _ids.Add(circle.ObjectId);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; _pts.Add(circle.Center);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p><br /><p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; <span style="color: blue;">static</span> <span style="color: blue;">void</span> rollbackLocations()</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Debug</span>.Assert(</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _ids.Count == _pts.Count,</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;Expected same number of ids and locations&quot;</span></p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> t =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _doc.Database.TransactionManager.StartTransaction();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: blue;">using</span> (t)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">int</span> i = 0;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="color: blue;">in</span> _ids)</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Circle</span> circle =</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;t.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForWrite) <span style="color: blue;">as</span> <span style="color: #2b91af;">Circle</span>;</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; circle.Center = _pts[i++];</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; t.Commit();</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; &nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">&nbsp; }</p>

<p style="margin: 0px; font-size: 8pt;">}</p></div>

<p>To see the code at work, draw some circles and make some of them red:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Lots%20of%20circles.png"><img width="426" height="305" border="0" src="/assets/Lots%20of%20circles_thumb.png" alt="Lots of circles" style="border-width: 0px;" /></a> </p>

<p>Now run the REACTOR command and try to MOVE all the circles:</p>

<p> <a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Moving%20the%20circles.png"><img width="420" height="322" border="0" src="/assets/Moving%20the%20circles_thumb.png" alt="Moving the circles" style="border-width: 0px;" /></a> </p>

<p>Although all the circles are dragged during the move, once we complete the command we can see that the red circles have remained in the same location (or have, in fact, had their location rolled back). The other circles have been moved, as expected.</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Circles%20post-move.png"><img width="422" height="338" border="0" src="/assets/Circles%20post-move_thumb.png" alt="Circles post-move" style="border-width: 0px;" /></a></p>
