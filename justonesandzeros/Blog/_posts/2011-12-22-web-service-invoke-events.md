---
layout: "post"
title: "Web Service Invoke Events"
date: "2011-12-22 08:21:30"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/12/web-service-invoke-events.html "
typepad_basename: "web-service-invoke-events"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>The third type of event we introduced in Vault 2012 is the Web Service Invoke Events.&#0160; If you want to read up on the other two types, you can go <a href="http://justonesandzeros.typepad.com/blog/2011/04/faq-events.html">here</a> and <a href="http://justonesandzeros.typepad.com/blog/2011/09/vault-explorer-command-events.html">here</a>.&#0160; The purpose of the Invoke events is to give you a single choke point for whenever <strong><em>your</em></strong> code invokes a web service call to the Vault Server.&#0160;</p>
<p>Notice that I stressed the word <em><strong>your</strong></em>.&#0160; This type of event is not for hooking to the existing Vault features.&#0160; That’s what the Web <a href="http://justonesandzeros.typepad.com/blog/2011/04/faq-events.html">Service Command Events</a> are for.</p>
<p>The Invoke events just helps you manage your own code.&#0160; And because it’s limited to you code, there is a lot more you can do with it.&#0160; For example, you can alter parameters going in and out.&#0160; With all the other event types, you are just an observer.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>So what is this good for?&#0160; Offhand, I can think of 4 uses:</p>
<p>1. <strong>Common Error Handling</strong> – I find that I want to handle Exceptions from the Vault Server in the same way most of the time.&#0160; I don’t want to have the same handler code sprinkled all over my app, so I can just put it all in an event handler.</p>
<p>2. <strong>Logging</strong> – Let’s say you’re a consultant, and you developed an app for a customer.&#0160; That app is not working properly, and you have to figure out why.&#0160; You can’t just run in the debugger on the customer’s machine.&#0160; So how to you get information on what is happening inside your app?&#0160; The solution is to sprinkle logging capability throughout your code so that you can get a nice trace from the customer.&#0160; Using Invoke events you now have a way to log every Vault server call.</p>
<p>3. <strong>Debugging</strong> – Even during the development process it’s good to have a print out of what is going on between your code and the Vault server.&#0160; If you code is very complex, it’s very difficult to keep track of which commands result in which API calls.&#0160; These events can help sort things out for you.&#0160;</p>
<p>4. <strong>Performance</strong> – If you app is slow, it’s helpful to know how much time is being used by calls to the Vault server.&#0160; Based on my past experience, slowness is usually due to redundant API calls or calls that can be optimized.&#0160;</p>
<p>&#0160;</p>
<p>Coming soon:&#0160; Sample code....</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
