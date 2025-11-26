---
layout: "post"
title: "Implementing copy multiple entities with a twist :)"
date: "2015-04-28 02:25:48"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/implementing-copy-multiple-entities-with-a-twist-.html "
typepad_basename: "implementing-copy-multiple-entities-with-a-twist-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In a recent query, a developer mentioned about a behavior of the AutoCAD's COPYcommand which I thought was a little different from how some of the other commands behaved. In the COPY command when multiple entities are selected for copying and after a base point is chosen, AutoCAD places copies of the selected entities as expected. But when the Enter key is hit, we would usually want the command to terminate like most other AutoCAD commands do. But in case of the COPY command, hitting the Enter key is treated as a "use first point as displacement" and a copy is placed before terminating the command.&nbsp;</p>
<p>If you do not want this behavior, you may need to implement the copy multiple entities command which is simple to do. Here is a sample code that implements it using ObjectARX API and exposes it as Lisp callable. Additionally, while dragging the entities the position is constrained along X axis just as the .yz coordinate filter would do with the native COPY command.</p>
<p>Here is the code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Global, so it can be used in the</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// DragGen callback function...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcGePoint3d basePt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Here is the callback...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  dragGenCallback(ads_point pt,ads_matrix mt)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">      <span style="color:#008000">//AcGeVector3d vec(pt[0]-basePt[0], </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	 <span style="color:#008000">// pt[1]-basePt[1], pt[2]-basePt[2]);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	 <span style="color:#008000">// To restrict dragging movement to X axis alone</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	 AcGeVector3d vec(pt[0]-basePt[0], 0, 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">      AcGeMatrix3d mat;</pre>
<pre style="margin:0em;">      <span style="color:#008000">// Set our matrix to translate the mouse </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	 <span style="color:#008000">// movements...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">      mat.setToTranslation(vec);</pre>
<pre style="margin:0em;">      <span style="color:#008000">// And place the results in mt</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">      <span style="color:#0000ff">for</span><span style="color:#000000"> (<span style="color:#0000ff">int</span><span style="color:#000000">  c=0;c&lt;4;c++)</pre>
<pre style="margin:0em;">      <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">           <span style="color:#0000ff">for</span><span style="color:#000000"> (<span style="color:#0000ff">int</span><span style="color:#000000">  cd=0;cd&lt;4;cd++)</pre>
<pre style="margin:0em;">           mt[c][cd]=mat(c,cd);</pre>
<pre style="margin:0em;">      <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">      <span style="color:#0000ff">return</span><span style="color:#000000">  RTNORM;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  CopyMultipleEntitiesFunc()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	 ads_name ss;   </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">      <span style="color:#008000">// Get the Selection Set</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">      acedSSGet(NULL, NULL, NULL, NULL, ss); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">      <span style="color:#008000">// Base Point for copying...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">      acedGetPoint(NULL, </pre>
<pre style="margin:0em;">                   L<span style="color:#a31515">&quot;Specify base point&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                    asDblArray(basePt));</pre>
<pre style="margin:0em;">      <span style="color:#0000ff">long</span><span style="color:#000000">  len;</pre>
<pre style="margin:0em;">      acedSSLength(ss,&amp;len);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> 	 <span style="color:#0000ff">int</span><span style="color:#000000">  ret = 0;</pre>
<pre style="margin:0em;"> 	 <span style="color:#0000ff">do</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//Value of the&#39;s final location </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//after the user finishes</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// dragging the selection set </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGePoint3d final_pt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Call DragGen with the callback function</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		ret = acedDragGen(ss, </pre>
<pre style="margin:0em;"> 					L<span style="color:#a31515">&quot;\\nSpecify second point&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 					0, </pre>
<pre style="margin:0em;"> 					dragGenCallback, </pre>
<pre style="margin:0em;"> 					asDblArray(final_pt));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (ret == RTNONE || ret == RTCAN)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span><span style="color:#008000">// enter key hit or cancelled</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// get the final matirx and do your work </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// such as copy with the value of final_pt </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbDatabase* pDb </pre>
<pre style="margin:0em;"> 			= acdbHostApplicationServices()</pre>
<pre style="margin:0em;"> 			-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> 		AcDbObjectIdArray idArr;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">long</span><span style="color:#000000">  l=0, lNumber=0;</pre>
<pre style="margin:0em;"> 			ads_sslength(ss, &amp;lNumber);</pre>
<pre style="margin:0em;"> 			idArr.setPhysicalLength(lNumber);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">for</span><span style="color:#000000">  (; l&lt;lNumber; ++l)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				ads_name ent;</pre>
<pre style="margin:0em;"> 				ads_ssname(ss, l, ent);</pre>
<pre style="margin:0em;"> 				AcDbObjectId idEnt;</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">if</span><span style="color:#000000">  (Acad::eOk </pre>
<pre style="margin:0em;"> 					== acdbGetObjectId(idEnt,ent))</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					idArr.append(idEnt);</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			AcDbIdMapping idMapping;</pre>
<pre style="margin:0em;"> 			AcDbObjectId idOwner </pre>
<pre style="margin:0em;"> 				=acdbSymUtil()-&gt;blockModelSpaceId (pDb);</pre>
<pre style="margin:0em;"> 			Acad::ErrorStatus es = curDoc()-&gt;database()</pre>
<pre style="margin:0em;"> 				-&gt;deepCloneObjects(idArr, </pre>
<pre style="margin:0em;"> 				idOwner, idMapping);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				AcDbIdMappingIter iter2 (idMapping) ;</pre>
<pre style="margin:0em;"> 				AcDbIdPair idPair2 ;</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">for</span><span style="color:#000000">  ( iter2.start ();</pre>
<pre style="margin:0em;"> 					!iter2.done ();</pre>
<pre style="margin:0em;"> 					iter2.next () ) </pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">if</span><span style="color:#000000">  ( !iter2.getMap (idPair2) )</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">continue</span><span style="color:#000000">  ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">if</span><span style="color:#000000">  ( !idPair2.isCloned () )</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">continue</span><span style="color:#000000">  ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					AcDbObjectId keyId = idPair2.key();</pre>
<pre style="margin:0em;"> 					AcDbObjectId valueId = idPair2.value();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#008000">// Transform the cloned entity based on the</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					<span style="color:#008000">// chosen target point</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#008000">//open the entity for write</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					AcDbEntity *pEnt;</pre>
<pre style="margin:0em;"> 					acdbOpenAcDbEntity(pEnt, </pre>
<pre style="margin:0em;"> 						valueId, AcDb::kForWrite);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					AcGeMatrix3d dispmat </pre>
<pre style="margin:0em;"> 						= AcGeMatrix3d::kIdentity;</pre>
<pre style="margin:0em;"> 					dispmat.setToTranslation(</pre>
<pre style="margin:0em;"> 						AcGeVector3d(final_pt[0]-basePt[0], </pre>
<pre style="margin:0em;"> 						0, 0));</pre>
<pre style="margin:0em;"> 					pEnt-&gt;transformBy(dispmat);</pre>
<pre style="margin:0em;"> 					pEnt-&gt;close();</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	 <span style="color:#000000">}</span><span style="color:#0000ff">while</span><span style="color:#000000"> (TRUE);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	 ads_ssfree(ss);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  0;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">class</span><span style="color:#000000">  CMyTest1App : <span style="color:#0000ff">public</span><span style="color:#000000">  AcRxArxApp </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 	CMyTest1App () : AcRxArxApp () </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  AcRx::AppRetCode On_kInitAppMsg (<span style="color:#0000ff">void</span><span style="color:#000000">  *pkt) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcRx::AppRetCode retCode </pre>
<pre style="margin:0em;"> 			=AcRxArxApp::On_kInitAppMsg (pkt) ;</pre>
<pre style="margin:0em;"> 		acedDefun(_T(<span style="color:#a31515">&quot;CopyMultiple&quot;</span><span style="color:#000000"> ), 1000);</pre>
<pre style="margin:0em;"> 		acedRegFunc(CopyMultipleEntitiesFunc, 1000);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  (retCode) ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  AcRx::AppRetCode On_kUnloadAppMsg (<span style="color:#0000ff">void</span><span style="color:#000000">  *pkt) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcRx::AppRetCode retCode </pre>
<pre style="margin:0em;"> 			=AcRxArxApp::On_kUnloadAppMsg (pkt) ;</pre>
<pre style="margin:0em;"> 		acedUndef(_T(<span style="color:#a31515">&quot;CopyMultiple&quot;</span><span style="color:#000000"> ),1000);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  (retCode) ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  RegisterServerComponents() </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is a recording showing the copy behavior :</p>
<iframe height="200" src="https://screencast.autodesk.com/Embed/Timeline/57ff3eee-1e06-44e5-8290-0beab7bcff56" frameborder="0" width="320" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>
