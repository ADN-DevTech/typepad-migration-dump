---
layout: "post"
title: "Create a copy of the current document into a new document"
date: "2012-12-25 05:28:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/create-a-copy-of-the-current-document-into-a-new-document.html "
typepad_basename: "create-a-copy-of-the-current-document-into-a-new-document"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>How can I have an identical copy of the current document and create it as a new document?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>One of the solution is to create a temporary drawing template file from the current database first, then create a new drawing with the template. Then delete it if necessary. Here is an outline of required steps followed by a code fragment.</p>
<p>1. Wblock the current open drawing.   <br />2. Save the wblocked drawing as a template file (with a DWT extension) to a temporary location on your hard drive.    <br />3. From the application context create a NEW document using the previously saved template file.    <br />4. If necessary, &#39;remove&#39; the temporary template file from your hard drive.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//help fuction: create a new document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> newDocHelper(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *pData)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcApDocument* pDoc = acDocManager-&gt;curDocument();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (acDocManager-&gt;isApplicationContext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acDocManager-&gt;appContextNewDocument((</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> ACHAR *)pData);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;\nERROR: in Document context : %s\n&quot;</span><span style="line-height: 140%;">,pDoc-&gt;fileName());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Please note, here we are using &quot;C:/temp.dwt&quot; as a location </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// for our temporary template file. Please change this location </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// to suit your requirements as appropriate.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> copydwg()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TODO: Implement the command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbDatabase *pDb = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; AcDbDatabase *pnewDb = NULL;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;<span style="line-height: 140%;">&#0160;&#0160; pDb = acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; assert( pDb != NULL );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">( pDb-&gt;wblock(pnewDb) != Acad::eOk ) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;Couldn&#39;t wblock.\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">( pnewDb-&gt;saveAs(L</span><span style="line-height: 140%; color: #a31515;">&quot;C:/temp.dwt&quot;</span><span style="line-height: 140%;">) != Acad::eOk) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">&quot;Couldn&#39;t saveAs C:/temp.dwt file.\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">delete</span><span style="line-height: 140%;"> pnewDb;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">delete</span><span style="line-height: 140%;"> pnewDb; </span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">wchar_t</span><span style="line-height: 140%;"> pData[] = L</span><span style="line-height: 140%; color: #a31515;">&quot;c:/temp.dwt&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; acDocManager-&gt;executeInApplicationContext(newDocHelper, (</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *)pData);</span>&#0160; </p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// &#39;remove&#39; is a C function to delete a file and its syntax is</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//int remove(const wchar_t *path );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; remove(</span><span style="line-height: 140%; color: #a31515;">&quot;c:/temp.dwt&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>NOTE: Do not register this command with flag &#39;ACRX_CMD_SESSION&#39; because   <br />AcDbDatabase::wblock() will fail in this context.</p>
