---
layout: "post"
title: "Debug ASP.NET in incognito mode and avoid cache issues"
date: "2017-01-18 06:16:18"
author: "Augusto Goncalves"
categories:
  - "ASP .NET"
  - "Augusto Goncalves"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/debug-aspnet-in-incognito-mode-and-avoid-cache-issues.html "
typepad_basename: "debug-aspnet-in-incognito-mode-and-avoid-cache-issues"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>Cache is a common source of problems during web development. It&#39;s not unusual&#0160;to solve some mysterious problems by cleaning the browser history (or just the pages stored locally). So why not do our localhost debug in incognito/private mode?</p>
<p>To make it the default option on Visual Studio is quite simple: on the &quot;Run&quot; toolbar button, click on the dropdown arrow, then select &quot;Browse With...&quot;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096e13fe970d-pi"><img alt="Run_button" class="asset  asset-image at-xid-6a0167607c2431970b01bb096e13fe970d img-responsive" src="/assets/image_0f34ad.jpg" style="margin: 0px 5px 5px 0px;" title="Run_button" /></a></p>
<p>Next, on select your browser. For Chrome add the <strong>--incognito</strong> argument, or <strong>-private-window</strong> for Firefox or&#0160;<strong>-private</strong> for Edge. Add a &quot;Friendly name&quot; such as &quot;Chrome Incognito&quot; and click &quot;Set as Default&quot; to make it even easier.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2551d5c970c-pi"><img alt="Add_browser" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2551d5c970c img-responsive" src="/assets/image_9bb40c.jpg" style="margin: 0px 5px 5px 0px;" title="Add_browser" /></a></p>
<p>That&#39;s it. Now when you close the browser and restart everything is cleaned and you should avoid cache problems during development.</p>
<p>Once your app is published, if you use a HTTPS, it also avoids cache as there is no local copy of files.</p>
