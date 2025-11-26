---
layout: "post"
title: "Accessing Array Data"
date: "2012-08-16 03:23:16"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/08/accessing-array-data.html "
typepad_basename: "accessing-array-data"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue      <br /></b>We have some problems in assigning the ArrayData to a new Path Collection.</p>  <p>Dim oPath As NavisWorksAPI6.InwOaPath    <br />oPath = p_state.ObjectFactory(NavisWorksAPI6.nwEObjectType.eObjectType_nwOaPath)     <br />Dim oArr(2) As Long     <br />oArr(0) = 1     <br />oArr(1) = 3     <br />oArr(2) = 4     <br />oPath.ArrayData = oArr</p>  <p>The last line in the code is throwing an Exception. How can we fix this?</p>  <p><a name="section2"></a></p>  <p><b>Solution      <br /></b></p>  <p>1.&#160; The indexes (in bold italic) are 1 based.&#160; e.g.</p>  <pre>          Dim oArr(3) As Long&#160; 'array 0 to 3<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oArr(0) = <i><b>1</b>&#160;<br /></i>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oArr(1) = <b><i>2<br /></i></b>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oArr(2) = <b><i>1<br /></i></b>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oArr(3) = <b><i>3<br /></i></b>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oPath.ArrayData = oArr </pre>

<p>2. It all depends what model is loaded. The indexes must be valid within the tree structure of the model. e.g.. the code provided below does work with the sample &lt;Navisworks Installation Path&gt;\api\COM\example\Clashtest.nwd.</p>

<pre>            Dim oArr(1) As Long<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oArr(0) = 1<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oArr(1) = 3</pre>

<p>3.&#160; It makes more sense if you have a selection and get the array from it.</p>
