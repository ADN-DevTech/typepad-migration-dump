---
layout: "post"
title: "Updating dimensions associated with a dynamic block"
date: "2012-09-05 20:42:00"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/updating-dimensions-associated-with-a-dynamic-block.html "
typepad_basename: "updating-dimensions-associated-with-a-dynamic-block"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>I have a dimension in a dynamic block. When I modify the parameter programmatically, the block gets updated but the dimension does not update itself. What can I do to resove this ?</p>
<div><strong>Solution</strong></div>
<p>When the parameter is modified, AutoCAD creates a new anonymous block. To ensure that the dimension also get updated to reflect the change in the parameter, this anonymous block must be opened for write and marked as "modified". This will ensure that AutoCAD updates the dimension.</p>
<p>Here is the sample code. The complete sample project can be downloaded from the link below.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId selectedObjectId = _blockRefOid;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;AcDbObjectId&gt; oidArray;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockReference *pBlockRef = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbOpenObject(pBlockRef, selectedObjectId, AcDb::kForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTableRecord *pBlockTableRecord1 = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbOpenObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pBlockTableRecord1, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pBlockRef-&gt;blockTableRecord(), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ACHAR *name = NULL; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pBlockTableRecord1-&gt;getName(name);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTableRecordIterator *pIterator = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pBlockTableRecord1-&gt;newIterator(pIterator);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;">(pIterator-&gt;done() == </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; AcDbEntity *pEnt1 = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; pIterator-&gt;getEntity(pEnt1, AcDb::kForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pEnt1-&gt;isKindOf(AcDbBlockReference::desc()))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; oidArray.append(pEnt1-&gt;objectId());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; pEnt1-&gt;assertWriteEnabled();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; pEnt1-&gt;recordGraphicsModified(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; pEnt1-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; pIterator-&gt;step();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pIterator;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pBlockTableRecord1-&gt;close(); </span></p>
</div>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01774477768e970d"><a href="http://adndevblog.typepad.com/files/testblock.dwg">Download Testblock</a></span>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3bc863f8970c"><a href="http://adndevblog.typepad.com/files/testapp.zip">Download Testapp</a></span>
