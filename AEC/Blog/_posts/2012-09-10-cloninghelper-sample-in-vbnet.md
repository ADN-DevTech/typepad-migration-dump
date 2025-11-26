---
layout: "post"
title: "CloningHelper sample in VB.NET"
date: "2012-09-10 13:25:17"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/09/cloninghelper-sample-in-vbnet.html "
typepad_basename: "cloninghelper-sample-in-vbnet"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>Previously I&#39;ve posted two sample using&#0160;CloningHelper utility class.&#0160;I showed the usage in the context of&#0160; <a href="http://adndevblog.typepad.com/aec/2012/08/importexport-styles-fromto-another-drawing-in-net-.html" target="_self" title="Import export styles">importing/exporting styles from/to anthoer drawing </a>and <a href="http://adndevblog.typepad.com/aec/2012/08/cloning-a-style-within-the-same-drawing.html" target="_self" title="cloning a style">cloning a style within the drawing</a>&#0160;in C#. Here is another one in VB.NET this time. In this example, we are using it to import a proper set definition. Same idea as before. Please refer to the previous posts for description. </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39; Import a property set definition from an external drawing </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &lt;</span><span style="color: #2b91af; line-hight: 140%;">CommandMethod</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;ACANetScheduleLabs&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;AcaImportPropSetDef&quot;</span><span style="line-hight: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">CommandFlags</span><span style="line-hight: 140%;">.Modal)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> ImportPropertySetDefinition()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Our destination file is the current db </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> dbDestination </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Database</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Our source file from which we import a prop set def.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; ex. the following is from: </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; C:\ProgramData\Autodesk\ACA 2013\enu\Styles\Metric\</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Schedule(Tables(Metric).dwg)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> sourcePath </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">String</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;C:\ACA\ACA .NET 2013\Drawings\Schedule Tables (Metric).dwg&quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> dbSource </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;">(</span><span style="color: blue; line-hight: 140%;">False</span><span style="line-hight: 140%;">, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; dbSource.ReadDwgFile(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; sourcePath, System.IO.</span><span style="color: #2b91af; line-hight: 140%;">FileShare</span><span style="line-hight: 140%;">.Read, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Get the source dictionary </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> dictPropSetDef </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">DictionaryPropertySetDefinitions</span><span style="line-hight: 140%;">(dbSource)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Get the list of def ids that you want to import&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; 1) if you want to import everything from the given </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; dictionary, use this. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;Dim objCollectionSrc As ObjectIdCollection =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#0160; dictPropSetDef.Records</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; 2) if you want to import a specific style, use this. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> objCollectionSrc </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ObjectIdCollection</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; The name of def you want to import&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; objCollectionSrc.Add(dictPropSetDef.GetAt(</span><span style="color: #a31515; line-hight: 140%;">&quot;DoorObjects&quot;</span><span style="line-hight: 140%;">))</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Use CloningHelper class to import styles.&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; There are four options for merge type:</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Normal&#0160; &#0160;&#0160; = 0, // no overwrite </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Overwrite&#0160; = 1, // this is default. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Unique&#0160; &#0160;&#0160; = 2, // rename it if the same name exists.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Merge&#0160; &#0160; &#0160; = 3&#0160; // no overwrite + add overlapping ones as </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; // anonymous name (intended for behind the </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; // scenes further processing.) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> helper </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">CloningHelper</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Uncomment one of these if you want to have a behavior other </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; than default (i.e., overwrite). </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;helper.MergeType = DictionaryRecordMergeBehavior.Unique&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;helper.MergeType = DictionaryRecordMergeBehavior.Merge&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;helper.MergeType = DictionaryRecordMergeBehavior.Normal&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39; Finally call clone. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; helper.Clone(dbSource, dbDestination, objCollectionSrc,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; dictPropSetDef.RecordType, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Catch</span><span style="line-hight: 140%;"> ex </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;error in AcaImportPropSetDef&quot;</span><span style="line-hight: 140%;"> + ex.Message + vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;Prop set def DoorObjects has been successfully imported&quot;</span><span style="line-hight: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; vbCrLf)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
</div>
