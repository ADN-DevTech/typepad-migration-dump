---
layout: "post"
title: "Events and Multiple Client Versions"
date: "2012-09-07 10:09:27"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/09/events-and-multiple-client-versions.html "
typepad_basename: "events-and-multiple-client-versions"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>When Vault 2012 came out, it introduced the <a href="http://justonesandzeros.typepad.com/blog/2011/04/faq-events.html" target="_blank">events feature</a>, which allowed you to write your code once and have it hook to all Vault clients on the machine.&#0160; However, I want to point out that these events are <strong>version specific</strong>.&#0160; </p>
<p>This aspect wasn’t a problem in Vault 2012, because you needed the 2012 API to connect to the 2012 server.&#0160; But now, the <a href="http://justonesandzeros.typepad.com/blog/2012/06/web-service-compatibility.html" target="_blank">web service compatibility feature</a> in Vault 2013 adds some complications.&#0160; The Vault 2013 server supports a mix of 2012 and 2013 clients.&#0160; For example, an engineer can use Vault Explorer 2013 and Inventor 2012 on the same vault.&#0160; If you write an event handler, <strong>it’s not going to be able to hook to both sets of clients</strong>.</p>
<p>When you write an event handler, you are writing an extension, and extensions are bound to a specific version of Vault SDK DLLs.&#0160; For example, if you use the Vault 2013 API to hook to the post check-out event, you would only get events from 2013 clients.&#0160; Any 2012 clients would completely bypass your event.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>How to workaround:</strong></p>
<ol>
<li>You need both the Vault 2012 SDK and Vault 2013 SDK. </li>
<li>Create an assembly project, reference the Vault 2012 SDK DLLs, and write your event handler. </li>
<li>Copy the assembly project, change the references to Vault 2013 SDK DLLs, and rebuild. </li>
<li>When you deploy, you need to deploy both assemblies to the appropriate locations. </li>
</ol>
<p>Steps 2 and 3 can be done in any order.&#0160; But the idea is you have two separate assemblies to build.</p>
<p>There are some things you can do to minimize the code copy.&#0160; One technique is the “paste as link” feature so that you can share source code projects.&#0160; You can also break out logic into a separate utility DLL, as long as it doesn’t reference any Vault DLLs.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Things that will not work:</strong></p>
<p>You are welcome to try other approaches to solving the problem, but I’m here to tell you that the following approaches will not work.&#0160; </p>
<ul>
<li>Create a Vault 2013 or 2012 extension and deploy it to both the 2012 and 2013 extension folders.&#0160; <br /><em>Vault will only load extension with the matching ApiVersion attribute.        <br /></em></li>
<li>Create an extension that references both 2012 and 2013.&#0160; <br /><em>Visual Studio will block you.        <br /></em></li>
<li>Dynamically load the correct SDK DLLs at runtime.&#0160; <br /><em>Remember that you are an event handler.&#0160; You don’t call the Vault API; the Vault API calls you.&#0160; The DLLs are already loaded.</em> </li>
</ul>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Final Notes:</strong></p>
<p>I’ve said this before, but event handlers that hook to all Vault clients is not a good idea in practice.&#0160; For a number of reasons, I’ve found that it’s much better if you can hook to a specific app.&#0160; This compatibility issue is just one more reason.</p>
<p>Here is an <a href="http://justonesandzeros.typepad.com/blog/2011/07/event-scope.html" target="_blank">article on event scope</a>, if you want to know other ways to hook to web service events.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
