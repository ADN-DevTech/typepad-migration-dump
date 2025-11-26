---
layout: "post"
title: "RevitAPI: How to get location of keynote file?"
date: "2015-09-07 02:08:25"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/09/revitapi-how-to-get-location-of-keynote-file.html "
typepad_basename: "revitapi-how-to-get-location-of-keynote-file"
typepad_status: "Publish"
---

<a href="http://blog.csdn.net/lushibi/article/details/48155411">中文链接</a>

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>

<p>
  After a research, I found location of keynote file is hidden deeply
</p>
<p>
  KeynoteTable.GetKeynoteTable(Document).GetExternalResourceReferences() returns a dictionary, from which we can get object of ExternalResourceReference, then using ExternalResourceReference.InSessionPath we could get its file location.
</p>
