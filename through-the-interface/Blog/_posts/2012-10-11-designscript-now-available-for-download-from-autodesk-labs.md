---
layout: "post"
title: "DesignScript now available for download from Autodesk Labs"
date: "2012-10-11 08:53:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "DesignScript"
  - "Geometry"
  - "Graphics system"
original_url: "https://www.keanw.com/2012/10/designscript-now-available-for-download-from-autodesk-labs.html "
typepad_basename: "designscript-now-available-for-download-from-autodesk-labs"
typepad_status: "Publish"
---

<p>I’m really excited about this. A new programming language and environment for AutoCAD is <a href="http://labs.autodesk.com/utilities/designscript" target="_blank">now available for download on Autodesk Labs</a> (and <a href="http://labs.blogs.com/its_alive_in_the_lab/2012/10/designscript-available-for-download-via-autodesk-labs.html" target="_blank">here’s the announcement on Scott’s blog</a>, in case, and you should also be aware of <a href="http://labs.blogs.com/its_alive_in_the_lab/2012/08/clicking-on-download-now-lets-me-login-but-does-not-take-me-to-the-download-page.html" target="_blank">this login/download issue</a> – something I just ran into myself).</p>
<p>Way back when, I helped integrate the initial incarnation of DesignScript – although at the time we were using its working name, D# – inside AutoCAD. The father of the language, Robert Aish, was put in touch with me on October 17th 2008 and by the time I headed for Las Vegas on November 30th we had a working prototype (which was good enough to demonstrate at the Design Computation Symposium and even to be included in a video shown during the AU 2008 mainstage presentation). It was quite an intense period, believe it or not.</p>
<p>Here’s an old screenshot for chuckles:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3ca02743970c-pi" target="_blank"><img alt="D# demo for AU 2008" border="0" height="315" src="/assets/image_428112.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="D# demo for AU 2008" width="284" /></a></p>
<p>It was a really interesting project: working with Robert was a great experience, and we managed to create a demo app that had its claws deeply into AutoCAD in many areas (aside from a nice UI-level integration, we made heavy use of transient graphics for performance purposes, for instance).</p>
<p>A lot has clearly happened since then: professional language designers have become involved in the project, creating a design-centric, multi-paradigm (which in this case means primarily associative and imperative) programming language. A team of coders have created a core implementation that works extensively with AutoCAD geometry (as opposed to the small number of types we delivered with the initial demo).</p>
<p>I’m really looking forward to taking this version of DesignScript for a spin and posting a few samples on this blog. While it’s ultimately a technology that’s targeted at problems that would benefit from algorithmic or computational design – and mostly in the AEC space – I suspect that it could also be used more broadly. We’ll see!</p>
<p>In the meantime, here’s a quick snapshot of DesignScript inside AutoCAD 2013 (you need to be running the 64-bit version to install the Labs release) with the script from “Tutorial 1” loaded and executed:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c3271a072970b-pi" target="_blank"><img alt="The all new DesignScript with the 1st tutorial script loaded and executed" border="0" height="323" src="/assets/image_544409.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="The all new DesignScript with the 1st tutorial script loaded and executed" width="474" /></a></p>
