---
layout: "post"
title: "Recycle Bin v1.1"
date: "2009-12-09 08:35:34"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2009/12/recycle-bin-v11.html "
typepad_basename: "recycle-bin-v11"
typepad_status: "Publish"
---

<strong>  <p class="asset asset-image"><img src="/assets/SampleApp2.png" /> </p> </strong> <p>I&#39;m going to do something a bit different this month.&#0160; Instead of creating a new sample application, I am going to update an existing one.&#0160; The result is an improved <a href="http://justonesandzeros.typepad.com/blog/2009/09/the-recycle-bin.html">Recycle Bin</a> application.</p> <p><a href="http://justonesandzeros.typepad.com/images/2009/RecycleBin/screenshot2.png"><img border="0" src="/assets/screenshot2_scaled.png" style="border: 0px none ;" /></a>   <br /><font color="#808080">(click image for full view)</font></p> <p><a href="http://justonesandzeros.typepad.com/Apps/RecycleBin/RecycleBin-1.1.1.0-bin.zip">Click here to download the application</a>  <br /><a href="http://justonesandzeros.typepad.com/Apps/RecycleBin/RecycleBin-1.1.1.0-source.zip">Click here to download the source</a></p> <p><strong>What&#39;s new</strong></p> <ul>
  <li>Multiple versions of a file can be backed-up when the recycle bin is emptied.&#0160; Previously only the latest version would get backed-up.</li>
  <li>The metadata for each version is stored.&#0160; This includes:</li>
  <ul>
   <li>Folder information</li>
   <li>Property information</li>
   <li>Revision information</li>
   <li>Lifecycle state information</li>
   <li>Parent and child file associations</li>
   <li>BOM information</li>
  </ul>
  <li>A new command line option, –O, which controls which versions get backed up.</li>
 </ul>
 <p><strong>Installing for the first time</strong></p> <p>Download version 1.1 from this page, then follow the instructions from the <a href="http://justonesandzeros.typepad.com/blog/2009/09/the-recycle-bin.html">original blog post</a>.</p> <p><strong>Upgrading from version 1.0</strong></p> <p>Download version 1.1 from this page, then overwrite the existing RecycleBinUtil.exe.&#0160; Add the –O option to your scheduled task command if needed (see readme).</p>
