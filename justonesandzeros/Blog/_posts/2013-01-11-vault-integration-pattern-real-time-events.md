---
layout: "post"
title: "Vault Integration Pattern - Real-time Events"
date: "2013-01-11 09:11:27"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2013/01/vault-integration-pattern-real-time-events.html "
typepad_basename: "vault-integration-pattern-real-time-events"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Welcome to part 2 of my 3-part series on Vault integration patterns.&#0160; There are many more than 3 patterns, but I’m going to stick to ones that I’ve actually used on an app or product.&#0160; </p>
<p>Today’s pattern is the idea that Vault communicates with the other system, in real time, whenever things change.&#0160; I used this pattern for <a href="http://justonesandzeros.typepad.com/blog/2011/09/vault-bluestreak.html">Vault ♥ Bluestreak</a>.&#0160; In that app, I hooked to events from Vault Explorer and Events.&#0160; This allowed the app to “listen” to what the user is doing and send out notifications to Bluestreak.&#0160; Bluestreak would then post on the notification on it’s Activity Stream, which is similar to a Facebook wall.</p>
<p>Maybe it will be clearer if you see the demo video (no audio):</p>
<iframe allowfullscreen="allowfullscreen" frameborder="0" height="315" src="http://www.youtube.com/embed/abF1CVQgdWU" width="420"></iframe>
<p>The general idea here is that the integration app listens to events and interacts with the other system as soon as the event comes in.&#0160; Vault doesn’t have server side events, so the app has to be client-side.&#0160; In this case, the communication is one way.&#0160; I’m sending alerts to Bluestreak, but I don’t need to get any sort of response.&#0160; So the code makes heavy use of Post events.&#0160; The communication with Bluestreak is done in another thread for performance reasons.</p>
<p>If you have a case where you need to read data from the other system, this pattern still works.&#0160; The only difference is that you would be using the GetRestrictions or Pre events.&#0160; Multi-threading probably won’t work here since you need to halt the Vault operation while waiting for the other system to respond.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Advantages:</p>
<ul>
<li><strong>Operations occur in real time</strong> - No need to keep hitting the refresh button, like when working with the Job Processor. </li>
<li><strong>User Interaction</strong> - If you need input from the user, you can do it.&#0160; You can also pop up errors immediately if something goes wrong. </li>
</ul>
<p>Disadvantages:</p>
<ul>
<li><strong>Events are not guaranteed</strong> - Because the operations are client-side, they occur outside the database transaction.&#0160; This means that there is a chance that the event happens, but the Post event never fires.&#0160; For example, a file changes lifecycle state, and the power goes out right before the Post event fires.&#0160; In this case, the Post even is lost forever.&#0160; Along the same lines, a Pre event may fire, but the actual operation my never get a chance to run.&#0160; So if you need 100% guarantee on your data, this is not the pattern to use. </li>
<li><strong>Client side install</strong> - If you have 100 Vault users that need to integrate with the other system, you need to install the app on 100 machines.&#0160; If an app starts failing for a user, it may not be immediately obvious. </li>
</ul>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
