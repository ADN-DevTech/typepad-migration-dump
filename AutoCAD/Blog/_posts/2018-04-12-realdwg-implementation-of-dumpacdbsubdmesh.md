---
layout: "post"
title: "RealDWG: Implementation of dumpAcDbSubDMesh"
date: "2018-04-12 01:34:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2018/04/realdwg-implementation-of-dumpacdbsubdmesh.html "
typepad_basename: "realdwg-implementation-of-dumpacdbsubdmesh"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>In RealDWG dumpDWG sample project [RealDWG 2018\Samples\DumpDWG], dumpAcDbSubDMesh implementation is missing, I received a query from a ADN partner, so for the benefit of every one I blogging this.</p><p><strong>Context</strong>: If input drawing to your <a href="http://adndevblog.typepad.com/autocad/2015/04/introduction-to-realdwg-net-programming.html">RealDWG</a> app contains Mesh elements, and you would like to view this elements in any other 3d viewer for example, OpenGL, this implementation will come handy.</p>

<pre class="prettyprint">void dumpAcDbSubDMesh(AcDbEntity *pEnt)
{
entInfo(pEnt, sizeof(AcDbSubDMesh));
if (pEnt-&gt;isKindOf(AcDbSubDMesh::desc())) 
{
	entInfo(pEnt, sizeof(AcDbSubDMesh));
	AcDbSubDMesh *pMesh = AcDbSubDMesh::cast(pEnt);
	Adesk::Int32  nFaces, 
					nEdges, 
					nVertices,
					nSubFaces,
					nSubVertices,
					nSubdLevel;
	bool bWaterTight;
	Acad::ErrorStatus es = pMesh-&gt;numOfFaces(nFaces);
	es = pMesh-&gt;numOfVertices(nVertices);
	AcGePoint3d pt;
	// this is just for simplicity instead of 
	//traversing all the vertices. 
	//if the vertices &gt; 50 it increments by that number fold.
	int increment = nVertices / 50;
	if (!increment)increment++;
	for (int i = 0; i<nvertices  ; {="" +="increment)" pmesh-="" \n"));="" points="" vertex="" _print(_t("subdmesh="" i="">getVertexAt(i, pt);
		_print(_T("\t\t Vertex Points[%d] %g,%g,%g\n"), i,
			pt.x, pt.y, pt.z);
	}

	es = pMesh-&gt;numOfSubDividedFaces(nSubFaces);
	es = pMesh-&gt;numOfSubDividedVertices(nSubVertices);
	es = pMesh-&gt;isWatertight(bWaterTight);
	es = pMesh-&gt;subdLevel(nSubdLevel);
	es = pMesh-&gt;numOfEdges(nEdges);
	
	AcGePoint3dArray nsubVerticesArray;
	es = pMesh-&gt;getSubDividedVertices(nsubVerticesArray);

	AcArray<adesk::int32> nsubfacesArray;
	es = pMesh-&gt;getSubDividedFaceArray(nsubfacesArray);
	// The content of the faces list is like so :
	// [number of vertices of next face],
	// <vertexindex1  , etc="" vertexindex2,="">,
	// e.g. [4], &lt;1, 2, 3, 4&gt;, [3], &lt;1, 2, 3&gt;, etc


	for (int numVertices = 0, 
		i = 0; 
		i &lt; nsubfacesArray.length();
		i += numVertices + 1)
	{
	numVertices = nsubfacesArray.at(i);
	// Let's print the edges of the face
	_print(_T("\t SubDMesh Edge Points \n"));
	_print(_T("\t\t Face vertex Points[%d] \n"), numVertices);						
	for (int j = 0; j &lt; numVertices; j++)
	{
		AcGePoint3d startPt = 
		nsubVerticesArray.at(nsubfacesArray.at(i + j + 1));
		AcGePoint3d endPt =
		nsubVerticesArray.at(nsubfacesArray.at(i + ((j + 1) % numVertices) + 1));							
		_print(_T("\t\t Vertex Points[%d] [{%g,%g,%g} ,{ %g,%g,%g }]\n"), j,
			startPt.x, startPt.y, startPt.z , endPt.x, endPt.y, endPt.z);
		//Now you can draw lines using these points in OpenGl

	}
	}
}
}
</vertexindex1,></adesk::int32></nvertices;></pre>

<p>Sample Output</p>
<pre>SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{10.1735,8.28866,16.5088} ,{ 16.5135,8.28866,16.5088 }]
                 Vertex Points[1] [{16.5135,8.28866,16.5088} ,{ 16.5135,11.4619,16.5088 }]
                 Vertex Points[2] [{16.5135,11.4619,16.5088} ,{ 10.1735,11.4619,16.5088 }]
                 Vertex Points[3] [{10.1735,11.4619,16.5088} ,{ 10.1735,8.28866,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{10.1735,5.11544,16.5088} ,{ 16.5135,5.11544,16.5088 }]
                 Vertex Points[1] [{16.5135,5.11544,16.5088} ,{ 16.5135,8.28866,16.5088 }]
                 Vertex Points[2] [{16.5135,8.28866,16.5088} ,{ 10.1735,8.28866,16.5088 }]
                 Vertex Points[3] [{10.1735,8.28866,16.5088} ,{ 10.1735,5.11544,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{10.1735,1.94223,16.5088} ,{ 16.5135,1.94223,16.5088 }]
                 Vertex Points[1] [{16.5135,1.94223,16.5088} ,{ 16.5135,5.11544,16.5088 }]
                 Vertex Points[2] [{16.5135,5.11544,16.5088} ,{ 10.1735,5.11544,16.5088 }]
                 Vertex Points[3] [{10.1735,5.11544,16.5088} ,{ 10.1735,1.94223,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{16.5135,8.28866,16.5088} ,{ 22.8534,8.28866,16.5088 }]
                 Vertex Points[1] [{22.8534,8.28866,16.5088} ,{ 22.8534,11.4619,16.5088 }]
                 Vertex Points[2] [{22.8534,11.4619,16.5088} ,{ 16.5135,11.4619,16.5088 }]
                 Vertex Points[3] [{16.5135,11.4619,16.5088} ,{ 16.5135,8.28866,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{16.5135,5.11544,16.5088} ,{ 22.8534,5.11544,16.5088 }]
                 Vertex Points[1] [{22.8534,5.11544,16.5088} ,{ 22.8534,8.28866,16.5088 }]
                 Vertex Points[2] [{22.8534,8.28866,16.5088} ,{ 16.5135,8.28866,16.5088 }]
                 Vertex Points[3] [{16.5135,8.28866,16.5088} ,{ 16.5135,5.11544,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{16.5135,1.94223,16.5088} ,{ 22.8534,1.94223,16.5088 }]
                 Vertex Points[1] [{22.8534,1.94223,16.5088} ,{ 22.8534,5.11544,16.5088 }]
                 Vertex Points[2] [{22.8534,5.11544,16.5088} ,{ 16.5135,5.11544,16.5088 }]
                 Vertex Points[3] [{16.5135,5.11544,16.5088} ,{ 16.5135,1.94223,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{22.8534,8.28866,16.5088} ,{ 29.1934,8.28866,16.5088 }]
                 Vertex Points[1] [{29.1934,8.28866,16.5088} ,{ 29.1934,11.4619,16.5088 }]
                 Vertex Points[2] [{29.1934,11.4619,16.5088} ,{ 22.8534,11.4619,16.5088 }]
                 Vertex Points[3] [{22.8534,11.4619,16.5088} ,{ 22.8534,8.28866,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{22.8534,5.11544,16.5088} ,{ 29.1934,5.11544,16.5088 }]
                 Vertex Points[1] [{29.1934,5.11544,16.5088} ,{ 29.1934,8.28866,16.5088 }]
                 Vertex Points[2] [{29.1934,8.28866,16.5088} ,{ 22.8534,8.28866,16.5088 }]
                 Vertex Points[3] [{22.8534,8.28866,16.5088} ,{ 22.8534,5.11544,16.5088 }]
         SubDMesh Edge Points
                 Face vertex Points[4]
                 Vertex Points[0] [{22.8534,1.94223,16.5088} ,{ 29.1934,1.94223,16.5088 }]
                 Vertex Points[1] [{29.1934,1.94223,16.5088} ,{ 29.1934,5.11544,16.5088 }]
                 Vertex Points[2] [{29.1934,5.11544,16.5088} ,{ 22.8534,5.11544,16.5088 }]
                 Vertex Points[3] [{22.8534,5.11544,16.5088} ,{ 22.8534,1.94223,16.5088 }]
</pre>
