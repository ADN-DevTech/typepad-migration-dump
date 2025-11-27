---
layout: "post"
title: "Blocking AutoCAD commands from .NET"
date: "2006-10-31 13:31:29"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Notification / Events"
  - "Runtime"
original_url: "https://www.keanw.com/2006/10/blocking_autoca.html "
typepad_basename: "blocking_autoca"
typepad_status: "Publish"
---

<p><em>Thanks to Viru Aithal from the DevTech team in India team for this idea (he reminded me of this technique in a recent ADN support request he handled).</em></p>

<p><strong>A quick disclaimer:</strong> the technique shown in this entry could really confuse your users, if implemented with inadequate care. Please use it for good, not evil.</p>

<p>I talked at some length in previous posts about MDI in AutoCAD, and how various commands lock documents when they need to work on them. When commands try to lock (or unlock) a document, an event gets fired. You can respond to this event in your AutoCAD .NET module, asking AutoCAD to veto the operation requesting the lock.</p>

<p>For some general background on events in .NET, I’d suggest checking out this information on MSDN, both <a href="http://msdn.microsoft.com/library/en-us/cpguide/html/cpconeventsdelegates.asp?frame=true">from the Developer’s Guide</a> and <a href="http://msdn.microsoft.com/msdnmag/issues/03/02/BasicInstincts/">from MSDN Magazine</a>. To see how they work in AutoCAD, take a look at the EventsWatcher sample on the ObjectARX SDK, under samples/dotNet/EventsWatcher. This is a great way to see what information is provided via AutoCAD events.</p>

<p>It seems that almost every command causes the DocumentLockModeChanged event to be fired – I would say <em>every</em> command, but I can imagine there being commands that don’t trigger it, even if the technique appears to work even for commands that have no reason to lock the current document, such as HELP.</p>

<p>As you can imagine, having something block commands that a user expects to use could result in a serious drop in productivity, but there are genuine cases where you might have a legitimate need to disable certain product functionality, such as to drive your users through a more automated approach to performing the same task. This is not supposed to act as a fully-fledged security layer – an advanced user could easily prevent a module from being loaded, which invalidates this technique – but it could still be used to help stop the majority of users from doing something that might (for instance) break the drawing standards implemented in their department.</p>

<p>So, let’s take a look at what needs to happen to define and register our event handler.</p>

<p>Firstly, we must declare a callback function somewhere in our code that we will register as an event handler. In this case we’re going to respond to the DocumentLockModeChanged event, and so will take as one of our arguments an object of type DocumentLockModeChangedEventArgs:</p>

<p>VB.NET:</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">Private</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">Sub</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> vetoLineCommand (</span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">ByVal</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> sender </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">As</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">Object</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">, _</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">ByVal</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> e </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">As</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> DocumentLockModeChangedEventArgs)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">If</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (e.GlobalCommandName = &quot;LINE&quot;) </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">Then</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; e.Veto()</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">End</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">If</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">End</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">Sub</span></p>

<p>C#:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> vetoLineCommand(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">DocumentLockModeChangedEventArgs</span> e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> (e.GlobalCommandName == <span style="COLOR: maroon">&quot;LINE&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; e.Veto();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p></div>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt">This callback function, named vetoLineCommand(), simply checks whether the command that is requesting the change in document lock mode is the LINE command, and, if so, it then vetoes it. In our more complete sample, later on, we’ll maintain a list of commands to veto, which can be manipulated by the user using come commands we define (you will <strong>not</strong> want to do this in your application – this is for your own testing during development).</p>

<p>Next we need to register the command. This event belongs to the DocumentManager object, but the technique for registering events is different between VB.NET and C#. In VB.NET you use the AddHandler() keyword, referring to the DocumentLockModeChanged event from the DocumentManager object – in C# you add it directly to the DocumentLockModeChanged property. This could be done from a command or from the Initialize() function, as I’ve done in the complete sample further below.</p>

<p>VB.NET:</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span lang="FR" style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR">Dim</span><span lang="FR" style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR"> dm </span><span lang="FR" style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR">As</span><span lang="FR" style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR"> DocumentCollection = Application.DocumentManager()</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">AddHandler</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> dm.DocumentLockModeChanged, </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">AddressOf</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> vetoLineCommand</span></p>

<p>C#:</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: teal; FONT-FAMILY: &quot;Courier New&quot;">DocumentCollection</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> dm = </span><span style="FONT-SIZE: 8pt; COLOR: teal; FONT-FAMILY: &quot;Courier New&quot;">Application</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">.DocumentManager;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">dm.DocumentLockModeChanged += </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">new</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> </span><span style="FONT-SIZE: 8pt; COLOR: teal; FONT-FAMILY: &quot;Courier New&quot;">DocumentLockModeChangedEventHandler</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">(vetoLineCommand);</span></p>

