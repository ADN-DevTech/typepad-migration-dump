---
layout: "post"
title: "Lifecycle Event Editor - The Plugin"
date: "2013-02-27 16:24:16"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2013/02/lifecycle-event-editor-the-plugin.html "
typepad_basename: "lifecycle-event-editor-the-plugin"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p>I decided to fulfill <a href="http://forums.autodesk.com/t5/Autodesk-Vault-IdeaStation/Add-Lifecycle-Event-Editor-to-the-administrator-tools/idi-p/3740578" target="_blank">my own request</a> for this month.&#0160; I didn’t have much time to spend writing a new app, so I decided to grab Lifecycle Event Editor code from the EXE and tweak it to run inside Vault Explorer.</p>
<p>The plug-in itself has the exact same feature set as the exe.&#0160; But if you are not familiar with it, here is a quick run down.&#0160; You can use the Lifecycle Event Editor to tell the server to fire jobs when objects move through specific lifecycle states.&#0160; For example, you want a job of type “Dougpunch” to be created every time a file goes from For Review to Released, you might set something up like this.&#0160; </p>
<p><img alt="" src="/assets/Screenshot.png" /></p>
<p>If you commit the changes then look at the Job Queue... you won’t see anything.&#0160; The Lifecycle Event Editor does not actually put any jobs on the queue, it just lets you configure the server settings.&#0160; The “Dougpunch” jobs show up when you move files from For Review to Released.&#0160; </p>
<p>Of course, if you try this out in your Vault nothing interesting will happen.&#0160; It will just result in a bunch of Dougpunch jobs on the queue that sit there until deleted.&#0160; There is nothing that is set up to handle Dougpunch jobs.&#0160; At least I hope there is not.&#0160; </p>
<p>The <a href="http://justonesandzeros.typepad.com/blog/2010/03/lifecycle-event-jobs.html" target="_blank">lifecycle event feature</a> is useful for when you define your own job types through the API.&#0160; There are also some utilities, such as <a href="http://wikihelp.autodesk.com/Vault/enu/Help/Help/0097-Administ97/0205-Project_205" target="_blank">Project Sync</a>, which hook into this framework.</p>
<hr noshade="noshade" style="color: #013181;" />
<p>Requirements:</p>
<ul>
<li>Vault 2013 Workgroup / Collaboration / Professional </li>
<li>Job Queue enabled </li>
<li>Admin rights </li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/LifecycleEventEditor/LifecycleEventEditorPlugin-1.0.1.0-bin.zip">Click here to download the application</a>     <br /><a href="http://justonesandzeros.typepad.com/Apps/LifecycleEventEditor/LifecycleEventEditorPlugin-1.0.1.0-src.zip">Click here to download the source code</a></p>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
