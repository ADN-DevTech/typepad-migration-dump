---
layout: "post"
title: "Another Way to Update Properties"
date: "2015-02-25 10:07:59"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2015/02/another-way-to-update-properties.html "
typepad_basename: "another-way-to-update-properties"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>If you want to update a Vault property and that property is mapped to a value within the file, you have a couple of options, and they both suck.&#0160; This post is to introduce a third, and non-sucky, option in Vault 2015 R2.&#0160; Again, I’ll be using the <a href="http://justonesandzeros.typepad.com/blog/2015/01/2-undocumented-functions.html" target="_blank">previously undocumented functions</a> from Copy Design.</p>
<p>Sucky option 1 is to checkout and download the CAD file.&#0160; Next, use the appropriate CAD API to update the file on disk.&#0160; Lastly, check the file back in to Vault.&#0160; What makes this option so difficult is that you need to involve the CAD API.&#0160; It would be nice to do just everything from within the Vault API.</p>
<p>Sucky option 2 is to use IExplorerUtil.UpdateFileProperties in the Vault API.&#0160; On paper, this is a great function.&#0160; It loads up core pieces of Vault Explorer libraries and executes the Edit Properties command just as if a user had done it through the UI.&#0160; It even handles the checkout/checkin.&#0160; The problem is that it’s a bulky operation and prone to failure.&#0160; Vault Explorer simply wasn’t meant to be invoked from the API.&#0160;&#0160;IExplorerUtil was always indented to be a temporary solution until something better came along.&#0160; And that something better is here....</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>The new option is to use the Copy Design features.&#0160; Similar to how you can <a href="http://justonesandzeros.typepad.com/blog/2015/02/roll-your-own-copy-design.html" target="_blank">‘roll your own’ copy design</a>, you can also run a property update without ever invoking a CAD API or downloading the file.&#0160; It’s a 4 step process:</p>
<p><strong>Step 1: Check out the file</strong> <br />DocumentService.CheckoutFile(...)</p>
<p>This step reserves the file to you for editing.&#0160; It also gives you a download ticket, which you will need in the next 2 steps.&#0160; Although you are not actually downloading anything, the ticket is the currency that the FilestoreService uses.</p>
<p><strong>Step 2: Read the property definitions from the file</strong> <br />FilestoreService.GetContentSourcePropertyDefinitions(...)</p>
<p>This step interrogates the binary file and pulls out the names properties that you may be able to edit.&#0160; Unfortunately, it doesn’t tell you the current values on those properties.&#0160; Also keep in mind that this is the property as it appears in the file, which may not be the same name that it appears in Vault.&#0160; In fact, it may not be mapped to a Vault property at all.</p>
<p><strong>Step 3:&#0160; Copy the binary contents file <br /></strong>FilestoreService.CopyFile(...)</p>
<p>This step will create a new binary blob of data in the Vault filestore.&#0160; It also lets you update properties on that blob.&#0160; The ‘writeReqs’ parameter let’s you pass in new values for the properties you discovered in part 2.&#0160; Only properties you pass in are updated.&#0160; All others are left with their existing value.&#0160; <br />It’s possible that not all properties you supplied will get updated, so check the ‘writeResults’ out parameter for confirmation.</p>
<p><strong>Step 4: Check-in the file</strong> <br />DocumentService.CheckinUploadedFile(...)</p>
<p>This steps ties the binary data in the filestore with actual records in the Vault database.&#0160; Without this step, you just have a blob of data floating in limbo.&#0160; Although you copied binary data, it won&#39;t be used for creating a new File Master like with copy design.&#0160; Instead, you are creating a new version of an existing file.&#0160; From the filestore’s point of view, there is no difference between the two.&#0160; Both are just blobs of binary data.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Sample Code</strong></p>
<p>You didn’t think I would go through all this without providing any sample code, did you?&#0160; Not a chance.&#0160; I actually have an mini app that illustrates this workflow and the a simple Copy Design workflow from the previous post.&#0160; Includes C# and VB.NET source code. Enjoy.</p>
<p><img alt="" src="/assets/UpdateProps.png" /></p>
<p><img alt="" src="/assets/CopyOperation_scaled.png" /></p>
<p><a href="http://justonesandzeros.typepad.com/Files/FileCopy/FileCopy.zip">Click here to download</a></p>
<hr noshade="noshade" style="color: #d09219;" />
