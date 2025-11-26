---
layout: "post"
title: "Preserving Draworder of Entities while &ldquo;wblockcloning&rdquo; the blocks"
date: "2014-11-16 19:30:00"
author: "Madhukar Moogala"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2014/11/preserving-draworder-of-entities-while-wblockcloning-the-blocks.html "
typepad_basename: "preserving-draworder-of-entities-while-wblockcloning-the-blocks"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>The WblockClone API doesn’t guarantee draw order when Blocks are cloned ,wblockclone is very low level function ,it does only copying of entities ,the high-level functionality likes preserving draw order should be explicitly implement by application developer.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c705ae56970b-pi"><img alt="image" border="0" height="133" src="/assets/image_715497.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="image" width="260" /></a></p>
<p>&#0160;</p>
<p>The above screenshots display correct and incorrect draw orders when wblock clone is perform ,to preserve order same a source block ,we need to use drawordertable API to sort entities as per source block.</p>
<p>C++ Code :</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> ADSKMyGroupWTEST()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span> es;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">TCHAR</span> fullpath[<span style="color: #6f008a;">_MAX_PATH</span>];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">int</span> ret = acedFindFile(<span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;C:\\VESSELBLOCKS.dwg&quot;</span>),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fullpath );</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ( ret != <span style="color: #6f008a;">RTNORM</span> )</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbDatabase</span> *pSrcDb = <span style="color: blue;">new</span> <span style="color: #2b91af;">AcDbDatabase</span>( <span style="color: blue;">false</span>, <span style="color: blue;">false</span> );</p>
<p style="margin: 0px;">es = pSrcDb-&gt;readDwgFile( fullpath, <span style="color: #6f008a;">_SH_DENYNO</span> );</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ( es != <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span> )</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">acutPrintf( <span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;\nCan not open file.&quot;</span>) );</p>
<p style="margin: 0px;"><span style="color: blue;">delete</span> pSrcDb;</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcApDocument</span> *pActiveDoc</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = <span style="color: #6f008a;">acDocManager</span>-&gt;mdiActiveDocument();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbDatabase</span> *pDestDb</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = pActiveDoc-&gt;database();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectIdArray</span> objIds2Copy;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbBlockTable</span> *pBlockTable,*pBlockTable2;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">es = pSrcDb-&gt;getSymbolTable(pBlockTable, <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectId</span> recordId = <span style="color: #2b91af;">AcDbObjectId</span>::kNull;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">es = pBlockTable-&gt;getAt(<span style="color: #6f008a;">ACRX_T</span>(<span style="color: #a31515;">&quot;CP_STERN_SY&quot;</span>), recordId, <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;">objIds2Copy.append(recordId);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">es = pBlockTable-&gt;close();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbIdMapping</span> idMap;</p>
<p style="margin: 0px;">es = pSrcDb-&gt;wblockCloneObjects(objIds2Copy,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acdbSymUtil()-&gt;blockModelSpaceId(pDestDb),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; idMap, <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kDrcReplace</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>(es == <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">acutPrintf( <span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;\nCloned the block to the current drawing.&quot;</span>) );</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectId</span> targetBlockId = <span style="color: #2b91af;">AcDbObjectId</span>::kNull;</p>
<p style="margin: 0px;">es = pDestDb-&gt;getSymbolTable(pBlockTable2, <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;">es = pBlockTable2-&gt;getAt(<span style="color: #6f008a;">ACRX_T</span>(<span style="color: #a31515;">&quot;CP_STERN_SY&quot;</span>),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; targetBlockId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;">SetBlockDrawOrder(recordId,targetBlockId,idMap);</p>
<p style="margin: 0px;">es = pBlockTable2-&gt;close();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">else</span></p>
<p style="margin: 0px;">acutPrintf( <span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;\nFailed to clone the block to the current drawing.&quot;</span>) );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">delete</span> pSrcDb;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> SetBlockDrawOrder(<span style="color: #2b91af;">AcDbObjectId</span> <span style="color: gray;">srcBlockId</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbObjectId</span> <span style="color: gray;">targetBlockId</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbIdMapping</span>&amp; <span style="color: gray;">idMap</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbBlockTableRecord</span> *pSrcBlock = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">acdbOpenObject(pSrcBlock,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">srcBlockId</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbSortentsTable</span> *pSortTab1 = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">pSrcBlock-&gt;getSortentsTable(pSortTab1,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectIdArray</span> oids;</p>
<p style="margin: 0px;">pSortTab1-&gt;getFullDrawOrder(oids);</p>
<p style="margin: 0px;">pSortTab1-&gt;close();</p>
<p style="margin: 0px;">pSrcBlock-&gt;close();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbBlockTableRecord</span> *pTargetBlock = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">acdbOpenObject(pTargetBlock,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">targetBlockId</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbSortentsTable</span> *pSortTab2 = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">pTargetBlock-&gt;getSortentsTable(pSortTab2,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForWrite</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectIdArray</span> targetIds;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">int</span> len = oids.length();</p>
<p style="margin: 0px;"><span style="color: blue;">for</span>(<span style="color: blue;">int</span> cnt = 0; cnt &lt; len; cnt++)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbIdPair</span> idPair(oids.at(cnt),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbObjectId</span>::kNull,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (<span style="color: gray;">idMap</span>.compute (idPair))</p>
<p style="margin: 0px;">targetIds.append(idPair.value());</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">pSortTab2-&gt;setRelativeDrawOrder(targetIds);</p>
<p style="margin: 0px;">pSortTab2-&gt;close();</p>
<p style="margin: 0px;">pTargetBlock-&gt;close();</p>
<p style="margin: 0px;">}</p>
</div>
<p>.NET Code :</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;WTEST&quot;</span>)]</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> MyWTEST()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">string</span> blockName = <span style="color: #a31515;">&quot;CP_STERN_SY&quot;</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">string</span> pathDWG = <span style="color: #a31515;">&quot;C:\\VESSELBLOCKS.dwg&quot;</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Database</span> OpenDb = <span style="color: blue;">new</span> <span style="color: #2b91af;">Database</span>(<span style="color: blue;">false</span>, <span style="color: blue;">false</span>))</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">OpenDb.ReadDwgFile(pathDWG,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.<span style="color: #2b91af;">FileShare</span>.ReadWrite,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">true</span>, <span style="color: #a31515;">&quot;&quot;</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectIdCollection</span> ids = <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">BlockTable</span> bt;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectId</span> sourceBlockId = <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; OpenDb.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">bt = (<span style="color: #2b91af;">BlockTable</span>)tr.GetObject(OpenDb.BlockTableId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (bt.Has(blockName))</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">ids.Add(bt[blockName]);</p>
<p style="margin: 0px;">sourceBlockId = bt[blockName];</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (ids.Count != 0)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> destdb = doc.Database;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">IdMapping</span> iMap = <span style="color: blue;">new</span> <span style="color: #2b91af;">IdMapping</span>();</p>
<p style="margin: 0px;">OpenDb.WblockCloneObjects(ids,</p>
<p style="margin: 0px;">destdb.BlockTableId,</p>
<p style="margin: 0px;">iMap,</p>
<p style="margin: 0px;"><span style="color: #2b91af;">DuplicateRecordCloning</span>.Replace,</p>
<p style="margin: 0px;"><span style="color: blue;">false</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> t =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; destdb.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectId</span> targetBlockId = <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">BlockTable</span> b = (<span style="color: #2b91af;">BlockTable</span>)t.GetObject(destdb.BlockTableId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (b.Has(blockName))</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">targetBlockId = b[blockName];</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">SetBlockDrawOrder(sourceBlockId, targetBlockId, iMap);</p>
<p style="margin: 0px;">t.Commit();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">tr.Commit();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> SetBlockDrawOrder(<span style="color: #2b91af;">ObjectId</span> sourceBlockId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectId</span> targetBlockId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">IdMapping</span> iMap)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> db = <span style="color: #2b91af;">HostApplicationServices</span>.WorkingDatabase;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> t = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> sourceBTR =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)t.GetObject(sourceBlockId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> dotSource =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DrawOrderTable</span>)t.GetObject(sourceBTR.DrawOrderTableId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectIdCollection</span> srcDotIds = <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="margin: 0px;">srcDotIds = dotSource.GetFullDrawOrder(0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> targetBTR =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)t.GetObject(targetBlockId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> dotTarget =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DrawOrderTable</span>)t.GetObject(targetBTR.DrawOrderTableId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForWrite, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectIdCollection</span> trgDotIds = <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="margin: 0px;"><span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> oId <span style="color: blue;">in</span> srcDotIds)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (iMap.Contains(oId))</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">IdPair</span> idPair = iMap.Lookup(oId);</p>
<p style="margin: 0px;">trgDotIds.Add(idPair.Value);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">dotTarget.SetRelativeDrawOrder(trgDotIds);</p>
<p style="margin: 0px;">t.Commit();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
</div>
