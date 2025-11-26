---
layout: "post"
title: "Determine whether an object is inserted as an XRef"
date: "2013-01-17 23:51:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/determine-whether-an-object-is-inserted-as-an-xref.html "
typepad_basename: "determine-whether-an-object-is-inserted-as-an-xref"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I run a query in a drawing to find entities that are part of an external reference (Xref), and am using AcDbObject::ownerId() to find the ID of the object&#39;s owner. After opening the owner (an AcDbBlockTableRecord) and calling its isFromExternalReference() function, it always returns False - even for an object that was inserted as part of an external reference. Why?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>The problem is that you are invoking isFromExternalReference() on the wrong object.&#0160; <br />Let us suppose you create AcDbLine (named &quot;Line&quot; in this example) in the model space of a drawing and save it as Test.dwg. Then you create another drawing named New.dwg and insert Test.dwg as an XRef into the model space of the new drawing.</p>
<p>The block table in the database for New.dwg now holds an AcDbBlockTableRecord named &quot;TEST&quot;. &quot;TEST&quot; holds an entry for &quot;Line&quot;. The model space in New.dwg has an AcDbBlockReference to &quot;TEST&quot;.</p>
<p>If you have a pointer to the AcDbBlockTableRecord for &quot;TEST&quot; (pBTR, say), a call to pBTR-&gt;isFromExternalReference() will correctly return True. A call to pBTR-&gt;ownerId() returns the ObjectID of the block table of New.dwg (for example, &quot;TEST&quot; is an XRef and is owned by the block table of New.dwg).</p>
<p>If you have a pointer to &quot;Line&quot; (pObj), you could find the ObjectId of its owner, using pObj-&gt;ownerId(). The AcDbObjectId returned by this call is not the ObjectId of &quot;TEST&quot;; it is the ObjectId of the model space of the database for Test.dwg that was opened when the XRef was inserted. The model space of Test.dwg is not an XRef of Test.dwg, and so it returns False when isFromExternalReference() is called.</p>
<p>Therefore, this is a situation where &quot;TEST&quot; effectively &#39;owns&#39; &quot;Line&quot;, but &quot;Line&quot; does not consider &quot;TEST&quot; to be its &#39;owner&#39;.</p>
<p>The simplest way to determine whether any given object has been inserted as an XRef is to compare its database (using AcDbObject::database()) to that of the current drawing if it is different, then the object is an XRef.</p>
<p>This example demonstrates this.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ASDKTEST_ARX_TestXref(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Get the Block Table Record for the XRef &quot;Test&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbDatabase * pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbBlockTable * pBT;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pDb-&gt;getSymbolTable(pBT, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbBlockTableRecord * pTest;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pBT-&gt;getAt(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;Test&quot;</span><span style="line-height: 140%;">), pTest,AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pBT-&gt;close();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// See if &quot;Test&quot; is an XRef</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pTest-&gt;isFromExternalReference())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nTest is an XRef.&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nTest is not an XRef.&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Get pointer to &#39;Line&#39; stored in &#39;Test&#39; block table record</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Because &#39;Line&#39; belongs to &quot;Test&quot;, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// it seems obvious that &#39;Line&#39;&#39;s owner is &quot;Test&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbBlockTableRecordIterator * pIter;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pTest-&gt;newIterator(pIter);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbLine * pLine = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">( ; ((!pIter-&gt;done()) &amp;&amp; (pLine == NULL)); pIter-&gt;step())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbEntity * pEnt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pIter-&gt;getEntity(pEnt, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pLine = AcDbLine::cast(pEnt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pLine != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pEnt-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pTest-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">delete</span><span style="line-height: 140%;"> pIter;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Query line&#39;s owner to see if it is an XRef</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">//&#0160; Now we find that &#39;Line&#39; is not owned by &quot;Test&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pLine != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbObjectId objId = pLine-&gt;ownerId();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbObject * pObj;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acdbOpenObject(pObj, objId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbBlockTableRecord * pBTR;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pBTR = AcDbBlockTableRecord::cast(pObj);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pBTR !=NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If you compare the addresses of pBTR and pTest </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// you will see that&#0160; they are not the same object.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> ( pBTR-&gt;isFromExternalReference() )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nLine&#39;s owner is an XRef.&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nLine&#39;s owner is not an XRef.&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pLine-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pObj-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> }</span></p>
</div>
