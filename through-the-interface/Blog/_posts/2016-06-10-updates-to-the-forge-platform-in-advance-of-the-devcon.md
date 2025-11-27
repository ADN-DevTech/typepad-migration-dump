---
layout: "post"
title: "Updates to the Forge platform in advance of the DevCon"
date: "2016-06-10 14:37:50"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "PaaS"
original_url: "https://www.keanw.com/2016/06/updates-to-the-forge-platform-in-advance-of-the-devcon.html "
typepad_basename: "updates-to-the-forge-platform-in-advance-of-the-devcon"
typepad_status: "Publish"
---

<p>&#0160;</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c86ba293970b-pi" rel="noopener" target="_blank"><img alt="The Future of Making Things - Design Make Use" height="125" src="/assets/image_151008.jpg" style="float: none; margin: 0px auto 30px; display: block;" title="The Future of Making Things - Design Make Use" width="500" /></a></p>
<p>In anticipation of next week’s first <a href="http://forge.autodesk.com/conference" rel="noopener" target="_blank">Forge DevCon</a>, the Forge platform has just been updated. Here’s a quick look at what’s new and changed:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1f5797e970c-pi"><img alt="Forge Platform APIs" border="0" height="269" src="/assets/image_438779.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="Forge Platform APIs" width="500" /></a></p>
<p>You’ll notice there’s a new <a href="https://developer.autodesk.com/en/docs/data/v2/overview/" rel="noopener" target="_blank">Data Management API</a>, which allows you to access data stored in A360, Fusion 360 and Autodesk’s Object Storage Service. This is going to make it much easier to keep things simple and consistent when developing applications requiring access to design data.</p>
<p>The <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/" rel="noopener" target="_blank">Model Derivative API</a> is another new API that does a few different things. Firstly it allows you to translate models into SVF – which is consumed by the <a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/" rel="noopener" target="_blank">Forge Viewer</a> – as well as a few other “published” formats such as OBJ and STL. It also provides access to the data in the translated models – it’s the “Data” that was formerly part of the “View &amp; Data API”.</p>
<p>On the changed side of things, the viewing component from the View &amp; Data API is now simply called the Forge Viewer (or just the Viewer). As it’s really a client-side JavaScript component, it’s no longer referred to as an API, as such. Which is more accurate.</p>
<p>AutoCAD I/O is now known as the <a href="https://developer.autodesk.com/en/docs/design-automation/v2/overview/" rel="noopener" target="_blank">Design Automation API</a>. I believe this is indicative of a desire to have other “I/O” products supported through Forge: in time it’ll be possible to run scripts against other headless design products, not just AutoCAD. Re-branding the API to be more a generic umbrella term is one step along that path.</p>
<p>You’ll hear a lot more about these technologies at (if you’re coming) or after (if you’re not) next week’s <a href="http://forge.autodesk.com/conference" rel="noopener" target="_blank">Forge DevCon</a> in San Francisco. I’m heading off to Zurich in a few hours to play in Autodesk’s annual football (soccer) tournament, after which – on Sunday – I’ll be flying straight out from there to SF.</p>
