---
layout: "post"
title: "Counting the active session number and tune up concurrent performance of MapGuide/AIMS"
date: "2012-08-29 22:46:23"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/counting-the-number-of-sessions-that-are-active-and-tune-up-concurrent-performance-of-mapguideaims.html "
typepad_basename: "counting-the-number-of-sessions-that-are-active-and-tune-up-concurrent-performance-of-mapguideaims"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>I was asked how to count the number of active sessions in AIMS, unfortunately there is no public API to do that. But I came across <a href="http://themapguyde.blogspot.com/2012/06/thoughts-on-mapguide-scalability.html">Jackie’s blog post</a> (Jackie is an active contributor of MapGuide Open Source), in which he exposed a method, it is pretty interesting, I would like to share with you:</p>  <p>&quot;You can actually count the number of sessions that are active by looking at the Repositories/Session folder of your MapGuide Server install directory. Each session takes 2 files (a .db file and a .dbxml file), so the math is not rocket science.”</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c318c451e970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_0f1122.jpg" width="463" height="329" /></a></p>  <p>In this blog post, Jackie also demonstrate the performance capability of MapGuide and provide suggestions how to tune it up. If you have performance problem with MapGuide, especially when you have many concurrent user, <a href="http://themapguyde.blogspot.com/2012/06/thoughts-on-mapguide-scalability.html">this blog</a> will definitely help you. </p>
