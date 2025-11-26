---
layout: "post"
title: "I Make Changes and Nothing Happens"
date: "2015-11-26 09:41:05"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "HTML"
  - "HTML5"
  - "Server"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/11/i-make-changes-and-nothing-happens.html "
typepad_basename: "i-make-changes-and-nothing-happens"
typepad_status: "Publish"
---

<p>Things are going great. You have figured out how to upload a file, how to translate it, and&#0160;maybe even add an extension or two in the viewer. Then it happens. You make a few changes in&#0160;your design model or code and when you view the new version on your website site, nothing has changed.</p>
<p>Your fix isn&#39;t fixed. Your change isn&#39;t changed. <strong>NOTHING HAS CHANGED!</strong> You want to scream, pull out your hair, pound on your computer, and shout blame.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d17b69f1970c-pi" style="display: inline;"><img alt="Headacke" class="asset  asset-image at-xid-6a0167607c2431970b01b8d17b69f1970c img-responsive" src="/assets/image_ca8da6.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Headacke" /></a></p>
<p>&#0160;</p>
<p>Hang on. Calm down. A great many errors are in fact mistakes born of haste. Even if you&#39;re operating under a tight deadline, try to remain calm, and collected, as you proceed. It will help. If you start to get unduly tense, or frustrated, simply get up and walk away for a few minutes. Sometimes all it takes is a fresh, rested pair of eyes to spot a missing semi-colon, or an extra quotation mark, or two to realize that the solution is much simpler than you originally thought. The problem might be with the viewer, it might be with your database, it might be with your server, or it might be something you did to screw things up, but the reality is that a lot of the time it is none of those things. The culprit is probably your Internet browser. Whatever the problem is, we&#39;re here to help.</p>
<p>Let&#39;s look at the possible problems and solutions for what to do when you make a change and nothing happens.</p>
<p>&#0160;</p>
<h3>The Browser Cache</h3>
<p>Did you know that a computer is supposed to make your life easier? Less complicated? It is supposed to save you time and energy and actually improve your life. No? Well, maybe not, but your Internet browser does its best to try to make your life a little easier.</p>
<p>When you first visit a web page, it often takes a while to load, right? But the next page you visit within that site doesn&#39;t take so long to load. This is because, in an effort to be helpful, the browser stores the information on your computer so it reloads&#0160;it from your computer, not from the actual site. This is called the cache and it is meant to speed up your Internet browsing.</p>
<p>The problem comes when you make a small change to your site and the browser doesn&#39;t recognize it as a significant change, so it reloads the same page you just looked at. The solution is to clear or empty your browser&#39;s cache.</p>
<p>&#0160;</p>
<h3>Clearing the Browser Cache</h3>
<p>Normally, to see the changes on your page, you click the Refresh button on the browser toolbar or press the F5 key on your keyboard. In many cases, this simply reloads the page without clearing the browser&#39;s cache. There are some techniques to wipe clean the browser&#39;s cache so you will see the changes when your page reloads. On Chrome, I added a shortcut plugin to do the work, it is call &#39;The Clean Cache Shortcut&#39;, but there is equivalent&#0160;solution for the other browsers such as the Ctrl-Shit-R for Firefox, and etc... Just try this before anything else.</p>
<p>&#0160;</p>
<h3>Server-side Caching</h3>
<p>Sometimes, your server might cache code/pages as well, or if you&#39;re running Node.js, you need to restart the server to see teh changes. It is always a good idea to disable server cache while developing. But make sure to test it with cache enabled before going on production.</p>
<p>&#0160;</p>
<h3>Viewer caching</h3>
<p>During the last Acceleartor in Praha (Czech Republic), I worked with Marius-Mihai Golea <br />from <a href="https://www.stabiplan.com/en-us" target="_self">Stabiplan</a>, &#0160;on a solution using the View &amp; Data API, and Marius-Mihai had a cache issue when re-loading a different version of a model using the same URN.</p>
<p>It is true that if you use the same bucket/filename, the URNs will be identical, and to speed-up&#0160;your viewing experience, the Autodesk viewer cache the information for 24h. So if you try viewing a changed URN, you&#39;ll immediately end-up seeing the same model again and again, unless you clear&#0160;your cache.</p>
<p>After debugging the viewer code, Marius-Mihai came out with a very neat solution which does not require you to clean the cache manually. His solution was to modify the viewer header request like this:</p>
<p>Autodesk.Viewing.HTTP_REQUEST_HEADERS ={<br /> &quot;If-Modified-Since&quot;: &quot;Sat, 29 Oct 1994 19:43:31 GMT&quot;<br />} ;</p>
<p>Very efficient and simple,</p>
<p>This is only needed when you modify and re-translate your design file again and again in your workflow within the same session. The advantage of this technique is that you are not really cleaning&#0160;the cache, you only force the viewer to reload the model definition from the server vs using the cache.</p>
<p>Cleaning the cache wasn&#39;t a solution in this case even if it would have helped. Marius-Mihai addressed&#0160;the real issue vs hiding it.</p>
<p>&#0160;</p>
<p>Special thanks to Marius-Mihai for sharing his solution with us,</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0895ce47970d-pi" style="float: left;"><img alt="Thumbs-up" class="asset  asset-image at-xid-6a0167607c2431970b01bb0895ce47970d img-responsive" src="/assets/image_f03c06.jpg" style="margin-top: 0px; margin-bottom: 5px;" title="Thumbs-up" /></a></p>
