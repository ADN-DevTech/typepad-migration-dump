---
layout: "post"
title: "Generating File Names"
date: "2013-02-15 07:51:28"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2013/02/generating-file-names.html "
typepad_basename: "generating-file-names"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>The Vault API is sometimes redundant, and it can also repeat itself.&#0160; One place were this is painfully obvious is with file name generation.&#0160; If you want to generate a file name, there are two completely separate mechanisms to choose from.&#0160; Each has its own set of API functions with their own sets of pros and cons.&#0160; You have to choose which is best for your needs.&#0160; There is no one mechanism to rule them all.</p>
<p>To make matters worse, they have similar names.&#0160; Once engine is for file <strong>naming</strong> schemes and the other is for file <strong>numbering</strong> schemes.&#0160; If you are not paying attention, you may mistake one word for the other.&#0160; It’s annoying, I know.&#0160; It’s like reading a three volume fantasy epic where one villain is called Sauron and the other is called Saruman.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>File Naming Schemes:</strong></p>
<p>This is a pretty basic mechanism, so it&#39;s good if you want something quick and simple.&#0160; It consists of an auto-increment number with a string prefix and suffix.&#0160; I believe that Copy Design uses this feature.</p>
<p>One nice feature is that you can “rollback” a name if you decide not to use it.&#0160;&#0160; For example, the user hits Cancel on your dialog.&#0160; </p>
<p>Functions for generating names:</p>
<ul>
<li>GetAllFileNamingSchemes </li>
<li>ReserveFileNamingDescriptions </li>
<li>RollbackFileNamingDescriptions </li>
</ul>
<p>Admin functions:</p>
<ul>
<li>AddFileNamingScheme </li>
<li>DeleteFileNamingScheme </li>
<li>SetDefaultFileNamingScheme </li>
<li>UpdateFileNamingScheme </li>
</ul>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>File Numbering Schemes:</strong></p>
<p>This is much more complex.&#0160; You chain together different field types to create your naming pattern.&#0160; It’s more powerful, but it’s also more difficult to work with.&#0160; Items and Change Orders use this engine too.&#0160; </p>
<p>Unfortunately, there is no way to “rollback” a number.&#0160; Once it’s generated, it’s reserved forever.</p>
<p>Functions for generating names:</p>
<ul>
<li>GenerateFileNumber </li>
<li>GenerateFileNumbers </li>
<li>GetNumberingSchemesByType </li>
</ul>
<p>Admin functions:</p>
<ul>
<li>ActivateNumberingSchemes </li>
<li>DeactivateNumberingSchemes </li>
<li>DeleteNumberingScheme </li>
<li>DeleteNumberingSchemeUnconditional </li>
<li>SetDefaultNumberingScheme </li>
<li>UpdateNumberingScheme </li>
</ul>
<p>Here are some screenshots from Vault Explorer that illustrate how fields are hooked together to form the numbering scheme.</p>
<p><img alt="" src="/assets/dialog1.png" /></p>
<p><img alt="" src="/assets/dialog2.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
