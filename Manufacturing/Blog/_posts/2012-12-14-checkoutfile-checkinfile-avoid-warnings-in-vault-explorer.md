---
layout: "post"
title: "CheckoutFile / CheckinFile - avoid warnings in Vault Explorer"
date: "2012-12-14 15:09:02"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/12/checkoutfile-checkinfile-avoid-warnings-in-vault-explorer.html "
typepad_basename: "checkoutfile-checkinfile-avoid-warnings-in-vault-explorer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>If you have used the Vault API to Check-out / Check-in a file you may have noticed a status on the file of &quot;Incorrect version Refresh the file&quot; or “Edited out of turn” in Vault Explorer. </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6423956970d-pi"><img alt="image" border="0" height="175" src="/assets/image_df5b49.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="346" /></a></p>
<p><strong>&quot;Incorrect version Refresh the file&quot;</strong></p>
<p>In vault explorer a file is checked out to the working folder, modified there, and then checked in. Because of this, the latest checked in file matches the file in the working folder. If your code puts the file in a different directory the version in your working folder is never updated and the status is telling you that what you have on disk is not the latest version in vault.&#0160; </p>
<p><strong>“Edited out of turn”</strong> </p>
<p>The “Edited out of turn”&#0160; status occurs when the create date of the local file does not match the create date of the File object in the vault. This <a href="http://justonesandzeros.typepad.com/blog/2010/02/setting-file-attributes-on-download.html" target="_blank">blog post</a> has more detail about this.</p>
<p><strong>Example code:</strong></p>
<p><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b017d3ecdc95e970c"><a href="http://adndevblog.typepad.com/files/vault_checkin_checkout.zip">Download Vault_CheckIn_CheckOut</a></span></strong></p>
<p><strong>&#0160;</strong></p>
<p>Below is code from this project that avoids both of these conditions. To use this example the first file in the root folder of the vault needs to be a txt file. (or change the code so that it uses a txt file).</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">void</span> button2_Click(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (m_serviceManager != <span style="color: blue;">null</span> )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Folder</span> rootFolder = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; m_serviceManager.DocumentService.GetFolderRoot();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">File</span>[] rootFolderFiles = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_serviceManager.DocumentService.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetLatestFilesByFolderId(rootFolder.Id, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (rootFolderFiles == <span style="color: blue;">null</span> || </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rootFolderFiles.Length == 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">File</span> file = rootFolderFiles[0];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ByteArray</span> byteArray;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span> localPath = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_vaultExplorer.GetWorkingFolder(rootFolder);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//Check out the file </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_serviceManager.DocumentService.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CheckoutFile(rootFolder.Id,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; file.Id,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">CheckoutFileOptions</span>.Master,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Environment</span>.MachineName, </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; localPath, <span style="color: #a31515;">&quot;test check-out&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">DownloadOptions</span>.Download, </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">false</span>, <span style="color: blue;">out</span> byteArray);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span> filePath = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">Path</span>.Combine(localPath, file.Name);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//Make readonly false for file</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">FileInfo</span> localFileInfo = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> System.IO.<span style="color: #2b91af;">FileInfo</span>(filePath);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; localFileInfo.IsReadOnly = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// edit the file</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">File</span>.WriteAllBytes</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (filePath, byteArray.Bytes);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">File</span>.AppendAllText</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (filePath, <span style="color: #2b91af;">Environment</span>.NewLine + </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Hello World_wB!&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// check in the file. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; byteArray.Bytes =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">File</span>.ReadAllBytes(filePath);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; file = m_serviceManager.DocumentService.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CheckinFile(file.MasterId, </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;test check-in wB&quot;</span>, <span style="color: blue;">false</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">File</span>.GetLastWriteTime</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (filePath), <span style="color: blue;">null</span>, <span style="color: blue;">null</span>, <span style="color: blue;">true</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; file.Name, file.FileClass, </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; file.Hidden, byteArray);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// avoid the &quot;Edited-out-of-turn&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// icon in Vault Explorer </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">FileInfo</span> info = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> System.IO.<span style="color: #2b91af;">FileInfo</span>(filePath);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; info.CreationTime = file.CreateDate;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; info.Attributes = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; info.Attributes | </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">FileAttributes</span>.ReadOnly;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
