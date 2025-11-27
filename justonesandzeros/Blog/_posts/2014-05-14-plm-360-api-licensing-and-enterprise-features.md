---
layout: "post"
title: "PLM 360 API Licensing and Enterprise Features"
date: "2014-05-14 16:24:18"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2014/05/plm-360-api-licensing-and-enterprise-features.html "
typepad_basename: "plm-360-api-licensing-and-enterprise-features"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/Announcements4.png" /></p>
<p>PLM 360 recently updated it’s <a href="http://www.autodeskplm360.com/pricing-model.html" target="_blank">pricing page</a>, so I’d like to point out a few things that are of interest to API developers.&#0160;</p>
<p><strong>API development is open to users with either a Professional or Enterprise license.</strong>&#0160; For in-house development, you can use your own Pro license for writing custom utilities.&#0160; For third party development, you can contact the Autodesk Development Network if you want a tenant and Pro license for development purposes.</p>
<p><strong>Sandbox environments are available.</strong>&#0160; This is very useful if you are writing an app or integration that modifies data.&#0160; Speaking from experience, you <em>don’t</em> want to debug on live data.&#0160; A sandbox environment is a clone of your live data.&#0160; So you can develop and debug your app without the worry of corrupting &quot;real&quot; data.&#0160; You are allowed one data refresh per month, so you still need to be careful.</p>
<p>Sandbox environments can either be on the regular production environment or on a “Preview” server.&#0160; Preview tenants run the beta code for the next release, so you can see changes before they go live.&#0160; This is useful if you have an integration that you need to keep healthy.&#0160; A Preview tenant can be used detect breakages before they happen.&#0160; At this time, I don’t have details on how much lead time you get with a Preview tenant.&#0160; I think it varies from release to release.</p>
<hr noshade="noshade" style="color: #ff0000;" />
