---
layout: "post"
title: "Using the Forge Viewer in a React Web Application"
date: "2016-10-14 07:05:06"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "Database"
  - "Forge"
  - "Frontend"
  - "HTML5"
  - "Javascript"
  - "NodeJS"
  - "Philippe Leefsma"
  - "React"
  - "Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/using-the-forge-viewer-in-a-react-web-application.html "
typepad_basename: "using-the-forge-viewer-in-a-react-web-application"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" target="_blank">(@F3lipek)</a></span></p>

<p><img src="/assets/Viewer-v2.10-green.svg" /></p>

<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://facebook.github.io/react" target="_blank">React</a>&nbsp;- arguably the best front-end technology to built medium to large-sized web applications at the moment, says the JavaScript community. Unlike other tech out there from the JavaScript jungle, React is focusing only on the <em>View</em> part of the <em>MVC</em> pattern, bringing&nbsp;more flexibility to the developer choices. Components that you write in React and that you use in your app can be seen as interchangeable building blocks, a bit like what we intend to provide with the Forge Web API's. Well written components should be de-coupled from app main architecture and can be updated, rewritten, or replaced at no or small cost for the developer. In the current JavaScript ecosystem where things are evolving so quickly and new technology show up every other day, I definitely see that coming handy... Who wants to rewrite his&nbsp;entire application as your main&nbsp;framework is bumping major version? Not targeting any specific tech here ;)</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp;&nbsp;&nbsp;&nbsp;I was asked to write a demo web app to illustrate how to integrate seamlessly database information graphically with the Forge Viewer and decided to start on a clean base with a React application. The challenge when using React is to get your app architecture right because you have more choices to do and more moving parts. I knew I wanted to use <a href="https://github.com/reactjs/redux" target="_blank">Redux</a>, to manage my app state,&nbsp;but that still leaves a great deal of choices to be made. After some research I stumbled accross&nbsp;that boiler plate: <a href="https://github.com/davezuko/react-redux-starter-kit" target="_blank">https://github.com/davezuko/react-redux-starter-kit</a>. Over 6000 stars on github and recommended by <a href="https://twitter.com/dan_abramov" target="_blank">Dan Abramov</a>, creator of Redux&nbsp;- you can't go wrong with such references!</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I will post regularly my React tips, tricks and experiments as I go along with that project, but I wanted to start by showing how to use the Forge viewer in a React app. This post does not intent to be a tutorial on how to get started with React, you will find already so much resources on the web for that, so if you&nbsp;are not familiar with the technology, I recommend you spend an hour or two taking a look <a href="https://facebook.github.io/react/docs/getting-started.html" target="_blank">at the basics</a>.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp;&nbsp;&nbsp;&nbsp;The viewer is somehow different than - let's say - a classic web component because you can instantiate <em>viewer3D</em> object &nbsp;only after the containing div has been appended to the DOM, so my code is using a little trick to reference that div from the component, see the&nbsp;<em>render</em> method. The rest of the code is pretty straightforward as far as the Viewer component is concerned. I tried to keep it as a strict minimum but provide methods to initialize the environment and load a document from its URN, so a maximum of control can be delegated to the controlling component while keeping the Viewer component flexible and easily reusable in any other project. But enough talking, here&nbsp;is the code of the Viewer React component in my app, links to the complete code on github and the live demo:</span></p>
<br>

<script src="https://gist.github.com/leefsmp/d6b6d20d07d8c2238f7019ac94fa7208.js"></script>

<p><a href="https://github.com/Autodesk-Forge/forge-rcdb.nodejs" target="_blank">https://github.com/Autodesk-Forge/forge-rcdb.nodejs</a></p>
<p><a href="https://forge-rcdb.autodesk.io" target="_blank">https://forge-rcdb.autodesk.io</a></p>
<p>Here is a screenshot of the app featuring the <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/04/markup3d-sample-for-view-data-api.html" target="_blank">Markup3D</a> extension:</p>


<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09430c45970d-pi"><img style="border: 1px solid #f8cf40;border-radius:4px;" class="asset  asset-image at-xid-6a0167607c2431970b01bb09430c45970d image-full img-responsive" alt="Forge-rcdb" title="Forge-rcdb" src="/assets/image_194d5b.jpg" border="0" /></a><br />
