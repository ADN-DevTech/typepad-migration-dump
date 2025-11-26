---
layout: "post"
title: "Making the Viewer's background transparent"
date: "2015-08-05 03:36:12"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/making-the-viewers-background-transparent.html "
typepad_basename: "making-the-viewers-background-transparent"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Unfortunately, there is no easy way to do this right now. Our off screen render buffer does not have alpha and the WebGL context is initialised without alpha. At the very least those two things would have to be changed.</p>
<p>Keep in mind that having transparent background may result in lower quality antialiasing around the object silhouette.</p>
