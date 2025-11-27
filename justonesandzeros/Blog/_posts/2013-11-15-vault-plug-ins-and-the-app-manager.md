---
layout: "post"
title: "Vault plug-ins and the App Manager"
date: "2013-11-15 12:48:39"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/11/vault-plug-ins-and-the-app-manager.html "
typepad_basename: "vault-plug-ins-and-the-app-manager"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>Previously Vault would only load plug-ins from one location.&#0160; Starting in 2014, it now can load plug-ins from 3 locations.&#0160; Two new locations were added to support the App Manager.&#0160; So if you are creating an app for the Vault Exchange store, you can install to the locations that App Manager is used to working with.</p>
<p>Here are the 3 locations:</p>
<ul>
<li>%programData%\Autodesk\<span style="color: #ff0000;">[Vault Version]</span>\Extensions <br /><em>This path is specific to Vault</em></li>
<li>%programData%\Autodesk\ApplicationPlugins <br /><em>This path is for App Store plug-ins which are installed to all users on the computer</em></li>
<li>%appData%\Autodesk\ApplicationPlugins <br /><em>This path is for App Store plug-in which are installed to a single user on the computer</em></li>
</ul>
<hr noshade="noshade" style="color: #d09219;" />
<p>Setting up the PackageContents.xml for the app store should be pretty straightforward for a Vault app.&#0160; The only major difference is that instead of pointing to a DLL, a Vault module should point to the .vcet.config file.&#0160; You should also have your plug-in DLLs in the same folder as your .vcet.config file.</p>
<p>Here is an example PackageContents.xml with a Vault module:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p>&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt; <br />&lt;ApplicationPackage SchemaVersion=&quot;1.0&quot; AutodeskProduct=&quot;Vault&quot; Name=&quot;Job Stuff&quot; Description=&quot;Job Stuff&quot; AppVersion=&quot;1.0.0&quot; FriendlyVersion=&quot;1.0.0&quot; ProductType=&quot;Application&quot; SupportedLocales=&quot;Enu&quot; AppNameSpace=&quot;appstore.exchange.autodesk.com&quot; ProductCode=&quot;{46D19B6E-6888-4E27-B6C1-65A7E596DF06}&quot; UpgradeCode=&quot;{284169AB-1CF4-4F5A-B7F3-64771319E9A8}&quot; OnlineDocumentation=&quot;http:\\www.autodesk.com&quot; Author=&quot;Autodesk&quot;&gt; <br />&#0160; &lt;CompanyDetails Name=&quot;Autodesk&quot; Url=&quot;http:\\www.autodesk.com&quot; Email=&quot;nobody@autodesk.com&quot; /&gt; <br />&#0160; &lt;RuntimeRequirements Platform=&quot;Vault&quot; /&gt; <br />&#0160; &lt;Components Description=&quot;Autodesk Job Stuff.bundle parts&quot;&gt; <br />&#0160;&#0160;&#0160; &lt;RuntimeRequirements <span style="color: #ff0000;">Platform=&quot;Vault&quot; SeriesMin=&quot;V2014&quot; SeriesMax=&quot;V2014&quot;</span> /&gt; <br />&#0160;&#0160;&#0160; &lt;ComponentEntry AppName=&quot;JobStuff&quot; Version=&quot;1.0.0&quot; <span style="color: #ff0000;">ModuleName=&quot;./JobProcessorApiSamples.vcet.config&quot;</span> /&gt; <br />&#0160; &lt;/Components&gt; <br />&lt;/ApplicationPackage&gt;</p>
</td>
</tr>
</tbody>
</table>
<hr noshade="noshade" style="color: #d09219;" />
