---
layout: "post"
title: "Create snapshot of the graphics in the ViewControl"
date: "2012-08-17 11:02:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/create-snapshot-of-the-graphics-in-the-viewcontrol.html "
typepad_basename: "create-snapshot-of-the-graphics-in-the-viewcontrol"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have an Autodesk.Navisworks.Api.Controls.ViewControl in my .NET application and would like to create a snapshot of the current view and save it to a bitmap.</p>
<p><strong>Solution</strong></p>
<p>You could simply use the .NET Framework to achieve that.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> buttonSnapShot_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Bitmap</span><span style="line-height: 140%;"> bmp = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Bitmap</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; viewControl.Bounds.Width, viewControl.Bounds.Height);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Graphics</span><span style="line-height: 140%;"> graphics = </span><span style="color: #2b91af; line-height: 140%;">Graphics</span><span style="line-height: 140%;">.FromImage(bmp);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// If you are stepping through the code and the debugger </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// (or anything else) is in front of the ViewControl, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// then that will be captured instead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; graphics.CopyFromScreen(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; viewControl.PointToScreen(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point</span><span style="line-height: 140%;">(0, 0)),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point</span><span style="line-height: 140%;">(0, 0),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Size</span><span style="line-height: 140%;">(bmp.Width, bmp.Height));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; bmp.Save(</span><span style="color: #a31515; line-height: 140%;">&quot;c:\\test.bmp&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
