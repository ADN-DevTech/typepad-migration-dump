---
layout: "post"
title: "Connecting vault and accessing vault file status through Inventor iLogic"
date: "2019-04-11 05:05:06"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2019/04/connecting-vault-and-accessing-vault-file-status-through-inventor-ilogic.html "
typepad_basename: "connecting-vault-and-accessing-vault-file-status-through-inventor-ilogic"
typepad_status: "Publish"
---

<p>By <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener" target="_blank">Chandra shekar Gopal</a></p>
<p>&#0160;</p>
<p>Inventor iLoigc is very useful to Inventor developers due to easy development and easy deployment with in built editor. As iLogic rules can be attached or incorporated with Inventor documents, it encourages reusability of iLogic rules.</p>
<p>iLogic rules can be triggered an <em>automatic</em> update of a whole host of parameters, feature/component suppression and iProperty values based on a variety of actions like below.</p>
<ul>
<li>Parameter value change</li>
<li>Model geometry or drawing view changes</li>
<li>Software Events</li>
<li>When a document is created</li>
<li>Right before a document is saved</li>
<li>On demand by the user</li>
</ul>
<p>Inventor iLogic can also be used to establish vault connection and access vault file status. To demonstrate the same, below iLogic can be used to connect vault and access vault file status of current document.</p>
<blockquote>
<p>AddReference &quot;Autodesk.Connectivity.WebServices.dll&quot;<br />Imports AWS = Autodesk.Connectivity.WebServices<br />AddReference &quot;Autodesk.DataManagement.Client.Framework.Vault.dll&quot;<br />Imports VDF = Autodesk.DataManagement.Client.Framework <br />AddReference &quot;Connectivity.Application.VaultBase.dll&quot;<br />Imports VB = Connectivity.Application.VaultBase</p>
<p>Dim mVltCon As VDF.Vault.Currency.Connections.Connection <br />mVltCon = VB.ConnectionManager.Instance.Connection<br />&#0160; <br />If&#0160; mVltCon Is Nothing Then<br />&#0160;&#0160;&#0160;&#0160; Messagebox.Show(&quot;Not Logged In to Vault! - Login first and repeat executing this rule.&quot;)<br />&#0160;&#0160;&#0160;&#0160; Exit Sub<br />End If</p>
<p>Dim VaultPath As String = ThisDoc.PathAndFileName(True)<br />VaultPath = VaultPath.Replace(&quot;C:\$WorkingFolder\&quot;, &quot;$/&quot;)<br />&#39;flip the slashes<br />VaultPath = VaultPath.Replace(&quot;\&quot;, &quot;/&quot;)</p>
<p>Dim VaultPaths() As String = New String() {VaultPath}<br />&#0160; <br />Dim wsFiels() As AWS.File = mVltCon.WebServiceManager.DocumentService.FindLatestFilesByPaths(VaultPaths)<br />&#0160; <br />Dim mFileIt As VDF.Vault.Currency.Entities.FileIteration = New VDF.Vault.Currency.Entities.FileIteration(conn,wsFiels(0))<br />Dim lifeCycleInfo As VDF.Vault.Currency.Entities.FileLifecycleInfo = mFileIt.LifecycleInfo</p>
<p>Messagebox.Show(lifeCycleInfo.Statename)</p>
</blockquote>
<p><strong><u>Note:</u></strong> Before using above iLogic rule, make sure that local working folder of vault is updated <strong>(VaultPath = VaultPath.Replace(&quot;C:\$WorkingFolder\&quot;, &quot;$/&quot;)).</strong> Because, it is system specific. In my system, <strong>“C:\$WorkingFolder\”</strong> is the local working folder of Vault.</p>
