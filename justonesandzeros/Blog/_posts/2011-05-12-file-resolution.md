---
layout: "post"
title: "File Resolution"
date: "2011-05-12 08:39:46"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/05/file-resolution.html "
typepad_basename: "file-resolution"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts2.png" /></p>
<p><strong>Update:</strong>&#0160; There is <a href="http://www.youtube.com/watch?v=lMqn_kSlHH8&amp;feature=player_profilepage" target="_self">a great video</a> explaining the end-user experience of this feature.&#0160; Contine reading for the technical details that make this feature possible.</p>
<p>File Associations have gone through some changes for Vault 2012.&#0160; Most of the changes are around a new feature which I will refer to as <strong>File Resolution</strong>.&#0160; Basically, the feature is about fixing up broken file references if a file is moved or renamed.</p>
<p>To enable this feature, we needed to add extra meta-data to file associations.&#0160; This meta-data is used to A) detect when a reference is broken and B) provide information necessary to fix the reference.</p>
<p>The first change you will notice is that AddFile and CheckinFile methods are different.&#0160; Instead of passing in 4 parameters (dependFileIds, dependSources, attachFileIds, attachSources) related to file associations, there is now only 1 parameter (associations).&#0160;</p>
<p>The association parameter is an array of FileAssocParam objects.&#0160; The FileAssocParam class contains the child file ID, the source meta data and the association type (attachment or dependency).&#0160; So it contains all the information from before, just in a different form.&#0160; The new pieces of information are the <strong>RefId</strong> and the <strong>ExpectedVaultPath</strong> properties.&#0160;</p>
<p>RefId is metadata indicating how to locate the reference within the parent file.&#0160; This data is file-type specific and is not something you want to set yourself.&#0160; The CAD plug-in will set this up when files are added in.&#0160; If you are doing an update, you need to re-specify the associations at check in, so just pass in the RefIds from the associations on the previous version.</p>
<p>ExpectedVaultPath is the vault location of the child file.&#0160; This piece is used to detect when moves or renames happen.&#0160; The word <em>expected</em> is there because you can never be 100% sure of that location.&#0160; Because Vault is a multi-user environment, it&#39;s possible that someone else renamed that child right before you do yout check-in.&#0160; The system is architected so that you don&#39;t need to know the exact path at the moment of check-in.&#0160; You just need to know the path that the parent is <em>expecting</em> the child to be in.&#0160; The File Resolve framework will do the rest.</p>
<p>If you are reading file associations on existing file, there some new parameters on FileAssoc.&#0160; The ExpectedVaultPath shows up here.&#0160; There is also a new property called VaultPathChanged.&#0160; This property is a boolean that indicates that broken reference has been found.&#0160; If the current Vault path is not equal to the expected Vault path for either the parent or the child, the value will be TRUE.&#0160;</p>
<p>When broken references are detected, components called Resolvers are in charge of fixing up the references.&#0160; Resolvers run <strong>client-side</strong> and are file-type specific.&#0160; Currently we only have resolvers for AutoCAD, Inventor and Navisworks files.&#0160; All other files cannot participate in the file resolution feature.</p>
<p>A Resolver is not something you should be dealing with directly. The new utility Autodesk.Connectivity.Explorer.ExtensibilityTools.IExplorerUtil contains DownloadFile() functions that will do the download and run the Resolver.&#0160; However, the older function Autodesk.Connectivity.WebServices.DocumentService.DownloadFile() will <strong>not</strong> run the resolver.&#0160; The reason is that the WebServices DLL is about server communication, not client-side operations.&#0160;</p>
<p>If you used checksum in the past to locate files, that&#39;s no longer a good mechanism.&#0160; Because the files are modified client-side, the checksum will no longer match the Vault checksum.&#0160; So you need to use things like filename, path, and last modified date to match between local and Vault files.&#0160; If you feel you absolutely need checksum capability, let me know and I will find you a workaround.</p>
<p>The File Resolution feature is a difficult one to consume from an API standpoint, but the end user experience is a very positive one.&#0160; In earlier versions of vault it was very difficult for a user to move and rename files, especially if a file was used in many places.&#0160; Under the new model, files can be easily and quickly moved and renamed.</p>
