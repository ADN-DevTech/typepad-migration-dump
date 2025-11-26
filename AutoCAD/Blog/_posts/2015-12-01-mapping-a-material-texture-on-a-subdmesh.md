---
layout: "post"
title: "Mapping a material texture on a SubDMesh"
date: "2015-12-01 01:58:54"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/12/mapping-a-material-texture-on-a-subdmesh.html "
typepad_basename: "mapping-a-material-texture-on-a-subdmesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When a material is assigned to the SubDMesh, the mapper (AcGiMapper)&nbsp;associated with the material takes care of most of the details involved in mapping the texture on to the SubDMesh. The mapper can be configured for its projection, scaling, translation, tiling and such parameters that affect the texture mapping. This approach is suitable for complex texture mapping and if your mapping fits one of the existing projection methods that AcGiMapper provides. In this blog post, we will look at another approach that relies on AcDbSubDMesh::setVertexTextureArray to set the texture mapping. If the SubDMesh was created using vertex and face information and you have an image that needs to be mapped, the "setVertexTextureArray" can be used to control the mapping.</p>
<p>Thanks to Erik Larsen from our AutoCAD engineering team for his help in getting the mapping right. Please note that the texture coordinates are normalized and does not depend on the actual image dimensions.</p>
<p> Here is a screenshot of an image mapped on to a box shaped SubDMesh.</p>
<p></p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0897a619970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0897a619970d img-responsive" alt="Map" title="Map" src="/assets/image_767018.jpg" style="margin: 0px 5px 5px 0px;" /></a>
</p>
<p>Here is the code : </p>
<pre>
static void SubDMeshWithTexture()
{
	AcDbSubDMesh *ptrMesh = new AcDbSubDMesh();
		
	int imgWidth = 320;
	int imgHeight = 160;

	// We will create a box shaped SubDMesh
	// The four side put together match the 
	// image width. 
	// This will let us fully wrap the image on the box shaped mesh.
	int cx = imgWidth / 4;
	int cy = cx;
	int cz = imgHeight;

	double points[30];
	int triangles[24];

	// Vertices 
	points[0]=0.0; points[1]=0.0; points[2]=0.0;
	points[3]=cx; points[4]=0.0; points[5]=0.0;
	points[6]=cx; points[7]=cy; points[8]=0.0;
	points[9]=0.0; points[10]=cy; points[11]=5.0;

	points[12]=0.0; points[13]=0.0; points[14]=cz;
	points[15]=cx; points[16]=0.0; points[17]=cz;
	points[18]=cx; points[19]=cy; points[20]=cz;
	points[21]=0.0; points[22]=cy; points[23]=cz;

	points[24]=0.0; points[25]=0.0; points[26]=0.0;
	points[27]=0.0; points[28]=0.0; points[29]=cz;

	// Face information as triangles
	triangles[0]=0; triangles[1]=1; triangles[2]=5;
	triangles[3]=0; triangles[4]=5; triangles[5]=4;

	triangles[6]=7; triangles[7]=6; triangles[8]=2;
	triangles[9]=7; triangles[10]=2; triangles[11]=3;

	triangles[12]=6; triangles[13]=5; triangles[14]=1;
	triangles[15]=6; triangles[16]=1; triangles[17]=2;

	triangles[18]=8; triangles[19]=7; triangles[20]=3;
	triangles[21]=8; triangles[22]=9; triangles[23]=7;

	// Create the subDMesh using vertex and face data
	Acad::ErrorStatus es  = CreateSubDMesh
				(10, 8, points,triangles, ptrMesh);
	if(es == Acad::eOk)
	{
		AcDbObjectId meshId = AcDbObjectId::kNull;
		AcDbBlockTable*        pBlockTable;
		AcDbBlockTableRecord*  pSpaceRecord;

		AcDbDatabase *pDb = acdbCurDwg();

		pDb->getBlockTable(pBlockTable, AcDb::kForRead);
		pBlockTable->getAt(
			ACDB_MODEL_SPACE, 
			pSpaceRecord, AcDb::kForWrite);
		es = pSpaceRecord->appendAcDbEntity(
										meshId, ptrMesh);
		es = ptrMesh->close();

		es = pSpaceRecord->close();
		es = pBlockTable->close();

		AcDbEntity* pEnt = NULL;
		acdbOpenAcDbEntity(pEnt, meshId, AcDb::kForWrite);

		// Create a material with our custom image 
		// as its texture
		CreateMaterial(_T("Fusion"));

		AcDbSubDMesh *pSubDMesh = AcDbSubDMesh::cast(pEnt);
		if(pSubDMesh != NULL)
		{
			// Assign the material to the subDMesh
			pSubDMesh->setMaterial(L"Fusion", Adesk::kTrue);

			// Ensure the material mapping is as we expect
			// fully wrapped around the faces
			AcGePoint3dArray textureArray;
			textureArray.append(AcGePoint3d(0.0, 0.0, 0.0));
			textureArray.append(AcGePoint3d(0.25, 0.0, 0.0));
			textureArray.append(AcGePoint3d(0.5, 0.0, 0.0));
			textureArray.append(AcGePoint3d(0.75, 0.0, 0.0));
			textureArray.append(AcGePoint3d(0.0, 1.0, 0.0));
			textureArray.append(AcGePoint3d(0.25, 1.0, 0.0));
			textureArray.append(AcGePoint3d(0.5, 1.0, 0.0));
			textureArray.append(AcGePoint3d(0.75, 1.0, 0.0));

			textureArray.append(AcGePoint3d(1.0, 0.0, 0.0));
			textureArray.append(AcGePoint3d(1.0, 1.0, 0.0));

			pSubDMesh->setVertexTextureArray(textureArray);
		}
		pEnt->close();
	}
}

