---
layout: "post"
title: "Another point cloud technology on Autodesk Labs: Shape Extraction for AutoCAD 2011"
date: "2010-07-30 17:10:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2010/07/another-point-cloud-technology-on-autodesk-labs-shape-extraction-for-autocad-2011.html "
typepad_basename: "another-point-cloud-technology-on-autodesk-labs-shape-extraction-for-autocad-2011"
typepad_status: "Publish"
---

<p>I’ve been looking at (<a href="http://through-the-interface.typepad.com/through_the_interface/2010/07/adn-devcast-episode-4-using-photofly-and-photosynth-with-autocad-2011.html">and talking about</a>) point clouds a lot, of late, especially those generated from sets of 2D photos. <a href="http://labs.blogs.com/its_alive_in_the_lab/2010/07/shape-extraction-for-autocad-now-available.html">Today’s announcement by Scott Sheppard</a> refers to a new technology on <a href="http://labs.autodesk.com">Autodesk Labs</a> that makes it even easier to work with and model using point clouds, <a href="http://labs.autodesk.com/utilities/shape_extraction_autocad">Shape Extraction for AutoCAD 2011</a>. I spent some time evaluating this technology, to see how it works with point clouds extracted from Photosynth, to make sure it proved useful. I used Photosynth because of the wealth of community content browsable online: Photofly content is not shared in the same way, but I would tend towards using this technology if wanting to generate point clouds from my own sets of photos.</p>
<p>A quick side note… this utility is currently only available to Autodesk Labs users in the following countries: Australia, Canada, Ireland, New Zealand, Singapore, United Kingdom, and United States. If you live elsewhere (like me, although I have special privileges in this situation) then please be patient. We hope to make it available more widely soon.</p>
<p>I started by installing the tool. To bring up the UI inside AutoCAD I had to load the partial CUIx file installed into AutoCAD’s main program folder (pclab.cuix) using CUILOAD:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f2b88f9a970b-pi"><img alt="Shape extraction CUIx file" border="0" height="319" src="/assets/image_905792.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Shape extraction CUIx file" width="400" /></a></p>
<p>At which point we should have the utility’s ribbon UI available:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f2b96484970b-pi"><img alt="Shape Extraction ribbon" border="0" height="136" src="/assets/image_7946.jpg" style="margin: 20px auto; display: block; float: none; border: 0px;" title="Shape Extraction ribbon" width="475" /></a> Even without the extraction and sectioning tools, just the cropping capability makes this tool worth the effort of installing: point clouds generated from images are often quite noisy and typically don’t go through the same pre-processing via vendor-supplied tools as 3D-scanned point clouds.</p>
<p>But there is clearly some potential when it comes to extracting shapes, too…</p>
<p>Here’s what I found when running the tools against some point clouds brought down from Photosynth:</p>
<p><strong>1. A castle in Azerbaijan called Девичья башня.</strong></p>
<p>The Synth:</p>
<p><iframe frameborder="0" height="280" src="http://photosynth.net/embed.aspx?cid=8919b044-95c5-4bce-8147-2149d1d5342e&amp;delayLoad=true&amp;slideShowPlaying=false" width="475"></iframe></p>
<p>The raw point cloud in AutoCAD 2011:</p>
<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2013485dc799d970c-pi"><img alt="Castle point cloud in AutoCAD 2011" border="0" height="285" src="/assets/image_439256.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Castle point cloud in AutoCAD 2011" width="475" /></a>
<p>A cylindrical tower extracted from the point cloud:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2013485dc80ec970c-pi"><img alt="Castle point cloud with cylindrical tower extracted in AutoCAD 2011" border="0" height="276" src="/assets/image_754143.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Castle point cloud with cylindrical tower extracted in AutoCAD 2011" width="475" /></a></p>
<p><strong>2. A flood column</strong></p>
<p>The Synth:</p>
<p><iframe frameborder="0" height="280" src="http://photosynth.net/embed.aspx?cid=c1ef40c4-f2b1-44c7-ace0-a19fe8563818&amp;delayLoad=true&amp;slideShowPlaying=false" width="480"></iframe></p>
<p>The raw point cloud in AutoCAD 2011:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f2b8a1b2970b-pi"><img alt="Flood column point cloud in AutoCAD 2011" border="0" height="294" src="/assets/image_3602.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Flood column point cloud in AutoCAD 2011" width="475" /></a></p>
<p>A cylindrical column extracted from the point cloud:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2013485dc8c73970c-pi"><img alt="Flood column point cloud with cylindrical column extracted in AutoCAD 2011" border="0" height="290" src="/assets/image_573663.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="Flood column point cloud with cylindrical column extracted in AutoCAD 2011" width="475" /></a></p>
<p>It remains to be seen how useful this capability proves to be in practice, but I was pleased that the examples I tried at least appeared to work well. For those of you who are interested in this utility and are located in a geographical region covered by the trial, please do give the utility a try, and be sure to <a href="mailto:labs.acad.shape@autodesk.com">send us feedback</a>.</p>
