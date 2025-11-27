---
layout: "post"
title: "Extracting XML data from drawings using a new .NET API in AutoCAD 2009"
date: "2008-04-07 14:02:49"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Object properties"
original_url: "https://www.keanw.com/2008/04/extracting-data.html "
typepad_basename: "extracting-data"
typepad_status: "Publish"
---

<p>This post is the latest in the series of closer looks at the <a href="http://through-the-interface.typepad.com/through_the_interface/2008/03/new-apis-in-aut.html" target="_blank">new APIs in AutoCAD 2009</a>. It covers the Data Extraction API, a new .NET API allowing you to drive the Data Extraction feature inside AutoCAD.</p>

<p>There is a very thorough C# sample included on the ObjectARX SDK (under <em>samples/dotNet/DataExtraction</em>), so I thought I'd focus on a much simpler example where we extract data from a single drawing.</p>

<p>Here's the C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Collections.Generic;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DataExtraction;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> DataExtraction</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> path =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">@&quot;c:\Program Files\Autodesk\AutoCAD 2009\Sample\&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> fileName =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;Visualization - Aerial.dwg&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> outputXmlFile =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">@&quot;c:\temp\data-extract.xml&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;extd&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> extractData()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (!System.IO.<span style="COLOR: teal">File</span>.Exists(path + fileName))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nFile does not exist.&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Create some settings for the extraction</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">IDxExtractionSettings</span> es =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">DxExtractionSettings</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">IDxDrawingDataExtractor</span> de =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; es.DrawingDataExtractor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;de.Settings.ExtractFlags =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">ExtractFlags</span>.ModelSpaceOnly |</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">ExtractFlags</span>.XrefDependent |</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">ExtractFlags</span>.Nested;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add a single file to the settings</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">IDxFileReference</span> fr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">DxFileReference</span>(path, path + fileName);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;de.Settings.DrawingList.AddFile(fr);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Scan the drawing for object types &amp; their properties</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;de.DiscoverTypesAndProperties(path);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">IDxTypeDescriptor</span>&gt; types =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; de.DiscoveredTypesAndProperties;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Select all the types and properties for extraction</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// by adding them one-by-one to these two lists</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">List</span>&lt;<span style="COLOR: blue">string</span>&gt; selTypes = <span style="COLOR: blue">new</span> <span style="COLOR: teal">List</span>&lt;<span style="COLOR: blue">string</span>&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">List</span>&lt;<span style="COLOR: blue">string</span>&gt; selProps = <span style="COLOR: blue">new</span> <span style="COLOR: teal">List</span>&lt;<span style="COLOR: blue">string</span>&gt;();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">IDxTypeDescriptor</span> type <span style="COLOR: blue">in</span> types)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; selTypes.Add(type.GlobalName);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">IDxPropertyDescriptor</span> pr <span style="COLOR: blue">in</span> type.Properties</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (!selProps.Contains(pr.GlobalName))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;selProps.Add(pr.GlobalName);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Pass this information to the extractor</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;de.Settings.SetSelectedTypesAndProperties(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; types,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; selTypes,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; selProps</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Now perform the extraction itself</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;de.ExtractData(path);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get the results of the extraction</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Data.<span style="COLOR: teal">DataTable</span> dataTable =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; de.ExtractedData;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Output the extracted data to an XML file</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (dataTable.Rows.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dataTable.TableName = <span style="COLOR: maroon">&quot;My_Data_Extract&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dataTable.WriteXml(outputXmlFile);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Here are the first few objects from the output file found in <em>c:\temp\data-extract.xml</em> after running the EXTD command:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;?</span><span style="COLOR: maroon">xml</span><span style="COLOR: blue">&nbsp;</span><span style="COLOR: red">version</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">1.0</span>&quot;<span style="COLOR: blue">&nbsp;</span><span style="COLOR: red">standalone</span><span style="COLOR: blue">=</span>&quot;<span style="COLOR: blue">yes</span>&quot;<span style="COLOR: blue">?&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">DocumentElement</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;</span><span style="COLOR: maroon">My_Data_Extract</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxHandleData</span><span style="COLOR: blue">&gt;</span>183<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">AcDxHandleData</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Layer</span><span style="COLOR: blue">&gt;</span>0<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Layer</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">LinetypeScale</span><span style="COLOR: blue">&gt;</span>1<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">LinetypeScale</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">PlotStyleName</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">PlotStyleName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">LineWeight</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">LineWeight</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Material</span><span style="COLOR: blue">&gt;</span>Material 3<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Material</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Linetype</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Linetype</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Color</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Color</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxObjectTypeName</span><span style="COLOR: blue">&gt;</span>3D Solid<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">AcDxObjectTypeName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxObjectTypeGlobalName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Autodesk.AutoCAD.DatabaseServices.Solid3d</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxObjectTypeGlobalName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Visualization - Aerial.dwg</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLocation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;c:\Program Files\Autodesk\AutoCAD 2009\Sample</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLocation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgSize</span><span style="COLOR: blue">&gt;</span>472480<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgSize</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgCreated</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;2007-01-03T00:44:20+01:00</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgCreated</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgModified</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;2007-01-03T00:44:20+01:00</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgModified</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgAccessed</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;2008-03-03T11:40:53.796875+01:00</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgAccessed</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgTitle</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgSubject</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgAuthor</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgKeywords</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgComments</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgHyperLinkBase</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLastSavedBy</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;thompsl</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLastSavedBy</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgRevisionNumber</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgTotalEditingTime</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;1179</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgTotalEditingTime</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxEntityHyperlink</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;/</span><span style="COLOR: maroon">My_Data_Extract</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;</span><span style="COLOR: maroon">My_Data_Extract</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxHandleData</span><span style="COLOR: blue">&gt;</span>191<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">AcDxHandleData</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Layer</span><span style="COLOR: blue">&gt;</span>0<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Layer</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">LinetypeScale</span><span style="COLOR: blue">&gt;</span>1<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">LinetypeScale</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">PlotStyleName</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">PlotStyleName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">LineWeight</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">LineWeight</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Material</span><span style="COLOR: blue">&gt;</span>Material 3<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Material</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Linetype</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Linetype</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">Color</span><span style="COLOR: blue">&gt;</span>ByLayer<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">Color</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxObjectTypeName</span><span style="COLOR: blue">&gt;</span>3D Solid<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">AcDxObjectTypeName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxObjectTypeGlobalName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Autodesk.AutoCAD.DatabaseServices.Solid3d</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxObjectTypeGlobalName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Visualization - Aerial.dwg</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgName</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLocation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;c:\Program Files\Autodesk\AutoCAD 2009\Sample</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLocation</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgSize</span><span style="COLOR: blue">&gt;</span>472480<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgSize</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgCreated</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;2007-01-03T00:44:20+01:00</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgCreated</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgModified</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;2007-01-03T00:44:20+01:00</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgModified</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgAccessed</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;2008-03-03T11:40:53.796875+01:00</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgAccessed</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgTitle</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgSubject</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgAuthor</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgKeywords</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgComments</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgHyperLinkBase</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLastSavedBy</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;thompsl</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgLastSavedBy</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgRevisionNumber</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxDwgSummaryDwgTotalEditingTime</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;1179</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;/</span><span style="COLOR: maroon">AcDxDwgSummaryDwgTotalEditingTime</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &nbsp; &lt;</span><span style="COLOR: maroon">AcDxEntityHyperlink</span><span style="COLOR: blue"> /&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;/</span><span style="COLOR: maroon">My_Data_Extract</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;!--</span><span style="COLOR: green"> Stuff deleted from the XML output </span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&nbsp; &lt;!--</span><span style="COLOR: green"> ... </span><span style="COLOR: blue">--&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">DocumentElement</span><span style="COLOR: blue">&gt;</span></p></div>
