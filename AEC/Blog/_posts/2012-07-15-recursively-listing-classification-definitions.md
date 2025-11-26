---
layout: "post"
title: "Recursively listing classification definitions"
date: "2012-07-15 19:41:40"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/07/recursively-listing-classification-definitions.html "
typepad_basename: "recursively-listing-classification-definitions"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>Using AutoCAD Architecture .NET API, I would like to list all classifications even if there are several levels to them.&#0160; If child.Children.Count below is more than 0, I don&#39;t know how to act to get to the children.</p>
<p><strong>Solution</strong></p>
<p>A classification definition is stored in a form of tree structure.&#0160; Given a top most node of a tree, you will need to visit each child node recursively.&#0160; i.e.,</p>
<p>&#0160;&#0160;&#0160; Public Sub ListClassificationTree(<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ByRef tr As Transaction, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ByRef node As ClassificationTree, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ByVal sIndent As String)</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ...</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; If node.Children.Count &gt; 0 Then</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#39; this is an intermediate node, go through each branch.</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim child As ClassificationTree<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; For Each child In node.Children<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ListClassificationTree(tr, child, sIndent)<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Next<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ...</p>
<p>The following command demonstrates how to list a tree structure of classification definition.&#0160; Try, for example, with &quot;Structural Usage&quot; classification that comes with a metric template.&#0160; The command lists the classification definitions that apply to either Mass Elements or Walls.</p>
<p>&#0160;</p>
<div style="font-family: Consolas; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ACANETClassificationDef</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; list classification definitions. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &lt;</span><span style="color: #2b91af; line-hight: 140%;">CommandMethod</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;ACANet&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #a31515; line-hight: 140%;">&quot;ListClassificationDef&quot;</span><span style="line-hight: 140%;">, </span><span style="color: #2b91af; line-hight: 140%;">CommandFlags</span><span style="line-hight: 140%;">.Modal)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> ListClassificationDef()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; Some basic things here</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> db </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> = </span><span style="color: #2b91af; line-hight: 140%;">HostApplicationServices</span><span style="line-hight: 140%;">.WorkingDatabase</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; Start a Transaction</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> tr </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;"> = db.TransactionManager.StartTransaction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; get the dictionary for the classification definition, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; and list of records in it </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> dict </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">DictionaryClassificationDefinition</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">DictionaryClassificationDefinition</span><span style="line-hight: 140%;">(db)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ids </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ObjectIdCollection</span><span style="line-hight: 140%;"> = dict.Records</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> id </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ObjectId</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; loop through each classification def </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">For</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Each</span><span style="line-hight: 140%;"> id </span><span style="color: blue; line-hight: 140%;">In</span><span style="line-hight: 140%;"> ids</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> obj </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Object</span><span style="line-hight: 140%;"> = tr.GetObject(id, </span><span style="color: #2b91af; line-hight: 140%;">OpenMode</span><span style="line-hight: 140%;">.ForRead, </span><span style="color: blue; line-hight: 140%;">False</span><span style="line-hight: 140%;">, </span><span style="color: blue; line-hight: 140%;">False</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> classCollDef </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ClassificationDefinition</span><span style="line-hight: 140%;"> = obj</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; as an example, check to see if class def is </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; applicable to Mass or Wall.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> classCollDef.AppliesToFilter.Contains(</span><span style="color: #a31515; line-hight: 140%;">&quot;AecDbMassElem&quot;</span><span style="line-hight: 140%;">) </span><span style="color: blue; line-hight: 140%;">Or</span><span style="line-hight: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160;&#0160; classCollDef.AppliesToFilter.Contains(</span><span style="color: #a31515; line-hight: 140%;">&quot;AecDbWall&quot;</span><span style="line-hight: 140%;">) </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; Found a classification definition that was applied to </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; AecDbMassElem/AcDbWall. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; print out the name of the classification. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(classCollDef.Name + vbCrLf)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; Get the tree and print it out </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> classTree </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ClassificationTree</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; classCollDef.ClassificationTree</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ListClassificationTree(tr, classTree, </span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Catch</span><span style="line-hight: 140%;"> ex </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ApplicationException</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Abort()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; System.Windows.Forms.</span><span style="color: #2b91af; line-hight: 140%;">MessageBox</span><span style="line-hight: 140%;">.Show(ex.Message)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Finally</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Dispose()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; Given a top most node of a tree, traverse the tree </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> ListClassificationTree(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">ByRef</span><span style="line-hight: 140%;"> tr </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">ByRef</span><span style="line-hight: 140%;"> node </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ClassificationTree</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">ByVal</span><span style="line-hight: 140%;"> sIndent </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">String</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> = </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> node </span><span style="color: blue; line-hight: 140%;">Is</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Nothing</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Return</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; let&#39;s get the name of the top most node. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Not</span><span style="line-hight: 140%;"> (node.Id.IsNull) </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> classi </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Classification</span><span style="line-hight: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">CType</span><span style="line-hight: 140%;">(tr.GetObject(node.Id, </span><span style="color: #2b91af; line-hight: 140%;">OpenMode</span><span style="line-hight: 140%;">.ForRead), </span><span style="color: #2b91af; line-hight: 140%;">Classification</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> s </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">String</span><span style="line-hight: 140%;"> = sIndent + classi.Name +</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;(&quot;</span><span style="line-hight: 140%;"> + node.Id.Handle.ToString + </span><span style="color: #a31515; line-hight: 140%;">&quot;)&quot;</span><span style="line-hight: 140%;"> + vbCrLf</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(s)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; visit childen.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> node.Children.Count &gt; 0 </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39; this is an intermediate node, go through each branch.</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> child </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ClassificationTree</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; sIndent = sIndent + </span><span style="color: #a31515; line-hight: 140%;">&quot;&#0160; &quot;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">For</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Each</span><span style="line-hight: 140%;"> child </span><span style="color: blue; line-hight: 140%;">In</span><span style="line-hight: 140%;"> node.Children</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; ListClassificationTree(tr, child, sIndent)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
</div>
