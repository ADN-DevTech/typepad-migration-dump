---
layout: "post"
title: "Adding a ribbon panel to the existing tab"
date: "2012-06-07 01:18:11"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/adding-a-ribbon-panel-to-the-existing-tab.html "
typepad_basename: "adding-a-ribbon-panel-to-the-existing-tab"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>You can add your ribbon panel not only to [Add-Ins] tab but also to an existing ribbon tab.</p>
<p>You can specify the name of the tab where you are adding your panel in CreateRibbonPanel method.&nbsp;</p>
<p><span style="color: #0000bf;">public</span> RibbonPanel UIControlledApplication.CreateRibbonPanel(<br />&nbsp; <span style="color: #0000bf;">string</span> tabName,<br />&nbsp; <span style="color: #0000bf;">string</span> panelName<br />)</p>
<p>This could be useful if you want to load additional add-in but want to share the existing UI.</p>
<p>Please note that Revit (the current release at least) registers the add-in with the file name in Alphabetic order. For example, if the ribbon tab “B” was added in add-in B (registered in B.addin file), you could register the add-in C (registered in C.addin file) and add the panel to tab B. But add-in A(registered in A.addin file) will not work.</p>
<p>&nbsp;</p>
