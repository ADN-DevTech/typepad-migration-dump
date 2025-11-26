---
layout: "post"
title: "Creating Featureline using COM API IAeccLandFeatureLines:: AddFromPolyline()"
date: "2013-04-30 00:00:11"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/creating-featureline-using-com-api-iaecclandfeaturelines-addfrompolyline.html "
typepad_basename: "creating-featureline-using-com-api-iaecclandfeaturelines-addfrompolyline"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Civil 3D .NET
API is yet to get the necessary functions to add / create featureline object. We
have a wish list logged for the same. Till we get the .NET API, let&#39;s see how
to use the existing COM API <strong>IAeccLandFeatureLines:: AddFromPolyline()</strong> in .NET
application using Interop.</p>
<p>&#0160;</p>
<p>Following
VB.NET code snippet demonstrates creation of a Featureline in AutoCAD Civil 3D
using COM API <strong>IAeccLandFeatureLines:: AddFromPolyline()</strong> -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> trans </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> = </span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase.TransactionManager.StartTransaction()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> oAcadApp </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; oAcadApp = GetObject(, </span><span style="color: #a31515; line-height: 140%;">&quot;AutoCAD.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(ex.Message)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; oAeccApp = oAcadApp.GetInterfaceObject(</span><span style="color: #a31515; line-height: 140%;">&quot;AeccXUiLand.AeccApplication.10.0&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; oAeccDoc = oAeccApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; oAeccDB = oAeccApp.ActiveDocument.Database</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Select the 3D Polyline which you want to convert to Feature Line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> promptEntOp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Select a 3D Polyline : &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> promptEntRs </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; promptEntRs = ed.GetEntity(promptEntOp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> promptEntRs.Status &lt;&gt; </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Exiting! Try Again !&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> idEnt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; idEnt = promptEntRs.ObjectId</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oFtrLn </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccLandFeatureLine</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oFtrLns </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccLandFeatureLines</span><span style="line-height: 140%;"> = oAeccDoc.Sites.Item(0).FeatureLines</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> plineObjId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Long</span><span style="line-height: 140%;"> = idEnt.OldIdPtr</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; oFtrLn = oFtrLns.AddFromPolyline(plineObjId, oAeccDB.FeatureLineStyles.Item(0))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Error : &quot;</span><span style="line-height: 140%;">, ex.Message &amp; vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
</div>
<p>&#0160;</p>
<p>Once you
build your application using above code snippet and run it in Civil 3D , you
would see a Featureline being created -</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901bb43d4b970b-pi" style="display: inline;"><img alt="Add_featureline_COM" class="asset  asset-image at-xid-6a0167607c2431970b01901bb43d4b970b" src="/assets/image_0cf339.jpg" title="Add_featureline_COM" /></a></p>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>
