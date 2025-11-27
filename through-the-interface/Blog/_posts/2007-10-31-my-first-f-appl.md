---
layout: "post"
title: "My first F# application for AutoCAD"
date: "2007-10-31 20:19:41"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/10/my-first-f-appl.html "
typepad_basename: "my-first-f-appl"
typepad_status: "Publish"
---

<p>I couldn't resist... I just had to have a play with this technology, today. :-)</p>

<p>Here are the steps to get your first (very simple) F# application working inside AutoCAD.</p>

<p>First we need to download and install the latest F# distributable from <a href="http://research.microsoft.com/fsharp/release.aspx">here</a> (at the time of writing this was the July 31 release - 1.9.2.9).</p>

<p>We create a base F# project, selecting the &quot;F# Project&quot; template:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=681,height=495,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/31/new_fsharp_project.png"><img title="New_fsharp_project" height="218" alt="New_fsharp_project" src="/assets/new_fsharp_project.png" width="300" border="0" /></a> </p>

<p>We now add a new item to the project of type &quot;F# Source File&quot; to the project:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=681,height=419,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/31/new_fsharp_source_file.png"><img title="New_fsharp_source_file" height="184" alt="New_fsharp_source_file" src="/assets/new_fsharp_source_file.png" width="300" border="0" /></a> </p>

<p>The file created contains a lot of boilerplate code that is definitely worth looking at to get a feel for the basics of the F# language.</p>

<p>We now go ahead and replace this with our own F# code (which I created by borrowing liberally from one of the F# samples from <a href="http://www.codeplex.com/fsharpsamples">CodePlex</a>... I should say - once again - that this is my very first attempt at using F#, so this was intended to be functional rather than elegant :-).</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">(* Use lightweight F# syntax *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">(* Declare a specific namespace</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; and module name</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">*)</span> </p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespace.MyApplication</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">(* Import managed assemblies *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Windows.Forms</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">(* Now we declare our command *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;Test&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> f () =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">(* Create our form *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> frm = <span style="COLOR: blue">new</span> Form()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.Text &lt;- <span style="COLOR: maroon">&quot;This is a WinForm&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.Height &lt;- 80</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.Width &lt;- 360</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.StartPosition &lt;-</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; FormStartPosition.CenterScreen</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">(* Create the contents:</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;a Label</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;a TextBox</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp;&nbsp; &nbsp;a Button</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> lb = <span style="COLOR: blue">new</span> Label()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; lb.Text &lt;- <span style="COLOR: maroon">&quot;Enter text: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; lb.Width &lt;- 60</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; lb.Left &lt;- 10</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; lb.Top &lt;- 12</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> tb = <span style="COLOR: blue">new</span> TextBox()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tb.Left &lt;- 80</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tb.Top &lt;- 10</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tb.Width &lt;- 200</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">(* Define an EventHandler for</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp; the Click event and attach</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; &nbsp; it to the Button</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> mb _ _ =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;MessageBox.Show(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tb.Text,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Text typed:&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> eh = <span style="COLOR: blue">new</span> EventHandler(mb)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt = <span style="COLOR: blue">new</span> Button()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; bt.Text &lt;- <span style="COLOR: maroon">&quot;Submit&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; bt.Left &lt;- 290</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; bt.Top &lt;- 8</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; bt.Width &lt;- 50</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; bt.Click.AddHandler(eh)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">(* Add the controls to our Form *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.Controls.Add(lb)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.Controls.Add(tb)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; frm.Controls.Add(bt)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">(* Display the Form *)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Application.Run(frm)</p></div>

<p>To get this code to build, we have to add assembly references to AutoCAD's managed assemblies in our project settings, as well as setting the project type to &quot;DLL&quot;:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=751,height=606,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/31/fsharp_project_settings.png"><img title="Fsharp_project_settings" height="242" alt="Fsharp_project_settings" src="/assets/fsharp_project_settings.png" width="300" border="0" /></a></p>

<p>The application should now build, creating &quot;myfirstfsharpapp.dll&quot;. We load this in AutoCAD using the standard NETLOAD command, and then execute our TEST command:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=682,height=524,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/31/first_fsharp_application_running.png"><img title="First_fsharp_application_running" height="230" alt="First_fsharp_application_running" src="/assets/first_fsharp_application_running.png" width="299" border="0" /></a> </p>

<p>When we enter some text and click &quot;Submit&quot;, a message box is displayed with the string we entered:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=682,height=524,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/31/first_fsharp_application_running_2.png"><img title="First_fsharp_application_running_2" height="230" alt="First_fsharp_application_running_2" src="/assets/first_fsharp_application_running_2.png" width="299" border="0" /></a> </p>

<p>That's it for this first attempt. In future posts I hope to solve more interesting problems with the F# language, but you do, of course, have to start somewhere.</p>
