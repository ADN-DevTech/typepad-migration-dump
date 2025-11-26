---
layout: "post"
title: "'Add' is not a member of 'Autodesk.AutoCAD.ApplicationServices.DocumentCollection'"
date: "2012-08-01 22:58:44"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/add-is-not-a-member-of-autodeskautocadapplicationservicesdocumentcollection.html "
typepad_basename: "add-is-not-a-member-of-autodeskautocadapplicationservicesdocumentcollection"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are migrating your application from 2012/2011/2010 or older releases to 2013 release and in your application you use acDocMgr.<strong>Add</strong>(strTemplatePath) Or doc.<strong>CloseAndSave</strong>() as given in the example code snippet in AutoCAD Civil 3D 2013 <a href="http://docs.autodesk.com/CIV3D/2013/ENU/index.html?url=filesACD/GUID-330A8DCB-626F-4271-8B89-9773A7631D87.htm,topicNumber=ACDd30e702843" target="_self">online documentation</a>, you might have notice Visual Studio shows error like :&#0160;</p>
<p>&#39;Add&#39; is not a member of &#39;Autodesk.AutoCAD.ApplicationServices.DocumentCollection&#39;.&#0160;</p>
<p>&#39;CloseAndSave&#39; is not a member of &#39;Autodesk.AutoCAD.ApplicationServices.Document&#39;.&#0160;</p>
<p>In 2013 release, there are some changes in AutoCAD .NET; we have added some namespaces to house extension methods like- DocumentExtension, DocumentCollectionExtension etc. Details are available in ObjectARX for AutoCAD 2013 Docs: Managed Class Reference -&gt; Migration Guide-&gt;.NET Migration Guide.</p>
<p>&#0160;</p>
<p>To resolve above mentioned errors in Visual Studio, simply add the following namespaces to your project (VB.NET).</p>
<p>&#0160;</p>
<p>Imports <strong>Autodesk.AutoCAD.ApplicationServices.DocumentCollectionExtension</strong></p>
<p>Imports <strong>Autodesk.AutoCAD.ApplicationServices.DocumentExtension</strong></p>
<p>&#0160;</p>
