---
layout: "post"
title: "API Versioning for PLM 360"
date: "2013-10-31 15:45:38"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2013/10/api-versioning-for-plm-360.html "
typepad_basename: "api-versioning-for-plm-360"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /><br /><img alt="" src="/assets/Concepts4.png" /></p>
<p>The PLM 360 API, like most REST API’s, have a version component to their URLs.&#0160; However, the meaning of the version changes from system to system.&#0160; So let me explain what that version means for the PLM 360 API.</p>
<p>The <strong>API version</strong> is the part you see in the URL for a REST call.&#0160; So if you are getting a list of workspaces from https://mytenant.autodeskplm360.net/api/<strong><span style="color: #ff0000;">v2</span></strong>/workspaces/, 2 is the API version number.&#0160; In PLM 360, this number identifies <strong>a compatible API set</strong>.&#0160; In other words, v2 will maintain compatibility as new releases of PLM come out.&#0160; Things may be added to REST v2, but existing content will still work the same as before.</p>
<p>At some point in the future, there will be a v3 API, which will probably not be compatible with v2.&#0160; When new API versions are introduced, they will live side-by-side with existing API versions.&#0160; This gives developers ample time to move their code to the latest API version.&#0160; Here is a rough diagram of what I’m talking about. </p>
<p><img alt="" src="/assets/timeline.png" /></p>
<p>There is no set time for any of this.&#0160; The v3 API may come out in a year, two years, or maybe never at all.&#0160; Likewise the end-of-life for v2 is not set either.&#0160; It may be a year after v3 comes out, or two years, or maybe never.&#0160; All I can say is that the end-of-life for an API version will be announced ahead of time.&#0160; </p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>The <strong>product version</strong> for PLM 360 gets updated for every major PLM 360 release, which happens about every month.&#0160; You can easily find the current version by going to <strong>/version.txt</strong> on your tenant.&#0160; At the time of writing, 9.1 is the current version.&#0160; When PLM updates, new features may be added to the API.&#0160; So if you want to name a specific API schema, you need to include both the API version and the product version.&#0160; For example, <strong>“API schema 2:9.1”</strong> or something like that.&#0160; Most of you shouldn’t have to care about exact schemas.&#0160; The API version is usually sufficient.&#0160; However, I do recommend that you record the current PLM 360 product version somewhere in your code and/or readme documentation.&#0160; It will be a helpful data point if you run into issues later on.</p>
<hr noshade="noshade" style="color: #5acb04;" />
