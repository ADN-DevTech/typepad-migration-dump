---
layout: "post"
title: "TypeScript Sample for View & Data API"
date: "2015-09-10 15:09:12"
author: "Philippe Leefsma"
categories:
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/typescript-sample-for-view-data-api.html "
typepad_basename: "typescript-sample-for-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>It's been a while I wanted to play with <a href="http://www.typescriptlang.org/" target="_self">TypeScript</a>&nbsp;as the technology seems to get a descent traction from the web development community. So here it is! You can check it out on the following github repository: <a href="https://github.com/Developer-Autodesk/TypeView">TypeView</a></p>

It's live version can be tested <a href="http://viewer.autodesk.io/node/typeview/">here</a>. Don't expect anything crazy yet, it's simply loading a predefined model.
<br/>
<p>Big thanks to <em><strong>Jan Liska</strong></em>, my colleague from Autodesk Consulting, who put together the initial version of that sample. The View &amp; Data client side API is rather big already so Jan created the type definition file only for the few methods that were needed in that specific sample. Hopefully over the time we will make that file bigger adding some more declarations to it. You could also use it as a boiler plate if you decide to start a Node.js + View & Data project or simply experiment on TypeScript as I did.</p>

<script src="https://gist.github.com/leefsmp/eff56368c6b246d114fb.js"></script>

I am using WebStorm for my web projects which has some level of <a href="https://www.jetbrains.com/webstorm/help/typescript-support.html">TypeScript support</a>. However I find the file watcher approach not really flexible, without saying that it won't help you if you are not using WebStorm obviously. So I added to the project a gulp file that will automatically handle the transpiling of .ts files. All you have to do to build the project is a "npm install" followed by "gulp" commands. The gulp task also generates the sourcemaps, so it looks better in my WebStorm IDE: it will automatically gather generated files .js and .js.map files under the .ts so it reduces the mess:
<br/>
<br/>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086ff4bc970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb086ff4bc970d image-full img-responsive" alt="Screen Shot 2015-09-10 at 10.58.06 AM" title="Screen Shot 2015-09-10 at 10.58.06 AM" src="/assets/image_0668f4.jpg" border="0" /></a>

After running command <strong><em>npm install</em></strong> for the required node modules, running <em><strong>gulp</strong></em> or <strong><em>gulp ts-build</em></strong> should automatically build the project, ready to run through your IDE or using start server.js from the app directory. Run <strong><em>ts-clean</em></strong> to remove all typescript generated files (.js and .js.map):
<br/>
<br/>
<script src="https://gist.github.com/leefsmp/174a0e2d2baeeb841d2e.js"></script>
