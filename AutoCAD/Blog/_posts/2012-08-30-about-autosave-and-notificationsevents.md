---
layout: "post"
title: "About AUTOSAVE and notifications/events"
date: "2012-08-30 13:36:01"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/about-autosave-and-notificationsevents.html "
typepad_basename: "about-autosave-and-notificationsevents"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>When AutoCAD does an AUTOSAVE, it saves the drawing at the OS Temp folder with the name &quot;DrawingName_Numbers.sv$&quot;, as specified on the OPTIONS dialog, and it sends a kSaveMsg message to all applications. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c318f3c4d970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="auto_save_options" border="0" alt="auto_save_options" src="/assets/image_511437.jpg" width="180" height="244" /></a></p>  <p>However, this kSaveMsg message may trigger some custom action on applications that are listening to this notification/event, but in the case of an AUTOSAVE, this is not the correct thing to do.</p>  <p>Because an AUTOSAVE does not trigger a CommandWillStart() notification, if we set a global flag from the CommandWillStart() notification tracking commands with “SAVE” on the command name, then we can detect if is an AUTOSAVE or a normal save called by the user.</p>
