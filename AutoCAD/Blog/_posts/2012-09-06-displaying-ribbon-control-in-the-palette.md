---
layout: "post"
title: "Displaying Ribbon Control in the palette"
date: "2012-09-06 20:54:00"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/displaying-ribbon-control-in-the-palette.html "
typepad_basename: "displaying-ribbon-control-in-the-palette"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>I want to show a separate ribbon control. Can you show me how to create and display it ? </p>
<div><strong>Solution</strong></div>
<p>The RibbonControl is a class that inherits “System.Windows.Controls.Control” and so requires a WPF host (Ex: WPF User control) to display it.</p>
<p> The WPF User control can be associated with the AutoCAD palette.</p>
<p> Only the pertinent code is shown here. Please download the attachment for the sample project.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(_ps == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; _ps = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PaletteSet(</span><span style="color: #a31515; line-height: 140%;">&quot;WPF Palette&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; _ps.Size = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Size(400, 600);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; _ps.DockEnabled </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = (DockSides)((</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)DockSides.Left + (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)DockSides.Right);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; MyWPFUserControl uc = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> MyWPFUserControl();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Autodesk.Windows.RibbonControl ribControl </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Autodesk.Windows.RibbonControl();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; RibbonTab ribTab = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> RibbonTab();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.Title = </span><span style="color: #a31515; line-height: 140%;">&quot;Test&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.Id = </span><span style="color: #a31515; line-height: 140%;">&quot;Test&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribControl.Tabs.Add(ribTab);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; RibbonPanelSource ribSourcePanel = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> RibbonPanelSource();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanel.Title = </span><span style="color: #a31515; line-height: 140%;">&quot;My Tools&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanel.DialogLauncher =&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> RibbonCommandItem();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanel.DialogLauncher.CommandHandler </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AdskCommandHandler();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//Add a Panel</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; RibbonPanel ribPanel = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> RibbonPanel();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribPanel.Source = ribSourcePanel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribTab.Panels.Add(ribPanel);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//Create button</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; RibbonButton ribButton1 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> RibbonButton();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.Text = </span><span style="color: #a31515; line-height: 140%;">&quot;Line&quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;\n&quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;Generator&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.CommandParameter = </span><span style="color: #a31515; line-height: 140%;">&quot;Line &quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.ShowText = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.LargeImage </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = Images.getBitmap((Bitmap)_resourceManager.GetObject(</span><span style="color: #a31515; line-height: 140%;">&quot;LineImage&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.Image </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = Images.getBitmap((Bitmap)_resourceManager.GetObject(</span><span style="color: #a31515; line-height: 140%;">&quot;LineImage&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.Size = RibbonItemSize.Large;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.Orientation = System.Windows.Controls.Orientation.Vertical;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.ShowImage = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.ShowText = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribButton1.CommandHandler = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AdskCommandHandler();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ribSourcePanel.Items.Add(ribButton1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; uc.Content = ribControl;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; _ps.AddVisual(</span><span style="color: #a31515; line-height: 140%;">&quot;Test&quot;</span><span style="line-height: 140%;">, uc);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">_ps.KeepFocus = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_ps.Visible = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
</div>
<p></p>
<p>Here is a screenshot of the ribbon displayed in a palette :</p>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3199cb7d970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017c3199cb7d970b image-full" alt="1" title="1" src="/assets/image_766408.jpg" border="0" /></a><br />
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c3199cc6f970b"><a href="http://adndevblog.typepad.com/files/wpfpalette0.zip">Download Wpfpalette0</a></span>
