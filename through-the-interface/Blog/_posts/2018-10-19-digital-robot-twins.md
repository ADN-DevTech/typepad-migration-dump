---
layout: "post"
title: "Digital robot twins"
date: "2018-10-19 18:49:03"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
  - "Robotics"
original_url: "https://www.keanw.com/2018/10/digital-robot-twins.html "
typepad_basename: "digital-robot-twins"
typepad_status: "Publish"
---

<p>This week I spent quite a bit of time <a href="http://keanw.com/2018/10/forge-devcon-europe.html" target="_blank">talking to people about digital twins that include skeletons and robots</a>. For skeletons I’ve been working off real data from static JSON files – not yet time-series database-resident – but for robots I’ve just been relying on simulated movements. Until today, that is.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad39a3b7a200d-pi" target="_blank"><img width="500" height="245" title="A (partial) digital twin of an industrial robot in Dasher 360" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="A (partial) digital twin of an industrial robot in Dasher 360" src="/assets/image_367641.jpg" border="0"></a></p><p>Josh Cameron, an Autodesk Research colleague in Toronto, sent through a video he took of a robot while streaming its data to our time-series back-end. This helped me interpret the data (reasonably) correctly, at least for a first pass. You’ll notice the virtal robot is a different model from the physical one, but that’s a relatively minor detail, at this stage.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3742f83200c-pi"><img width="500" height="321" title="Synced bots" style="margin: 30px auto; float: none; display: block;" alt="Synced bots" src="/assets/image_803554.jpg"></a></p><p>This is a significant milestone for <a href="http://dasher360.com" target="_blank">Dasher 360</a>: we’re now able to display the current state of a robot in the Forge viewer, matching it with its real-world equivalent at a certain moment in time. There’s still quite a bit of plumbing work needed for this to happen – each joint has a unique sensor in the database, and these need to be matched up – but the basics are working. Progress!</p>
