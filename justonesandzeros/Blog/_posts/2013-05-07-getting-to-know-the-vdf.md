---
layout: "post"
title: "Getting to Know the VDF"
date: "2013-05-07 15:27:06"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/05/getting-to-know-the-vdf.html "
typepad_basename: "getting-to-know-the-vdf"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>The VDF will feel a lot different than the rest of the Vault API.&#0160; It’s almost as if it was built by a completely different team.&#0160; The reason is because it was built by a completely different team.&#0160; There are other reasons too.&#0160; Working with UI components is different than working with server calls.&#0160; Also, the VDF is able to follow a more object oriented approach than the web service layer.</p>
<p>But I&#39;m getting ahead of myself.&#0160; This post is about getting to know the VDF.&#0160; No talk of commitment or marriage or anything.&#0160; It&#39;s just a nice casual tour of the major components.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>The DLLs</strong></p>
<p>There are 4 DLLs that make up the VDF.&#0160; They are the ones that start with Autodesk.DataManagement.</p>
<p><strong>Autodesk.DataManagement.Client.Framework.dll</strong> - This contains components that are not Vault-specific and do not contain UI.&#0160; Features include logging and error parsing.</p>
<p><strong>Autodesk.DataManagement.Client.Framework.Forms.dll</strong> - This contains UI components that are not Vault-specific.&#0160; Features include an error dialog and progress dialog.</p>
<p><strong>Autodesk.DataManagement.Client.Framework.Vault.dll</strong> - This contains Vault utilities but no UI.&#0160; Features include Connection management, download/upload workflows, working folder information, and Entity objects.</p>
<p><strong>Autodesk.DataManagement.Client.Framework.Vault.Forms.dll</strong> - This contains Vault UI.&#0160; Features include the login dialog, browse control, and checkout dialog.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Entry Points</strong></p>
<p>Each DLL has a <strong>Library</strong> object which contains a set of static methods.&#0160; The methods themselves are dependent on the feature set of the DLL.&#0160; The other major entry point is the <strong>Connection</strong> object, which has a set of managers.&#0160; Each manager contains methods for invoking high level workflows.&#0160; There is some overlap between VDF managers and web service managers.&#0160; For example, you can add a category through Connection.CategoryManager.AddCategory or Connection.WebServiceManager.CategoryService.AddCategory.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Currency and Services</strong></p>
<p>The VDF has its own set of <strong>currency</strong> objects.&#0160; These are basic objects used only for structured data.&#0160; Objects that perform actions are referred to as <strong>services</strong> (aka. <strong>managers</strong>).&#0160; VDF currency is similar to the Web Service currency, but there is usually more information in the VDF currency.&#0160; For example, the VDF’s FileIteration object has object references to the parent Folder and Category object reference.&#0160; The web service equivalent, File, has only ID references to parent Folder and Category.</p>
<p>You will frequently have to convert between the VDF and web service currency.&#0160; You can construct a VDF object from a web service object.&#0160; You can also cast down from VDF to web service object.&#0160; So it’s not too bad, but you still have to keep track of the different sets of currency.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Settings and Results</strong></p>
<p>The VDF supports many complex workflows, which may have multiple configuration options.&#0160; Instead of having functions with 20+ inputs, the VDF uses a <strong>settings</strong> object instead.&#0160; If you want the default behavior, you can just new up a settings object and pass it in to the workflow or UI.&#0160; Alter the settings object if you want non-default behavior.</p>
<p>The results of a workflow can be just as complex as the inputs, which is where the <strong>results</strong> object comes in.&#0160; If something goes wrong, don’t assume you will get an exception.&#0160; In many cases, you need to check the results object to see if there were failures or not.&#0160; Also, when UI is involved, you can’t assume that the user did what they were supposed to do.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
