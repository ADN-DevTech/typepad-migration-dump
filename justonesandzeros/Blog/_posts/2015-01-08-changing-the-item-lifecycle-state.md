---
layout: "post"
title: "Changing the Item Lifecycle State"
date: "2015-01-08 15:11:57"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2015/01/changing-the-item-lifecycle-state.html "
typepad_basename: "changing-the-item-lifecycle-state"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>In the Vault 2015 R2 release, the old Item lifecycle engine was replaced with the entity-based engine used by Files and Folders.&#0160; The good news is that the entity-based engine is filled with awesome features to make your Items more useful.&#0160; The bad news is that you have to update your code.</p>
<p>If you already know how to change File Lifecycle States, then you pretty much know how to change Item Lifecycle States.&#0160; This article is geared mostly to those that use Items but are not familiar with the File/Folder lifecycle engine.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Step 1:&#0160; Figuring out the States and Transitions</strong></p>
<p>In Vault 2015 and earlier, it was easy to know the possible Item states and transitions because they were hard coded.&#0160; There were only 4 states and they were all connected.&#0160; In 2015 R2, the administrator can define their own states and transitions.&#0160; They can even define multiple lifecycle definitions.&#0160; For example, one Item may be in the “Engineering Release Process” while another is in the “Document Approval Process”.&#0160;</p>
<p>So your app has to figure out what the states and transitions are.&#0160; The Item itself knows what Lifecycle State it is in.&#0160; But to get more context about the surrounding States you need to go to the <strong>LifeCycleService</strong>.&#0160; Not only can you find out about all the states and transitions, you can also update them if you have admin rights.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Step 2:&#0160; Updating the Item object</strong></p>
<p>The LifeCycleService does everything at the <a href="http://justonesandzeros.typepad.com/blog/2011/09/entities-and-behaviors.html">Entity</a> level.&#0160; If you want to modify the Item itself, you need to go to the ItemService.&#0160; When changing the state on an Item, you will use one of two functions...</p>
<p><strong>UpdateItemLifeCycleStates</strong> is the function to use when moving an Item from one state to another within a lifecycle definition.&#0160; For example, when moving from “Released” to “Quick Change”.</p>
<p><strong>UpdateItemLifeCycleDefinitions</strong> is the function to use when moving an Item to a different lifecycle entirely.&#0160; For example, when moving from “Flexible Release Process” to “Item Release Process”.</p>
<p>The nice thing about both functions is that they are one-shot functions.&#0160; No need to call EditItems prior or UpdateAndCommitItems afterward.&#0160; Everything happens in a single call.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p><strong>Sample code:</strong></p>
<p>Here is a quick sample app you can use to see how it all comes together.&#0160; Both C# and VB.NET code are included.</p>
<p><img alt="" src="/assets/ChangeItemLifecycleApp.png" /></p>
<p><a href="http://justonesandzeros.typepad.com/Files/ChangeItemLifecycle/ChangeItemLifecycle.zip">Click here to download</a></p>
<hr noshade="noshade" style="color: #d09219;" />
