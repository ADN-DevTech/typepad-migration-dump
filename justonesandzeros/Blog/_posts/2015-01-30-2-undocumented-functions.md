---
layout: "post"
title: "2 Undocumented Functions"
date: "2015-01-30 13:57:15"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2015/01/2-undocumented-functions.html "
typepad_basename: "2-undocumented-functions"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>There are some undocumented functions in Vault 2015 R2 I’d like to tell you about.&#0160; Of course, the process of me telling you about them essentially makes them documented.&#0160; So by the time you get to the bottom of this article, the title becomes untrue.&#0160;</p>
<p>Yes, I did get permission from the team before going public with this information.&#0160; So feel free to use the knowledge that I am about to impart upon you as long as you are in Vault 2015 R2.&#0160; In fact, I’ll be going into more details in the upcoming posts on how you can use these functions to do cool things.&#0160; But first, some background.</p>
<p>You know that <a href="http://help.autodesk.com/view/VAULT/2015/ENU/?guid=GUID-E4C6B707-448D-467E-B7FD-00625A98B791">new Copy Design</a> app in 2015 R2?&#0160; It’s more than just a UI redesign.&#0160; There are some server side enhancements too.&#0160; Specifically the copy happens entirely on the server side.&#0160; Previously, the client would download all the files, copy them, modify the copies and upload them as new files.&#0160; The new mechanism saves network bandwidth and reduces the load on the client.&#0160; But in order to develop the new Copy Design client, some new server APIs needed to be made...</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>CopyFile</strong></p>
<p>This function copies the binary contents of a file in the filestore.&#0160; The Vault meta data, such as Name and Version Number, are <strong>not</strong> copied.</p>
<p><em>byte[] FilestoreService.CopyFile ( <br />&#0160;&#0160;&#0160; byte[] downloadTicket, <br />&#0160;&#0160;&#0160; bool allowSync, <br />&#0160;&#0160;&#0160; PropWriteReq[] writeReqs,&#0160; <br />&#0160;&#0160;&#0160; out PropWriteResults writeResults)</em></p>
<p><strong>downloadTicket</strong> – The identifier to the file you want to copy. The FilestoreService doesn’t care about File.Id values. Download/upload tickets are the identifiers here.&#0160; Call DocumentService.GetDownloadTicketsByFileIds to get a download ticket.</p>
<p><strong>allowSync</strong> – If true and the file is not on the local filestore, the file is copied as part of the operation. If false, the operation fails if the file is not in the local filestore.</p>
<p><strong>writeReqs</strong> – A set of properties to update within the file. For example, you can update Inventor iProperties in the copied file.</p>
<p><strong>writeResults</strong> – The result of the property updates.&#0160; Just because you <em>requested</em> a property change, doesn’t mead the change actually happened.</p>
<p><strong>Return value</strong> – The upload ticket for the new file.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>GetContentSourcePropertyDefinitions</strong></p>
<p>This function gets you the list of property names contained within the file.&#0160; The CAD file is the <em>source</em> of the <em>content</em>.&#0160; This type of property is different than Vault properties, which are stored in the Vault database.&#0160;</p>
<p><em>CtntSrcPropDef[] FilestoreService.GetContentSourcePropertyDefinitions ( <br />&#0160;&#0160;&#0160; byte[] downloadTicket, <br />&#0160;&#0160;&#0160; bool allowSync)</em></p>
<p><strong>downloadTicket</strong> – The identifier for the file to read property names from. Call DocumentService.GetDownloadTicketsByFileIds to get a download ticket.</p>
<p><strong>allowSync</strong> – If true and the file is not on the local filestore, the file is copied as part of the operation. If false, the operation fails if the file is not in the local filestore.</p>
<p><strong>Return value</strong> – The properties that may be read from or written to in the file.</p>
<hr noshade="noshade" style="color: #d09219;" />
