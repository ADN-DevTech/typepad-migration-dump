---
layout: "post"
title: "Creating Buttons for VBA Macros"
date: "2008-11-18 20:07:23"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Visual Basic for Applications (VBA)"
original_url: "https://modthemachine.typepad.com/my_weblog/2008/11/creating-buttons-for-vba-macros.html "
typepad_basename: "creating-buttons-for-vba-macros"
typepad_status: "Publish"
---

<p><strong><font size="3">What is a Macro?</font></strong></p>
<p>The title for this posting is specific to a question that&#39;s come up a couple of times recently, but the post is actually more general and covers the many ways you can execute an Inventor VBA macro.</p>
<p>First, let&#39;s define what a macro is.&#0160; It&#39;s a public sub without any arguments within a standard code module in an Inventor VBA project.&#0160; The following code shows a valid macro.&#0160; It&#39;s declared as a Public sub with no arguments.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Public Sub SampleMacro() <br />&#0160;&#0160;&#0160; MsgBox &quot;The macro is running.&quot; <br />End Sub</div>
<p>The following is also a valid macro since subs default to being Public if you don&#39;t specify it to be Public or Private.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Sub SampleMacro() <br />&#0160;&#0160;&#0160; MsgBox &quot;The macro is running.&quot; <br />End Sub</div>
<p>The following is not a valid macro since it has arguments.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Public Sub AddNumbers(Value1 As Double, Value2 As Double) <br />&#0160;&#0160;&#0160; MsgBox &quot;Value sum: &quot; &amp; Value1 + Value2 <br />End Sub</div>
<br />
<p>Standard code modules are shown in the project window under the <strong>Modules</strong> node in the tree, as shown below.&#0160; You can have any number of code modules within a project and all new projects will contain a module named &quot;Module1&quot;.&#0160; A macro can exist in any module.</p>
<p style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010536013916970c-pi"><img alt="AppProjectModule1" border="0" height="137" src="/assets/image_961504.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="232" /></a> </p>
<p style="text-align: center">VBA&#39;s Project window and a code module.</p>
<p style="text-align: left">&#0160;</p>
<p><strong><font size="3">Running a Macro Using the Macros Command</font></strong></p>
<p>One interface you can use to run a VBA macro is the <strong>Macros</strong> command (Tools -&gt; Macro -&gt; Macros... or Alt-F8).&#0160; Running this command will display the dialog shown below.&#0160; This dialog displays a list of all of the available macros.&#0160; It lists them as ModuleName.MacroName.&#0160; The &quot;Macros in&quot; drop-down list let you select different VBA projects.&#0160; To run a macro, select it in the list and click the <strong>Run</strong> button.</p>
<p style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010535f9d428970b-pi"><img alt="MacroDialog" border="0" height="230" src="/assets/image_364453.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="304" /></a> </p>
<p style="text-align: center">The Macros command dialog.</p>
<br />
<p>This interface is always available to run a macro but is not the most convenient or easiest to use, especially if you have macros that you frequently use.</p>
<br />
<p><font size="3"><strong>Running a Macro Using a Button</strong></font></p>
<p>For frequently used macros it&#39;s much more convenient to create a button to execute the macro.&#0160; To create a button for a macro there&#39;s one additional requirement on your macro.&#0160; It must exist in the default VBA project.&#0160; This is the VBA project that Inventor loads automatically every time it is started.&#0160; It&#39;s likely that your macro is already in this project.&#0160; The default VBA project is defined using the File tab of Application Options command, as shown below.&#0160; In this case it is Default.ivb in the Bin\Macros directory, but it can be any .ivb file.</p>
<p style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010535f9d42c970b-pi"><img alt="DefaultVBAProject" border="0" height="232" src="/assets/image_366202.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="354" /></a></p>
<p style="text-align: center">Specifying the default VBA project.</p>
<p style="text-align: left">&#0160;</p>
<p style="text-align: left">To create a button use the <strong>Customize</strong> command (Tools -&gt; Customize...) and select the Commands tab of the dialog.&#0160; In the Categories list there is a &quot;Macros&quot; category.&#0160; After selecting that you&#39;ll see a list of available macros in the Commands list on the right.&#0160; You can drag and drop any of the macros in the list onto any available toolbar, including the panel bar.&#0160; This is illustrated in the picture below.</p>
<p style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010536013928970c-pi"><img alt="CustomizeDialog" border="0" height="341" src="/assets/image_776280.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="404" /></a> </p>
<p style="text-align: center">Using the Customize command to create a macro button.</p>
<p style="text-align: left">&#0160;</p>
<p style="text-align: left">It&#39;s easy to create a button for your macro but it will have a default icon that&#39;s used for all VBA macros.&#0160; If you have more than one macro button it can be difficult to tell which button is for which macro.&#0160; The tool tip for the button does display the macro name, however it&#39;s also possible to create your own icon for a macro button.</p>
<p style="text-align: left">Icons are created using whatever bitmap editor you choose.&#0160; The Paint program that comes with Windows will work.&#0160; Create a bitmap that is 16x16 pixels in size.&#0160; It can use any colors but magenta is interpreted as the transparent color.&#0160; Below is an example.&#0160; The picture on the left shows the icon (zoomed in) within the Paint program.&#0160; The picture on the right shows the same icon used for a button.&#0160; Notice that the magenta color has become the background color.</p>
<p style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010536013e59970c-pi"><img alt="IconEdit" border="0" height="302" src="/assets/image_742105.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="204" /></a>&#0160;&#0160; <a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010536013e5e970c-pi"><img alt="IconSample" border="0" height="237" src="/assets/image_116380.jpg" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" width="184" /></a> </p>
<p style="text-align: center">Icon samples.</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: left">To associate an icon with a macro you must name it correctly and put it in the correct location.&#0160; The rules for naming are:</p>
<p style="text-align: left">&#0160;&#0160; ModuleName.MacroName.Size.bmp</p>
<p style="text-align: left">The ModuleName is the name of the VBA module where your macro is.&#0160; In this example it is &quot;Module1&quot;.&#0160; The MacroName is the name of your macro.&#0160; In this example it is &quot;SampleMacro&quot;.&#0160; The Size can be either &quot;Small&quot; or &quot;Large&quot;.&#0160; Small icons are 16x16 pixels and large icons are 32x32 pixels.&#0160; Inventor allows the end-user to choose to use large or small icons.&#0160; If you only supply a small icon Inventor will scale it and create a large icon if needed.&#0160; For the example above you would create a 16x16 pixel bitmap called:</p>
<p style="text-align: left">&#0160;&#0160; Module1.SampleMacro.Small.bmp</p>
<p style="text-align: left">The large bitmap name is:</p>
<p style="text-align: left">&#0160;&#0160; Module1.SampleMacro.Large.bmp</p>
<p style="text-align: left">These files need to be located in the same directory as Default.ivb (or your default VBA project).&#0160; Inventor will automatically associate them with the macro when creating a button.&#0160; If your icon does not display then double-check the name and the size of the icon. </p>
<p style="text-align: left">&#0160;</p>
<p style="text-align: left"><font size="3"><strong>Running a Macro from a Program</strong></font></p>
<p style="text-align: left">Although not commonly used, it&#39;s also possible to run a macro from another program.&#0160; This isn&#39;t limited to running only macros, as defined above, but any function or sub in any project and module can be executed.&#0160; I won&#39;t go into a lot of detail since there is likely not much interest in this but here is an example that demonstrates the process.&#0160; First, here are two macros in Inventor&#39;s default VBA project.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Public Sub SampleMacro() <br />&#0160;&#0160;&#0160; MsgBox &quot;The macro is running.&quot; <br />End Sub </div>
<p></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Public Sub AddNumbers(Value1 As Double, Value2 As Double) <br />&#0160;&#0160;&#0160; MsgBox &quot;Value sum: &quot; &amp; Value1 + Value2 <br />End Sub</div>
<br />
<p>Here is an Excel macro that connects to Inventor and executes the two Inventor macros.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Public Sub RunInventorMacros()&#0160;&#0160;&#0160;&#0160; <br /><strong><font color="#0000ff">&#0160;&#0160;&#0160; &#39; Get a reference to Inventor.&#0160; This assumes Inventor is running. <br /></font></strong>&#0160;&#0160;&#0160;&#0160; Dim invApp As Inventor.Application <br />&#0160;&#0160;&#0160; Set invApp = GetObject(, &quot;Inventor.Application&quot;) <br /><br /><strong><font color="#0000ff">&#0160;&#0160;&#0160; &#39; Get a reference to the default VBA project. <br />&#0160;&#0160;&#0160; &#39; It will always be the first item in the collection. <br /></font></strong>&#0160;&#0160;&#0160; Dim invVBAProject As InventorVBAProject <br />&#0160;&#0160;&#0160; Set invVBAProject = invApp.VBAProjects.Item(1) <br /><br /><strong><font color="#0000ff">&#0160;&#0160;&#0160; &#39; Get a reference to the Module1 code module. <br /></font></strong>&#0160;&#0160;&#0160; Dim invModule As InventorVBAComponent <br />&#0160;&#0160;&#0160; Set invModule = invVBAProject.InventorVBAComponents.Item(&quot;Module1&quot;) <br /><br /><strong><font color="#0000ff">&#0160;&#0160;&#0160; &#39; Get a reference to the SampleMacro sub. <br /></font></strong>&#0160;&#0160;&#0160; Dim invSub As InventorVBAMember <br />&#0160;&#0160;&#0160; Set invSub = invModule.InventorVBAMembers.Item(&quot;SampleMacro&quot;) <br /><br /><font color="#0000ff"><strong>&#0160;&#0160;&#0160; &#39; Execute the macro. <br /></strong></font>&#0160;&#0160;&#0160; invSub.Execute <br /><br /><font color="#0000ff"><strong>&#0160;&#0160;&#0160; &#39; Get a reference to the AddNumbers sub. <br /></strong></font>&#0160;&#0160;&#0160; Set invSub = invModule.InventorVBAMembers.Item(&quot;AddNumbers&quot;) <br /><br /><strong><font color="#0000ff">&#0160;&#0160;&#0160; &#39; Set the arguments. <br /></font></strong>&#0160;&#0160;&#0160; invSub.Arguments.Item(&quot;Value1&quot;).Value = 3.5 <br />&#0160;&#0160;&#0160; invSub.Arguments.Item(&quot;Value2&quot;).Value = 4.75 <br /><br /><font color="#0000ff"><strong>&#0160;&#0160;&#0160; &#39; Execute the sub <br /></strong></font>&#0160;&#0160;&#0160; Call invSub.Execute <br />End Sub</div>
