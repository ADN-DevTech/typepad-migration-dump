---
layout: "post"
title: "Creating partial cuix that appends Panel to \"Add-ins\" tab"
date: "2015-02-27 00:52:44"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/creating-partial-cuix-that-appends-panel-to-add-ins-tab.html "
typepad_basename: "creating-partial-cuix-that-appends-panel-to-add-ins-tab"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to create a partial cuix at runtime. It creates a ribbon panel with a command button. After the cuix is loaded in AutoCAD using cuiload, the panel should appear in the "Add-ins" tab.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> CustomizationSection csNew = <span style="color:#0000ff">new</span><span style="color:#000000">  CustomizationSection();</pre>
<pre style="margin:0em;"> csNew.MenuGroupName = <span style="color:#a31515">&quot;myMenuGroup&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Create a new menu macro</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> MacroGroup myMacGroup = </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">new</span><span style="color:#000000">  MacroGroup(<span style="color:#a31515">&quot;myMacroGroup&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">     csNew.MenuGroup);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> MenuMacro macroLine = myMacGroup.CreateMenuMacro(</pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;TestLine&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;^C^C_Line &quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;ID_MyLineCmd&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;My Line help&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         MacroType.Any, </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;RCDATA_16_LINE&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;RCDATA_32_LINE&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;My Test Line&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> RibbonRoot ribbonRoot = csNew.MenuGroup.RibbonRoot;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Create a panel</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> RibbonPanelSource ribPanelSource</pre>
<pre style="margin:0em;">                 = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonPanelSource(ribbonRoot);</pre>
<pre style="margin:0em;"> ribPanelSource.Text = <span style="color:#a31515">&quot;MyPanel&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Create a ribbon row</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> RibbonRow ribRow = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonRow();</pre>
<pre style="margin:0em;"> ribPanelSource.Items.Add((RibbonItem)ribRow);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Create a Ribbon Command Button</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> RibbonCommandButton ribCommandButton </pre>
<pre style="margin:0em;">                    = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonCommandButton(ribRow);</pre>
<pre style="margin:0em;"> ribCommandButton.Text = <span style="color:#a31515">&quot;MyTestLineButton&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> ribCommandButton.MacroID = macroLine.ElementID;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Add the ribbon command button to the ribbon row </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> ribRow.Items.Add((RibbonItem)ribCommandButton);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//Get the panels from the RibbonRoot </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> RibbonPanelSourceCollection panels </pre>
<pre style="margin:0em;">                 = ribbonRoot.RibbonPanelSources;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Add the Ribbon Panel Source to the Ribbon Panels </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> panels.Add(ribPanelSource);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> RibbonTabSourceCollection tabs </pre>
<pre style="margin:0em;">                     = ribbonRoot.RibbonTabSources;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Create a new Ribbon Tab Source </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> RibbonTabSource tabSrc </pre>
<pre style="margin:0em;">                 = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonTabSource(ribbonRoot);</pre>
<pre style="margin:0em;"> tabSrc.Name = <span style="color:#a31515">&quot;MyTab&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> tabSrc.Text = <span style="color:#a31515">&quot;MyTab&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> tabSrc.ElementID = <span style="color:#a31515">&quot;MyTabElementID&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> tabSrc.Aliases.Add(<span style="color:#a31515">&quot;ID_ADDINSTAB&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> tabSrc.WorkspaceBehavior </pre>
<pre style="margin:0em;">             = TabWorkspaceBehavior.MergeOrAddTab;</pre>
<pre style="margin:0em;"> tabs.Add(tabSrc);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (tabSrc != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#008000">//Add the Panel to the Tab </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     RibbonPanelSourceReference ribPanelSourceRef </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonPanelSourceReference(tabSrc);</pre>
<pre style="margin:0em;">     ribPanelSourceRef.PanelId </pre>
<pre style="margin:0em;">             = ribPanelSource.ElementID;</pre>
<pre style="margin:0em;">     ribPanelSourceRef.ElementID = <span style="color:#a31515">&quot;MyPanelID&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     tabSrc.Items.Add(ribPanelSourceRef);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> csNew.SaveAs(strCuiFile);</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7556d7c970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7556d7c970b img-responsive" alt="Ribbon Panel" title="Ribbon Panel" src="/assets/image_447692.jpg" style="margin: 0px 5px 5px 0px;" /></a>
