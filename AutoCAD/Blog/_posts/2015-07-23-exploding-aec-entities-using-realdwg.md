---
layout: "post"
title: "Exploding AEC entities using RealDWG"
date: "2015-07-23 04:08:07"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "2016"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/exploding-aec-entities-using-realdwg.html "
typepad_basename: "exploding-aec-entities-using-realdwg"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Exploding an AEC entity such as a Wall in AutoCAD results in a block reference and the block table record that it refers to contains faces. But when exploding the Wall entity in a RealDWG application results in a block reference and the block table record that it refers to contains lines.&nbsp;</p>
<p>In this blog post we will look into the reason for this difference and a way to workaround it. The workaround was provided by my colleagues Mikako Harada and Tony Zou. Many thanks to them.</p>
<p>While RealDWG can read a drawing created by any of the AutoCAD verticals, the object enablers specific to the verticals will still be needed for the host application to recognize the entities in that drawing. The object enabler defines the resulting entity set when the entity is exploded. As object enabler provides the explode functionality for its custom entities, they may also have other considerations.</p>
<p>For example, when an AEC drawing has its viewing direction set as Top View, a wall appears as a rectangle. When the viewing direction is any other view, the wall appears as a collection of faces.&nbsp;</p>
<p>Also, AEC drawings do not fully load into a side database when using the readDwgFile. This is a known behavior in AutoCAD Architecture and here is a <a href="http://adndevblog.typepad.com/aec/2012/09/problems-reading-an-aec-side-database-with-a-mode-other-than-sh_denyno.html">blog post</a> by my colleague Adam Nagy. For the database to get fully loaded in a side database it is required to call AecAppDbx::drawingPromoterAndIniter method.</p>
<p></p>
<p>
Considering the above reasons, here are the C++ and .Net sample codes to explode a wall entity in a RealDWg application. The methods to have the database initialized and to set its viewing direction are invoked by accessing the exported methods from AecBase.dbx using their ordinal numbers. Please note that the ordinal numbers can change and are version specific. In the below code, the ordinal numbers for 2015 and 2016 releases are provided. For any other version, you may need to find them out using dumpbin on AecBase.lib from the <OMF SDK>\Lib-x64 folder.
</p>
<p></p>
<p>Here is the C# code snippet :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  System.Runtime.InteropServices;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Runtime;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">internal</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  <span style="color:#2b91af">Workaround</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">StructLayout</span><span style="color:#000000"> (<span style="color:#2b91af">LayoutKind</span><span style="color:#000000"> .Sequential)]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">struct</span><span style="color:#000000">  <span style="color:#2b91af">AcGeVector3d</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">double</span><span style="color:#000000">  x;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">double</span><span style="color:#000000">  y;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">double</span><span style="color:#000000">  z;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// for 2016</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//[DllImport(@&quot;C:\\Program Files\\Autodesk\\RealDWG 2015\\AecBase.dbx&quot;, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//        CallingConvention = CallingConvention.Cdecl, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//        EntryPoint = &quot;#1202&quot;)]</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">    </pre>
<pre style="margin:0em;">     <span style="color:#008000">// for 2015</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">DllImport</span><span style="color:#000000"> (<span style="color:#a31515">@&quot;C:\\Program Files\\Autodesk\\RealDWG 2015\\AecBase.dbx&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         CallingConvention = <span style="color:#2b91af">CallingConvention</span><span style="color:#000000"> .Cdecl, </pre>
<pre style="margin:0em;">         EntryPoint = <span style="color:#a31515">&quot;#1204&quot;</span><span style="color:#000000"> )]   </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  setLastViewDir(<span style="color:#0000ff">ref</span><span style="color:#000000">  <span style="color:#2b91af">AcGeVector3d</span><span style="color:#000000">  vDir);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SetLastViewDirection(Vector3d direction)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (! SystemObjects.ServiceDictionary.Contains(</pre>
<pre style="margin:0em;">             <span style="color:#a31515">&quot;AecBaseServices&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#2b91af">AcGeVector3d</span><span style="color:#000000">  vec;</pre>
<pre style="margin:0em;">         vec.x = direction.X;</pre>
<pre style="margin:0em;">         vec.y = direction.Y;</pre>
<pre style="margin:0em;">         vec.z = direction.Z;</pre>
<pre style="margin:0em;">         setLastViewDir(<span style="color:#0000ff">ref</span><span style="color:#000000">  vec);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// No change in ordinals</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// For 2015 and 2016</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> [<span style="color:#2b91af">DllImport</span><span style="color:#000000"> (<span style="color:#a31515">@&quot;C:\\Program Files\\Autodesk\\RealDWG 2015\\AecBase.dbx&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">     CallingConvention = <span style="color:#2b91af">CallingConvention</span><span style="color:#000000"> .Cdecl, </pre>
<pre style="margin:0em;">     CharSet = <span style="color:#2b91af">CharSet</span><span style="color:#000000"> .Unicode, EntryPoint = <span style="color:#a31515">&quot;#897&quot;</span><span style="color:#000000"> )] </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  drawingPromoterAndIniter</pre>
<pre style="margin:0em;"> (IntPtr db, <span style="color:#0000ff">bool</span><span style="color:#000000">  sideDb);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Database db = <span style="color:#0000ff">new</span><span style="color:#000000">  Database(<span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> db.ReadDwgFile(</pre>
<pre style="margin:0em;">     <span style="color:#a31515">@&quot;D:\\Temp\\wall.dwg&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">     System.IO.FileShare.None, </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">false</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">     <span style="color:#a31515">&quot;&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HostApplicationServices.WorkingDatabase = db;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> drawingPromoterAndIniter(db.UnmanagedObject, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> Workaround.SetLastViewDirection(Vector3d.XAxis);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//... Usual Explode of the entity</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p></p>
<p>Here is the C++ code snippet :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> typedef <span style="color:#0000ff">void</span><span style="color:#000000">  (*drawingPromoterAndIniter)</pre>
<pre style="margin:0em;">     (AcDbDatabase* pDb, <span style="color:#0000ff">bool</span><span style="color:#000000">  bUseCurrentViewInfo);</pre>
<pre style="margin:0em;"> typedef <span style="color:#0000ff">void</span><span style="color:#000000">  (*setLastViewDir)(AcGeVector3d direction);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> acdbSetHostApplicationServices(&amp;gDumpDwgHostApp);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> CString testFilePath = <span style="color:#a31515">&quot;D:\\\\Temp\\\\wall.dwg&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">long</span><span style="color:#000000">  lcid = 0x00000409;  <span style="color:#008000">// English</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> acdbValidateSetup(lcid);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">bool</span><span style="color:#000000">  isLoaded = acrxLoadModule(</pre>
<pre style="margin:0em;">     _T(<span style="color:#a31515">&quot;C:\\\\Program Files\\\\Autodesk\\\\RealDWG 2016\\\\AecBase.dbx&quot;</span><span style="color:#000000"> ), 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HMODULE hModule=GetModuleHandle(_T(<span style="color:#a31515">&quot;AecBase.dbx&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  ordinal = 897;</pre>
<pre style="margin:0em;"> auto drawingPromoterFunc = </pre>
<pre style="margin:0em;">     (drawingPromoterAndIniter)GetProcAddress(hModule, (LPCSTR) ordinal);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (drawingPromoterFunc == nullptr) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AfxMessageBox(_T(<span style="color:#a31515">&quot;Error ! function not in AecBase.dbx !!&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  1;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ordinal = 1202; <span style="color:#008000">// 2016</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//ordinal = 1204; //2015</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> auto setLastViewDirFunc = </pre>
<pre style="margin:0em;">     (setLastViewDir)GetProcAddress(hModule, (LPCSTR) ordinal);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (setLastViewDirFunc == nullptr) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AfxMessageBox(_T(<span style="color:#a31515">&quot;Error ! function not in AecBase.dbx !!&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  1;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> AcDbDatabase *pDb = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbDatabase(Adesk::kFalse);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (pDb == NULL)</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus es = Acad::eOk;</pre>
<pre style="margin:0em;"> es = pDb-&gt;readDwgFile(testFilePath);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> acdbHostApplicationServices()-&gt;setWorkingDatabase(pDb);</pre>
<pre style="margin:0em;"> acdbResolveCurrentXRefs(pDb);</pre>
<pre style="margin:0em;"> drawingPromoterFunc(pDb, <span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> setLastViewDirFunc(AcGeVector3d::kXAxis);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//... Usual Explode of the entity</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
