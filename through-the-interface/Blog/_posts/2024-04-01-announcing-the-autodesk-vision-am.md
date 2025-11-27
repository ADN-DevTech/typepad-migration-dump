---
layout: "post"
title: "Announcing the Autodesk Vision Am"
date: "2024-04-01 00:00:00"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Autodesk Research"
  - "Human-centric building design"
original_url: "https://www.keanw.com/2024/03/announcing-the-autodesk-vision-am.html "
typepad_basename: "announcing-the-autodesk-vision-am"
typepad_status: "Publish"
---

<p>I’m excited to be pre-announcing a new experience capture platform from Autodesk Research: the Autodesk Vision Am.</p>
<p><a href="/assets/image_297874.jpg" rel="noopener" target="_blank"><img alt="Autodesk Vision A m." border="0" height="500" src="/assets/image_297874.jpg" style="display: block; margin: 30px auto;" title="Autodesk Vision Am" width="375" /></a></p>
<p>It’s the data capture system we used for our project with The Bentway and showcased at our <a href="https://www.keanw.com/2023/10/more-information-on-from-steps-to-stories.html" rel="noopener" target="_blank">From Steps to Stories exhibit</a> both <a href="https://thebentway.ca/event/from-steps-to-stories/" rel="noopener" target="_blank">in Toronto</a> and <a href="https://www.keanw.com/2023/11/au-2023-day-1.html" rel="noopener" target="_blank">at AU</a> 2023 in Las Vegas. The goal is to <a href="https://www.keanw.com/2023/11/capturing-the-human-experience-of-the-built-environment.html" rel="noopener" target="_blank">collect as much data as we can about the human experience of the built environment</a>.</p>
<p><a href="/assets/image_737796.jpg" rel="noopener" target="_blank"><img alt="Capturing data at The Bentway." border="0" src="/assets/image_737796.jpg" style="display: block; margin: 30px auto;" title="Capturing data at The Bentway" width="500" /></a></p>
<p><a href="/assets/image_736671.jpg" rel="noopener" target="_blank"><img alt="Vision Ams at AU 2023." border="0" height="375" src="/assets/image_736671.jpg" style="display: block; margin: 30px auto;" title="Vision Ams at AU 2023" width="500" /></a></p>
<p><a href="/assets/image_192276.jpg" rel="noopener" target="_blank"><img alt="Wearing a Vision Am at AU 2023." border="0" height="500" src="/assets/image_192276.jpg" style="display: block; margin: 30px auto;" title="Wearing a Vision Am at AU 2023" width="375" /></a></p>
<p>You can think of it as a purposefully basic, <strong>Am</strong>ateurish alternative to Apple’s Vision Pro, which we hope you’ll just want to put on first thing in the <strong>Am</strong> (i.e. the morning).</p>
<p>Here’s a breakdown of the hardware used in the Vision Am system:</p>
<p><a href="/assets/image_213896.jpg" rel="noopener" target="_blank"><img alt="Vision Am hardware components." border="0" src="/assets/image_213896.jpg" style="display: block; margin: 30px auto;" title="Vision Am hardware components" width="500" /></a></p>
<p>Why would you want to wear the Vision Am? That’s actually a much tougher sell, in all honesty. While the Vision Pro shows you pretty pictures in an immersive context, the Vision Am just captures everything it can about what you’re experiencing and sends it to our servers to be used for anything we want to. So basically it’s like the Vision Pro but without the pretty pictures.</p>
<p>Here’s the software architecture for everything running locally on the Vision Am system, between the Raspberry Pi and the phones that are used both by the participants and the local admin. The data is stored locally until there’s sufficient connectivity for it to be uploaded to our cloud-hosted back-end.</p>
<p><a href="/assets/image_197229.jpg" rel="noopener" target="_blank"><img alt="Vision Am software architecture." border="0" src="/assets/image_197229.jpg" style="display: block; margin: 30px auto;" title="Vision Am software architecture" width="500" /></a></p>
<p>There’s something refreshingly honest about this technology offering: a lot of companies are collecting data about people while making them feel like they’re getting sufficient value from other features being offered - playing games, cataloging photos, etc. etc. - but the Vision Am does <strong>nothing but collect data about you</strong>. It’s completely unpretentious in that respect.</p>
<p>I expect you’re asking yourself how you can get hold of a Vision Am for yourself? It’s not a product, per se, rather than a set of instructions on how to build your own. This is a new area for Autodesk, but hopefully this DIY approach will appeal to a certain subset of our customers.</p>
<p>Watch this space for more details on how to procure and build your Vision Am. It’s most likely to be in the form of an Amazon shopping list (the overall cost will be around $1000, with the most costly component being the GoPro MAX) and a small set of 3D-printable parts. You’ll then be able to install the open source software suite on your Raspberry Pi, connect to it via a browser on your mobile device, and then get started collecting (and - above all - sending us) your data. Sounds like fun, right?</p>
