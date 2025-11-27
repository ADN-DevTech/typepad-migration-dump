---
layout: "post"
title: "Load iges or Parasolid files into the active Inventor document"
date: "2012-05-29 21:29:54"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/load-iges-or-parasolid-files-into-the-active-inventor-document.html "
typepad_basename: "load-iges-or-parasolid-files-into-the-active-inventor-document"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue<br /> </strong>I want to load iges or parasolid files into the active document of Inventor via the api.  I want this to work in the same way as the [Import] command in Inventor.  The code below works but does not load the iges file into the active document, it creates its own file.</p>
<p><strong>Solution  <br /></strong>The argument “TargetObject” of TranslatorAddIn.Open has to use the new document.  There&#39;s an easy way to do this with Inventor 2011. Use NonParametricBaseFeatures.Add to create a base feature in the desired part file. But it is as designed that NonParametricBaseFeatures.Add should create a solid if there are no other solids in the part and a surface if there is at least one solid in the part. So if batching importing, the next file will be imported as surface.     To have all as solid, you could use CreateDefinition and AddByDefinition methods of the NonParametricBaseFeatures object which were added in Inventor 2011 and support additional capabilities that the Add method doesn’t. The code below imports ttwo Parasolid files to the current part.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> testImport()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; import file 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ImportXB (</span><span style="color: #a31515; line-height: 140%;">&quot;C:\Temp\1.X_B&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; import file 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ImportXB (</span><span style="color: #a31515; line-height: 140%;">&quot;C:\Temp\2.X_B&quot;</span><span style="line-height: 140%;"> )</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; import function</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> ImportXB(fileName </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; assume we have got Inventor application&#0160;&#0160;&#0160; </span>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; get active document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oActiveDoc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PartDocument</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; _InvApplication.ActiveDocument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; get&#0160; ComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oActiveDocDef </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PartComponentDefinition</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; oActiveDoc.ComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; get the&#0160; TranslatorAddIn</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAddin </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TranslatorAddIn</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; _InvApplication.ApplicationAddIns. _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ItemById(</span><span style="color: #a31515; line-height: 140%;">&quot;{A8F8F8E5-BBAB-4F74-8B1B-AC011251F8AC}&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; prepare the arguments for&#0160; TranslatorAddIn&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oContext </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TranslationContext</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; _InvApplication.TransientObjects. _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; CreateTranslationContext</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oContext.Type = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">IOMechanismEnum</span><span style="line-height: 140%;">.kFileBrowseIOMechanism&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oOptions </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">NameValueMap</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; _InvApplication.TransientObjects. _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CreateNameValueMap</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oDataMedium </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DataMedium</span><span style="line-height: 140%;"> = _&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; _InvApplication.TransientObjects.CreateDataMedium</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; set the option ImportSolid = true</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; oOptions.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;ImportSolid&quot;</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; oDataMedium.fileName = fileName</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; import the file to one new part document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oNewDoc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PartDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oAddin.Open(oDataMedium, oContext, oOptions, oNewDoc)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oNewDoc.Views.Add</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; get PartComponentDefinition of the new part document&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oNewDocDef </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PartComponentDefinition</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; oNewDoc.ComponentDefinition </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; create one Definition of NonParametricBaseFeature</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oNPFD </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">NonParametricBaseFeatureDefinition</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; oActiveDocDef.Features.NonParametricBaseFeatures. _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CreateDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oObjColl </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectCollection</span><span style="line-height: 140%;"> = _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; _InvApplication.TransientObjects.CreateObjectCollection()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39;check the solids</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oSB </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SurfaceBody</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> oSB </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> oNewDocDef.SurfaceBodies</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; oObjColl.Add (oSB)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oNPFD.BRepEntities = oObjColl</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oNPFD.OutputType =</span><span style="color: #2b91af; line-height: 140%;">BaseFeatureOutputTypeEnum</span><span style="line-height: 140%;">.kSolidOutputType </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; add the bodies of the new part document to the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; active document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oActiveDocDef.Features.NonParametricBaseFeatures. _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AddByDefinition( oNPFD&#0160; )</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39;close the temp doc without saving </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; because we do not need it any more</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oNewDoc.Close( </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
