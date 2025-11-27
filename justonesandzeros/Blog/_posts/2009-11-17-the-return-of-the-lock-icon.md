---
layout: "post"
title: "The Return of the Lock Icon"
date: "2009-11-17 14:56:33"
author: "Doug Redmond"
categories:
  - "Announcements"
original_url: "https://justonesandzeros.typepad.com/blog/2009/11/the-return-of-the-lock-icon.html "
typepad_basename: "the-return-of-the-lock-icon"
typepad_status: "Publish"
---

<p><img src="/assets/Announcements2.png" /> </p> <p><img align="left" src="/assets/lock_large.png" style="display: inline; margin-left: 0px; margin-right: 0px;" /> The lock icon has returned.&#0160; All hail the lock icon!!!</p> <p>We have just released <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=14161879&amp;linkID=9261341">Vault 2010 Update 1</a>.&#0160; It contains several improvements and fixes, but I want to focus on the lock icon for this post.&#0160; The meaning has changed a bit since Vault 2009, and I want to make sure that developers understand the change. <img src="/assets/lock_small.png" /> </p> <p>In Vault 2009, the lock icon meant that the file was in the locked state.&#0160; It was basically a bit on the File object.&#0160; A File would either be locked or not locked, and there were API functions to change the locked state.&#0160; The locked state could be read from the Locked property of the File object.<img src="/assets/lock_small.png" /> </p> <p>In Vault 2010, we decided to remove the locked state of a file in favor of ACL permissions.&#0160; The lock icon was removed from the Vault client.&#0160; At the API level, File.Locked still existed but it always returned false.<img src="/assets/lock_small.png" /> </p> <p>Now with <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=14161879&amp;linkID=9261341">Vault 2010 Update 1</a>, we added the lock icon back to the client.&#0160; However the meaning is different than before.&#0160; A lock icon now means that the current user has read only permissions based on the ACL of the file.&#0160; So the concept of a locked file is still gone in favor of ACL permissions.&#0160; At the API level, File.Locked will now return true if the user does not have write permissions.&#0160; Otherwise false is returned.<img src="/assets/lock_small.png" /> </p> <p>Long live the lock icon! <img src="/assets/lock_small.png" /></p>
