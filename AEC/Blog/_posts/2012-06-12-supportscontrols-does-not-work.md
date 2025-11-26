---
layout: "post"
title: "SupportsControls does not work"
date: "2012-06-12 19:20:42"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/supportscontrols-does-not-work.html "
typepad_basename: "supportscontrols-does-not-work"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong></p>  <p>As I understand, any plug-in will be the PluginRecord in the plug-in collection of Navisworks. They are visible in Navisworks. And Options = PluginOptions.SupportsControls will make the plug-in visible to an application of .NET control. One sample code (topic PluginRecord Class) demos how to iterate the plug-ins in a control application. However the collection always returned 0 items when I tested with a .NET control application, even though I have set one of my plug-ins with SupportsControls.</p>  <p><strong>Solution</strong></p>  <p>Currently .NET API does not support loading plug-ins within control based solutions, so the flag SupportsControls is&#160; for now</p>
