---
layout: "post"
title: "Docking toolbars in rows using CUI API"
date: "2015-10-09 11:16:45"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/10/docking-toolbars-in-rows-using-cui-api.html "
typepad_basename: "docking-toolbars-in-rows-using-cui-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The toolbar creation and docking in AutoCAD API is exposed via the COM API. Although the toolbars can be docked using the COM API, it does not provide a way to arrange the docked toolbars as in multiple rows. AutoCAD internally uses a method that can dock toolbars to the desired location in rows, which is not exposed via the COM API.</p>
<p>A way to workaround it is to configure the workspace toolbars and set their arrangement using CUI API. Here is a sample code snippet that arranges the toolbars in the current workspace as shown in the following screenshot. All other toolbars in the current workspace are hidden. You can modify the code to show other toolbars if required.</p>
<p>Here is the toolbar arrangement that we seek :</p>
<p>Row : 0 Column : 0 - Smooth Mesh toolbar</p>
<p>Row : 1 Column : 0 - Smooth Mesh Primitives toolbar</p>
<p>Row : 0 Column : 1 - Draw toolbar</p>
<p>Row : 1 Column : 1 - Draw order toolbar</p>
<p>Row : 0 Column : 2 - Standard toolbar</p>
<p>Row : 1 Column : 2 - CAD Standard toolbar</p>
<p></p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1654a02970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1654a02970c img-responsive" alt="DockedToolbars" title="DockedToolbars" src="/assets/image_647597.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Customization;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;accore.dll&quot;</span><span style="color:#000000"> , CharSet = CharSet.Ansi, </pre>
<pre style="margin:0em;"> CallingConvention = CallingConvention.Cdecl, </pre>
<pre style="margin:0em;"> EntryPoint = <span style="color:#a31515">&quot;acedCmdS&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  acedCmdS(System.IntPtr vlist);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;SetDockedToolbars&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SetDockedToolbarsMethod()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document activeDoc </pre>
<pre style="margin:0em;"> 		= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Database db = activeDoc.Database;</pre>
<pre style="margin:0em;">     Editor ed = activeDoc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     string mainCuiFile = </pre>
<pre style="margin:0em;"> 		(string)Application.GetSystemVariable(<span style="color:#a31515">&quot;MENUNAME&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     mainCuiFile += <span style="color:#a31515">&quot;.cuix&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     CustomizationSection cs </pre>
<pre style="margin:0em;"> 		= <span style="color:#0000ff">new</span><span style="color:#000000">  CustomizationSection(mainCuiFile);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     string curWorkspace = </pre>
<pre style="margin:0em;"> 		(string)Application.GetSystemVariable(<span style="color:#a31515">&quot;WSCURRENT&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     Workspace ws = cs.getWorkspace(curWorkspace);  </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Turn off all the workspace toolbars.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// We will later turn on the ones that we need and </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// dock them as we desire</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     foreach (WorkspaceToolbar wsTb in ws.WorkspaceToolbars)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTb.Display = 0;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Smooth Mesh toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Toolbar tbSmoothMesh = </pre>
<pre style="margin:0em;"> 		cs.MenuGroup.Toolbars.</pre>
<pre style="margin:0em;"> 		FindToolbarWithName(<span style="color:#a31515">&quot;Smooth Mesh&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     WorkspaceToolbar wsTbSmoothMesh = </pre>
<pre style="margin:0em;"> 		ws.WorkspaceToolbars.FindWorkspaceToolbar</pre>
<pre style="margin:0em;"> 		(tbSmoothMesh.ElementID, cs.MenuGroup.Name);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (wsTbSmoothMesh == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTbSmoothMesh = <span style="color:#0000ff">new</span><span style="color:#000000">  WorkspaceToolbar(ws, tbSmoothMesh);</pre>
<pre style="margin:0em;">         ws.WorkspaceToolbars.Add(wsTbSmoothMesh);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     wsTbSmoothMesh.Display = 1;</pre>
<pre style="margin:0em;">     wsTbSmoothMesh.ToolbarOrient = ToolbarOrient.left;</pre>
<pre style="margin:0em;">     wsTbSmoothMesh.DockRow = 0;</pre>
<pre style="margin:0em;">     wsTbSmoothMesh.DockColumn = 0;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Smooth Mesh Primitives toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Toolbar tbSmoothMeshPrimitives </pre>
<pre style="margin:0em;"> 		= cs.MenuGroup.Toolbars.FindToolbarWithName</pre>
<pre style="margin:0em;"> 		(<span style="color:#a31515">&quot;Smooth Mesh Primitives&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     WorkspaceToolbar wsTbSmoothMeshPrimitives </pre>
<pre style="margin:0em;"> 		= ws.WorkspaceToolbars.FindWorkspaceToolbar</pre>
<pre style="margin:0em;"> 		(tbSmoothMeshPrimitives.ElementID, cs.MenuGroup.Name);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (wsTbSmoothMeshPrimitives == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTbSmoothMeshPrimitives = </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">new</span><span style="color:#000000">  WorkspaceToolbar(ws, tbSmoothMeshPrimitives);</pre>
<pre style="margin:0em;">         ws.WorkspaceToolbars.Add(wsTbSmoothMeshPrimitives);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     wsTbSmoothMeshPrimitives.Display = 1;</pre>
<pre style="margin:0em;">     wsTbSmoothMeshPrimitives.ToolbarOrient = ToolbarOrient.left;</pre>
<pre style="margin:0em;">     wsTbSmoothMeshPrimitives.DockRow = 1;</pre>
<pre style="margin:0em;">     wsTbSmoothMeshPrimitives.DockColumn = 0;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Draw toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Toolbar tbDraw = </pre>
<pre style="margin:0em;"> 		cs.MenuGroup.Toolbars.FindToolbarWithName(<span style="color:#a31515">&quot;Draw&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     WorkspaceToolbar wsTbDraw = </pre>
<pre style="margin:0em;"> 		ws.WorkspaceToolbars.FindWorkspaceToolbar</pre>
<pre style="margin:0em;"> 		(tbDraw.ElementID, cs.MenuGroup.Name);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (wsTbDraw == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTbDraw = <span style="color:#0000ff">new</span><span style="color:#000000">  WorkspaceToolbar(ws, tbDraw);</pre>
<pre style="margin:0em;">         ws.WorkspaceToolbars.Add(wsTbDraw);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     wsTbDraw.Display = 1;</pre>
<pre style="margin:0em;">     wsTbDraw.ToolbarOrient = ToolbarOrient.left;</pre>
<pre style="margin:0em;">     wsTbDraw.DockRow = 0;</pre>
<pre style="margin:0em;">     wsTbDraw.DockColumn = 1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Draw order toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Toolbar tbDrawOrder = </pre>
<pre style="margin:0em;"> 		cs.MenuGroup.Toolbars.FindToolbarWithName(<span style="color:#a31515">&quot;Draw Order&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     WorkspaceToolbar wsTbDrawOrder = </pre>
<pre style="margin:0em;"> 		ws.WorkspaceToolbars.FindWorkspaceToolbar</pre>
<pre style="margin:0em;"> 		(tbDrawOrder.ElementID, cs.MenuGroup.Name);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (wsTbDrawOrder == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTbDrawOrder = <span style="color:#0000ff">new</span><span style="color:#000000">  WorkspaceToolbar(ws, tbDrawOrder);</pre>
<pre style="margin:0em;">         ws.WorkspaceToolbars.Add(wsTbDrawOrder);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     wsTbDrawOrder.Display = 1;</pre>
<pre style="margin:0em;">     wsTbDrawOrder.ToolbarOrient = ToolbarOrient.left;</pre>
<pre style="margin:0em;">     wsTbDrawOrder.DockRow = 1;</pre>
<pre style="margin:0em;">     wsTbDrawOrder.DockColumn = 1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Standard toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Toolbar tbStandard = </pre>
<pre style="margin:0em;"> 		cs.MenuGroup.Toolbars.FindToolbarWithName(<span style="color:#a31515">&quot;Standard&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     WorkspaceToolbar wsTbStandard = </pre>
<pre style="margin:0em;"> 		ws.WorkspaceToolbars.FindWorkspaceToolbar</pre>
<pre style="margin:0em;"> 		(tbStandard.ElementID, cs.MenuGroup.Name);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (wsTbStandard == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTbStandard = <span style="color:#0000ff">new</span><span style="color:#000000">  WorkspaceToolbar(ws, tbStandard);</pre>
<pre style="margin:0em;">         ws.WorkspaceToolbars.Add(wsTbStandard);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     wsTbStandard.Display = 1;</pre>
<pre style="margin:0em;">     wsTbStandard.ToolbarOrient = ToolbarOrient.left;</pre>
<pre style="margin:0em;">     wsTbStandard.DockRow = 0;</pre>
<pre style="margin:0em;">     wsTbStandard.DockColumn = 2;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// CAD Standard toolbar</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Toolbar tbCADStandards = </pre>
<pre style="margin:0em;"> 		cs.MenuGroup.Toolbars.FindToolbarWithName</pre>
<pre style="margin:0em;"> 		(<span style="color:#a31515">&quot;CAD Standards&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     WorkspaceToolbar wsTbCADStandards </pre>
<pre style="margin:0em;"> 		= ws.WorkspaceToolbars.FindWorkspaceToolbar</pre>
<pre style="margin:0em;"> 		(tbCADStandards.ElementID, cs.MenuGroup.Name);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000"> (wsTbCADStandards == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         wsTbCADStandards = </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">new</span><span style="color:#000000">  WorkspaceToolbar(ws, tbCADStandards);</pre>
<pre style="margin:0em;">         ws.WorkspaceToolbars.Add(wsTbCADStandards);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     wsTbCADStandards.Display = 1;</pre>
<pre style="margin:0em;">     wsTbCADStandards.ToolbarOrient = ToolbarOrient.left;</pre>
<pre style="margin:0em;">     wsTbCADStandards.DockRow = 1;</pre>
<pre style="margin:0em;">     wsTbCADStandards.DockColumn = 2;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     saveCui(cs);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  saveCui(CustomizationSection cs)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (cs.IsModified)</pre>
<pre style="margin:0em;">         cs.Save();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ResultBuffer rb = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer();</pre>
<pre style="margin:0em;">     rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5005, <span style="color:#a31515">&quot;FILEDIA&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">     rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5005, <span style="color:#a31515">&quot;0&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">     acedCmdS(rb.UnmanagedObject);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     string cuiMenuGroup = cs.MenuGroup.Name;</pre>
<pre style="margin:0em;">     rb = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer();</pre>
<pre style="margin:0em;">     rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5005, <span style="color:#a31515">&quot;_CUIUNLOAD&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">     rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5005, cuiMenuGroup));</pre>
<pre style="margin:0em;">     acedCmdS(rb.UnmanagedObject);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     string cuiFileName = cs.CUIFileName;</pre>
<pre style="margin:0em;">     rb = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer();</pre>
<pre style="margin:0em;">     rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5005, <span style="color:#a31515">&quot;_CUILOAD&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">     rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5005, cuiFileName));</pre>
<pre style="margin:0em;">     acedCmdS(rb.UnmanagedObject);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
