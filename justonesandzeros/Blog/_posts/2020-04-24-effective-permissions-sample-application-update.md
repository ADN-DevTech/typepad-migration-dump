---
layout: "post"
title: "Effective Permissions Sample Application Update!"
date: "2020-04-24 16:51:51"
author: "Jeffrey Fishman"
categories:
  - "Sample Applications"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2020/04/effective-permissions-sample-application-update.html "
typepad_basename: "effective-permissions-sample-application-update"
typepad_status: "Publish"
---

<p><img src="/assets/Vault4.png" /> <br /><img src="/assets/SampleApp4.png" /></p>
<p>Hi Folks,</p>
<p>My name is Jeffrey Fishman. I&#39;m a software engineer with the Vault team, and I&#39;m hoping to bring along some goodies you&#39;ll find interesting! I&#39;ll include a photo of myself below:</p>
<p><img height="430" src="/assets/JeffreyFishman.jpg" width="232" /></p>
<p>The Effective Permissions sample application has been updated to work with the Vault 2021 SDK and client. Since the client now has a native tab to view effective permissions per object, that functionality was removed from the sample application. It&#39;ll simply show a permissions matrix users have for each Vault folder, depending on how you&#39;ve configured the sample application settings.</p>
<p>For those who haven&#39;t tried this sample app yet, it calculates which permissions a user will ultimately have for viewing and manipulating a folder in Vault. You can configure the folder depth you wish to be displayed in the matrix, starting from the Vault&#39;s root folder ($).&#0160; You can also configure whether you&#39;d like permissions to be displayed for users who don&#39;t have access to the current vault but may be available on the Vault Server, or users who have been disabled on the Vault. The last settings available are folder exclusions and inclusions. The blacklist takes higher precedence over the inclusion, and the inclusion allows you to add folders to the results that may be out of the configured folder depth&#39;s scope.</p>
<p>As a quick side note -- you&#39;ll want to add in a key to sign the sample code if you wish to compile it. Visual Studio allows you to do this right inside of the IDE by right-clicking on the assembly, selecting &#39;Properties&#39;, and then moving to the &#39;Signing&#39; section on the left of the assembly properties window.&#0160;</p>
<p>Outside of Visual Studio, the .NET Framework provides a utility to <a href="https://docs.microsoft.com/en-us/dotnet/framework/tools/sn-exe-strong-name-tool?redirectedfrom=MSDN#examples">generate strong-name key files you can find by clicking here.</a></p>
<p>&#0160;</p>
<hr />
<h3>Screenshots of update:</h3>
<p><img height="307" src="/assets/EffectivePermissionsLocation.png" width="468" /> <img height="306" src="/assets/EffectivePermissionsOverview.png" width="306" /></p>
<hr />
<h3><span class="asset  asset-generic at-xid-6a0120a5728249970b0240a4fff735200d img-responsive">Demo Video:</span></h3>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="315" src="https://www.youtube.com/embed/-6lVFBODEK0" width="560"></iframe></p>
<p>&#0160;</p>
<hr />
<h3><span class="asset  asset-generic at-xid-6a0120a5728249970b0240a4fff735200d img-responsive">Requirements:</span></h3>
<ul>
<li><span class="asset  asset-generic at-xid-6a0120a5728249970b0240a4fff735200d img-responsive">Vault 2021 Workgroup or Professional</span></li>
<li><span class="asset  asset-generic at-xid-6a0120a5728249970b0240a4fff735200d img-responsive">Administrative rights on the Vault</span></li>
</ul>
<p><span class="asset  asset-generic at-xid-6a0120a5728249970b0240a4fff735200d img-responsive"><a href="https://justonesandzeros.typepad.com/Apps/EffectiveFolderPermissions/effectivePermissions-6.0.0.0-bin.zip">Click here to download the application</a><a href="https://justonesandzeros.typepad.com/Apps/EffectiveFolderPermissions/effectivePermissions-6.0.0.0-src.zip"><br />Click here to download the source code</a></span></p>
<p><a href="https://justonesandzeros.typepad.com/blog/2013/10/effective-permissions-2014.html">Click here to go to the previous 2014 version</a></p>
