---
layout: "post"
title: "Event Scope"
date: "2011-07-22 08:00:00"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/07/event-scope.html "
typepad_basename: "event-scope"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Although the web service events feature was designed to hook to all Vault clients on a computer, it doesn&#39;t meant that it is<em> required</em> to hook to them all.&#0160; Sometimes you want to hook to an event for just one application, and that&#39;s fine too.</p>
<p>I&#39;ll refer to the <strong>scope</strong> of the event as the set of applications that you are interested in receiving events from.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Scenario 1:&#0160; Global scope</strong></p>
<p>If you want to get events from all Vault clients on a computer, you can follow the instructions in the SDK documentation.&#0160; The &quot;Web Service Command Events&quot; page in the Getting Started section goes over all the details.&#0160; Here is a basic summary:</p>
<ol>
<li>Create a DLL project. </li>
<li>Create a class that implements the IWebServiceExtension interface. </li>
<li>Subscribe to events in the OnLoad() function. </li>
<li>Write your event handler logic. </li>
<li>Set your &lt;extensionType&gt; to WebService in the .vcet.config file. </li>
<li>Deploy to the ProgramData folder. </li>
</ol> 
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Scenario 2:&#0160; Vault Explorer scope</strong></p>
<p>Let&#39;s say you only care about events within your Vault Explorer plug-in.&#0160; For example, you have a custom command or tab view that needs to know about these events.&#0160; The steps are different in this case:</p>
<ol>
<li>Open up the DLL project for your Vault Extension. </li>
<li>Do not implement the IWebServiceExtension interface. </li>
<li>Subscribe to events in the OnStartup() function for your IExplorer implmentation. </li>
<li>Write your event handler logic. </li>
<li>Your &lt;extensionType&gt; is still VaultClient in the .vcet.config file.&#0160; No changes or additions are needed. </li>
<li>Deployment is still to the ProgramData folder.&#0160; Again, no changes. </li>
</ol> 
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Scenario 3:&#0160; CAD application scope using a plug-in</strong></p>
<p>If you are plugging into a CAD app, the steps are very similar to scenario 2.&#0160; The main difference is that your plug-in will have a different set of rules on how to set up and deploy.&#0160; Here are the steps, at a high level:</p>
<ol>
<li>Open up the project for your CAD plug-in. </li>
<li>Do not implement the IWebServiceExtension interface. </li>
<li>During startup, subscribe to the web service events from the Vault API.&#0160; This assumes that the CAD API framework lets your code run during startup. </li>
<li>Write your event handler logic. </li>
<li>Deploy according to the rules of the CAD framework. </li>
</ol>
<p>This technique will allow you to hook to all the Vault web service events for that CAD application, even if another plug-in fired them.&#0160; For example, you can write your own AutoCAD plug-in which responds to events from the default Vault plug-in.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Scenario 4:&#0160; &quot;Application X&quot; scope without using a plug-in</strong></p>
<p>If you want to scope your event to an application, but you can&#39;t write a plug-in, there is still an option.&#0160; All you need to do is follow scenario 1.&#0160; In step 4, when you are writing your event handler logic, check the name of the running process.&#0160; If it&#39;s not an application you want events from, exit the function.</p>
<p>There is no perfect way I know of to get the name of the current process.&#0160; The safest ways is probably to can use the first value from System.Environment.GetCommandLineArgs() to find the application name.&#0160; You can also use System.Diagnostics.Process.GetCurrentProcess().ProcessName, but it might throw security exceptions depending on the user permissions.&#0160; System.Windows.Forms.Application.ExecutablePath is also unreliable because it only works for Windowed applications.</p>
<p>The nice thing about this technique is that you can allow multiple applications.&#0160; Or you could use it to <em>exclude</em> specific applications.&#0160; For example, you exit the event handler if the application name is &quot;JobProcessor.exe&quot;.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
