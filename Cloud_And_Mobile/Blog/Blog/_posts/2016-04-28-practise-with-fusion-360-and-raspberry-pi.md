---
layout: "post"
title: "Practise with Fusion 360 and  Raspberry Pi"
date: "2016-04-28 04:05:38"
author: "Xiaodong Liang"
categories:
  - "Cloud"
  - "IoT"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/04/practise-with-fusion-360-and-raspberry-pi.html "
typepad_basename: "practise-with-fusion-360-and-raspberry-pi"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>These days, I played with <a href="https://www.raspberrypi.org/">Raspberry Pi</a>. Thanks to the tutorials on internet, it went smoothly when I configured the environment, got familiar with the terminal and learnt how to get/set data to some sensors. &#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c84bfe68970b-pi" style="float: left;"><img alt="111" class="asset  asset-image at-xid-6a0167607c2431970b01b7c84bfe68970b img-responsive" src="/assets/image_7109e5.jpg" style="margin: 0px 5px 5px 0px;" title="111" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>I got an idea to make an interesting experiment with Fusion 360. I practiced with (<a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/03/practise-with-fusion-360-and-socketio-javascript-api.html">Socket.IO + Fusion 360</a>&#0160;a few weeks ago. The idea is to emit the temperature value to Fusion 360. Next, the code of Fusion API will change the color of a model according to the values. To have more funs, the code will produce random cracks with the model as if a model is being fractured. I am cleaning up the code and will submit to Github soon.</p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="720" src="https://www.youtube.com/embed/Q62FuDzDwSk" width="1280"></iframe></p>
<p>I will be happy to share my practice in detail in the next blog. One thing I’d like to highlight is: at the beginning, I used Python only to collect the sensor data, but it frequently failed to transfer&#0160;a valid data, but C code works well, however I am not familiar with Socket in C. Finally the solution is to combine C and &#0160;Python, i.e. C collects data and saves to a temp file, while Python reads the file and emit the values to my server. Yes, there is performance issue, but in most cases, it works well.</p>
<p>Sorry, the model looks ugly. I have a dream to be an art designer who designs tne cool model, but it looks I can only be a programmer so far. :)</p>
