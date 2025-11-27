---
layout: "post"
title: "Document.Thumbnail property always fails using Inventor API from out of process"
date: "2012-08-16 15:24:33"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/documentthumbnail-property-always-fails-using-inventor-api-from-out-of-process.html "
typepad_basename: "documentthumbnail-property-always-fails-using-inventor-api-from-out-of-process"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>The Document.Thumbnail property will always return nothing when called from an out of process application. There is a known Microsoft limitation on marshalling bitmaps across COM APIs between two processes. If your VB application runs out-of-process of Inventor process, then you hit the Microsoft limitation. Also if you are debugging an in-process client application (e.g. Inventor AddIn), then the VB environment actually creates an out-of-process exe as part of the debugging process, this would also prevent you from marshalling bitmaps across the API.</p>  <p>Usually you can use one of the following several ways to avoid that problem:    <br />1. Use Apprentice because it's running in its own process. (VBA 32 bit runs in the same process with Inventor but not 64bit).&#160; <br />This <a href="http://adndevblog.typepad.com/manufacturing/2012/07/get-thumbnail-image-by-metafile.html" target="_blank">post</a> shows how to use Apprentice to get the thumbnail.</p>  <p>2. Create an in-process Addin application (DLL addin) - Note for this way, running the Addin application (not debug) is ok, but if you debug the application from VB or .net IDE, you still need to comment out the codes relevant to the thumbnail picture interface so your application can be loaded in Inventor successfully, at last comment in those thumbnail picture related codes before releasing your application.</p>  <p>This <a href="http://modthemachine.typepad.com/my_weblog/2010/03/document-thumbnails-and-button-icons.html" target="_blank">Mod The Machine post</a> has more information about this issue. </p>
