---
layout: "post"
title: "Item In Script"
date: "2015-05-16 23:09:13"
author: "Michal Liu"
categories:
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2015/05/item-in-script.html "
typepad_basename: "item-in-script"
typepad_status: "Publish"
---

<p>In the previous posts, we had seen different script types and how to trigger scripts from different places in PLM. The next few posts will focus on the code itself. We will see how to use script create an item, update the bill of materials, add a milestone, etc. First of all, let’s start with the most important object in PLM script: <strong><em>Item</em></strong>.</p>
<hr />
<p><em><strong>Item</strong></em> object is the representation of an item in scripting. In each item’s web page, we can easily acquire the relevant information about the item, such as the value in each field, the item’s owner, all the bill of materials, etc. We also can update the item by creating a relationship with another item, performing a workflow action and so on. All of those operations can also be done with <strong><em>Item </em></strong>object in scripting. So <em><strong>Item</strong></em> object will be our next big topic. In the each of coming posts, we will focus on one aspect of this object. In today’s post, I will show you how to load, create, and delete an item.</p>
<hr />
<p><strong>Load Item: <em>loadItem(dmsId)</em></strong></p>
<p>To load an existing item, we need use the built-in function:<strong><em> loadItem. </em></strong>It takes the item’s DMS ID as the parameter, and returns the corresponding Item object. For example, if we want to load the existing item whose DMS ID is 1234 into a variable called myItem, then this is what we need:</p>
<blockquote>
<p><em>var myItem = loadItem(1234);</em></p>
</blockquote>
<p>Since every PLM script only can be triggered within an item’s scope, a reserved keyword “<em><strong>item</strong></em>” is used to represent the item which the script is triggered from.&#0160; We can call that item the owner item of the script. In each script, you can assume that the <strong>owner item</strong> the script has been loaded into a variable called “<em><strong>item</strong></em>” for you, so you can use “<em><strong>item</strong></em>” directly to work on the owner item. If you need any other items, then you should use <em><strong>loadItem</strong></em> function.&#0160;</p>
<hr />
<p><strong>Create Item: <em>createItem(workspaceId)</em></strong></p>
<p><strong><em>createItem </em></strong>can be used to create a new item in a certain workspace. The particular workspace should be specified using the parameter “workspaceId”; the created Item object will be returned. This “workspaceId” is not the numerical workspace identity, but the string identity, which can be found in each workspace’s setting page. [Administration -&gt; Workspace Manager -&gt; workspace –&gt; Workspace Settings]. Then you can assign value to each field of the returned item variable, which will be saved into this new item.&#0160;</p>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b7c78b3a98970b-pi"><img alt="Capture" border="0" height="193" src="/assets/image_1759d9.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="Capture" width="462" /></a></p>
<p>For example, to create an item in workspace “Project”:</p>
<blockquote>
<p><em>var newItem = createItem(“WS_PROJECT”);<br /></em><em>newItem.NAME = “PLM”;&#0160; &#0160; // <span style="font-size: 8pt;">set String value to a Text field</span><br /></em><em>newItem.STARTDATE = new Date();&#0160; &#0160; // <span style="font-size: 8pt;">set Date value to the Date field</span></em></p>
</blockquote>
<hr />
<p><strong>Delete Item: <em>item.deleteItem()</em></strong></p>
<p>To delete the owner item of the script, we only need call the delete function from the owner item:</p>
<blockquote>
<p><em>item.deleteItem();</em></p>
</blockquote>
<p>To delete other item, we need load the item first, and then delete it:</p>
<blockquote>
<p><em>var itemToDelete = loadItem(1234);<br /></em><em>itemToDelete.deletetItem();</em></p>
<p>&#0160;</p>
</blockquote>
<p>--Michal</p>
