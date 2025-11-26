---
layout: "post"
title: "Making a custom entity Associative Dimension enabled"
date: "2015-02-12 22:35:07"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/making-a-custom-entity-associative-dimension-enabled.html "
typepad_basename: "making-a-custom-entity-associative-dimension-enabled"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>You can create associative dimensions after setting the system variable DIMASSOC value to 2 and calling any of the dimensioning commands. This works fine on all the native AutoCAD entities, but it does not with your own custom entity.&nbsp;To enable associative dimensioning for your custom entity, there are two ways. In this blog post and in its attached sample, these are demonstrated.</p>
<p>Before we look at sample code, here is a short video of its working :</p>
<p></p>
<iframe width="470" height="294" src="https://screencast.autodesk.com/Embed/a61ff936-fcba-4c99-9aa0-95cde134cf6c" frameborder="0" allowfullscreen webkitallowfullscreen></iframe>
</p>
<p>Method 1 :&nbsp;</p>
<p>You need to take help of few exported APIs in AcDimX20.dll. Unfortunately these are not published APIs and hence they are *not supported* and you have to use them at your own risk.</p>
<p>The three APIs exported in the AcDimX20.dll are:<br /> <br />Acad::ErrorStatus acdbEnableDimAssocForEntity(AcRxClass* pClass)<br />Acad::ErrorStatus acdbDisableDimAssocForEntity(AcRxClass* pClass)<br />bool acdbDimAssocIsEnabledForEntity(AcRxClass* pClass)<br /> <br />Their function is self explanatory. The AcDimX.dll file is available in the AutoCAD install folder.<br /> <br />Using acdbEnableDimAssocForEntity() API you can enable associative dimensioning for your custom entity. In the attached sample project, "MyEnt2" uses this approach.<br /> <br />You should look at the On_kInitAppMsg() function in the acrxEntryPoint.cpp that registers the custom entity class for associative dimensioning.<br /> <br />Also, you *must* implement subSubentPtr() override for your custom entity. In the attached sample project, "MyEnt2" is a custom entity that enables associative dimension using this method. It has subSubentPtr method implemented which allows the user to use associative dimensioning at its end points.</p>
<p>Here is the relevant portion of the code. To try it, please download the attached sample project.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">typedef</span><span style="color:#000000">  Acad::ErrorStatus </pre>
<pre style="margin:0em;"> 	(*exp_acdbDimAssocForEntity)(AcRxClass* pClass);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> MyEnt2::rxInit();</pre>
<pre style="margin:0em;"> acrxBuildClassHierarchy();</pre>
<pre style="margin:0em;"> HMODULE hModule = LoadLibrary( _T(<span style="color:#a31515">&quot;AcDimX20.dll&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> exp_acdbDimAssocForEntity fPtrEnableDimAssoc = NULL;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (NULL != hModule)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	fPtrEnableDimAssoc = (exp_acdbDimAssocForEntity)</pre>
<pre style="margin:0em;"> 		GetProcAddress(hModule, </pre>
<pre style="margin:0em;"> 		<span style="color:#a31515">&quot;acdbEnableDimAssocForEntity&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (fPtrEnableDimAssoc)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Enable associative dimension for MyEnt2 using </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// acdbEnableDimAssocForEntity</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	es = fPtrEnableDimAssoc(MyEnt2::desc());</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//free the library</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (NULL != hModule)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	FreeLibrary(hModule);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Method 2 :</p>
<p>For custom entity derived from AcDb3dSolid and AcDbSurface, Method 1 does not work. Here is the explanation provided by Jiri Kripac from our engineering team on why the first approach does not work :</p>

<p style="color:blue">AcDb3dSolid and AcDbSurface use the new associativity mechanism based on the Associative Framework.&nbsp;The associative dimension is controlled by an action derived from AcDbAssocAnnotationActionBody, not by the legacy AcDbDimAssoc. This allows to maintain associativity even when the topology of the solid/surface changes because the new mechanism uses persistent subentity ids (complex internal objects) to persistently identify subentities&nbsp;(faces/edges/vertices) of the entity, whereas AcDbDimAssoc uses just simple indices that become invalid when the&nbsp;topology of the entity changes.</p>
<p style="color:blue">Entities reveal their subentities by implementing AcDbAssocPersSubentIdPE.&nbsp;The Associative Framework queries this protocol extension when creating new associative dimensions based&nbsp;on AcDbAssocAnnotationActionBody. AcDb3dSolid exposes this protocol extension.&nbsp;Your custom entity derived from AcDb3dSolid does not expose this protocol extension to reveal your subentities,&nbsp;therefore the base class protocol extension for AcDb3dSolid is used that reveals subentities of the solid.&nbsp;</p>
<p style="color:blue">You need to implement AcDbAssocPersSubentIdPE to reveal your subentities of your custom entity.</p>
<p>In the attached sample project, "MyEnt1" is a custom entity derived from AcDb3dSolid and implements AcDbAssocPersSubentIdPE to reveal its subentities.</p>
<p>The header file of "AcDbAssocPersSubentIdPE" is very well documented by our engineering team. Please read it and that should help in customizing it for your custom entity. Here is the relevant portion of the custom AcDbAssocPersSubentIdPE implementation for "MyEnt1" custom entity from the attached sample project.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// MyEntPersSubentIdPE.cpp</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbAssocPersSubentId* MyEntPersSubentIdPE::</pre>
<pre style="margin:0em;"> 	createNewPersSubent(</pre>
<pre style="margin:0em;"> 					AcDbEntity*                 pEntity,</pre>
<pre style="margin:0em;"> 					AcDbDatabase*               pDatabase,</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbCompoundObjectId&amp; compId,</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbSubentId&amp;         subentId)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">switch</span><span style="color:#000000"> (subentId.index())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  100: </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbAssocEdgePersSubentId(101, 102);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  101: </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbAssocEdgePersSubentId(101, 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  102: </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbAssocEdgePersSubentId(102, 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">default</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\n createNewPersSubent </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				not implemented !!<span style="color:#a31515">&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbAssocSimplePersSubentId(subentId);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus MyEntPersSubentIdPE::</pre>
<pre style="margin:0em;"> 				getTransientSubentIds(</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbEntity* pEntity, </pre>
<pre style="margin:0em;">                 AcDbDatabase*                pDatabase,</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbAssocPersSubentId* pPersSubentId,</pre>
<pre style="margin:0em;">                 AcArray&lt;AcDbSubentId&gt;&amp;       subents) <span style="color:#0000ff">const</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = Acad::eNotImplemented;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	MyEnt1 *pMyEnt1 = MyEnt1::cast(pEntity);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pMyEnt1 != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcDbAssocEdgePersSubentId *pEdgePerSubEntId </pre>
<pre style="margin:0em;"> 			= AcDbAssocEdgePersSubentId::cast(pPersSubentId);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pEdgePerSubEntId != NULL)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">int</span><span style="color:#000000">  index1 = pEdgePerSubEntId-&gt;index1();</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">int</span><span style="color:#000000">  index2 = pEdgePerSubEntId-&gt;index2();</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (index2 == 0)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				subents.append(</pre>
<pre style="margin:0em;"> 					AcDbSubentId(</pre>
<pre style="margin:0em;"> 					AcDb::kVertexSubentType, </pre>
<pre style="margin:0em;"> 					index1));</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				subents.append(</pre>
<pre style="margin:0em;"> 					AcDbSubentId(</pre>
<pre style="margin:0em;"> 					AcDb::kEdgeSubentType, </pre>
<pre style="margin:0em;"> 					100));</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus MyEntPersSubentIdPE::</pre>
<pre style="margin:0em;"> 			getAllSubentities(</pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbEntity* pEntity,</pre>
<pre style="margin:0em;"> 					AcDb::SubentType subentType,</pre>
<pre style="margin:0em;">                     AcArray&lt;AcDbSubentId&gt;&amp; allSubentIds)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = Acad::eNotImplemented;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	MyEnt1 *pMyEnt1 = MyEnt1::cast(pEntity);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pMyEnt1 != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (subentType == AcDb::kEdgeSubentType)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			allSubentIds.append(</pre>
<pre style="margin:0em;"> 				AcDbSubentId(AcDb::kEdgeSubentType, 100));</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000"> (subentType == AcDb::kVertexSubentType)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			allSubentIds.append(</pre>
<pre style="margin:0em;"> 				AcDbSubentId(</pre>
<pre style="margin:0em;"> 				AcDb::kVertexSubentType, 101));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			allSubentIds.append(</pre>
<pre style="margin:0em;"> 				AcDbSubentId(</pre>
<pre style="margin:0em;"> 				AcDb::kVertexSubentType, 102));</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus MyEntPersSubentIdPE::</pre>
<pre style="margin:0em;"> 				getEdgeVertexSubentities(</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbEntity*      pEntity,</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbSubentId&amp;    edgeSubentId,</pre>
<pre style="margin:0em;"> 				AcDbSubentId&amp;          startVertexSubentId,</pre>
<pre style="margin:0em;">                 AcDbSubentId&amp;          endVertexSubentId,</pre>
<pre style="margin:0em;"> 				AcArray&lt;AcDbSubentId&gt;&amp; otherVertexSubentIds)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = Acad::eNotImplemented;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	MyEnt1 *pMyEnt1 = MyEnt1::cast(pEntity);</pre>
<pre style="margin:0em;"> 	Adesk::GsMarker gsMarker = edgeSubentId.index();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">switch</span><span style="color:#000000"> (gsMarker)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  100:</pre>
<pre style="margin:0em;"> 			startVertexSubentId = AcDbSubentId(</pre>
<pre style="margin:0em;"> 				AcDb::kVertexSubentType, 101);</pre>
<pre style="margin:0em;"> 			endVertexSubentId = AcDbSubentId(</pre>
<pre style="margin:0em;"> 				AcDb::kVertexSubentType, 102);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">default</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(ACRX_T(</pre>
<pre style="margin:0em;"> 				<span style="color:#a31515">&quot;\\n getEdgeVertexSubentities </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				not implemented !!<span style="color:#a31515">&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus MyEntPersSubentIdPE::</pre>
<pre style="margin:0em;"> 				getVertexSubentityGeometry(</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbEntity*   pEntity,</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbSubentId&amp; vertexSubentId,</pre>
<pre style="margin:0em;">                 AcGePoint3d&amp;        vertexPosition)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es =  Acad::eNotImplemented;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	MyEnt1 *pMyEnt1 = MyEnt1::cast(pEntity);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	Adesk::GsMarker gsMarker = vertexSubentId.index();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">switch</span><span style="color:#000000"> (gsMarker)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  100:</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\n </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				getVertexSubentityGeometry </pre>
<pre style="margin:0em;"> 				not implemented <span style="color:#0000ff">for</span><span style="color:#000000">  %d !!<span style="color:#a31515">&quot;), gsMarker);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			vertexPosition = pMyEnt1-&gt;m_ptEP;</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  101:</pre>
<pre style="margin:0em;"> 			vertexPosition = pMyEnt1-&gt;m_ptSP;</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  102:</pre>
<pre style="margin:0em;"> 			vertexPosition = pMyEnt1-&gt;m_ptEP;</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  0: </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">default</span><span style="color:#000000">  :</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\n </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				getVertexSubentityGeometry </pre>
<pre style="margin:0em;"> 				not implemented !!<span style="color:#a31515">&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus MyEntPersSubentIdPE::</pre>
<pre style="margin:0em;"> 			getEdgeSubentityGeometry(</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbEntity*   pEntity,</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">const</span><span style="color:#000000">  AcDbSubentId&amp; edgeSubentId,</pre>
<pre style="margin:0em;">                 AcGeCurve3d*&amp;       pEdgeCurve)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es =  Acad::eNotImplemented;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	MyEnt1 *pMyEnt1 = MyEnt1::cast(pEntity);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">switch</span><span style="color:#000000"> (edgeSubentId.index())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  100:</pre>
<pre style="margin:0em;"> 			pEdgeCurve = <span style="color:#0000ff">new</span><span style="color:#000000">  AcGeLine3d(</pre>
<pre style="margin:0em;"> 				pMyEnt1-&gt;m_ptSP, pMyEnt1-&gt;m_ptEP);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">default</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\n </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				getEdgeSubentityGeometry </pre>
<pre style="margin:0em;"> 				not implemented <span style="color:#0000ff">for</span><span style="color:#000000">  %d!!<span style="color:#a31515">&quot;), </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				edgeSubentId.index());</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is the project for download :</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0e69fa3970c img-responsive"><a href="http://adndevblog.typepad.com/files/customentity_assocdimenabled-1.zip">Download Customentity_assocdimEnabled</a></span>
