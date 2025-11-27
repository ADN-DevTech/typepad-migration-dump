---
layout: "post"
title: "Thumbnail Optimization"
date: "2012-05-16 09:23:00"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/05/thumbnail-optimization.html "
typepad_basename: "thumbnail-optimization"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>One of the optimizations in Vault 2013 is the use of JPEG as the format for storing thumbnails.&#0160; Previously, the format used by the CAD file was stored in the database.&#0160; For example, Inventor uses the metafile format, which is usually much bigger than a JPEG when at thumbnail size.&#0160; Regardless of what format the file’s thumbnail is, the Vault 2013 Server converts it to JPEG when storing it in the database.</p>
<p>What this means for the Vault developer is that you have to handle 2 possible image formats if you want to render thumbnail data.&#0160; You need to handle the metafile format for files added in Vault 2012 and earlier and you need to handle the JPEG format for files added in Vault 2013.</p>
<p>So here is the updated code for you to use.&#0160;</p>
<p><a href="http://justonesandzeros.typepad.com/Files/RenderThumbnail/RenderThumbnailToImage.cs">RenderThumbnailToImage.cs</a> <br /><a href="http://justonesandzeros.typepad.com/Files/RenderThumbnail/RenderThumbnailToImage.vb">RenderThumbnailToImage.vb</a></p>
<p>Note:&#0160; This code is more optimal than the code in one of my <a href="http://justonesandzeros.typepad.com/blog/2011/05/viewing-thumbnails.html">older posts</a>.&#0160; The old code worked by trying one algorithm then trying the second algorithm if the first one failed.&#0160; The new code detects ahead of time what the image format it.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
