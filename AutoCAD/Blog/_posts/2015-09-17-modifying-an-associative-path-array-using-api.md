---
layout: "post"
title: "Modifying an Associative Path Array using API"
date: "2015-09-17 22:24:01"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/09/modifying-an-associative-path-array-using-api.html "
typepad_basename: "modifying-an-associative-path-array-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In this blog post, we will look at modifying an associative path array. A path array can either be using the item spacing or the item count depending on how the path array is configured in its properties. The below code snippet decrements the item count or increases the item spacing to reduce the number of items along the path.</p>
<p>Here is a recording and a code snippet :</p>
<iframe height="200" src="https://screencast.autodesk.com/Embed/Timeline/4eaf8eb9-0343-4dce-9e1e-eeff12af78cd" frameborder="0" width="320" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;AcDbAssocManager.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;AcDbAssocArrayActionBody.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;AcDbAssocArrayPathParameters.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ads_name ename; </pre>
<pre style="margin:0em;"> ads_point pickPt;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  rc = acedEntSel(_T(<span style="color:#a31515">&quot;\\nSelect Entity&quot;</span><span style="color:#000000"> ), ename, pickPt);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (rc != RTNORM)</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> AcDbObjectId entId; </pre>
<pre style="margin:0em;"> acdbGetObjectId(entId, ename);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectPointer &lt;AcDbEntity&gt; pEntity(entId, AcDb::kForRead);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> ((es = pEntity.openStatus()) != Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (! AcDbAssocArrayActionBody::isAssociativeArray(pEntity))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\nNot an associative array !&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> AcDbObjectId actionBodyId </pre>
<pre style="margin:0em;"> 	= AcDbAssocArrayActionBody::getControllingActionBody(</pre>
<pre style="margin:0em;"> 	pEntity, NULL);</pre>
<pre style="margin:0em;"> pEntity-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbAssocArrayActionBody* pArrayActionBody = NULL;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> ( (es = acdbOpenAcDbObject(</pre>
<pre style="margin:0em;"> 	(AcDbObject*&amp;)pArrayActionBody, </pre>
<pre style="margin:0em;"> 	actionBodyId, </pre>
<pre style="margin:0em;"> 	AcDb::kForWrite)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbAssocArrayParameters *pAssocArrayParams </pre>
<pre style="margin:0em;"> 	= pArrayActionBody-&gt;parameters();</pre>
<pre style="margin:0em;"> AcDbAssocArrayPathParameters *pAssocArrayPathParams </pre>
<pre style="margin:0em;"> 	= AcDbAssocArrayPathParameters::cast(pAssocArrayParams);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbAssocArrayPathParameters::Method pathArrMethod </pre>
<pre style="margin:0em;"> 	= pAssocArrayPathParams-&gt;method();</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (pathArrMethod </pre>
<pre style="margin:0em;"> 	== AcDbAssocArrayPathParameters::Method::kDivide)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//The Divide method arranges the given number of items on the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// entire path equidistantly. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// So we will decrement the number of items along the path</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	pAssocArrayPathParams-&gt;setItemCount</pre>
<pre style="margin:0em;"> 		(pAssocArrayPathParams-&gt;itemCount() - 1);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000"> (pathArrMethod </pre>
<pre style="margin:0em;"> 	== AcDbAssocArrayPathParameters::Method::kMeasure)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">double</span><span style="color:#000000">  itemSpacingBefore </pre>
<pre style="margin:0em;"> 		= pAssocArrayPathParams-&gt;itemSpacing();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//The Measure method arranges the given number of items </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// using the specified item spacing along the path.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	pAssocArrayPathParams-&gt;setItemSpacing</pre>
<pre style="margin:0em;"> 		(pAssocArrayPathParams-&gt;itemSpacing() * 1.1);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">double</span><span style="color:#000000">  itemSpacingAfter </pre>
<pre style="margin:0em;"> 		= pAssocArrayPathParams-&gt;itemSpacing();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (fabs(itemSpacingBefore-itemSpacingAfter) &lt; 0.0001)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">// Reduce the number of items</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		pAssocArrayPathParams-&gt;setItemCount</pre>
<pre style="margin:0em;"> 			(pAssocArrayPathParams-&gt;itemCount() - 1);</pre>
<pre style="margin:0em;"> 		pAssocArrayPathParams-&gt;setItemSpacing</pre>
<pre style="margin:0em;"> 			(pAssocArrayPathParams-&gt;itemSpacing() * 1.1);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> pArrayActionBody-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbAssocManager::evaluateTopLevelNetwork(entId.database());</pre>
<pre style="margin:0em;"> acedUpdateDisplay();</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
