---
layout: "post"
title: "Including fonts and other files using eTransmit API "
date: "2014-10-20 00:00:59"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/10/including-fonts-and-other-files-using-etransmit-api-.html "
typepad_basename: "including-fonts-and-other-files-using-etransmit-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently one of my colleague from Product support enquired if it was possible to find fonts and shape files included in a drawing using the eTransmit API. To configure what gets included as dependents when a drawing is added to the TransmittalOperation, it is required to setup the TransmittalInfo.</p>
<p>Here is a sample code that should also fetch the fonts and shape files associated with a drawing using e-Transmit API :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">//AcETransmit19.Interop.dll</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  AcETransmit;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;DependentFiles&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  DependentFilesMethod()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     TransmittalFile tf;</pre>
<pre style="margin:0em;">     TransmittalOperation  to </pre>
<pre style="margin:0em;">                 = <span style="color:#0000ff">new</span><span style="color:#000000">  TransmittalOperation();</pre>
<pre style="margin:0em;">     TransmittalInfo ti </pre>
<pre style="margin:0em;">                 = to.getTransmittalInfoInterface();</pre>
<pre style="margin:0em;">     ti.includeDataLinkFile = 1;</pre>
<pre style="margin:0em;">     ti.includeDGNUnderlay = 1;</pre>
<pre style="margin:0em;">     ti.includeDWFUnderlay = 1;</pre>
<pre style="margin:0em;">     ti.includeFontFile  = 1;</pre>
<pre style="margin:0em;">     ti.includeImageFile  = 1;</pre>
<pre style="margin:0em;">     ti.includeInventorProjectFile  = 1;</pre>
<pre style="margin:0em;">     ti.includeInventorReferences  = 1;</pre>
<pre style="margin:0em;">     ti.includeMaterialTextureFile  = 1;</pre>
<pre style="margin:0em;">     ti.includeNestedOverlayXrefDwg = 1;</pre>
<pre style="margin:0em;">     ti.includePDFUnderlay  = 1;</pre>
<pre style="margin:0em;">     ti.includePhotometricWebFile  = 1;</pre>
<pre style="margin:0em;">     ti.includePlotFile = 1;</pre>
<pre style="margin:0em;">     ti.includeUnloadedXrefDwg  = 1;</pre>
<pre style="margin:0em;">     ti.includeXrefDwg = 1;</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">string</span><span style="color:#000000">  dwgFile = <span style="color:#a31515">@&quot;D:\\Temp\\Sample.dwg&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (to.addDrawingFile(dwgFile, <span style="color:#0000ff">out</span><span style="color:#000000">  tf) </pre>
<pre style="margin:0em;">                 == AddFileReturnVal.eFileAdded)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         TransmittalFilesGraph tfg </pre>
<pre style="margin:0em;">                 = to.graphInterfacePtr();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         TransmittalFile rootTF = tfg.getRoot();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         DisplayDependent(rootTF);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  DisplayDependent(TransmittalFile tf)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">int</span><span style="color:#000000">  numberOfDependents = tf.numberOfDependents;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  i = 0; i &lt; numberOfDependents; ++i)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         TransmittalFile childTF = tf.getDependent(i);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         FileType ft = childTF.FileType;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">string</span><span style="color:#000000">  sourcePath = childTF.sourcePath;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Application.DocumentManager.MdiActiveDocument.Editor</pre>
<pre style="margin:0em;">                             .WriteMessage(String.Format(</pre>
<pre style="margin:0em;">                             <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Dependent <span style="color:#000000">{</span>1<span style="color:#000000">}</span> - <span style="color:#000000">{</span>2<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                             Environment.NewLine, </pre>
<pre style="margin:0em;">                                 ft.ToString(), sourcePath));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         DisplayDependent(childTF);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
