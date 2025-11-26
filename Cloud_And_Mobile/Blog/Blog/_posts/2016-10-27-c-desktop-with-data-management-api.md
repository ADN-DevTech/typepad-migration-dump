---
layout: "post"
title: "C# desktop with Data Management API"
date: "2016-10-27 04:01:55"
author: "Augusto Goncalves"
categories:
  - "ASP .NET"
  - "Augusto Goncalves"
  - "Data Management"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/c-desktop-with-data-management-api.html "
typepad_basename: "c-desktop-with-data-management-api"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>This is a follow up from <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/10/3-legged-oauth-on-desktop-apps-c-winform.html">this previous post</a>, which demonstrates a basic approach for desktop app with 3-legged OAuth. One of the biggest challenge is to ensure your client ID &amp; secret are protected.&#0160;</p>
<p>For that, this sample has 3 projects:</p>
<ol>
<li>Forge: a class library (.DLL) that wraps some endpoints for OAuth, Data Management API and OSS.</li>
<li>ServerOAuth: this is the ASP.NET server that stores the client ID &amp; secret and implements the redirects and callback for 3-legged OAuth.</li>
<li>A360Sync: a WinForm (.EXE) desktop app that monitors a local folder and upload files to the respective A360 account.</li>
</ol>
<p>Enough talking, let&#39;s see this sample in action:</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/4Pgg05tLW-M?feature=oembed" width="500"></iframe></p>
<p class="asset-video">The <span style="text-decoration: underline;">A360Sync</span>&#0160;project will call the <span style="text-decoration: underline;">ServerOAuth</span>&#0160;project to get the <a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authorize-GET/">Authorize endpoint URL</a>&#0160;and redirect the end-user to the sign-in page. As the client ID is passed on the URL, it&#39;s not really hidden (e.g. a proxy server will see this URL with the client ID), but the desktop client don&#39;t expose it. As soon as the end-user types his/her password, it will callback the <span style="text-decoration: underline;">ServerOAuth</span>, which calls <a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/">GetToken endpoint</a> with the client secret (which should not be shared under any circumstance). Now the <span style="text-decoration: underline;">ServerOAuth</span>&#0160;has the access token, so it redirects to a fake url that should trigger an error on the <span style="text-decoration: underline;">A360Sync</span> desktop app webbrowser control and bingo: it can store the access token. As this handles&#0160;<strong>only</strong>&#0160;the user data, it&#39;s ok that the desktop app can see the access token (its access is limited to his/her data and there is no reason to hack his own data).</p>
<p class="asset-video">One concern: <a href="https://en.wikipedia.org/wiki/Information_security#Non-repudiation">non-repudiation</a>. As the access token is a permission to our app access the end-user data, if the same end-user uses this access token to modify its data, on the records, the data was performed by the application (as the 3-legged OAuth is a permission between Autodesk, developer/app &amp; the end-user).</p>
<p>The source code is <a href="https://github.com/Developer-Autodesk/data.management-csharp-a360sync">available on Github</a>, but it should not be used on production just yet (still a work in progress), but I would love some feedback and will be improving it over the next few week.&#0160;</p>
<p>&#0160;</p>
