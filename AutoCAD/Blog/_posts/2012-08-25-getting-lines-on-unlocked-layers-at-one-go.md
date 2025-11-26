---
layout: "post"
title: "Getting Lines on unlocked layers at one go"
date: "2012-08-25 11:00:47"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/getting-lines-on-unlocked-layers-at-one-go.html "
typepad_basename: "getting-lines-on-unlocked-layers-at-one-go"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently a developer asked this question and here is the code snippet. The code snippet can be modified to fetch entities based on any such criteria. The "acedssget" method can take filter in the form a result buffer. It can be formed using the "acutBuildList" method, but I havent used it in this code snippet since the layers that are unlocked are not known until we iterate the Layer table. Instead, the result buffer is created node by node based on the number of unlocked layer that we find.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">resbuf* pLineResbuf;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">resbuf* pIter = 0;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;"> szLineBuff[] = L</span><span style="color: #a31515; line-height: 140%;">&quot;LINE&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pLineResbuf = acutNewRb(0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter = pLineResbuf;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter-&gt;resval.rstring = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;">[wcslen(szLineBuff) + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">wcscpy(pIter-&gt;resval.rstring, szLineBuff);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;"> szConditionBuff1[] = L</span><span style="color: #a31515; line-height: 140%;">&quot;&lt;OR&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter-&gt;rbnext = acutNewRb(-4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter = pIter-&gt;rbnext;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter-&gt;resval.rstring = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;">[wcslen(szConditionBuff1) + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">wcscpy(pIter-&gt;resval.rstring, szConditionBuff1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId layerId = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbLayerTable* pLayerTable;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcApDocument *pActiveDoc = acDocManager-&gt;mdiActiveDocument();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbDatabase *pActiveDB = pActiveDoc-&gt;database();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pActiveDB-&gt;getSymbolTable(pLayerTable, AcDb::kForRead);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbLayerTableIterator *pLayerTableIter = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pLayerTable-&gt;newIterator(pLayerTableIter);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pLayerTableIter-&gt;setSkipHidden(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (; !pLayerTableIter-&gt;done(); pLayerTableIter-&gt;step()) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbLayerTableRecord *pLTR = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayerTableIter-&gt;getRecord(pLTR, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; TCHAR *lname;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLTR-&gt;getName(lname);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pLTR-&gt;isLocked() == </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pIter-&gt;rbnext = acutNewRb(8);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pIter = pIter-&gt;rbnext;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pIter-&gt;resval.rstring = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;">[wcslen(lname) + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; wcscpy(pIter-&gt;resval.rstring, lname);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLTR-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pLayerTableIter;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pLayerTable-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;"> szConditionBuff2[] = L</span><span style="color: #a31515; line-height: 140%;">&quot;OR&gt;&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter-&gt;rbnext = acutNewRb(-4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter = pIter-&gt;rbnext;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pIter-&gt;resval.rstring = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">wchar_t</span><span style="line-height: 140%;">[wcslen(szConditionBuff2) + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">wcscpy(pIter-&gt;resval.rstring, szConditionBuff2);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name selectset; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedSSGet(L</span><span style="color: #a31515; line-height: 140%;">&quot;_X&quot;</span><span style="line-height: 140%;">, NULL, NULL, pLineResbuf, selectset); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">long</span><span style="line-height: 140%;"> length=0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedSSLength (selectset, &amp;length);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nNumber of Entities: %d&quot;</span><span style="line-height: 140%;">, length);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> idx=0; idx&lt;length; ++idx)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ads_name entres; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acedSSName(selectset, idx, entres) != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; acedSSFree(selectset);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId id;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbGetObjectId(id, entres);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectPointer&lt;AcDbEntity&gt; pEntity(id, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nEntity: %s&quot;</span><span style="line-height: 140%;">, pEntity-&gt;isA()-&gt;name());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedSSFree(selectset);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutRelRb(pLineResbuf);</span></p>
</div>
