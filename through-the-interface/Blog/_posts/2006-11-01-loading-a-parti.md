---
layout: "post"
title: "Loading a partial CUI and making its toolbars visible through .NET"
date: "2006-11-01 17:30:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2006/11/loading_a_parti.html "
typepad_basename: "loading_a_parti"
typepad_status: "Publish"
---

<p>A discussion in the comments on <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html">this previous entry</a> seemed worthy of turning into a post.</p>

<p>The problem appears to be that when you load a partial CUI file into AutoCAD, by default the various resources (pull-down menus, toolbars) are not displayed.</p>

<p>This snippet of code shows you how to both load a CUI file into AutoCAD and then loop through the toolbars in your menu-group, making them all visible. You could extend it fairly easily to add the pull-down menus contained in the CUI by using mg.Menus.InsertMenuInMenuBar(). I'm choosing to leave that as an exercise for the reader mainly because the choice of where the various menus go can be quite specific to individual applications... toolbars are much simpler - we're just going to turn them all on. :-)</p>

<p>So here's the code... for convenience I wrote it in VB.NET, but it uses COM Interop to access the menu API in AutoCAD. </p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Interop</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> ToolbarCmds</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;LoadTBs&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> LoadToolbars()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Const</span> cuiname <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = <span style="COLOR: maroon">&quot;mycuiname&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Const</span> cuifile <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = <span style="COLOR: maroon">&quot;c:\mycuifile.cui&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> mg <span style="COLOR: blue">As</span> AcadMenuGroup</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">'Attempt to access our menugroup</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;mg = Application.MenuGroups.Item(cuiname)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Catch</span> ex <span style="COLOR: blue">As</span> System.Exception</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">'Failure simply means we need to load the CUI first</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.MenuGroups.Load(cuifile)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;mg = Application.MenuGroups.Item(cuiname)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Try</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">'Cycle through the toobars, setting them to visible</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> i <span style="COLOR: blue">As</span> <span style="COLOR: blue">Integer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">For</span> i = 0 <span style="COLOR: blue">To</span> mg.Toolbars.Count - 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;mg.Toolbars.Item(i).Visible = <span style="COLOR: blue">True</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Next</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p></div>
