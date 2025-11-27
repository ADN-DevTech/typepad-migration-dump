---
layout: "post"
title: "Latest Version of Fusion Introduces Add-Ins"
date: "2015-03-16 22:49:37"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/03/latest-version-of-fusion-introduces-add-ins.html "
typepad_basename: "latest-version-of-fusion-introduces-add-ins"
typepad_status: "Publish"
---

<p>The version of Fusion that went out this past weekend has a lot of new features but for the API the most important is the introduction of add-in support.&#0160; Previously there was only support for scripts.&#0160; There’s not a big difference between a script and an add-in but that small difference makes a huge difference in how it’s seen and used by the user.</p>
<p>A script is run by the user by executing the “Scripts and Add-Ins” command from the File menu, choosing the script they want to run, and clicking the “Run” button.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb08085797970d-pi"><img alt="ScriptRunning" border="0" height="313" src="/assets/image_379169.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="ScriptRunning" width="377" /></a></p>
<p>&#0160;</p>
<p>An add-in has the option to be loaded automatically when Fusion starts.&#0160; Taking advantage of this small difference, an add-in can use the API to create commands and add them to the Fusion user-interface.&#0160; The add-in’s commands appear to the user like any other Fusion command.&#0160; Unless explicitly unloaded by the user, the add-in remains running in the background throughout the Fusion session so it can respond whenever any of its commands are run and do whatever that command is supposed to do.</p>
<p>The dialog below shows the new dialog for creating a new add-in or script.&#0160;</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb0808579b970d-pi"><img alt="NewAddIn" border="0" height="447" src="/assets/image_691962.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="NewAddIn" width="410" /></a>&#0160;</p>
<p>For this release there are four new topics in the <a href="http://fusion360.autodesk.com/resources">Fusion API help</a> that discuss add-ins and some related technology, that although isn’t exclusive to add-ins is more important for a typical add-in workflow.&#0160; These new topics are shown in the picture below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c7644e71970b-pi"><img alt="FusionNewHelp" border="0" height="719" src="/assets/image_652438.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="FusionNewHelp" width="314" /></a></p>
<p>&#0160;</p>
<p>Add-ins also make it easier to give your program to someone else, either by giving them the add-in files or package it into an installer.&#0160; The files can simply be copied to the correct location on the users machine and Fusion will automatically find the add-in and load it the next time is starts.&#0160; An installer does the same thing but simplifies the copy process.&#0160; The next time Fusion is started, the new commands added by the add-in are immediately available to the user.</p>
<p>-Brian</p>
