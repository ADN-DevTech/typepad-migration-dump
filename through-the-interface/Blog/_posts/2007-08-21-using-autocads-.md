---
layout: "post"
title: "Using AutoCAD's file selection dialog from .NET"
date: "2007-08-21 18:42:26"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2007/08/using-autocads-.html "
typepad_basename: "using-autocads-"
typepad_status: "Publish"
---

<p>Today I started putting together some code showing how to link an Excel sheet to an AutoCAD table (watch this space - there should be something posted later this week). As I was working through it I decided enough was enough, and rather than - yet again - using the command-line to get the name of the file, I'd use the opportunity to demonstrate how to make use of AutoCAD's standard file selection dialog in your applications.</p>

<p>At which point I decided to cut the original post short, as I didn't want this useful (but quite short) topic to get swamped by the broader one.</p>

<p>Here's the C# code that asks the user to select an Excel spreadsheet:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Windows;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> OpenFiles</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;SS&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SelectSpreadsheet()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">OpenFileDialog</span> ofd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">OpenFileDialog</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;Select Excel spreadsheet to link&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">null</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;xls; xlsx&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;ExcelFileToLink&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenFileDialog</span>.<span style="COLOR: teal">OpenFileDialogFlags</span>.DoNotTransferRemoteFiles</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Windows.Forms.<span style="COLOR: teal">DialogResult</span> dr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ofd.ShowDialog();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (dr != System.Windows.Forms.<span style="COLOR: teal">DialogResult</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nFile selected was \&quot;{0}\&quot;.&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ofd.Filename</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div><p>A few notes on the arguments to the OpenFileDialog constructor:</p>

<ol><li>The first string is the one shown in the title bar (see below)</li>

<li>You can pass in a default filename in the second argument, but in our case it isn't appropriate</li>

<li>You can provide multiple file extensions upon which to filter in the third argument, separated by semi-colons</li>

<li>The fourth argument is an internal identifier used to store data about the dialog, such as size, position and last navigated path (this data will be picked up automatically the next time the identifier is used)</li>

<li>The fifth argument is for flags: we're choosing not to copy remote files locally if selected via a URL, in this case</li></ol>

<p>If we were opting to allow multiple file selection (passing AllowMultiple as an additional flag to the fifth argument), then we would access the files returned using ofd.GetFileNames() to access an array of strings.</p>

<p>Here's the dialog we see when we run the SS command:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=614,height=460,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/08/21/open_file_dialog.png"><img title="Open_file_dialog" height="224" alt="Open_file_dialog" src="/assets/open_file_dialog.png" width="299" border="0" /></a> </p>
