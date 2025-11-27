---
layout: "post"
title: "Enhancing exploration of sensor data inside Dasher"
date: "2021-02-23 10:20:10"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2021/02/enhancing-exploration-of-sensor-data-inside-dasher.html "
typepad_basename: "enhancing-exploration-of-sensor-data-inside-dasher"
typepad_status: "Publish"
---

<p>I added a “simple” feature to <a href="https://dasher360.com" rel="noopener" target="_blank">Project Dasher</a> (although it’s not live just yet) the other day, that I think is worth sharing some information about – especially as from a UX perspective it’s completely hidden and amounts to an <a href="https://en.wikipedia.org/wiki/Easter_egg_(media)" rel="noopener" target="_blank">Easter egg</a>.</p>
<p>For a particular project I’ve been searching through sensor data for time periods that tell an interesting story. In Dasher the simplest way to explore the data for one or more sensors (as you can add multiple sensors to a single chart by holding down the Shift key when you click on a sensor) is to use our <a href="https://www.autodesk.com/research/publications/dive-in" rel="noopener" target="_blank">Splash</a> technology.</p>
<p>Splash is a visualization component that allows streamlined navigation of sensor data – it uses the “rollup” feature in out time-series back-end to access the appropriate level of detail as you zoom in and out of the data. Much as Google Maps brings in more fine-grained resolutions of imagery as you zoom into a location, Splash streams in data seamlessly as you navigate through data, leading to a highly responsive experience as you go from looking at years’ worth of data all the way down to an individual sensor reading.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdebf684c200c-pi" rel="noopener" target="_blank"><img alt="Splashing around" height="363" src="/assets/image_207903.jpg" style="margin: 30px auto; float: none; display: block;" title="Splashing around" width="500" /></a></p>
<p>Splash is a great tool for exploring sensor data. What was missing was the ability to say “I really like this time period – let’s apply it to the timeline”. In the past we’ve talked about adding some kind of button for this, but just to test out its applicability I added a double-click handler to the footer of each Splash chart. This way whenever you want to apply a particular time period to the timeline, you double-click the footer. This time period then gets propagated to all the other UI features inside Dasher that are driven by the current time period, which is actually really useful.</p>
<p><a href="/assets/image_984038.jpg" rel="noopener" target="_blank"><img alt="Coordinating Splash with the timeline" height="313" src="/assets/image_984038.jpg" style="margin: 30px auto; float: none; display: block;" title="Coordinating Splash with the timeline" width="500" /></a></p>
<p>I’m really happy with the potential displayed by this simple prototype: I’ve already found it really useful during my exploration of sensor data. It surprised me that such a simple enhancement would prove so immediately useful.</p>
<p>I know the UX is somewhat obscure – I should really add something a little more obvious, as no-one is going to discover this capability with the way it works right now… you have to know the feature is there to make use of it – but that seems like a relatively minor detail.</p>
