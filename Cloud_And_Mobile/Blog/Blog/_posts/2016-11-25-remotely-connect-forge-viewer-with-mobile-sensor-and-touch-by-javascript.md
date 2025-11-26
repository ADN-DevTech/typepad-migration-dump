---
layout: "post"
title: "Remotely Connect Forge Viewer with Mobile Sensor and Touch by JavaScript"
date: "2016-11-25 09:40:25"
author: "Xiaodong Liang"
categories:
  - "Javascript"
  - "Mobile"
  - "View and Data API"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/11/remotely-connect-forge-viewer-with-mobile-sensor-and-touch-by-javascript.html "
typepad_basename: "remotely-connect-forge-viewer-with-mobile-sensor-and-touch-by-javascript"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<img src="/assets/Viewer-2.7.*-green.svg" alt="" />
<p>Recently, in order to prepare some demos, I played a bit with mobile + Forge Viewer. I was thinking about the scenario which gets data of mobile sensors/touch behavior and emits them to Forge Viewer. It will be tedious to me if setup a native app of mobile, so I tried to check the possibility if JavaScript could do the job. Fortunately, such technologies are quite ready. searching by ‘javascript mobile sensor’ on internet, you will find quite a lot of relevant articles.</p>
<p>So, I played with something: Gyro sensor, Touch, Camera / Photo. In this article, I will show the demos on the Gyro sensor and Touch, and show Camera / Photo in the next blog.</p>
<p>Actually, the core code is very straightforward, e.g. to monitor Gyro sensor, the relevant codes would be:</p>
<script src="https://gist.github.com/xiaodongliang/67cad4fda231b4e190393cff8e73d33a.js"></script>
<p>While for touch, it is similar to the process of mouse with PC. The relevant events are:&nbsp; TouchStart = MouseDown; TouchEnd = MouseUp; TouchMove = MouseMove.</p>
<p>The only thing you would need to be aware is on mobile, the touch could be 1 finger or several fingers. So you might need to get the corresponding finger touch from the collection: evt.changedTouches.</p>
<script src="https://gist.github.com/xiaodongliang/bdec62b589bdeff62d61043a8abf3481.js"></script>
<p>To have fun, I applied the workflow to two demos:</p>
<p>1. Remotely move an object within Forge Viewer by the data of Gyro sensor. The code draws a cylinder in the viewer by Three.js and monitors the data from socket. When the data is changed, the cylinder will move with the three values of Gyro.</p>
<p>&nbsp;</p>
<p><iframe width="1280" height="720" src="https://www.youtube.com/embed/LXiuOpCibyo" frameborder="0" allowfullscreen></iframe></p>
<p>2. Remotely navigate in the viewer. The web page on mobile will send data of Gyro sensor or touch distance to socket. The main page monitors the&nbsp; data from socket and translate Gyro value to the rotate angles of the viewer. And also translate the touch distance to the distance of moving forward/backward (walking).</p>
<p>&nbsp;</p>
<p><iframe width="1280" height="720" src="https://www.youtube.com/embed/BiwD-E3sXoQ" frameborder="0" allowfullscreen></iframe></p>
<p>The whole sample codes are available at:</p>
<p><a title="https://github.com/xiaodongliang/Forge-Viewer-Mobile-Sensor" href="https://github.com/xiaodongliang/Forge-Viewer-Mobile-Sensor">https://github.com/xiaodongliang/Forge-Viewer-Mobile-Sensor</a></p>
