---
layout: "post"
title: "Get Working Folder by Vault API"
date: "2012-08-19 20:18:25"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/get-working-folder-by-vault-api.html "
typepad_basename: "get-working-folder-by-vault-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>
<p><strong>Issue</strong> <br />From Vault Explorer or Inventor, you can map local folder (e.g. C:\Cad\parts) to Vault folder (e.g. $\StandardParts) through using Map Folders command under Access panel in Vault ribbon tab in Inventor. Is it possible to get the mapped local folder path through API? I would need this to know where I have to store Files downloaded via the Vault method “downloadFile(...)” (Storing it under the wrong path results in Vault not working correctly).     <br /><strong>Solution</strong></p>
<p>You can call DesignProject.VirtualVaultPath from Inventor API to get the mapped Vault folder information, as shown in below picture:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01630631c56b970d-pi"><img alt="Screen shot 2012-06-07 at 11.44.16 AM" border="0" height="404" src="/assets/image_66afb8.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="Screen shot 2012-06-07 at 11.44.16 AM" width="482" /></a></p>
<p>Once the mapped Vault folder information is set up, call GetWorkingFolder from Vault API to get the local path for the mapped Vault folder (e.g. above displayed “$/ProjectName1”).</p>
<table border="1" cellpadding="2" cellspacing="0" width="485">
<tbody>
<tr>
<td valign="top" width="483"><strong>Tip</strong></td>
</tr>
<tr>
<td valign="top" width="483">
<p style="margin: 0cm 0cm 0pt;">GetWorkingFolder() is provided by IExplorerUtil interface from Autodesk.Connectivity.Explorer.Extensibility.dll. IExplorerUtil interface can be achieved by ExplorerLoader.GetExplorerUtil or ExplorerLoader.LoadExplorerUtil. For more detailed information, please see Vault API help document. The following is the sample code snippet:</p>
<pre><p style="margin: 0cm 0cm 0pt;"><span lang="EN-US">&#0160;</span></p><p style="margin: 0cm 0cm 0pt;"><span lang="EN-US"><span style="font-size: xx-small;">IExplorerUtil VaultExplorer = ExplorerLoader.</span></span></p><p style="margin: 0cm 0cm 0pt;"><span lang="EN-US"><span style="font-size: xx-small;">GetExplorerUtil(application);<br />string filePath = VaultExplorer.GetWorkingFolder</span></span></p><p style="margin: 0cm 0cm 0pt;"><span lang="EN-US"><span style="font-size: xx-small;">(folders[0]) + latestFile.Name;</span><br /></span></p></pre>
<p style="margin: 0cm 0cm 0pt;"><span lang="EN-US">&#0160;</span></p>
<p style="margin: 0cm 0cm 0pt;"><span lang="EN-US">This function has a problem in Vault 2012 shipping release and there is a fix available for download on Doug Redmond’s blog:</span></p>
<p style="margin: 0cm 0cm 0pt;"><span lang="EN-US">&#0160;</span></p>
<p style="margin: 0cm 0cm 0pt;"><span lang="EN-US">“2 API-Related Hotfixes”</span></p>
<p style="margin: 0cm 0cm 0pt;"><span lang="EN-US"><a href="http://justonesandzeros.typepad.com/blog/2011/06/2-api-related-hotfixes.html">http://justonesandzeros.typepad.com/blog/2011/06/2-api-related-hotfixes.html</a></span></p>
</td>
</tr>
</tbody>
</table>
<p>Another thing that needs to be handled is, if the Vault cleint is set up with the enforced working folder, then the GetEnforceWorkingFolder API would return true, you then should Call GetRequiredWorkingFolderLocation to retrieve the real mapped folder path, instead of using the information from WorkingFolders.xml because the later doesn’t help in this case.</p>
<p>Note:</p>
<p>For versions older than Inventor 2011, there is no DesignProject.VirtualVaultPath API that returns the mapped Vault folder set in the Inventor project. Let’s look at a workaround descript as the following:   <br />*****    <br /><span style="color: #4f81bd;">The correct place to get the Vault mapped folder is to read the Inventor’s IPJ file for Inventor IPJ file contains the mapping between Vault path and local workspace path.? Inventor project file is XML format, so you can rename .ipj file to .xml file then read/edit the file. If the project is Vault project, then you will find VaultOptions node in the project file, if the mapping path exist, you will see , you will see VaultVirtualFolder such as the following:</span></p>
<p><span style="color: #4f81bd;">localhost      <br /> Vault       <br /> $/ProjectName1 </span></p>
<p><span style="color: #809ec2;"><span style="color: #4f81bd;">However the .ipj file format might be subject to any change in future releases without notice (so that we don’t officially publish its format). We don’t support direct access to the .ipj file. Please note that my above suggestion might be no longer able to work in future release.</span> <br /></span>*****    <br />With Vault 2011 and older versions, there is no GetWorkingFolder API. The following describes a workaround that can get the mapped folder information by indirect way:    <br />*****&#0160; <br /><span style="color: #4f81bd;">The Vault to local folder mapping can be found in a file called WorkingFolders.xml. Actually there can be several of these files. Different users/servers/vaults cause more of these files to pop up. Here is the path:</span></p>
<p><span style="color: #4f81bd;">C:\Documents and Settings\[Windows user]\Application Data\Autodesk\VaultCommon\Servers\[ADMS Version]\[Server]\Vaults\[Vault Name]\Objects</span></p>
<p><span style="color: #4f81bd;">For example:     <br />C:\Documents and Settings\Administrator\Application Data\Autodesk\VaultCommon\Servers\Services_Security_01_17_2008\localhost\Vaults\Vault\Objects</span></p>
<ul>
<li><span style="color: #4f81bd;">Administrator: The Windows user</span></li>
<li><span style="color: #4f81bd;">Services_Security_01_17_2008: The signature (version) of the security service. (1/17/2008 = ADMS 2009)</span></li>
<li><span style="color: #4f81bd;">Localhost: The vault server location</span></li>
<li><span style="color: #4f81bd;">Vault: The name of the Vault</span></li>
</ul>
<p><span style="color: #4f81bd;">Please only read this file. Don’t edit it. The customized application can read in this file and figure out where to copy the files so that they show up in the correct spot when the user fires up Vault Explorer.     <br /></span>*****&#0160;</p>
