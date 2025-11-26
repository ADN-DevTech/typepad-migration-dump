---
layout: "post"
title: "2-Way databinding to a RibbonTextBox using WPF"
date: "2015-03-23 09:46:18"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/2-way-databinding-to-a-ribbontextbox-using-wpf.html "
typepad_basename: "2-way-databinding-to-a-ribbontextbox-using-wpf"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To have a RibbonTextBox automatically update your data when the user updates the ribbon textbox and vice-versa, you can use the RibbonTextBox.TextValueBinding to establish a 2-way databinding. Here is a sample code :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  ManufacturerData </pre>
<pre style="margin:0em;"> 	: System.ComponentModel.INotifyPropertyChanged</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">private</span><span style="color:#000000">  string manufacturerName;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  ManufacturerData() <span style="color:#000000">{</span> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  ManufacturerData(String manufacturer)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         manufacturerName = manufacturer;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  String ManufacturerProperty</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         get <span style="color:#000000">{</span> <span style="color:#0000ff">return</span><span style="color:#000000">  manufacturerName; <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         set</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             manufacturerName = value;</pre>
<pre style="margin:0em;">             OnPropertyChanged(<span style="color:#a31515">&quot;ManufacturerProperty&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">event</span><span style="color:#000000">  </pre>
<pre style="margin:0em;"> 		System.ComponentModel.</pre>
<pre style="margin:0em;"> 		PropertyChangedEventHandler PropertyChanged;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  OnPropertyChanged(string info)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         System.ComponentModel.PropertyChangedEventHandler </pre>
<pre style="margin:0em;"> 			handler = PropertyChanged;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (handler != null)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             handler(<span style="color:#0000ff">this</span><span style="color:#000000"> , <span style="color:#0000ff">new</span><span style="color:#000000">  System.ComponentModel.</pre>
<pre style="margin:0em;"> 				PropertyChangedEventArgs(info));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  Commands </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  ManufacturerData _data </pre>
<pre style="margin:0em;"> 		= <span style="color:#0000ff">new</span><span style="color:#000000">  ManufacturerData(<span style="color:#a31515">&quot;Autodesk&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  _added = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [CommandMethod(<span style="color:#a31515">&quot;RTB&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  RibbonTextBoxMethod()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (!_added)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Autodesk.Windows.RibbonControl rc </pre>
<pre style="margin:0em;"> 				= Autodesk.Windows.ComponentManager.Ribbon;</pre>
<pre style="margin:0em;">             Autodesk.Windows.RibbonTab rt = null;</pre>
<pre style="margin:0em;">             foreach (Autodesk.Windows.RibbonTab tab </pre>
<pre style="margin:0em;"> 												in rc.Tabs)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (tab.AutomationName.Equals(<span style="color:#a31515">&quot;Add-ins&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     rt = tab;</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (rt == null)</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Autodesk.Windows.RibbonPanelSource rps </pre>
<pre style="margin:0em;"> 				= <span style="color:#0000ff">new</span><span style="color:#000000">  Autodesk.Windows.RibbonPanelSource();</pre>
<pre style="margin:0em;">             rps.Title = <span style="color:#a31515">&quot;MyPanel&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             Autodesk.Windows.RibbonPanel rp </pre>
<pre style="margin:0em;"> 				= <span style="color:#0000ff">new</span><span style="color:#000000">  Autodesk.Windows.RibbonPanel();</pre>
<pre style="margin:0em;">             rp.Source = rps;</pre>
<pre style="margin:0em;">             rt.Panels.Add(rp);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Autodesk.Windows.RibbonTextBox rtb </pre>
<pre style="margin:0em;"> 				= <span style="color:#0000ff">new</span><span style="color:#000000">  Autodesk.Windows.RibbonTextBox();</pre>
<pre style="margin:0em;">             rtb.Id = <span style="color:#a31515">&quot;MyRTB&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             rtb.Text = <span style="color:#a31515">&quot;Manufacturer&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             rtb.ShowText = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             rps.Items.Add(rtb);</pre>
<pre style="margin:0em;">             rt.IsActive = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             System.Windows.Data.Binding myBinding </pre>
<pre style="margin:0em;"> 				= <span style="color:#0000ff">new</span><span style="color:#000000">  System.Windows.Data.Binding</pre>
<pre style="margin:0em;"> 				(<span style="color:#a31515">&quot;ManufacturerProperty&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             myBinding.Source = _data;</pre>
<pre style="margin:0em;">             myBinding.Mode </pre>
<pre style="margin:0em;"> 				= System.Windows.Data.BindingMode.TwoWay;</pre>
<pre style="margin:0em;">             rtb.TextValueBinding = myBinding;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             _added = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
