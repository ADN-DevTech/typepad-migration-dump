---
layout: "post"
title: "Deploying Forge ASP.NET samples to Appharbor"
date: "2017-01-26 03:04:31"
author: "Augusto Goncalves"
categories:
  - "ASP .NET"
  - "Augusto Goncalves"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/deploying-forge-aspnet-samples-to-appharbor.html "
typepad_basename: "deploying-forge-aspnet-samples-to-appharbor"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p><a href="https://appharbor.com/">Appharbor</a>: <em>Where .NET apps grow and prosper</em>. &#0160;That&#39;s their slogan, but I like to think them as a &quot;Heroku for ASP.NET&quot;. And they are that easy and have a <a href="https://appharbor.com/pricing" rel="noopener noreferrer" target="_blank">free tier</a>!</p>
<p>This post shows some basic steps on how&#0160;to deploy a Github code (one of our samples, for instance), but can also be directly connected to <a href="https://support.appharbor.com/kb/getting-started/deploying-your-first-application-using-git" rel="noopener noreferrer" target="_blank">another git</a>.&#0160;The very first step is to create an account. When properly registered, go to &quot;Your applications&quot; on the top menu, then &quot;Create new application&quot;.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09718ceb970d-pi" style="display: inline;"><img alt="Create_app" class="asset  asset-image at-xid-6a0167607c2431970b01bb09718ceb970d img-responsive" src="/assets/image_d18e79.jpg" title="Create_app" /></a></p>
<p>Next, on getting started, select Github deploy. The next page will show a list of all your repositories. You should select a .NET sample, if you don&#39;t have one, fork one of our <a href="https://autodesk-forge.github.io/">ASP.NET samples</a>.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09718d07970d-pi" style="display: inline;"><img alt="Connect_github" class="asset  asset-image at-xid-6a0167607c2431970b01bb09718d07970d img-responsive" src="/assets/image_45ec78.jpg" title="Connect_github" /></a></p>
<p>Finally, we need to setup the Forge Client ID &amp; Secret (assuming you already have a <a href="https://developer.autodesk.com">Forge Developer account</a> with an app created). Go to &quot;Configuration variables&quot; section, then click on &quot;New configuration variable&quot; and add both FORGE_CLIENT_ID and FORGE_CLIENT_SECRET values. The image below show how it should look like when both variables are created.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09718d33970d-pi" style="display: inline;"><img alt="Config_vars" class="asset  asset-image at-xid-6a0167607c2431970b01bb09718d33970d img-responsive" src="/assets/image_eeefa8.jpg" title="Config_vars" /></a></p>
<p>Many of our samples will handle with upload of files, which needs to be uploaded to the app server (in this case, Appharbor) before the app can upload again to Autodesk servers. For that, go to &quot;Settings&quot; section and &quot;Enable File System Write Access&quot;. Best practice is to upload files to /App_Data folder. Note: an app should not allow end-user to upload directly to Autodesk as this requires an access token with write capabilities, and if the end-user have such a token, this is a security breach.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ce5324970b-pi" style="display: inline;"><img alt="Settings" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ce5324970b img-responsive" src="/assets/image_d4983d.jpg" title="Settings" /></a></p>
<p>As a result, under your Github account settings, you should see Appharbor as an &quot;Authorized&#0160;application&quot;.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2589e7b970c-pi" style="display: inline;"><img alt="Github_settings" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2589e7b970c img-responsive" src="/assets/image_cc95b1.jpg" title="Github_settings" /></a></p>
<p>That&#39;s it. Now when you commit changes to the repository the Appharbor app will deploy and rebuild.</p>
