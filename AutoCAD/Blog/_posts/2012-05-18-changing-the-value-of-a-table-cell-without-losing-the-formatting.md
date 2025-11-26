---
layout: "post"
title: "Changing the value of a Table cell without losing the formatting"
date: "2012-05-18 08:54:39"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/changing-the-value-of-a-table-cell-without-losing-the-formatting.html "
typepad_basename: "changing-the-value-of-a-table-cell-without-losing-the-formatting"
typepad_status: "Publish"
---

<p>By <a target="_self" href="http://adndevblog.typepad.com/autocad/adam-nagy.html">Adam Nagy</a></p>
<p>I'm trying to change the value of a Table cell which has some formatting that keeps the value in fractional format.</p>
<p>When I change the value the following way then the ["] sign will be missing from the end of the value and also when the user edits the value it will not be formatted to fractional anymore, i.e. the cell loses its formatting. What is the solution?</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">table.SetValue(row, col, </span><span style="color: #a31515; line-height: 140%;">"6 1/2"</span><span style="line-height: 140%;">, ParseOption.SetDefaultFormat);</span></p>
</div>
<p><strong>Solution</strong></p>
<p>The cell's Value type is double, but when you use SetValue() and pass in a string the Value type will be converted to string.</p>
<p>Another thing is that no matter what ParseOption you pass in, the DataFormat of that cell will be erased.</p>
<p>So you need to store the Format and pass in a double value, then restore the Format. This way the value will be formatted as it should be.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;ChangeTableValue&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ChangeTableValue()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a table&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> row = 1, col = 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Table</span><span style="line-height: 140%;"> table = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: #2b91af; line-height: 140%;">Table</span><span style="line-height: 140%;">)tr.GetObject(per.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// table.SetValue(), table.GetDataFomat() </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// and table.SetDataFormat() are obsolete.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Use below functions instead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">CellContent</span><span style="line-height: 140%;"> cont = table.Cells[row, col].Contents[0]; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> format = cont.DataFormat;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; cont.SetValue(8.5, </span><span style="color: #2b91af; line-height: 140%;">ParseOption</span><span style="line-height: 140%;">.SetDefaultFormat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; cont.DataFormat = format;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
