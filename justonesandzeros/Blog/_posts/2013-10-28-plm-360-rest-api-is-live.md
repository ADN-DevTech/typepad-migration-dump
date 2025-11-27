---
layout: "post"
title: "PLM 360 REST API is Live"
date: "2013-10-28 13:00:33"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2013/10/plm-360-rest-api-is-live.html "
typepad_basename: "plm-360-rest-api-is-live"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" />    <br /><img alt="" src="/assets/Announcements4.png" /></p>
<p>If you go to <a href="http://help.autodesk.com/view/PLM/ENU/">PLM 360 Online Help</a>, you may notice a new section at the bottom.&#0160; The “<strong>REST API v2 Reference</strong>” has everything you need to get started developing client applications for PLM 360.</p>
<p>Since I’m an engineer, I’ll immediately dive in to the bad news...</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p>The API is in <strong>tech preview</strong> mode.&#0160; That means we are still stabilizing it.&#0160; Support is limited, and breaking API changes may occur without warning.&#0160; It’s perfect if you want to be an early adopter, but I highly discourage writing anything mission critical at this time.&#0160; Please keep your apps limited to prototypes and easy-to-update utilities.&#0160; </p>
<p>Once the API stabilizes, we will remove the tech preview restrictions and provide official support.&#0160; I have no ETA for when this will happen since stability isn’t something I want to rush.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p>The next piece of bad news is that you need an <strong>Oxygen key</strong> to log in.&#0160; Oxgyen is the service that let’s users log in with their Autodesk ID.&#0160; You need to first request a key.&#0160; Once you have your key, you have to write a fair amount of code to allow your app to log in to PLM.&#0160; All these steps are outlined in the “Authentication” section of the online API help.</p>
<p>The good news is that one you are logged in, the rest of the PLM 360 API is pretty straightforward.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p>Lastly, there is a distinct lack of <strong>API libraries</strong>.&#0160; Most of the burden is on the client developer to deal with the low level REST details.&#0160; Again, this is because the API is for early adopters.&#0160; There are some <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=22261515&amp;linkID=19386672">sample apps</a> to help you, but they don’t contain full API coverage.&#0160; If you want to do anything not shown in a sample app, you will have to write the proxy code yourself.</p>
<p>We will eventually have a set of libraries to hide the low level REST details, but I don’t have an ETA at this time.&#0160; If you are a long time reader of this blog, you know that I like to write sample apps.&#0160; So I’m <em>very</em> motivated to get these libraries in place.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p>If you have any questions or comments about the REST API, I suggest posting to the <a href="http://forums.autodesk.com/t5/PLM-360-General/bd-p/705">PLM 360 discussion group</a>.&#0160; Please put “REST API” in the title, so that I can easily find it. </p>
<p>Keep reading this blog for the latest PLM 360 API announcements, sample apps, articles, tips and tricks, etc.&#0160; From here on out, this blog will be covering the APIs for both Vault and PLM 360.</p>
<hr noshade="noshade" style="color: #ff0000;" />
