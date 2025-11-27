---
layout: "post"
title: "Syncing Vault Properties for Property Equivalence"
date: "2015-08-20 08:25:13"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2015/08/syncing-vault-properties-for-property-equivalence.html "
typepad_basename: "syncing-vault-properties-for-property-equivalence"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /><br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>In prior posts, Doug introduced a couple <a href="http://justonesandzeros.typepad.com/blog/2015/01/2-undocumented-functions.html" target="_blank">undocumented methods</a> in the Filestore Service and described how you could use them to <a href="http://justonesandzeros.typepad.com/blog/2015/02/roll-your-own-copy-design.html" target="_blank">roll your own copy design</a>. In this installment, I’ll show you how to use them to sync up properties to get rid of property equivalence errors.</p>
<p>A vaulted file will have property equivalence errors if it needs to have property values written back to it.&#0160; This behavior is controlled by the write mappings in the property definitions. The errors mean that the property values in the vault database not matching the property values stored within the file. You can remedy this situation by doing a property sync in Vault Explorer, but that’s too easy – instead let’s do the property sync via the Vault 2016 API.</p>
<p>In the Document Service there is a rather obscure method called <strong>GetComponentProperties</strong>, whose description in the help reads: “Gets property data for a file which has been assigned to an item”. What this method really does (in 2015-R2 and beyond) is provide exactly what needs to be written back to a file in order for it to have property equivalence. And it isn’t limited to files that have been assigned to items – it works perfectly fine where no items are present (such as in Vault Workgroup or Vault Basic).</p>
<p>Since nothing is easy, there are a number of potential issues that we will need to work around. First of all, if you were to blindly take the property data from GetComponentProperties and feed it to CopyFile, you may notice datetimes shifting by your timezone offset; or numerical values shifting by orders of magnitude in locales where the thousandths separator is a period (“.”) and the decimal point is a comma (“,”). That is some serious badness that must be avoided. Also, we don’t want to create a new version of each file we sync if we didn’t actually do anything – that would be really annoying.</p>
<p>I have wrapped up everything that needs to happen to avoid these issues in a simple class (“PropertySync”) which just has a constructor and one method. The idea is to gather the files you want to do the sync for, construct the PropertySync object once and then call its SyncProperties method on each file. The reason for the two-step process is so it only needs to get property definitions once. Many more details are in the comments.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/PropSync/PropertySync.cs">Click here for sample code (C#)</a></p>
<p>-- Dave Mink</p>
