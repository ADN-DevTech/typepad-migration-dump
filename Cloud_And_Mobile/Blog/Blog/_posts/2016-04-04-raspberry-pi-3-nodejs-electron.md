---
layout: "post"
title: "Raspberry Pi 3 + NodeJS + Electron"
date: "2016-04-04 10:53:03"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "IoT"
  - "Javascript"
  - "NodeJS"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/04/raspberry-pi-3-nodejs-electron.html "
typepad_basename: "raspberry-pi-3-nodejs-electron"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>A couple weeks ago I did a testing project with <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/03/handsfree-view-data-voice-activated.html">voice recognition and voice feedback for AutoCAD View &amp; Data</a>. After that I started asking myself: can we expand this to create a personal assistant? The idea was to leave it on the house, for instance, and then interact with it, so it should be cheap enough...</p>
<p>That was my weekend fun project: setup Raspberry Pi $35 dollar computer with NodeJS to run a simple voice activated personal assistant! This still my first interaction, but seems to work for some basic tasks...&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/cGe0hh9j5OM?feature=oembed" width="459"></iframe>&#0160;</p>
<p>Of course I&#39;m not starting from zero, I&#39;m using <a href="https://www.talater.com/annyang/">Annyang</a> and <a href="http://responsivevoice.org/">Responsive Voice</a> libraries for voice. But the really cool piece was <a href="http://electron.atom.io/">Electron</a>: it allow a JavaScript code to run as a desktop application and it&#39;s available for many different platforms.</p>
<p>From zero to hero, follow the steps at this <a href="https://github.com/augustogoncalves/personal-assistant">Github repository</a>. The <a href="https://github.com/augustogoncalves/personal-assistant/blob/master/config.js">config.js file</a> should be adjusted for different phrases, but new commands need implementation at <a href="https://github.com/augustogoncalves/personal-assistant/blob/master/js/app.js">app.js file</a>.</p>
<p>Enjoy! And have fun!</p>
