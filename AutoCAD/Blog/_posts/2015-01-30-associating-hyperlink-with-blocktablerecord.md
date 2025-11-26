---
layout: "post"
title: "Associating hyperlink with BlockTableRecord"
date: "2015-01-30 03:26:19"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/associating-hyperlink-with-blocktablerecord.html "
typepad_basename: "associating-hyperlink-with-blocktablerecord"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In ObjectARX, BlockTableRecord provides access to its hyperlink collection using the&nbsp;AcDbEntityHyperlinkPE. In AutoCAD 2015, the Hyperlinks collection is now also accessible using the AutoCAD .Net API. &nbsp;</p>
<p>Here is a C++ and .Net code snippets to create a new block with hyperlink to AutoCAD Devblog :</p>
<p></p>
<p>ObjectARX C++ API :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> AcApDocument *pActiveDoc = acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin:0em;"> AcDbDatabase *pDB = pActiveDoc-&gt;database();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbBlockTable *pBlockTable = NULL;</pre>
<pre style="margin:0em;"> Acad::ErrorStatus es = pDB-&gt;getBlockTable(</pre>
<pre style="margin:0em;"> 						pBlockTable, kForWrite);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (! pBlockTable-&gt;has(ACRX_T(<span style="color:#a31515">&quot;Test&quot;</span><span style="color:#000000"> )))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Create the BlockTableRecord with hyperlink</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecord *pBTR = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbBlockTableRecord();</pre>
<pre style="margin:0em;"> 	pBTR-&gt;setName(ACRX_T(<span style="color:#a31515">&quot;Test&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	AcDbObjectId btrId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 	pBlockTable-&gt;add(btrId, pBTR);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbCircle *pCircle = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbCircle(</pre>
<pre style="margin:0em;"> 		AcGePoint3d::kOrigin, </pre>
<pre style="margin:0em;"> 		AcGeVector3d::kZAxis, 10.0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	pBTR-&gt;appendAcDbEntity(pCircle);</pre>
<pre style="margin:0em;"> 	pCircle-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbHyperlinkCollection * pcHCL = NULL;</pre>
<pre style="margin:0em;"> 	ACRX_X_CALL(pBTR, AcDbEntityHyperlinkPE)</pre>
<pre style="margin:0em;"> 		-&gt;getHyperlinkCollection(pBTR, pcHCL, <span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	pcHCL-&gt;addTail(</pre>
<pre style="margin:0em;"> 		ACRX_T(<span style="color:#a31515">&quot;http://adndevblog.typepad.com/autocad/&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		ACRX_T(<span style="color:#a31515">&quot;AutoCAD DevBlog&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	ACRX_X_CALL(pBTR, AcDbEntityHyperlinkPE)</pre>
<pre style="margin:0em;"> 	-&gt;setHyperlinkCollection(pBTR, pcHCL);</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">delete</span><span style="color:#000000">  pcHCL;</pre>
<pre style="margin:0em;"> 	pBTR-&gt;close();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> pBlockTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>AutoCAD .Net API (should work in AutoCAD 2015+) :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Database db = </pre>
<pre style="margin:0em;"> 	Application.DocumentManager.MdiActiveDocument.Database;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr = </pre>
<pre style="margin:0em;"> 	db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     BlockTable bt = tr.GetObject(</pre>
<pre style="margin:0em;">                                     db.BlockTableId,</pre>
<pre style="margin:0em;">                                     OpenMode.ForRead</pre>
<pre style="margin:0em;">                                 ) as BlockTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (bt.Has(<span style="color:#a31515">&quot;MyBlock&quot;</span><span style="color:#000000"> ) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         bt.UpgradeOpen();</pre>
<pre style="margin:0em;">         BlockTableRecord btr = <span style="color:#0000ff">new</span><span style="color:#000000">  BlockTableRecord();</pre>
<pre style="margin:0em;">         btr.Name = <span style="color:#a31515">&quot;MyBlock&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         btr.Origin = Point3d.Origin;</pre>
<pre style="margin:0em;">         bt.Add(btr);</pre>
<pre style="margin:0em;">         tr.AddNewlyCreatedDBObject(btr, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Circle c = <span style="color:#0000ff">new</span><span style="color:#000000">  Circle(</pre>
<pre style="margin:0em;"> 			Point3d.Origin, </pre>
<pre style="margin:0em;"> 			Vector3d.ZAxis, 10.0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         btr.AppendEntity(c);</pre>
<pre style="margin:0em;">         tr.AddNewlyCreatedDBObject(c, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         <span style="color:#008000">//Get the hyperlink collection from the entity</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         HyperLinkCollection linkCollection = btr.Hyperlinks;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">//Create a new hyperlink</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         HyperLink hyperLink = <span style="color:#0000ff">new</span><span style="color:#000000">  HyperLink();</pre>
<pre style="margin:0em;">         hyperLink.Description = <span style="color:#a31515">&quot;AutoCAD DevBlog&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         hyperLink.Name </pre>
<pre style="margin:0em;"> 			= <span style="color:#a31515">&quot;http://adndevblog.typepad.com/autocad/&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         hyperLink.SubLocation = <span style="color:#a31515">&quot;&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         linkCollection.Add(hyperLink);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     tr.Commit();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