static Acad::ErrorStatus CreateSubDMesh(int numberOfPoints, 
									int numberOfTriangles,
									double *points,
									int *triangles, 
									AcDbSubDMesh *ptrMesh) 
{
	if((numberOfPoints <3) || (numberOfTriangles<1)) 
		return eNotImplementedYet;

	Acad::ErrorStatus es;
	AcGePoint3dArray *vertexArray 
			= new AcGePoint3dArray ();
	AcArray<Adesk::Int32> *faceArray 
			= new AcArray<Adesk::Int32>();

	vertexArray->setPhysicalLength(numberOfPoints*3);
	faceArray->setPhysicalLength(numberOfTriangles*3);

	for(int i=0; i<numberOfPoints; i++) 
	{
		AcGePoint3d pt;
		pt.set(points[i*3],points[i*3+1],points[i*3+2]);
		vertexArray->append(pt);
	}

	int id=-1;
	for(int i=0; i<numberOfTriangles; i++) {

		id++; faceArray->insertAt(id,3); 
		
		id++; faceArray->insertAt(id,triangles[i*3]);

		id++; faceArray->insertAt(id,triangles[i*3+1]);

		id++; faceArray->insertAt(id,triangles[i*3+2]); 
	};

	es = ptrMesh->setSubDMesh(*vertexArray,*faceArray, 0);

	delete vertexArray;
	delete faceArray;

	return es;
}

static void CreateMaterial(
		const ACHAR* name, AcDbDatabase *pDb) 
{ 
	AcDbDictionary *pMaterialDict; 
	Acad::ErrorStatus es; 
	es = pDb->getMaterialDictionary(
		pMaterialDict, AcDb::kForWrite);
	if (es == Acad::eOk)
	{ 
        AcGiImageFileTexture tex;
		tex.setSourceFileName(
			_T("D:\\TestFiles\\FusionLogo.jpg"));
 
		double uScale = 1.0;
		double vScale = 1.0;
        double uOffset = 0;
		double vOffset = 0;
 
        AcGeMatrix3d mx;
		mx(0, 0) = uScale;
		mx(0, 1) = 0;
		mx(0, 2) = 0;
		mx(0, 3) = uScale * uOffset;

		mx(1, 0) = 0;
		mx(1, 1) = vScale;
		mx(1, 2) = 0;
		mx(1, 3) = vScale * vOffset;

		mx(2, 0) = 0;
		mx(2, 1) = 0;
		mx(2, 2) = 1;
		mx(2, 3) = 0;

		mx(3, 0) = 0;
		mx(3, 1) = 0;
		mx(3, 2) = 0;
		mx(3, 3) = 1;
			 
        AcGiMapper mapper;
		mapper.setProjection
			(AcGiMapper::Projection::kBox);
		mapper.setUTiling
			(AcGiMapper::Tiling::kCrop);
		mapper.setVTiling
			(AcGiMapper::Tiling::kCrop);
		mapper.setAutoTransform
			(AcGiMapper::AutoTransform::kNone);
		mapper.setTransform(mx);

        AcGiMaterialMap map;
		map.setSourceFileName(name);
		map.setTexture(&tex);
		map.setBlendFactor(1.0);
		map.setMapper(mapper);

		AcDbMaterial *pMaterialObj 
			= new AcDbMaterial(); 
		pMaterialObj->setName(name);

		AcDbObjectId materialId; 
		es = pMaterialDict->setAt
			(name, pMaterialObj, materialId); 

		AcGiMaterialColor diffuseColor; 
		pMaterialObj->setDiffuse
			(diffuseColor, map);
		pMaterialObj->setMode
			(AcGiMaterialTraits::kRealistic);

		pMaterialObj->close(); 
		pMaterialDict->close(); 
	} 
} 
</pre>
