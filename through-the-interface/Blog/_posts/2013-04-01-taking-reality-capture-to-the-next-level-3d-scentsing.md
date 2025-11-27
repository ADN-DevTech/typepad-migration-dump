---
layout: "post"
title: "Taking reality capture to the next level: 3D scentsing"
date: "2013-04-01 08:00:00"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "Autodesk"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2013/04/taking-reality-capture-to-the-next-level-3d-scentsing.html "
typepad_basename: "taking-reality-capture-to-the-next-level-3d-scentsing"
typepad_status: "Publish"
---

<p>I’m excited to announce some revolutionary new technology our &quot;reality capture” team is working on at Autodesk. By now many people will be familiar with the Rip-Mod-Fab workflow (the 3D equivalent of Rip-Mix-Burn for music). The problem with this is that there’s something fundamental missing from the equation: that most aromatic of dimensions, the world of odours.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee9438a98970d-pi" target="_blank"><img alt="Smelling a flower" border="0" height="348" src="/assets/image_828488.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Smelling a flower" width="470" /></a>How many of you have used <a href="http://www.123dapp.com/catch" target="_blank">123D Catch</a> to copy a real-world object, printing it using with <a href="http://www.makerbot.com" target="_blank">MakerBot</a>, only to find something missing? That once you’ve got over the utter coolness of being able to print something in three dimensions, you’re somehow left with a feeling of emptiness? It’s completely natural: while sight and touch are often fooled by current 3D prints, that most powerful of senses, the sense of smell, causes confusion as it – at best – reports nothing or – at worst – tells you the object you’re looking at has been synthesized from thermoplastic.</p>
<p>Autodesk is tackling this challenge head-on. We have a working internal prototype build of <a href="http://autodesk.com/recap" target="_blank">Autodesk ReCap Studio</a> implementing what we’re calling “3D Scentsing”.</p>
<p>To make this work, we’ve had to partner with a number of industry players. To start with, we needed to identify <a href="http://nespal.cpes.peachnet.edu/machine%20olfactory.html" target="_blank">an appropriate “rip” technology</a> to help gather odour input alongside the traditional laser, RGB-D or photogrammetric scan data. We also needed to work with leading 3D printing manufacturers to make sure the “fab” portion of the process is able to deal with odour, providing the ability to imbue resulting prints with complex fragrances. (This has already <a href="http://www.forbes.com/sites/tjmccue/2012/03/28/shoes-from-a-3d-printer-that-smell-like-bubblegum" target="_blank">started to happen</a>.)</p>
<p>Autodesk’s piece of the puzzle, of course, is around the “mod” technology. At a very basic level we needed to introduce the capability for the .RCS and .RCP formats to deal with odour data. “Initially we’ve added a meta-data field at the file-level to store a model’s odour,” says Keshav Sahoo, Director of Engineering for the Reality Capture team at Autodesk. “But we’re very clear that this is just the beginning: as ‘sniff’ technology advances, we’ll inevitably get a much more granular sense of an object’s odour and therefore need to plan for a much higher resolution. Watch – or should that be smell? – this space!”</p>
<p>Luckily the .RCS format has been designed with this in mind: we can store odour information down at the point level, if needed, and have licensed technology that allows us to blend odours from different materials as they get applied to a model. We’ve also introduced the ability to improve the smell of models, allowing you literally to make your models smell of roses (perhaps to reduce the pheromonal impact of scans of human bodies, for example).</p>
<p>Expect to see something on Autodesk Labs during the coming weeks. These are really exciting times for the world of 3D scan-to-print: the future is not only bright – it’s downright pungent!</p>
<p><span style="color: #666666;">photo credit: </span><a href="http://www.flickr.com/photos/stopdown/2429607697/"><span style="color: #666666;">jesse.millan</span></a><span style="color: #666666;"> via </span><a href="http://photopin.com"><span style="color: #666666;">photopin</span></a><span style="color: #666666;"> </span><a href="http://creativecommons.org/licenses/by/2.0/"><span style="color: #666666;">cc</span></a></p>
