---
layout: "post"
title: "The Folder Property"
date: "2010-02-11 08:08:06"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/02/the-folder-property.html "
typepad_basename: "the-folder-property"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p><strong>Update:</strong><br /><a href="http://justonesandzeros.typepad.com/blog/2011/06/the-folder-property-2012.html" target="_self">The Vault 2012 version can be found here</a>.</p>
<p><strong>Overview:   <br /></strong>A utility that sets the Vault path as a file property.</p>
<p><img alt="" src="/assets/FolderProperty.png" /></p>
<p><strong>Requirements:</strong> <br />Vault Workgroup/Collaboration/Manufacturing 2010</p>
<p><a href="http://justonesandzeros.typepad.com/Apps/FolderProperty/FolderProperty-bin.zip">Click here to get the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/FolderProperty/FolderProperty-src.zip">Click here to get the source code</a></p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
<p><strong>Introduction:</strong> <br />I&#39;ve had a few requests for this one.&#0160; Since it was a pretty cool idea and not too hard to write, I decided to go for it.&#0160; Don&#39;t ever say that I never did anything for you.</p>
<p>Under the hood, this is just a re-write of <a href="http://justonesandzeros.typepad.com/blog/2010/01/vault-mirror.html">Vault Mirror</a>.&#0160; Just like with Vault Mirror, this utility can be run from the task manager.&#0160; And this utility also has 2 ways of running.&#0160; There is a &quot;quick mode&quot; that only works on new files and a &quot;slow mode&quot; that scans every file in the Vault.&#0160; The recommendation is to create a Windows scheduled task that runs every 10 minutes or so to keep things mostly in sync.</p>
<p><img alt="" src="/assets/FolderProperty2.png" /></p>
<p>The Folder Property is not as reliable as Vault Mirror.&#0160; Because locked files can&#39;t be edited, you will find that several files won&#39;t have the property set.&#0160; Another difference is that The Folder Property edits data, so it consumes a license while it is running.&#0160; To mitigate the impact, the utility only consumes a license during the actual file editing.&#0160; When scanning for files, no license is consumed.&#0160; Once all the edits are made, the utility frees up the license.</p>
<p>See the readme file for additional instructions and command line parameters.</p>
<p>Enjoy!</p>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
<p>&#0160;</p>
