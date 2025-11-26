---
layout: "post"
title: "Working with vertices, edges and faces of a SubDMesh"
date: "2012-04-20 06:21:51"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/working-with-vertices-edges-and-faces-of-a-subdmesh.html "
typepad_basename: "working-with-vertices-edges-and-faces-of-a-subdmesh"
typepad_status: "Publish"
---

<div>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></div>
<div>The &quot;SubDMesh&quot; does provide properties such as &quot;Vertices&quot;, &quot;EdgeArray&quot; and &quot;FaceArray&quot;, to retrieve the vertices, edges and faces that form the mesh. To make use of this information, it is important to understand the way the vertices, edges and faces are obtained from these methods.</div>
<div>In the &quot;FaceArray&quot;, the number of vertices in a face is the first to appear in the array, which is then followed by the indices of the edges that make up the face.</div>
<div>The &quot;EdgeArray&quot; simply returns the indices of the vertices that make each edge.</div>
<div>The &quot;Vertices&quot; are point coordinates of each vertex in the mesh.</div>
<div>For more details on the layout of the face array, please refer to the ObjectARX documentation under the “AcGiGeometry::shell” topic.</div>
<div>Here is a sample code that prints the edges that make the faces and the vertices that make the edges.</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;SubDMeshTest&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SubDMeshTest()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> activeDoc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = activeDoc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = activeDoc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">[] values ={</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">DxfCode</span><span style="line-height: 140%;">.Start,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;MESH&quot;</span><span style="line-height: 140%;">)};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">SelectionFilter</span><span style="line-height: 140%;"> filter = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SelectionFilter</span><span style="line-height: 140%;">(values);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptSelectionResult</span><span style="line-height: 140%;"> psr = ed.SelectAll(filter);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">SelectionSet</span><span style="line-height: 140%;"> ss = psr.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ss == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; ss.Count; ++i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">SubDMesh</span><span style="line-height: 140%;"> mesh =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; trans.GetObject(ss[i].ObjectId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SubDMesh</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertices : {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; mesh.NumberOfVertices));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Edges&#0160;&#0160;&#0160; : {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; mesh.NumberOfEdges));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Faces&#0160;&#0160;&#0160; : {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160; mesh.NumberOfFaces));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Get the Face information</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">[] faceArr = mesh.FaceArray.ToArray();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> edges = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> fcount = 0;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> x = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; x &lt; faceArr.Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; x = x + edges + 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Face {0} : &quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; fcount++));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; edges = faceArr[x];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> y = x + 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; y &lt;= x + edges;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; y++</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n\t Edge - {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; faceArr[y]));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Get the Edge information</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ecount = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">[] edgeArr = mesh.EdgeArray.ToArray();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> x = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; x &lt; edgeArr.Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; x = x + 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Edge {0} : &quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ecount++));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertex - {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; edgeArr[x]));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertex - {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; edgeArr[x + 1]));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// Get the vertices information</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> vcount = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> vertex </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> mesh.Vertices)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertex {0} - {1} {2}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; vcount++,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; vertex.X,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; vertex.Y));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160;&#0160;</span><span style="line-height: 140%;">&#0160;</span><span style="color: green; line-height: 140%;">//Here is the output for very simple SubDMesh :</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Vertices : 9</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edges&#0160;&#0160;&#0160; : 12</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Faces&#0160;&#0160;&#0160; : 4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Face</span><span style="line-height: 140%;"> 0 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Face</span><span style="line-height: 140%;"> 1 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Face</span><span style="line-height: 140%;"> 2 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 6</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 7</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Face</span><span style="line-height: 140%;"> 3 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 7</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 8</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Edge - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 0 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 1 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 2 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 3 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 4 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 5 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 6 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 7 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 6</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 8 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 6</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 7</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 9 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 7</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 10 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 7</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 8</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;Edge 11 :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> - 8</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 0 - 0 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 1 - 0.5 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 2 - 0.5 0.5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 3 - 0 0.5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 4 - 1 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 5 - 1 0.5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 6 - 1 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 7 - 0.5 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: #2b91af; line-height: 140%;">Vertex</span><span style="line-height: 140%;"> 8 - 0 1</span></p>
</div>
