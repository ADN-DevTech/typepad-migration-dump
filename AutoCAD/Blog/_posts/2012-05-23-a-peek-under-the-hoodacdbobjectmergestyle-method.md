---
layout: "post"
title: "A peek under the hood–AcDbObject::mergeStyle method"
date: "2012-05-23 16:57:16"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/a-peek-under-the-hoodacdbobjectmergestyle-method.html "
typepad_basename: "a-peek-under-the-hoodacdbobjectmergestyle-method"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>The AcDbObject::mergeStyle method determines the way objects contained in dictionaries are handled during INSERT, XREF/BIND and XREF/INSERT operations. The typical return values for this method can be:</p>
<p>kDrcIgnore <br />kDrcReplace <br />kDrcMangleName</p>
<p>There are a couple of other return values specific to XRef mangling (kDrcXrefMangleName) and unmangling (kDrcUnmangleName).</p>
<p>The default implementation at AcDbObject level is to return kDrcIgnore, which means that these objects are ignored during merging of dictionaries. There is no way of overruling this behavior at AcDbObject level and there&#39;s no public data member or method in AcDbObject that will let you modify the merge style.</p>
<p>However, an application that derives from AcDbObject can freely override the virtual mergeStyle method to return any of the other valid merge styles.</p>
<p>If an object returns AcDb::kDrcReplace, the object will replace an object that is stored under the same key in the target database of an INSERT, XREF/BIND or XREF/INSERT operation. On the other hand, AcDb::kDrcMangleName means that the object will be merged into the existing entries of the target dictionary, creating entries with the $0$ string.</p>
<p>Also, the virtual function &#39;mergeStyle()&#39; is overridden for the following classes:</p>
<p>AcDbXRecord <br />AcDbDictionary <br />AcDbProxyObject</p>
<p>AcDbXrecord and AcDbDictionary store an associated data member that can be set via a call to their new method setMergeStyle() and AcDb::DuplicateRecordCloning).</p>
<p>AcDbProxyObject has the following proxy flags that control the merge style of a proxy object:</p>
<p>kMergeIgnore (the default) <br />kMergeReplace <br />kMergeMangleName</p>
<p>These flags can be defined in ACRX_DXF_DEFINE_MEMBERS macro of an entity’s class definition. The value returned by AcDbProxyObject::mergeStyle() depends on these proxy flags.</p>
