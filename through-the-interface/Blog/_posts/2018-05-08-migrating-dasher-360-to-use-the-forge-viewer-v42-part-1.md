---
layout: "post"
title: "Migrating Dasher 360 to use the Forge viewer v4.2 - Part 1"
date: "2018-05-08 23:22:10"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2018/05/migrating-dasher-360-to-use-the-forge-viewer-v42-part-1.html "
typepad_basename: "migrating-dasher-360-to-use-the-forge-viewer-v42-part-1"
typepad_status: "Publish"
---

<p>Over the last few days I’ve been battling with migrating <a href="http://dasher360.com" target="_blank">Dasher 360</a> from using v3.2.1 of the Forge viewer to the latest &amp; greatest at the time of writing, v4.2. Somewhere around the v4.0 release, quite a few changes were integrated into the viewer that relate to UI theming and docking panels. Some of these are really interesting: the UI feels a lot cleaner than it did with v3.x, for instance.</p><p>To give a sense for how some of these changes might break an application, here’s what happened when I simply upgraded the viewer and stylesheet references in Dasher 360:</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20224df3147bf200b-pi" target="_blank"><img width="500" height="336" title="Dasher 360 jumping from v3.2 to v4.2 of the Forge viewer" style="margin: 30px auto; border-image: none; float: none; display: block; background-image: none;" alt="Dasher 360 jumping from v3.2 to v4.2 of the Forge viewer" src="/assets/image_385482.jpg" border="0"></a></p><p>After these few days of work – with a fair amount of CSS hacking – I’ve arrived at something much more presentable:</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20224e0383a8c200d-pi" target="_blank"><img width="500" height="336" title="Starting to shape up" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Starting to shape up" src="/assets/image_789490.jpg" border="0"></a></p><p>During the next post or two I’ll take a look at some of the specific changes I’ve had to make to Dasher 360 for it to work with the latest version of the Forge viewer. It will hopefully give a sense for some of the challenges we’ve faced, but also highlight how being so tightly coupled with the viewer can lead to challenges, down the line (and what you can do to avoid them).</p>
