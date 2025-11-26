---
layout: "post"
title: "Programmatically generating Navisworks files"
date: "2012-06-13 17:09:28"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/06/programmatically-generating-navisworks-files.html "
typepad_basename: "programmatically-generating-navisworks-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>  <p>How can we generate Navisworks file programmatically? Is NWCreate used for this? </p>  <p>We can create Navisworks file programmatically using NWCreate. NWCreate API is provided as a LIB that can be linked into the standalone application. The api/nwcreate folder contains all the files we need. </p>  <p>But using a standalone application, we can only create NWC file. Please note that NWC files cannot be loaded into Freedom but can be loaded in Simulate and Manage. There are two functions that can be used to write a cache file in NWCreate:   <br />LiNwcSceneWriteCache()    <br />LiNwcSceneWriteCacheEx()</p>  <p>Both the functions are the same, except that the second one can return more errors codes. </p>  <p>The NWCreate SDK sample which ships with Navisworks includes a standalone program which creates a NWC file.&#160;&#160; </p>  <p>To create NWD file, we need to have a licensed copy of Navisworks installed on each system. And this involves some additional tasks like determining the version of Navisworks that is installed on the system. </p>
