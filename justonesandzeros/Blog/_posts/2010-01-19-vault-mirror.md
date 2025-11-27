---
layout: "post"
title: "Vault Mirror"
date: "2010-01-19 17:09:38"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/01/vault-mirror.html "
typepad_basename: "vault-mirror"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp2.png" /></p>
<p>Vault Mirror holds a special place in my heart.&#0160; It&#39;s the first utility I ever released publicly, and it was one of the first utilities to be featured on Autodesk Labs.&#0160; It took me only a few hours to write, and I figured it would have a short lifetime.&#0160;</p>
<p>Much to my delight, the utility is still in use today by many people, and in ways that I never imagined.&#0160; Brian Schanen has already <a href="http://mfgcommunity.autodesk.com/blogs/blog/view/5/Vault_Mirror/">blogged</a> about this, but I want to add my own technical perspective.</p>
<p><img alt="" src="/assets/VaultMirror.png" /></p>
<p><strong>What is Vault Mirror?</strong> <br />It&#39;s a utility that downloads all the files from Vault for read-only purposes.&#0160; It was designed to be automated, lightweight and efficient.&#0160; It works with the base version of Vault, and it won&#39;t consume a license in the pay-for versions.</p>
<p>Vault Mirror does <strong>not</strong> create a perfect replica of a Vault.&#0160; It only downloads the latest version of the files.&#0160; Other things like items, properties, file history, security, lifecycle states etc. are not copied.</p>
<p><strong>Where can I get it?</strong> <br />It&#39;s installed with the Vault Server, but you have to dig a bit in the SDK to find it.&#0160; The folder location is <span style="color: #008080;">[ADMS dir]\SDK\VS8\CSharp\VaultMirror\bin\Release</span></p>
<p><span style="color: #008080;"><span style="color: #ff0000;">Update:&#0160; In Vault 2011, the utility has been moved to a new location [ADMS dir]\SDK\util\VaultMirror</span><br /></span></p>
<p>The readme (and source code) can be found two levels higher.&#0160; Feel free to copy things to a more convenient location.</p>
<p><strong>Uses for Vault Mirror:  <br /></strong>If you found a use not mentioned in the list below, please let us know by posting it in the comments section.</p>
<ul>
<li><strong>A basic version of replication.</strong>&#0160; You want to push files to a remote location or an outside group so that they can consume the files in read-only mode. </li>
<li><strong>A basic form of backup.</strong>&#0160; If you use Vault for strictly document storage, this is a good way to make backups.&#0160; As an added bonus, you can quickly restore a single file that gets deleted.&#0160; </li>
<li><strong>Copying files to a new Vault.</strong>&#0160; Use Vault Mirror to get the files, then use Autoloader to add them to the new Vault. </li>
<li><strong>Sample code.</strong>&#0160; Since the Vault API has no event model, you need to use polling to detect changes.&#0160; The Vault Mirror source code has a great example of how this works.&#0160; It&#39;s the sample that I always refer people to. </li>
</ul>
<p><strong>Full Mirror command:</strong> <br />The full mirror command will basically synchronize your windows folders to match the Vault folder.&#0160; Be warned, this includes deleting local files/folders if it there is not a match in the Vault.&#0160;</p>
<p>The algorithm works by going through the Vault folder-by-folder and comparing the Vault files with the files on disk.&#0160; If a file exists in the Vault but not on disk, the file is downloaded.&#0160; If a file exists on disk but not in the Vault, the local file is deleted.&#0160; If a file exists on disk and in the Vault, the checksum is compared.&#0160; If the local file has a different checksum as the Vault file, the local file is overwritten.</p>
<p>In practice, this command is only used to clean up things.&#0160; It may take a long time to run depending on the size of the Vault, so this command should be run rarely if ever.</p>
<p><strong>Partial Mirror command:</strong> <br />The partial mirror command will basically download all new files.&#0160; Any new file is downloaded.&#0160; No effort is made to clean up the local folder.</p>
<p>The algorithm works by doing a file search.&#0160; Vault Mirror remembers when the last, time a command was run, so it searches all file versions created after the time of the last search.&#0160; Any matching files are downloaded.</p>
<p>Since only new files are downloaded, this is a very scalable command.&#0160; It&#39;s no problem to run this command every few minutes so that your mirror folder is mostly up-to-date.</p>
<p><strong>Similar features:</strong> <br />Since its release, Vault has added a lot of features that I thought would make Vault Mirror obsolete.&#0160; However the utility is still in use.&#0160; Maybe people don&#39;t know about the other features, maybe they like Vault Mirror better, or maybe there is some aspect of the built-in feature that makes it unusable.&#0160; Whatever the reason, I thought I should list the features that Vault Mirror &quot;competes&quot; against.&#0160; That way you can choose the utility or feature that works best for you.</p>
<ul>
<li><strong>Workspace Sync</strong> - With a single click you can update your workspace to match the files in Vault.&#0160; It can also be run from the command line, so it&#39;s possible to automate things just like with Vault Mirror.&#0160; But Workspace Sync comes with many more options.</li>
<li><strong>Replication</strong> - This features creates many versions of a file store.&#0160; But unlike Vault Mirror, the replicated stores can be edited.&#0160; Vault users can run check-out, rename, copy design, and so on.&#0160; Basically any operation can be done.&#0160; To the user, it works just like a regular Vault.</li>
<li><strong>DWF Publish</strong> - This feature existed since the early versions of Vault.&#0160; You can set a Vault to publish DWF files to a folder share.&#0160; Basically it&#39;s Vault Mirror, but just for DWF files.&#0160; </li>
<li><strong>Hot Backup</strong> - This feature lets you back up the Vault with out having to take Vault offline.&#0160; With this type of backup, you get everything.&#0160; File history, meta-data, items, etc.</li>
</ul>
