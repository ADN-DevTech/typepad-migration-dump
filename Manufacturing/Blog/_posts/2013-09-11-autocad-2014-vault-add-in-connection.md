---
layout: "post"
title: "AutoCAD or Inventor 2014 Vault Add-In connection"
date: "2013-09-11 14:48:17"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/autocad-2014-vault-add-in-connection.html "
typepad_basename: "autocad-2014-vault-add-in-connection"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>You may want to get the login from the AutoCAD 2014 Vault Add-In. Here is a VB.NET example that does this. Please keep in mind that this approach has not been through any QA and is not officially supported. It does work in my tests however.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b019aff547022970d"><a href="http://adndevblog.typepad.com/files/autocad_vault_2014_get_connection.zip">Download AutoCAD_Vault_2014_get_Connection</a></span>&#0160;</p>
<p>This discussion group thread has a suggestion for getting the login from the AutoCAD 2013 Vault Add-In.</p>
<p><a href="http://forums.autodesk.com/t5/Autodesk-Vault-Customization/Get-Securityheader-from-Vault-Login-in-AutoCAD/td-p/3773017" title="http://forums.autodesk.com/t5/Autodesk-Vault-Customization/Get-Securityheader-from-Vault-Login-in-AutoCAD/td-p/3773017">http://forums.autodesk.com/t5/Autodesk-Vault-Customization/Get-Securityheader-from-Vault-Login-in-AutoCAD/td-p/3773017</a></p>
<p>&#0160;</p>
<p>You can use the same approach to get the Vault Add-In connection from Inventor. Here is a project I have that does this. To test it follow steps to get the Add-In to load. In Inventor start a part and there should be a new ribbon panel with a button.</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01a5113ae9a5970c img-responsive"><a href="http://adndevblog.typepad.com/files/lab_10_with_vault_test.zip">Download Lab_10_With_Vault_Test</a></span></p>
<p><br /><strong>Here is the command from the AutoCAD VB.NET the project:</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&lt;CommandMethod(<span style="color: #a31515;">&quot;MyGroup&quot;</span>, <span style="color: #a31515;">&quot;testVault&quot;</span>, <span style="color: #a31515;">&quot;testVault&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">CommandFlags</span>.Modal)&gt; _</p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> test_Vault()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> ed <span style="color: blue;">As</span> <span style="color: #2b91af;">Editor</span> = <span style="color: #2b91af;">Application</span>.DocumentManager.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MdiActiveDocument.Editor</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myConnection <span style="color: blue;">As</span> VDF.Vault.Currency.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Connections.<span style="color: #2b91af;">Connection</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the Vault connection from </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the AutoCAD Vault log in</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myConnection = Connectivity.Application.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VaultBase.<span style="color: #2b91af;">ConnectionManager</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Instance.Connection()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> myConnection <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Unable to get Vault connection&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; test the connection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myRootFolder <span style="color: blue;">As</span> <span style="color: #2b91af;">Folder</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myConnection.FolderManager.RootFolder()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myWebServ <span style="color: blue;">As</span> <span style="color: #2b91af;">WebServiceManager</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myConnection.WebServiceManager</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myDocServ <span style="color: blue;">As</span> <span style="color: #2b91af;">DocumentService</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; myWebServ.DocumentService</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> MyFolder <span style="color: blue;">As</span> <span style="color: #2b91af;">Folder</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Need to change the string argument </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; to a Folder in your vault</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MyFolder = myDocServ.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetFolderByPath(<span style="color: #a31515;">&quot;$/wb_Excel_Files&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;$/wb_Excel_Files created: &quot;</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; MyFolder.CreateDate.ToString())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> System.<span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;unable to get Folder.&#0160; &quot;</span> &amp; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ex.ToString())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
