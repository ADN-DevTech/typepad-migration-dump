---
layout: "post"
title: "Merging coplanar faces of a SubDMesh"
date: "2012-03-26 23:12:27"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/03/merging-coplanar-faces-of-a-subdmesh.html "
typepad_basename: "merging-coplanar-faces-of-a-subdmesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The faces of a SubDMesh can be merged together if they are coplanar using the &quot;mergeFaces&quot; method of the &quot;AcDbSubDMesh&quot; class. Here is sample code to demonstrated this.</p>
<div style="font-family: Courier New; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">// meshObjectId : </span></p>
<p style="margin: 0px;"><span style="color: green;">// ObjectId of the SubDMesh entity whose faces need merging</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Acad::ErrorStatus es;</p>
<p style="margin: 0px;">AcDbSubDMesh *pMesh = NULL;</p>
<p style="margin: 0px;">es = acdbOpenObject(pMesh, meshObjectId, kForWrite);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>(es != Acad::eOk)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">AcDbFullSubentPathArray subentPaths;</p>
<p style="margin: 0px;">es = pMesh-&gt;getSubentPath(-1, kFaceSubentType, subentPaths);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Adesk::Int32 numOfFaces = 0;</p>
<p style="margin: 0px;">pMesh-&gt;numOfFaces(numOfFaces);</p>
<p style="margin: 0px;"><span style="color: blue;">for</span>(<span style="color: blue;">int</span> face=0; face &lt; numOfFaces; face++)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">int</span> faceIndex = 0;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; AcGePlane facePlane1;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; es = pMesh-&gt;getFacePlane(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;subentPaths.at(faceIndex).subentId(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; facePlane1);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; AcGePlane facePlane2;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; es = pMesh-&gt;getFacePlane(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; subentPaths.at(faceIndex+1).subentId(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; facePlane2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span>(facePlane1.isCoplanarTo(facePlane2) == Adesk::kTrue)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AcDbFullSubentPathArray subentPathsToMerge;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; subentPathsToMerge.append(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; subentPaths.at(faceIndex));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; subentPathsToMerge.append(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; subentPaths.at(faceIndex+1));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; es = pMesh-&gt;mergeFaces(subentPathsToMerge);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span>(es == Acad::eOk)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; acutPrintf(ACRX_T(<span style="color: #a31515;">&quot;\n2 faces merged.&quot;</span>));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; acutPrintf(ACRX_T(<span style="color: #a31515;">&quot;\nError merging the faces.&quot;</span>));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; acutPrintf(ACRX_T(<span style="color: #a31515;">&quot;\nFaces are non-coplanar.&quot;</span>));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">pMesh-&gt;close();</p>
</div>
