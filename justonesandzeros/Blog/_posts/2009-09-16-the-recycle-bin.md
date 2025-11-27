---
layout: "post"
title: "The Recycle Bin"
date: "2009-09-16 07:44:04"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2009/09/the-recycle-bin.html "
typepad_basename: "the-recycle-bin"
typepad_status: "Publish"
---

<strong>
  <p class="asset asset-image"><img src="/assets/SampleApp2.png" /> </p>
The Recycle Bin</strong>

<br />Let’s start things off with a bang.&#160; Here is a fully working utility with full source code.&#160; Enjoy.

<br />

<br />

<p class="asset asset-image"><a href="http://justonesandzeros.typepad.com/images/2009/RecycleBin/screenshot1.png"><img style="display: inline; margin-left: 0px; margin-right: 0px" src="/assets/screenshot1_scaled.png" /></a> 

  <br /><font color="#808080">(click image for larger view)</font></p>

<br /><strong>Overview:</strong>

<br />The Recycle Bin prevents users from deleting files directly.&#160; Instead of using the Delete command, they can send files to the Recycle Bin state.&#160; On the weekend, a utility backs up all the files in the Recycle Bin and deletes them from the Vault.

<br />

<br /><strong>Requirements:</strong>

<br /><em>Vault Workgroup 2010 </em>or <em>Vault Collaboration 2010</em> or <em>Vault Manufacturing 2010</em>

<br />

<br /><strong>Introduction:</strong>

<br />This is what I will refer to as a “high touch” utility.&#160; What I mean by high touch is that the utility doesn’t work entirely on its own.&#160; It will only work if the Vault is configured in a special way.&#160; In this case, we need to add a Recycle Bin state to the lifecycle definition.&#160; We also need to create a specialized user.

<br />

<br /><a title="Recycle Bin Application" href="http://justonesandzeros.typepad.com/Apps/RecycleBin/RecycleBin-1.0.1.0-bin.zip">Click here to download the application</a>

<br /><a title="Recycle Bin Source Code" href="http://justonesandzeros.typepad.com/Apps/RecycleBin/RecycleBin-1.0.1.0-source.zip">Click here to download the source code</a> 

<br />

<br /><strong>Configuring the Vault:</strong>

<br />First let’s create a Vault to put all our backup files:

<br />

<ol>
  <li>Open the Autodesk Data Management Server Console </li>

  <li>Select the Vaults entry in the left tree </li>

  <li>Select Actions -&gt; Create from the menu </li>

  <li>Create a new Vault with the name BackupVault </li>

  <li>Exit the console </li>
</ol>
<strong>Next let’s create the specialized user:</strong> 

<ol>
  <li>Log into Vault as an administrator </li>

  <li>In the menu, select Tools -&gt; Administration </li>

  <li>Select the Users tab </li>

  <li>Click on the Users button </li>

  <li>Click on the New User button </li>

  <li>Fill in “AppRecycleBin” as the UserName, choose a password, and set the role as “Administrator”, “Document Editor (Level 2)” and “Document Manager (Level 2)”.&#160; Also, give access to the main Vault and the backup Vault.
    <br />

    <p class="asset asset-image"><img src="/assets/New-User_scaled.png" /> </p>
  </li>

  <li>Click OK to create the user. </li>

  <li>[Optional] Create a Group called Automations and add AppRecycleBin to that group.&#160; In the future, any other application specific logins can go in this group. </li>

  <li>Close all open windows by clicking OK. </li>
</ol>

<br />Now let’s configure the lifecycle states.&#160; For this example I will use the Flexible Release Process.

<br />

