---
layout: "post"
title: "Accessing Linked File Path Information using Revit .NET API"
date: "2012-10-01 22:47:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/10/accessing-linked-file-path-information-using-revit-net-api.html "
typepad_basename: "accessing-linked-file-path-information-using-revit-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Using the Revit API, I have been trying to determine, whether a linked RVT file or an imported DWG file has a relative or absolute path. This information is displayed in the Manage Links dialog, but how can we access this information via the API?</p>  <p>In the Revit API (since Revit 2012), we have the following APIs to help with this exact requirement -    <br />&#160; <br />PathType : The path type of the link (relative, absolute, or server).&#160; <br />GetPath() : Gets the path of the link, relative or absolute according to the link's settings&#160; </p>
