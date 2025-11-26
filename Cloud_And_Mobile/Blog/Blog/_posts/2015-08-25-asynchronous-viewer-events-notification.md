---
layout: "post"
title: "Asynchronous viewer events notification"
date: "2015-08-25 09:32:41"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/asynchronous-viewer-events-notification.html "
typepad_basename: "asynchronous-viewer-events-notification"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is a little code snippet that you may find helpful. In a recent thread, we were helping a developer who wanted to select a component in the viewer as soon as the model was loaded using <strong><em>viewer.select([nodeId])</em></strong></p>
<p>It appears that you need to wait for two different events if you want to make sure the selection is going to work reliably:&nbsp;</p>
<p><em>Autodesk.Viewing.<strong>GEOMETRY_LOADED_EVENT</strong></em></p>
<p><em>Autodesk.Viewing.<strong>OBJECT_TREE_CREATED_EVENT</strong></em></p>
<p>However there is no guarantee about which event will be fired first upon loading a model. So we could come up with various code constructs that allow to wait for both events but since this is a fairly standard situation in JavaScript asynchronous programming, it's nice to come up with a more generic solution.</p>
<p>I'm using here <a href="https://github.com/caolan/async" target="_self">async</a>, a very handy library that you can install using <em>"bower install async"</em> for example (available also for node <em>npm install async</em>) and which expose many useful methods to wait or delay execution of custom code, see the doc for more details, you'll probably need it at some point if you write enough JavaScript ;)</p>
<p>Here we go, the following sample illustrates how to wait for both viewer events and parse the model structure once they have been fired. As you can tell, it will be very straightforward to add some more events or change them if needed:</p>

<script src="https://gist.github.com/leefsmp/e03ba2a35ef03ecf3dda.js"></script>
