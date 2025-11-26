---
layout: "post"
title: "Extracting XML data of selected objects from current drawing using .NET"
date: "2016-10-13 05:23:51"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "2016"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2016/10/extracting-xml-data-of-selected-objects-from-current-drawing-using-net.html "
typepad_basename: "extracting-xml-data-of-selected-objects-from-current-drawing-using-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Here is a <a href="http://through-the-interface.typepad.com/through_the_interface/2008/04/extracting-data.html">blog </a>by Kean Walmsley, which demonstrates Extracting XML data from drawings.</p>
<p>To extract data from the selected objects of current drawing, following modification to the code in the Kean's blog is required :</p>
<p>1) Current drawing name and path can be&nbsp;used in place of that of the external file.<br />2) Create a List&lt;Handle&gt; of selected objects.<br />2) The List&lt;Handle&gt; of selected objects to be assigned to the API "IDxDrawingDataExtractor.Settings.SelectedObjects"</p>
<p>Here is the modified code:&nbsp;</p>
<pre>
<p>using System;<br />using Autodesk.AutoCAD.Runtime;<br />using Autodesk.AutoCAD.ApplicationServices;<br />using Autodesk.AutoCAD.DatabaseServices;<br />using Autodesk.AutoCAD.Geometry;<br />using Autodesk.AutoCAD.EditorInput;</p>
<p>using System.Collections;<br />using System.Collections.Specialized;</p>
<p>using System.Collections.Generic;<br />using Autodesk.AutoCAD.DataExtraction;<br />using System.IO;</p>
<p>namespace DataExtraction<br />{</p>
<p>// This class is instantiated by AutoCAD for each document when<br /> // a command is called by the user the first time in the context<br /> // of a given document. In other words, non static data in this class<br /> // is implicitly per-document!<br /> public class MyCommands<br /> {<br /> const string outputXmlFile =<br /> @"c:\temp\ExtAttr.xml";</p>
<p>[CommandMethod("extd")]<br /> public void extractData()<br /> {<br /> Document doc = Application.DocumentManager.MdiActiveDocument;<br /> Database db = doc.Database;<br /> Editor ed = doc.Editor;<br /> string fileName = Path.GetFileName(doc.Name); <br /> string path = Path.GetDirectoryName(doc.Name) + "\\";</p>
<p>try<br /> {<br /> if (!System.IO.File.Exists(path + fileName))<br /> {<br /> ed.WriteMessage("\nFile does not exist.");<br /> return;<br /> }</p>
<p>//List to get handle of all the selected objects<br /> List&lt;Handle&gt; selObjsHandle = new List&lt;Handle&gt;();</p>
<p>PromptSelectionResult selectionRes = ed.SelectImplied();<br /> // If there's no pickfirst set available...<br /> if (selectionRes.Status == PromptStatus.Error)<br /> {<br /> // ... ask the user to select entities<br /> PromptSelectionOptions selectionOpts = new PromptSelectionOptions();<br /> selectionOpts.MessageForAdding = "\nSelect objects to extract data: ";<br /> selectionRes = ed.GetSelection(selectionOpts);<br /> }<br /> else<br /> {<br /> // If there was a pickfirst set, clear it<br /> ed.SetImpliedSelection(new ObjectId[0]);<br /> }</p>
<p>// If the user has not cancelled...<br /> if (selectionRes.Status == PromptStatus.OK)<br /> {<br /> // ... take the selected objects one by one<br /> Transaction tr =<br /> doc.TransactionManager.StartTransaction();<br /> try<br /> {<br /> ObjectId[] objIds =<br /> selectionRes.Value.GetObjectIds();<br /> foreach (ObjectId objId in objIds)<br /> {</p>
<p>DBObject obj =<br /> tr.GetObject(objId, OpenMode.ForRead);<br /> Entity ent = (Entity)obj;<br /> // This time access the properties directly<br /> ed.WriteMessage("\n Handle: " + ent.Handle.ToString());<br /> selObjsHandle.Add(ent.Handle);<br /> obj.Dispose();<br /> }<br /> // Although no changes were made, use Commit()<br /> // as this is much quicker than rolling back<br /> tr.Commit();<br /> }<br /> catch (Autodesk.AutoCAD.Runtime.Exception ex)<br /> {<br /> ed.WriteMessage(ex.Message);<br /> tr.Abort();<br /> }<br /> }</p>
<p>// Create some settings for the extraction</p>
<p>IDxExtractionSettings es =<br /> new DxExtractionSettings();</p>
<p>IDxDrawingDataExtractor de =<br /> es.DrawingDataExtractor;</p>
<p>de.Settings.ExtractFlags =<br /> ExtractFlags.ModelSpaceOnly |<br /> ExtractFlags.XrefDependent |<br /> ExtractFlags.Nested;</p>
<p>// Add a single file to the settings</p>
<p>IDxFileReference fr =<br /> new DxFileReference(path, path + fileName);</p>
<p>de.Settings.DrawingList.AddFile(fr);</p>
<p>//selected objects handle list assigned to IDxDrawingDataExtractor<br /> de.Settings.SelectedObjects = selObjsHandle;</p>
<p>// Scan the drawing for object types &amp; their properties<br /> de.DiscoverTypesAndProperties(path);</p>
<p>List&lt;IDxTypeDescriptor&gt; types = de.DiscoveredTypesAndProperties;</p>
<p>// Select all the types and properties for extraction<br /> // by adding them one-by-one to these two lists</p>
<p>List&lt;string&gt; selTypes = new List&lt;string&gt;();<br /> List&lt;string&gt; selProps = new List&lt;string&gt;();</p>
<p>foreach (IDxTypeDescriptor type in types)<br /> {<br /> selTypes.Add(type.GlobalName);<br /> foreach (<br /> IDxPropertyDescriptor pr in type.Properties<br /> )<br /> {<br /> if (!selProps.Contains(pr.GlobalName))<br /> selProps.Add(pr.GlobalName);<br /> }<br /> }</p>
<p>// Pass this information to the extractor<br /> de.Settings.SetSelectedTypesAndProperties(<br /> types,<br /> selTypes,<br /> selProps<br /> );</p>
<p>// Now perform the extraction itself<br /> de.ExtractData(path);</p>
<p>// Get the results of the extraction<br /> System.Data.DataTable dataTable =<br /> de.ExtractedData;</p>
<p>// Output the extracted data to an XML file</p>
<p>if (dataTable.Rows.Count &gt; 0)<br /> {<br /> dataTable.TableName = "My_Data_Extract";<br /> dataTable.WriteXml(outputXmlFile);<br /> }</p>
<p>}<br /> catch (Autodesk.AutoCAD.Runtime.Exception ex)<br /> {<br /> ed.WriteMessage(ex.Message);<br /> }<br /> }</p>
<p>}</p>
<p>}</p>
<h3 class="entry-header">&nbsp;</h3>
</pre>
