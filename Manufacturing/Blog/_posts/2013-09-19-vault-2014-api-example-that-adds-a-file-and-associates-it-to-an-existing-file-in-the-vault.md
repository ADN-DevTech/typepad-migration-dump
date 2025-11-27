---
layout: "post"
title: "Vault 2014 API example that Adds a file and associates it to an existing file in the vault"
date: "2013-09-19 15:09:40"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/vault-2014-api-example-that-adds-a-file-and-associates-it-to-an-existing-file-in-the-vault.html "
typepad_basename: "vault-2014-api-example-that-adds-a-file-and-associates-it-to-an-existing-file-in-the-vault"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>You may need to add a file and associate it to a file that already exists in the vault. This VB.NET example shows how this can be done. The example as I tested it adds an excel file on disk to a text file in the vault. </p>
<p><em>connection.FileManager.AddFile()</em> is used to add a file from the disk to the vault. </p>
<p><em>connection.FileManager.AcquireFiles()</em> is used to check out the file that is going to have another file attached to it. (it is checked out without being downloaded). </p>
<p><em>connection.FileManager.GetFileAssociationLites()</em> is used to get the existing file associations. A new <em>FileAssocParam</em> is used to add the attachment. The associated files are added as a parameter to the <em>connection.</em><em>FileManager.CheckinFile</em> method. </p>
<p>The <em>CheckinFile</em> method has two signatures. The one with the System.IO.Stream bytes parameter is used to check in a file that was not downloaded. (the parameter is Nothing)</p>
<p>The connection object is an: Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections.Connection</p>
<p>You could use the WebService function <em>GetFileAssociationsByIds</em> to get the associated files. One of my colleagues in engineering has found that <em>GetFileAssociationLites</em>() works better if there are a large number of associated files and is used in this example. (<em>GetFileAssociationsByIds()</em> is in the example as comments)</p>
<p><strong>Note:</strong> Currently the Vault API does not support adding cad files such as Inventor Assemblies. I have also found that attaching a file to an .idw can adversely effect the dwf file that is attached as a design visualization file. (the dwf is gone as an attachment and needs to be recreated – this happens when you check in the idw using the Vault Inventor Add-In) Also the Inventor Vault Add-In will list the attached files if you check out all. The attached files may not be downloaded however and this causes an error on check in.&#0160;&#0160; </p>
<p>&#0160;
<span class="asset  asset-generic at-xid-6a0167607c2431970b019aff961962970d"><a href="http://adndevblog.typepad.com/files/vault_list_add_and_associate.zip">Download Vault_list_Add_and_Associate</a></span></p>
<p>The example is an update to VaultList SDK sample. It has three new buttons. One of the buttons will do the “add and attach file”.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff7e6a5d970c-pi"><img alt="image" border="0" height="251" src="/assets/image_9c8032.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="394" /></a></p>
<p><strong>From the attached project:</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> Button4_Click(sender <span style="color: blue;">As</span> System.<span style="color: #2b91af;">Object</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; e <span style="color: blue;">As</span> System.<span style="color: #2b91af;">EventArgs</span>) <span style="color: blue;">Handles</span> Button4.Click</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; For demonstration purposes, the information</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; is hard-coded.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> results <span style="color: blue;">As</span> VDF.Vault.Results.<span style="color: #2b91af;">LogInResult</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.<span style="color: #2b91af;">Library</span>.ConnectionManager.LogIn _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;localhost&quot;</span>, <span style="color: #a31515;">&quot;Vault&quot;</span>, <span style="color: #a31515;">&quot;Administrator&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Currency.Connections. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AuthenticationFlags</span>.Standard,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Nothing</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> results.Success <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> connection <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Connections.<span style="color: #2b91af;">Connection</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = results.Connection</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Need to change this string to a file on </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; a drive that you want to add to the vault</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> filePath <span style="color: blue;">As</span> <span style="color: blue;">String</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;C:\Temp\myFile_20.xlsx&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFldrCol <span style="color: blue;">As</span> System.Collections.Generic.<span style="color: #2b91af;">List</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">Of</span> VDF.Vault.Currency.Entities.<span style="color: #2b91af;">Folder</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myFldrCol = connection.FolderManager. _</p>
<p style="margin: 0px;"> GetChildFolders(connection.FolderManager.RootFolder,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">False</span>, <span style="color: blue;">False</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the folder to add the new file to change </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the FullName test to a Folder in your vault</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFolder <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.<span style="color: #2b91af;">Folder</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> Flder <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.<span style="color: #2b91af;">Folder</span> <span style="color: blue;">In</span> myFldrCol</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> Flder.FullName = <span style="color: #a31515;">&quot;$/wB_Excel_Files&quot;</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFolder = Flder</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit For</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFileIterationNewFile <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.<span style="color: #2b91af;">FileIteration</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Using</span> fileStream <span style="color: blue;">As</span> <span style="color: #2b91af;">Stream</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">FileStream</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (filePath, <span style="color: #2b91af;">FileMode</span>.Open, <span style="color: #2b91af;">FileAccess</span>.Read)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Add the file to the vault</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileIterationNewFile =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.FileManager.AddFile(myFolder,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Path</span>.GetFileName(filePath),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Added by wB code&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">DateTime</span>.Now, <span style="color: blue;">Nothing</span>, <span style="color: blue;">Nothing</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ACW.<span style="color: #2b91af;">FileClassification</span>.None,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">False</span>, fileStream)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> myFileIterationNewFile <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> bAddedAttachment <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span> = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Need to change this string to an existing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; file in the Vault</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> strNameOfFileToAddTo = <span style="color: #a31515;">&quot;wB_test.txt&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> fldrId <span style="color: blue;">As</span> <span style="color: blue;">Long</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileIterationNewFile.FolderId</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bAddedAttachment = AddMyAttachment _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (strNameOfFileToAddTo,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileIterationNewFile.EntityIterationId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fldrId, connection)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> bAddedAttachment = <span style="color: blue;">False</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Unable to get FileIteration object&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Success - Added and attached file&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Logout</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.<span style="color: #2b91af;">Library</span>.ConnectionManager.LogOut _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (connection)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Function</span> AddMyAttachment _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (nameOfFileToAttachTo <span style="color: blue;">As</span> <span style="color: blue;">String</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileIterationId <span style="color: blue;">As</span> <span style="color: blue;">Long</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFolderIdOfNewFileIteration <span style="color: blue;">As</span> <span style="color: blue;">Long</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Connections.<span style="color: #2b91af;">Connection</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oFileIteration <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.<span style="color: #2b91af;">FileIteration</span> =</p>
<p style="margin: 0px;">getFileIteration(nameOfFileToAttachTo, connection)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oFileIteration <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;Unable to get FileIteration&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get settings </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oSettings <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Settings.<span style="color: #2b91af;">AcquireFilesSettings</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">New</span> VDF.Vault.Settings.<span style="color: #2b91af;">AcquireFilesSettings</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (connection)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Going to Check Out (not download file)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSettings.DefaultAcquisitionOption =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Settings.<span style="color: #2b91af;">AcquireFilesSettings</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcquisitionOption</span>.Checkout</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; add the file to the settings</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSettings.AddEntityToAcquire(oFileIteration)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Do the CheckOut</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myAcquireVaultSettings <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Results.<span style="color: #2b91af;">AcquireFilesResults</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myAcquireVaultSettings =</p>
<p style="margin: 0px;">&#0160; connection.FileManager.AcquireFiles(oSettings)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oNewFileIteration <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.<span style="color: #2b91af;">FileIteration</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFileAcqRes <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Results.<span style="color: #2b91af;">FileAcquisitionResult</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myFileAcqRes =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myAcquireVaultSettings.FileResults(0)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oNewFileIteration =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileAcqRes.NewFileIteration</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oNewFileIteration.IsCheckedOut = <span style="color: blue;">True</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; settings used in GetFileAssociationLites()</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFileRelationshipSettings <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">VDF.Vault.Settings.<span style="color: #2b91af;">FileRelationshipGatheringSettings</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileRelationshipSettings = _</p>
<p style="margin: 0px;"><span style="color: blue;">New</span> VDF.Vault.Settings.<span style="color: #2b91af;">FileRelationshipGatheringSettings</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileRelationshipSettings. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IncludeAttachments = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileRelationshipSettings. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IncludeChildren = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileRelationshipSettings. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IncludeParents = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileRelationshipSettings. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IncludeRelatedDocumentation = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myColOfFileAssocLite <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Collections.Generic.<span style="color: #2b91af;">IEnumerable</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">Of</span> ACW.<span style="color: #2b91af;">FileAssocLite</span>) = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myColOfFileAssocLite =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; connection.FileManager.GetFileAssociationLites _</p>
<p style="margin: 0px;">&#0160; (<span style="color: blue;">New</span> <span style="color: blue;">Long</span>() {oNewFileIteration.EntityIterationId},</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileRelationshipSettings)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Going to add new FileAssocParam </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; objects to this list </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; ArrayList to contain </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; FileAssocParam objects</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> fileAssocParams <span style="color: blue;">As</span> <span style="color: #2b91af;">ArrayList</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">New</span> <span style="color: #2b91af;">ArrayList</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Add FileAssocParam objects to the ArrayList</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; using values from the collection </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; of FileAssocLite in the collection </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; returned from GetFileAssociationLites()</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> myColOfFileAssocLite <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFileAssocLite <span style="color: blue;">As</span> <span style="color: #2b91af;">FileAssocLite</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160;&#0160; &#39;Go through each FileAssoLite in the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;&#0160;&#0160; in the collection of FileAssocLite</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> myFileAssocLite <span style="color: blue;">In</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myColOfFileAssocLite</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; This is a new FileAssocParam that </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; is going to be added to the List</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; getting the properties </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> par <span style="color: blue;">As</span> <span style="color: #2b91af;">FileAssocParam</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">New</span> <span style="color: #2b91af;">FileAssocParam</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; par.CldFileId =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileAssocLite.CldFileId</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; par.RefId = myFileAssocLite.RefId</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; par.Source = myFileAssocLite.Source</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; par.Typ = myFileAssocLite.Typ</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; par.ExpectedVaultPath =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileAssocLite.ExpectedVaultPath</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fileAssocParams.Add(par)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the folder of the file </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; we are associating</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myDictionary <span style="color: blue;">As</span> <span style="color: #2b91af;">IDictionary</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">Of</span> <span style="color: blue;">Long</span>, VDF.Vault.Currency.Entities.<span style="color: #2b91af;">Folder</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myDictionary =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.FolderManager.GetFoldersByIds _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">New</span> <span style="color: blue;">Long</span>() {myFolderIdOfNewFileIteration})</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the folder from the dictionary</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> keyPair <span style="color: blue;">As</span> Generic.<span style="color: #2b91af;">KeyValuePair</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">Of</span> <span style="color: blue;">Long</span>, VDF.Vault.Currency.Entities.<span style="color: #2b91af;">Folder</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; keyPair = myDictionary.First</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFldr <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.<span style="color: #2b91af;">Folder</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = keyPair.Value</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Add the new association</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> newFileAssocPar <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">FileAssocParam</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">FileAssocParam</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newFileAssocPar.CldFileId = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileIterationId</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newFileAssocPar.RefId = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newFileAssocPar.Source = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newFileAssocPar.Typ = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssociationType</span>.Attachment</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newFileAssocPar.ExpectedVaultPath = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFldr.FolderPath</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Add our new FileAssocParam</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fileAssocParams.Add(newFileAssocPar)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Populate this with the FileAssocParam</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; objects that we create from properties&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; in the FileAssocArray and the new file</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFileAssocParamArray <span style="color: blue;">As</span> <span style="color: #2b91af;">FileAssocParam</span>() _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">DirectCast</span>(fileAssocParams.ToArray _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">GetType</span>(<span style="color: #2b91af;">FileAssocParam</span>)), <span style="color: #2b91af;">FileAssocParam</span>())</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Use the overloaded method of CheckInFile </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; that takes a stream This will allow </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; checking in a file that has not been</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; downloaded</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; (by passing in a stream that is Nothing) </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myStream <span style="color: blue;">As</span> System.IO.<span style="color: #2b91af;">Stream</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.FileManager.CheckinFile _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oNewFileIteration, <span style="color: #a31515;">&quot;wbTesting&quot;</span>, <span style="color: blue;">False</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Date</span>.Now,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myFileAssocParamArray,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Nothing</span>, <span style="color: blue;">False</span>, <span style="color: blue;">Nothing</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ACW.<span style="color: #2b91af;">FileClassification</span>.None,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">False</span>, myStream)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;Unable to check out&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
</div>
