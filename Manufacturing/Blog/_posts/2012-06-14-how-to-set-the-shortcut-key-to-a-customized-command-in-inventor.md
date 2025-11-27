---
layout: "post"
title: "How to set the shortcut key to a customized command in Inventor?"
date: "2012-06-14 23:41:00"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/how-to-set-the-shortcut-key-to-a-customized-command-in-inventor.html "
typepad_basename: "how-to-set-the-shortcut-key-to-a-customized-command-in-inventor"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>&quot;What is the best method to assign a shortcut key to a command in inventor programmatically via the api? I can see on the command definition there are default shortcut key and override shortcut key properties but which one should I set?&quot;</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The following VBA sample demonstrates how to assign a shortcut key to a customized button added to assembly Panel Bar.</p>
<p>Please note the following several points:</p>
<p>If the shortcut key has been consumed by another command, you can’t use it again for your own command, otherwise you will get a runtime error. For example Ctrl+A is used by the “Select All” command, so you can’t use “Ctrl+A” again. You can get a list of all commands and the shortcut keys consumed by Inventor through the “Copy to clipboard” command which is on the keyboard page of the Customize dialog (Tools&gt;Customize¡­).</p>
<p>The DefaultShortcut property can be used to set the command alias, but can’t be used for setting the accelerator, you should use OverrideShortcut instead. The user interface won’t delete the controls you added, so you need to remove them manually from the user interface.</p>
<p>If the shortcut key is used by you, you can’t use it again even if you delete the corresponding control from UI, unless you reset all keys from the keyboard page on the customize dialog.</p>
<p>Shortcuts of kAcceleratorShortcut type are created by defining the OverrideShortcut to an allowed non-alphanumeric character, for example “Alt+U”, but those are defined as key combinations by Inventor, like “Ctrl+C” or “Alt+H”, you can’t use.</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Private WithEvents oBtnDef As ButtonDefinition</p>
<p>Sub AddBtnDefWithShortcut()</p>
<p>Dim oControlDefs As ControlDefinitions</p>
<p>Set oControlDefs = ThisApplication.CommandManager.ControlDefinitions</p>
<p>&#39;Create our own button command</p>
<p>Set oBtnDef = oControlDefs.AddButtonDefinition(&quot;MyControldefinition1&quot;, &quot;Internal name for my controldef 1&quot;, kShapeEditCmdType)</p>
<p>&#39;Assign shortcut key to the button command</p>
<p>oBtnDef.OverrideShortcut = &quot;Alt+A&quot;</p>
<p>&#39; Find the main assembly command bar.</p>
<p>Dim oAsmCmdBar As CommandBar</p>
<p>Set oAsmCmdBar = ThisApplication.UserInterfaceManager.CommandBars.Item(&quot;AMxAssemblyPanelCmdBar&quot;)</p>
<p>&#39; Add a control to the command bar.</p>
<p>Call oAsmCmdBar.Controls.AddButton(oBtnDef)</p>
<p>End Sub</p>
<p>&#0160;</p>
<p>You can delete the button command created by above code, like this:</p>
<p>&#0160;</p>
<p>Sub DeleteControlDef()</p>
<p>Dim oControlDefs As ControlDefinitions</p>
<p>Set oControlDefs = ThisApplication.CommandManager.ControlDefinitions</p>
<p>Dim oDef As ControlDefinition</p>
<p>Set oDef = oControlDefs.Item(&quot;Internal name for my controldef 1&quot;)</p>
<p>oDef.Delete</p>
<p>End Sub</p>
