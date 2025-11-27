---
layout: "post"
title: "HoloLens is here!"
date: "2016-07-01 15:48:50"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Augmented Reality"
  - "Autodesk"
  - "Conferences"
  - "HoloLens"
original_url: "https://www.keanw.com/2016/07/hololens-is-here.html "
typepad_basename: "hololens-is-here"
typepad_status: "Publish"
---

<p>I’m happy to report that a <a href="https://www.microsoft.com/microsoft-hololens/en-us" target="_blank">HoloLens</a> device has arrived in the Neuchâtel office. I’m meant to be on holiday, next week, but as the kids are signed up for various fun activities of their own I foresee some amount of fooling around with HoloLens in my immediate future. :-)</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09197c47970d-pi" target="_blank"><img alt="HoloKean" border="0" height="285" src="/assets/image_478618.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border: 0px;" title="HoloKean" width="504" /></a></p>
<p>I was pleased to see a few HoloLens devices floating around at the recent <a href="https://forge.autodesk.com/devcon-2016" target="_blank">Forge DevCon</a>. In some cases it was inevitable – in that one of our speakers, <a href="https://twitter.com/donasarkar" target="_blank">Dona Sarkar</a>, comes from the HoloLens team and also participated in the <a href="http://www.meetup.com/virtualreality/events/231606877/" target="_blank">SFVR Meetup at the DevCon</a> – but a nice surprise was that my friends at <a href="http://www.hsbcad.be" target="_blank">hsbcad</a> had one on display in the exhibition area.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c8760bc6970b-pi" target="_blank"><img alt="Kris Riemslagh from hsbCad wowing a DevCon attendee" border="0" height="504" src="/assets/image_386947.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="Kris Riemslagh from hsbCad wowing a DevCon attendee" width="339" /></a></p>
<p>Somewhat frustratingly, I didn’t find time to try the demo myself – the conference was just too hectic, I had too much going on – but I did reach out to Kris Riemslagh at hsbcad to understand a bit more about what they’d done. And see whether I could give it a try, now that I have access to a device.</p>
<p>Right now the application is still a prototype – although the plan is to make it available to users of the hsbshare service, in due course – so it’s not practical to get it working here, just yet. But there is a <a href="https://vimeo.com/169560210" target="_blank">nice demo video</a> that shows it in action:</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><iframe allowfullscreen="" frameborder="0" height="281" mozallowfullscreen="" src="https://player.vimeo.com/video/169560210?title=0&amp;byline=0&amp;portrait=0" webkitallowfullscreen="" width="500"></iframe></p>
<p style="text-align: center">&#0160;</p>
<p>Kris gave some additional insight into what they’ve done. Interestingly they’ve used the Forge platform – specifically the translation service that is now part of the <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/" target="_blank">Model Derivative API</a> and generates geometry for the <a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/" target="_blank">Viewer</a> – to create the 3D geometry that gets displayed by the HoloLens application.</p>
<p>In Kris’ own words (with a few minor edits from my side to clarify terminology):</p>
<blockquote>
<p><em>We added a method in our hsbshare to grab the geometry + materials that are present in the Viewer, and create a file on the node server from it. The file contains whatever is turned on in the Viewer model, and manipulated in the model. So we do not use the URN, but rather the manipulations to the geometry that were done in the viewer.</em></p>
<p><em>On Hololens we run an application developed in Unity. In this app we download the file from our server, scale and move the objects onto our virtual “table”, the white rectangular shape in the video.</em></p>
<p><em>We added a number of voice commands for “scale up”, “scale down”, “reload model”… </em></p>
<p><em>As in the Microsoft 101 or 102 demo app, the table can be placed on top of anything in the real world.</em></p>
<p><em>We added a number of tiles (currently primitively white). When you airtap on them some other commands kick in: start stop and reset exploding and also add and remove physics, which makes all the pieces drop onto the floor (as a gag).</em></p>
</blockquote>
<p>I was curious about whether the app could work with additional models, beyond the truck and house used in the demo. Apparently it can work today with any model that’s been translated for viewing via Forge, which is extremely interesting. For instance:</p>
<blockquote>
<p><em>The truck with panels is an agglomeration of 2 models (URNs): the truck + a small house. The panels of the house are then moved inside the Viewer to stack on the truck. The video shows a small animation to restore them to their original position. But the model sent to HoloLens is the one with the panels on the truck.</em></p>
</blockquote>
<p>I can see that Kris and team have put in quite a bit of thought and effort into this prototype. I’m looking forward to trying it firsthand, at some point. I’m also looking forward to sharing my own experiments with the HoloLens platform, once I’ve managed to do something myself that’s worth sharing.</p>
