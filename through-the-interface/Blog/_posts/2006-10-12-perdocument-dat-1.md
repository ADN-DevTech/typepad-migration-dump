---
layout: "post"
title: "Per-document data in AutoCAD .NET applications - Part 1"
date: "2006-10-12 18:12:08"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2006/10/perdocument_dat_1.html "
typepad_basename: "perdocument_dat_1"
typepad_status: "Publish"
---

<p>The last few posts have focused on the <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/some_background.html">history of MDI in AutoCAD</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/perdocument_dat.html">how to store per-document data in ObjectARX applications</a>.&nbsp; Now let’s take a look at what can be done for AutoCAD .NET applications...</p>

<p>There are two main approaches for storing per-document data in managed applications loaded into AutoCAD – I’ll take a look at the first in this entry and tackle the second technique next time.</p>

<p><strong>Define commands as instance members of a class</strong></p>

<p>In managed applications you can declare instance or static methods. Static methods are those specified as “static” in C# or “Shared” in VB.NET. Methods that are not shared/static are known as instance methods.</p>

<p>Static methods are “global” – they do not use data that is specific to an instance of the class they belong to. It’s common to call static methods directly from the class namespace (MyClass.myMethod()) rather than from specific instance of the class (myObject.myMethod()). To declare a method as static, you use the “static” keyword in C# and the “Shared” keyword in VB.NET.</p>

<p>Predictably enough, MSDN contains some good information on <a href="http://msdn2.microsoft.com/en-us/library/79b3xss3.aspx">static classes and static class members</a>.</p>

<p>Instance methods can be considered “local” in scope – they work on a specific instance of a class. A method not declared as static/Shared will be an instance method by default.</p>

<p>So what does this mean for us, as implementers of AutoCAD commands in .NET?</p>

<p>Here’s what the documentation says:</p><blockquote dir="ltr"><p>&quot;For an instance command method, the method's enclosing type is instantiated separately for each open document. This means that each document gets a private copy of the command's instance data. Thus there is no danger of overwriting document-specific data when the user switches documents. If an instance method needs to share data globally, it can do so by declaring static or Shared member variables.&quot;</p></blockquote><p>So to get the benefits of per-document data in a .NET application, you simply need to declare the relevant command(s) as instance (non-static). Here’s some sample C# code defining two separate commands: one is declared as static (accessing static data and even contained within a static class) while the other is an instance method:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[assembly: <span style="COLOR: teal">CommandClass</span>(<span style="COLOR: blue">typeof</span>(CommandClasses.<span style="COLOR: teal">FirstClass</span>))]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[assembly: <span style="COLOR: teal">CommandClass</span>(<span style="COLOR: blue">typeof</span>(CommandClasses.<span style="COLOR: teal">SecondClass</span>))]</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> CommandClasses</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">FirstClass</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">int</span> counter = 0;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [Autodesk.AutoCAD.Runtime.<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;glob&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> global()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nCounter value is: &quot;</span> + counter++);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">SecondClass</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">int</span> counter = 0;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [Autodesk.AutoCAD.Runtime.<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;loc&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> local()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nCounter value is: &quot;</span> + counter++);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Here’s what happens when you execute the two commands in two separate documents:</p>

<p>[From first drawing...]</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">glob</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">glob</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">glob</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">loc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">loc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">loc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">new</span></p></div>

<p>[From second drawing...]</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">glob</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 3</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">glob</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 4</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">glob</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 5</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">loc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">loc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">loc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Counter value is: 2</p></div>

<p>So you can see that when you create and switch to a new document, the “glob” command (a static method) continues counting from where it left off. The “loc” command (an instance method) starts from zero, as a new instance of its command class has been created.</p>

<p>If you choose to mix static and instance methods &amp; data in your class, then you will need to be a little careful about making sure the data gets initialized at the right time and place. In the above example I’ve kept things separate and have simply used variable initialization to specify the initial values for the two counters – you will probably want to define your own constructors (<a href="http://msdn2.microsoft.com/en-us/library/k9x6w0hc.aspx">static</a> or <a href="http://msdn2.microsoft.com/en-us/library/k6sa6h87.aspx">instance</a>) if you have more complex scenarios to deal with.</p>

<p>In my next post I’ll take a look at the Document.UserData container, the alternative approach to managing per-document data in an AutoCAD .NET application.</p>
