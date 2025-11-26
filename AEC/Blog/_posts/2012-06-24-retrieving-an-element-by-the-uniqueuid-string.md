---
layout: "post"
title: "Retrieving an element by the UniqueuId string"
date: "2012-06-24 18:44:42"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/retrieving-an-element-by-the-uniqueuid-string.html "
typepad_basename: "retrieving-an-element-by-the-uniqueuid-string"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>What method can I use to retrieve an element from an unique id string using Revit API?&#0160;</p>
<p><strong>Solution </strong></p>
<p>You can use Document.GetElement(String). RevitAPI.chm file shipped at original release time has a typo.&#0160;String argument in the method GetElement(String) should&#0160;be unique id.&#0160;</p>
