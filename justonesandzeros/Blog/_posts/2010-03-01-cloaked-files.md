---
layout: "post"
title: "Cloaked Files"
date: "2010-03-01 08:37:20"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/cloaked-files.html "
typepad_basename: "cloaked-files"
typepad_status: "Publish"
---

<p><img src="/assets/TipsAndTricks2.png" /> </p> <p>Here on the Vault team, we like Star Trek as much as the next group of computer programmers, which partly explains how we came up with the name &quot;Cloaked&quot; for one of the File properties.&#0160; The concept is that there is a file in the Vault that the logged-in user does not have read access to.&#0160; At an API level, you can detect that the file exists, but you can&#39;t get any useful information from it.&#0160; When this happens, we refer to the file as Cloaked.</p> <p>In order for a file to be cloaked, there must be an ACL on the file and that ACL must evaluate to the current user not having read access.</p> <p>There are only 3 pieces of information you can get from a cloaked file:&#0160; The File ID, the File Master ID and the Cloaked bit.&#0160; All other properties are removed or set to useless information.</p> <p>To illustrate things further, here is a comparison of the same File object accessed by different users.&#0160; One user has read access and the other one does not.  <br /><img src="/assets/cloaked.png" /> </p> <br /> <p><strong>Hidden Files</strong></p> <p>A cloaked file is very different than a hidden file.&#0160; The hidden bit is just meta-data that suggests whether a file should be visible to the user or not.&#0160; At the API level, a hidden file is exactly the same as a non-hidden file (except for the Hidden bit).&#0160; The hidden bit does not change the security settings or the functionality.&#0160; There are a few API search functions where you can choose to omit hidden files, but that&#39;s about the extent of this property. </p> <br /> <p><strong>Testing</strong></p> <p>When testing, make sure to use a non-administrator account.&#0160; Administrators always have read access, regardless of the ACL.&#0160; Therefore you will never run into a cloaked file as an administrator.&#0160; Depending on your view of things, the &quot;administrator sees all&quot; feature is either a pro or a con.  <br />Kaaaaaaaaaaaaaaaaaaaahhhhhhhhhhnnnnnnn!!!!!!</p> <br /> <p><strong>If you liked this article, you might also like:</strong>   <br /><a href="http://justonesandzeros.typepad.com/blog/2009/10/security.html">Security</a></p>
