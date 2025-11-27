---
layout: "post"
title: "Features of the PLM 360 REST API"
date: "2013-10-29 12:19:58"
author: "Doug Redmond"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2013/10/features-of-the-plm-360-rest-api.html "
typepad_basename: "features-of-the-plm-360-rest-api"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" />    <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>OK, great, there is an API for PLM 360.&#0160; So what can one do with it?&#0160; How is it different than scripting?&#0160; What does REST even mean?</p>
<p><strong>Ultra short definition of REST:</strong>&#0160;&#0160; <br /><em>REST allows a client to communicate with a server via HTTP.</em></p>
<p>You can <a href="http://en.wikipedia.org/wiki/REST">read more about REST</a> if you want. But for the purposes of this article, REST means that you can write a client app that talks to PLM 360 over HTTP.</p>
<p><strong>Scripting vs REST:      <br /></strong>PLM 360 has a scripting engine for performing <strong>server-site</strong> tasks in response to certain events.&#0160; Scripting is great but it can’t integrate two systems or plug-in to AutoCAD.&#0160; For <strong>client-side</strong> operations, you need the REST API.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Features of the API:      <br /></strong>There are a lot of things PLM does, but not all those features are available yet in the API.&#0160; The initial focus of the API is to provide a stable framework to build on.&#0160; I believe we now have that framework, so the next steps are to add more features.</p>
<p>Current feature set:</p>
<ul>
<li>Login / logout.</li>
<li>Get all the workspaces. </li>
<li>Get detailed information on a workspace (item details tab only). </li>
<li>Get a list of all the items in the workspace. </li>
<li>Get a filtered list of items in a workspace. </li>
<li>Read data on an item (item details tab only). </li>
<li>Add / edit / delete an item. </li>
<li>Get a list of all files attached to an item. </li>
<li>Upload /download files. </li>
<li>Checkin / checkout / undo checkout of a file. </li>
</ul>
<p>Note: All operations and data are scoped to the permissions of the logged in user.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Uses for the API:      <br /></strong>There is enough functionality here to start writing some interesting apps.&#0160; Here are some ideas.&#0160; Some of these ideas can already be seen in sample apps.</p>
<ul>
<li>Take a picture with your mobile device and attach it to an item in PLM 360.&#0160; (see <a href="http://images.autodesk.com/adsk/files/PLMPhotoAttach-Java-src.zip">Java sample</a>) </li>
<li>CAD plug-in that pulls data from PLM 360 and uses it in the model. (see <a href="https://github.com/ADN-DevTech/PLM360-API-Samples/tree/master/Material%20Profiler%20Inventor%20Add-In%20%2B%20PLM360">Material Profile example</a>) </li>
<li>Integrations with other systems. </li>
<li>Custom views of PLM data. </li>
</ul>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>API Updates:      <br /></strong>In theory, the API can be updated any time PLM 360 updates, and <strong>PLM 360 updates about every 4 weeks</strong>.&#0160;&#0160; That means there will be a constant stream of new API functionality.&#0160; It’s not like with Vault where you have to wait a year for API updates.&#0160; If there is a specific feature you want API support for, let us know in the <a href="http://forums.autodesk.com/t5/PLM-360-IdeaStation/idb-p/3">idea station</a>.&#0160; That will help us prioritize what to work on next.</p>
<hr noshade="noshade" style="color: #d09219;" />
