---
layout: "post"
title: "Event Watcher Extension for View & Data"
date: "2015-10-12 14:07:57"
author: "Philippe Leefsma"
categories:
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/10/event-watcher-extension-for-view-data.html "
typepad_basename: "event-watcher-extension-for-view-data"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>A quick post about events in the viewer. It's pretty straightforward to listen to events, all you need is knowing the eventId you are looking for. For example the following code illustrates how to set and remove an event listener for the selection changed:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0880ae6f970d-pi"><img 
style="height:250px;" class="asset  asset-image at-xid-6a0167607c2431970b01bb0880ae6f970d image-full img-responsive" title="Screen Shot 2015-10-12 at 13.03.17" src="/assets/image_4cda78.jpg" alt="Screen Shot 2015-10-12 at 13.03.17" border="0" /></a></p>
<p>The problem is that unfortunately the documentation of the JavaScript API doesn't expose a list of all available events you can subscribe to. No worries, your humble servant is here to the rescue and he's been skimming the <em>viewer3d.js</em> file to grab the list of all events for you:</p>
<p><em>Autodesk</em>.Viewing.<strong>ANIMATION_READY_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>CAMERA_CHANGE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>CUTPLANES_CHANGE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>ESCAPE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>EXPLODE_CHANGE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>FULLSCREEN_MODE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>GEOMETRY_LOADED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>HIDE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>HIGHLIGHT_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>ISOLATE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>LAYER_VISIBILITY_CHANGED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>MODEL_ROOT_LOADED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>NAVIGATION_MODE_CHANGED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>OBJECT_TREE_CREATED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>OBJECT_TREE_UNAVAILABLE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>PROGRESS_UPDATE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>RENDER_OPTION_CHANGED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>RESET_EVENT<br /> </strong><em>Autodesk</em>.Viewing.<strong>SELECTION_CHANGED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>SHOW_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>TOOLBAR_CREATED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>TOOL_CHANGE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>VIEWER_RESIZE_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>VIEWER_STATE_RESTORED_EVENT</strong><br /> <em>Autodesk</em>.Viewing.<strong>VIEWER_UNINITIALIZED<br /> </strong></p>
<p>I also went a bit further and created an extension which allows to set and remove events dynamically while outputting arguments in a logger window.</p>
<p>I took the opportunity to try a library called <a href="https://github.com/yesmeck/jquery-jsonview" target="_self">jquery-jsonview</a>, pretty slick to use and handy it lets you dump a json object into a div and nicely formats it.</p>
<p>One limitation I had when stringifying the json to display it in the jsonview is that some of the events are circular structure, hence throwing exception when passed to the built-in <em>JSON.stringify</em> method. No worries once again because no matter what issue you have, there has to be a lib that can help: ended up using <a href="https://github.com/WebReflection/circular-json" target="_self">circular-json</a> which does as well a pretty good job at handling circular json objects.</p>
<p>Here is the complete code the for the extension and a live version that can be tried from <a href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&amp;extIds=Autodesk.ADN.Viewing.Extension.EventWatcher" target="_self">here</a>. Some of the events cannot be tested from the EventWatcher due to timing or other constraints, for example <em>TOOLBAR_CREATED</em> or <em>GEOMETRY_LOADED</em> will be fired before the event extension is even loaded.</p>
<p><a class="asset-img-link" style="display: inline; height:300px;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0880af5a970d-pi"><img style="height:550px; margin:auto;" class="asset  asset-image at-xid-6a0167607c2431970b01bb0880af5a970d image-full img-responsive" title="Screen Shot 2015-10-11 at 20.44.15" src="/assets/image_ab149f.jpg" alt="Screen Shot 2015-10-11 at 20.44.15" border="0" /></a></p>
<p>&nbsp;</p>
<script src="https://gist.github.com/leefsmp/df4ada295d51f0c1973e.js"></script>
