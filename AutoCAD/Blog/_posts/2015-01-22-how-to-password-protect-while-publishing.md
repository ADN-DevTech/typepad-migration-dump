---
layout: "post"
title: "How to Password Protect While Publishing"
date: "2015-01-22 22:27:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "Madhukar Moogala"
  - "WPF"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/how-to-password-protect-while-publishing.html "
typepad_basename: "how-to-password-protect-while-publishing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>  <p>In this blog I will be discussing how can we allows users to password protect while publishing documents programmatically , as there is no direct API to allow user to enter password ,we need to rely on some other means , for this I have two workarounds,</p>  <p>First and foremost is to edit the <a href="http://help.autodesk.com/view/ACD/2015/ENU/?guid=GUID-55DEBF19-ABC4-461F-9C48-956D7FFF703B">DSD</a> file [ASCII compatible],find for the strings “PromptForPwd = FALSE” and “PwdProtectPublishedDWF = FALSE” and replace strings with TRUE attribute, something like this, here is only a snippet but you can find fully usage in this forum which I’ve written. <a href="http://forums.autodesk.com/t5/net/password-protect-dwf-upon-publishing-using-vf-net/m-p/5479572#M43095" target="_blank">PasswordProtect</a> , those who are not aware of programmatically execute publish DSD please find helpful <a href="http://help.autodesk.com/view/ACD/2015/ENU/?guid=GUID-B08B5173-0C17-4663-BD1C-A40E4C8F3A75" target="_blank">resource</a>&#160;</p>  <div style="font-size: 9pt; font-family: consolas; background: white; color: black">   <p style="margin: 0px"><span style="color: green">/*DsdFile */</span></p>    <p style="margin: 0px"><span style="color: blue">string</span> dsdFile = dsdData.ProjectPath + dwgFileName + <span style="color: #a31515">&quot;.dsd&quot;</span>;</p>    <p style="margin: 0px">dsdData.WriteDsd(dsdFile);</p>    <p style="margin: 0px">System.IO.<span style="color: #2b91af">StreamReader</span> sr = <span style="color: blue">new</span> System.IO.<span style="color: #2b91af">StreamReader</span>(dsdFile);</p>    <p style="margin: 0px"><span style="color: blue">string</span> str = sr.ReadToEnd();</p>    <p style="margin: 0px">sr.Close();</p>    <p style="margin: 0px">str = str.Replace(<span style="color: #a31515">&quot;PromptForDwfName=TRUE&quot;</span>,</p>    <p style="margin: 0px">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">&quot;PromptForDwfName=FALSE&quot;</span>);</p>    <p style="margin: 0px"><span style="color: green">/*Prompts User to Enter Password and Reconfirms*/</span></p>    <p style="margin: 0px">str = str.Replace(<span style="color: #a31515">&quot;PromptForPwd=FALSE&quot;</span>,</p>    <p style="margin: 0px">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">&quot;PromptForPwd=TRUE&quot;</span>);</p>    <p style="margin: 0px">str = str.Replace(<span style="color: #a31515">&quot;PwdProtectPublishedDWF=FALSE&quot;</span>, </p>    <p style="margin: 0px">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">&quot;PwdProtectPublishedDWF=TRUE&quot;</span>);</p>    <p style="margin: 0px">&#160;</p>    <p style="margin: 0px"><font size="2" face="Arial">Second, is to use WPF <a href="https://msdn.microsoft.com/en-us/library/system.windows.controls.passwordbox%28v=vs.110%29.aspx" target="_blank">PasswordBoxx</a> API, I’ve was not aware of WPF earlier but this task led me to learn WPF and it was interesting to have this in my bucket, WPF experts looking at this blog are welcome to put suggestions and improvements.</font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial">There are few pundits suggesting web developers not to use PasswordBox as storing password in text form is prone to security risks,but I feel this would be significant in complex <a href="http://en.wikipedia.org/wiki/Model_View_ViewModel" target="_blank">MVVM</a> projects.And there are tricks like <a href="http://blog.functionalfun.net/2008/06/wpf-passwordbox-and-data-binding.html" target="_blank">PBDataBinding</a> to use this API with out loosing MVVM pattern architecture, as Password property of the API is not a dependency object we may lost pattern design,anyway we should not be bothering much about this fact in our case.</font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial">So, retrieve password from user&#160; and assign it to our DsdData.Password API. WPF PasswordBox API allows us to mask characters while entering and which is most desired.</font></p>    <p style="margin: 0px"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c73d2e2b970b-pi"><img title="PWD2" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="PWD2" src="/assets/image_584998.jpg" width="244" height="126" /></a></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial">Sample XAML:</font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px"><font size="2" face="Arial"></font></p>    <p style="margin: 0px">&#160;</p>    <p style="margin: 0px"><font size="2"></font></p> </div>  <pre style="margin: 0px; line-height: 125%"><font face="Consolas"><span style="color: #007700">&lt;Window</span> <span style="color: #0000cc">x:Class=</span><span style="background-color: #fff0f0">&quot;PWDProtectionPlot.PasswordWindow&quot;</span>
<span style="color: #0000cc">xmlns=</span><span style="background-color: #fff0f0">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span>
<span style="color: #0000cc">xmlns:x=</span><span style="background-color: #fff0f0">&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span>
<span style="color: #0000cc">Title=</span><span style="background-color: #fff0f0">&quot;PasswordWindow&quot;</span> <span style="color: #0000cc">Height=</span><span style="background-color: #fff0f0">&quot;88.746&quot;</span> <span style="color: #0000cc">Width=</span><span style="background-color: #fff0f0">&quot;300&quot;</span><span style="color: #007700">&gt;</span>
<span style="color: #007700">&lt;StackPanel&gt;</span>
<span style="color: #007700">&lt;Label</span> <span style="color: #0000cc">Content=</span><span style="background-color: #fff0f0">&quot;Password:&quot;</span> <span style="color: #007700">/&gt;</span>
<span style="color: #007700">&lt;PasswordBox</span> <span style="color: #0000cc">x:Name=</span><span style="background-color: #fff0f0">&quot;passwordBox&quot;</span>
             <span style="color: #0000cc">Width=</span><span style="background-color: #fff0f0">&quot;130&quot;</span>
             <span style="color: #0000cc">MaxLength=</span><span style="background-color: #fff0f0">&quot;8&quot;</span>
             <span style="color: #0000cc">Focusable=</span><span style="background-color: #fff0f0">&quot;True&quot;</span>
             <span style="color: #0000cc">GotFocus=</span><span style="background-color: #fff0f0">&quot;passwordBox_GotFocus&quot;</span>
             <span style="color: #0000cc">KeyDown=</span><span style="background-color: #fff0f0">&quot;passwordBox_KeyDown&quot;</span><span style="color: #007700">&gt;</span>
