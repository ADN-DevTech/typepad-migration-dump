---
layout: "post"
title: "Update to Thumbnail Parts List"
date: "2010-02-26 00:08:32"
author: "Adam Nagy"
categories:
  - "Announcements"
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2010/02/update-to-thumbnail-parts-list.html "
typepad_basename: "update-to-thumbnail-parts-list"
typepad_status: "Publish"
---

<p>I’ve re-written the utility to create a parts list with the thumbnail image.&#0160; I’ve updated the <a href="http://modthemachine.typepad.com/my_weblog/2010/02/parts-list-with-thumbnail-image.html">original post</a> with the new program.&#0160; The new program is an add-in instead of a VBA macro.</p>
<p>For those of you interested, the problem on 64-bit machines was a side effect of a limitation of COM Automation.&#0160; COM Automation is the underlying Microsoft technology that Inventor’s API is based on.&#0160; A limitation it has is that you can’t pass a bitmap between processes.&#0160; When the macro is running on a 32-bit machine, VBA is running in the same process as Inventor so when the API call is made to get the thumbnail image Inventor is able to pass it back to VBA.&#0160; VBA doesn’t have 64-bit support so in order to still provide VBA capabilities with 64-bit Inventor, VBA is run using a separate 32-bit process.&#0160; When the API call is made to get the thumbnail it fails because it can’t pass the bitmap between the Inventor process and the process hosting VBA.&#0160; Rewriting this as an add-in solves the problem because it can be 64-bit and so it can run within Inventor’s process.</p>
