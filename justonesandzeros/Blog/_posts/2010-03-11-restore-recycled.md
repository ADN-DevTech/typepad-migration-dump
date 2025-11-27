---
layout: "post"
title: "Restore Recycled"
date: "2010-03-11 08:34:33"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/restore-recycled.html "
typepad_basename: "restore-recycled"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p> <p><strong>Introduction:</strong>   <br />6 Months ago I posted a utility called <a href="http://justonesandzeros.typepad.com/blog/2009/12/recycle-bin-v11.html">The Recycle Bin</a>.&#0160; So by now, it&#39;s being used by millions of people worldwide, right?&#0160; <br />Maybe not.&#0160; The real number is probably somewhere around 0 or less.&#0160; But that&#39;s not going to stop me from writing a companion utility.&#0160; I&#39;m kind of dumb that way.</p> <p><strong>Overview:</strong>   <br />Restore Recycled will restore a file that was removed via The Recycle Bin.&#0160; If you recall, when you empty The Recycle Bin the file isn&#39;t actually deleted, instead it&#39;s moved to a backup Vault.&#0160; Restore Recycled allows you to bring the file back to the original Vault. </p> <p><img src="/assets/screenshot1.png" />   <br /><em>Search the backup vault for the file you want to restore.<br /><br /></em></p> <p><img src="/assets/screenshot2.png" />&#0160; <br /><em>Restore the file and configure the appropriate settings.<br /><br /></em></p> <p><strong>Requirements:</strong>   <br />Vault Workgroup/Collaboration/Manufacturing 2010   <br />The Recycle Bin</p> <p><a href="http://justonesandzeros.typepad.com/Apps/RestoreRecycled/RestoreRecycled-1.0.1.0-bin.zip">Click here to get the application</a>  <br /><a href="http://justonesandzeros.typepad.com/Apps/RestoreRecycled/RestoreRecycled-1.0.1.0-src.zip">Click here to get the source code</a></p> <p><strong>How to use:</strong></p> <ol>
  <li>Run Restore Recycled. </li>
  <li>Provide the necessary login information.&#0160; The &quot;backup database&quot; is the Vault where we are restoring files from.&#0160; The &quot;target database&quot; is the Vault where we are restoring files to. </li>
  <li>Type in the file name of the file you want to restore.&#0160; Wildcard characters are supported.</li>
  <li>Browse the list of files to find the correct file and version you want to restore.&#0160; If the file was backed up with Recycle Bin 1.1, the version name will be encoded in the file name (ex. Blower.3.iam is really the 3rd version of Blower.iam).</li>
  <li>Click the Restore button.</li>
  <li>In the restore dialog, set the relevant information, such as the folder location and category.&#0160; If meta-data is available, the utility will pre-populate this information, but it can still be edited if needed.</li>
  <li>Click OK and wait for the operation to complete.</li>
 </ol>
 <p><strong>Important notes:</strong></p> <ul>
  <li>Restore Recycled doesn&#39;t fix up file dependencies or BOM data.&#0160; If you are restoring a CAD file, I recommend checking out and checking back in through the CAD application.&#0160; This should repair the file linkages and metadata. </li>
  <li>When a file is restored, it is not deleted from the backup database. </li>
  <li>Based on how the Vault is set up, it may not be possible to restore the file in the exact way intended.&#0160; </li>
  <ul>
   <li>For example, the lifecycle state might not get set properly because there is no allowed transition from the default state to the intended state.&#0160; </li>
   <li>In other cases, automatic operations, such as increasing the revision when the file moves to Released, might cause the restored file to have incorrect data.    </li>
  </ul>
 </ul>
