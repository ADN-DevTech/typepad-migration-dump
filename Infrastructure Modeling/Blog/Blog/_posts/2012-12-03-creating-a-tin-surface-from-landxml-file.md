---
layout: "post"
title: "Creating a TIN surface from LandXML file"
date: "2012-12-03 00:30:15"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Cloud"
  - "Mobile"
  - "Partha Sarkar"
  - "Web"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/12/creating-a-tin-surface-from-landxml-file.html "
typepad_basename: "creating-a-tin-surface-from-landxml-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD
Civil 3D we can use the <strong>LANDXMLIN</strong> command and import a LandXML file to create
Civil 3D objects like Surface, Alignment, Pipe Network etc. Sometime ago, a
Civil 3D application developer asked me about how do I create a Surface from a
LandXML file using API ? </p>
<p>Civil 3D has
the following COM API to create a Surface from LandXML -</p>
<p><em><strong>IAeccSurfaces::ImportXML()</strong></em> -&gt; <em>Given the specified surface name and XML file,
it creates a new surface from the imported XML data, adds it to the collection,
and returns an instance of the new object.</em> </p>
<p>.NET equivalent of this COM API is not yet available (in the
current release), however, we can use COM Interop and create a .NET
application. Here is a relevant C# code snippet -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Civil3DImportLandXML(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> filelocation)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//get editor and database </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AcadApplication</span><span style="line-height: 140%;"> oApp = (</span><span style="color: #2b91af; line-height: 140%;">AcadApplication</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.AcadApplication;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> sAppName = </span><span style="color: #a31515; line-height: 140%;">&quot;AeccXUiLand.AeccApplication.10.0&quot;</span><span style="line-height: 140%;">; <span style="color: #60bf00;">// Civil 3D 2013</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AeccApplication</span><span style="line-height: 140%;"> aeccApp = (</span><span style="color: #2b91af; line-height: 140%;">AeccApplication</span><span style="line-height: 140%;">)oApp.GetInterfaceObject(sAppName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AeccDocument</span><span style="line-height: 140%;"> aeccDoc = (</span><span style="color: #2b91af; line-height: 140%;">AeccDocument</span><span style="line-height: 140%;">)aeccApp.ActiveDocument;&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get the AeccSurfaces object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccSurfaces</span><span style="line-height: 140%;"> oAeccSurfaces = aeccDoc.Surfaces;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> landxmlFile = filelocation;&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">&#0160; &#0160; oAeccSurfaces.ImportXML(landxmlFile);</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Call AutoCAD COM API ZoomExtents to see the Surface object </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; oApp.ZoomExtents();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span><span style="color: gray; line-height: 140%;">///</span></p>
</div>
<p>Here is the
result of calling the above function in Civil 3D 2013 [I used Civil 3D tutorial
dataset here] -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5dafe0e970d-pi" style="display: inline;"><img alt="LandXML_Imported" class="asset  asset-image at-xid-6a0167607c2431970b017ee5dafe0e970d" src="/assets/image_f5271b.jpg" title="LandXML_Imported" /></a><br /><br /></p>
<p>While trying
this in Civil 3D, I was thinking how about keeping my LandXML in Cloud storage
and using it from anywhere, anytime ? Does it sound interesting ? I will
continue this to see how we can store Civil 3D LandXML files in cloud and use
them whenever and wherever I want to create a TIN surface.</p>
<br />
