---
layout: "post"
title: "Create Folder functionality in Vault API SelectEntity dialog"
date: "2018-06-27 05:01:12"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/06/create-folder-functionality-in-vault-api-selectentity-dialog.html "
typepad_basename: "create-folder-functionality-in-vault-api-selectentity-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p>
<p>You can add a ‘Create Folder’ command for the SelectEntity dialog, using the <strong>CreateFolder</strong> property in the <strong>SelectEntitySettings.SelectEntityOptionsExtensibility</strong> Class. If this property is not set then the ‘Create Folder’ button is not shown at all.</p>
<p>Once this property is set, you should be able to see the ‘Create Folder’ button as shown below.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39c4efd200b-pi"><img alt="image" border="0" height="524" src="/assets/image_5d0c9e.jpg" style="display: inline; background-image: none;" title="image" width="750" /></a></p>
<p><br /></p>
<p>Below is the code sample for reference. The easiest way to test the below code would be to add it in the VaultList sample that comes along with the Vault SDK.</p>
<pre>&#0160; try<br />&#0160;&#0160; {<br />&#0160;&#0160;&#0160; VDF.Vault.Forms.Settings.SelectEntitySettings settings =  new VDF.Vault.Forms.Settings.SelectEntitySettings();<br />&#0160;&#0160;&#0160; settings.OptionsExtensibility.CreateFolder = createFolder;
<p>VDF.Vault.Forms.Results.SelectEntityResults selresults = VDF.Vault.Forms.Library.SelectEntity (connection, settings);<br />&#0160;&#0160;&#0160;&#0160; }<br />&#0160; catch (Exception ex)<br />&#0160;&#0160; {<br />&#0160;&#0160;&#0160; MessageBox.Show(ex.ToString(), &quot;Error&quot;);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160; }<br />&#0160; VDF.Vault.Library.ConnectionManager.LogOut(connection);
</p></pre><pre>VDF.Vault.Currency.Entities.IEntity createFolder(VDF.Vault.Currency.Entities.IEntity dir)<br />&#0160;&#0160;&#0160;&#0160;&#0160; {<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.Folder parentfolder = dir as VDF.Vault.Currency.Entities.Folder;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VDF.Vault.Currency.Entities.Folder createdfolder = connection.FolderManager.CreateFolder(parentfolder, &quot;Demo_folder&quot;, false, EntityCategory.EmptyCategory);<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; return createdfolder;<br />&#0160;&#0160;&#0160;&#0160;&#0160; }</pre>
<p>When the &#39;Create folder&#39; button is pressed, you would find that the new folder has been created in the current location.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37c69a8200d-pi"><img alt="image" border="0" height="524" src="/assets/image_b8561d.jpg" style="display: inline; background-image: none;" title="image" width="750" /></a></p>
