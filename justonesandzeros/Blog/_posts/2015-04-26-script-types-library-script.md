---
layout: "post"
title: "Script Types: Library Script"
date: "2015-04-26 21:55:55"
author: "Michal Liu"
categories:
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2015/04/script-types-library-script.html "
typepad_basename: "script-types-library-script"
typepad_status: "Publish"
---

<p><span style="font-size: small;">To make scripting easier, <strong>library script</strong> type is introduced in PLM. </span>Today let&#39;s finish the Script Type topic by talking about it.</p>
<p>Library script is meant to contain utility functions that can be used in any other type of scripts just by importing it. So more reusing, less repeating. Let’s create an example of sending out email using library script:</p>
<p><strong>Step 1: Create library script</strong></p>
<p>Create a library script called EmailUtils with a function which can send email to the received recipients:</p>
<blockquote>
<p><em>function sendEmailTo(recipients) { <br />&#0160;&#0160;&#0160; var email = new Email(); <br />&#0160;&#0160;&#0160; email.subject = &quot;Email subject&quot;; <br />&#0160;&#0160;&#0160; email.body = &quot;Email body. &lt;br&gt;Supporting HTML formatting.&lt;br&gt;&quot;; <br />&#0160;&#0160;&#0160; email.to = recipients; <br />&#0160;&#0160;&#0160; email.send(); <br />}</em></p>
</blockquote>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d1064489970c-pi"><img alt="Capt1ure" border="0" height="295" src="/assets/image_b57be2.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="Capt1ure" width="475" /></a></p>
<p><strong>Step 2: Import library script</strong></p>
<p>Create an action script that imports the EmailUtils and call the “sendEmailTo” function. <strong>To import library script</strong>, just click the “plus” icon in the <strong>Imports</strong> field, and pick a library script from the list. Multiple libraries can be imported by repeating the action. Our action script “NotifyUsers” will generate a recipient list and feed it into the library function:</p>
<blockquote>
<p><em>var myRecipients = &quot;recipient1@mail.com, recipient2@mail.com&quot;; <br />sendEmailTo(myRecipients);</em></p>
</blockquote>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d1064491970c-pi"><img alt="Captur2e" border="0" height="136" src="/assets/image_6d7e63.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="Captur2e" width="478" /></a></p>
<p><strong>Step 3: Fire the action script</strong></p>
<p>Fire the action script “NotifyUsers” as an on-create, on-edit, on-demand, workflow or scheduled script. Your choice!</p>
<p>The sent email will seems like this.</p>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01bb0820c250970d-pi"><img alt="Capture" border="0" height="193" src="/assets/image_582a85.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="Capture" width="491" /></a></p>
<p>-- Michal</p>
