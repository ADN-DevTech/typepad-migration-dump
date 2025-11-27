---
layout: "post"
title: "Vault is &ldquo;Insanely Configurable&rdquo; Too 2"
date: "2012-05-03 10:54:14"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/05/vault-is-insanely-configurable-too-2.html "
typepad_basename: "vault-is-insanely-configurable-too-2"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Welcome to part 2 of my articles on custom objects.&#0160; In <a href="http://justonesandzeros.typepad.com/blog/2012/04/vault-is-insanely-configurable-too.html">part 1</a>, I went over the UI features.&#0160; Now I will go over the API features, which is where the real fun begins.&#0160; If you want some sample code, have a look at my <a href="http://justonesandzeros.typepad.com/blog/2012/04/discussion-thread-app.html">Discussion Thread app</a>.&#0160; But first... terminology.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong><span style="font-size: medium;">Terminology</span></strong></p>
<p><strong>Custom Object and Custom Entity</strong></p>
<p>These are the exact same things.&#0160; Custom Object is the name we use in the UI.&#0160; Custom Entity is the name we use in the API.</p>
<p><strong>Entity Class and Custom Entity Definition</strong></p>
<p>I’ve talked before about <a href="http://justonesandzeros.typepad.com/blog/2011/09/entities-and-behaviors.html">entities</a>.&#0160; As you probably guessed, custom entities are entities.&#0160; What can be confusing is that all custom entities belong to the same Entity Class, which has “CUSTENT” as the ID value.&#0160;</p>
<p>Custom Entity Definitions can be considered a sub-type of CUSTENT.&#0160; So if I create custom entity definitions called “Foo” and “Bar”, they are both part of the CUSTENT entity class.&#0160;</p>
<p><img alt="" src="/assets/SubTypes.png" /></p>
<p>This distinction is important because a lot of things operate on the CUSTENT level.&#0160; For example, FindCustomEntitiesBySearchConditions will search across custom entities of all sub-types.&#0160; If you want to scope the search to a specific sub-type, you can do that with a search condition.</p>
<p>Permissions is another thing that operates on the CUSTENT level.&#0160; If a user has the Custom Object Editor (Level 2) role, it applies to all custom entity sub-types.&#0160; If you want each sub-type to have their own permissions, you can use the ACL settings.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong><span style="font-size: medium;">API Features</span></strong></p>
<p><strong>Custom Commands      <br /></strong>Your custom commands in Vault Explorer can be bound to specific custom entity sub types.&#0160; That way the command is only visible when an object of your custom object sub-type is selected.&#0160; Also, you can add commands to the context menu for your sub types.</p>
<p>To make use of this feature, create custom commands just like normal.&#0160; The only difference is that you set the navigation type to a new SelectionTypeId which points to your sub-type.</p>
<p><strong>Custom Tabs      <br /></strong>You can define your own Vault Explorer tabs which only show up when your custom entity sub-type is selected.&#0160; You also have the option of hiding the default tabs.</p>
<p>Just like with custom commands, you create a new SelectionTypeId to hook the tab to your sub-type.</p>
<p><strong>New and Delete Override      <br /></strong>You can take over the New and Delete commands in Vault Explorer so that your code gets called instead of the default.&#0160; The IExtension interface has a new method, CustomEntityHandlers.&#0160; Pass back a new CustomEntityHandler, which has New and Delete events that you can hook to.</p>
<p><strong>Update Lifecycle State - Web Service Command Event      <br /></strong>Through the CustomEntityService, you can hook to the GetRestrictions, Pre and Post events whenever a custom entity changes lifecycle state.&#0160; These hooks apply to all CUSTENT state changes regardless of sub-type.</p>
<p><strong>Update Lifecycle State - Lifecycle Event Job      <br /></strong>Through the Lifecycle Event Editor you can configure the server to fire jobs when custom entities change lifecycle states.&#0160; These jobs fire on sepecific state transitions regarless of entity type or sub-type.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
