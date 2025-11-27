---
layout: "post"
title: "Surface shading in Dasher 360 based on real sensor data"
date: "2017-03-30 18:54:16"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2017/03/surface-shading-in-dasher-360-based-on-real-sensor-data.html "
typepad_basename: "surface-shading-in-dasher-360-based-on-real-sensor-data"
typepad_status: "Publish"
---

<p>I mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2017/03/march-update-of-dasher-360.html" target="_blank">last week’s post</a> describing the March update to <a href="http://dasher360.com" target="_blank">Dasher 360</a> that we had temporarily removed the fake surface shading animation – something I talked about <a href="http://through-the-interface.typepad.com/through_the_interface/2016/12/dasher-360-give-it-a-try.html" target="_blank">way back when</a> – from the public site in order to focus on something based on actual sensor data.</p> <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d270f9b0970c-pi" target="_blank"><img title="Humidity data in Dasher 360" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-top-width: 0px" border="0" alt="Humidity data in Dasher 360" src="/assets/image_953642.jpg" width="500" height="348"></a></p> <p>Well, we’ve made quicker progress than expected – largely thanks to the genius of my colleague Simon Breslav, who’s a complete GLSL wizard – allowing me to give you all a sneak peek of what’s about to go into the public demo:</p> <p align="center">&nbsp;</p> <p align="center"><iframe height="281" src="https://www.youtube-nocookie.com/embed/BriKHcD455o?rel=0&amp;showinfo=0?ecver=1" frameborder="0" width="500" allowfullscreen></iframe></p> <p>&nbsp;</p> <p>As you can see we have an adjustable range on the timeline, along with animation – with differently (and, for now, arbitrarily) coloured gradient scales for the various sensor types. I extended the code generating the legend to work with different sensor types and units, allowing custom colours for each which then get passed onto the shader (rather than having these defined in multiple places).</p> <p>This is big (and exciting) progress. Hopefully you’ll be able to give it a try yourselve on the live site in the next week or so!</p>
