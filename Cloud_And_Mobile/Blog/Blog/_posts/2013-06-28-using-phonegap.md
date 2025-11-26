---
layout: "post"
title: "Using PhoneGap"
date: "2013-06-28 03:56:11"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Mobile"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/06/using-phonegap.html "
typepad_basename: "using-phonegap"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When writing applications for multiple platforms the obvious choice could be web technologies that would work in any browser on any device. At the moment that would not provide though the following:</p>
<ul>
<li>access platform specific functionality: camera, push notifications, etc.</li>
<li>ability to sell your app on the various platform dependent app stores &#0160;</li>
</ul>
<p>The solution could be creating native applications that would host a web viewer which would provide the majority of the functionality through HTML, CSS and JavaScript. That is exactly what <a href="http://phonegap.com" target="_self">PhoneGap</a> from Adobe provides with the added benefit of not having to create the platform specific frameworks yourself. You can just provide the web part and the rest could be taken care of by the <a href="http://build.phonegap.com/apps" target="_self">PhoneGap Build</a> functionality. This is free for public projects plus a single private project. Beyond that you would need to pay for the service.&#0160;</p>
<p>Another cool feature of this solution is the ability to <a href="https://build.phonegap.com/docs/phonegap-debug" target="_self">debug into your application</a> on a specific device through a web browser and that you can easily push the latest version of your project to the test device using <a href="https://build.phonegap.com/docs/hydration" target="_self">Hydration</a>.&#0160;</p>
<p>There is a good tutorial on getting started with PhoneGap: <a href="http://coenraets.org/blog/phonegap-tutorial/" target="_self"></a><a href="http://coenraets.org/blog/phonegap-tutorial/" target="_self">Tutorial: Developing a PhoneGap Application</a></p>
<p><a href="http://coenraets.org/blog/phonegap-tutorial/" target="_self"></a>Before doing the above it might be worth setting up a GitHub account, adding a new repository and then setting up the sample tutorial to save to that online repository and using that on <a href="https://build.phonegap.com/apps" target="_self">https://build.phonegap.com/apps</a>&#0160;so that it will be really easy to keep the app on the test device up-to-date.</p>
<p>Since I&#39;m using <a href="http://www.eclipse.org/juno/" target="_self">Eclipse</a> for the Android development I used the <a href="http://www.eclipse.org/egit/" target="_self">EGit plugin</a>. There is a nice tutorial for setting that up too: <a href="http://www.vogella.com/articles/EGit/article.html" target="_self">http://www.vogella.com/articles/EGit/article.html</a>&#0160;&#0160;</p>
<p>Nothing is ever straight forward. I had issues with pushing my Eclipse project to GitHub, then the PhoneGap build did not work because of server issues, then the above tutorial project did not work becasue of a loading issue. :( The letter could be solved using this: <a href="http://www.robertkehoe.com/2013/01/fix-for-phonegap-connection-to-server-was-unsuccessful/" target="_self">Fix for PhoneGap: Connection to server was unsuccessful</a>&#0160;&#0160;</p>
<p>For a long time I could not make the onilne debugger work. I think they had issues with the server. Today I managed to use it. The webpage takes you through the 3 simple steps to look into your running application: <a href="http:\\debug.phonegap.com" target="_self">http:\\debug.phonegap.com</a></p>
<p><a href="http:\\debug.phonegap.com" target="_self"></a>So I needed to update my index.html with the script tag (I placed it inside the &lt;header&gt; section) - just to be on the safe side I also added it to main.html. :) Then I just pushed the latest version to github from Eclipse. Then clicked &quot;Update code&quot; on the phonegap build site, downloaded the latest version to my Android device and started it. Then when I when to the phonegap debugger site I could see my device listed. When I clicked the &quot;Elements&quot; button I could see and change the html content of my app on the mobile device. :) &#0160;&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192abb59902970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Phonegap_debug" class="asset  asset-image at-xid-6a0167607c2431970b0192abb59902970d" src="/assets/image_c2eb2c.jpg" style="width: 450px;" title="Phonegap_debug" /></a></p>
<p>Overview of alternative ways of debugging your app:&#0160;<a href="https://github.com/phonegap/phonegap/wiki/Debugging-in-PhoneGap" target="_self">https://github.com/phonegap/phonegap/wiki/Debugging-in-PhoneGap</a></p>
<p>If you are interested in this sort of technology then it&#39;s worth looking around a bit on the net to find the best one for you as there are many solutions similar to PhoneGap. There are differences between what they offer and the price they charge you.</p>
