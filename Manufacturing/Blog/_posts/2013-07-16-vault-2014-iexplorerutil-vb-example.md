---
layout: "post"
title: "Vault 2014 - IExplorerUtil VB example"
date: "2013-07-16 18:27:22"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/vault-2014-iexplorerutil-vb-example.html "
typepad_basename: "vault-2014-iexplorerutil-vb-example"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>IExplorerUtil.UpdateFileProperties() can be used to update mapped properties such as Inventor iProperties. There are multiple types of properties. These properties will be either:</p>
<ul>
<li>Properties that live inside the file </li>
<li>Properties that are stored in the Vault database. </li>
</ul>
<p>DocumentService.UpdateFileProperties() is only able to update the Vault DB property value, it doesn’t update the property in a file. If you use DocumentService.UpdateFileProperties() the following happens:    <br />1.You check out the file. It has a property value of “oldValue”.</p>
<p>2. You call DocumentService.UpdateFileProperties(), which changes the Vault property to “newValue”.&#0160;&#0160; <br />3. You check in the file. The file still has a value of “oldValue”, so when the Vault Server extracts the file properties, “newValue” is switched back to “oldValue”.</p>
<p>To use IExplorerUtil you need to call GetExplorerUtil or LoadExplorerUtil. GetExplorerUtil is for when you are running inside Vault Explorer.</p>
<p>This <a href="http://justonesandzeros.typepad.com/blog/2011/09/autodeskconnectivityexplorerextensibilitytoolsdll.html" target="_blank">blog post</a> has more information about IExplorerUtil. </p>
<p>Here are a couple of things I found about IExplorerUtil.</p>
<p>1. An IllegalInputParam exception is a somewhat generic exception thrown by the IExplorerUtil.UpdateFileProperties() when it gets any kind of unsuccessful outcome when trying to update the properties that isn’t the result of some kind of programming error (i.e. null reference exception, empty collections, etc.).&#0160; </p>
<p>2. IExplorerUtils.UpdateProperties() will write back to the file before checking it in, but it does so in a temporary location so that the file in the working directory isn’t modified. This means that after the update, the file in the working directory doesn’t match the latest version in Vault, making the vault status out-of-date.</p>
<p><strong>VB.NET Example</strong></p>
<p>The 2013 VaultBrowserSample has an example of using IExplorerUtil. The 2014 VaultBrowserSample was updated to use the VDF download and so it does not rely on IExplorerUtil to do downloads anymore (in fact, IExplorerUtil doesn’t offer a download API anymore in the Vault 2014 SDK). </p>
<p>Here is an update to the VaultList SDK example that uses IExplorerUtil.UpdateFileProperties() to update the property of an Inventor part file. Notice that you do not need to explicitly check out or check in the file. (It is done by UpdateFileProperties however)</p>
<p>&#0160;
<span class="asset  asset-generic at-xid-6a0167607c2431970b019104428225970c"><a href="http://adndevblog.typepad.com/files/vaultlist_with_iexplorerutil.zip">Download VaultList_With_IExplorerUtil</a></span>&#0160; </p>
<p>Here is the pertinent code from this example:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> Button4_Click(sender <span style="color: blue;">As</span> System.<span style="color: #2b91af;">Object</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; e <span style="color: blue;">As</span> System.<span style="color: #2b91af;">EventArgs</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Handles</span> Button4.Click</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; For demonstration purposes, </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the information is hard-coded.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> results <span style="color: blue;">As</span> VDF.Vault.Results.<span style="color: #2b91af;">LogInResult</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.<span style="color: #2b91af;">Library</span>.ConnectionManager.LogIn _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;localhost&quot;</span>, <span style="color: #a31515;">&quot;Vault&quot;</span>, <span style="color: #a31515;">&quot;Administrator&quot;</span>, <span style="color: #a31515;">&quot;&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Currency.Connections. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AuthenticationFlags</span>.Standard,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Nothing</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> results.Success <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> connection <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.Currency.Connections.<span style="color: #2b91af;">Connection</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; results.Connection</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get IExplorerUtil using properties of the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; connection</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> VaultExplorerUtil <span style="color: blue;">As</span> <span style="color: #2b91af;">IExplorerUtil</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VaultExplorerUtil =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ExplorerLoader</span>.LoadExplorerUtil(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.Server,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.Vault,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.UserID,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; connection.Ticket)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the FileIteration of a file with </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; this name</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oFileIteration <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">VDF.Vault.Currency.Entities.<span style="color: #2b91af;">FileIteration</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; getFileIteration _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Box_with_holes_wb.ipt&quot;</span>, connection)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; This is the new part number</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> updateValueObj <span style="color: blue;">As</span> <span style="color: blue;">Object</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;wB - part number 2222&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the Part number property</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myPropDefArray <span style="color: blue;">As</span> <span style="color: #2b91af;">PropDef</span>() =</p>
<p style="margin: 0px;">connection.WebServiceManager.PropertyService.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FindPropertyDefinitionsBySystemNames _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;FILE&quot;</span>, <span style="color: blue;">New</span> <span style="color: blue;">String</span>() {<span style="color: #a31515;">&quot;PartNumber&quot;</span>})</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; This is the PropDef in the array</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myPropDef <span style="color: blue;">As</span> <span style="color: #2b91af;">PropDef</span> = myPropDefArray(0)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; make a dictionary with the Part name </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; PropDef and the new value for the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; part number</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> propsDict <span style="color: blue;">As</span> <span style="color: #2b91af;">Dictionary</span>(<span style="color: blue;">Of</span> ACW.<span style="color: #2b91af;">PropDef</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Object</span>) =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">New</span> <span style="color: #2b91af;">Dictionary</span>(<span style="color: blue;">Of</span> ACW.<span style="color: #2b91af;">PropDef</span>, <span style="color: blue;">Object</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; propsDict.Add(myPropDef, updateValueObj)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Update the part number</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VaultExplorerUtil.UpdateFileProperties _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oFileIteration, propsDict)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VaultExplorerUtil = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(ex.ToString())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VaultExplorerUtil = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; VDF.Vault.<span style="color: #2b91af;">Library</span>.ConnectionManager.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; LogOut(connection)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