<p>That's really all there is to it... as mentioned, the complete sample maintains a list of commands that are currently being vetoed. I've made this shared/static, and so it will be shared across documents (so if you try to modify this in concurrent drawings, you might get some interesting results).</p>

<p>Here's the complete sample (just to reiterate - use this technique both at your own risk and with considerable caution).</p>

<p>VB.NET:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System.Collections.Specialized</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Namespace</span> VetoTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> VetoCmds</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Implements</span> IExtensionApplication</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Shared</span> cmdList <span style="COLOR: blue">As</span> StringCollection _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;= <span style="COLOR: blue">New</span> StringCollection</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Initialize() _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Implements</span> IExtensionApplication.Initialize</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> dm <span style="COLOR: blue">As</span> DocumentCollection</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dm = Application.DocumentManager()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">AddHandler</span> dm.DocumentLockModeChanged, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">AddressOf</span> vetoCommandIfInList</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Terminate() _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Implements</span> IExtensionApplication.Terminate</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> dm <span style="COLOR: blue">As</span> DocumentCollection</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dm = Application.DocumentManager()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">RemoveHandler</span> dm.DocumentLockModeChanged, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">AddressOf</span> vetoCommandIfInList</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;ADDVETO&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> AddVeto()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; = Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> pr <span style="COLOR: blue">As</span> PromptResult</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pr = ed.GetString(vbLf + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Enter command to veto: &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (pr.Status = PromptStatus.OK) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">If</span> (cmdList.Contains(pr.StringResult.ToUpper())) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(vbLf + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;Command already in veto list.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cmdList.Add(pr.StringResult.ToUpper())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;vbLf + <span style="COLOR: maroon">&quot;Command &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pr.StringResult.ToUpper() + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot; added to veto list.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;LISTVETOES&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> ListVetoes()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; = Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Commands currently on veto list: &quot;</span> + vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">For</span> <span style="COLOR: blue">Each</span> str <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> <span style="COLOR: blue">In</span> cmdList</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(str + vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Next</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;REMVETO&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> RemoveVeto()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ListVetoes()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; = Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> pr <span style="COLOR: blue">As</span> PromptResult</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pr = ed.GetString( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Enter command to remove from veto list: &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (pr.Status = PromptStatus.OK) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">If</span> (cmdList.Contains(pr.StringResult.ToUpper())) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cmdList.Remove(pr.StringResult.ToUpper())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(vbLf + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;Command not found in veto list.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> <span style="COLOR: blue">Sub</span> vetoCommandIfInList(<span style="COLOR: blue">ByVal</span> sender <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span>, _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">ByVal</span> e <span style="COLOR: blue">As</span> DocumentLockModeChangedEventArgs)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (cmdList.Contains(e.GlobalCommandName.ToUpper())) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.Veto()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Namespace</span></p></div>

<p>C#:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Collections.Specialized;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> VetoTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">VetoCmds</span> : <span style="COLOR: teal">IExtensionApplication</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">StringCollection</span> cmdList =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="COLOR: teal">StringCollection</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentCollection</span> dm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dm.DocumentLockModeChanged += <span style="COLOR: blue">new</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">DocumentLockModeChangedEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vetoCommandIfInList</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentCollection</span> dm;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dm = <span style="COLOR: teal">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;dm.DocumentLockModeChanged -= <span style="COLOR: blue">new</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">DocumentLockModeChangedEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vetoCommandIfInList</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;ADDVETO&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> AddVeto()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptResult</span> pr = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetString(<span style="COLOR: maroon">&quot;\nEnter command to veto: &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (cmdList.Contains(pr.StringResult.ToUpper()))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nCommand already in veto list.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cmdList.Add(pr.StringResult.ToUpper());</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nCommand &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pr.StringResult.ToUpper() +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot; added to veto list.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;LISTVETOES&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ListVetoes()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;Commands currently on veto list:\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: blue">string</span> str <span style="COLOR: blue">in</span> cmdList)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(str + <span style="COLOR: maroon">&quot;\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;REMVETO&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> RemoveVeto()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ListVetoes();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptResult</span> pr;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetString(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;Enter command to remove from veto list: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (cmdList.Contains(pr.StringResult.ToUpper()))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cmdList.Remove(pr.StringResult.ToUpper());</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nCommand not found in veto list.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> vetoCommandIfInList(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentLockModeChangedEventArgs</span> e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (cmdList.Contains(e.GlobalCommandName.ToUpper()))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.Veto();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>In terms of usage, it should be simple enough to work out while playing with it:</p>

<ul><li>ADDVETO adds commands to the veto list</li>

<li>LISTVETOES lists the commands on the veto list</li>

<li>REMVETO removes commands from the veto list</li></ul>

<p>These commands are really only intended for you to try out different commands on the veto list, to see what happens. If you need to control use of command(s) from your application, you should not allow your users to manipulate the list of blocked commands themselves.</p>
