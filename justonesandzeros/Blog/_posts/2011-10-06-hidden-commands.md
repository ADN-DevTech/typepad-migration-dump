---
layout: "post"
title: "Hidden Commands"
date: "2011-10-06 09:10:26"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/10/hidden-commands.html "
typepad_basename: "hidden-commands"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>If you have developed an extension to Vault Explorer, you probably noticed the HiddenCommands() function in the IExtension interface.&#0160; The theory behind this function is that your extension can overwrite a command by hiding it from the menus and providing a replacement command.&#0160; It&#39;s a good theory on paper, but does it hold up in practice?</p>
<p>I&#39;ve written two apps that make use of this feature, with varying degrees of success.&#0160;</p>
<p><a href="http://justonesandzeros.typepad.com/blog/2010/06/the-recycle-bin-20.html">Recycle Bin 2.0</a> hides the Delete command in an attempt to force a new process.&#0160; Things didn&#39;t work out too well when I deployed this on our internal Vault.&#0160; Delete is a pretty fundamental operation.&#0160; If you are going to remove something like that, you&#39;d better have something good as a replacement.&#0160; Unfortunately, Recycle Bin wasn&#39;t up to the challenge, and I reverted things after a few weeks.</p>
<p><a href="http://justonesandzeros.typepad.com/blog/2011/06/hyperlink-maestro.html">Hyperlink Maestro</a> hides the Copy Hyperlink command and replaces it with a better one.&#0160; This time things worked out better.&#0160; Mainly because the new function does everything the old one does and more.&#0160; So end users are not losing anything by having the extension installed.&#0160; Personally, I can&#39;t go back to the default command.&#0160; I&#39;m hooked on Maestro.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>Here are some alternatives to hiding a command:</p>
<ul>
<li>Place your new command alongside the default command.&#0160; </li>
<li>Use events to work with the existing command instead of trying to hide it. </li>
</ul>
<p><br />If you still want to use the hidden commands feature, here are some best practices:</p>
<ul>
<li>Don&#39;t hide a function unless you can provide a superior replacement. </li>
<li>Don&#39;t override a function unless you fully understand it&#39;s functionality.&#0160; Even something that seems simple, like Delete, can have uses that you didn&#39;t consider. </li>
<li>Most functions are too cost prohibitive to override.&#0160; Copy Hyperlink was easy to do, but something like Copy Design would take months to write. </li>
</ul>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
