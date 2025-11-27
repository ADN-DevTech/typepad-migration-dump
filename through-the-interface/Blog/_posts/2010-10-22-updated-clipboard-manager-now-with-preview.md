---
layout: "post"
title: "Updated Clipboard Manager &ndash; now with preview!"
date: "2010-10-22 15:06:54"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Plugin of the Month"
  - "User interface"
original_url: "https://www.keanw.com/2010/10/updated-clipboard-manager-now-with-preview.html "
typepad_basename: "updated-clipboard-manager-now-with-preview"
typepad_status: "Publish"
---

<p>As mentioned <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/previewing-the-contents-of-the-clipboard-in-an-autocad-palette-using-net.html" target="_blank">yesterday</a>, I went ahead and integrated previewing into the <a href="http://through-the-interface.typepad.com/through_the_interface/2009/09/clipboard-manager-octobers-adn-plugin-of-the-month-now-live-on-autodesk-labs.html" target="_blank">Clipboard Manager</a> <a href="http://labs.autodesk.com/utilities/ADN_Plugins" target="_blank">Plugin of the Month</a>. I won’t include the code directly in the post – it’s fundamentally similar in nature to that posted yesterday and is, in any case, included in the project. There were a few additional tricks needed, such as modifying the SplitterDistance property of a SplitContainer to make sure the aspect ratio of the image gets maintained, but nothing particularly earth-shattering.</p>  <p><a href="http://through-the-interface.typepad.com/files/ClipboardManager-1.0.4.zip" target="_blank">Here’s the updated project</a> – I’ll be looking into the “save between session” feature (which I suspect is going to morph into a “save to DWG file” feature, as that feels like the simplest and safest to implement) before posting a new version on <a href="http://labs.autodesk.com" target="_blank">Autodesk Labs</a>.</p>  <p>Here’s this version in action:</p>  <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f5434ce6970b-pi"><img style="border-right-width: 0px; margin: 20px auto; display: block; float: none; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px" title="Clipboard Manager with preview" border="0" alt="Clipboard Manager with preview" src="/assets/image_141066.jpg" width="474" height="449" /></a>I’m pleased with how the preview adjusts in size according to the image contained by the selected item. I’d be happy to hear from people who’ve given it a try and formed an opinion, one way or another. :-)</p>
