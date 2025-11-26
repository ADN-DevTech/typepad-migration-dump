---
layout: "post"
title: "How to use Toggle Button Ribbon API"
date: "2015-03-23 22:52:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/how-to-use-toggle-button-ribbon-api.html "
typepad_basename: "how-to-use-toggle-button-ribbon-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>Recently I have received a request from an ADN partner on how to create a Toggle Button which has toggle state IsChecked.</p>
<p>I will discuss about Windows runtime API only not CUI,if want to explore CUI you can refer this nice <a href="http://spiderinnet1.typepad.com/blog/2012/07/autocad-cui-ribbon-net-ribbon-toggle-button-not-possible.html" target="_blank">blog</a>.</p>
<p>One example of existing Toggle Button on AutoCAD UI is</p>
<p>Home\Groups</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080a58c5970d-pi"><img alt="ToggleButton" border="0" height="134" src="/assets/image_593892.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="ToggleButton" width="96" /></a></p>
<p>Code is self explanatory.</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;ToggleButton&quot;</span>)]</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> ToggleButton()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonControl</span> ribbonControl</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = Autodesk.Windows.<span style="color: #2b91af;">ComponentManager</span>.Ribbon;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">RibbonTab</span> Tab = <span style="color: blue;">new</span> <span style="color: #2b91af;">RibbonTab</span>();</p>
<p style="margin: 0px;">Tab.Title = <span style="color: #a31515;">&quot;Test Ribbon&quot;</span>;</p>
<p style="margin: 0px;">Tab.Id = <span style="color: #a31515;">&quot;TESTRIBBON_TAB_ID&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">ribbonControl.Tabs.Add(Tab);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonPanelSource</span> srcPanel</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = <span style="color: blue;">new</span> Autodesk.Windows.<span style="color: #2b91af;">RibbonPanelSource</span>();</p>
<p style="margin: 0px;">srcPanel.Title = <span style="color: #a31515;">&quot;Panel1&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">RibbonPanel</span> Panel = <span style="color: blue;">new</span> <span style="color: #2b91af;">RibbonPanel</span>();</p>
<p style="margin: 0px;">Panel.Source = srcPanel;</p>
<p style="margin: 0px;">Tab.Panels.Add(Panel);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonToggleButton</span> button</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = <span style="color: blue;">new</span> Autodesk.Windows.<span style="color: #2b91af;">RibbonToggleButton</span>();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">button.Text = <span style="color: #a31515;">&quot;Toggle Button&quot;</span>;</p>
<p style="margin: 0px;">button.Size = <span style="color: #2b91af;">RibbonItemSize</span>.Large;</p>
<p style="margin: 0px;"><span style="color: green;">/*</span></p>
<p style="margin: 0px;"><span style="color: green;">Embedding Image resource to your project</span></p>
<p style="margin: 0px;"><span style="color: green;">1.On the Project menu, choose Add Existing Item.</span></p>
<p style="margin: 0px;"><span style="color: green;">2.Navigate to the image you want to add to your project.</span></p>
<p style="margin: 0px;"><span style="color: green;">* Click the Open button to add the image to your project&#39;s file list.</span></p>
<p style="margin: 0px;"><span style="color: green;">3.Right-click the image in your project&#39;s file list and choose Properties.</span></p>
<p style="margin: 0px;"><span style="color: green;">* The Properties window appears.</span></p>
<p style="margin: 0px;"><span style="color: green;">4.Find the Build Action property in the Properties window.</span></p>
<p style="margin: 0px;"><span style="color: green;">* Change its value to Embedded Resource.</span></p>
<p style="margin: 0px;"><span style="color: green;">*</span></p>
<p style="margin: 0px;"><span style="color: green;">Then opens a stream on the embedded image.</span></p>
<p style="margin: 0px;"><span style="color: green;">* The name used to refer to the image takes this form:</span></p>
<p style="margin: 0px;"><span style="color: green;">&lt;namespace&gt;.&lt;image name&gt;.&lt;format&gt; </span></p>
<p style="margin: 0px;"><span style="color: green;">*/</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">button.Image = getBitmap(<span style="color: #a31515;">&quot;TestCmd2015.Koala.jpg&quot;</span>, 16, 16);</p>
<p style="margin: 0px;">button.LargeImage = getBitmap(<span style="color: #a31515;">&quot;TestCmd2015.Koala.jpg&quot;</span>, 32, 32);</p>
<p style="margin: 0px;">button.ShowText = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">button.CommandParameter = <span style="color: #a31515;">&quot;&quot;</span>;</p>
<p style="margin: 0px;">button.CommandHandler = <span style="color: blue;">new</span> <span style="color: #2b91af;">ToggleButtonCmdHandler</span>();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">srcPanel.Items.Add(button);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Tab.IsActive = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">ToggleButtonCmdHandler</span> : System.Windows.Input.<span style="color: #2b91af;">ICommand</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">bool</span> CanExecute(<span style="color: blue;">object</span> parameter)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">event</span> <span style="color: #2b91af;">EventHandler</span> CanExecuteChanged;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> Execute(<span style="color: blue;">object</span> parameter)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Document</span> doc =</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonToggleButton</span> button</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = parameter <span style="color: blue;">as</span> Autodesk.Windows.<span style="color: #2b91af;">RibbonToggleButton</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">doc.Editor.WriteMessage(</p>
<p style="margin: 0px;"><span style="color: #a31515;">&quot;\nRibbonButton Executed: &quot;</span> +</p>
<p style="margin: 0px;">button.Text +</p>
<p style="margin: 0px;"><span style="color: #a31515;">&quot; (IsChecked: &quot;</span> + button.IsChecked.ToString() + <span style="color: #a31515;">&quot;)&quot;</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: #2b91af;">BitmapImage</span> getBitmap(<span style="color: blue;">string</span> imageName, <span style="color: blue;">int</span> Height, <span style="color: blue;">int</span> Width)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">BitmapImage</span> image = <span style="color: blue;">new</span> <span style="color: #2b91af;">BitmapImage</span>();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">image.BeginInit();</p>
<p style="margin: 0px;">image.StreamSource</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = <span style="color: #2b91af;">Assembly</span>.GetExecutingAssembly().GetManifestResourceStream(imageName);</p>
<p style="margin: 0px;"><span style="color: green;">// image.UriSource = new Uri(imageName);</span></p>
<p style="margin: 0px;">image.DecodePixelHeight = Height;</p>
<p style="margin: 0px;">image.DecodePixelWidth = Width;</p>
<p style="margin: 0px;">image.EndInit();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> image;</p>
<p style="margin: 0px;">}</p>
</div>
