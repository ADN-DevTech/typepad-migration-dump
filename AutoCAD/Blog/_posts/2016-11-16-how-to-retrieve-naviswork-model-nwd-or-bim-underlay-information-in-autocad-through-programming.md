---
layout: "post"
title: "How To Retrieve Naviswork Model NWD Or BIM Underlay Information In AutoCAD Through Programming"
date: "2016-11-16 23:06:00"
author: "Madhukar Moogala"
categories:
  - "2015"
  - "2016"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2016/11/how-to-retrieve-naviswork-model-nwd-or-bim-underlay-information-in-autocad-through-programming.html "
typepad_basename: "how-to-retrieve-naviswork-model-nwd-or-bim-underlay-information-in-autocad-through-programming"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>I have received a query from an ADN partner, how to read the scale, rotation angle, insertion point and underlay reference path of&nbsp; NWD model, though API to handle such information is not exposed to ObjectARX, but still not to loose hope we can retrieve the same through reading DXF.</p> <p>When a Naviswork model is attached in AutoCAD through _COORDINATIONMODELATTACH user is invoked to enter insertion point,scale and other parameters and the model is attached a overlay in AutoCAD model space creating a ACDBNAVISWORKSMODELDEF symbol table and stored in the ACAD_BIM_DEFINITIONS dictionary. The corresponding reference entity in Model space is AcDbNavisworkModel.</p> <p>AcDbNavisworkModel is not an AutoCAD native entity, it is created by a DBX application <strong>AcBIMUnderlay.dbx</strong></p> <p>Useful links to understand what each designated DXF group meant for AcDbNavisworkModel.</p> <p><a href="http://help.autodesk.com/view/ACD/2016/ENU/?guid=GUID-F57A7CC8-02C8-4829-9391-EE97F33FE8F3">COORDINATION MODEL (DXF)</a></p> <p><a href="http://help.autodesk.com/view/ACD/2016/ENU/?guid=GUID-9689E3B8-8A07-4E23-8C66-20B1A73F11CB">ACDBNAVISWORKSMODELDEF (DXF)</a></p> <p>&nbsp;</p> <p><strong></strong></p><pre>/*Get  CoordinationModel*/
void test5()
{
	
	#pragma region Retrieve NWD Model from BlockTable
	AcDbObjectId entId;
	
	AcDbObjectIdArray blockEnts;

	AcDbBlockTable* blkTbl;
	AcDbDatabase* db = acdbHostApplicationServices()-&gt;workingDatabase();
	if (!eOkVerify(db-&gt;getSymbolTable(blkTbl, AcDb::kForRead))) return;


	// open named block and get NWD model
	AcDbBlockTableRecord* blkRec;
	AcDbBlockTableRecordIterator* iter;
	if (!eOkVerify(blkTbl-&gt;getAt(ACDB_MODEL_SPACE, blkRec, AcDb::kForRead))) return;

	if (!eOkVerify(blkRec-&gt;newIterator(iter, true, true)))
	{
		blkRec-&gt;close(); return;
	}

	for (; !iter-&gt;done(); iter-&gt;step(true, true)) {
		Acad::ErrorStatus es = iter-&gt;getEntityId(entId);
		if (es == Acad::eOk)
			blockEnts.append(entId);
	}

	blkRec-&gt;close();
	delete iter;
	blkTbl-&gt;close();

	#pragma endregion

	#pragma region Get entityname from ObjectId
	/*test drawing has only one NWD and it would be the first one*/
	ads_name ent_name;
	if(!eOkVerify(acdbGetAdsName(ent_name, blockEnts[0]))) return;
	#pragma endregion


	/*Get Resbuf struct of DXF Data of Selected  Model*/
	resbuf* args = acutNewRb(RTSTR);
	acutNewString(_T("*"), args-&gt;resval.rstring);
	resbuf* entdata = acdbEntGetX(ent_name, args);
	acutRelRb(args);
	LoopRbChain(entdata);
	acutRelRb(entdata);

	/*Get File Path*/
	acutPrintf(_T("NWD file path is %s"), nwdData.filePath);
	/*Create a transform Matrix to get Scale\Insertion\Rotation angle*/
	AcGeMatrix3d mat;
	int size = nwdData.rreals.length();
	for (int i = 0; i &lt; 4; i++)
	{
		for (int j = 0; j &lt; 4; j++)
		{
			mat(i,j) = nwdData.rreals.at(4*i + j);
		}
	}
	double scale = mat.scale();
	AcGeVector3d insertionV = mat.translation();
	AcGePoint3d insertionPt(insertionV.x, insertionV.y, insertionV.z);
	
	/*Some Math  to get rotation angle from  Matrix*/
	double acosVal = mat(0, 0) / scale;
	if (acosVal &gt; 1.0)
		acosVal = 1.0;
	else if (acosVal &lt; -1.0)
		acosVal = -1.0;

	double rotAngle = acos(acosVal);
	ASSERT(0.0 &lt;= rotAngle &amp;&amp; rotAngle &lt;= M_PI);
	if (mat(0, 1) &gt; 0.0)
		rotAngle = 2.0 * M_PI - rotAngle;



	acutPrintf(_T("\n Insertion Point [%.2f,%.2f,%.2f],\n Scale = %.2f \n Rotation = %.2f"),
		insertionV.x, insertionV.y, insertionV.z, scale, (rotAngle*180)/M_PI);

}
</pre><pre>struct NWDData
{
	CString filePath;
	AcArray<double> rreals;
};

