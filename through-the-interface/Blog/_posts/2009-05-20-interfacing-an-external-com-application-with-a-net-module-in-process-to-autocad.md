---
layout: "post"
title: "Interfacing an external COM application with a .NET module in-process to AutoCAD"
date: "2009-05-20 10:55:06"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Runtime"
original_url: "https://www.keanw.com/2009/05/interfacing-an-external-com-application-with-a-net-module-in-process-to-autocad.html "
typepad_basename: "interfacing-an-external-com-application-with-a-net-module-in-process-to-autocad"
typepad_status: "Publish"
---

<p>This question came in recently by email from Michael Fichter of Superstructures Engineers and Architects:</p>
<blockquote>
<p><em>Could you suggest an approach that would enable me to drive a .NET function (via COM) that could return a value from .NET back to COM? </em><em>I have used SendCommand in certain instances where return values were not needed.</em></p></blockquote>
<p>Michael’s referring to a technique used in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/12/launching-autoc.html">this previous post</a>, which shows how to launch AutoCAD from a .NET executable via COM and then launch a command which can then safely interface with AutoCAD in-process via its managed API.</p>
<p>And yes, this technique is fine if you don’t want to return results, but has limitations if you do. You could populate AutoCAD user variables or create a file for the calling application to read but such approaches are cumbersome.</p>
<p>So… in spite of my initial doubtful reaction I decided to give it a try. Here are the steps I used to get this working…</p>
<p>First we create a Class Library for our in-process component with references to the usual acmgd.dll and acdbmgd.dll assemblies, adding the following C# code:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Runtime.InteropServices;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> LoadableComponent</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ProgId</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LoadableComponent.Commands&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A simple test command, just to see that commands</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// are loaded properly from the assembly</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;MYCOMMAND&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> MyCommand()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nTest command executed.&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A function to add two numbers and create a</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// circle of that radius. It returns a string</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// withthe result of the addition, just to use</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// a different return type</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> AddNumbers(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> arg1, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">double</span><span style="LINE-HEIGHT: 140%"> arg2)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// During tests it proved unreliable to rely</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// on DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (which was null) so we will go from the</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// HostApplicationServices&#39; WorkingDatabase</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span><span style="LINE-HEIGHT: 140%"> db =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">HostApplicationServices</span><span style="LINE-HEIGHT: 140%">.WorkingDatabase;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.GetDocument(db);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Perform our addition</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">double</span><span style="LINE-HEIGHT: 140%"> res = arg1 + arg2;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Lock the document before we access it</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DocumentLock</span><span style="LINE-HEIGHT: 140%"> loc = doc.LockDocument();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (loc)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create our circle</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%"> cir =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Circle</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Point3d</span><span style="LINE-HEIGHT: 140%">(0, 0, 0),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Vector3d</span><span style="LINE-HEIGHT: 140%">(0, 0, 1),</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cir.SetDatabaseDefaults(db);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add it to the current space</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BlockTableRecord</span><span style="LINE-HEIGHT: 140%"> btr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BlockTableRecord</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.CurrentSpaceId,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(cir);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(cir, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Commit the transaction</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Return our string result</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> res.ToString();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>You’ll see we mark our Commands class as having the ProgId of “LoadableComponent.Commands” (this doesn’t have to follow the namespace.class-name convention, if you’d rather use something else). Be sure to edit the AssemblyInfo.cs file to make sure the ComVisible assembly attribute is set to true (the default is false), otherwise no classes will be exposed via COM.</p>
<p>The code is mostly pretty simple… it includes a command just to make sure commands are registered when the assembly loads. The AddNumbers() function uses a slightly different technique to get the working database and its document, mainly because I found MdiActiveDocument to be null when I needed it. I suspect this is simply a timing issue, and that if AutoCAD had the time to fully initialize we wouldn’t have to code this defensively. There may well be a clean way to wait for this to happen (comments, anyone?).</p>
<p>Once we’ve built the assembly it needs to be registered via COM. The way I tend to do this is via a “Visual Studio Command Prompt” (which has the path set nicely to call the VS development tools). I browse to the location of my assembly and then run “regasm LoadableComponent.dll” (you can specify the optional /reg parameter if you’d rather create a .reg file rather than modifying the Registry directly).</p>
<p>Now we can create an executable project to drive this component with COM references to the AutoCAD Type Library (I’m using the one for AutoCAD 2010) and the AutoCAD/ObjectDBX Common Type Library (AutoCAD 2010’s is version 18.0), as well as a reference to our .NET assembly (which I have called LoadableComponent.dll).</p>
<p>Inside the default form created with the executable project we can add a button behind which we copy the code in the post referred to earlier, adding some logic to load our component and dynamically execute its AddNumbers() function:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Interop;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Windows.Forms;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Runtime.InteropServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Reflection;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> LoadableComponent;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> DrivingAutoCAD</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">partial</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Form1</span><span style="LINE-HEIGHT: 140%"> : </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Form</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> Form1()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; InitializeComponent();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> button1_Click(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> sender, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">EventArgs</span><span style="LINE-HEIGHT: 140%"> e)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">const</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> progID = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AutoCAD.Application.18&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadApplication</span><span style="LINE-HEIGHT: 140%"> acApp = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acApp =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadApplication</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Marshal</span><span style="LINE-HEIGHT: 140%">.GetActiveObject(progID);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Type</span><span style="LINE-HEIGHT: 140%"> acType =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Type</span><span style="LINE-HEIGHT: 140%">.GetTypeFromProgID(progID);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acApp =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">AcadApplication</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Activator</span><span style="LINE-HEIGHT: 140%">.CreateInstance(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acType,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">MessageBox</span><span style="LINE-HEIGHT: 140%">.Show(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Cannot create object of type \&quot;&quot;</span><span style="LINE-HEIGHT: 140%"> +</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; progID + </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\&quot;&quot;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (acApp != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// By the time this is reached AutoCAD is fully</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// functional and can be interacted with through code</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acApp.Visible = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> app =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acApp.GetInterfaceObject(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LoadableComponent.Commands&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (app != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s generate the arguments to pass in:</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// an integer and a double</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%">[] args = { 5, 6.3 };</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now let&#39;s call our method dynamically</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> res =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; app.GetType().InvokeMember(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AddNumbers&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">BindingFlags</span><span style="LINE-HEIGHT: 140%">.InvokeMethod,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; app,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; args</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%"></span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acApp.ZoomAll();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%"></span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">MessageBox</span><span style="LINE-HEIGHT: 140%">.Show(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">this</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;AddNumbers returned: &quot;</span><span style="LINE-HEIGHT: 140%"> + res.ToString()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Exception</span><span style="LINE-HEIGHT: 140%"> ex)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">MessageBox</span><span style="LINE-HEIGHT: 140%">.Show(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">this</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Problem executing component: &quot;</span><span style="LINE-HEIGHT: 140%"> +</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ex.Message</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>I decided to try Application.GetInterfaceObject() – the classic way to load an old VB6 ActiveX DLL into AutoCAD from VBA or Visual LISP – to see whether it worked for .NET assemblies that have a ProgId assigned. It not only worked, but the commands contained within the module were registered properly. A nice surprise! :-)</p>
<p>I started by defining an interface in the Class Library to be used in the Executable, but ended up going with a more dynamic approach, using InvokeMember() on the class of the object returned by GetInterfaceObject(). This avoids having to define and cast to the interface but adds a little uncertainty to the operation (as I’ve mentioned a number of times in recent weeks when we start getting dynamic we lose most of the compiler crutches we’ve all become used to :-). I also hit a problem if the command function was declared as static, but presumably that can be resolved with the right arguments to InvokeMember().</p>
<p>When we run this code and select the button, we see a circle get created with the radius of 11.3, the result of adding the integer (5) and double (6.3) we passed to the AddNumbers() function:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201157098ffa3970b-pi"><img alt="Result of driving AutoCAD via in- and out-of-proc code" border="0" height="391" src="/assets/image_733256.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Result of driving AutoCAD via in- and out-of-proc code" width="485" /></a></p>
<p>The combination of COM for out-of-process control with .NET for in-process power and performance will hopefully be a useful technique for many of you needing to automate AutoCAD from an external executable. Be sure to post comments if any of you have things to share on this topic. Thanks for the question, Michael! :-)</p>
<p><em><strong>Update:</strong></em></p>
<p>A much-improved implementation of this application can be found in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/interfacing-an-external-com-application-with-a-net-module-in-process-to-autocad-redux.html">this post</a>.</p>
