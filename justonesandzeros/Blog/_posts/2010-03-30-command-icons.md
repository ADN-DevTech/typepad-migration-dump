---
layout: "post"
title: "Command icons"
date: "2010-03-30 08:11:41"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/command-icons.html "
typepad_basename: "command-icons"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /> </p>
<p><strong>Update:</strong>&#0160; For Vault 2013 and higher, the recommended size for command icons is 16x16.</p>
<p>Vault 2011 gives you the ability to add commands to Vault Explorer.&#0160; You also have the option to include an icon with the command.&#0160;&#0160;&#0160; I don&#39;t need to tell you how helpful icons can be in presenting your command.&#0160; But I <em>do</em> need to tell you about a few things you need to be aware of in the Vault Client API.&#0160; <img alt="" src="/assets/icon.png" /> </p>
<p>I&#39;ll also talk a bit on how deal with icon transparency.&#0160; I&#39;ve created a grand total of 1 icon in my life, so I&#39;m not an expert or anything.&#0160; But if have less experience than I, you might want to have a quick read. <img alt="" src="/assets/icon.png" /></p>
<p>&#0160;</p>
<p><strong>Icons in the Vault Client API</strong></p>
<p>Here are the basic steps in getting an icon to work with your Vault command:</p>
<ol>
<li>Create the icon file.&#0160; However the Client API does not support .ico files.&#0160; So I recommend creating a 32x32 PNG file instead. </li>
<li>Make sure that the image still looks good when scaled to 16.x16.&#0160; Since you are not dealing with an .ico file, you can&#39;t just create a 16x16 version. </li>
<li>In your Visual Studio project, create a resource (.resx) file. </li>
<li>Add your PNG file to the resource. </li>
<li>In your IExtension implementation, ResourceCollectionName should return the name of the resource with the icon in it.&#0160; For example, if your resource file is called &quot;Icons.resx&quot;, you would return &quot;Icons&quot;. </li>
<li>When creating your CommandItem object, set the IconResourceName property to the name of your icon as it shows up in the resource file.&#0160; In other words, the resource key of the image. </li>
<li>Set the ToolbarPaintStyle in your CommandItem object to either Glyph or TextAndGlyph depending on how you want it displayed.&#0160; Glyph is useful if it&#39;s just a toolbar command.&#0160; TextAndGlyph works best for menu commands. </li>
<li>Compile and run.&#0160; You should now have an icon with your command. </li>
</ol>
<p><strong><br /></strong></p>
<p><strong>Using transparency in icons<br /></strong></p>
<p>Unless your icon has a 32x32 square shape, you will probably want to use transparency.&#0160; For PNG files, this means selecting a single color and designating it as your transparent color.&#0160; At runtime, this color becomes transparent.&#0160; Not all image editors let you set a transparent color.&#0160; I don&#39;t think Microsoft Paint or Visual Studio let you do this, so you will need a better image editor. <img alt="" src="/assets/icon.png" /> </p>
<p>When using transparency, it&#39;s important to have a clear and solid boundary between transparent and opaque pixels.&#0160; Things like blurry edges and shadows are a bad idea.&#0160; They might look good on a white background, but you get a halo effect on a different color background.&#0160; <img alt="" src="/assets/icon.png" /> </p>
<p style="text-align: left;">Here are some examples comparing a white background with a black background (white is the transparency color).  <br />  <br /><img alt="" src="/assets/blurCompare.png" />   <br />Bad - blurred edges</p>
<p><img alt="" src="/assets/shadowCompare.png" />   <br />Bad - shadow</p>
<p><img alt="" src="/assets/goodCompare.png" />   <br />Good - image looks the same regardless of background</p>
<br />
<p>A common technique is to use something other than white as your transparent color.&#0160; Usually this is something bold and completely different than any other colors used. <img alt="" src="/assets/icon.png" />   <br />  <br /><img alt="" src="/assets/nonWhiteBg.png" /></p>
