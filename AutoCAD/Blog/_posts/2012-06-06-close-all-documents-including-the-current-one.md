---
layout: "post"
title: "Close all documents including the current one"
date: "2012-06-06 20:04:59"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/close-all-documents-including-the-current-one.html "
typepad_basename: "close-all-documents-including-the-current-one"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The sample code closes all documents, including the current one by first closing all documents except the current one in the document context. The current&nbsp;document is then closed from the application context using the executeInApplicationContext API. This allows the current document to be closed from the same command which is running in the&nbsp;document context.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Close the other documents</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcApDocumentIterator *Iter </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acDocManager-&gt;newAcApDocumentIterator();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;">(!Iter-&gt;done())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (Iter-&gt;document()!=acDocManager-&gt;curDocument())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acDocManager-&gt;closeDocument(Iter-&gt;document());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Iter-&gt;step();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> Iter;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Now close the current document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">::acDocManagerPtr()-&gt;appContextCloseDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (acDocManager-&gt;curDocument());</span></p>
</div>
