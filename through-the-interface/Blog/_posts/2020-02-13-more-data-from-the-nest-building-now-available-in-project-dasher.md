---
layout: "post"
title: "More data from the NEST building now available in Project Dasher"
date: "2020-02-13 11:17:25"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2020/02/more-data-from-the-nest-building-now-available-in-project-dasher.html "
typepad_basename: "more-data-from-the-nest-building-now-available-in-project-dasher"
typepad_status: "Publish"
---

<p>I’ve been heads-down this week on a couple of tasks related to Dasher: the first relates to working on the integration of a new timeline control. I’m hoping this will be ready for people to try in the coming weeks, but we’re not quite there yet.</p><p>The second relates to bringing more data into our time-series back-end from the NEST building: I noticed last week that we were missing data from October of last year onwards. It turns out we had a credentials problem – we have a “conduit” that regularly federates the NEST data into our back-end, but it was no longer being given access. Once we fixed the credentials, I decided to pull across not only the last few months of data, but the data going back to the beginning of 2019. Head on over to the “Demo” link on <a href="http://dasher360.com" target="_blank">Dasher 360</a> (which we’re increasingly referring to as Project Dasher, to emphasise the fact it’s ongoing research).</p><p>One basically unknown feature (that has been there for ages, although if you don’t know about it, you won’t think to try it) is that if you hold down the Shift key while clicking on a sensor to bring up its plot (whether via a sensor dot, the sensor list or the dashboard), it will be added to the most recently created plot window. Which means you can have multiple sensors in the same graph:</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a50b9197200b-pi" target="_blank"><img width="500" height="312" title="NEST data in Dasher" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="NEST data in Dasher" src="/assets/image_817582.jpg" border="0"></a></p><p>Bear in mind that Dasher isn’t very picky: you can add sensors with very different types (and data ranges) to the same plot window, without any effort being made to overlay the data in a useful way. It’s really best to add sensors of the same type, where possible, which is made easier by the sensor list’s filter capability.</p><p>Have fun <a href="http://dasher360.com" target="_blank">giving it a try</a>!</p>
