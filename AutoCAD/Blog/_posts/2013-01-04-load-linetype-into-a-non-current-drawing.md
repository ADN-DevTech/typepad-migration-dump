---
layout: "post"
title: "Load linetype into a non-current drawing"
date: "2013-01-04 02:37:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/load-linetype-into-a-non-current-drawing.html "
typepad_basename: "load-linetype-into-a-non-current-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The code below loads linetypes starting with character &#39;H&#39; from the acad.lin file into all the open documents. Note that the command should be registered in the application context (for example, with the ACRX_CMD_SESSION flag).</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> asdkgttrial()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcApDocumentIterator* pIter = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acDocManager-&gt;newAcApDocumentIterator(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(!pIter) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcApDocument *pDocCur=curDoc(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(;!pIter-&gt;done();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pIter-&gt;step()) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcApDocument *pDoc1; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pDoc1 = pIter-&gt;document();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acDocManager-&gt;setCurDocument(pDoc1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcAp::kWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcDbDatabase* pDwg=pDoc1-&gt;database(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pDwg-&gt;loadLineTypeFile(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;H*&quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;acad.lin&quot;</span><span style="line-height: 140%;">)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acDocManager-&gt;unlockDocument(pDoc1); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">delete</span><span style="line-height: 140%;"> pIter; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acDocManager-&gt;activateDocument(pDocCur); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
