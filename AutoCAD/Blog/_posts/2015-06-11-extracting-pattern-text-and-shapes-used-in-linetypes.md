---
layout: "post"
title: "Extracting pattern, text and shapes used in linetypes"
date: "2015-06-11 00:29:31"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/06/extracting-pattern-text-and-shapes-used-in-linetypes.html "
typepad_basename: "extracting-pattern-text-and-shapes-used-in-linetypes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>As you may already know, linetype definition in AutoCAD consists of series of values representing the pattern and can include text and shapes. Here is a sample code to iterate the linetypes loaded in the database and display the definition. In case of embedded shapes, a preview of the shape is created by adding an AcDbShape to the modelspace.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">double</span><span style="color: #000000;">  previewOffset = 5.0;</span></pre>
<pre style="margin: 0em;"> AcGePoint3d pos = AcGePoint3d::kOrigin;</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> AcApDocument *pActiveDoc </pre>
<pre style="margin: 0em;"> 	= acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin: 0em;"> AcDbDatabase *pDB = pActiveDoc-&gt;database();</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> AcDbLinetypeTable* pLineTypeTable = NULL; </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;">  ( pDB-&gt;getLinetypeTable(</span></pre>
<pre style="margin: 0em;"> 	pLineTypeTable, AcDb::kForRead) != Acad::eOk) </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = acDocManager-&gt;lockDocument(pActiveDoc);</pre>
<pre style="margin: 0em;"> AcDbBlockTableRecordPointer </pre>
<pre style="margin: 0em;"> 	pMS(ACDB_MODEL_SPACE, pDB, AcDb::kForWrite);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> AcDbLinetypeTableIterator* pIter = NULL; </pre>
<pre style="margin: 0em;"> es = pLineTypeTable-&gt;newIterator( pIter); </pre>
<pre style="margin: 0em;"> es = pLineTypeTable-&gt;close(); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">for</span><span style="color: #000000;"> (;! pIter-&gt;done(); pIter-&gt;step()) </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span> </pre>
<pre style="margin: 0em;"> 	AcDbLinetypeTableRecord* pLinetype = NULL; </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( pIter-&gt;getRecord( pLinetype, </span></pre>
<pre style="margin: 0em;"> 		AcDb::kForRead) == Acad::eOk) </pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span> </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">const</span><span style="color: #000000;">  ACHAR *pLinetypeName = NULL; </span></pre>
<pre style="margin: 0em;"> 		es = pLinetype-&gt;getName(pLinetypeName); </pre>
<pre style="margin: 0em;"> 		acutPrintf(_T(<span style="color: #a31515;">"\\nLinetype : %s"</span><span style="color: #000000;"> ), </span></pre>
<pre style="margin: 0em;"> 			pLinetypeName); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">for</span><span style="color: #000000;"> ( <span style="color: #0000ff;">int</span><span style="color: #000000;">  dash = 0; dash &lt; </span></span></pre>
<pre style="margin: 0em;"> 			pLinetype-&gt;numDashes(); ++dash) </pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span> </pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">int</span><span style="color: #000000;">  shapeNumber </span></pre>
<pre style="margin: 0em;"> 				= pLinetype-&gt;shapeNumberAt(dash);</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">double</span><span style="color: #000000;">  dashLen </span></pre>
<pre style="margin: 0em;"> 				= pLinetype-&gt;dashLengthAt(dash); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			AcDbObjectId objIdShape</pre>
<pre style="margin: 0em;"> 				(pLinetype-&gt;shapeStyleAt( dash)); </pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( objIdShape == AcDbObjectId::kNull) </span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span> </pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">const</span><span style="color: #000000;">  ACHAR *pText = NULL; </span></pre>
<pre style="margin: 0em;"> 				es = pLinetype-&gt;textAt(dash, pText);</pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">if</span><span style="color: #000000;"> (pText == NULL)</span></pre>
<pre style="margin: 0em;"> 					acutPrintf(_T(<span style="color: #a31515;">"\\nDash : %d DashLen : %lf"</span><span style="color: #000000;"> ),</span></pre>
<pre style="margin: 0em;"> 					dash,  dashLen); </pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">else</span></pre>
<pre style="margin: 0em;"> 					acutPrintf(_T(<span style="color: #a31515;">"\\nDash : %d Text : %s"</span><span style="color: #000000;"> ), </span></pre>
<pre style="margin: 0em;"> 					dash,  pText); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">continue</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// Shape involved, lets preview it...</span></pre>
<pre style="margin: 0em;"> 			AcDbObject* pObject = NULL;</pre>
<pre style="margin: 0em;"> 			es = acdbOpenAcDbObject(</pre>
<pre style="margin: 0em;"> 				pObject, objIdShape, AcDb::kForRead);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			AcDbTextStyleTableRecord* pTextSyleTableRec </pre>
<pre style="margin: 0em;"> 				= AcDbTextStyleTableRecord::cast( pObject); </pre>
<pre style="margin: 0em;"> 			Adesk::Boolean isShapeFile </pre>
<pre style="margin: 0em;"> 				= pTextSyleTableRec-&gt;isShapeFile();</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;"> (isShapeFile)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #008000;">// Create a preview of the shape</span></pre>
<pre style="margin: 0em;"> 				AcDbShape *pShape = <span style="color: #0000ff;">new</span><span style="color: #000000;">  AcDbShape</span></pre>
<pre style="margin: 0em;"> 					(AcGePoint3d::kOrigin, 1.0); </pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">if</span><span style="color: #000000;"> ( pShape-&gt;setShapeNumber(shapeNumber) </span></pre>
<pre style="margin: 0em;"> 					!= Acad::eOk || </pre>
<pre style="margin: 0em;"> 					pShape-&gt;setStyleId(objIdShape) </pre>
<pre style="margin: 0em;"> 					!= Acad::eOk)</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 					<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pShape;</span></pre>
<pre style="margin: 0em;"> 					pShape = NULL;</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">if</span><span style="color: #000000;"> (pShape)</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 					es = pShape-&gt;setWidthFactor(1.0);</pre>
<pre style="margin: 0em;"> 					AcDbObjectId id;</pre>
<pre style="margin: 0em;"> 					es = pMS-&gt;appendAcDbEntity(id, pShape);</pre>
<pre style="margin: 0em;"> 					es = pShape-&gt;setPosition(pos);</pre>
<pre style="margin: 0em;"> 					pos += </pre>
<pre style="margin: 0em;"> 					AcGeVector3d::kXAxis * previewOffset;</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 					es = pShape-&gt;setSize(1);</pre>
<pre style="margin: 0em;"> 					es = pShape-&gt;setRotation(0);</pre>
<pre style="margin: 0em;"> 					es=pShape-&gt;close();</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 					<span style="color: #0000ff;">const</span><span style="color: #000000;">  ACHAR *pShapeFileName = NULL; </span></pre>
<pre style="margin: 0em;"> 					Acad::ErrorStatus es </pre>
<pre style="margin: 0em;"> 						= pTextSyleTableRec-&gt;fileName</pre>
<pre style="margin: 0em;"> 						(pShapeFileName); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 					acutPrintf(_T(<span style="color: #a31515;">"\\nDash : %d </span></pre>
<pre style="margin: 0em;"> 							ShapeNumber : %d </pre>
<pre style="margin: 0em;"> 							ShapeName : %s </pre>
<pre style="margin: 0em;"> 							ShapeFile : %s<span style="color: #a31515;">"), </span></pre>
<pre style="margin: 0em;"> 							dash, shapeNumber, </pre>
<pre style="margin: 0em;"> 							pShape-&gt;name(), pShapeFileName);</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">else</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">const</span><span style="color: #000000;">  ACHAR *pText = NULL; </span></pre>
<pre style="margin: 0em;"> 				es = pLinetype-&gt;textAt(dash, pText);</pre>
<pre style="margin: 0em;"> 				acutPrintf(_T(<span style="color: #a31515;">"\\nDash : %d Text : %s"</span><span style="color: #000000;"> ), </span></pre>
<pre style="margin: 0em;"> 					dash,  pText); </pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			pObject-&gt;close(); </pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		pLinetype-&gt;close();</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">delete</span><span style="color: #000000;">  pIter;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> es = acDocManager-&gt;unlockDocument(pActiveDoc);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<!-- End block -->
<p></p>
<p>The output generated by this code snippet is as seen in this screenshot :</p>

<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0840b954970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0840b954970d img-responsive" alt="LineType" title="LineType" src="/assets/image_971606.jpg" style="margin: 0px 5px 5px 0px;" /></a>
