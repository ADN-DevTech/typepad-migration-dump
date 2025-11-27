---
layout: "post"
title: "Upgrading to Forge viewer v7.0"
date: "2019-07-11 14:04:40"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2019/07/upgrading-to-forge-viewer-v70.html "
typepad_basename: "upgrading-to-forge-viewer-v70"
typepad_status: "Publish"
---

<p>I mentioned <a href="https://www.keanw.com/2019/06/this-years-forge-accelerator-in-barcelona.html" rel="noopener" target="_blank">a few weeks ago</a> that I’d spent some of my time at the Forge Accelerator in Barcelona migrating the <a href="http://dasher360.com" rel="noopener" target="_blank">Dasher 360</a> codebase to work with v7.0 of the Forge viewer. This version of the viewer does have breaking changes – all of which should be <a href="https://forge.autodesk.com/blog/viewer-release-notes-v-70" rel="noopener" target="_blank">documented in Jaime’s blog post</a> – but I thought it worth listing the changes that affected us in a post of my own.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4bbd098200b-pi" rel="noopener" target="_blank"><img alt="Forge viewer update" border="0" height="300" src="/assets/image_6888.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="Forge viewer update" width="500" /></a></p>
<p>The first big change we had to deal with – and this one was pervasive, albeit easy to handle – was the promotion of GuiViewer3D from the Private namespace to be a fully supported member of Autodesk.Viewing. This meant a simple search &amp; replace across the codebase to change Autodesk.Viewing.Private.GuiViewer3D to Autodesk.Viewing.GuiViewer3D. Easy.</p>
<p>The other big “breaking” change was that we needed to modify our onDocumentLoad() handler not to use Autodesk.Viewing.Document.getSubItemsWithProperties() anymore: instead we now use doc.getRoot().search(options), where options are the same we formerly passed through to getSubItemsWithProperties(). Really not too tricky, either.</p>
<p>Beyond that we really didn’t have anything that was required to get v7.0 working: applications that make use of ViewingApplication will have more work to do, but we’d never quite gotten around to using that ourselves.</p>
<p>During the course of the development of this release, the Forge team had requested input on what “impl” APIs should be promoted to be fully supported. I’d provided a list of those we make heavy use of in Dasher 360, and was happy to see a number of them had been added to the supported API. The big ones related to the use of “overlay scenes” to add graphics to Three.js: we do this for sensor dots, surface shading, skeletons, streamlines and robots (there may be other use-cases, too, but these are the ones I remember off the top of my head). The good news on this front is that the supported API is via a new OverlayManager object – accessible via viewer.overlays – that calls through to the former lower-level impl functions. So no actual work is needed unless you want to migrate to have fewer impl dependencies in your code. Which I’m for sure going to do, at some point.</p>
<p>So all in all this wasn’t a scary process for us, at least. I hope you find the same.</p>
<p>I’ve been stuck working offline on the train to Paris for the last few hours, and so made a start on upgrading our TypeScript typings for v7.0. I now have a fairly decent set – for both Viewer3D and GuiViewer3D – so do let me know if you want me to share them. I’ll probably send them across to the Forge team to host and maintain – assuming they don’t have anything up-to-date themselves – but would be happy to share them in the meantime.</p>
