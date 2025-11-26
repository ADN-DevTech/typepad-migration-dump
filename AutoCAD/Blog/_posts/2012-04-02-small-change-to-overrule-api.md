---
layout: "post"
title: "Small change to Overrule API"
date: "2012-04-02 17:37:36"
author: "Madhukar Moogala"
categories:
  - "2012"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/small-change-to-overrule-api.html "
typepad_basename: "small-change-to-overrule-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html" target="_self">Stephen Preston</a></p>
<p>I recently became aware of a change in the Overrule API. As of AutoCAD 2012, you can no longer turn overruling off using Overrule.Overruling = False. This change was made to prevent programmers inadvertently disabling the increasing number of AutoCAD features that make use of this API – which is sensible. To turn your custom overrules on and off, you’ll need to register and unregister your overrules with the system – using Overrule&gt;AddOverrule() and Overrule.RemoveOverrule(). Unfortunately, this means that all the Overrule samples I wrote for my AU <a href="http://au.autodesk.com/?nd=class&amp;session_id=4940" target="_blank">2009</a> and <a href="http://au.autodesk.com/?nd=class&amp;session_id=7560" target="_blank">2010</a> classes are now outdated <img class="wlEmoticon wlEmoticon-confusedsmile" style="border-style: none;" src="/assets/image_612184.jpg" alt="Confused smile" />.</p>
<p>Overrules have been around since AutoCAD 2010.</p>
