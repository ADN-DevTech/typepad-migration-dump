---
layout: "post"
title: "Watch Folder"
date: "2010-05-07 10:59:41"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/05/watch-folder.html "
typepad_basename: "watch-folder"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p> <p>Wouldn&#39;t it be nice if Vault could email you when a file was added or changed?&#0160; Wouldn&#39;t it be even nicer if somebody were to program that feature and post it on his blog?&#0160; If the answer is yes and yes, then go out and buy a lottery ticket because today is your lucky day.</p> <p><img src="/assets/preview.png" /> </p> <p><strong>Overview:</strong></p> <p>Watch Folder is a program that lets you set &quot;watches&quot; on Vault folders that you want to monitor.&#0160; Whenever a file is added or checked-in to one of your watch folders, you get an email.&#0160; You have the option of getting immediate emails or periodic summary emails.</p> <p><img src="/assets/menuCommand.png" /> </p> <p><img src="/assets/dialog.png" /> </p> <p><strong>Requirements:</strong></p> <ul>
 <li>Vault Workgroup/Collaboration/Professional 2011</li>
 <li>Job Server enabled</li>
 <li>An SMTP server somewhere on the network</li>
 </ul>
 <table border="1" cellpadding="2" cellspacing="0" width="450"><tbody> <tr> <td valign="top" width="450">NOTE:&#0160; This isn&#39;t something you can just download and run.&#0160; The Vault administrator needs to set up some server components before this feature can be used.</td> </tr> </tbody></table> <p><a href="http://justonesandzeros.typepad.com/Apps/WatchFolder/WatchFolder-1.0.2.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/WatchFolder/WatchFolder-1.0.2.0-admin.zip">Click here to download the server components</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/WatchFolder/WatchFolder-1.0.2.0-src.zip">Click here to download the source code</a></p> <p><strong>To use</strong></p> <p>For Vault users, download the application and run the installer.&#0160; Just make sure that the administrator has set up the proper server components first.</p> <p>For Vault administrators, download the server components and follow the instructions in the readme file.</p> <p><strong>Notes:</strong></p> <ul>
 <li>A watch on a folder will automatically apply to any sub-folders.</li>
 <li>Removing a watch will also remove watches on sub-folders.</li>
 <li>You can&#39;t have an immediate watch and a summary watch on the same folder.&#0160; Setting a folder to one type overwrites the existing setting.</li>
 <li>You need to be at least Document Editor (level 1) in order to set up a watch on a folder.</li>
 <li>You need to have a valid email address in your Vault user settings.</li>
 <li>The contents of the email can be customized by the Vault administrator.&#0160; See readme file.</li>
 </ul>
