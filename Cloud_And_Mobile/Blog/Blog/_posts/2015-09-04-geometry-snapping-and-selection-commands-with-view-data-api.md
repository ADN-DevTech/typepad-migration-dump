---
layout: "post"
title: "Geometry snapping and selection commands with View & Data API"
date: "2015-09-04 07:47:46"
author: "Philippe Leefsma"
categories:
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/geometry-snapping-and-selection-commands-with-view-data-api.html "
typepad_basename: "geometry-snapping-and-selection-commands-with-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Pretty much everything is said in the title, so I'm not going to expand in explanations... I grabbed the snapping tool which has been implemented by our development team to create the measure command (you can find this code under viewer3D.js which is shipped with View &amp; Data API, see <em>Autodesk.Viewing.Extensions.Measure.Snapper) and tweaked it a little adding some custom methods, properties and events in order to turn it into a tool that can be used to snap THREE.js geometry as the user is hovering the mouse over the meshes and fire events.</p>
<p>The extension shows how you can filter weather the snapped geometry is a vertex, and edge or a face and also implement commands that will prompt the user to graphically pick some of that geometry on the model. Quite useful for those who want to leverage View &amp; Data API to implement more complex commands where selection is involved.</p>
<p>Below is the code for the Geometry Selector extension but most of the logic is implemented in the&nbsp;Autodesk.ADN.Viewing.Tool.Snapper.js (attached in the zip):</p>
<p>A live sample can be tested from <a href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.GeometrySelector">here</a>.</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c8f3db970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c8f3db970b image-full img-responsive" alt="Screen Shot 2015-09-04 at 4.17.25 PM" title="Screen Shot 2015-09-04 at 4.17.25 PM" src="/assets/image_f12194.jpg" border="0" /></a><br/>

<script src="https://gist.github.com/leefsmp/8b274a0b5fda41766140.js"></script>

<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb086d433e970d img-responsive"><a href="http://adndevblog.typepad.com/files/autodesk.adn.viewing.extension.geometryselector.zip">Autodesk.ADN.Viewing.Extension.GeometrySelector</a></span>