<span style="color: #007700">&lt;PasswordBox.ToolTip&gt;</span>
<span style="color: #007700">&lt;ToolTip</span> <span style="color: #0000cc">ToolTipService.ShowDuration=</span><span style="background-color: #fff0f0">&quot;20&quot;</span><span style="color: #007700">&gt;</span>
<span style="color: #007700">&lt;StackPanel&gt;</span>
<span style="color: #007700">&lt;TextBlock</span> <span style="color: #0000cc">FontWeight=</span><span style="background-color: #fff0f0">&quot;Light&quot;</span><span style="color: #007700">&gt;</span>
Eight (8)&quot; characters.Press Enter to OK
<span style="color: #007700">&lt;/TextBlock&gt;</span>
<span style="color: #007700">&lt;/StackPanel&gt;</span>
<span style="color: #007700">&lt;/ToolTip&gt;</span>
<span style="color: #007700">&lt;/PasswordBox.ToolTip&gt;</span>
<span style="color: #007700">&lt;/PasswordBox&gt;</span>        
<span style="color: #007700">&lt;/StackPanel&gt;</span>
<span style="color: #007700">&lt;/Window&gt;</span></font></pre>

<pre style="margin: 0px; line-height: 125%"><font face="Consolas"><span style="color: #007700"></span></font></pre>

