---
layout: "post"
title: "Inventor: Create copy of Inventor document through API"
date: "2013-01-06 03:50:12"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/inventor-create-copy-of-inventor-document-through-api.html "
typepad_basename: "inventor-create-copy-of-inventor-document-through-api"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>  <p><b>Issue</b></p>  <p>There are many ways to create copy of Inventor documents but which one we should use and why.</p>  <p><a name="section2"></a><b>Solution</b></p>  <p>We can use following 3 ways to create copy of Inventor Documents</p>  <p>1. Using FilesSaveAs::ExecuteSaveCopyAs() method of apprentice server API   <br />2. Using CopyFile()&#160; Win32 API function    <br />3. Using CopyFileEx() Win32 API function</p>  <p>Among 2 and 3 which one should be used and why?</p>  <p>The CopyFile() is not recommended for OLE structured documents and since Inventor document is OLE structured document we shouldn't use CopyFile() to create copy of Inventor document. There are chances that Structured storage may get corrupted.</p>  <p>The CopyFileEx() is specially designed for handling OLE structured documents.</p>
