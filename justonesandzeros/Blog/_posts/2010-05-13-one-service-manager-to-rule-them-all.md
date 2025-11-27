---
layout: "post"
title: "One Service Manager to Rule Them All"
date: "2010-05-13 08:46:33"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/05/one-service-manager-to-rule-them-all.html "
typepad_basename: "one-service-manager-to-rule-them-all"
typepad_status: "Publish"
---

<p><img src="/assets/TipsAndTricks2.png" /> </p> <p>If you have looked at the code for the SDK or blog samples, you probably noticed a class called ServiceManager, which acts as a factory for creating service objects.&#0160; It&#39;s very useful for things like setting the URL and security information each time a service object is created.&#0160; </p> <p>The only problem is that no two ServiceManagers are the same.&#0160; I would end up coping it from project to project.&#0160; Then I would modify it for that specific app.&#0160; For example, some apps would have GetItemService while other would now.&#0160; Some Service managers allowed multiple logins while others assumed a single login.</p> <p>So, I decided to create a single ServiceManager that could be used with any app without the need for modification.&#0160; I put in in a separate DLL called JustOnesAndZeros.WebServices.dll and added it to my <a href="http://justonesandzeros.typepad.com/blog/2010/04/effective-folder-permissions-20.html">Effective Folder Permissions 2.0</a> and <a href="http://justonesandzeros.typepad.com/blog/2010/05/watch-folder.html">Watch Folder</a> samples.&#0160; </p> <p>Features:</p> <ul>
  <li>A single function, GetService() works for all web services classes in the Vault API. </li>
  <li>Manages the security context for all services it creates. </li>
  <li>Recognizes VaultContext objects from the Vault Client or Job Processor APIs. </li>
  <li>Can handle multiple simultaneous logins. </li>
 </ul>
 <table border="1" cellpadding="2" cellspacing="0" width="450"><tbody>   <tr>    <td valign="top" width="450">Note:&#0160; I titled the DLL after my blog, not after my company.&#0160; I deliberately avoided using the word &quot;Autodesk&quot; to make it clear that this is my own set of utilities and the contents of the DLL are not part of the official Vault SDK.&#0160; </td>   </tr>  </tbody></table> <p>Feel free to use ServiceManager or any other items from JustOnesAndZeros.WebServices.dll.&#0160; But I recommend copying the code into your project instead of linking to the DLL.&#0160; I will be constantly updating the DLL as new samples come out.&#0160; So it&#39;s not a very stable thing to link to.</p>
