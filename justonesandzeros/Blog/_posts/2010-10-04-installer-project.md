---
layout: "post"
title: "Installer Project"
date: "2010-10-04 10:23:00"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/10/installer-project.html "
typepad_basename: "installer-project"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p> <p>One of the worst tasks you can get as a programmer is having to write an installer.&#0160; Installers are hard to write, hard to debug and hard to maintain.&#0160; What&#39;s worse is that nobody appreciates all the hard work you did.&#0160; As far as the end user (or your boss) is concerned, an installer is either &quot;adequate&quot; or &quot;a complete piece of garbage&quot;.&#0160; Those are the only 2 states.&#0160; There is no in-between and no way to go above adequate.&#0160; Nobody ever says, &quot;Wow! What a great installer!&quot;</p> <p>And don&#39;t even get me started on the MSI framework.</p> <p>Anyway, you may have noticed that most of my samples are deployed via an installer.&#0160; So I&#39;ve posted a simple Visual Studio 2008 installer project that you can use to deploy your custom Vault Explorer extensions.</p> <p><strong>Features:</strong></p> <ul>
  <li>Detects if Vault Collaboration, Workgroup or Professional client is installed and sets the [VAULTWGDIR], [VAULTCOLLABDIR] and [VAULTPRODIR] paths respectively.&#0160; If the path is empty, then it means that the client is not installed, and the path will be empty.</li>
  <li>Installer will not work if it doesn&#39;t detect one of the above mentioned Vault clients.</li>
  <li>Performs a menu reset, so your custom command <em>should</em> show up after the installer runs and your restart Vault Explorer.</li>
 </ul>
 <p>&#0160;<a href="http://justonesandzeros.typepad.com/Apps/Installer/VaultAddInInstaller2011.zip" target="_blank">Click here to download the installer project</a></p> <p><strong>To use:</strong></p> <p>You can look at the MSDN documentation on how to work with installer projects.&#0160; Also, Kean Walmsley posted a great 3 part series on installers on <a href="http://through-the-interface.typepad.com/through_the_interface/2010/09/building-an-installer-part-1.html" target="_blank">his blog</a>.&#0160; </p> <p>To use my installer project, you basically have to change the parts that say &quot;ChangeMe&quot;.&#0160; Next you add your files to the appropriate folders.&#0160; For example, if you are supporting all Vault Collaboration and Vault Professional, then you would add your extension files to both of those locations.&#0160;&#0160;&#0160; </p> <p>Make sure to set the appropriate condition for each file you are installing.&#0160; For example, all Vault Collaboration files should have [VAULTCOLLABDIR] as the condition.</p> <p>You may want to update the Vault2011 launch condition if you are not supporting all 3 customizable Vault clients.</p> <br /> <p><strong>Warning:</strong> Use of this project may result in you getting stuck as the &quot;Installer Guy&quot;.</p>
