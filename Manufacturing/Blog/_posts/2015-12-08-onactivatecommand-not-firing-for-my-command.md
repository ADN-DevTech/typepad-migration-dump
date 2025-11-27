---
layout: "post"
title: "OnActivateCommand not firing for my command"
date: "2015-12-08 06:32:07"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/onactivatecommand-not-firing-for-my-command.html "
typepad_basename: "onactivatecommand-not-firing-for-my-command"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Built-in <strong>Inventor</strong> commands in general are expected to fire the <strong>OnActivateCommand</strong> event of the <strong>UserInputEvents</strong> object and set the <strong>ActiveCommand</strong> property of the <strong>CommandManager</strong>&#0160;object. <br />However, some of them do not for the following reason (which is the same reason your command might not fire it either): no interaction has been started.</p>
<p>To start an interaction you need to create an <strong>InteractionEvents</strong> object and call its <strong>Start</strong> function at the beginning of your command and its <strong>Stop</strong> function at the end.&#0160;<br />You should also&#0160;name the interaction, which then will show up as the&#0160;<strong>CommandName</strong> parameter in the <strong>OnActivateCommand</strong>&#0160;/ <strong>OnTerminateCommand</strong> events, and as the <strong>ActiveCommand</strong> property of the&#0160;<strong>CommandManager</strong> object.</p>
<pre>// My command&#39;s OnExecute handler
void m_btnDef_OnExecute(NameValueMap Context)
{
  // Start interaction
  CommandManager cmdMgr = m_inventorApplication.CommandManager;
  InteractionEvents intEvents = cmdMgr.CreateInteractionEvents();
  intEvents.Name = m_btnDef.InternalName;
  intEvents.Start(); // OnActivateCommand fires

  // My command&#39;s implementation goes here
  // If during this time the CommandManager.ActiveCommand is accessed
  // it will retrieve the value we set to intEvents.Name  

  // Stop interaction
  intEvents.Stop(); 
} 
// OnTerminateCommand fires once interaction is stopped and
// control got back to Inventor </pre>
<p><strong>Example</strong>:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d180b737970c-pi" style="display: inline;"><img alt="ActiveCommand" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d180b737970c image-full img-responsive" src="/assets/image_33c3d4.jpg" title="ActiveCommand" /></a></p>
<p>&#0160;</p>
