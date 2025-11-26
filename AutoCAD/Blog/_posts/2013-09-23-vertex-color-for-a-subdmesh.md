---
layout: "post"
title: "Vertex color for a SubDMesh"
date: "2013-09-23 23:46:59"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/09/vertex-color-for-a-subdmesh.html "
typepad_basename: "vertex-color-for-a-subdmesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To assign per-vertex colors for a SubDMesh, the subDMesh entity must be added to the database before the "AcDbSubDMesh::setVertexColorArray" can be used. Here is a sample code :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Vertex color</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcCmEntityColor vColor; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vColor.setColorMethod(AcCmEntityColor::kByACI); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;AcCmEntityColor&gt; clrArray;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Vertices</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3dArray vertexArray;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vertexArray.setPhysicalLength(4);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Vertex-1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d pt1(0.0, 0.0, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vertexArray.append(pt1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vColor.setColorIndex(1); </span><span style="color: green; line-height: 140%;">// Red</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">clrArray.append(vColor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Vertex-2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d pt2(20.0, 0.0, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vertexArray.append(pt2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vColor.setColorIndex(3); </span><span style="color: green; line-height: 140%;">// Green </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">clrArray.append(vColor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Vertex-3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d pt3(20.0, 10.0, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vertexArray.append(pt3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vColor.setColorIndex(2); </span><span style="color: green; line-height: 140%;">// Yellow</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">clrArray.append(vColor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Vertex-4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d pt4(0.0, 10.0, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vertexArray.append(pt4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">vColor.setColorIndex(5); </span><span style="color: green; line-height: 140%;">// Blue </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">clrArray.append(vColor);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Faces</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;Adesk::Int32&gt; faceArray;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">faceArray.setPhysicalLength(8);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Face-1 (Vertex-1 Vertex-2 Vertex-4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">faceArray.append(3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">faceArray.append(0);faceArray.append(1);faceArray.append(3);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Face-2 (Vertex-2 Vertex-3 Vertex-4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">faceArray.append(3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">faceArray.append(1);faceArray.append(2);faceArray.append(3);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbSubDMesh *pSubDMesh = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbSubDMesh();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus es = pSubDMesh-&gt;setSubDMesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (vertexArray, faceArray, 0);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTable *pBlockTable;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTableRecord *pSpaceRecord;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = acdbHostApplicationServices()-&gt;workingDatabase()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; -&gt;getSymbolTable(pBlockTable, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pBlockTable</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; -&gt;getAt(ACDB_MODEL_SPACE, pSpaceRecord, AcDb::kForWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pBlockTable-&gt;close();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// For Vertex color to work, the SubDMesh must be added </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// to the database </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId meshId = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pSpaceRecord-&gt;appendAcDbEntity(meshId, pSubDMesh);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pSubDMesh-&gt;setVertexColorArray(clrArray);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pSubDMesh-&gt;close();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pSpaceRecord-&gt;close();</span></p>
</div>
<p></p>
<p></p>
<p>Here is the SubDMesh created by the sample code :</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff92beb4970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019aff92beb4970c" alt="Per-VertexColors" title="Per-VertexColors" src="/assets/image_791868.jpg" style="margin: 0px 5px 5px 0px;" /></a>
