---
layout: "post"
title: "Some cool copy/paste tools for Visual Studio"
date: "2006-08-03 21:50:58"
author: "Kean Walmsley"
categories:
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/08/some_cool_copyp.html "
typepad_basename: "some_cool_copyp"
typepad_status: "Publish"
---

<p>I won't tell you just how bored I've been getting while &quot;post-processing&quot; the HTML created when I copy/paste code from Visual Studio into this blog's editing interface. My aim is simply to maintain a certain amount of the formatting provided in Visual Studio (font, syntax colouring and indentation would be great, but I end up settling for the last two, and only then after a lot of HTML editing).</p>

<p>So I decided to take a look at what tools might be out there on the web to help with this. The first tool I found wasn't what I wanted, but was so cool that I thought I'd mention it anyway (ah, the joy of the internet! :-).</p>

<p><strong>Copy C#, Paste VB!</strong></p>

<p>It's a Visual Studio Add-In that allows you to copy C# code into the clipboard and &quot;Paste As Visual Basic&quot; into a .vb document. It allows you to choose between a couple of translator web services (although so far it only does C#-&gt;VB, while I'd also make strong use of a version that goes the other way).</p>

<p><a href="http://msdn.microsoft.com/msdnmag/issues/06/02/PasteAs/">http://msdn.microsoft.com/msdnmag/issues/06/02/PasteAs/</a></p>

<p>A couple of tips to get this working:</p>

<ul><li>You will only be able to use Visual Studio versions that support AddIns (the Express versions do not, for instance)</li>

<li>You will find an installer inside the <a href="http://download.microsoft.com/download/f/2/7/f279e71e-efb0-4155-873d-5554a0608523/PasteAs.exe">downloadable archive</a> file:<ul><li><em>PasteAs\PasteAsVBSetup\Debug\setup.exe</em></li></ul></li>

<li>Once installed, you should find a &quot;Paste as Visual Basic&quot; menu item added to the Edit menu<ul><li>This will only be added if there is text in the clipboard and you are inside a .vb file</li>

<li><a onclick="window.open(this.href, '_blank', 'width=239,height=519,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/pasteasvbmenu.png"><img title="Pasteasvbmenu" height="217" alt="Pasteasvbmenu" src="/assets/pasteasvbmenu.png" width="100" border="0" /></a> </li></ul></li>

<li>I found the menu item did nothing whatsoever to start with, until I copied these files from where they were installed into <em>C:\My Documents\Visual Studio 2005\Addins</em><ul><li><em>PasteAsVB.dll, PasteAsVBAddin.dll</em></li></ul></li></ul>

<p>You will know it is working if you see a dialog appear:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=571,height=475,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/pasteasvbdialog.png"><img title="Pasteasvbdialog" height="83" alt="Pasteasvbdialog" src="/assets/pasteasvbdialog.png" width="100" border="0" /></a> </p>

<p>As for the code it creates, here's an example of a C# code snippet from the &quot;prompts&quot; sample of the ObjectARX SDK, and the VB.NET output:</p>

<p><em>C# Input</em></p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> Prompts</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">PromptsTest</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">PromptAngleOptions</span> useThisAngleOption;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">PromptDoubleResult</span> useThisAngleResult;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">PromptPointOptions</span> useThisPointOption;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">PromptPointResult</span> useThisPointResult;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">PromptEntityOptions</span> useThisEntityOption;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: teal">PromptEntityResult</span> useThisEntityResult;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//A small function that shows how to prompt for an integer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;GetInteger&quot;</span>)] </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> integerTest()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptIntegerOptions</span> opt0 = <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptIntegerOptions</span>(<span style="COLOR: maroon">&quot;Enter your age&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.AllowNegative = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.AllowNone = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.AllowZero = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.DefaultValue = 1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptIntegerResult</span> IntRes = ed.GetInteger(opt0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span>(IntRes.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: blue">string</span>.Format(<span style="COLOR: maroon">&quot;\nYou entered {0}&quot;</span>,IntRes.Value));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p><em>VB.NET Output</em></p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.GeomeTry</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Namespace</span> Prompts</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> PromptsTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Shared</span> useThisAngleOption <span style="COLOR: blue">As</span> PromptAngleOptions</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Shared</span> useThisAngleResult <span style="COLOR: blue">As</span> PromptDoubleResult</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Shared</span> useThisPointOption <span style="COLOR: blue">As</span> PromptPointOptions</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Shared</span> useThisPointResult <span style="COLOR: blue">As</span> PromptPointResult</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Shared</span> useThisEntityOption <span style="COLOR: blue">As</span> PromptEntityOptions</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Shared</span> useThisEntityResult <span style="COLOR: blue">As</span> PromptEntityResult</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">'A small function that shows how to prompt for an integer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;GetInteger&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> integerTest()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> opt0 <span style="COLOR: blue">As</span> PromptIntegerOptions = <span style="COLOR: blue">New</span> PromptIntegerOptions(<span style="COLOR: maroon">&quot;Enter your age&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.AllowNegative = <span style="COLOR: blue">False</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.AllowNone = <span style="COLOR: blue">False</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.AllowZero = <span style="COLOR: blue">False</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;opt0.DefaultValue = 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> IntRes <span style="COLOR: blue">As</span> PromptIntegerResult = ed.GetInteger(opt0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> IntRes.Status = PromptStatus.OK <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: blue">String</span>.Format(<span style="COLOR: maroon">&quot;\nYou entered {0}&quot;</span>, IntRes.Value))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Namespace</span></p></div>

<p><strong>Copy as HTML</strong></p>

<p>The next tool is the one I was really in need of - as you can see from the above code snippets, it works very well:</p>

<p><a href="http://www.jtleigh.com/people/colin/software/CopySourceAsHtml/">http://www.jtleigh.com/people/colin/software/CopySourceAsHtml/</a></p>

<p>This is another Visual Studio Addin, this time to enable copying of your code along with its formatting (as defined by Visual Studio) into the clipboard as an HTML fragment. This is really a great tool for bloggers, technical writers or people providing API support (as my team does).</p>

<p>This time a &quot;Copy as HTML...&quot; menu item gets added to both the Edit menu and to the right-click context menu:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=150,height=245,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/copyashtmlmenu.png"><img title="Copyashtmlmenu" height="163" alt="Copyashtmlmenu" src="/assets/copyashtmlmenu.png" width="100" border="0" /></a> </p>

<p>When you select some text in the Visual Studio editor and choose this menu option, a dialog appears to allow some configuration:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=571,height=475,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/pasteasvbdialog_1.png"><img title="Copyashtmldialog" height="58" alt="Copyashtmldialog" src="/assets/copyashtmldialog.png" width="100" border="0" /></a></p>

<p>And that's it - from there you can paste into Word, Outlook, TypePad etc. (see above for the results!)</p>
