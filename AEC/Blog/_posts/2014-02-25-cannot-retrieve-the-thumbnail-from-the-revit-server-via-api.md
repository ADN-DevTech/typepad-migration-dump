---
layout: "post"
title: "Cannot retrieve the thumbnail of model file from the Revit Server via API"
date: "2014-02-25 07:23:50"
author: "Joe Ye"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2014/02/cannot-retrieve-the-thumbnail-from-the-revit-server-via-api.html "
typepad_basename: "cannot-retrieve-the-thumbnail-from-the-revit-server-via-api"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>&#160;</p>  <p>Via the Revit Server REST API, developers can get the folders and model files’ information in the Revit Sever. </p>  <p>Recently we received the report that developers cannot get the thumbnails of Revit models in the Revit Server.&#160; The REST API returns this error message: System.Net.WebException: The remote server returned an error: (404) Not Found.</p>  <p>However developers can read other file information using the REST API. </p>  <p>This is because of the lacking of the Microsoft Visual C++ 2010 redistributable in the computer. Retrieving thumbnail depends on this compoment. You can download the X86/ 64&#160; from the Microsoft Website, and install that to solve the issue. </p>
