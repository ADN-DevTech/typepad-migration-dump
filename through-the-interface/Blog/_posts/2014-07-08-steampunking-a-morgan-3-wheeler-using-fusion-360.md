---
layout: "post"
title: "Steampunking a Morgan 3 Wheeler using Fusion 360"
date: "2014-07-08 18:47:51"
author: "Kean Walmsley"
categories:
  - "Graphics system"
  - "HTML"
  - "Morgan"
  - "PaaS"
  - "SaaS"
  - "Solid modeling"
  - "Web/Tech"
original_url: "https://www.keanw.com/2014/07/steampunking-a-morgan-3-wheeler-using-fusion-360.html "
typepad_basename: "steampunking-a-morgan-3-wheeler-using-fusion-360"
typepad_status: "Publish"
---

<p>My friends in the Autodesk Developer Network team asked me to get involved with creating a sample for the API we’re planning to launch soon for <a href="http://through-the-interface.typepad.com/through_the_interface/2014/05/a-sneak-peek-at-the-new-autodesk-360-viewer.html" target="_blank">the new Autodesk 360 viewer</a>. They were quite specific about the requirements, which was very helpful: something fun, perhaps with a <a href="http://en.wikipedia.org/wiki/Steampunk" target="_blank">steampunk</a> theme, that shows some interesting possibilities around both the HTML5 container and the embedded viewer. I was also suggested the Morgan 3 Wheeler as a possible model to look into hosting, so I really didn’t need to be asked twice. ;-)</p>
<p>I started by tracking down a model: I ended up using the Inventor files <a href="https://www.talenthouse.com/i/design-visualization-for-autodesk-morgan-motor-company-hp-nvidia/2" target="_blank">posted for the Morgan advertisement competition</a> towards the end of last year. The posted archive actually has the originating Alias model inside the ZIP, it turns out, so I went ahead and imported that into Fusion 360.</p>
<p>The model is <strong><em>huge</em></strong>, so this has been a real test of the technology, and Fusion 360 has mostly been up to the challenge. I’m new to Fusion, but the experience has been positive – I’ve had to learn quite a bit as I’ve gone along, but the UI has been intuitive enough. Although I’m really only applying materials to geometry, so nothing very challenging from a hardcore modelling perspective.</p>
<p>Here are a few shots of the model. I really need a “grimy” visual style to go for the full-on steampunk feel, but overall I like the results, so far.</p>
<p>Let’s start with a view of the front:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a73de8aa4d970d-pi" target="_blank"><img alt="A view from the front in Fusion 360" border="0" height="250" src="/assets/image_792085.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="A view from the front in Fusion 360" width="394" /></a></p>
<p>Here’s an animation of a few images taken at different stages of applying a material to a set of components. Yes, I know that copper is a really poor choice for a suspension spring – just as brass is less than ideal for an exhaust pipe – but this is all about the look, not at all about the eventual presumed performance. :-)</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a511dd5d1a970c-pi" target="_blank"><img alt="Making a suspension spring copper using Fusion 360" height="247" src="/assets/image_469710.jpg" style="float: none; margin-left: auto; display: block; margin-right: auto;" title="Making a suspension spring copper using Fusion 360" width="390" /></a></p>
<p>And here’s an overall view of the car in Fusion 360…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a3fd2db0eb970b-pi" target="_blank"><img alt="The overall Morgan 3 Wheeler" border="0" height="250" src="/assets/image_102794.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="The overall Morgan 3 Wheeler" width="394" /></a></p>
<p>… as well as one in the Autodesk 360 viewer, although not all the materials appear with full fidelity (and the generic material currently comes through as red, whereas I haven’t started working on the interior, as yet):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a73de8aa69970d-pi" target="_blank"><img alt="Our Fusion 360 model in the Autodesk 360 viewer" border="0" height="292" src="/assets/image_370257.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="Our Fusion 360 model in the Autodesk 360 viewer" width="450" /></a></p>
<p>Oh, and the RaaS (Rendering as a Service images) look really beautiful, in case you’re wondering about that:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a3fd2db0f9970b-pi" target="_blank"><img alt="RaaS images of the steampunked Morgan model" border="0" height="350" src="/assets/image_314234.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="RaaS images of the steampunked Morgan model" width="450" /></a></p>
<p>Right now I need to work on uploading the model to data service and embedding the viewer into a fun steampunk-themed HTML page I found on the web… I’ll post more about that, in due course.</p>
