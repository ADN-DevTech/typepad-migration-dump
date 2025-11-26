---
layout: "post"
title: "Activating a view of the document created by NewProjectDocument method"
date: "2012-06-01 01:47:27"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/activating-a-view-of-the-document-created-by-newprojectdocument-method.html "
typepad_basename: "activating-a-view-of-the-document-created-by-newprojectdocument-method"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>I wanted to create a document and activate a specific view such as ’Level 1’. I can use Revit.ApplicationServices.Application.NewProjectDocument method to create my document. However, there is no way to get associated UIDocument object for the document. So UIDocumen.ActiveView property can not be used to set a view to activate it.</p>
<p>However, I found a workaround. You could achieve it by following steps.</p>
<p>1. Close the document created by NewProjectDocument method.<br />2. Open that file by UIApplication.OpenAndActivateDocument method.<br />3. Set the view you want to active to ActiveView property of the UI document obtained at the step 2.</p>
<p>Done.</p>
<p>&nbsp;</p>
