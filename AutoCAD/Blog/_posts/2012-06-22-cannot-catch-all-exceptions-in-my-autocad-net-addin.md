---
layout: "post"
title: "Cannot catch all exceptions in my AutoCAD .NET AddIn"
date: "2012-06-22 08:02:15"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/cannot-catch-all-exceptions-in-my-autocad-net-addin.html "
typepad_basename: "cannot-catch-all-exceptions-in-my-autocad-net-addin"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In my application I can catch any exceptions derived from System.Exception by using</p>
<p><span style="font-family: &#39;courier new&#39;, courier; background-color: #e6e6e6;">catch (Exception ex)</span></p>
<p>but inside my AutoCAD AddIn it does not seem to work. What could I do?</p>
<p><strong>Solution</strong></p>
<p>It&#39;s a better practice to only catch exceptions that you actually expect to encounter and should let the rest be handled by AutoCAD. Otherwise you might be &quot;ignoring&quot; errors caused by your code that you did not think about - perhaps even leaving the drawing in a corrupt state.</p>
<p>Just check the error message that AutoCAD pops up. That should contain the exact exception type encountered: System.ArgumentException, System.Runtime.InteropServices.COMException, etc.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017615b87b2e970c-pi" style="display: inline;"><img alt="Exception" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017615b87b2e970c" src="/assets/image_960742.jpg" title="Exception" /></a></p>
<p>Once you know the exact exception, try to catch that in your code:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcadApplication acadApp = (AcadApplication)acApp.AcadApplication;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acadApp.MenuGroups.Load(</span><span style="color: #a31515; line-height: 140%;">&quot;something&quot;</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.ArgumentException ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; System.Windows.Forms.MessageBox.Show(</span><span style="color: #a31515; line-height: 140%;">&quot;Caught the exception&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
