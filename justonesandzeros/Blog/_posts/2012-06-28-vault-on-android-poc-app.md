---
layout: "post"
title: "Vault on Android - POC App"
date: "2012-06-28 10:10:34"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2012/06/vault-on-android-poc-app.html "
typepad_basename: "vault-on-android-poc-app"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p>I recently decided to join the 21st century and get a smartphone.&#0160; And guess what, you can write programs on these things.&#0160; I just happen to be a programmer, so it’s app writing time!!!&#0160; Fish gotta’ swim; birds gotta’ fly.</p>
<p>This is just a proof-of-concept app.&#0160; It doesn’t do anything useful, but I wanted to prove that it could be done.&#0160; The app is a port of the VaultList sample in the SDK.&#0160; It lists out all the files in the Vault.&#0160; That’s all it does.</p>
<p><img alt="" src="/assets/screenshot.png" /></p>
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault 2013 </li>
<li>An Android device - v2.1 or later </li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/Android/VaultList.apk" target="_blank">Click here for the app</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/Android/VaultList-Android-src.zip" target="_blank">Click here for the source code</a></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Installation and Usage:</strong></p>
<p>To get the app installed, you need to go into your phone settings an allow apps from unknown sources (non-Market locations).&#0160; Next you need to get the .apk file installed somehow.&#0160; The only way I found was to email it to my gmail account.&#0160; When I use the Gmail client on my phone, it allows me to install the app.</p>
<p>Running the app is pretty straightforward.&#0160; There are fields for the Vault information.&#0160; Fill it in and hit run.&#0160; You need a Wi-Fi connection to the network where Vault is running.&#0160; If you are having trouble connecting, try using the IP address of the Vault server.&#0160; You can also try opening your phone’s browser and going to http://[server]/AutodeskDM/Services/v17/DocumentService.asmx to verify that your phone can locate the Vault server.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Source Code:</strong></p>
<p>The code is written in Java using the Eclipse IDE.&#0160; It’s similar to my other <a href="http://justonesandzeros.typepad.com/blog/2010/01/java-vault-client.html" target="_blank">Java sample app</a>.&#0160; For the Android app, I used a library called <a href="http://code.google.com/p/ksoap2-android/" target="_blank">ksoap2</a> to make the web service calls to Vault.&#0160; My code is written bottom-up.&#0160; I only implemented the Vault API functions and classes that I needed.&#0160; I didn’t implement the entire DocumentService or File class, for example.&#0160; However the pattern for implementing these functions and classes is pretty obvious.</p>
<p>Special thanks to the <a href="http://java.dzone.com/articles/invoke-webservices-android" target="_blank">DZone blog article</a> for instructions on using ksoap2.&#0160; These developer blogs are quite helpful!</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Future plans:</strong></p>
<p>I would like to write a useful mobile app for Vault one day.&#0160; So if you have any ideas on what that app should be, post a comment.</p>
<p>I have no plans to develop in iPad or iPhone since it requires me to spend thousands of dollars buying a Mac.</p>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
