---
layout: "post"
title: "Update: Oxygen Key Scope"
date: "2013-11-08 07:46:07"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2013/11/update-oxygen-key-scope.html "
typepad_basename: "update-oxygen-key-scope"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/Announcements4.png" /></p>
<p>The Oxygen team just added a feature that allows <strong>*.autodeskplm360.net</strong> as the scope on a Consumer Key.&#0160; Previously you couldn’t have a wildcard in the front, which meant your key would only work on a specific PLM 360 tenant.</p>
<p>Now you can use the <strong>*.autodeskplm360.net</strong> scope to write apps that work with all PLM 360 tenants.&#0160; Of course, you can still scope things to a specific tenant if you want.&#0160; A narrow scope is useful for things like in-house utilities and consulting projects.</p>
<p>If you already have an Oxygen key, you can get the scope changed.&#0160; Just make another <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-1F1639DF-F8A6-45EE-885F-950C29FF1DBE.htm">key request</a> and indicate that you want the scope updated on your existing key.&#0160; Make sure to include your public Consumer Key in the request.</p>
<hr noshade="noshade" style="color: #ff0000;" />
