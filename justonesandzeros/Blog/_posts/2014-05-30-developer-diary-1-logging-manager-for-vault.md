---
layout: "post"
title: "Developer Diary #1 - Logging Manager for Vault"
date: "2014-05-30 13:59:53"
author: "Doug Redmond"
categories:
  - "Sample Applications"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/05/developer-diary-1-logging-manager-for-vault.html "
typepad_basename: "developer-diary-1-logging-manager-for-vault"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/SampleApp4.png" /></p>
<p>Usually I don’t post any information about my Vault apps until it’s completed.&#0160; But this time I want to try something different.&#0160; I’m going to post information on the app as it is being developed.&#0160; That way people can provide early feedback and suggestions.&#0160; If this format works out well I may use it for other projects in the future.</p>
<p>In this edition, I’ll just talk about the concept for the app.&#0160; I haven’t started coding yet.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Coming Up with the App</strong></p>
<p>I get my app ideas from many places.&#0160; Sometimes it&#39;s from my own use of the product.&#0160; Sometimes people make suggestions to me.&#0160; Sometimes I just want to show off a cool API feature.&#0160; Usually it comes from hearing about people’s usage of the product.&#0160;</p>
<p>For example, AU.&#0160; It’s hard to go to that event and <em>not</em> come away with a bunch of ideas on what can be improved.&#0160; In addition to in-person events, there are plenty of online sources to go for ideas.&#0160; There are discussion groups, beta forums, and the <a href="http://forums.autodesk.com/t5/Vault-IdeaStation/idb-p/2" target="_blank">Idea Exchange</a>.&#0160;</p>
<p>This time around, I’m going to write an app to help manage the various log files that Vault spits out.&#0160; Like most of my apps, this one has been in my head for a while.&#0160; It’s a theme that keeps coming up when talking to customers, and I don’t think there are any Vault-specific solutions out there yet.&#0160; In fact, the problem got worse in Vault 2014 with the AVFS, which has it’s own separate log file.&#0160; This kind of thing that is perfect grounds for a custom app.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>My Vision For App</strong></p>
<p>I’m not big into problem statements, at least not when it comes to apps.&#0160; I usuall just have an idea in my head of how it should work, and I try to make that vision a reality.&#0160; Here is my vision for this app...</p>
<ul>
<li><strong>Consolidation</strong> - Bring all the Vault logs together in one place.&#0160; The 3 main ones I will be focusing on are the ADMS logs, the AVFS logs and the IIS logs.&#0160; The ADMS Console lets you view some of these logs, but it’s just a text reader.&#0160; And you have to read them one at a time.&#0160; I’m thinking of something more like a single database table that can store and retrieve years’ worth of Vault logs in one place.</li>
<li><strong>Deeper Information</strong> - If you mine the log files right, you can find out things like how user 863 downloaded file 16734 yesterday afternoon.&#0160; That’s probably not much use to you unless you are the Rain Man.&#0160; To decode those numbers, you need an app that uses the Vault API.&#0160; So my app will be able to show that user Bob looked at file ProjectX.dwg.</li>
<li><strong>Push Operations</strong> - A couple of people requested a way to get alerts.&#0160; So this app may be able to help out with that.&#0160; It could possible do things like, send out weekly reports, or send out immediate alerts when critical errors pop up.</li>
</ul>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>App Framework</strong></p>
<p>“Don’t re-invent the wheel,” is something engineers like to say.&#0160; Personally, I think it’s fun to re-invent the wheel.&#0160; But in this case, I’m going to follow the popular advice.&#0160; <a href="http://www.splunk.com/" target="_blank">Splunk</a> is a tool that I’ve seen in action and have been very impressed with.&#0160; And Splunk has an API.&#0160;&#0160; So I’m sold at this point.&#0160; At least sold enough to investigate further.</p>
<p>That’s all the blogging I&#39;m goint to do for now.&#0160; In my next Developer Diary, I’ll be installing Splunk, hooking it to Vault and seeing what I can do with their API.&#0160; So keep a look out for that article.&#0160; It will be the most fun you can have without a catapult and a truckload of fruit pies.</p>
<hr noshade="noshade" style="color: #013181;" />
