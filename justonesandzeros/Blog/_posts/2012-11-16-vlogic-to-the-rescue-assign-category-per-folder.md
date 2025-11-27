---
layout: "post"
title: "vLogic to the Rescue - Assign Category per Folder"
date: "2012-11-16 13:42:18"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/11/vlogic-to-the-rescue-assign-category-per-folder.html "
typepad_basename: "vlogic-to-the-rescue-assign-category-per-folder"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>I created vLogic to be a useful tool in a variety of situations.&#0160; It allows for customization without the overhead of a full-blown .NET DLL.&#0160; A lot of people think vLogic is a good idea, but I’m not aware of it being use in any real capacity.&#0160; As we all know, there is sometimes a big gap between “good idea” and “useful”.&#0160; So I though I would spend a few posts taking a crack at some real-word problems using vLogic.</p>
<p>This week’s post will be addressing an entry on the <a href="http://forums.autodesk.com/t5/Autodesk-Vault-IdeaStation/Assign-Category-per-Folder/idi-p/3609610" target="_blank">Vault Idea Exchange</a>.&#0160; A user wants to “<em>Have a category automatically assigned to a document, based on the folder it is &#39;imported&#39; to.&#0160; This would work only when there is a new document, or not another category, assigned to the document.”</em></p>
<p>Yes, I think I can do that with vLogic.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Requirements:</strong>&#0160; <br />Vault Workgroup/Collaboration/Professional 2013 and <a href="http://justonesandzeros.typepad.com/blog/2012/03/vlogic-2013.html" target="_blank">vLogic 2013</a></p>
<p><strong>Steps:</strong>&#0160; </p>
<ol>
<li>Install vLogic if not installed already. </li>
<li>Download the <strong><a href="http://justonesandzeros.typepad.com/Files/vLogicScripts/Events/AddFilePost.SetToParentCategory.ps1" target="_blank">AddFilePost.SetToParentCategory.ps1</a></strong> script and put it in the vLogic’s “Event Scripts” folder.&#0160; The folder can be found at <strong>%ProgramData%\Autodesk\Vault 2013\Extensions\vLogic\Event Scripts</strong> with Windows Explorer. </li>
<li>If “Base” is not your default file category, open up the .ps1 script and edit the value for $BaseCategory. </li>
<li>Start or restart Vault Explorer. </li>
<li>Set up File and Folder categories with matching names for cases where you want the script to run.&#0160; This step is needed because categories can’t be shared across Files and Folders.</li>
<li>Set a folder category and add a file to it through Vault Explorer.&#0160; If the file goes into the default category, the vLogic script will assign it to the file category with the same name as the folder’s category. </li>
</ol>
<p><strong>Notes:</strong></p>
<ul>
<li>The script only works for files added through Vault Explorer. </li>
<li>This script can only run on clients with vLogic and the .ps1 file.&#0160; <a href="http://justonesandzeros.typepad.com/blog/2012/08/project-thunderdome-2013.html" target="_blank">Project Thunderdome</a> can be used to deploy them both to multiple clients. </li>
</ul>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Source code:</strong></p>
<p>Here is the source code.&#0160; As you can see, there is not a lot of code needed here.    </p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p><span style="font-size: 8pt;"># This script updates new files not assigned a category are updated to match the category on the parent folder.              </span><br /><span style="font-size: 8pt;"># A manual refresh is needed to see the category change.               </span><br />&#0160; <br /><span style="font-size: 8pt;"># Set this value to the display name of the &quot;default&quot; file category               </span><br /><span style="font-size: 8pt;">$BaseCategory = &quot;Base&quot;&#0160; </span><br />&#0160; <br /><span style="font-size: 8pt;">if ($e.Status -eq [Autodesk.Connectivity.WebServices.EventStatus]::SUCCESS -and               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160; $e.ReturnValue.Cat -and               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160; $e.ReturnValue.Cat.CatName -eq $BaseCategory)               </span><br /><span style="font-size: 8pt;">{               </span><br /><span style="font-size: 8pt;">&#0160;&#0160; $parentFolder = $serviceManager.DocumentService.GetFolderById($e.FolderId)</span></p>
<p><span style="font-size: 8pt;">&#0160;&#0160; if ($parentFolder.Cat)              </span><br /><span style="font-size: 8pt;">&#0160;&#0160; {               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; $fileCats = $serviceManager.CategoryService.GetCategoriesByEntityClassId(&quot;FILE&quot;, $true)               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; $fileCatId = $fileCats |               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Where-Object {$_.Name -eq $parentFolder.Cat.CatName} |               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Select-Object -ExpandProperty Id               </span><br />&#0160;&#0160;&#0160;&#0160;&#0160; <br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; if ($fileCatId)               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; {               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $temp = $serviceManager.DocumentServiceExtensions.UpdateFileCategories(@($e.ReturnValue.MasterId), @($fileCatId),               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;Setting category to match parent folder&quot;)               </span><br /><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160; }               </span><br /><span style="font-size: 8pt;">&#0160;&#0160; }               </span><br /><span style="font-size: 8pt;">}              </span> </p>
</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
