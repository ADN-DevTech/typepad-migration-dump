---
layout: "post"
title: "Adding new file location using Sheetset API"
date: "2015-05-20 02:50:54"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/adding-new-file-location-using-sheetset-api.html "
typepad_basename: "adding-new-file-location-using-sheetset-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a code snippet to add a new file location and to set it as the new sheet location using the Sheetset API :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> AcSmSheetSetMgr manager = <span style="color:#0000ff">new</span><span style="color:#000000">  AcSmSheetSetMgr();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcSmDatabase sheetDb = </pre>
<pre style="margin:0em;">         manager.FindOpenDatabase(<span style="color:#a31515">@&quot;D:\\Temp\\MySheetset.dst&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> sheetDb.LockDb(sheetDb);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// New sheet location</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcSmResources resources </pre>
<pre style="margin:0em;">             = sheetDb.GetSheetSet().GetResources();</pre>
<pre style="margin:0em;"> AcSmFileReference fileRef = <span style="color:#0000ff">new</span><span style="color:#000000">  AcSmFileReference();</pre>
<pre style="margin:0em;"> fileRef.InitNew(resources);</pre>
<pre style="margin:0em;"> fileRef.SetFileName(<span style="color:#a31515">@&quot;D:\\Temp\\SampleDrawings&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> sheetDb.GetSheetSet().SetNewSheetLocation(fileRef);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Add New location</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcSmFileReference fileRef1 = <span style="color:#0000ff">new</span><span style="color:#000000">  AcSmFileReference();</pre>
<pre style="margin:0em;"> fileRef1.InitNew(sheetDb);</pre>
<pre style="margin:0em;"> fileRef1.SetFileName(<span style="color:#a31515">@&quot;D:\\Temp\\SampleDrawings&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> resources.Add(fileRef1);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> sheetDb.UnlockDb(sheetDb);</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>After you run the code, the model views from all the drawings in the path should get listed under Model Views as shown in this screenshot :</p>
<p></p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11702a9970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d11702a9970c img-responsive" alt="Sheetset" title="Sheetset" src="/assets/image_450917.jpg" style="margin: 0px 5px 5px 0px;" /></a>
