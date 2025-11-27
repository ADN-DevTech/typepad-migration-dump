---
layout: "post"
title: "My Take on Vault Data Standard"
date: "2014-07-11 17:39:43"
author: "Doug Redmond"
categories:
  - "Commentary"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/07/my-take-on-vault-data-standard.html "
typepad_basename: "my-take-on-vault-data-standard"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /><img alt="" src="/assets/Commentary4.png" /></p>
<p>Earlier this week I listened in on a webinar on <a href="https://www1.gotomeeting.com/register/921695928" target="_blank">Vault Data Standard</a> and it got me to thinking.&#0160; Coming from a Vault API background, I have a different view of Data Standard then what is usually presented.&#0160; So I’d like to provide my thoughts since this <em>is</em> a blog and all....</p>
<hr noshade="noshade" style="color: #666666;" />
<p><strong>APIs on top of APIs</strong></p>
<p>Data Standard provides a lot of features you can get through the API.&#0160; You can create custom commands, custom UI and make calls to the Vault server.&#0160; What’s interesting is that Data Standard itself is a Vault Explorer plug-in.&#0160; So the Data Standard API is basically an API on top of the Vault API.</p>
<p>It’s basically like the movie Inception, but with APIs instead of dreams.</p>
<p>Many pieces of the Data Standard just pass through to the Vault API.&#0160; Adding custom commands is an example of this.&#0160; The settings in the DS .mnu files are exactly the same as the properties on CommandItem from the Vault API.&#0160; In this aspect, knowledge of the Vault API transfers directly over to Data Standard.</p>
<p>Another example is the $vault object that shows up in the .ps1 files.&#0160; The purpose of the object is for making web service calls to the Vault server.&#0160; $vault is a WebServiceManager object being passed in directly from the Vault SDK DLLs.&#0160; If you want to do anything with $vault, you need the Vault API documentation on-hand.&#0160; Again, if you are already familiar with the Vault API, than this is no problem.</p>
<p>What I’m more curious about is the people <em>without</em> a Vault API background.&#0160; How well are they able to utilize Data Standard?&#0160; Does DS ease them into the world of Vault programming, or does the Vault API hit them like an impassable brick wall?</p>
<hr noshade="noshade" style="color: #666666;" />
<p><strong>Beyond the API</strong></p>
<p>The stuff that interests me most is the stuff that can’t be done through the Vault API.&#0160;</p>
<p>First and foremost, the CAD plug-ins are awesome.&#0160; Data Standard is not just a plug-in to Vault; it plugs into AutoCAD and Inventor as well.&#0160; That way you can easily create an Inventor dialog that is Vault-aware, for example.&#0160; Going through the traditional APIs would be a daunting task.&#0160; You would need to be an expert in both APIs and would have to figure out how to hook the two together.&#0160; DS solves all that stuff for you in a way that makes it look easy.</p>
<p>Another aspect of DS are the template features.&#0160; New CAD files can be copied from a template instead of staring from a blank file.&#0160; DS uses Vault functionality to centralize the storage of the templates.&#0160; This is less of an example of a generic API and more of an example of a focused solution.&#0160; Data Standard is really a dual product: It’s an API, and it’s an end-user utility.&#0160;&#0160;</p>
<p>The two aspects are not at odds with each other, but I’m not sure if they blend well together either.&#0160; It feels to me more like a Swiss Army knife.&#0160; It’s a bunch of seemingly unrelated stuff packaged together.&#0160; Maybe that’s why it’s a hard product to describe it to people.</p>
<hr noshade="noshade" style="color: #666666;" />
<p><strong>Migration</strong></p>
<p>One thing like like about compiled programming languages, such as C#, is that you get compile errors when something goes wrong.&#0160; That way if an API changes, you know right away what broke.&#0160; PowerShell, however, is a scripting language.&#0160; So it’s harder to find the breakages.&#0160; Usually, they show up at runtime and only if specific pieces of code are run.&#0160;</p>
<p>If you have a lot of PowerShell code in your Data Standard implementation, you may find it hard to maintain.&#0160; Everything will seem fine at first after an upgrade, but things start failing as people start using the custom functions.&#0160; Even if you think you have everything updated, a CAD user somewhere may hit lesser-used code that calls an obsolete Vault API function.&#0160;</p>
<p>Yes, Vault provides compatibility for older versions of the web service API, but in order to use them, you need to have the older SDK objects.&#0160; If Data Standard is passing in the 2015 version of the WebServiceManager, then you can’t make use of the 2014 server APIs.</p>
<p>There are lots of ways to solve, or minimize these issues when they do come up.&#0160; For now, Data Standard is new, so nobody has run into migration issues... yet.</p>
<hr noshade="noshade" style="color: #666666;" />
