---
layout: "post"
title: "Roll Your Own Copy Design"
date: "2015-02-11 16:24:15"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2015/02/roll-your-own-copy-design.html "
typepad_basename: "roll-your-own-copy-design"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p><a href="http://justonesandzeros.typepad.com/blog/2015/01/2-undocumented-functions.html">Previously</a>, I documented two new functions related to the new Copy Design app in Vault 2015 R2.&#0160; Now I would like to explore some uses for those functions.&#0160; The most obvious usage is to build your own Copy Design algorithm that meets your specific needs.</p>
<p>Technically, a custom Copy Design as always possible in Vault, but these new functions make things easier.&#0160; Most significantly, your app does not have to engage in any CAD-specific APIs.&#0160; Assuming, of course, the CAD file is one of the types that Vault recognizes.&#0160;</p>
<p>I said that the new functions make a custom Copy Design easi<em><strong>er</strong></em>, but it’s still not something I would describe as simple.&#0160; You still have to manage things like file relationships, and file naming.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>For each file you want to copy, you basically have to do three steps:</p>
<p><strong>Step 1:&#0160; Get a download ticket. <br /></strong>DocumentService.GetDownloadTicketsByFileIds(...)</p>
<p>You are not actually downloading anything, but let’s not worry about that for the moment.&#0160; The important thing is that we are swapping the File Id for ticket, and the ticket is required in step 2.&#0160;</p>
<p><strong>Step 2:&#0160; Copy the binary file contents.</strong> <br />FilestoreService.CopyFile(...)</p>
<p>This step will do a server-side copy of the file bits.&#0160; Nothing is actually download or uploaded from your computer.&#0160; The operation just does a copy of the <em>binary data</em>.&#0160; The <em>meta data</em> still has not been copied yet, so it doesn’t really exist in Vault.&#0160; It has no file name, no folder location, no create time, and so on.&#0160; It’s basically a set of data floating in limbo.</p>
<p><strong>Step 3:&#0160; Add the file to Vault</strong> <br />DocumentService.AddUploadedFile(...)</p>
<p>This step is where you tell the Vault database about the file.&#0160; It’s here that you set the new name, new location, and new relationships for the copied file.&#0160; Again, the function name isn’t technically correct.&#0160; We didn’t upload the file, but this function doesn’t care.&#0160; It just needs an uploadTicket so that it can cement the binary data to the meta data.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>Another key aspect of the copy is that the internal file relationships have not yet been modified.&#0160; Step 2 is a perfect copy, so it still has pointers to old file names.&#0160; Vault is designed so that <strong>file relationships are updated when the file is downloaded</strong>.&#0160; This is another reason you want to use AcquireFiles when downloading.&#0160; It fixes up the references for your local files.</p>
<p>The new copy design app works the same way.&#0160; It relies on download to fix up and broken references.&#0160; Those references will technically stay broken until an engineer checks out the files, makes changes and checks the files back in.</p>
<hr noshade="noshade" style="color: #d09219;" />
