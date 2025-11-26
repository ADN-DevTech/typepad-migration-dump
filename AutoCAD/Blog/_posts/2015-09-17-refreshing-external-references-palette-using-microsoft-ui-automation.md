---
layout: "post"
title: "Refreshing External References palette using Microsoft UI Automation"
date: "2015-09-17 17:11:02"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "UI"
original_url: "https://adndevblog.typepad.com/autocad/2015/09/refreshing-external-references-palette-using-microsoft-ui-automation.html "
typepad_basename: "refreshing-external-references-palette-using-microsoft-ui-automation"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In this blog post we will look at refreshing the external references palette in AutoCAD. Before we get into the details, here is some background information on why it might be required to refresh that palette.</p>
<p>The external references palette in AutoCAD turns into an Enhanced Standard Window (ESW) when the Vault plugin for AutoCAD is installed. Since the check-in/ check-out status of the files are displayed in the external references palette, it is necessary for a way to refresh the palette to display the current status in case the file status got modified externally using Vault client.</p>
<p>Because there is no public API to do this, we will look at using Microsoft's UI Automation to simulate a click on the Refresh button. Please note that the approach suggested here is unsupported by Autodesk as it relies on Win32 and UI Automation API. If you need to use this approach, please test it more completely with your application.</p>
<p>To get the click on Refresh button to work, the Microsoft UI Automation has all the necessary API. Unfortunately, the nature of the Refresh button in the external references palette in AutoCAD posed a problem. By looking at the layout of the External References palette using Spy++, it becomes evident that the Refresh button is part of a Toolbar inside the palette. Also, the Refresh button is a drop-down with Reload and Refresh options. By using the UI Automation's Invoke pattern, it was not possible to simulate a mouse click on the Refresh button.</p>
<p>To work around this, we can resort to Win32 API's SendInput method to simulate a mouse click and gather the inputs required by that method using Microsoft UI Automation API. Here is a sample code that works ok in AutoCAD 2016. For other AutoCAD versions, you may need to identify the control-id of the Refresh button using Spy++ and update the code accordingly.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Add reference to UIAutomationClient.dll </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// and UIAutomationTypes.dll</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.Windows.Automation;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.Diagnostics;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.Runtime.InteropServices;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Runtime;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.EditorInput;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.DatabaseServices;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.ApplicationServices;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Based on http://www.pinvoke.net</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// /default.aspx/user32/SendInput.html</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// /default.aspx/Structures/MOUSEINPUT.html</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// /default.aspx/user32/mouse_event.html</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;user32.dll&quot;</span><span style="color:#000000"> , SetLastError = <span style="color:#0000ff">true</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  uint SendInput(uint nInputs, </pre>
<pre style="margin:0em;"> 	INPUT[] pInputs, <span style="color:#0000ff">int</span><span style="color:#000000">  cbSize);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [StructLayout(LayoutKind.Sequential)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">internal</span><span style="color:#000000">  <span style="color:#0000ff">struct</span><span style="color:#000000">  MOUSEINPUT</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  dx;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  dy;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  uint mouseData;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  uint dwFlags;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  uint time;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  IntPtr dwExtraInfo;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [StructLayout(LayoutKind.Sequential)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">internal</span><span style="color:#000000">  <span style="color:#0000ff">struct</span><span style="color:#000000">  INPUT</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  type;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  MOUSEINPUT mi;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  INPUT(uint flag)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         type = 0; <span style="color:#008000">// Mouse input</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         mi.dx = 0;</pre>
<pre style="margin:0em;">         mi.dy = 0;</pre>
<pre style="margin:0em;">         mi.mouseData = 0;</pre>
<pre style="margin:0em;">         mi.time = 0;</pre>
<pre style="margin:0em;">         mi.dwExtraInfo = IntPtr.Zero;</pre>
<pre style="margin:0em;">         mi.dwFlags = flag;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  MOUSEEVENTF_LEFTDOWN = 0x0002;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  MOUSEEVENTF_LEFTUP = 0x0004;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;XRefPalRefresh&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  XRefPalRefresh()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Editor ed = </pre>
<pre style="margin:0em;"> 		Application.DocumentManager.MdiActiveDocument.Editor;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">try</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         System.Diagnostics.Process p </pre>
<pre style="margin:0em;"> 			= Process.GetCurrentProcess();</pre>
<pre style="margin:0em;">         AutomationElement acadAutoElem = </pre>
<pre style="margin:0em;"> 			AutomationElement.RootElement.FindFirst(</pre>
<pre style="margin:0em;"> 			TreeScope.Children, </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">new</span><span style="color:#000000">  PropertyCondition(</pre>
<pre style="margin:0em;"> 			AutomationElement.ProcessIdProperty, p.Id));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// Control Id retreived for the Refresh button </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// in AutoCAD 2016 using Spy++</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         string btnRefreshhexID = <span style="color:#a31515">&quot;000075FB&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         string btnRefreshdecimalID = </pre>
<pre style="margin:0em;"> 			System.Convert.ToInt32(btnRefreshhexID, 16)</pre>
<pre style="margin:0em;"> 			.ToString();</pre>
<pre style="margin:0em;">         AutomationElement refreshBtnAutoElem </pre>
<pre style="margin:0em;"> 			= acadAutoElem.FindFirst(</pre>
<pre style="margin:0em;">             TreeScope.Descendants, </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">new</span><span style="color:#000000">  PropertyCondition(</pre>
<pre style="margin:0em;"> 			AutomationElement.AutomationIdProperty, </pre>
<pre style="margin:0em;"> 			btnRefreshdecimalID));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (refreshBtnAutoElem == null)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ed.WriteMessage(<span style="color:#a31515">&quot;Refresh button in </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				External References</pre>
<pre style="margin:0em;"> 				palette was not identified !<span style="color:#a31515">&quot;);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 </pre>
<pre style="margin:0em;">         <span style="color:#008000">// Using UI&#39;s Invoke pattern</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// Does work for simple buttons but not for </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// the Refresh button in </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">//&#39;s external references palette.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">//InvokePattern ipClickRefreshBtn = </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// (InvokePattern)refreshBtnAutoElem.GetCurrentPattern</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// (InvokePattern.Pattern);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">//ipClickRefreshBtn.Invoke();</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">//&#39;s resort to clicking by location.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         System.Windows.Point point </pre>
<pre style="margin:0em;"> 			= refreshBtnAutoElem.GetClickablePoint();</pre>
<pre style="margin:0em;">         System.Windows.Forms.Cursor.Position </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  System.Drawing.Point(</pre>
<pre style="margin:0em;"> 			(<span style="color:#0000ff">int</span><span style="color:#000000"> )point.X, (<span style="color:#0000ff">int</span><span style="color:#000000"> )point.Y);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         INPUT input1 = <span style="color:#0000ff">new</span><span style="color:#000000">  INPUT(MOUSEEVENTF_LEFTDOWN);</pre>
<pre style="margin:0em;">         INPUT input2 = <span style="color:#0000ff">new</span><span style="color:#000000">  INPUT(MOUSEEVENTF_LEFTUP);</pre>
<pre style="margin:0em;">         SendInput(2, <span style="color:#0000ff">new</span><span style="color:#000000"> [] <span style="color:#000000">{</span> input1, input2 <span style="color:#000000">}</span>, </pre>
<pre style="margin:0em;"> 			Marshal.SizeOf(typeof(INPUT)));</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">catch</span><span style="color:#000000">  (System.Exception ex)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ed.WriteMessage(ex.Message);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Before we end this blog post, a known limitation of this approach is that the external palette must be open and the Refresh button in it must not be masked by any other window.</p>
