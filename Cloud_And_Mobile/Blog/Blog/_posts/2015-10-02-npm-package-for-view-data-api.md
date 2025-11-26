---
layout: "post"
title: "NPM Package for View & Data API"
date: "2015-10-02 15:55:03"
author: "Philippe Leefsma"
categories:
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/10/npm-package-for-view-data-api.html "
typepad_basename: "npm-package-for-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>This week I gave a try at creating my first <a href="https://www.npmjs.com/" target="_self">npm</a> package, bringing a modest contribution to that wonderful technology.</p>
<p>I'm not going to write yet another tutorial about how to publish your own npm package, you should be able to find plenty of resources on the web.&nbsp;Here a <a href="https://quickleft.com/blog/creating-and-publishing-a-node-js-module/" target="_self">pretty neat and concise tutorial</a> I stumble across.</p>
<p>Basically creating an npm package consists of creating a repository on github which include a specific description file <strong><em>package.json</em></strong>, then register that package under a unique name so other programmers will be able to install it easily by typing <strong><em>npm install my-package-name</em></strong>&nbsp;and start using it in their application.</p>
<p>What I liked about the tutorial I pointed out above is that it describes how to create and register your package, but also how to write a test for it using <a href="https://www.npmjs.com/package/mocha" target="_self">mocha</a>, a test framework and <a href="https://www.npmjs.com/package/chai" target="_self">chai</a>, an assertion library.</p>
<p>In the case of View &amp; Data API, any test would need to call a REST webservice, hence it needs to be asynchronous. I had to tweak a little the approach exposed in the post in order to achieve asynchronous tests.</p>
<p>At the moment this version of the package is quite minimalistic but it already allows you to achieve the basic complete workflow from your node application:&nbsp;</p>
<p>- Generate authetication tokens</p>
<p>- File upload, including large uploads using the resumable API</p>
<p>- Registration (ie. triggering translation of your file)</p>
<p>- And monitoring the translation of your model</p>
<p>In the future, I'm planning to keep supporting and enhance this package to maintain it on the edge of what is doable with View &amp; Data API.</p>
<p>The best example of use is probably the tests I wrote, so here they are. Remember, this is a <u>node.js server-side library</u>, so it doesn't do viewing.</p>
<p>You are able to install it using <em><strong>npm install view-and-data</strong></em> command.</p>
<p>Check <a href="https://github.com/Developer-Autodesk/view-and-data-npm" target="_self">view-and-data-npm</a> on github for more details.</p>
<script src="https://gist.github.com/leefsmp/36617f7e0a34e91f95be.js"></script>
