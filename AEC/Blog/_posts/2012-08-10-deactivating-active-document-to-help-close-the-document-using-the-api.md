---
layout: "post"
title: "Deactivating Active Document to help Close the Document using the API"
date: "2012-08-10 15:42:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/08/deactivating-active-document-to-help-close-the-document-using-the-api.html "
typepad_basename: "deactivating-active-document-to-help-close-the-document-using-the-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>A partner had used the API to open a Revit document programmatically using <em>UIApplication.Application.OpenDocumentFile (FileName).</em> He then, set the active view and the document got activated. Next, he tried to close this active document which is currently not supported by the API (as discussed in a blog post in <a href="http://adndevblog.typepad.com/aec/2012/06/close-the-active-document-via-api.html">this</a> ADN AEC DevBlog as well as in Jeremy Tammik’s <a href="http://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html">blog post</a>). He wanted to find out if there are any methods to deactivate the document so that he could close the active document using API.</p>  <p>The workaround for closing the active document is to use the OpenAndActivateDocument() to open a new Revit document and then we can close the previous active document (which is what this partner/developer wanted to close with the API). This will deactivate the last opened document and thus we can close it using the Document.Close() method. The newly opened document would still need to closed manually at some point. The <a href="http://apps.exchange.autodesk.com/RVT/Detail/Index?id=autodesk.appstore.exchange.autodesk.com%3aADNPlugins_FileUpgrader%3aen">Revit File Upgrader</a> uses this exact workaround to open a batch of Revit documents one by one and close the ones which have been upgraded to the newer version (with the documented limitation that the last opened document needs to be manually closed).&#160; </p>
