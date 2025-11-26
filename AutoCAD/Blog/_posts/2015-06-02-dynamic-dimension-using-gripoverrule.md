---
layout: "post"
title: "Dynamic dimension using GripOverrule"
date: "2015-06-02 22:44:29"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/06/dynamic-dimension-using-gripoverrule.html "
typepad_basename: "dynamic-dimension-using-gripoverrule"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Dynamic dimensions are an easy and intuitive way to let users modify entity dimensions using grips. For this to work, the dimensional input is to be turned on by setting the DYNMODE system variable to either 2 or 3. If you wish to change the default grip editing behavior for an entity and prompt for another dimension that is more intuitive and readily available for users, grip overrule can help do that.</p>
<p>For a circle, when a grip turns hot, a dynamic dimension appears prompting for the radius value. In the following sample code, we will change that to accept diameter value by displaying a dynamic dimension diametrically on the circle being grid edited. Also, the prompt at the command line window is changed to make the user aware of the diameter input that we are expecting.</p>
<p>Here is a recording of the behavior with/without the grip overrule and the sample code :</p>
<iframe height="200" src="https://screencast.autodesk.com/Embed/7564f7a2-51ec-4b59-aabb-6becb1542a58" frameborder="0" width="320" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;dbentityoverrule.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">class</span><span style="color:#000000">  CMyGripOverrule: <span style="color:#0000ff">public</span><span style="color:#000000">  AcDbGripOverrule</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  CMyGripOverrule* _pTheOverrule;</pre>
<pre style="margin:0em;"> 	ACRX_DECLARE_MEMBERS(CMyGripOverrule);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  AcDbDimData *mpDimData;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  CMyGripOverrule::AddOverrule()</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		mpDimData = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (_pTheOverrule != NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		_pTheOverrule = <span style="color:#0000ff">new</span><span style="color:#000000">  CMyGripOverrule();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcRxOverrule::addOverrule(</pre>
<pre style="margin:0em;"> 						AcDbCircle::desc(),</pre>
<pre style="margin:0em;"> 						_pTheOverrule, </pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">true</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						);</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		CMyGripOverrule::setIsOverruling(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  CMyGripOverrule::RemoveOverrule()</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (_pTheOverrule == NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		CMyGripOverrule::setIsOverruling(<span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcRxOverrule::removeOverrule(</pre>
<pre style="margin:0em;"> 					AcDbCircle::desc(), </pre>
<pre style="margin:0em;"> 					_pTheOverrule</pre>
<pre style="margin:0em;"> 					);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  _pTheOverrule;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		_pTheOverrule = NULL;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	ACDB_PORT Acad::ErrorStatus moveGripPointsAt(</pre>
<pre style="margin:0em;"> 						AcDbEntity* pSubject,</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbVoidPtrArray&amp; gripAppData,</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">const</span><span style="color:#000000">  AcGeVector3d&amp; offset, </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  bitflags)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (!pSubject-&gt;isKindOf(AcDbCircle::desc())) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbCircle* pCircle = AcDbCircle::cast(pSubject);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (pCircle == NULL) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGeVector3d normal = pCircle-&gt;normal();</pre>
<pre style="margin:0em;"> 		AcGeVector3d horizDir = normal.perpVector();</pre>
<pre style="margin:0em;"> 		AcGePoint3d center = pCircle-&gt;center();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  radius = pCircle-&gt;radius();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGePoint3d pt1 = center + radius * horizDir;</pre>
<pre style="margin:0em;"> 		pt1 = pt1 + offset;</pre>
<pre style="margin:0em;"> 		AcGeVector3d radVec = pt1 - center;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		updateDimensions(</pre>
<pre style="margin:0em;"> 			pCircle, </pre>
<pre style="margin:0em;"> 			center-radVec, </pre>
<pre style="margin:0em;"> 			center+radVec);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbGripOverrule::moveGripPointsAt(</pre>
<pre style="margin:0em;"> 			pSubject, </pre>
<pre style="margin:0em;"> 			gripAppData, </pre>
<pre style="margin:0em;"> 			offset, </pre>
<pre style="margin:0em;"> 			bitflags); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	ACDB_PORT Acad::ErrorStatus getGripPoints(</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbEntity* pSubject, </pre>
<pre style="margin:0em;"> 		AcDbGripDataPtrArray&amp; grips, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">double</span><span style="color:#000000">  curViewUnitSize, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  gripSize, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcGeVector3d&amp; curViewDir, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  bitflags</pre>
<pre style="margin:0em;"> 	)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (!pSubject-&gt;isKindOf(AcDbCircle::desc())) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbCircle* pCircle </pre>
<pre style="margin:0em;"> 			= AcDbCircle::cast(pSubject);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (pCircle == NULL) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbGripDataPtrArray oldGrips;</pre>
<pre style="margin:0em;"> 		AcDbGripOverrule::getGripPoints(</pre>
<pre style="margin:0em;"> 			pSubject, oldGrips, </pre>
<pre style="margin:0em;"> 			curViewUnitSize, gripSize, </pre>
<pre style="margin:0em;"> 			curViewDir, bitflags); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbGripData * pGripData </pre>
<pre style="margin:0em;"> 			= <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbGripData(*(oldGrips.at(1)));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pGripData-&gt;setGripPoint</pre>
<pre style="margin:0em;"> 			(oldGrips.at(1)-&gt;gripPoint());</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pGripData-&gt;setHotGripDimensionFunc</pre>
<pre style="margin:0em;"> 			(MyGripHotGripDimensionfunc);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pGripData-&gt;setHoverDimensionFunc</pre>
<pre style="margin:0em;"> 			(MyHoverGripDimensionfunc);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pGripData-&gt;setCLIPromptFunc</pre>
<pre style="margin:0em;"> 			(MyGripCLIPromptCallback);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		grips.append(pGripData);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  gripDimensionCbackFunc(</pre>
<pre style="margin:0em;"> 		AcDbGripData* pGrip, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbObjectId&amp; objId, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  dimScale, </pre>
<pre style="margin:0em;"> 		AcDbDimDataPtrArray&amp; dimDataArr)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (pGrip == NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbEntity *pEnt = NULL;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (acdbOpenAcDbEntity(</pre>
<pre style="margin:0em;"> 			pEnt, objId, </pre>
<pre style="margin:0em;"> 			AcDb::kForRead) != Acad::eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbCircle *pCircle </pre>
<pre style="margin:0em;"> 			= AcDbCircle::cast(pEnt);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (pCircle == NULL) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			pEnt-&gt;close();</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGeVector3d normal = pCircle-&gt;normal();</pre>
<pre style="margin:0em;"> 		AcGeVector3d horizDir = normal.perpVector();</pre>
<pre style="margin:0em;"> 		AcGeVector3d vertDir </pre>
<pre style="margin:0em;"> 			= normal.crossProduct(horizDir);</pre>
<pre style="margin:0em;"> 		AcGePoint3d center = pCircle-&gt;center();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  radius = pCircle-&gt;radius();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pCircle-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbAlignedDimension *pAlignedDim </pre>
<pre style="margin:0em;"> 			= <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbAlignedDimension();</pre>
<pre style="margin:0em;"> 		pAlignedDim-&gt;setDatabaseDefaults();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setDimsah(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setDimse1(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setDimblk1(_T(<span style="color:#a31515">&quot;None&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setNormal(normal);</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setElevation(0.0);</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setHorizontalRotation(0.0);</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setXLine1Point</pre>
<pre style="margin:0em;"> 			(center - radius * horizDir);</pre>
<pre style="margin:0em;"> 	    es = pAlignedDim-&gt;setXLine2Point</pre>
<pre style="margin:0em;"> 			(center + radius * horizDir);</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setDimLinePoint</pre>
<pre style="margin:0em;"> 			(center + 0.5 * radius * horizDir);</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setDynamicDimension(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pAlignedDim-&gt;setColorIndex(1);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbDimData *pDimData </pre>
<pre style="margin:0em;"> 			= <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbDimData(pAlignedDim);</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setOwnerId(objId);</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setDimFocal(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setDimEditable(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setDimRadius(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setDimHideIfValueIsZero(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setDimValueFunc</pre>
<pre style="margin:0em;"> 			(setDimValueCbackFunc);</pre>
<pre style="margin:0em;"> 		es = pDimData-&gt;setDimension(pAlignedDim);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		dimDataArr.append(pDimData);</pre>
<pre style="margin:0em;"> 		mpDimData = pDimData;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">bool</span><span style="color:#000000">  isApplicable(</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxObject *pOverruledSubject) <span style="color:#0000ff">const</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (!pOverruledSubject-&gt;isKindOf</pre>
<pre style="margin:0em;"> 			(AcDbCircle::desc())) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">const</span><span style="color:#000000">  ACHAR* MyGripCLIPromptCallback</pre>
<pre style="margin:0em;"> 		(AcDbGripData *pGripData)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  ACRX_T(<span style="color:#a31515">&quot;\\nRing diameter : &quot;</span><span style="color:#000000"> ); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  MyGripHotGripDimensionfunc</pre>
<pre style="margin:0em;"> 		(AcDbGripData *pGripData, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbObjectId &amp;entId, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  dimScale, </pre>
<pre style="margin:0em;"> 		AcDbDimDataPtrArray &amp;dimDataArr)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		gripDimensionCbackFunc</pre>
<pre style="margin:0em;"> 			(pGripData, entId, dimScale, dimDataArr);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  MyHoverGripDimensionfunc</pre>
<pre style="margin:0em;"> 		(AcDbGripData *pGripData, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbObjectId &amp;entId, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  dimScale, </pre>
<pre style="margin:0em;"> 		AcDbDimDataPtrArray &amp;dimDataArr)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		gripDimensionCbackFunc(pGripData, </pre>
<pre style="margin:0em;"> 			entId, dimScale, dimDataArr);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  AcGeVector3d setDimValueCbackFunc</pre>
<pre style="margin:0em;"> 		(AcDbDimData* pDimData, AcDbEntity* pEnt, </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  newValue, <span style="color:#0000ff">const</span><span style="color:#000000">  AcGeVector3d&amp; offset)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcGeVector3d newOffset(offset);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  ((pDimData == NULL) || (pEnt == NULL))</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  newOffset;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectId objId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbCircle *pCir = AcDbCircle::cast(pEnt);</pre>
<pre style="margin:0em;">    		 <span style="color:#0000ff">if</span><span style="color:#000000">  (pCir == NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  newOffset;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		pCir-&gt;setRadius(newValue * 0.5);</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  newOffset;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  updateDimensions</pre>
<pre style="margin:0em;"> 		(AcDbCircle* pCircle, </pre>
<pre style="margin:0em;"> 		AcGePoint3d xline1Pt, </pre>
<pre style="margin:0em;"> 		AcGePoint3d xline2Pt)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (!pCircle || !mpDimData)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectId entId = pCircle-&gt;objectId();</pre>
<pre style="margin:0em;"> 		AcGeVector3d normal = pCircle-&gt;normal();</pre>
<pre style="margin:0em;"> 		AcGeVector3d horizDir = normal.perpVector();</pre>
<pre style="margin:0em;"> 		AcGeVector3d vertDir </pre>
<pre style="margin:0em;"> 			= normal.crossProduct(horizDir);</pre>
<pre style="margin:0em;"> 		AcGePoint3d center = pCircle-&gt;center();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  radius = pCircle-&gt;radius();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectId ownerId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbDimData *pDimData = mpDimData;</pre>
<pre style="margin:0em;"> 		AcDbAlignedDimension *pAlignedDim </pre>
<pre style="margin:0em;"> 			= AcDbAlignedDimension::cast(</pre>
<pre style="margin:0em;"> 			pDimData-&gt;dimension());</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pAlignedDim != NULL)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			pAlignedDim-&gt;setXLine1Point(xline1Pt);</pre>
<pre style="margin:0em;"> 			pAlignedDim-&gt;setXLine2Point(xline2Pt);</pre>
<pre style="margin:0em;"> 			pAlignedDim-&gt;setDimLinePoint</pre>
<pre style="margin:0em;"> 				(xline1Pt + (xline2Pt - xline1Pt) * 0.5);</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbDimData * CMyGripOverrule::mpDimData = NULL;</pre>
<pre style="margin:0em;"> CMyGripOverrule* CMyGripOverrule::_pTheOverrule = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ACRX_NO_CONS_DEFINE_MEMBERS(</pre>
<pre style="margin:0em;"> 	CMyGripOverrule, </pre>
<pre style="margin:0em;"> 	AcDbGripOverrule);</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
