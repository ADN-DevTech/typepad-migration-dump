---
layout: "post"
title: "Debugging PHP"
date: "2014-08-06 14:32:12"
author: "Madhukar Moogala"
categories:
  - "PHP"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/08/debugging-php.html "
typepad_basename: "debugging-php"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/announcing-apphack-20.html">Stephen Preston</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511f18b69970c-pi" style="float: left;"><img alt="Xdebug" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511f18b69970c img-responsive" src="/assets/image_d184db.jpg" style="margin: 0px 5px 5px 0px;" title="Xdebug" /></a>I&#39;ve been enjoying playing with the soon to be released Autodesk 3D viewer client-side API (although I&#39;ve done nothing as cool as <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/my-first-autodesk-360-viewer-sample.html" target="_blank">Kean&#39;s sample</a> yet) and teaching myself a bit of JavaScript and THREE.js along the way. For a long time I cheated, and was using a Java applet someone else wrote for me to generate my API AccessToken because I wanted to focus on the client-side API and not have to worry about anything on the server-side (except basic setup). But now we&#39;ve moved our viewer APIs to our production environment (a 15 minute AccessToken instead of 2 hours) its time to take the plunge and start learning server-side too.</p>
<p>I&#39;ve previously written up my tribulations with (and reasons for) <a href="http://adndevblog.typepad.com/cloud_and_mobile/2014/03/installing-apache-http-server-on-mac-osx.html" target="_self">installing Apache HTTP Server on my Mac</a>, and that decision (along with a recommendation from my colleague Cyrille) made my decision on which server-side API to choose - PHP. My first goal was simply to get the 3D viewer PHP sample <a href="https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples" target="_blank">we&#39;ve posted on GitHub</a> to work. (Thank you to iUX Labs for allowing us to post the code they wrote at our recent Hackathon in Shanghai). This goal has put me in the uncomfortable position of trying to get a non-trivial sample up and running when I know absolutely nothing about PHP (and I&#39;m still pretty sketchy on Apache setup and general Unix terminal usage). I&#39;m writing this up so that anyone in a similar situation can benefit from my pain - and hopefully will save the half day it took me to get up and running.</p>
<p>I&#39;m still using my <a href="http://www.ampps.com/" target="_blank">AMPPS</a> installation, so installing PHP was easy - its part of AMPPS. I just had to change the PHP version to 5.4 or 5.5 (I chose 5.5). My problem was that, although I could easily get the PHP sample to run, every call from my localhost server to the Autodesk API server was failing - I couldn&#39;t even get my AccessToken.</p>
<p>Ok - that should be easy - I can debug the PHP code. How do I debug? Hmm - Not so easy. The most common way to debug PHP is to install <a href="http://xdebug.org/" target="_blank">Xdebug</a> on your server along with a debugger (Eclipse or NetBeans for example, although I&#39;ve started out using <a href="https://www.bluestatic.org/software/macgdbp/" target="_blank">MacGDBp</a> because it looked a bit simpler to setup).</p>
<p>I started looking into installing Xdebug, which was leading me down the same path as my Apache investigation - I was finding a long chain of stuff I had to install so I could install Xdebug. Here is a <a href="https://www.lullabot.com/blog/article/configuring-xdebug-osx-mountain-lion" target="_blank">nice looking blog post on installing/configuring Xdebug</a> that I found after I&#39;d followed up a lot of dead ends.</p>
<p>Of course, then I realized that AMPPS comes with Xdebug pre-installed. You just have to add a few settings to your php.ini files. <a href="http://www.softaculous.com/board/index.php?tid=4714&amp;title=Setting_up_XDebug_on_AMPPS_in_Mac_OS_X" target="_blank">This forum post</a> gives some step-by-step instructions. I also added an extra line (&#39;xdebug.remote_autostart=1&#39;) to the ini file to ensure Xdebug would immediately start when my PHP ran (and would then break on the first line of code).</p>
<p>Now with MacGDBp running, any PHP code executed on my server automatically attaches to MacGDBp and breaks - waiting for me to step through the code. So far so good.</p>
<p>Now I could debug the code, I could see that my authentication call was returning a null AccessToken. Being new to PHP, it took me way too long to realize that my HTTP call was returning an error:</p>
<p style="padding-left: 30px;">SSL certificate problem: unable to get local issuer certificate</p>
<p>(Getting to this point is what took&#0160; most of my time).</p>
<p>Applying the golden rule that if I&#39;m having a problem someone else has probably already posted a solution somewhere, I slipped that error message into a google search, and (as if by magic!) the answer appears.</p>
<p>It turns out that PHP.ini leaves an important line commented out that is required of you&#39;re using SSL. The solution is to create or download a list of certificate authorities you want your server to trust and uncomment and complete the line:</p>
<p style="padding-left: 30px;">;curl.cainfo =</p>
<p>to point to your certificate authority list. Here&#39;s the <a href="http://stackoverflow.com/questions/17478283/paypal-access-ssl-certificate-unable-to-get-local-issuer-certificate" target="_blank">StackOverflow post</a> where I found the answer.</p>
<p>And - hey presto - everything is working as expected.</p>
<p>And now I&#39;ve got the sample to run I have to go back and start pearnign some PHP basics :-).</p>
<p>&#0160;</p>
<p><code>&#0160;</code></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
