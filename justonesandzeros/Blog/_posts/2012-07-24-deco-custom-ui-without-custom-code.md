---
layout: "post"
title: "DECO - Custom UI without Custom Code"
date: "2012-07-24 08:30:02"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2012/07/deco-custom-ui-without-custom-code.html "
typepad_basename: "deco-custom-ui-without-custom-code"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p>Update:&#0160; The <a href="http://justonesandzeros.typepad.com/blog/2013/04/deco-2014.html" target="_self">Vault 2014 version</a> of DECO is now available.</p>
<p>Take one last good look at yourself in the mirror...because I’m about to rock your face off.&#0160; Introducing Deco, the <span style="text-decoration: underline;">D</span>esign <span style="text-decoration: underline;">E</span>ditor for <span style="text-decoration: underline;">C</span>ustom <span style="text-decoration: underline;">O</span>bjects.&#0160; Deco lets you define your own custom object UI <strong>without the need for custom code</strong>.</p>
<p>Deco allows you to define your own dialog when creating a new custom object.&#0160; It allows you to define your own edit dialog.&#0160; And it allows you to have a custom tab for viewing your custom object data.</p>
<p>You start out by creating your user interface using Visual Studio:<img alt="" src="/assets/edit-scaled.png" /></p>
<p>Next, you hook it in to Vault Explorer:<img alt="" src="/assets/run.png" /></p>
<p>Deco can’t do everything, however.&#0160; Complex UI will still need custom programming.&#0160; But Deco should hit most of the common cases.&#0160; Specifically “property page” type functionality, where you want to display or edit the properties on a single object.</p>
<p>It’s probably best if you watch the how-to video on my Youtube channel.&#0160;</p>
<p>In other news, I now have a <a href="http://www.youtube.com/ItsAllJust1sAnd0s" target="_blank">Youtube channel</a>.&#0160; There is not much up there right now, but I’ll be porting over most of the videos from this blog.</p>
<p><iframe allowfullscreen="allowfullscreen" frameborder="0" height="360" src="http://www.youtube.com/embed/fQStIjwWQ-w?feature=player_detailpage" width="470"></iframe></p>
<p>For those of you blocked from Youtube, here is a download link:&#0160; <a href="http://justonesandzeros.typepad.com/Videos/Deco/Deco.wmv" title="http://justonesandzeros.typepad.com/Videos/Deco/Deco.wmv">http://justonesandzeros.typepad.com/Videos/Deco/Deco.wmv</a></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault Professional 2013 </li>
<li>XAML editor such as <a class="zem_slink" href="http://www.microsoft.com/express/Windows/" rel="homepage" target="_blank" title="Microsoft Visual Studio Express">Visual Studio Express</a> or Expression Blend </li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/Deco/Deco-1.0.1.0-bin.zip" target="_blank">Click here for the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/Deco/Deco-1.0.1.0-src.zip" target="_blank">Click here for the source code</a></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Current limitations:</strong></p>
<ul>
<li>Custom Objects only.&#0160; No support for files, folders, items, and so on. </li>
<li>Vault Professional 2013 only, since that’s the only product with custom objects. </li>
<li>Vault data can only be hooked to TextBox, CheckBox, ComboBox an DatePicker controls. </li>
<li>The XAML files and Settings.xml need to be distributed to all clients in order for them to see the custom UI.&#0160; This issue will be addressed in the next release of Project Thunderdome. </li>
</ul>
<p><span style="font-size: xx-small;">As with all the samples on this site, the </span><a href="http://justonesandzeros.typepad.com/blog/disclaimer.html"><span style="font-size: xx-small;">legal disclaimer</span></a><span style="font-size: xx-small;"> applies. </span></p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
