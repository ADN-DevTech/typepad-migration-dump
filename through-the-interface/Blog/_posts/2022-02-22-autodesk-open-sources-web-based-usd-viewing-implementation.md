---
layout: "post"
title: "Autodesk open-sources web-based USD viewing implementation"
date: "2022-02-22 14:46:28"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "Graphics system"
  - "Web/Tech"
original_url: "https://www.keanw.com/2022/02/autodesk-open-sources-web-based-usd-viewing-implementation.html "
typepad_basename: "autodesk-open-sources-web-based-usd-viewing-implementation"
typepad_status: "Publish"
---

<p>I’ve been part of Autodesk’s open source committee – responsible for driving and approving <a href="https://opensource.autodesk.com/" rel="noopener" target="_blank">open source contributions by the company</a> – as the representative for Autodesk Research for the last few years.</p>
<p>One of the most interesting projects to come through the committee – at least from my perspective – has now been released into the wild: Autodesk’s Visualization team (who also develop the Forge viewer) has open-sourced <a href="https://github.com/autodesk-forks/USD/tree/hdJavaScript" rel="noopener" target="_blank">an implementation of the USD toolkit that targets the web</a>.</p>
<p>For those of you unfamiliar with <a href="https://graphics.pixar.com/usd" rel="noopener" target="_blank">USD</a> – and I’m talking about the file format, not the currency – it stands for Universal Scene Description, and is a core part of Pixar’s 3D graphics pipeline that they chose to open-source themselves several years ago.</p>
<p>What’s possibly most interesting about USD is that it’s highly composable: you can build up scenes or assemblies to an arbitrary level from more primitive elements (assets, parts or sub-assemblies). And while its origins are in film-making, the format clearly has huge potential for other industries needing high-fidelity 3D representations. Aside from 3ds Max and Maya, <a href="https://www.autodesk.com/products/fusion-360/blog/how-to-view-your-fusion-360-designs-in-ar-with-usdz-file-format" rel="noopener" target="_blank">Fusion 360 also exports to USD</a>.</p>
<p>The best way to get a sense of Autodesk’s work on this project is to watch <a href="https://drive.google.com/file/d/1MsGcVwVstIWukOporWtyrxuO_oP3I4qN/view" rel="noopener" target="_blank">this video of Kai Schröder presenting the project to the USD working group</a>.</p>
<p>The Visualization team’s efforts in this area revolve around the desire to take the <a href="https://github.com/PixarAnimationStudios/USD" rel="noopener" target="_blank">largely C++-based USD implementation</a> to work well with the web. They considered three main approaches for making this happen:</p>
<ol>
<li>Translate USD to <a href="https://github.com/KhronosGroup/glTF" rel="noopener" target="_blank">glTF</a>, which already has widespread support on the web</li>
<ul>
<li>This would unfortunately mean certain valuable features from USD – such as those relating to collaboration – would be lost</li>
</ul>
<li>Implement a loader in JavaScript</li>
<ul>
<li>Absolutely feasible for static scenes, but hard to maintain over time (and also very tricky when dealing with physics, etc.)</li>
</ul>
<li>Compile the USD toolkit to WebAssembly</li>
<ul>
<li>Initially concerned about the size of the resultant binaries</li>
<li>The biggest blocker was that USD requires 64-bit memory addressing, while browsers currently only support 32-bit addressing for WASM</li>
</ul>
</ol>
<p>In the end, despite the blocker, option 3 proved to be the most attractive to explore more deeply. The team – Roland Ruiters-Christou, Sebastian Dunkel, Aura Munoz, Philipp Frericks, Cedrick Munstermann and Kai – figured out how not only to have USD target a new architecture – using Emscripten to build WebAssembly binaries – but to have USD support 32-bit memory addressing, allowing it to be loaded by browsers once packaged via WebAssembly.</p>
<p>Not only has the team added WebAssembly as a compile target for the USD toolkit, they’ve added a new <a href="https://graphics.pixar.com/usd/release/glossary.html#hydra" rel="noopener" target="_blank">Hydra</a> render delegate called <a href="https://github.com/autodesk-forks/USD/tree/hdJavaScript" rel="noopener" target="_blank">hdJavaScript</a> that targets a THREE.js render delegate, as well as <a href="https://github.com/autodesk-forks/USD/tree/usdjs" rel="noopener" target="_blank">JavaScript bindings for USD</a> that will allow JS devs to read and manipulate USD scenes directly.</p>
<p>The beauty of the third approach is that having the USD scene graph and Hydra’s rendering abstraction available – not just the import of the USD file format itself – opens up the possibility of implementing editing and interactive workflows in the browser with USD at their core. It also opens up the possibility of using native USD extensions for physics, MaterialX etc. using the same codebase and behaviour in the future. This happens to tie in nicely with another open source effort known as <a href="https://github.com/materialx/MaterialX/pull/709">MaterialX Web</a>, where we have extended the existing <a href="https://materialx.github.io/MaterialX" rel="noopener" target="_blank">MaterialX</a> project to work (you guessed it) with the web.</p>
<p>The proposed changes to the USD toolkit are in two draft pull requests – one for <a href="https://github.com/autodesk-forks/USD/pull/1" rel="noopener" target="_blank">the new compilation target</a> and one for <a href="https://github.com/autodesk-forks/USD/pull/2" rel="noopener" target="_blank">the hdJavaScript and JS API work</a> – but only to share the initial code: this is still a partial implementation that the team believes successfully demonstrates the feasibility of supporting USD in the browser, but more work will be needed to a) complete the work and b) maintain it, over time. Putting this out through open source will hopefully allow the community to engage with this important effort.</p>
<p>It was mentioned above that there were some concerns about the eventual size of the WASM binaries: right now they weigh in at 2MB (when gzip-compressed for transmission to clients, and 8.7MB when when uncompressed) without any major effort to optimize for smaller size. This is really encouraging.</p>
<p>Interested in giving it a try? Head on over to <a href="https://autodesk-forks.github.io/USD/" rel="noopener" target="_blank">the demo page</a>, and load the three models shown by Kai during his working group presentation. Here’s a recording of the gearbox demo:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278806cf16d200d-pi" rel="noopener" target="_blank"><img alt="Exploding USD gearbox in the browser" height="641" src="/assets/image_211362.jpg" style="margin: 30px auto; float: none; display: block;" title="Exploding USD gearbox in the browser" width="477" /></a></p>
<p>Ricardo Cabello (aka Mr Doob), the father of THREE.js, has <a href="https://twitter.com/mrdoob/status/1491769632654602240" rel="noopener" target="_blank">expressed his appreciation of the project</a>, which is just fantastic.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202942f9aa7a7200c-pi" rel="noopener" target="_blank"><img alt="Early feedback on the PRs" border="0" height="285" src="/assets/image_684329.jpg" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Early feedback on the PRs" width="500" /></a></p>
<p>It’s really encouraging to see Autodesk accelerating our contributions to open source projects – something I very much hope to see continue. I asked Sebastian Dunkel about his experience with these contributions, and he said:</p>
<blockquote>After being a consumer of open source software for many years, it has been super rewarding to contribute back to the community. With the ports of USD and MaterialX to Web Assembly we opened up these technologies to a new audience and it has been awesome to see it being used in the wild in a matter of days by the community.</blockquote>
<p>Many of you will be asking yourselves what this means for the future. Right now, this effort is very much at an experimental stage: it’s been shared very early – with the hope of getting feedback from the open source community – and it’s not clear how this will influence our own technology strategy moving forward, particular with respect to our web-based visualization offerings such as with the Forge viewer. Time will tell what combination of technology makes most sense to address the needs of our customers, but having this tool in the box is going to be extremely helpful.</p>
<p>There’s a lot of hype regarding USD in the context of the (sigh) Metaverse. Jensen Huang, CEO of Nvidia, recently said in <a href="https://venturebeat.com/2022/02/20/jensen-huang-interview-nvidias-post-arm-strategy-omniverse-and-self-driving-cars/" rel="noopener" target="_blank">an interview</a> that “I’m hoping that everybody will go to USD and then it will become essentially the HTML of the metaverse.”</p>
<p>I’d actually go one step further, and combine this aspiration with Tony Parisi’s <a href="https://medium.com/meta-verses/the-seven-rules-of-the-metaverse-7d4e06fa864c" rel="noopener" target="_blank">fourth and seventh rules of the Metaverse</a>: <strong><em>I’m hoping that everybody will go to USD and then it will become essentially the core of the open, web-based metaverse</em>.</strong></p>
<p><em>Many thanks to Kai Schröder and Sebastian Dunkel for their input to this post, and congratulations to the whole Visualization team for this really important achievement!</em></p>
