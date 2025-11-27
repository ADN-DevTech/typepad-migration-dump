---
layout: "post"
title: "Set Revision iProperties"
date: "2010-03-26 07:31:00"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/set-revision-iproperties.html "
typepad_basename: "set-revision-iproperties"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p> <p>I know that most people don&#39;t have Vault 2011 yet, but the new API&#39;s are just too cool for me <strong>not</strong> to use.&#0160; So I present to you my first sample app for Vault 2011.</p> <p><strong>Overview:</strong>   <br />Set Revision iProperties takes the revision history of a file and sets them as custom iProperties on an IDW or Inventor DWG file.</p> <p>  <br /><img alt="" src="/assets/MenuCommand.png" style="border: 1px solid black;" />   <br /><em>Select the drawing and run the command from Vault Explorer</em></p> <em></em> <p>  <br /><img src="/assets/iProperties.png" />&#0160; <br /><em>Custom iProperties are added to the Inventor file</em></p> <p><strong>Requirements:</strong>   <br />Vault Workgroup/Collaboration/Professional 2011   <br />Inventor 2011</p> <p><a href="http://justonesandzeros.typepad.com/Apps/SetRevisionIProperties/SetRevisionIProperties-1.0.1.0-bin.zip">Click here to download the application</a>   <br /><a href="http://justonesandzeros.typepad.com/Apps/SetRevisionIProperties/SetRevisionIProperties-1.0.1.0-src.zip">Click here to download the source</a></p> <p><strong>Installation:</strong>   <br />This utility is a command extension to Vault Explorer, and there are a few steps you need to go through to deploy.</p> <ol>
  <li>Locate the Vault client install folder.&#0160; (ex. C:\Program Files (x86)\Autodesk\Vault Professional 2011) </li>
  <li>Navigate to the Explorer sub folder. </li>
  <li>Create an &quot;Extensions&quot; sub folder. </li>
  <li>Create a &quot;SetRevisionProperties&quot; sub folder under Extensions. </li>
  <li>Place all the files from the ZIP in the SetRevisionProperties folder. </li>
  <li>Start up Vault Explorer and go to a folder.&#0160; You should see the &quot;Set Revision iProperties&quot; command under the Actions menu.   <br /><img src="/assets/deployment.png" /> </li>
  <li>If the command is not in the Actions menu, you may need to place it yourself using the Customize feature of the menu bar.&#0160; Right click and select Customize.&#0160; Then go to the Commands tab and select the Tools category.   <br /><img src="/assets/CustomizeMenu.png" /> </li>
 </ol>
 <p>Enjoy.</p>
