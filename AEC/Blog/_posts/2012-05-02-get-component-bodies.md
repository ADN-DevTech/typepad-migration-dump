---
layout: "post"
title: "Get component bodies"
date: "2012-05-02 06:11:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Architecture"
  - "OMF"
original_url: "https://adndevblog.typepad.com/aec/2012/05/get-component-bodies.html "
typepad_basename: "get-component-bodies"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Entities like windows, doors can have various sub parts like frame, glass, etc. If you want to get back those parts separately then you can create your own class derived from AecStreamAcad and collect each body there.</p>
<p>In the below sample we also collect the color of each body and then use that to change the color of the mass entity we create from them.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> AecStreamCollectColors : </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> AecStreamAcad</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcArray&lt;Body&gt; bodies;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcArray&lt;AcCmColor&gt; colors;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AecStreamCollectColors(AcDbDatabase* pdb)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; :AecStreamAcad(pdb)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; streamWcs(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp; start, </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp; end)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; streamWcs(Body&amp; body, AecStreamColorToPropsMap* colorToPropsMap)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; bodies.append(body);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; colors.append(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">-&gt;trueColor());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CollectBodiesWithColor()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbDatabase* pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbHostApplicationServices()-&gt;workingDatabase(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// Select an architectural entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AecUiPrEntitySetSingle prEnt(L</span><span style="line-height: 140%; color: #a31515;">"Select an architectural entity"</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; prEnt.addAllowedClass(AecDbGeo::desc()); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(prEnt.go() != AecUiPr::kOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectPointer&lt;AecDbGeo&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pEnt(prEnt.objectId(), AcDb::kForRead); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (Acad::eOk != pEnt.openStatus()) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AecStreamCollectColors stream(pDb);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectId modelViewSetId = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AecDictDispRepSet::getStandardViewSet(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AecDictDispRepSet::standardModelName(), pDb);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectPointer&lt;AecDbDispRepSet&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; viewSet(modelViewSetId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (Acad::eOk != viewSet.openStatus())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; stream.pushViewSet(viewSet.object());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; stream.stream(pEnt.object());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i = 0; i &lt; stream.bodies.length(); i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Body &amp; b = stream.bodies.at(i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(L</span><span style="line-height: 140%; color: #a31515;">"Face count = %d\n"</span><span style="line-height: 140%;">, b.faceCount());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AecDbMassElem *pMass = AecDbMassElem::create(AecMassElem::kBrep);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pMass-&gt;setBody(b);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcCmColor c = stream.colors.at(i);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pMass-&gt;setColor(c);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; es = Aec::addToModelSpaceAndClose(pMass, pDb);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>If you wanted to collect the bodies of the components based on their material then you could use the existing AecStreamCollectMaterials class for that.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CollectBodiesWithMaterial()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbDatabase * pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; acdbHostApplicationServices()-&gt;workingDatabase(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// Select an architectural entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AecUiPrEntitySetSingle prEnt(L</span><span style="color: #a31515; line-height: 140%;">"Select an architectural entity"</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; prEnt.addAllowedClass(AecDbGeo::desc()); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(AecUiPr::kOk != prEnt.go())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectPointer&lt;AecDbGeo&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; pEnt(prEnt.objectId(), AcDb::kForRead); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pEnt.openStatus() != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AecStreamCollectMaterials stream(pDb);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; stream.setCombineBodies(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectId modelViewSetId = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; AecDictDispRepSet::getStandardViewSet(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; AecDictDispRepSet::standardModelName(), pDb);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectPointer&lt;AecDbDispRepSet&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; viewSet(modelViewSetId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (Acad::eOk != viewSet.openStatus())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; stream.pushViewSet(viewSet.object());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; stream.stream(pEnt.object());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; stream.materialCount(); i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; AcDbObjectId id = stream.getMaterialAt(i);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; Body * body;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; stream.getBody(id, body);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">"Face count = %d\n"</span><span style="line-height: 140%;">, body-&gt;faceCount());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; AecDbMassElem *pMass = AecDbMassElem::create(AecMassElem::kBrep);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; pMass-&gt;setBody(*body);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; es = Aec::addToModelSpaceAndClose(pMass, pDb);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
