---
layout: "post"
title: "Retrieving dimensions in a drawing view"
date: "2015-07-01 23:48:12"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/07/retrieving-dimensions-in-a-drawing-view.html "
typepad_basename: "retrieving-dimensions-in-a-drawing-view"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To retrieve only a few of the dimensions, the “GeneralDimensions.Retrieve” method can be used with second parameter being a collection of dimensions to retrieve. In Inventor 2016, this also brings along other dimensions that were not in the collection. A request has been logged with our engineering team to address this.</p>
<p>As a workaround, all the dimensions can be retrieved in a drawing view and the ones that are not needed can be deleted. In the below code snippet, this workaround is demonstrated and it only retains the dimensions in the drawing view which were retrieved from dimensions which have parameter names matching a specific string.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oDoc <span style="color:#0000ff">As</span><span style="color:#000000">  DrawingDocument</pre>
<pre style="margin:0em;"> oDoc = ThisApplication.ActiveDocument</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> oDrawDimsForView _</pre>
<pre style="margin:0em;"> = ThisApplication.TransientObjects.CreateObjectCollection</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSheet <span style="color:#0000ff">As</span><span style="color:#000000">  Sheet</pre>
<pre style="margin:0em;"> oSheet = oDoc.ActiveSheet</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oDims <span style="color:#0000ff">As</span><span style="color:#000000">  GeneralDimensions</pre>
<pre style="margin:0em;"> oDims = oSheet.DrawingDimensions.GeneralDimensions</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oView <span style="color:#0000ff">As</span><span style="color:#000000">  DrawingView</pre>
<pre style="margin:0em;"> oView = oSheet.DrawingViews(1)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">Dim</span><span style="color:#000000">  oRetrievableDims <span style="color:#0000ff">As</span><span style="color:#000000">  ObjectCollection</pre>
<pre style="margin:0em;"> oRetrievableDims _</pre>
<pre style="margin:0em;"> = ThisApplication.TransientObjects.CreateObjectCollection</pre>
<pre style="margin:0em;"> oRetrievableDims = oDims.GetRetrievableDimensions(oView)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">If</span><span style="color:#000000">  oRetrievableDims.Count &gt; 0 <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  dimsEnum <span style="color:#0000ff">As</span><span style="color:#000000">  GeneralDimensionsEnumerator</pre>
<pre style="margin:0em;">     dimsEnum = oDims.Retrieve(oView)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  paramName <span style="color:#0000ff">As</span><span style="color:#000000">  <span style="color:#0000ff">String</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oDim <span style="color:#0000ff">As</span><span style="color:#000000">  GeneralDimension</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">For</span><span style="color:#000000">  <span style="color:#0000ff">Each</span><span style="color:#000000">  oDim <span style="color:#0000ff">In</span><span style="color:#000000">  dimsEnum</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">If</span><span style="color:#000000">  oDim.Retrieved <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             paramName = oDim.retrievedFrom.Parameter.Name</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">If</span><span style="color:#000000">  paramName &lt;&gt; <span style="color:#a31515">&quot;ShoeWidth&quot;</span><span style="color:#000000">  <span style="color:#0000ff">And</span><span style="color:#000000">  _</pre>
<pre style="margin:0em;">             paramName &lt;&gt; <span style="color:#a31515">&quot;ShoeHeight&quot;</span><span style="color:#000000">  <span style="color:#0000ff">Then</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 oDim.Delete()</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Next</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">End</span><span style="color:#000000">  <span style="color:#0000ff">If</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
