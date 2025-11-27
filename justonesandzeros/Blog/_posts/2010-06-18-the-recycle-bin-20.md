---
layout: "post"
title: "The Recycle Bin 2.0"
date: "2010-06-18 15:17:59"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/06/the-recycle-bin-20.html "
typepad_basename: "the-recycle-bin-20"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p> <p>Armed with the new capabilities in the Vault 2011 APIs, I decided to re-design the <a href="http://justonesandzeros.typepad.com/blog/2009/09/the-recycle-bin.html">Recycle Bin</a> application.&#0160; The result is closer to how things work in Windows.&#0160; The Recycle Bin is now an actual folder and you delete things by putting them into the Recycle Bin. <br />Also, I bundled in the <a href="http://justonesandzeros.typepad.com/blog/2010/03/restore-recycled.html">Restore Recycled</a> app too.&#0160; </p> <p><img src="/assets/FileContextMenuScaled.png" /> </p> <p><img src="/assets/RecycleBinFolder.png" /> </p> <p><img src="/assets/RestoreCommand.png" /></p> <p><strong>Features:</strong></p> <ul>
 <li>No more permanent deleting of files.&#0160; Instead, files go into the Recycle Bin where they are backed up to another vault.</li>
 <li>Files accidently moved into the Recycle Bin can be recovered using the Restore From Recycle Bin command.</li>
 <li>The Empty Recycle Bin admin command backs up files and deletes them from the main Vault.</li>
 <li>Files can be easily recovered from the backup Vault via the Restore From Backup admin command.</li>
 </ul>
 <p><strong>Requirements:</strong></p> <ul>
 <li>Vault Workgroup 2011, Vault Collaboration 2011, or Vault Professional 2011.</li>
 </ul>
 <table border="1" cellpadding="2" cellspacing="0" width="450"><tbody> <tr> <td valign="top" width="450">NOTE:&#0160; This isn&#39;t something you can just download and run.&#0160; The Vault administrator needs to configure some settings before this feature can be used.</td> </tr> </tbody></table> <p><a href="http://justonesandzeros.typepad.com/Apps/RecycleBin/RecycleBin-2.0.2.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/RecycleBin/RecycleBin-2.0.2.0-src.zip">Click here to download the source code</a></p> <p><strong>User Setup:</strong> <br />Once the administrator has configured the system properly, the Recycle Bin is ready to be used.</p> <ol>
 <li>Run the installer and select &quot;no&quot; when asked to install the administrator controls.</li>
 <li>To add file or folder to the Recycle Bin, right-click and select &quot;Add to Recycle Bin&quot;</li>
 <li>If you accidently put something into the Recycle Bin, you can recover it.&#0160; Just select the object and run Tools -&gt; Recycle Bin -&gt; Restore from Recycle Bin from the menu.</li>
 </ol>
 <p><strong>Administrator Setup:</strong> <br />The Vault administrator must first configure the system.</p> <ol>
 <li>Run the installer and select &quot;yes&quot; when asked to install the administrator controls.</li>
 <li>Run the ADMS Console and create a new vault, which will be the location that files go when the Recycle Bin is emptied.</li>
 <li>Exit the ADMS Console.</li>
 <li>Run Vault Explorer.</li>
 <li>Create a new user which will be used to run Recycle Bin commands.&#0160; This user needs to be and administrator and have access to the main vault and the backup vault.</li>
 <li>Create a folder to be used as the Recycle Bin folder.</li>
 <li>Under the Tools menu, select Recycle Bin -&gt; Configure Recycle Bin.</li>
 <li>Fill in the necessary information and click OK.&#0160; </li>
 <li>Refresh your folder tree and you should see a lock icon next to the Recycle Bin folder.</li>
 <li>Use folder security to remove delete permissions from folders where you have sensitive information.</li>
 <li>Inform vault users that the Recycle Bin utility is required to delete files or folders.</li>
 </ol>
 <p>Some screenshots of administrator commands...</p> <p><img src="/assets/AdminControls.png" />&#0160;</p> <p> <br /><img src="/assets/RestoreFromBackup.png" /> </p> <p> <br /><img src="/assets/RestoreFromBackup2.png" /></p>
