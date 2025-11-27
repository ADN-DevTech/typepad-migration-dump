---
layout: "post"
title: "How to use private storage and stream in C++"
date: "2012-08-15 23:35:02"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/how-to-use-private-storage-and-stream-in-c.html "
typepad_basename: "how-to-use-private-storage-and-stream-in-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><span style="background-color: #ffff00;"><span style="font-size: 8pt;"><em>Note: the solution in this article uses the&#0160;the wrapped functions of Inventor API with IStorage and IStream which require to make the document dirty. If you do not want to use Inventor API, please refer to <a href="http://adndevblog.typepad.com/manufacturing/2014/04/save-extra-data-in-inventor-file-without-inventor-api.html" target="_self">the other article</a>.</em></span>&#0160;</span></p>
<p>MSDN: &quot;<em>The IStorage interface supports the creation and management of structured storage objects. Structured storage allows hierarchical storage of information within a single file, and is often referred to as &quot;a file system within a file&quot;. Elements of a structured storage object are storages and streams. Storages are analogous to directories, and streams are analogous to files. Within a structured storage there will be a primary storage object that may contain substorages, possibly nested, and streams. Storages provide the structure of the object, and streams contain the data, which is manipulated through the IStream interface</em>.&quot;</p>
<p>The Inventor API allows the use of Private Storages and Streams inside Inventor documents, but it encapsulates the COM Interfaces IStorage and IStream in a way that make it easier to manipulate.</p>
<p>For further details about the COM interfaces, please refer to the online Microsoft documentation:</p>
<p>IStorage: <a href="http://msdn2.microsoft.com/en-us/library/aa380015.aspx">http://msdn2.microsoft.com/en-us/library/aa380015.aspx</a> <br />IStream: <a href="http://msdn2.microsoft.com/en-us/library/aa380034.aspx">http://msdn2.microsoft.com/en-us/library/aa380034.aspx</a></p>
<p><strong>Important Notes:</strong></p>
<p>- When Inventor opens a file, it reads in the embedded streams and edit it in memory, and writes it back out on save. If you edit this storage using the Microsoft structured storage APIs directly on the file root storage opened off disk, your changes would be overwritten if the file was also opened in Inventor and saved after your changes.&#0160; Also changes made like this would not be realized in the in memory open document until the document is closed and reopened (if it was not saved and overwrote the changes). For this reason you should not use the Microsoft API directly on an open Inventor document, use the exposed Inventor API below instead.</p>
<p>- Also, manipulating the file storage is not a transacted operation, i.e. you cannot use the undo/redo mechanism after a change. For this reason, any modification to the file storage will not set it as &#39;Dirty&#39;, so simply saving and closing the file through Inventor will result in the lost of these changes. In order to keep the storage modifications across sessions, you will need to manually set the &#39;Dirty&#39; flag to True prior to performing the save. This is illustrated in the sample below.&#0160;&#0160;</p>
<p>The following C++ sample contains three methods that illustrate how to create a storage and a stream inside it, write and read from it and also delete the stream. It is from a standard alone EXE. It assumes Inventor has been launched.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// manipulate Private Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> HRESULT PrivateStream()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; HRESULT Result=NOERROR;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Result = ::CoInitialize (NULL); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get active inventor application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CLSID clsid;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; ::CLSIDFromProgID(L</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;clsid);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CComPtr&lt;IUnknown&gt; pUnk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; ::GetActiveObject(clsid,NULL,&amp;pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CComPtr&lt;Application&gt; pApp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; Result = pUnk-&gt;QueryInterface(</span><span style="line-height: 140%; color: blue;">__uuidof</span><span style="line-height: 140%;">(Application),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">**)&amp;pApp); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Get active Document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CComPtr&lt;Document&gt; pDoc;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; Result = pApp-&gt;get_ActiveDocument(&amp;pDoc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> outData[256];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//COM Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Result = CreatePrivateStorageAndStream(pDoc, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;MyPrvStorage1&quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;MyStream1&quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Some private stored data&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(Result != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to create Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Result;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; Result = ReadPrivateStorageAndStream(pDoc, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;MyPrvStorage1&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;MyStream1&quot;</span><span style="line-height: 140%;">,&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; outData);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(Result != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to read Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Result;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> Result = DeletePrivateStream(pDoc, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;MyPrvStorage1&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;MyStream1&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(Result != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to delete Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Result;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: green;">//Set document to Dirty prior to perform the Save</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: green;">//otherwise the stream won&#39;t be save through sessions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> pDoc-&gt;Dirty = VARIANT_TRUE;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> pDoc-&gt;Save();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">wrapup:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> ::CoUninitialize(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//Create Private Storage And Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> HRESULT CreatePrivateStorageAndStream(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComPtr&lt;Document&gt; pDoc, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* StorageName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* StreamName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* data)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; HRESULT hr;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Try to get Private Storage, if does not exist try to create</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CComPtr&lt;IUnknown&gt; pUnk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; hr=pDoc-&gt;GetPrivateStorage(CComBSTR(StorageName),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VARIANT_TRUE,&amp;pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(hr != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to create or open Storage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; CComQIPtr&lt;IStorage&gt; pStg(pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">//Create stream within private storage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; CComPtr&lt;IStream&gt; pStream = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; hr=pStg-&gt;CreateStream(CComBSTR(StreamName),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; STGM_DIRECT|STGM_CREATE|</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; STGM_READWRITE|STGM_SHARE_EXCLUSIVE,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0, 0, &amp;pStream);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(hr != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to create Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ULONG lsize=0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; lsize = strlen(data);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; hr=pStream-&gt;Write( &amp;lsize,</span><span style="line-height: 140%; color: blue;">sizeof</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;">), NULL ) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; hr=pStream-&gt;Write( data, strlen(data), NULL ) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Save the data</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; hr=pStream-&gt;Commit(STGC_DEFAULT|STGC_OVERWRITE); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Don&#39;t forget to commit changes also in storage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; hr=pStg-&gt;Commit(STGC_DEFAULT|STGC_OVERWRITE);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Read Private Storage And Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> HRESULT ReadPrivateStorageAndStream(CComPtr&lt;Document&gt; pDoc, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* StorageName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* StreamName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* outData)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; HRESULT hr;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Try to get Private Storage, if does not exist fail</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CComPtr&lt;IUnknown&gt; pUnk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; hr = pDoc-&gt;GetPrivateStorage(CComBSTR(StorageName),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VARIANT_FALSE,&amp;pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(hr != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to open Storage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CComQIPtr&lt;IStorage&gt; pStg(pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Open stream within private storage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CComPtr&lt;IStream&gt; pStream = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; hr = pStg-&gt;OpenStream(CComBSTR(StreamName),0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; STGM_DIRECT|STGM_READ|</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; STGM_SHARE_EXCLUSIVE, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;pStream);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(hr != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to open Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; ULONG lsize = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; hr = pStream-&gt;Read( &amp;lsize,</span><span style="line-height: 140%; color: blue;">sizeof</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;">), NULL);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; hr = pStream-&gt;Read( outData,lsize, NULL ) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; outData[lsize] = </span><span style="line-height: 140%; color: #a31515;">&#39;\0&#39;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Delete Private Stream</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> HRESULT DeletePrivateStream(CComPtr&lt;Document&gt; pDoc,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* StorageName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* Stream2Delete)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; HRESULT hr ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CComPtr&lt;IUnknown&gt; pUnk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; hr = pDoc-&gt;GetPrivateStorage(CComBSTR(StorageName),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; VARIANT_FALSE,&amp;pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(hr != S_OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ERROR: unable to open Storage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; CComQIPtr&lt;IStorage&gt; pStg(pUnk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Perform deletion of the stream element</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; hr = pStg-&gt;DestroyElement(CComBSTR(Stream2Delete));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; pStg-&gt;Commit(STGC_DEFAULT);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> hr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
