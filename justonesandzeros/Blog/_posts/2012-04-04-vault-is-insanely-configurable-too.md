---
layout: "post"
title: "Vault is &ldquo;Insanely Configurable&rdquo; Too"
date: "2012-04-04 08:29:18"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/04/vault-is-insanely-configurable-too.html "
typepad_basename: "vault-is-insanely-configurable-too"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>It looks like <a href="http://underthehood-autodesk.typepad.com/" target="_blank">Brian Schanen</a> is only blogging about PLM 360.&#0160; So that leaves it to me to explain the Custom Entity feature in Vault Professional 2013.&#0160; It’s probably better this way.&#0160; Although you can create Custom Entities without API programming, it’s like setting off fireworks in the daytime.&#0160; You just don’t get the full effect.</p>
<p>I don’t think I’ll be able to cover everything in just one article, so I’ll just focus on the UI aspects for today.&#0160; In the coming weeks, I’ll dig deeper into the API details.&#0160; And if your nose is detecting a nice meaty aroma, that’s the smell of a sample app coming soon.</p>
<p>You can manage your custom entities through the Vault Settings dialog.&#0160; You may notice that there is a new tab for Custom Objects. (NOTE: custom entities are called “custom objects” in the UI).&#0160; This tab is mostly empty.&#0160; The only thing you can do is click the Configure button, which lets you manage the custom entity definitions.&#0160; You can think of a custom entity definition as a class definition in .NET or a table definition in SQL.</p>
<p><img alt="" src="/assets/ConfigureCustomObjectDefs.png" /></p>
<p>If you create a new definition, you’ll see that there are various fields to set.&#0160; There the display name and plural display name determines how the objects will be shown to the user.&#0160; The security defines the default ACL for new objects.&#0160; The icon is the image that will get displayed next to your objects.&#0160; This is an .ico file, so you can define multiple sizes.&#0160; We recommend 16x16 , 32x32, 64x64, or 128x128.</p>
<p><img alt="" src="/assets/CustomObjectDef.png" /></p>
<p>After a custom object definition is created, you need to restart Vault Explorer to see it properly.</p>
<p><img alt="" src="/assets/Ve1.png" /></p>
<p>Our custom entities get their own section in Vault Explorer.&#0160; If we go to one of these sections and create a new object, you will see the default New behavior, which is pretty basic.&#0160; All you can set is the name for the entity.&#0160; In another post, I’ll show you how to override this dialog.</p>
<p><img alt="" src="/assets/NewFoo.png" /></p>
<p>Now we have our object, but I’m guessing you are going to want to set more than just the name.&#0160; To configure things like properties, category, you need to go to the property and category sections of Vault Settings.&#0160; You will see that Custom Object capability has been added to these settings.&#0160; Through the category configuration you can set things like property definitions and lifecycle.</p>
<p><img alt="" src="/assets/UDP.png" /></p>
<p><img alt="" src="/assets/CatConfig.png" /></p>
<p>It’s also a good idea to set up a rule to move your custom objects to their own category upon creation.&#0160;</p>
<p><img alt="" src="/assets/CatRules.png" /></p>
<p>The last thing I want to go over is the default tabs.&#0160; The Contents tab works like the Uses tab for files; it shows the children entities.&#0160; The Were Used tab works the same way but it identifies parents.&#0160; To set these relationships you use the Link feature.&#0160; Just copy an entity and “Paste as Link” into the Contents tab of your custom entity.&#0160; You can link your custom object to and from all the major entity types (files, folders, items, change orders and custom entities).</p>
<p><img alt="" src="/assets/Ve2.png" /></p>
<p>Hopefully you are starting to see the possibilities, and I’ve only gone over the standard UI behavior.&#0160; The real fun begins when we get into the API features in <a href="http://justonesandzeros.typepad.com/blog/2012/05/vault-is-insanely-configurable-too-2.html" target="_self">part 2</a>.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
