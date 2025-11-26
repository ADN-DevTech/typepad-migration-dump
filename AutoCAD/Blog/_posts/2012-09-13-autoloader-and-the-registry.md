---
layout: "post"
title: "Autoloader and the registry"
date: "2012-09-13 17:22:52"
author: "Madhukar Moogala"
categories:
  - "2013"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/autoloader-and-the-registry.html "
typepad_basename: "autoloader-and-the-registry"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>  <p>An Exchange store publisher emailed us today after he’d been surprised by some ‘as designed’ Autoloader behavior, so I’m publicizing it here to prevent others having the same surprise. </p>  <p>When Autoloader detects a new bundle, it adds the demand load settings inferred from the PackageContents.xml file to the registry. After your bundle has loaded, your registry will look something like this:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c31d829b0970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="SNAGHTML22e82d0" border="0" alt="SNAGHTML22e82d0" src="/assets/image_211566.jpg" width="484" height="228" /></a></p>  <p>Which is the same as you’d expect if you’d added the entries via your custom installer or through some setup code that is invoked when your app first runs.</p>  <p>However, the demand load entries added by Autoloader are deleted when AutoCAD exits. </p>  <p>This means that you shouldn’t use your plug-in’s demand load registry entries to store additional data if you’re using Autoloader. Store it somewhere else in the registry instead (if you must use the registry), or in a config file. You can find the registry location for your running AutoCAD instance with the HostApplicationServices.UserRegistryProductRootKey and UserRegistryProductRootKey properties.</p>  <p>As an aside, there are some settings persisted by Autoloader so it knows not to show a balloon notification for a plug-in every time AutoCAD starts:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3c06808f970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="SNAGHTML23711c6" border="0" alt="SNAGHTML23711c6" src="/assets/image_800358.jpg" width="491" height="168" /></a></p>  <p>The GUIDs shown in the screenshot come from the ProductCode parameter in PackageContents.xml.</p>
