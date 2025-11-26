---
layout: "post"
title: "Check If SOLID3D Has Valid Shape Manger Object"
date: "2017-10-24 00:09:29"
author: "Madhukar Moogala"
categories:
  - "2017"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/10/check-if-solid3d-has-valid-shape-manger-object.html "
typepad_basename: "check-if-solid3d-has-valid-shape-manger-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?lang=css&amp;skin=sunburst"></script>
<p>Having Solid’s with invalid shape manager object is always prone to fatal crash it is suggested to check the sanity of underlying shape manager object for a given AcDb3dSolid.</p>
<p>You can check using SOLIDEDIT command.</p>
<p>&nbsp;</p>
<pre class="prettyprint lang-lisp">;51 is Handle of the solid entity
(command "._solidedit" "B" "C" "51" "X" "X")
</pre>
<p>With API</p>
<pre class="prettyprint">Acad::ErrorStatus selectEntity(AcDbObjectId&amp; eId)
{
    ads_name en;
    ads_point pt;	
    ads_entsel(_T("\nSelect an entity: "), en, pt);
	return acdbGetObjectId(eId, en);
}

void solidCheckForValidASM()
{
	AcDbObjectId solId = AcDbObjectId::kNull;
	if (!eOkVerify(selectEntity(solId))) return;

	AcDbSmartObjectPointer pSolid(solId, AcDb::kForRead);
	if (!eOkVerify(pSolid.openStatus())) return;	

	//Get access underlying raw pointer
	if (nullptr == pSolid.object()) return;
	std::unique_ptr pEnt(new AcBrBrep());

	//pass raw pointer to BREP API
	if(pEnt-&gt;set(*pSolid.object())!= AcBr::eOk) return;
	
	bool isGoodAsmBody = pEnt-&gt;checkEntity();
	if (!isGoodAsmBody)
	{
		acedAlert(_T("Null or Invalid Shape Manager"));
	}

	return;
}

</pre>
