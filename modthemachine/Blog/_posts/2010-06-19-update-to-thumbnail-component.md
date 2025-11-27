---
layout: "post"
title: "Update to Thumbnail Component"
date: "2010-06-19 00:16:11"
author: "Adam Nagy"
categories:
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2010/06/update-to-thumbnail-component.html "
typepad_basename: "update-to-thumbnail-component"
typepad_status: "Publish"
---

<p>In my <a href="http://modthemachine.typepad.com/my_weblog/2010/06/accessing-thumbnail-images.html">previous post</a> I described how to access thumbnail images in Inventor documents.&#160; At the bottom of that post is a link to an updated version of a component that provides access to thumbnails.&#160; A problem has been reported since I posted that code and has now been fixed.&#160; </p>  <p>It’s likely that most of you would have never noticed the problem, since it is a bit obscure.&#160; The issue was with how the component was registering itself.&#160; It was registering itself in the Current User portion of the registry which meant only the user that it was registered for would have access to the component.&#160; In the problem that was reported, they were running Win7 and the application that was using the component needed to run as an administrator.&#160; Running as an administrator looks at a different portion of the registry so the application couldn’t find the thumbnail component and would fail.&#160; The fix changes the component so it is now registered for everyone.&#160; Please see the readme for details on how to register it.</p>  <p>I updated the previous post so the link at the bottom is to the new version of the component and you can also access <a href="http://modthemachine.typepad.com/ThumbnailView.zip">here</a>.</p>
