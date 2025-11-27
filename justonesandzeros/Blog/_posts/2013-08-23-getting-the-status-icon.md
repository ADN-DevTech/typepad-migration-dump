---
layout: "post"
title: "Getting the status icon"
date: "2013-08-23 08:14:44"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/08/getting-the-status-icon.html "
typepad_basename: "getting-the-status-icon"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>If you have played around with the new Grid Control, you may have noticed that there is a column for the status icon.&#0160; It’s a highly useful piece of information that is difficult to calculate.&#0160; The good news is that you can leverage the 2014 API to give you this data, either in icon form or object form.</p>
<p><img alt="" src="/assets/screenshot.png" /></p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Client side properties:</strong></p>
<p>The status icon is an example of a client-side property.&#0160; It’s not something the server can do because it requires data from your hard-drive.&#0160; There are many factors that go into the status image, including comparing the state of the local file with the state of the corresponding file in Vault.&#0160; So, if you are looking for an easy way for your app to see what files are out of date, the status property gives that to you easily.</p>
<p>You can’t search on client-side properties, since the server controls the search feature.&#0160; You also can’t use any of the web service functions because those functions only deal with server-side data.&#0160; You need to use VDF libraries (Autodesk.DataManagement.*) in order to work with client-side properties.&#0160; </p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Getting the icon:</strong></p>
<p>If you are displaying the grid, the control itself will take care of displaying the icon.&#0160; But if you want to use the icon in your own UI, there is a way to get it.&#0160; It’s probably best if I just start with the code and then explain what the code is doing.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9pt;">using</span></span></span><span><span style="font-size: 9pt; color: #000000;"> VDF = Autodesk.DataManagement.Client.Framework;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9pt;">using</span></span></span><span><span style="font-size: 9pt; color: #000000;"> Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;">&#0160;</p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt;"><span style="color: #000000;">&#0160;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">&#0160;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">PropertyDefinitionDictionary</span></span></span><span><span style="font-size: 9pt; color: #000000;"> props = </span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">m_conn.PropertyManager.GetPropertyDefinitions(</span><span>&#0160;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="mso-spacerun: yes;"><span style="color: #a31515;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;">VDF.Vault.Currency.Entities.</span><span><span style="color: #2b91af;">EntityClassIds</span></span><span style="color: #000000;">.Files, </span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 9pt;"><span><span style="color: #0000ff;">Null</span></span><span style="color: #000000;">, </span><span><span style="color: #2b91af;">PropertyDefinitionFilter</span></span><span style="color: #000000;">.IncludeAll);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">PropertyDefinition</span></span></span><span><span style="font-size: 9pt; color: #000000;"> statusProp = </span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">props[</span></span><span style="font-size: 9pt;"><span><span style="color: #2b91af;">PropertyDefinitionIds</span></span><span style="color: #000000;">.</span><span><span style="color: #2b91af;">Client</span></span><span style="color: #000000;">.VaultStatus];</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">EntityStatusImageInfo</span></span></span><span><span style="font-size: 9pt; color: #000000;"> status = </span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 9pt;">m_conn.PropertyManager.GetPropertyValue(fileIter, </span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; statusProp, </span></span><span style="font-size: 9pt;"><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">) </span><span><span style="color: #0000ff;">as</span></span><span style="color: #000000;"> </span></span></span></span><span><span style="font-family: Consolas;"><span style="font-size: 9pt;"><span><span style="color: #2b91af;">EntityStatusImageInfo</span></span><span style="color: #000000;">;</span></span></span></span></p>
</td>
</tr>
</tbody>
</table>
<p>Step one is to get the VDF PropertyDefintion for the status information.&#0160; I do this by first calling Connection.PropertyManager.GetPropertyDefinitions and grabbing all file-related PropertyDefinitions.&#0160; This is a VDF function, so I get back both client and server property definitions.&#0160; The return object is indexed by ID and system name.&#0160; So, it’s easy to look up a property if I know the system name.&#0160; In the case of Vault status, the ID is defined in PropertyDefinitionsIds.</p>
<p>The last step is to call GetPropertyValue for on my FileIteration object.&#0160; The function returns an Object, which I convert to EntityStatusImageInfo.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>EntityStatusImageInfo</strong></p>
<p>This is a very useful class.&#0160; Not only does it give you the Image object itself, but it gives you a data structure describing the status.&#0160; So you can, for example, write special logic for cases where the local file version doesn’t match any vault version (ie. edited out of turn).</p>
<p>You can call GetImage to get an Image object that you can display in your UI.&#0160; The algorithm for calculating that image is surprisingly complex, so it’s good do have a functions that does the calculation for you.&#0160; Because this is the same code used by the clients, your icon will match the icon seen in Vault Explorer and the CAD plug-ins.   <br /><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
