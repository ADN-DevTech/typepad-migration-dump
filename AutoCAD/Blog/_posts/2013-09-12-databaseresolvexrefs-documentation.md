---
layout: "post"
title: "Database.ResolveXrefs documentation"
date: "2013-09-12 16:41:41"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/09/databaseresolvexrefs-documentation.html "
typepad_basename: "databaseresolvexrefs-documentation"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are wondering what the parameters are used for…</p>  <p><strong>public void Database.ResolveXrefs(bool useThreadEngine, bool doNewOnly)</strong></p>  <p>…then here’s the documentation on the equivalent ObjectARX function found in the ObjectARX Reference guide…</p>  <p><strong>acdbResolveCurrentXRefs</strong></p>  <pre>Acad::ErrorStatus <strong>acdbResolveCurrentXRefs</strong>(
    AcDbDatabase* <strong>pHostDb</strong>, 
    <strong>bool</strong> <strong>useThreadEngine</strong> = <strong>true</strong>, 
    <strong>bool</strong> <strong>doNewOnly</strong> = <strong>false</strong>
);</pre>

<p><a name="46696C65"></a></p>

<p>acdbxref.h</p>

<p><a name="506172616D6574657273"></a></p>

<p><strong>Parameters Description </strong></p>

<p>AcDbDatabase* pHostDb </p>

<p>Input pointer to the AcDbDatabase to be used as the host </p>

<p>bool useThreadEngine = true </p>

<p>Input Boolean indicating whether to use the thread engine for xref resolution </p>

<p>bool doNewOnly = false </p>

<p>Input Boolean indicating whether to process only newly added xrefs </p>

<p><a name="4465736372697074696F6E"></a></p>

<p><a><img title="" border="0" alt="" src="mk:@MSITStore:C:%5CAPIs%5CObjectARX%5C2013%5Cdocs%5Carxref.chm::/btn_collapse_large.gif" />Description</a></p>

<p>This function resolves existing xrefs in pHostDb. </p>

<p>If useThreadEngine is true, and other factors haven't disabled it, then the multi-thread engine is used for resolving the xrefs. </p>

<p>If doOnlyNew is true, only unresolved xref records are processed. Existing resolved xref AcDbLayerTableRecords, AcDbLinetypeTableRecords, and AcDbBlockTableRecords are ignored. In this case, the useThreadEngine argument also is ignored, and the multi-thread engine is not used. </p>

<p>If pHostDb points to an AcDbDatabase that is the primary database for a document in AutoCAD (in other words, a database that is loaded in the AutoCAD editor), doOnlyNew should be set to true to avoid reprocessing existing xrefs. </p>

<p>Warning</p>

<p>If pHostDb already contains resolved xrefs, then you must set the useThreadEngine argument to false. Otherwise, this function fails. </p>

<p>No document locking is done by this function. If pHostDb is associated with a document, the caller is responsible for locking that document. </p>

<p>This function is available to non-AutoCAD-based host applications. </p>

<p>Returns Acad::eOk if successful.</p>
