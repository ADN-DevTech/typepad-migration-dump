---
layout: "post"
title: "Top 5 Awesome things in the Vault 2011 SDK"
date: "2010-03-31 07:51:06"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/top-5-awesome-things-in-the-vault-2011-sdk.html "
typepad_basename: "top-5-awesome-things-in-the-vault-2011-sdk"
typepad_status: "Publish"
---

<p><strong><img src="/assets/Concepts2.png" /> </strong></p> <ol>
  <li><strong>Vault Client API</strong> - Add custom commands and tab views to the Vault Explorer client. </li>
  <li><strong>Job Processor API</strong> - Write handlers for custom job types and plug the handler into JobProcessor.exe. </li>
  <li><strong>Autodesk.Connectivity.WebServices.dll</strong> - All web services are packaged up into a single DLL.&#0160; So just reference this DLL instead of setting up multiple web references.&#0160; Also, this fixes WSE and namespace headaches. </li>
  <li><strong>Lifecycle Event Editor</strong> - Configure ADMS to add a job to the queue automatically when a file changes lifecycle state.&#0160; </li>
  <li><strong>Job Server is now included with Vault Workgroup 2011.</strong>&#0160; <br />&#0160;</li>
 </ol>
 <p>I realized that I never did a proper matrix on which APIs go with which 2011 versions of Vault.&#0160; So let me correct that here:</p> <table border="1" cellpadding="2" cellspacing="0" width="450"><tbody>   <tr>    <td valign="top" width="123">&#0160;</td>    <td valign="top" width="57">Vault</td>    <td valign="top" width="90">Vault Workgroup</td>    <td valign="top" width="90">Vault Collaboration</td>    <td valign="top" width="90">Vault Professional</td>   </tr>   <tr>    <td valign="top" width="123">Web Services API</td>    <td valign="top" width="57">Yes</td>    <td valign="top" width="90">Yes</td>    <td valign="top" width="90">Yes</td>    <td valign="top" width="90">Yes</td>   </tr>   <tr>    <td valign="top" width="123">Vault Client API</td>    <td valign="top" width="57">No</td>    <td valign="top" width="90">Yes</td>    <td valign="top" width="90">Yes</td>    <td valign="top" width="90">Yes</td>   </tr>   <tr>    <td valign="top" width="123">Job Processor API</td>    <td valign="top" width="57">No</td>    <td valign="top" width="90">Yes</td>    <td valign="top" width="90">Yes</td>    <td valign="top" width="90">Yes</td>   </tr>  </tbody></table>
