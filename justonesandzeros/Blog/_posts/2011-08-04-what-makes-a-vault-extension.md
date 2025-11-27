---
layout: "post"
title: "What Makes a &quot;Vault Extension&quot;"
date: "2011-08-04 08:28:35"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/08/what-makes-a-vault-extension.html "
typepad_basename: "what-makes-a-vault-extension"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>The Vault 2012 API has a formalized concept of an Extension.&#0160; In the Vault 2011, the concept was there, but there was no clear set of rules or behaviors.&#0160; By defining things more clearly in 2012, the concept is easier to understand, and the groundwork is laid for future API enhancements.</p>
<p><strong>The Definition:      <br /></strong>A Vault Extension is a component that doesn&#39;t run on its own, but gets loaded by the Vault framework.     <br />Currently, there are 3 types:&#0160; Vault Explorer extensions, Job Processor extensions, and Web Service Command extensions.</p>
<p><strong>The Rules:      <br /></strong>There is a common set of rules that all extensions must follow.</p>
<ul>
<li>They are all .NET assemblies.</li>
<li>They must have values for 5 specific assembly attributes:&#0160; AssemblyCompany, AssemblyProduct, AssemblyDescription, ExtensionId, and APIVersion.      <br />The first three are put in by Visual Studio.&#0160; The last two are from the Vault API. </li>
<li>They must be deployed with a .vcet.config file. </li>
<li>They must be deployed to a folder under %programData%\Autodesk\Vault 2012\Extensions.&#0160; 1 Extension per folder. </li>
<li>They must have a single, public class that implements the interface for that extension type. </li>
</ul>
<p><strong>The interfaces:</strong></p>
<p>The interface implementations are important because the Vault framework uses that class as the entry point into your customization.&#0160; The interface is how the Vault sees your code, and Vault is not concerned with anything underneath.</p>
<p><strong>Vault Explorer Extensions:</strong></p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="125">Description:</td>
<td valign="top" width="345">Vault Explorer extensions are for extending the Vault Explorer client application.&#0160; Commonly used for things like adding commands and tab views.</td>
</tr>
<tr>
<td valign="top" width="125">Interface:</td>
<td valign="top" width="345">Autodesk.Connectivity.Explorer.Extensibility.IExtension</td>
</tr>
<tr>
<td valign="top" width="125">ExtensionType in the .vcet.config:</td>
<td valign="top" width="345">VaultClient</td>
</tr>
<tr>
<td valign="top" width="125">Loads in:</td>
<td valign="top" width="345">Connectivity.VaultWkg.exe          <br />Connectivity.VaultCollaboration.exe           <br />Connectivity.VaultPro.exe</td>
</tr>
</tbody>
</table>
<p><strong>Job Processor Extensions:</strong></p>
<table border="1" cellpadding="2" cellspacing="0" width="472">
<tbody>
<tr>
<td valign="top" width="126">Description:</td>
<td valign="top" width="344">Job Processor extensions are for extending the Job Processor application.&#0160; Commonly used for creating job handlers for custom job types.</td>
</tr>
<tr>
<td valign="top" width="126">Interface:</td>
<td valign="top" width="344"><span style="font-size: xx-small;">Autodesk.Connectivity.JobProcessor.Extensibility.IJobHandler</span></td>
</tr>
<tr>
<td valign="top" width="126">ExtensionType in the .vcet.config:</td>
<td valign="top" width="344">JobProcessor</td>
</tr>
<tr>
<td valign="top" width="126">Loads in:</td>
<td valign="top" width="344">JobProcessor.exe</td>
</tr>
</tbody>
</table>
<p><strong>Web Service Command Extensions:</strong></p>
<table border="1" cellpadding="2" cellspacing="0" width="472">
<tbody>
<tr>
<td valign="top" width="126">Description:</td>
<td valign="top" width="344">Web Service Command extensions are for responding to events from any Vault client running on a computer.</td>
</tr>
<tr>
<td valign="top" width="126">Interface:</td>
<td valign="top" width="344">Autodesk.Connectivity.WebServices.IWebServiceExtension</td>
</tr>
<tr>
<td valign="top" width="126">ExtensionType in the .vcet.config:</td>
<td valign="top" width="344">WebService</td>
</tr>
<tr>
<td valign="top" width="126">Loads in:</td>
<td valign="top" width="344">Any application using Autodesk.Connectivity.WebServices.dll on that computer.</td>
</tr>
</tbody>
</table>
<p><strong>Mixing and matching:      <br /></strong>It&#39;s possible to have a DLL to implement more than 1 extension type.&#0160; In fact, this works really well with the Job Processor and Vault Client extension types.&#0160; Mixing the Web Service extension type with other types is trickier because you don&#39;t know the application context.&#0160; As a result, the DLLs from the other extension type might not be available to the running application.&#0160;&#0160; <br />Job Processor and Vault Client work well together because they have the same working folder, and therefore, have access to the same DLLs.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
