---
layout: "post"
title: "Building Property Pages"
date: "2012-01-18 12:20:50"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/01/building-property-pages.html "
typepad_basename: "building-property-pages"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>By default, Vault Explorer provides the View Properties Grid which provides a list of all property values for the selected object.&#0160; This is a nice default view, but us developers commonly want to have customized property views (aka. property pages).&#0160; So here is a quick primer on creating property pages in Vault Explorer.</p>
<p><strong>Note:</strong>&#0160; This article is basically a condensed version of Dan Leighton’s excellent AU 2011 class, <a href="http://au.autodesk.com/?nd=class&amp;session_id=9315" target="_blank">Manage Dozens of Autodesk Vault Workgroup Properties with Custom Forms and Reports</a>.&#0160; If you have the time, feel free to skip the rest of this post and view the video on the AU site.&#0160; Also, his handout is full of sample code!</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>The first step is to set up your project for writing a Vault Explorer extension.&#0160; You can read up on this in the SDK documentation.&#0160; Or you can just copy the HelloWorld example and build off that.&#0160;</p>
<p>Property pages in Vault Explorer most commonly take the form of custom tabs or custom dialogs.&#0160; I’ll be showing an example of a custom tab, which means we start by defining our own User Control (right click on your project and select Add –&gt; User Control).&#0160; Now we have a blank control.&#0160; Next you drag and drop controls from the Toolbox into your custom control.&#0160; Usually each property will have two controls, one to display the property name and one to display the property value. A Label control is usually used for the property name.&#0160; The value is usually represented by a TextBox, but there are tons of controls to choose from.&#0160; Other useful controls are ComboBoxes, CheckBoxes, DateTimePickers, RadioButtons, and so on.&#0160; For this example, I’ll keep it simple and just stick to TextBoxes.</p>
<p><img alt="" src="/assets/control.png" /></p>
<p>Now that we have our controls in place, we need a way to map the specific property values to the correct controls.&#0160; Sure we could just hard code the mapping for each TextBox, but that doesn’t leave us with any reusable code.&#0160; It also doesn’t make things easy for us in the future if we have to make changes to our property page.&#0160; A better way is to add meta-data to our controls.&#0160; At runtime we route the property data to the correct control based on the meta-data.</p>
<p>There are a couple of ways to set the meta-data on a control.&#0160; In this example, I’ll use the “Tag” property.&#0160; I want to set my TextBox Tag values to something that will let me locate the Vault property.&#0160; You can use the display name of the Vault property if you are sure that it won’t change.&#0160; Otherwise, I suggest using the system name.</p>
<p><img alt="" src="/assets/tagProperty.png" /></p>
<p>Now we have our control, and our TextBoxes are smart enough to know what Vault property they are supposed to display.&#0160; The next step is to write code that looks up the Vault properties on a File and routes those values to the correct TextBox…    <br />Or you could just copy my code from the examples below.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/PropertyPages/FilePropertyPage.cs" target="_blank">Click here for C# code</a> <br /><a href="http://justonesandzeros.typepad.com/Files/PropertyPages/FilePropertyPage.vb" target="_blank">Click here for VB.NET code</a></p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>Here is the final output.</p>
<p><img alt="" src="/assets/output.png" /></p>
<p>If we ever need to display more properties on the page, it’s as easy as dropping in the new TextBox and setting the Tag value.</p>
<p>This is just a simple example to get you started.&#0160; More complex cases involve different data types, different control types and allowing the user to edit values through your property page.&#0160;</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
