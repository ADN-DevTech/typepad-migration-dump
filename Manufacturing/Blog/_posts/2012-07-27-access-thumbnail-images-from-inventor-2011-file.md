---
layout: "post"
title: "Access thumbnail Images from Inventor 2011 file"
date: "2012-07-27 08:44:21"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/access-thumbnail-images-from-inventor-2011-file.html "
typepad_basename: "access-thumbnail-images-from-inventor-2011-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>This post is for Inventor 2011 only.</p>  <p><b>Issue</b></p>  <p>The thumbnail preview in Inventor 2011 has changed and is no longer accessible as it was in previous versions. The ThumbnailViewer.dll and provided SDK samples from Autodesk cannot work for the files of Inventor 2011. It failed at ImageConverter.IpictureToImage</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>This is a known issue. The format for thumbnails changed from bmp to <strong>png</strong> in 2011, while ThumbnailViewer does not update accordingly.We have provided the updated version in SP1 of Inventor 2011.&#160; </p>  <p>Before SP1, our expert Brian has posted the updated version of ThumbnailViewer on his blog. It has the source code you can even use that to see what has changed in ThumbnailViewer.</p>  <p><a href="http://modthemachine.typepad.com/my_weblog/2010/06/accessing-thumbnail-images.html">http://modthemachine.typepad.com/my_weblog/2010/06/accessing-thumbnail-images.html</a></p>  <p>and an updated version to fix the problem to enable the application that was using the component to run as an administrator.</p>  <p><a href="http://modthemachine.typepad.com/my_weblog/2010/06/update-to-thumbnail-component.html#tp">http://modthemachine.typepad.com/my_weblog/2010/06/update-to-thumbnail-component.html#tp</a></p>
