---
layout: "post"
title: "Neuroscience, getting roads from Forma and following them with VASA"
date: "2025-04-09 17:45:49"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "Conferences"
  - "Forma"
  - "Human-centric building design"
  - "VASA"
original_url: "https://www.keanw.com/2025/04/neuroscience-getting-roads-from-forma-and-following-them-with-vasa.html "
typepad_basename: "neuroscience-getting-roads-from-forma-and-following-them-with-vasa"
typepad_status: "Publish"
---

<p>After a relatively quiet winter season - I’ve had a few trips to the <a href="https://www.keanw.com/2025/01/researchx-in-the-uk-and-beyond.html" rel="noopener" target="_blank">UK</a> and <a href="https://www.keanw.com/2025/02/a-day-in-milan-for-the-neuroscience-and-design-course-kick-off.html" rel="noopener" target="_blank">Italy</a>, but nothing too hectic - things are about to get much more busy. Looking at my calendar I see trips to Italy and the UK again, but also trips to Boston, L.A. and SF (thankfully all in one trip) and others to Amsterdam and Toronto. And it’s only going to get busier.</p>
<p>I’m actually really lucky to have been able to attend the <a href="https://www.keanw.com/2025/02/a-course-on-neuroscience-and-design-at-polidesign.html" rel="noopener" target="_blank">Neuroscience and Design class</a> during this period: only one of the 12 sessions looks like it’s going to be affected by travel (I’ll be dialing into one of the classes from Boston). It could have been far worse.</p>
<p>This week we had an exceptional Monday afternoon session due to a holiday in Italy this week, I believe, and then on Tuesday afternoon I headed across to Lavigny to meet with members of the <a href="https://www.chuv.ch/fr/chuv-home" rel="noopener" target="_blank">CHUV</a>'s <a href="https://www.chuv.ch/en/neurosciences/dnc-home/recherche/centre-de-recherche-en-neurosciences/neurotech/research-labs/myspaceneurotech" rel="noopener" target="_blank">MySpace Lab</a> (yes - it’s really called that, which I find very cool) and other visitors from the <a href="https://www.lombardini22.com/neurosciencelab" rel="noopener" target="_blank">Neuroscience Lab at Lombardini22</a> and the University of Parma.</p>
<p>I didn’t take photos during the trip - I tend not to whip my phone out for selfies on the first meeting with people - but I did take a photo of the chapel in the hospital grounds.</p>
<p><a href="/assets/image_376781.jpg" rel="noopener" target="_blank"><img alt="Chapel and red bench." border="0" height="500" src="/assets/image_376781.jpg" style="display: block; margin: 30px auto;" title="Chapel and red bench" width="375" /></a></p>
<p>Many thanks to Andrea, Anna, Giovanni, Giorgia, Federica and Ash for the warm welcome and the stimulating discussion. I was really fascinated to hear about everyone’s work, and look forward to discussing more in the future!</p>
<p>Back to reality, I’ve been preparing for the <a href="https://www.keanw.com/2025/01/register-today-for-the-autodesk-devcon-2025-in-amsterdam.html" rel="noopener" target="_blank">Autodesk DevCon in Amsterdam</a>. I’ll be showing how we’ve been <a href="https://www.keanw.com/2025/02/learn-about-combining-aps-and-webassembly-at-autodesk-devcon-europe.html" rel="noopener" target="_blank">using WebAssembly inside Forma</a> to integrate C++ components into the browser.</p>
<p>I was looking at the pathfinding implementation I’ll be showcasing during one of the demos - and looked at how it would work with a scene brought in for downtown Amsterdam - and I realised that while it worked nicely for Las Vegas (the last place I demoed it), the number of canals in Amsterdam made a naive approach of “just avoiding buildings” to make a lot less sense. I simply cannot recommend swimming across canals in Amsterdam.</p>
<p>I decided to figure out how to voxellise the road network coming from Forma and (optionally) use that as the basis for pathfinding inside Forma.</p>
<p>My friend Håvard Høiby came to the rescue with a snippet of code and an explanation for working with roads. I had hoped to treat them like a mesh - getting the triangles representing them, which I could then throw at VASA for voxellisation - but it turned out to not be quite as simple as that:</p>
<blockquote>
<p>A road does not have a 3d representation in Forma. It has a Footprint which is the horizontal alignment discretized (e.g no real curves), and it has a TerrainShape which is the visual impression on the ground.</p>
</blockquote>
<p>Here’s a simple function I created based on the code Håvard provided:</p>
<p>&nbsp;</p>
<div style="color: #cccccc; background-color: #1f1f1f; font-family: Menlo, Monaco, 'Courier New', monospace; font-weight: normal; font-size: 12px; line-height: 18px; white-space: pre;">
<div><span style="color: #569cd6;">async</span> <span style="color: #569cd6;">function</span> <span style="color: #dcdcaa;">getRoadFootprints</span><span style="color: #cccccc;">() {</span></div>
<div><span style="color: #569cd6;"> const</span> <span style="color: #4fc1ff;">paths</span> <span style="color: #d4d4d4;">=</span> <span style="color: #c586c0;">await</span> <span style="color: #9cdcfe;">Forma</span><span style="color: #cccccc;">.</span><span style="color: #9cdcfe;">geometry</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">getPathsByCategory</span><span style="color: #cccccc;">({ </span><span style="color: #9cdcfe;">category</span><span style="color: #9cdcfe;">:</span> <span style="color: #ce9178;">"road"</span><span style="color: #cccccc;"> });</span></div>
<div><span style="color: #c586c0;"> return</span> <span style="color: #c586c0;">await</span> <span style="color: #4ec9b0;">Promise</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">all</span><span style="color: #cccccc;">(</span><span style="color: #4fc1ff;">paths</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">map</span><span style="color: #cccccc;">((</span><span style="color: #9cdcfe;">path</span><span style="color: #cccccc;">) </span><span style="color: #569cd6;">=&gt;</span> <span style="color: #9cdcfe;">Forma</span><span style="color: #cccccc;">.</span><span style="color: #9cdcfe;">geometry</span><span style="color: #cccccc;">.</span><span style="color: #dcdcaa;">getFootprint</span><span style="color: #cccccc;">({ </span><span style="color: #9cdcfe;">path</span><span style="color: #cccccc;"> }))</span><span style="color: #cccccc;">);</span></div>
<div><span style="color: #cccccc;">}</span></div>
</div>
<p>&nbsp;</p>
<p>This will return the footprint objects for each segment of road. You can loop through them and get the “coordinates” property of each, and use the X and Y values to create your road. I actually added a width to the line - by figuring out the perpendicular vector and using that to create a series of vertices representing a segment’s corners - and then created triangles from there that I could pump into VASA.</p>
<p>Here are some of Amsterdam’s roads, voxelised and displayed in zebra striping:</p>
<p><a href="/assets/image_600490.jpg" rel="noopener" target="_blank"><img alt="The voxelised roads in Forma." border="0" height="375" src="/assets/image_600490.jpg" style="display: block; margin: 30px auto;" title="The voxelised roads in Forma" width="500" /></a></p>
<p>From here it was a simple matter to use this voxel model as a basis for pathfinding.</p>
<p><a href="/assets/image_370633.jpg" rel="noopener" target="_blank"><img alt="Pathfinding with Forma roads." border="0" height="375" src="/assets/image_370633.jpg" style="display: block; margin: 30px auto;" title="Pathfinding with Forma roads" width="500" /></a></p>
<p>I’ll be showing more during <a href="https://events.autodesk.com/flow/autodesk/devcon25emea/mainevent/page/agenda/session/1742203494575001JBb6" rel="noopener" target="_blank">my DevCon session</a> on May 20th at 3:30pm.</p>
<p><a href="/assets/image_912249.jpg" rel="noopener" target="_blank"><img alt="My DevCon session." border="0" height="375" src="/assets/image_912249.jpg" style="display: block; margin: 30px auto;" title="My DevCon session" width="500" /></a></p>
<p>Hopefully see you there!&nbsp;</p>
