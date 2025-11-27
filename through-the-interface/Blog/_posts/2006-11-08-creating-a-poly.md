---
layout: "post"
title: "Controlling interactive polyline creation - Part 1"
date: "2006-11-08 15:26:45"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoLISP / Visual LISP"
original_url: "https://www.keanw.com/2006/11/creating_a_poly.html "
typepad_basename: "creating_a_poly"
typepad_status: "Publish"
---

<p>I received this interesting question through by email over the weekend:</p><blockquote dir="ltr"><p><em>“How can I ask AutoCAD to let the user to draw a Polyline (just like the user pushed Polyline button) and then just after finishing drawing get the ObjectID of that entity? Is it possible?”</em></p></blockquote><p>This is a fun one, as there are a number of different approaches to take. I’m going to outline (or just go ahead and implement, depending on the complexity) the various possibilities – taking the first two today and the others in (a) subsequent post(s).</p>

<p>The idea is to define our own command, say MYPOLY, and make sure we’re left with execution control – and the object ID/entity name – after the user has defined a polyline in the drawing window.</p>

<p>There are two basic ways to solve this problem, and each of these has two variants. The initial (and major) choice is whether to let the standard AutoCAD PLINE command provide the user-interface for creating the polyline. Doing so is certainly simpler, assuming you want the user to have access to all the polyline options. That said, you may actually prefer to limit the user’s options (for example, not to allow width or arc segments), in which case the approach to implement the UI yourself would be better suited.</p>

<p>So, now to tackle the first two options...</p>

<p>From the MYPOLY command, we want to call the PLINE command. Once the command has completed, we want to make sure our code is being executed, which will allow us to get the polyline's object ID.</p>

<p>This is where we get our next choice: how to find out when the PLINE command has ended. The first option (and the one typically used from Visual LISP for this type of task) is to loop until the command is finished, checking either CMDACTIVE or CMDNAMES. This is important, as polylines can have an arbitrary number of vertices, so we don’t know exactly how long the command will take to complete (in terms of how many “pauses” the command will have, requesting a point selection from the user).</p>

<p>Here’s how I’d do this in LISP (the technique is published on the ADN site in this DevNote: <a href="http://adn.autodesk.com/adn/servlet/devnote?siteID=4814862&amp;id=5407851&amp;linkID=4900509">Waiting for (command) to finish in AutoLISP</a>):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">(defun C:MYPOLY()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (command &quot;_.PLINE&quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (while (= (getvar &quot;CMDNAMES&quot;) &quot;PLINE&quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (command pause)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ &quot;\nEntity name of polyline: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ (entlast))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">)</p></div>

<p>And here's what happens when we execute this code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">mypoly</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">_.PLINE</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify start point:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Current line-width is 0.0000</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Arc/Halfwidth/Length/Undo/Width]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Arc/Close/Halfwidth/Length/Undo/Width]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Arc/Close/Halfwidth/Length/Undo/Width]: <span style="COLOR: red">a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify endpoint of arc or</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[Angle/CEnter/CLose/Direction/Halfwidth/Line/Radius/Second pt/Undo/Width]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify endpoint of arc or</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[Angle/CEnter/CLose/Direction/Halfwidth/Line/Radius/Second pt/Undo/Width]:</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Entity name of polyline: &lt;Entity name: 7ef90048&gt;</p></div>

<p>The second option to wait for the command to complete is to register a callback handling the CommandEnded() event.</p>

<p>Here’s some C# code showing this approach:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> MyPlineApp</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">MyPlineCmds</span> : <span style="COLOR: teal">IExtensionApplication</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Flag used to check whether it's our command</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// that launched PLINE</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">bool</span> myCommandStarted = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;doc.CommandEnded += <span style="COLOR: blue">new</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">CommandEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; plineCommandEnded</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;doc.CommandEnded -= <span style="COLOR: blue">new</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">CommandEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; plineCommandEnded</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;MYPOLY&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> MyPoly()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Set the flag and launch PLINE</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;myCommandStarted = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;doc.SendStringToExecute(<span style="COLOR: maroon">&quot;_PLINE &quot;</span>,<span style="COLOR: blue">false</span>,<span style="COLOR: blue">false</span>,<span style="COLOR: blue">false</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> plineCommandEnded(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">CommandEventArgs</span> e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (myCommandStarted</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &amp;&amp; e.GlobalCommandName.ToUpper() == <span style="COLOR: maroon">&quot;PLINE&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// We're just performing a simple check, so OK..</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// We could launch a follow-on command, if needed</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">PromptSelectionResult</span> lastRes =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; doc.Editor.SelectLast();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (lastRes.Value != <span style="COLOR: blue">null</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &amp;&amp; lastRes.Value.Count == 1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; doc.Editor.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nPolyline entity is: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;+ lastRes.Value[0].ObjectId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; myCommandStarted = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>And here's what happens when we execute this code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">mypoly</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify start point:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Current line-width is 0.0000</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Arc/Halfwidth/Length/Undo/Width]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Arc/Close/Halfwidth/Length/Undo/Width]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Arc/Close/Halfwidth/Length/Undo/Width]: <span style="COLOR: red">a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify endpoint of arc or</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[Angle/CEnter/CLose/Direction/Halfwidth/Line/Radius/Second pt/Undo/Width]:</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">Polyline entity is: (2130247840)</p></div>

<p>That's where I'll stop it there for now… the other two options I want to look at both revolve around defining your own user-interface. The first will simply collect a sequence of points from the user using GetPoint(), the second uses a Jig to do the same thing (I haven’t yet decided whether to actually implement this last one or not – we’ll see how much time I have later in the week).</p>
