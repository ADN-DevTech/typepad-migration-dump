---
layout: "post"
title: "Building self-contained UI components for the viewer"
date: "2015-05-11 06:24:52"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/building-self-contained-ui-components-for-the-viewer.html "
typepad_basename: "building-self-contained-ui-components-for-the-viewer"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Today's post focuses on writing self-contained, portable UI components for the viewer. By <em>"portable"</em> I mean you could easily take a component from one project and use it in another one without having to change any of its code.</p>
<p>The idea is to write your custom feature as a <a href="http://adndevblog.typepad.com/cloud_and_mobile/2014/10/how-to-write-custom-extensions-for-the-large-model-viewer.html" target="_self">viewer extension</a> with business logic and UI all included in the same JavaScript file. Although mixing JavaScript, html and css is usually not a recommended practice, I feel that in this specific case it makes sense. &nbsp;</p>
<p>Let say you need a dialog to manage items in your viewer application: could be viewer states, extensions you want to load/unload from the UI, records from a side database and so on... The viewer API propose an handy object to create custom panels with the same look and feel than the ones from GuiViewer3D, you just need to extend your custom object from the base class&nbsp;<em>Autodesk.Viewing.UI.DockingPanel</em>. The panel can then be populated by DOM elements you create dynamically from the js code.</p>
<p>In the gist below you can see a complete example of a simple UI component: it works with Viewer3D or GUIViewer3D and will add a custom toolbar with a button, pressing the button will display a panel where you can add and remove clickable items. In a real-life scenario, each item could be linked to a web-service call or a database record.</p>
<p>The only dependencies, other than the viewer API, are to jquery and bootstrap css (included by the script).</p>
<p>Here is a picture of the result:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d111ec47970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d111ec47970c image-full img-responsive" title="UI Component" src="/assets/image_1aac22.jpg" alt="UI Component" border="0" /></a></p>
<p>You can test a live version of that component <a href="http://viewer.autodesk.io/node/gallery/embed?id=5538047f1cbfdc380ed888fa&amp;extIds=Autodesk.ADN.Viewing.Extension.UIComponent" target="_blank">here</a>, click the "Show panel" button to make it visible.</p>

<script src="https://gist.github.com/leefsmp/50db10126b607bbc7540.js"></script>
