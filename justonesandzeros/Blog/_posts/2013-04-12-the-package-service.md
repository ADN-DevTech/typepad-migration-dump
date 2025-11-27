---
layout: "post"
title: "The Package Service"
date: "2013-04-12 08:23:06"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2013/04/the-package-service.html "
typepad_basename: "the-package-service"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>The Package Service is harder to describe than the rest of the Vault web services.&#0160; It was designed with one thing in mind, but ended up doing something else.&#0160; As a result, there are some artifacts left around that make it confusing to work with.&#0160; The main use nowadays is to import and export BOM data between the Item service and a file.&#0160; But there is still some active legacy behavior that you might find useful.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>File Import/Export:</strong></p>
<p>The Package Service implements the Import and Export commands for Item BOM data.&#0160; You can find sample code in the ItemEditor example in the SDK.</p>
<p>The biggest confusion with import/export API is that the Package Service has its own BOM objects, which act as an intermediate between the Item Service BOM and the imported/exported file.&#0160; Another point of confusion is the fact that the words &quot;import&quot; and &quot;export&quot; don&#39;t show up in any of the API functions. &#0160; </p>
<p>For example, to export, you call PackageService.GetItemsAndBOMsFromItemIds() to convert the Item BOM to the Package BOM.&#0160; Next you call PackageService.DownloadPackage() to convert the Package BOM into a file.</p>
<p>Import works pretty much the same way but in reverse.&#0160; The major difference is that the Package BOM has can detect conflicts in the import.&#0160; For example, if you try to import revision A of an item but Vault has the same item at revision B, that&#39;s a conflict.&#0160; The Package BOM allows you to see, and sometimes fix, conflicts like these before the Item BOM gets updated.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Legacy Behavior:</strong></p>
<p>The original intent of the Package service was to be an integration point between Vault and ERP Systems.&#0160; The original name for the service was the ERPService and you can still see ERP used in a few class names.&#0160; Out of the box, Vault supported compatibility to Microsoft Great Plains.&#0160; There was an Axapta integration too, but I think it required Autodesk Consulting.&#0160; Both integrations consisted of Vault importing/exporting data to a Microsoft Message Queue (MSMQ). </p>
<p>The intent was to support more integrations and maybe even have a pluggable interface.&#0160; But once the Job Server feature was developed, it became the main focus for integrations.&#0160; I wouldn’t go as far to say that the Package Service is gathering dust, but I don’t expect any other systems to be added to the Package Service.&#0160; You can basically import/export to an MSMQ or a file (which is labeled as “other” in the UI).&#0160; As far as I know, the MSMQ features still work, so you can use it as an alternative to Vault job queue.</p>
<p><img alt="" src="/assets/exportWizard.jpg" /></p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
