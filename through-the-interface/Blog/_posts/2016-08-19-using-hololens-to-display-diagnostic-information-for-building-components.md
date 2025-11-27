---
layout: "post"
title: "Using HoloLens to display diagnostic information for building components"
date: "2016-08-19 16:17:28"
author: "Kean Walmsley"
categories:
  - "Augmented Reality"
  - "HoloLens"
  - "IoT"
  - "Unity3D"
original_url: "https://www.keanw.com/2016/08/using-hololens-to-display-diagnostic-information-for-building-components.html "
typepad_basename: "using-hololens-to-display-diagnostic-information-for-building-components"
typepad_status: "Publish"
---

<p>I’ve been looking at using HoloLens for more “serious” applications (yes, beyond <a href="http://through-the-interface.typepad.com/through_the_interface/2016/08/making-our-hololens-robot-dance.html" target="_blank">dancing</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2016/08/our-dancing-hololens-robot-now-viewable-in-germany.html" target="_blank">robots</a> :-). One simple prototype I’ve worked on over the last few weeks – once again with the help of Tom Eriksson, who did another stellar job transforming the CAD data into a good-looking Unity model – is to show an electrical transformer and allow the user to pinpoint a technical problem inside it for the purposes of (hopefully predictive) maintenance.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c888d941970b-pi"><img alt="HoloLens transformer" height="280" src="/assets/image_24996.jpg" style="float: none; margin: 30px auto; display: block;" title="HoloLens transformer" width="500" /></a></p>
<p>Here’s a quick demo video, showing a prototype where we open up the electrical transformer and show its insides before scaling and exploding it and then highlighting the “problematic” component.</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube-nocookie.com/embed/NQNEEDnImhw?rel=0&amp;showinfo=0" width="500"></iframe></p>
<p>&#0160;</p>
<p>In theory the problematic component has been identified by some kind of Machine Learning system analysing sensor data coming from the transformer, but this is pure fantasy (or perhaps sci-fi), for now. In time I fully expect this kind of application to be a compelling use for AR technology, especially when viewed in-context by maintenance staff inside the building where the equipment resides.</p>
