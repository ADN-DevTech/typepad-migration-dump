---
layout: "post"
title: "Intellesense"
date: "2012-08-16 17:05:52"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/08/intellesense.html "
typepad_basename: "intellesense"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Sorry but there was a problem with the intellesense for the Vault 2013 SDK.&#0160; When you are calling a web service function in Visual Studio, you should be seeing documentation appear as you code.&#0160; You may have noticed those XML files in the SDK bin directory.&#0160; The only purpose of those files is to provide documetation as you code.&#0160; However, there is a problem with the one for Autodesk.Connectivity.WebServices.</p>
<p><img alt="" src="/assets/NoDocs2.png" /></p>
<p>To fix this, I am posting <a href="http://justonesandzeros.typepad.com/Files/IntelleSense/2013/Autodesk.Connectivity.WebServices.XML" target="_blank">the corrected XML file</a>.&#0160; Just overwrite the existing Autodesk.Connectivity.WebServices.XML and you will see the function descriptions as you code.&#0160; All the other XML files should be correct.</p>
<p><img alt="" src="/assets/WithDocs2.png" /></p>
<p>Unfortunately, only the functions themselves are documented.&#0160; The parameter descriptions didn’t get exported for some reason.&#0160; Again, this is only a problem with Autodesk.Connectivity.WebServices.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
