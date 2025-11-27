---
layout: "post"
title: "Q-Tools and Watch Folder 2013"
date: "2012-05-30 20:17:05"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2012/05/q-tools-and-watch-folder-2013.html "
typepad_basename: "q-tools-and-watch-folder-2013"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p>For this post, I bring you <strong>two whole apps</strong>.&#0160; I’ve updated Q-Tools and Watch Folder for Vault 2013.&#0160; Technically the 2012 versions can still work with Vault 2013 server, but there were a few optimizations I wanted to do using the new API features.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Q-Tools 2013</strong></p>
<p>For Q-Tools, the feature set is the same as before, but I make use of the new Job.IsOnSite parameter to determine if a job needs to be rerouted or not.</p>
<p>Here is the full feature list:</p>
<ul>
<li><strong>Logging</strong> - Issues are logged to a text file. </li>
<li><strong>Stale Job Detection</strong> - Jobs on the queue too long can be considered an issue. </li>
<li><strong>Email Reports</strong> - Issues can be emailed to one or more recipients. </li>
<li><strong>Queue Cleanup</strong> - Jobs with issues can be automatically removed from the queue. </li>
<li><strong>Queue Forwarding</strong> - In multi-site and multi-workgroup environments, jobs can get routed to a single site. </li>
</ul>
<p><img alt="" src="/assets/email.png" /></p>
<p><strong>Requirements:      <br /></strong>- Vault Workgroup/Collaboration/Professional 2013     <br />- Job Server feature enabled     <br /><a href="http://justonesandzeros.typepad.com/Apps/QTools/Q-Tools.3.0.1.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/QTools/Q-Tools.3.0.1.0-src.zip">Click here to download the source code</a></p>
<p><a href="http://justonesandzeros.typepad.com/blog/2011/07/q-2ools.html" target="_self">Click here for Q-Tools 2012</a></p>
<p><strong>To setup:      <br /></strong></p>
<ul>
<li>Designate a computer on the network that the Q-Tools service should run on.&#0160; Only 1 Q-Tools can be running for a given Vault deployment. </li>
<li>Run the installer. </li>
<li>Go to the installation folder and update the settings.xml.&#0160; The XML comments will explain this step. </li>
<li>Run the Q-Tools service. </li>
</ul>
<p><img alt="" src="/assets/Services-scaled.png" /></p>
<p><strong>Migrating from Q-Tools 2012:</strong></p>
<ul>
<li>Actually I don&#39;t recommend migrating.&#0160; The 2012 Q-Tools version should work fine with the Vault 2013 server.&#0160; </li>
<li>If you still want to migrate, it should be fairly easy.&#0160; The Settings.xml has not changed.&#0160; I recommend making a copy of the XML file, uninstall Q-Tools 2012, then install Q-Tools 2013.</li>
</ul>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Watch Folder 2013</strong></p>
<p>Although the feature set is the same, the setup is much easier.&#0160; No longer do you have to configure Windows tasks to add notification jobs to the queue.&#0160; Using the new features on IJobHander, the jobs are added from JobProcessor plug in.&#0160; Right before JobProcessor goes idle, Watch Folder puts a new notification job on the queue.&#0160; That way new files are checked every time JobProcessor cycles.</p>
<p>In case you aren&#39;t familiar with it, Watch Folder is an app that allows you to &quot;watch&quot; a set of Vault folders so that you receive email notifications when files get added or updated in those folders.</p>
<p><img alt="" src="/assets/Inbox.png" /></p>
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault Workgroup/Collaboration/Professional 2013</li>
<li>Job Server enabled </li>
<li>An SMTP server somewhere on the network </li>
</ul>
<p>NOTE:&#0160; This isn&#39;t something you can just download and run.&#0160; The Vault administrator needs to set up some server components before this feature can be used.</p>
<p><a href="http://justonesandzeros.typepad.com/Apps/WatchFolder/WatchFolder-3.0.3.0-bin.zip" target="_self">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/WatchFolder/WatchFolder-3.0.3.0-src.zip" target="_self">Click here to download the source code</a></p>
<p><a href="http://justonesandzeros.typepad.com/blog/2011/11/watch-folder-2012.html" target="_self">Click here for Watch Folder 2012</a></p>
<p><strong>To use</strong></p>
<p>For Vault users, download the application and run the installer.&#0160; Just make sure that the administrator has set up the Vault environment first.&#0160; Next, start up Vault Explorer and right-click on a folder that you want to watch.&#0160; Select the Watch Folder command and decide how frequently you want notifications.</p>
<p><img alt="" src="/assets/menuCommand.png" /></p>
<p><img alt="" src="/assets/dialog.png" /></p>
<p>For Vault administrators, download the application and run the installer.&#0160; Next follow the instructions in the readme file in the install folder (%ProgramData%\Autodesk\Vault 2013\Extensions\WatchFolder).</p>
<p><strong>Notes:</strong></p>
<ul>
<li>A watch on a folder will automatically apply to any sub-folders. </li>
<li>Removing a watch will also remove watches on sub-folders. </li>
<li>You can&#39;t have an immediate watch and a summary watch on the same folder.&#0160; Setting a folder to one type overwrites the existing setting. </li>
<li>You need to be at least Document Editor (level 1) in order to set up a watch on a folder. </li>
<li>You need to have a valid email address in your Vault user settings. </li>
<li>The contents of the email can be customized by the Vault administrator.&#0160; See readme file. </li>
</ul>
<p><strong>Migrating from Watch Folder 2012:</strong></p>
<ul>
<li>WatchSettings.xml now needs a Vault username and password.&#0160; Everything else is the same, so you can just add &lt;Vault_Username&gt; and &lt;Vault_Password&gt; tags to your existing config file.&#0160; The Vault user must be able to add and delete jobs from the queue.</li>
<li>All the other settings files from R1 can still be used, just copy over WatchCollection.xml and WatchEmailBody over to the install folder. </li>
</ul>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><span style="font-size: xx-small;"><img alt="" src="/assets/SampleApp3-1.png" /></span></p>