/*Global Declration*/
NWDData nwdData;
</pre><pre>/*Helper Methods*/
int dxfCodeToDataType(int resType)
{
	// which data type is this value
	if ((resType &gt;= 0) &amp;&amp; (resType &lt;= 9))
		return RTSTR;
	else if ((resType &gt;= 10) &amp;&amp; (resType &lt;= 17))
		return RT3DPOINT;
	else if ((resType &gt;= 38) &amp;&amp; (resType &lt;= 59))
		return RTREAL;
	else if ((resType &gt;= 60) &amp;&amp; (resType &lt;= 79))
		return RTSHORT;
	else if ((resType &gt;= 90) &amp;&amp; (resType &lt;= 99))
		return RTLONG;
	else if ((resType == 100) || (resType == 101) || (resType == 102) || (resType == 105))
		return RTSTR;
	else if ((resType &gt;= 110) &amp;&amp; (resType &lt;= 119))
		return RT3DPOINT;
	else if ((resType &gt;= 140) &amp;&amp; (resType &lt;= 149))
		return RTREAL;
	else if ((resType &gt;= 170) &amp;&amp; (resType &lt;= 179))
		return RTSHORT;
	else if ((resType &gt;= 210) &amp;&amp; (resType &lt;= 219))
		return RT3DPOINT;
	else if ((resType &gt;= 270) &amp;&amp; (resType &lt;= 299))
		return RTSHORT;
	else if ((resType &gt;= 300) &amp;&amp; (resType &lt;= 309))
		return RTSTR;
	else if ((resType &gt;= 310) &amp;&amp; (resType &lt;= 369))
		return RTENAME;
	else if ((resType &gt;= 370) &amp;&amp; (resType &lt;= 379))
		return RTSHORT;
	else if ((resType &gt;= 380) &amp;&amp; (resType &lt;= 389))
		return RTSHORT;
	else if ((resType &gt;= 390) &amp;&amp; (resType &lt;= 399))
		return RTENAME;
	else if ((resType &gt;= 400) &amp;&amp; (resType &lt;= 409))
		return RTSHORT;
	else if ((resType &gt;= 410) &amp;&amp; (resType &lt;= 419))
		return RTSTR;
	else if (resType == 1004)
		return resType;        // binary chunk
	else if ((resType &gt;= 999) &amp;&amp; (resType &lt;= 1009))
		return RTSTR;
	else if ((resType &gt;= 1010) &amp;&amp; (resType &lt;= 1013))
		return RT3DPOINT;
	else if ((resType &gt;= 1038) &amp;&amp; (resType &lt;= 1059))
		return RTREAL;
	else if ((resType &gt;= 1060) &amp;&amp; (resType &lt;= 1070))
		return RTSHORT;
	else if ((resType == 1071))
		return RTLONG;
	else if ((resType &lt; 0) || (resType &gt; 4999))
		return resType;
	else
		return RTNONE;
}
void LoopRbChain(resbuf* entData)
{
	resbuf* tmp = entData;
	CString valueStr;
	//increase size of internal buffer from 128 to 512
	valueStr.GetBuffer(512);
	int count = 0; // To monitor Matrix filling
	while (tmp)
	{
		int dataType = dxfCodeToDataType(tmp-&gt;restype);
	
		switch (dataType)
		{
		case RTSTR:
			if (tmp-&gt;restype == 1) /*For NWD Model file path*/
			{
				if (tmp-&gt;resval.rstring == NULL)
					valueStr = _T("(NULL)");
				else
				{
					valueStr.Format(_T("\"%s\""), tmp-&gt;resval.rstring);
					nwdData.filePath = valueStr;
				}

			}
			break;
		case RTREAL:
			if (tmp-&gt;restype == 40)
			{
				if (nwdData.rreals.length() &lt; 16/*Max size of 4 X 4 transformMatrix*/)
					nwdData.rreals.append(tmp-&gt;resval.rreal);			
			}
			break;
		case RTENAME:
			if (tmp-&gt;restype == 340)
			{
				resbuf* args = acutNewRb(RTSTR);
				acutNewString(_T("*"), args-&gt;resval.rstring);
				resbuf* entdata = acdbEntGetX(tmp-&gt;resval.rlname, args);
				LoopRbChain(entdata);
				acutRelRb(args);
				acutRelRb(entdata);
			}
			break;

		}
		tmp = tmp-&gt;rbnext;
	}
	return ;
}
</pre>
Video Demonstration:
<iframe height="620" src="https://screencast.autodesk.com/Embed/Timeline/e09855b8-2200-4e98-8975-297f59cbb152" frameborder="0" width="640" allowfullscreen webkitallowfullscreen></iframe>
