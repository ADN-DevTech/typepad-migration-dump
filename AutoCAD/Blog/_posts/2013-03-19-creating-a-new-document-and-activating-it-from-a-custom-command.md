---
layout: "post"
title: "Creating a new document and activating it from a custom command"
date: "2013-03-19 00:36:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/creating-a-new-document-and-activating-it-from-a-custom-command.html "
typepad_basename: "creating-a-new-document-and-activating-it-from-a-custom-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue      <br /></strong>How can I create a new document and activate it from a custom command?</p>
<p><a name="section2"></a></p>
<p><strong>Solution      <br /></strong>There are two ways to do this:</p>
<p>Method #1    <br />Register your command with ACRX_CMD_SESSION flag and use the appContextNewDocument API to create the new document. The drawback of this approach is that your command will be executed in the application context, therefore, you will need to do explicit document locking if you want to interact with any documents. Also, you cannot use acedCommand/acedCmd/acedInvoke APIs from the application context. </p>
<p>Method #2    <br />Register your command with ACRX_CMD_MODAL flag and use the executeInApplicationContext API within your command handler to switch the application context.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">ACED_ARXCOMMAND_ENTRY_AUTO(CArxProject3App, ArxProject3, _MyCommand2, MyCommand2, <span style="color: #ff0000;">ACRX_CMD_MODAL</span>, NULL)</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: green;">// command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> MyARXProject_MyCommand2(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; addDoc_By_Command_Modal(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> addDoc_By_Command_Modal()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// template of new drawing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> TCHAR pData[] = _T(</span><span style="line-height: 140%; color: green;">/*NOXLATE*/</span><span style="line-height: 140%; color: #a31515;">&quot;acad.dwt&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcApDocument* pDoc = acDocManager-&gt;curDocument();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pDoc) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nCurrently in Document context : %s,&#0160; \</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Switching to App.\n&quot;</span><span style="line-height: 140%;">),pDoc-&gt;fileName());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acDocManager-&gt;executeInApplicationContext(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newSyncDocHelper, (</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *)pData);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">newSyncDocHelper( </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *pData)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// void function for executeInApplicationContext </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcApDocument* pDoc = acDocManager-&gt;curDocument();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (acDocManager-&gt;isApplicationContext()) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nSucessfully Switched to App. Context\n&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acDocManager-&gt;appContextNewDocument((</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR *)pData);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nOpened a new document synchronously.\n&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nERROR: in Document context : %s\n&quot;</span><span style="line-height: 140%;">),pDoc-&gt;fileName());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
