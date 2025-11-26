---
layout: "post"
title: "REX SDK: adding a new menu and menu items"
date: "2012-05-29 16:06:26"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/rex-sdk-adding-a-new-menu-and-menu-items.html "
typepad_basename: "rex-sdk-adding-a-new-menu-and-menu-items"
typepad_status: "Publish"
---

<div style="font-family: Consolas; font-size: 8pt; color: black; background: white;">
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada </a></span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;"><strong>Issue</strong></span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">I&#39;m looking at a ContentGeneratorWPF sample in the REX SDK.&#0160; I want to add my own menu items to the Revit extension menu. I can add the commands and menuItem in the MainControl.xaml file and it appears fine in the VS2010 designer, but when I run the menu is not visible. I tried setting the visibility in the OnLayout overload but still had no success. How can we add an item to the extension?&#0160;</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;"><strong>Solution<br /></strong></span><br /><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">In the sample ContentGeneratorWPF in the SDK, you will see MainControl.xaml.&#0160; There is a menu item called Calculations in the design view. It doesn’t appear when running in the Revit.&#0160;&#0160;</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">In order to make the menu visible, you will need to modify the code in:</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">Extension.cs &gt;&gt;&#0160; OnCreateLayout(), and add:</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; | (long)REXUI.SetupOptions.Menu;</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">and</span><br /><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UI.ShowCommand(REXUI.CommandOptions.MenuCalculation);</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UI.ShowCommand(REXUI.CommandOptions.MenuCalculationRun);</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">Below is the whole OnCreateLayout() method:</span></p>
<p><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt;">&#0160;</span></p>
</div>
<div style="font-family: Consolas; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">override</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">void</span><span style="line-hight: 140%;"> OnCreateLayout()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">base</span><span style="line-hight: 140%;">.OnCreateLayout();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; System.SetCaption();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">global</span><span style="line-hight: 140%;">::System.Windows.Media.Imaging.</span><span style="color: #2b91af; line-hight: 140%;">BitmapImage</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; logoImage =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; REXLibrary.GetResourceImage(GetType().Assembly,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;Resources/Other/Images/REX_logo.png&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// UI.SetLogo(logoImage);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Layout.ConstOptions =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)REXUI.SetupOptions.HSplitFixed</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; | (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)REXUI.SetupOptions.VSplitFixed</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; | (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)REXUI.SetupOptions.TabDialog</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; | (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)REXUI.SetupOptions.List</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; | (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)REXUI.SetupOptions.FormFixed</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// (1) added the menu on the dialog </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; | (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)REXUI.SetupOptions.Menu;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Layout.AddLayout(</span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> REXLayoutItem(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; REXLayoutItem.LayoutType.Layout, </span><span style="color: #a31515; line-hight: 140%;">&quot;Element&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;Element&quot;</span><span style="line-hight: 140%;">, (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)0, SelectedElementControlRef,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">, logoImage));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Layout.AddLayout(</span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> REXLayoutItem(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; REXLayoutItem.LayoutType.Layout, </span><span style="color: #a31515; line-hight: 140%;">&quot;Recognition&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;Recognition&quot;</span><span style="line-hight: 140%;">, (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)0, CGReadControlRef,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">, logoImage));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Layout.AddLayout(</span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> REXLayoutItem(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; REXLayoutItem.LayoutType.Layout, </span><span style="color: #a31515; line-hight: 140%;">&quot;Creation&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;Creation&quot;</span><span style="line-hight: 140%;">, (</span><span style="color: blue; line-hight: 140%;">long</span><span style="line-hight: 140%;">)0, CGCreateControlRef,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">, logoImage));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// (2) added </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; Menu is visible </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; UI.ShowCommand(REXUI.CommandOptions.MenuCalculation);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// Command is visible </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; UI.ShowCommand(REXUI.CommandOptions.MenuCalculationRun);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">////</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// insert code here.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (ExtensionRef != </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ExtensionRef.OnCreateLayout();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; SelectedElementControlRef.SetDialog();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CGReadControlRef.SetDialog();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CGCreateControlRef.SetDialog();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="font-family: arial,helvetica,sans-serif; font-size: 10pt; line-hight: 140%;">After this modification, the menu becomes visible.</span></p>
</div>
