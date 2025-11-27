---
layout: "post"
title: "The TTP Project - Update 1"
date: "2014-03-14 13:24:46"
author: "Doug Redmond"
categories:
  - "PLM 360"
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2014/03/the-ttp-project-update-1.html "
typepad_basename: "the-ttp-project-update-1"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/SampleApp4.png" /></p>
<p>Here is the first major update to The TTP Project.&#0160; The initial release was focused on proving out a prototype SDK for the PLM 360 API.&#0160; This time around I focused on making the app itself more useful.&#0160; There are some nice new features here, including things you can’t do through the web UI.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Custom UI</strong></p>
<p><a href="http://justonesandzeros.typepad.com/images/2014/TTP-update1/screenshot.png" target="_blank"><img alt="" src="/assets/screenshot_scaled.png" /></a> <br /><span style="color: #666666; font-size: xx-small;">(click for full size image)</span></p>
<p>You can now build your own UI for item detail fields.&#0160; This is done using XAML files to define the UI.&#0160; Next, you link those files to the workspaces and operations you want to apply them too.&#0160; If you are familiar with Vault Data Standard or <a href="http://justonesandzeros.typepad.com/blog/2012/07/deco-custom-ui-without-custom-code.html" target="_blank">DECO</a>, then you get the idea.&#0160; In fact, most of the code is copied from DECO.</p>
<p>Custom UI is specific to a workspace.&#0160; You can also define different UI for each operation (view, new and edit).&#0160; The Layout Settings window that allows you to configure which UI is used in which cases.&#0160; If no UI is defined, then the default view is used.</p>
<p><img alt="" src="/assets/layoutSettings_scaled.png" /></p>
<p><span style="color: #666666; font-size: xx-small;">(And yes, I notice that I forgot to change the dialog title)</span></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Multi-select and multi-edit</strong></p>
<p>You can now select multiple items in a workspace.&#0160; The view panel will show all the fields that have the same value across all the selections.&#0160; If a field is different, it shows a blank value.</p>
<p><img alt="" src="/assets/multiSelect.png" /></p>
<p>If you run the edit command during a multi-select, any field you set will be applied to all the selected items.&#0160; All other field values are left alone.&#0160; So it’s a nice easy way to set a value across multiple items.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Other goodies</strong></p>
<ul>
<li>Support for picklists.</li>
<li>Auto-renewal for expired security credentials.&#0160; If you liked my earlier <a href="http://justonesandzeros.typepad.com/blog/2014/02/user-less-login-to-plm-360.html" target="_blank">OAuth article</a>, you can grab the TTP source code for examples.</li>
<li>Quicker navigation of items.&#0160; Caching has been implemented in the code so viewing an item for the second time doesn’t involve a web call.</li>
</ul>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Requirements:</strong></p>
<ul>
<li>PLM 360</li>
<li>Windows OS</li>
<li>.NET 4.5</li>
<li>For creating custom UI: A XAML editor such as Visual Studio or Expression Blend</li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/TTP/TheTTPProject-1.0.2.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/TTP/TheTTPProject-1.0.2.0-src.zip">Click here to download the source code</a></p>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies.</span></p>
<hr noshade="noshade" style="color: #013181;" />