<ol>
  <li>In the menu, select Tools -&gt; Administration </li>

  <li>Select the Behaviors tab </li>

  <li>Click on the Lifecycles button </li>

  <li>Select the Flexible Release Process and click on the Edit button </li>

  <li>Click on the Add button in the Lifecycle Details section </li>

  <li>Create a new state called Recycle Bin
    <br />

    <p class="asset asset-image"><img src="/assets/New_Lifecycle_State.png" /> </p>
  </li>

  <li>Click OK to go back to the Lifecycle Definition dialog.&#160; You should now see the Recycle Bin state in blue </li>

  <li>Click Apply and the Recycle Bin state should turn black </li>

  <li>Click on the Recycle Bin state and select the Transitions tab
    <br />

    <p class="asset asset-image"><img src="https://justonesandzeros.typepad.com/images/2009/RecycleBin/lifecycle_definition 1_scaled.png" /> </p>
  </li>

  <li>Select Recycle Bin -&gt; Released then click the Edit button </li>

  <li>Select the Security tab </li>

  <li>Uncheck the No Restrictions checkbox </li>

  <li>Click the Add button and select Everyone </li>

  <li>Give Everyone the Deny permission </li>

  <li>Click OK 
    <p class="asset asset-image"><img src="/assets/transition_1_scaled.png" /> </p>
  </li>

  <li>Repeat steps 10-15 for states Recycle Bin -&gt; Quick-Change, Released -&gt; Recycle Bin, and Quick-Change -&gt; Recycle Bin </li>

  <li>In the Lifecycle Definition dialog, select the Security tab for the Recycle Bin state </li>

  <li>Uncheck the No State Based Security checkbox </li>

  <li>Add an entry for Everyone and set the permission to Allow Read. </li>

  <li>Add an entry for AppRecycleBin and set the permission to Allow Read, Write and Delete.
    <br />

    <p class="asset asset-image"><img src="/assets/lifecycle_definition_2_scaled.png" /> </p>
  </li>

  <li>Update the security on the Work In Progress and For Review states so that Everyone has a Deny Delete setting and Allow for Read and Modify.
    <br />

    <p class="asset asset-image"><img src="/assets/lifecycle_definition_3_scaled.png" /> </p>
  </li>

  <li>Exit all dialogs by clicking OK. </li>
</ol>
Now we have our Vault configured properly.&#160; The only way to delete a file using the Flexible Release Process is to put it in the Recycle Bin and let the Recycle Bin utility delete it.

<br />

<br />

<br /><strong>Configuring the Recycle Bin Utility:</strong>

<br />

<ol>
  <li>Download and unzip the utility </li>

  <li>Create a file called Run.bat in the same directory ast RecycleBinUtil </li>

  <li>Edit Run.bat in a text editor and write out the RecycleBinUtil command.&#160; See the readme.txt file for a description of the parameters.
    <br />Example:&#160; <font color="#990000">RecycleBinUtil -U AppRecycleBin -P pass -S novdrwxp -V Vault -BV BackupVault -L out.txt</font> </li>

  <li>[Optional] Run the bat file to make sure things work </li>

  <li>Go to the Windows Control Panel and select Scheduled Tasks </li>

  <li>Right click and select New-&gt;Scheduled Task </li>

  <li>Name the scheduled task.&#160; Ex. Empty Vault Recycle Bin </li>

  <li>Right click the task and select Properties </li>

  <li>Go to the Task tab </li>

  <li>Set the Run parameter to the Run.bat file.&#160;&#160; Set the Start In parameter to be the directory that run.bat lives in. 
    <p class="asset asset-image"><img src="/assets/TaskSchedule1.png" /> </p>
  </li>

  <li>Go to the Schedule tab </li>

  <li>Set the task to run once a week at 1:00 AM on Sunday, or whenever you want.
    <br />

    <p class="asset asset-image"><img src="/assets/TaskSchedule2.png" /> </p>
  </li>

  <li>Click OK and set login info if needed. </li>
</ol>
<strong>
  <br />Disclaimer:</strong>

<br />This application is experimental in nature. It has not been tested in any manner, and Autodesk does not represent that it is reliable, accurate, complete, or otherwise valid. Accordingly, this application is provided “as is” with no warranty of any kind and you use the application at your own risk.
