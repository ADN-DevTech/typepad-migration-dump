---
layout: "post"
title: "Using \"SwapPartFamilyAndSize\" in late-binding .NET application"
date: "2012-10-11 02:18:56"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/10/using-swappartfamilyandsize-in-late-binding-net-application.html "
typepad_basename: "using-swappartfamilyandsize-in-late-binding-net-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AeccPipeNetworkPart::<strong>SwapPartFamilyAndSize()</strong>&#0160; is a Civil 3D COM API which swaps the part
family. It doesn&#39;t have a .NET version yet (2013 release of Civil 3D). If you
are trying to avoid any COM references and opting a late binding approach and
not sure about how to prepare the input argument for the SwapPartFamilyAndSize,
here is a relevant code snippet :&#0160;</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3ca2b98f970c-pi" style="display: inline;"><img alt="Pipe_Part_1" class="asset  asset-image at-xid-6a0167607c2431970b017d3ca2b98f970c" src="/assets/image_b1b8f1.jpg" title="Pipe_Part_1" /></a></p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> ts = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Part</span><span style="line-height: 140%;"> part = (</span><span style="color: #2b91af; line-height: 140%;">Part</span><span style="line-height: 140%;">)ts.GetObject(res.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> acadpart = part.AcadObject;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> partfam = acadpart.GetType().InvokeMember(</span><span style="color: #a31515; line-height: 140%;">&quot;PartFamily&quot;</span><span style="line-height: 140%;">, System.Reflection.</span><span style="color: #2b91af; line-height: 140%;">BindingFlags</span><span style="line-height: 140%;">.GetProperty, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, acadpart, </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[0]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> prtfamId = Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;">.FromAcadObject(partfam);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PartFamily</span><span style="line-height: 140%;"> partfamily = (</span><span style="color: #2b91af; line-height: 140%;">PartFamily</span><span style="line-height: 140%;">)ts.GetObject(prtfamId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[] dataArry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[2];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; dataArry[0] = partfamily.GUID;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> partSizeId = partfamily[0];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PartSize</span><span style="line-height: 140%;"> partSize = ts.GetObject(partSizeId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PartSize</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; dataArry[1] = partSize.AcadObject;</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; acadpart.GetType().InvokeMember(</span><span style="color: #a31515; line-height: 140%;">&quot;SwapPartFamilyAndSize&quot;</span><span style="line-height: 140%;">, System.Reflection.</span><span style="color: #2b91af; line-height: 140%;">BindingFlags</span><span style="line-height: 140%;">.InvokeMethod, </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, acadpart, dataArry);</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ts.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
Hope this is useful to you!
