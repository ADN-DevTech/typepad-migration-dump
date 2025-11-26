---
layout: "post"
title: "Creating a simple Polygon Mesh"
date: "2016-08-23 06:17:17"
author: "Deepak A S Nadig"
categories:
  - "AutoCAD"
  - "Deepak A S Nadig"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2016/08/creating-a-simple-polygon-mesh.html "
typepad_basename: "creating-a-simple-polygon-mesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Here is a sample code to create a simple Polygon mesh using ObjectARX. When&nbsp;AcDbPolygonMesh() constructor is used without any parameters,vertex count in M and N directions has to be set explicitly and it needs to be specified if PolygonMesh is to be open or closed in M and N directions.&nbsp;</p>
<pre>
<p>void createSimplePolygonMesh() <br />{<br /> // polyline creation <br /> AcGePoint3dArray ptArr;<br /> ptArr.setLogicalLength(4);<br /> for (int i = 0; i &lt; 4; i++) <br /> {<br /> ptArr[i].set((double)(i/2), (double)(i%2), 0.0);<br /> }<br /> AcDb2dPolyline *pNewPline = new AcDb2dPolyline( AcDb::k2dSimplePoly, ptArr, 0.0, Adesk::kTrue);<br /> pNewPline-&gt;setColorIndex(3);</p>
<p>//polygon mesh constructor without any&nbsp;parameter<br /> AcDbPolygonMesh *pMesh = new AcDbPolygonMesh(); <br /> pMesh-&gt;setMSize(1);<br /> pMesh-&gt;setNSize(4);<br /> pMesh-&gt;makeMClosed();<br /> pMesh-&gt;makeNClosed();</p>
<p>AcDbVoidPtrArray arr; <br /> arr.append(pMesh);</p>
<p>AcDbBlockTable *pBlockTable;<br /> acdbHostApplicationServices()-&gt;workingDatabase()-&gt;getSymbolTable(pBlockTable, AcDb::kForRead);</p>
<p>AcDbBlockTableRecord *pBlockTableRecord;<br /> pBlockTable-&gt;getAt(ACDB_MODEL_SPACE, pBlockTableRecord,AcDb::kForWrite);<br /> pBlockTable-&gt;close();</p>
<p>AcDbObjectId plineObjId;<br /> pBlockTableRecord-&gt;appendAcDbEntity(plineObjId,pNewPline);</p>
<p>AcDbObjectIterator *pVertIter= pNewPline-&gt;vertexIterator();<br /> AcDb2dVertex *pVertex;<br /> AcGePoint3d location;<br /> AcDbObjectId vertexObjId; <br /> for (int vertexNumber = 0; !pVertIter-&gt;done();<br /> vertexNumber++, pVertIter-&gt;step())<br /> {<br /> vertexObjId = pVertIter-&gt;objectId();<br /> acdbOpenObject(pVertex, vertexObjId,<br /> AcDb::kForRead);<br /> location = pVertex-&gt;position();<br /> pVertex-&gt;close(); <br /> AcDbPolygonMeshVertex* polyVertex = new AcDbPolygonMeshVertex(pVertex-&gt;position()); <br /> pMesh-&gt;appendVertex(polyVertex);<br /> polyVertex-&gt;close();<br /> }<br /> delete pVertIter;</p>
<p>pBlockTableRecord-&gt;appendAcDbEntity(pMesh);<br /> pBlockTableRecord-&gt;close();<br /> pNewPline-&gt;close();<br /> pMesh-&gt;close(); <br />}</p>
<p>&nbsp;</p>
</pre>