<pre style="margin: 0px; line-height: 125%"><font face="Arial"><span style="color: #007700"></span></font></pre>

<p>Sample associated C# code:</p>

<div style="font-size: 9pt; font-family: consolas; background: white; color: black">
  <p style="margin: 0px"><span style="color: blue">public</span> <span style="color: blue">partial</span> <span style="color: blue">class</span> <span style="color: #2b91af">PasswordWindow</span> : <span style="color: #2b91af">Window</span></p>

  <p style="margin: 0px">{</p>

  <p style="margin: 0px"><span style="color: blue">public</span> PasswordWindow()</p>

  <p style="margin: 0px">{</p>

  <p style="margin: 0px">InitializeComponent();</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px"><span style="color: green">/*Password Changed Handler*/</span></p>

  <p style="margin: 0px"><span style="color: blue">private</span> <span style="color: blue">void</span> passwordBox_PasswordChanged(<span style="color: blue">object</span> sender, <span style="color: #2b91af">RoutedEventArgs</span> e)</p>

  <p style="margin: 0px">{</p>

  <p style="margin: 0px"><span style="color: blue">if</span> (passwordBox.Password.Length == 8)</p>

  <p style="margin: 0px">passwordBox.PasswordChanged -= passwordBox_PasswordChanged;</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px"><span style="color: green">/*We 'll close our password window when Enter key is pressed */</span></p>

  <p style="margin: 0px"><span style="color: blue">private</span> <span style="color: blue">void</span> passwordBox_KeyDown(<span style="color: blue">object</span> sender, <span style="color: #2b91af">KeyEventArgs</span> e)</p>

  <p style="margin: 0px">{</p>

  <p style="margin: 0px"><span style="color: blue">if</span> (e.Key == <span style="color: #2b91af">Key</span>.Enter) <span style="color: blue">if</span> (passwordBox.Password.Length &lt; 8)</p>

  <p style="margin: 0px"><span style="color: #2b91af">MessageBox</span>.Show(<span style="color: #a31515">&quot;Password Should be of \&quot;Eight\&quot;(8) Characters&quot;</span>);</p>

  <p style="margin: 0px"><span style="color: blue">else</span></p>

  <p style="margin: 0px">{</p>

  <p style="margin: 0px">e.Handled = <span style="color: blue">true</span>;</p>

  <p style="margin: 0px"><span style="color: blue">this</span>.Close();</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px"><span style="color: green">/*We'll trigger password changed handler when we get focus*/</span></p>

  <p style="margin: 0px"><span style="color: blue">private</span> <span style="color: blue">void</span> passwordBox_GotFocus(<span style="color: blue">object</span> sender, <span style="color: #2b91af">RoutedEventArgs</span> e)</p>

  <p style="margin: 0px">{</p>

  <p style="margin: 0px">passwordBox.PasswordChanged += passwordBox_PasswordChanged;</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px">}</p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><font size="2" face="Arial">Our WPF widget looks like,</font></p>

  <p style="margin: 0px"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c73d2e37970b-pi"><img title="PWD1" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="PWD1" src="/assets/image_44635.jpg" width="244" height="160" /></a></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><font face="Arial">Full source code can be <a href="https://github.com/MadhukarMoogala/MyBlogs/tree/master/PWDProtectionPlot" target="_blank">downloaded</a>.</font></p>
</div>

<p>Demo video is available <a href="https://screencast.autodesk.com/Main/Details/6dd38e4f-c131-4e60-aab9-fefc1e69cdd3" target="_blank">at</a>.</p>
