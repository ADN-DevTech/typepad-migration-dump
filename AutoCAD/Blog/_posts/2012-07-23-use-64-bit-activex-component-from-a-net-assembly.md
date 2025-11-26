---
layout: "post"
title: "Use 64 bit ActiveX component from a .NET assembly"
date: "2012-07-23 03:18:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/use-64-bit-activex-component-from-a-net-assembly.html "
typepad_basename: "use-64-bit-activex-component-from-a-net-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;ve been using the ADN utilities, which include some ActiveX controls. Now I&#39;m migrating my project to x64 OS and have downloaded the 64 bit version of the controls, however I cannot place them on my Form in Visual Studio.</p>
<p><strong>Solution</strong></p>
<p>Visual Studio is 32 bit on a 64 bit operating system as well, and it does not support 64 bit ActiveX components in the Form Designer. What you could do is create the component programmatically in your code.</p>
<p>In the following example I will be adding the AcadColor.ocx control to my .NET Form.</p>
<p>First, we need to create a wrapper for the ActiveX control. I start up <strong>Visual Studio 2008 x64 Win64 Command Prompt</strong>, go to the directory where the control is and run <strong>aximp.exe</strong> on it: Note: in case of Vista/Windows 7 you need to run the Command Prompt utility with elevated rights - right-click on the Command Prompt shortcut and select <strong>Run as administrator</strong></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176168affd0970c-pi" style="display: inline;"><img alt="Activex_aximp" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0176168affd0970c image-full" src="/assets/image_628993.jpg" title="Activex_aximp" /></a></p>
<p>If you get back the error <strong>AxImp Error: Did not find a registered ActiveX control in ...</strong>, then register the control first in the same command prompt window using <strong>regsvr32 AcadColor.ocx</strong>.</p>
<p>Now we can reference the created 2 dll&#39;s from the project (<strong>AutoCADColor.dll</strong> and <strong>AxAutoCADColor.dll</strong>) and then add the following code to the Form&#39;s constructor:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Windows.Forms;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> TestActiveX</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">partial</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Form1</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">Form</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> AxAutoCADColor.AxAcadColor acadColor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> Form1()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; InitializeComponent();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acadColor = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AxAutoCADColor.AxAcadColor();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acadColor.Location = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Drawing.</span><span style="color: #2b91af; line-height: 140%;">Point</span><span style="line-height: 140%;">(7, 7);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acadColor.Size = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Drawing.</span><span style="color: #2b91af; line-height: 140%;">Size</span><span style="line-height: 140%;">(150, 20);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; acadColor.Visible = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">this</span><span style="line-height: 140%;">.Controls.Add(acadColor);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Once we compiled the project to either <strong>Any CPU</strong> or <strong>x64</strong>, we can run it:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167689620a6970b-pi" style="display: inline;"><img alt="Activex_form1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0167689620a6970b" src="/assets/image_801408.jpg" title="Activex_form1" /></a><br /><br /></p>
