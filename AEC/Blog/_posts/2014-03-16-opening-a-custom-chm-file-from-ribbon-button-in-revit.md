---
layout: "post"
title: "Opening a custom chm file from Ribbon button in Revit"
date: "2014-03-16 22:35:59"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "Partha Sarkar"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/03/opening-a-custom-chm-file-from-ribbon-button-in-revit.html "
typepad_basename: "opening-a-custom-chm-file-from-ribbon-button-in-revit"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>We can use <strong>ContextualHelp&#0160;</strong>class to associate custom help.&#0160;<strong>ContextualHelp&#0160;</strong>allows us to start a locally installed help (chm) file, or linking to an external URL for custom help. The ContextualHelp class is used to create a type of contextual help, and then RibbonItem.<strong>SetContextualHelp()</strong> or RibbonItemData.<strong>SetContextualHelp()</strong> is used to associate it with a control. When a ContextualHelp instance is associated with a control, the text &quot;Press F1 for more help&quot; will appear below the tooltip when the mouse hovers over the control as shown below -</p>
<p>&#0160;</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d924fb3970d-pi" style="display: inline;"><img alt="Revit_Custom_HelpCHM_File" class="asset  asset-image at-xid-6a0167607c2431970b01a73d924fb3970d img-responsive" src="/assets/image_716943.jpg" title="Revit_Custom_HelpCHM_File" /></a></p>
<p>&#0160;</p>
<p>And here is the relevant C# .NET code snippet :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PushButtonData</span><span style="line-height: 140%;"> pushButtonDataHello </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; = </span><span style="color: blue; line-height: 140%;">new</span><span style="color: #2b91af; line-height: 140%;">PushButtonData</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;PushButtonHello&quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Hello World&quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _introLabPath,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _introLabName + </span><span style="color: #a31515; line-height: 140%;">&quot;.HelloWorld&quot;</span><span style="line-height: 140%;"> ); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PushButton</span><span style="line-height: 140%;"> pushButtonHello = panel.AddItem(pushButtonDataHello) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">PushButton</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pushButtonHello.LargeImage = NewBitmapImage(</span><span style="color: #a31515; line-height: 140%;">&quot;ImgHelloWorld.png&quot;</span><span style="line-height: 140%;">);&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pushButtonHello.ToolTip = </span><span style="color: #a31515; line-height: 140%;">&quot;simple push button&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// ContextualHelp class is used to create a type of contextual help&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">ContextualHelp</span><span style="line-height: 140%;"> contextHelp = </span><span style="color: blue; line-height: 140%;">new&#0160;</span><span style="color: #2b91af; line-height: 140%;">ContextualHelp</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">ContextualHelpType</span><span style="line-height: 140%;">.ChmFile, </span><span style="color: #a31515; line-height: 140%;">@&quot;C:\Temp\ToolTipDemo.chm&quot;</span><span style="line-height: 140%;">);</span></strong></span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="line-height: 140%;">pushButtonHello.SetContextualHelp(contextHelp);</span></strong></span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
