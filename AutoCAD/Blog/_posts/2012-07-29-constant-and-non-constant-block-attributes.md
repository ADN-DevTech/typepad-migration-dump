---
layout: "post"
title: "Constant and Non-Constant Block Attributes"
date: "2012-07-29 08:40:02"
author: "Balaji"
categories:
  - "2013"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/constant-and-non-constant-block-attributes.html "
typepad_basename: "constant-and-non-constant-block-attributes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When iterating attributes of a specific block reference, you may not find&nbsp;all of them as expected. The reason for this is because an AcDbBlockTableRecord owns all attribute definitions, and an AcDbBlockReference (the insert of a block) owns all the non-constant attributes. If a block definition defines some constant attributes, AutoCAD will not create attributes in the AcDbBlockReference for them. By iterating the AcDbBlockReference object, you can find only the non-constant attributes, if you also want the constant attributes, you must iterate the AcDbBlockTableRecord as well.</p>
<p>The following sample code demonstrates this:</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name ename ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pt ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedEntSel (L</span><span style="color: #a31515; line-height: 140%;">&quot;Select a Block Reference: &quot;</span><span style="line-height: 140%;">, ename, pt) ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId id ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbGetObjectId (id, ename) ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockReference *pRef ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbOpenAcDbObject ((AcDbObject *&amp;)pRef, id, AcDb::kForRead) ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//----- List Non-Const attributes </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectIterator&nbsp; *pIter ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter = pRef-&gt;attributeIterator () ; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> cnt =0 ; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> ( !pIter-&gt;done () )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; cnt++ ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId ida =pIter-&gt;objectId () ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf (L</span><span style="color: #a31515; line-height: 140%;">&quot;\nAttribute %d - ObjectId %ld&quot;</span><span style="line-height: 140%;">, cnt, ida) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pIter-&gt;step();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pIter ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf (L</span><span style="color: #a31515; line-height: 140%;">&quot;\n%d Non-Const Attribute(s) Found!&quot;</span><span style="line-height: 140%;">, cnt) ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//----- List Constant attributes </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">cnt =0 ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">id =pRef-&gt;blockTableRecord () ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTableRecord *pRec ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbOpenAcDbObject ((AcDbObject *&amp;)pRec, id, AcDb::kForRead) ;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> ( pRec-&gt;hasAttributeDefinitions () == Adesk::kTrue ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbBlockTableRecordIterator *pIter2 ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pRec-&gt;newIterator (pIter2) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> ( !pIter2-&gt;done () )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDbEntity *pEnt ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pIter2-&gt;getEntity (pEnt, AcDb::kForRead) ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> ( pEnt-&gt;isKindOf (AcDbAttributeDefinition::desc ()) )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDbAttributeDefinition *pDef=(AcDbAttributeDefinition *)pEnt ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> ( pDef-&gt;isConstant () == Adesk::kTrue ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cnt++; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutPrintf (L</span><span style="color: #a31515; line-height: 140%;">&quot;\nConstant Attribute %d - ObjectId %ld)&quot;</span><span style="line-height: 140%;">, cnt, pDef-&gt;objectId()) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pEnt-&gt;close () ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pIter2-&gt;step () ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pIter2 ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf (L</span><span style="color: #a31515; line-height: 140%;">&quot;\n%d Constant Attribute(s) Found!&quot;</span><span style="line-height: 140%;">, cnt) ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pRec-&gt;close () ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pRef-&gt;close () ;</span></p>
</div>
