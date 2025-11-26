---
layout: "post"
title: "Import/Export styles from/to another drawing in .NET "
date: "2012-08-12 21:07:48"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/08/importexport-styles-fromto-another-drawing-in-net-.html "
typepad_basename: "importexport-styles-fromto-another-drawing-in-net-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada </a></p>
<p><strong>Issue</strong></p>
<p>I need to load styles for doors and members etc etc from an AutoCAD Architecture template file to a drawing file. This can be done easily in style dialog by copy and paste through the UI. But I need to do this programmatically using .NET. I tried to use WBlockCloneObjects. But it does not seem to work all the time.&#0160; Is there a way to more reliably to do this?&#0160;</p>
<p><strong>Solution</strong></p>
<p>There is a helper function called CloningHelper under Autodesk.Aec.ApplicationServices.Utility Namespace.&#0160; This allows you to import/export style based information in the same manner as UI&#39;s styles copy/paste operations between drawings.&#0160; Below are the sample commands that demonstrate the usage of this class. It is written for AutoCAD Architecture (ACA).&#0160; But the same concept should apply to the object that you see in the style dialog in AutoCAD MEP.</p>
<p>In the code below, please look at the lines:</p>
<p>&#0160; Autodesk.Aec.ApplicationServices.Utility.CloningHelper helpme = <br />&#0160;&#0160;&#0160; new Autodesk.Aec.OmfSamples.CloningHelper(); <br />&#0160; helpme.Clone(dbSource, dbDestination, objCollectionSrc, rxcls, true);</p>
<p>There are four options for merge type:<br /><br />&#0160;&#0160;&#0160;&#0160; Normal&#0160;&#0160;&#0160;&#0160; = 0, // no overwrite <br />&#0160;&#0160;&#0160;&#0160; Overwrite&#0160; = 1, // this is default. <br />&#0160;&#0160;&#0160;&#0160; Unique&#0160;&#0160;&#0160;&#0160; = 2, // rename it if the same name exists.<br />&#0160;&#0160;&#0160;&#0160; Merge&#0160;&#0160;&#0160;&#0160;&#0160; = 3&#0160; // no overwrite + add overlapping ones as anonymous name <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;// (Intended for behind the scenes further processing.) <br />&#0160;</p>
<p>Note #1: WBlockCloneObject may work with simple cases. But when it has more complex relations, it may not work. For example, we are aware that it won&#39;t work with Schedule Table Styles and Structural Members Styles. CloningHelper is the one ACA uses internally.&#0160;&#0160;&#0160;</p>
<p>Note #2: CloningHelper is exposed in ACA 2009 and later versions in .NET. Only C++/OMF version is available for ACA 2008 and <span style="font-size: 10pt;">earlier versions</span>.&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Class1</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> ed = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; Import a door style from an external drawing. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; [</span><span style="color: #2b91af; line-hight: 140%;">CommandMethod</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;importDoorStyleTest&quot;</span><span style="line-hight: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">void</span><span style="line-hight: 140%;"> importDoorStyleTest()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; our destination file is the current db. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> dbDestination = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">HostApplicationServices</span><span style="line-hight: 140%;">.WorkingDatabase;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; our source file from which we import styles.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; The name of source drawing is harded for </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160; simplicity&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> SourcePath = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #a31515; line-hight: 140%;">@&quot;C:\temp\Door Styles (Metric).dwg&quot;</span><span style="line-hight: 140%;">;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> dbSource = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;">(</span><span style="color: blue; line-hight: 140%;">false</span><span style="line-hight: 140%;">, </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; dbSource.ReadDwgFile(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SourcePath, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.IO.</span><span style="color: #2b91af; line-hight: 140%;">FileShare</span><span style="line-hight: 140%;">.Read, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span></p>
<p style="margin: 0px;"><span style="color: #a31515; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; get the source dictionary&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">DictionaryDoorStyle</span><span style="line-hight: 140%;"> dictStyle = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">DictionaryDoorStyle</span><span style="line-hight: 140%;">(dbSource);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; get the list of style ids that you want to import.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; (1) if you want to import everything, use this.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// the list of ids in the style dictionary. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//ObjectIdCollection objCollectionSrc = </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160; dictStyle.Records; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; (2) if you want to import a specific style, </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160;&#0160; use this.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">// we assume you know the name of style you want to </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; // import.&#0160;</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">ObjectIdCollection</span><span style="line-hight: 140%;"> objCollectionSrc = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ObjectIdCollection</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; objCollectionSrc.Add(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dictStyle.GetAt(</span><span style="color: #a31515; line-hight: 140%;">&quot;Bifold - Double&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; now use CloningHelper class to import styles.&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; there are four options for merge type:</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; Normal&#0160;&#0160;&#0160;&#0160; = 0, // no overwrite </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; Overwrite&#0160; = 1, // this is default. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; Unique&#0160;&#0160;&#0160;&#0160; = 2, // rename it if the same name </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;// exists.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160;&#0160;&#0160; Merge&#0160;&#0160;&#0160; &#0160; = 3&#0160; // no overwrite + add </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;// overlapping as anonymous</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Aec.ApplicationServices.Utility.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CloningHelper</span><span style="line-hight: 140%;">&#0160;</span><span style="line-hight: 140%;">helpme = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> Autodesk.Aec.ApplicationServices.Utility.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">CloningHelper</span><span style="line-hight: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; uncomment&#0160;below if you want a behavior other than </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160; default.</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//helpme.MergeType = </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160; </span><span style="color: green; line-hight: 140%;">Autodesk.Aec.ApplicationServices.Utility.</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160;&#0160; &#0160;DictionaryRecordMergeBehavior.Unique;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//helpme.MergeType = </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //&#0160; Autodesk.Aec.ApplicationServices.Utility.</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;//&#0160;&#0160;&#0160; DictionaryRecordMergeBehavior.Merge;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//helpme.MergeType =</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; // &#0160;Autodesk.Aec.ApplicationServices.Utility.</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;//&#0160;&#0160;&#0160; DictionaryRecordMergeBehavior.Normal;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-hight: 140%;">//&#0160; finally call clone. </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; helpme.Clone(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dbSource, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dbDestination, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objCollectionSrc, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dictStyle.RecordType, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">true</span></p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">catch</span><span style="line-hight: 140%;"> (System.</span><span style="color: #2b91af; line-hight: 140%;">Exception</span><span style="line-hight: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(e.Message);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; }</span></p>
</div>
