---
layout: "post"
title: "Disabled event handlers"
date: "2012-05-01 16:30:25"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/disabled-event-handlers.html "
typepad_basename: "disabled-event-handlers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>A post on the <a href="http://forums.autodesk.com/t5/NET/bd-p/152" target="_self">AutoCAD .NET forum</a> the other day reminded me of an issue I wasted a lot of time debugging when I was developing my <a href="http://labs.autodesk.com/utilities/digsigstamp/" target="_blank">DigSigStamp</a> <a href="http://labs.autodesk.com/utilities/ADN_plugins/" target="_blank">Plugin of the Month</a>. Everything was working fine until <a href="http://through-the-interface.typepad.com/through_the_interface/" target="_blank">Kean</a> cleaned up my code ready for posting to the Labs website. Then my event handlers would suddenly stop being called the second time my code ran.</p>
<p>It turned out that the code cleanup had introduced a small bug in my event handler that was causing an exception to be thrown - which I wasn&#39;t catching. (Slap my wrist for shoddy error handling technique:-). This led to a conversation with Albert Szilvasy - the Architect for the AutoCAD .NET API - who explained slowly and patiently to me that letting a managed exception pass up to AutoCAD from your event handler was a really silly thing to do.&#0160;Make sure you&#0160;handle exceptions thrown in your event handler in the event handler to stop them bubbling up to AutoCAD. (Of course, its good practice to always handle any exceptions you can handle in all the code you write).</p>
<p>So if your event handlers suddenly stop working, go to Debug -&gt; Exceptions and set Visual Studio to stop when exceptions are thrown. It might just show you your &#39;smoking gun&#39;.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163050967be970d-pi" style="display: inline;"><img alt="5-1-2012 4-22-48 PM" class="asset  asset-image at-xid-6a0167607c2431970b0163050967be970d" src="/assets/image_148609.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="5-1-2012 4-22-48 PM" /></a><br /><br /></p>
<p>&#0160;</p>
