---
layout: "post"
title: "The arrow of the Dialog Launcher button on my Ribbon panel does not show"
date: "2012-05-28 09:37:50"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/the-arrow-of-the-dialog-launcher-button-on-my-ribbon-panel-does-not-show.html "
typepad_basename: "the-arrow-of-the-dialog-launcher-button-on-my-ribbon-panel-does-not-show"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I set the DialogLauncher property of my RibbonPanelSource, but the icon does not appear on it:</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebe13257970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168ebe13257970c" alt="Arrow1" title="Arrow1" src="/assets/image_787861.jpg" border="0" /></a></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> RibbonPanel AddOnePanel()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; RibbonButton rb;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; RibbonPanelSource rps = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> RibbonPanelSource();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rps.Title = </span><span style="line-height: 140%; color: #a31515;">"Test One"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; RibbonPanel rp = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> RibbonPanel();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rp.Source = rps;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rb = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> RibbonButton();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rb.Name = </span><span style="line-height: 140%; color: #a31515;">"Test Button"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rb.ShowText = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rb.Text = </span><span style="line-height: 140%; color: #a31515;">"Test Button"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// Add the Button to the Tab</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rps.Items.Add(rb);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// Create a Command Item that the Dialog Launcher can use,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// for this test it is just a place holder.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; RibbonCommandItem rci = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> RibbonCommandItem();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rci.Name = </span><span style="line-height: 140%; color: #a31515;">"TestCommand"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// Assign the Command Item to the DialogLauncher which auto-enables</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// the little button at the lower right of a Panel, but where's </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&nbsp; // the arrow </span><span style="line-height: 140%; color: green;">you see in the stock Ribbons?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; rps.DialogLauncher = rci;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> rp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>If I tried to set the image myself, that did not work either:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">BitmapImage myBitMapImage = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> BitmapImage();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// BitmapImage.UriSource must be in a BeginInit/EndInit block.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">myBitMapImage.BeginInit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Change this string to a bmp file on your system</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">myBitMapImage.UriSource = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Uri(</span><span style="line-height: 140%; color: #a31515;">"C:/temp.png"</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UriKind.RelativeOrAbsolute);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">myBitMapImage.EndInit();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">rci.Image = myBitMapImage;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">rci.LargeImage = myBitMapImage;</span></p>
</div>
<p><strong>Solution</strong></p>
<p>You need to use a RibbonButton and assign that to the DialogLauncher:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">RibbonButton dialogLauncherButton = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> RibbonButton();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dialogLauncherButton.Name = </span><span style="line-height: 140%; color: #a31515;">"TestCommand"</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">rps.DialogLauncher = dialogLauncherButton;</span></p>
</div>

<p>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766dfc018970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016766dfc018970b" alt="Arrow2" title="Arrow2" src="/assets/image_212859.jpg" border="0" /></a></p>
