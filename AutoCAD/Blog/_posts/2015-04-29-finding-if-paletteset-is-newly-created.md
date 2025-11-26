---
layout: "post"
title: "Finding if PaletteSet is newly created"
date: "2015-04-29 05:25:15"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/finding-if-paletteset-is-newly-created.html "
typepad_basename: "finding-if-paletteset-is-newly-created"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>You may want to know if your PaletteSet is being created for the very first time to set its Docking or for any other purpose. To do this, you can save custom data when the PaletteSet gets saved which obviously will not be available at the very first time the PaletteSet is loaded.</p>
<p>Here is a sample code to set the default docking of a PaletteSet to DockSides.Left when it gets created. In subsequent sessions, the docking is not changed and the PaletteSet should retain the position that was set by the user.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Windows;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  _isFirstTime = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> MyUserControl ctrl = null;</pre>
<pre style="margin:0em;"> PaletteSet set = null;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;Test&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  commandMethodTest()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (set == null)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         set = <span style="color:#0000ff">new</span><span style="color:#000000">  PaletteSet(<span style="color:#a31515">&quot;MyPalette&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">new</span><span style="color:#000000">  Guid(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>43FFB063-DF0B-474B-9856-7886305CC3E8<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         set.Load += </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">new</span><span style="color:#000000">  PalettePersistEventHandler</pre>
<pre style="margin:0em;"> 			(ps_Load);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         set.Save += </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">new</span><span style="color:#000000">  PalettePersistEventHandler</pre>
<pre style="margin:0em;"> 			(ps_Save);</pre>
<pre style="margin:0em;">                 </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (ctrl == null)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ctrl = <span style="color:#0000ff">new</span><span style="color:#000000">  MyUserControl();</pre>
<pre style="margin:0em;">             set.Add(<span style="color:#a31515">&quot;MyPalette&quot;</span><span style="color:#000000"> , ctrl);</pre>
<pre style="margin:0em;">             set.Style = PaletteSetStyles.ShowCloseButton;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     set.Visible = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Document doc </pre>
<pre style="margin:0em;"> 		= Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;">             </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (_isFirstTime)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ed.WriteMessage(<span style="color:#a31515">&quot;First Time, Set the Dock status&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         set.Dock = DockSides.Left;</pre>
<pre style="margin:0em;">         _isFirstTime = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ed.WriteMessage(<span style="color:#a31515">&quot;Not the first time, Do nothing. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			Leave it to the previous settings<span style="color:#a31515">&quot;);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ps_Load(object sender, </pre>
<pre style="margin:0em;"> 	PalettePersistEventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     String sIsFirstTime = </pre>
<pre style="margin:0em;"> 		(String)e.ConfigurationSection.ReadProperty</pre>
<pre style="margin:0em;"> 		(<span style="color:#a31515">&quot;IsFirstTime&quot;</span><span style="color:#000000"> , <span style="color:#a31515">&quot;Yes&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (sIsFirstTime.Equals(<span style="color:#a31515">&quot;No&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         _isFirstTime = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ps_Save(object sender, </pre>
<pre style="margin:0em;"> 	PalettePersistEventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     e.ConfigurationSection.WriteProperty(</pre>
<pre style="margin:0em;"> 		<span style="color:#a31515">&quot;IsFirstTime&quot;</span><span style="color:#000000"> , <span style="color:#a31515">&quot;No&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
