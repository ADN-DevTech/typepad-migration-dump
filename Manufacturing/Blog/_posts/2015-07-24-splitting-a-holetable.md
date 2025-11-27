---
layout: "post"
title: "Splitting a HoleTable"
date: "2015-07-24 02:48:36"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/07/splitting-a-holetable.html "
typepad_basename: "splitting-a-holetable"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Splitting a HoleTable using API is not possible at present. In this blog post we will look at a workaround that mimics a split HoleTable by creating two HoleTables each of which lists a certain hole information. Please note that this workaround creates two separate HoleTables both of which are unrelated and have Sheet as its parent. In case of splitting a HoleTable using Inventor UI, the split HoleTable is nested under a parent HoleTable. This structure provides the option to un-split the HoleTables at any time, if you need to. Such un-split option would not be available when using the workaround demonstrated in this blog post, as the two HoleTables are unrelated. As the HoleTables are separate and have Sheet as their parent, Inventor would not know that the two tables are related by a split.</p>
<p>Here is a VBA code snippet which should work with the attached drawing. If you are trying it on any other drawing, the index values may need to be modified accordingly.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oDoc <span style="color:#0000ff">As</span><span style="color:#000000">  DrawingDocument</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oDoc = ThisDocument</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSheet <span style="color:#0000ff">As</span><span style="color:#000000">  Sheet</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oSheet = oDoc.Sheets.Item(1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oDrawingView <span style="color:#0000ff">As</span><span style="color:#000000">  DrawingView</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oDrawingView = oSheet.DrawingViews.Item(2)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Modify the index to ensure that the right</span></pre>
<pre style="margin:0em;"> <span style="color:#008000">'drawing curve is used to create the intent</span></pre>
<pre style="margin:0em;"> <span style="color:#008000">'For the attached drawing, this will locate </span></pre>
<pre style="margin:0em;"> <span style="color:#008000">'the origin indicator at the lower left corner</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCurve <span style="color:#0000ff">As</span><span style="color:#000000">  DrawingCurve</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oCurve = oDrawingView.DrawingCurves.Item(53)</pre>
<pre style="margin:0em;"> Debug.Print oCurve.CurveType</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oIntent <span style="color:#0000ff">As</span><span style="color:#000000">  GeometryIntent</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oIntent _</pre>
<pre style="margin:0em;">     = oSheet.CreateGeometryIntent(oCurve, oCurve.StartPoint)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Create an origin indicator</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">If</span><span style="color:#000000">  <span style="color:#0000ff">Not</span><span style="color:#000000">  oDrawingView.HasOriginIndicator <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Call</span><span style="color:#000000">  oDrawingView.CreateOriginIndicator(oIntent)</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Positions of the hole tables</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPt1 <span style="color:#0000ff">As</span><span style="color:#000000">  Point2d</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oPt1 _</pre>
<pre style="margin:0em;">     = ThisApplication.TransientGeometry.CreatePoint2d(35, 45)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oPt2 <span style="color:#0000ff">As</span><span style="color:#000000">  Point2d</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oPt2 _</pre>
<pre style="margin:0em;">     = ThisApplication.TransientGeometry.CreatePoint2d(55, 45)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Create a hole table for the drawing view</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oHoleTable1 <span style="color:#0000ff">As</span><span style="color:#000000">  HoleTable</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oHoleTable1 = oSheet.HoleTables.Add(oDrawingView, oPt1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCollection1 <span style="color:#0000ff">As</span><span style="color:#000000">  ObjectCollection</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oCollection1 _</pre>
<pre style="margin:0em;">     = ThisApplication.TransientObjects.CreateObjectCollection()</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCollection2 <span style="color:#0000ff">As</span><span style="color:#000000">  ObjectCollection</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oCollection2 _</pre>
<pre style="margin:0em;">     = ThisApplication.TransientObjects.CreateObjectCollection()</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Identify the row to split</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  splitRow <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Integer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  rowsCnt <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Integer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> rowsCnt = oHoleTable1.HoleTableRows.Count</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">If</span><span style="color:#000000">  rowsCnt <span style="color:#0000ff">Mod</span><span style="color:#000000">  2 = 0 <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     splitRow = rowsCnt / 2</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     splitRow = (rowsCnt - 1) / 2</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Get the hole curves for creating the hole tables</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  cnt <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">Integer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> cnt = 1</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">For</span><span style="color:#000000">  <span style="color:#0000ff">Each</span><span style="color:#000000">  oRow <span style="color:#0000ff">In</span><span style="color:#000000">  oHoleTable1.HoleTableRows</pre>
<pre style="margin:0em;">     cnt = cnt + 1</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">If</span><span style="color:#000000">  cnt &lt; splitRow <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         oCollection1.Add oRow.ReferencedHole</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         oCollection2.Add oRow.ReferencedHole</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Next</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> oHoleTable1.Delete</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Split hole table - 1</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSplitHoleTable1 <span style="color:#0000ff">As</span><span style="color:#000000">  HoleTable</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oSplitHoleTable1 _</pre>
<pre style="margin:0em;">     = oSheet.HoleTables.AddSelected(oCollection1, oPt1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Split hole table - 2</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSplitHoleTable2 <span style="color:#0000ff">As</span><span style="color:#000000">  HoleTable</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Set</span><span style="color:#000000">  oSplitHoleTable2 _</pre>
<pre style="margin:0em;">     = oSheet.HoleTables.AddSelected(oCollection2, oPt2)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">'Renumber the second hole table to mimic a split table</span></pre>
<pre style="margin:0em;"> cnt = oSplitHoleTable1.HoleTableRows.Count + 1</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">For</span><span style="color:#000000">  <span style="color:#0000ff">Each</span><span style="color:#000000">  oRow <span style="color:#0000ff">In</span><span style="color:#000000">  oSplitHoleTable2.HoleTableRows</pre>
<pre style="margin:0em;">     oRow.Item(1).FormattedText = &quot;A&quot; &amp; <span style="color:#0000ff">CStr</span><span style="color:#000000"> (cnt)</pre>
<pre style="margin:0em;">     cnt = cnt + 1</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Next</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13cc346970c img-responsive"><a href="http://adndevblog.typepad.com/files/basepart.idw">Download BasePart.idw</a></span>
<p></p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13cc34c970c img-responsive"><a href="http://adndevblog.typepad.com/files/basepart-1.ipt">Download BasePart.ipt</a></span>
