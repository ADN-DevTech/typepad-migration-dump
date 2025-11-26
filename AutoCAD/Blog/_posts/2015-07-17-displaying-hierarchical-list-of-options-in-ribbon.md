---
layout: "post"
title: "Displaying hierarchical list of options in Ribbon"
date: "2015-07-17 01:51:47"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/displaying-hierarchical-list-of-options-in-ribbon.html "
typepad_basename: "displaying-hierarchical-list-of-options-in-ribbon"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you only need a list of options to be displayed, the RibbonSplitButton should do. Here is a <a href="http://adndevblog.typepad.com/autocad/2012/06/ribbonsplitbutton-with-icons-images.html">blog post</a> on that. But if you need further sub items for any one of those options, the RibbonMenuButton is suitable. Here is a sample code to display the options as shown in the below screenshot :</p>
<p></p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1395b43970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1395b43970c img-responsive" alt="MenuButton" title="MenuButton" src="/assets/image_379533.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Requires reference to AdWindows.dll</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.Windows;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.Drawing.Imaging;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;RibbonMenuButton&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  RibbonMenuButton()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     RibbonControl ribbonControl </pre>
<pre style="margin:0em;">         = ComponentManager.Ribbon;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonTab Tab = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonTab();</pre>
<pre style="margin:0em;">     Tab.Title = <span style="color:#a31515">&quot;Test Ribbon&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     Tab.Id = <span style="color:#a31515">&quot;TESTRIBBON_TAB_ID&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ribbonControl.Tabs.Add(Tab);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonPanelSource srcPanel </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonPanelSource();</pre>
<pre style="margin:0em;">     srcPanel.Title = <span style="color:#a31515">&quot;Panel1&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonPanel Panel = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonPanel();</pre>
<pre style="margin:0em;">     Panel.Source = srcPanel;</pre>
<pre style="margin:0em;">     Tab.Panels.Add(Panel);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonMenuItem button1 </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonMenuItem();</pre>
<pre style="margin:0em;">     button1.Text = <span style="color:#a31515">&quot;Button1&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     button1.ShowText = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     button1.LargeImage </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 32, 32);</pre>
<pre style="margin:0em;">     button1.Image </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 16, 16);</pre>
<pre style="margin:0em;">     button1.CommandHandler = <span style="color:#0000ff">new</span><span style="color:#000000">  MenuButtonCmdHandler();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonMenuItem subButton1 = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonMenuItem();</pre>
<pre style="margin:0em;">     subButton1.Text = <span style="color:#a31515">&quot;SubButton1&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     subButton1.ShowText = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     subButton1.LargeImage </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 32, 32);</pre>
<pre style="margin:0em;">     subButton1.Image </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 16, 16);</pre>
<pre style="margin:0em;">     subButton1.CommandHandler </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  MenuButtonCmdHandler();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonMenuItem subButton2 = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonMenuItem();</pre>
<pre style="margin:0em;">     subButton2.Text = <span style="color:#a31515">&quot;SubButton2&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     subButton2.ShowText = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     subButton2.LargeImage </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 32, 32);</pre>
<pre style="margin:0em;">     subButton2.Image </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 16, 16);</pre>
<pre style="margin:0em;">     subButton2.CommandHandler </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  MenuButtonCmdHandler();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     button1.Items.Add(subButton1);</pre>
<pre style="margin:0em;">     button1.Items.Add(subButton2);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonMenuItem button2 = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonMenuItem();</pre>
<pre style="margin:0em;">     button2.Text = <span style="color:#a31515">&quot;Button2&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     button2.ShowText = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     button2.LargeImage </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 32, 32);</pre>
<pre style="margin:0em;">     button2.Image </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 16, 16);</pre>
<pre style="margin:0em;">     button2.CommandHandler = <span style="color:#0000ff">new</span><span style="color:#000000">  MenuButtonCmdHandler();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     RibbonMenuButton ribMenuButton </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  RibbonMenuButton();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ribMenuButton.Id = <span style="color:#a31515">&quot;ADN.RibbonMenuButton.1&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ribMenuButton.Text = <span style="color:#a31515">&quot;RibbonMenuButton&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     ribMenuButton.ShowText = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     ribMenuButton.Size = RibbonItemSize.Large;</pre>
<pre style="margin:0em;">     ribMenuButton.LargeImage </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 32, 32);</pre>
<pre style="margin:0em;">     ribMenuButton.Image </pre>
<pre style="margin:0em;">         = getBitmap(Resources.Resource1.Image1, 16, 16);</pre>
<pre style="margin:0em;">     ribMenuButton.ShowImage = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     ribMenuButton.MaxHeight = <span style="color:#0000ff">double</span><span style="color:#000000"> .PositiveInfinity;</pre>
<pre style="margin:0em;">     ribMenuButton.MinHeight = 0;</pre>
<pre style="margin:0em;">     ribMenuButton.IsSplit = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     ribMenuButton.IsSynchronizedWithCurrentItem = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ribMenuButton.Items.Add(button1);</pre>
<pre style="margin:0em;">     ribMenuButton.Items.Add(button2);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     srcPanel.Items.Add(ribMenuButton);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Tab.IsActive = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> BitmapImage getBitmap(Bitmap bitmap, <span style="color:#0000ff">int</span><span style="color:#000000">  height, <span style="color:#0000ff">int</span><span style="color:#000000">  width)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     MemoryStream stream = <span style="color:#0000ff">new</span><span style="color:#000000">  MemoryStream();</pre>
<pre style="margin:0em;">     bitmap.Save(stream, ImageFormat.Png);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     BitmapImage bmp = <span style="color:#0000ff">new</span><span style="color:#000000">  BitmapImage();</pre>
<pre style="margin:0em;">     bmp.BeginInit();</pre>
<pre style="margin:0em;">     bmp.StreamSource = <span style="color:#0000ff">new</span><span style="color:#000000">  MemoryStream(stream.ToArray());</pre>
<pre style="margin:0em;">     bmp.DecodePixelHeight = height;</pre>
<pre style="margin:0em;">     bmp.DecodePixelWidth = width;</pre>
<pre style="margin:0em;">     bmp.EndInit();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  bmp;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  MenuButtonCmdHandler </pre>
<pre style="margin:0em;">     : System.Windows.Input.ICommand</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  CanExecute(<span style="color:#0000ff">object</span><span style="color:#000000">  parameter)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">event</span><span style="color:#000000">  EventHandler CanExecuteChanged;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  Execute(<span style="color:#0000ff">object</span><span style="color:#000000">  parameter)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Document doc </pre>
<pre style="margin:0em;">             = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (parameter <span style="color:#0000ff">is</span><span style="color:#000000">  RibbonMenuItem)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             RibbonMenuItem menuItem </pre>
<pre style="margin:0em;">                 = parameter <span style="color:#0000ff">as</span><span style="color:#000000">  RibbonMenuItem;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (menuItem != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">                     <span style="color:#a31515">&quot;\\nMenu Item Executed: &quot;</span><span style="color:#000000">  </pre>
<pre style="margin:0em;">                     + menuItem.Text);</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
