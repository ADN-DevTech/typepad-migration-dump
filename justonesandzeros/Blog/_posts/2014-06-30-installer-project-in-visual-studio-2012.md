---
layout: "post"
title: "Installer Project in Visual Studio 2012"
date: "2014-06-30 17:16:45"
author: "Doug Redmond"
categories:
  - "Sample Applications"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/06/installer-project-in-visual-studio-2012.html "
typepad_basename: "installer-project-in-visual-studio-2012"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/SampleApp4.png" /></p>
<p>In the past, I posted the <a href="http://justonesandzeros.typepad.com/blog/2012/02/installer-project.html" target="_blank">installer project</a> I used to build Vault apps.&#0160; However, that project was for Visual Studio 2010.&#0160; In the 2012 version, Microsoft dropped the installer project type and replaced it with an Install Shield project type.&#0160; So I’ll share with you my latest installer template.&#0160; You do not need to purchase Install Shield.</p>
<p><a href="http://justonesandzeros.typepad.com/Apps/Installer/InstallerProject-VS2012.zip" target="_blank">Click here to download the project source</a></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>My recommendations on how to configure:</strong></p>
<p>In the solution explorer, you will see that Install Shield sets things up in numbered steps, so I’ll just go through the relevant parts, from the top on down.</p>
<p><img alt="" src="/assets/SolutionExplorer.png" /></p>
<p>I usually just skip over the Getting Started and Project Assistant stuff.&#0160; Personally, I found it more confusing than helpful.</p>
<ol>
<li>Organize Your Setup<ol>
<li>General Information<ol>
<li>Replace all the TODO parts.&#0160; Such as ProductName, INSTALLDIR, Subject, Author and Publisher.</li>
<li>Generate new values for Product Code and Upgrade Code.</li>
</ol></li>
</ol></li>
<li>Specify Application Data<ol>
<li>Files<ol>
<li>Rename the TODO folder if there is one.</li>
<li>Put all your extension files in the <strong>[CommonAppDataFolder]\Autodesk\Vault 2015\Extensions\YourAppName</strong> folder.</li>
<li>You can ignore the Database folder.</li>
<li>You don’t need to put anything in the [AppDataFolder] and [CommonFilesFolder]</li>
</ol></li>
</ol></li>
<li>Configure the Target System<ol>
<li>You can ignore this stuff.</li>
</ol></li>
<li>Customize the Setup Appearance<ol>
<li>Dialogs<ol>
<li>Check the dialogs that you want to show up during the install.</li>
</ol></li>
</ol></li>
<li>Define Setup Requirements and Actions<ol>
<li>You can ignore this stuff.</li>
</ol></li>
<li>Prepare for Release<ol>
<li>The SingleImage is the output type you probably want.&#0160; Here are the settings I use.</li>
</ol></li>
</ol>
<p><img alt="" src="/assets/BuildSettings.png" /></p>
<hr noshade="noshade" style="color: #013181;" />
<p>Now you are ready to build.&#0160; If all goes well, you will see your setup.exe file in your output folder, which is something like <strong>[projectFolder]\Express\SingleImage\DiskImages\DISK1</strong></p>
<p>Now all you have to do is zip up the EXE, post it on your blog, and wait for the money to roll in.&#0160;</p>
<hr noshade="noshade" style="color: #013181;" />
