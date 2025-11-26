---
layout: "post"
title: "Importing AutoCAD solids to Three.js"
date: "2015-01-05 09:33:13"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "JavaScript"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/importing-autocad-solids-to-threejs.html "
typepad_basename: "importing-autocad-solids-to-threejs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently I have been looking at using <a href="http://chandlerprall.github.io/Physijs/">PhysiJS</a> for collision detection in AutoCAD. To get started with it, AutoCAD solids had to be imported in a Three.js scene. The following nice blog posts in Kean's blog provided the required head start.
</p>
<p>
<a href ="http://through-the-interface.typepad.com/through_the_interface/2014/05/javascript-in-autocad-viewing-3d-solids-using-threejs.html">Viewing 3d solids using Three.js</a>
</p>
<p>
<a href ="http://through-the-interface.typepad.com/through_the_interface/2014/10/connecting-threejs-to-an-autocad-model-part-1.html">Connecting Three.js to an AutoCAD model - Part I</a>
</p>
<p>
<a href ="http://through-the-interface.typepad.com/through_the_interface/2014/10/connecting-threejs-to-an-autocad-model-part-2.html">Connecting Three.js to an AutoCAD model - Part II</a>
</p>
<p>
In these blog posts, the geometric extents of the AutoCAD solids are displayed as bounding boxes in Three.js. To include Physics, it was required to get the actual shape of the solid, so a few changes were needed.
</p>
<p>
In this blog post, I will only hightlight the changes to import AutoCAD solids into Three.js. I will include the PhysiJS portion of the code in my next blog post. This is to keep the Three.js and PhysiJS portions separate.
</p>
<p>
Here are the changes to the code from "Connecting Three.js to an AutoCAD model - Part II" :
</p>
<p>
Changes to Utils.cs
</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">internal</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  MeshData</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">double</span><span style="color:#000000">  _dist;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  string _handle;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  Extents3d _exts;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  Point3dCollection _vertices;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  Int32Collection _faces;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  string _color;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  MeshData()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         _dist = 1.0;</pre>
<pre style="margin:0em;">         _handle = String.Empty;</pre>
<pre style="margin:0em;">         _exts = <span style="color:#0000ff">new</span><span style="color:#000000">  Extents3d();</pre>
<pre style="margin:0em;">         _vertices = <span style="color:#0000ff">new</span><span style="color:#000000">  Point3dCollection();</pre>
<pre style="margin:0em;">         _faces = <span style="color:#0000ff">new</span><span style="color:#000000">  Int32Collection();</pre>
<pre style="margin:0em;">         _color = String.Empty;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  MeshData(</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">   dist, </pre>
<pre style="margin:0em;"> 		string handle, </pre>
<pre style="margin:0em;"> 		Extents3d exts, </pre>
<pre style="margin:0em;"> 		Point3dCollection vertices, </pre>
<pre style="margin:0em;"> 		Int32Collection faces, </pre>
<pre style="margin:0em;"> 		string color)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         _dist = dist;</pre>
<pre style="margin:0em;">         _handle = handle;</pre>
<pre style="margin:0em;">         _exts = exts;</pre>
<pre style="margin:0em;">         _vertices = vertices;</pre>
<pre style="margin:0em;">         _faces = faces;</pre>
<pre style="margin:0em;">         _color = color;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">using</span><span style="color:#000000">  Autodesk.AutoCAD.Geometry;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">internal</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  List&lt;MeshData&gt; SolidInfoForCollection(</pre>
<pre style="margin:0em;"> 	Document doc, </pre>
<pre style="margin:0em;"> 	Point3d camPos, </pre>
<pre style="margin:0em;"> 	ObjectIdCollection ids)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     var sols = <span style="color:#0000ff">new</span><span style="color:#000000">  List&lt;MeshData&gt;();</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (var tr </pre>
<pre style="margin:0em;"> 		= doc.TransactionManager.StartOpenCloseTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         foreach (ObjectId id in ids)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Entity ent = tr.GetObject(</pre>
<pre style="margin:0em;"> 				id, </pre>
<pre style="margin:0em;"> 				OpenMode.ForRead) as Entity;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Entity handle to name the Three.js objects</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             String hand = ent.Handle.ToString();</pre>
<pre style="margin:0em;">             Autodesk.AutoCAD.Colors.EntityColor clr </pre>
<pre style="margin:0em;"> 										= ent.EntityColor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Entity color to use for the Three.js objects</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">long</span><span style="color:#000000">  b, g, r;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (ent.ColorIndex &lt; 256)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 System.Byte byt </pre>
<pre style="margin:0em;"> 					= System.Convert.ToByte(ent.ColorIndex);</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  rgb = EntityColor.LookUpRgb(byt);</pre>
<pre style="margin:0em;">                 b = (rgb &amp; 0xffL);</pre>
<pre style="margin:0em;">                 g = (rgb &amp; 0xff00L) &gt;&gt; 8;</pre>
<pre style="margin:0em;">                 r = rgb &gt;&gt; 16;</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 b = 127;</pre>
<pre style="margin:0em;">                 g = 127;</pre>
<pre style="margin:0em;">                 r = 127;</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             String entColor </pre>
<pre style="margin:0em;"> 			= <span style="color:#a31515">&quot;0x&quot;</span><span style="color:#000000">  </pre>
<pre style="margin:0em;"> 			+ String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0:X2<span style="color:#000000">}</span><span style="color:#000000">{</span>1:X2<span style="color:#000000">}</span><span style="color:#000000">{</span>2:X2<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , r, g, b);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Entity extents</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             Extents3d ext = ent.GeometricExtents;</pre>
<pre style="margin:0em;">             </pre>
<pre style="margin:0em;"> 			var tmp = ext.MinPoint + </pre>
<pre style="margin:0em;"> 				0.5 * (ext.MaxPoint - ext.MinPoint);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Vector3d dir = ext.MaxPoint - ext.MinPoint;</pre>
<pre style="margin:0em;">             var mid = </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">new</span><span style="color:#000000">  Point3d(ext.MinPoint.X, tmp.Y, tmp.Z);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             var dist = camPos.DistanceTo(mid);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (id.ObjectClass.Name.Equals(<span style="color:#a31515">&quot;AcDbSubDMesh&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#008000">// Already a Mesh. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				<span style="color:#008000">//Get the face info and clean it up</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 <span style="color:#008000">// a bit to export it as a THREE.Face3 </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				<span style="color:#008000">// which only has three vertices</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 var mesh = ent as SubDMesh;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Point3dCollection threeVertices </pre>
<pre style="margin:0em;"> 						= <span style="color:#0000ff">new</span><span style="color:#000000">  Point3dCollection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Int32Collection threeFaceInfo </pre>
<pre style="margin:0em;"> 					= <span style="color:#0000ff">new</span><span style="color:#000000">  Int32Collection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Point3dCollection vertices = mesh.Vertices;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000"> [] faceArr = mesh.FaceArray.ToArray();</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000"> [] edgeArr = mesh.EdgeArray.ToArray();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Int32Collection faceVertices </pre>
<pre style="margin:0em;"> 								= <span style="color:#0000ff">new</span><span style="color:#000000">  Int32Collection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  verticesInFace = 0;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  facecnt = 0;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  x = 0; </pre>
<pre style="margin:0em;"> 					x &lt; faceArr.Length; facecnt++, </pre>
<pre style="margin:0em;"> 					x = x + verticesInFace + 1)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     faceVertices.Clear();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     verticesInFace = faceArr[x];</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  y = x + 1; </pre>
<pre style="margin:0em;"> 						y &lt;= x + verticesInFace; y++)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         faceVertices.Add(faceArr[y]);</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// Merging of mesh faces can result in </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					<span style="color:#008000">// faces with multiple vertices</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#008000">// A simple collinearity check can help </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					<span style="color:#008000">// remove those redundant vertices</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">bool</span><span style="color:#000000">  continueCollinearCheck = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">do</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         continueCollinearCheck = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  index = 0; </pre>
<pre style="margin:0em;"> 							index &lt; faceVertices.Count; </pre>
<pre style="margin:0em;"> 							index++)</pre>
<pre style="margin:0em;">                         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                             <span style="color:#0000ff">int</span><span style="color:#000000">  v1 = index;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             <span style="color:#0000ff">int</span><span style="color:#000000">  v2 = </pre>
<pre style="margin:0em;"> 								(index + 1) &gt;= faceVertices.Count ?</pre>
<pre style="margin:0em;"> 								(index + 1) - faceVertices.Count : </pre>
<pre style="margin:0em;"> 								index + 1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             <span style="color:#0000ff">int</span><span style="color:#000000">  v3 = </pre>
<pre style="margin:0em;"> 								(index + 2) &gt;= faceVertices.Count ?</pre>
<pre style="margin:0em;"> 								(index + 2) - faceVertices.Count : </pre>
<pre style="margin:0em;"> 								index + 2;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             <span style="color:#008000">// Check collinear</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                             Point3d p1 </pre>
<pre style="margin:0em;"> 								= vertices[faceVertices[v1]];</pre>
<pre style="margin:0em;">                             Point3d p2 </pre>
<pre style="margin:0em;"> 								= vertices[faceVertices[v2]];</pre>
<pre style="margin:0em;">                             Point3d p3 </pre>
<pre style="margin:0em;"> 								= vertices[faceVertices[v3]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                             Vector3d vec1 = p1 - p2;</pre>
<pre style="margin:0em;">                             Vector3d vec2 = p2 - p3;</pre>
<pre style="margin:0em;">                             <span style="color:#0000ff">if</span><span style="color:#000000">  (vec1.IsCodirectionalTo(vec2))</pre>
<pre style="margin:0em;">                             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                                 faceVertices.RemoveAt(v2);</pre>
<pre style="margin:0em;">                                 continueCollinearCheck = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                                 <span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span> <span style="color:#0000ff">while</span><span style="color:#000000">  (continueCollinearCheck);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">if</span><span style="color:#000000">  (faceVertices.Count == 3)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         Point3d p1 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[0]];</pre>
<pre style="margin:0em;">                         Point3d p2 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[1]];</pre>
<pre style="margin:0em;">                         Point3d p3 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[2]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p1))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p1);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p2))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p2);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p2));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p3))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p3);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p3));</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  (faceVertices.Count == 4)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span> <span style="color:#008000">// A face with 4 vertices, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						<span style="color:#008000">// lets split it to </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                         <span style="color:#008000">// make it easier later to </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						<span style="color:#008000">// create a THREE.Face3 </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                         Point3d p1 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[0]];</pre>
<pre style="margin:0em;">                         Point3d p2 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[1]];</pre>
<pre style="margin:0em;">                         Point3d p3 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[2]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p1))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p1);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p2))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p2);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p2));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p3))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p3);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p3));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         p1 = vertices[faceVertices[2]];</pre>
<pre style="margin:0em;">                         p2 = vertices[faceVertices[3]];</pre>
<pre style="margin:0em;">                         p3 = vertices[faceVertices[0]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p1))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p1);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p2))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p2);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p2));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p3))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p3);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p3));</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         Application.DocumentManager.MdiActiveDocument.</pre>
<pre style="margin:0em;"> 							Editor.WriteMessage(</pre>
<pre style="margin:0em;"> 							<span style="color:#a31515">&quot;Face with more than 4 vertices will need triangulation to import in Three.js &quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 sols.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  MeshData(</pre>
<pre style="margin:0em;"> 					dist, </pre>
<pre style="margin:0em;"> 					hand, </pre>
<pre style="margin:0em;"> 					ext, </pre>
<pre style="margin:0em;"> 					threeVertices, </pre>
<pre style="margin:0em;"> 					threeFaceInfo, </pre>
<pre style="margin:0em;"> 					entColor));</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000"> (id.ObjectClass.Name.Equals(<span style="color:#a31515">&quot;AcDb3dSolid&quot;</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 <span style="color:#008000">// Mesh the solid to export to Three.js</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 Autodesk.AutoCAD.DatabaseServices.Solid3d sld </pre>
<pre style="margin:0em;"> 					= tr.GetObject(</pre>
<pre style="margin:0em;"> 					id, OpenMode.ForRead) </pre>
<pre style="margin:0em;"> 					as Autodesk.AutoCAD.DatabaseServices.Solid3d;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 MeshFaceterData mfd = <span style="color:#0000ff">new</span><span style="color:#000000">  MeshFaceterData();</pre>
<pre style="margin:0em;">                 <span style="color:#008000">// Only triangles</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 mfd.FaceterMeshType = 2; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#008000">// May need change based on </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				<span style="color:#008000">// how granular we want the mesh to be.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 mfd.FaceterMaxEdgeLength = dir.Length * 0.025;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 MeshDataCollection md </pre>
<pre style="margin:0em;"> 					= SubDMesh.GetObjectMesh(sld, mfd);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Point3dCollection threeVertices </pre>
<pre style="margin:0em;"> 					= <span style="color:#0000ff">new</span><span style="color:#000000">  Point3dCollection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 nt32Collection threeFaceInfo </pre>
<pre style="margin:0em;"> 					= <span style="color:#0000ff">new</span><span style="color:#000000">  Int32Collection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Point3dCollection vertices = md.VertexArray;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000"> [] faceArr = md.FaceArray.ToArray();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 Int32Collection faceVertices </pre>
<pre style="margin:0em;"> 					= <span style="color:#0000ff">new</span><span style="color:#000000">  Int32Collection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  verticesInFace = 0;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  facecnt = 0;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  x = 0; </pre>
<pre style="margin:0em;"> 					x &lt; faceArr.Length; facecnt++, </pre>
<pre style="margin:0em;"> 					x = x + verticesInFace + 1)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     faceVertices.Clear();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     verticesInFace = faceArr[x];</pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  y = x + 1; </pre>
<pre style="margin:0em;"> 						y &lt;= x + verticesInFace; </pre>
<pre style="margin:0em;"> 						y++)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         faceVertices.Add(faceArr[y]);</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                     <span style="color:#0000ff">if</span><span style="color:#000000">  (faceVertices.Count == 3)</pre>
<pre style="margin:0em;">                     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                         Point3d p1 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[0]];</pre>
<pre style="margin:0em;">                         Point3d p2 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[1]];</pre>
<pre style="margin:0em;">                         Point3d p3 </pre>
<pre style="margin:0em;"> 							= vertices[faceVertices[2]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p1))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p1);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p2))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p2);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p2));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">if</span><span style="color:#000000">  (!threeVertices.Contains(p3))</pre>
<pre style="margin:0em;">                             threeVertices.Add(p3);</pre>
<pre style="margin:0em;">                         threeFaceInfo.Add(</pre>
<pre style="margin:0em;"> 							threeVertices.IndexOf(p3));</pre>
<pre style="margin:0em;">                     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 sols.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  MeshData(</pre>
<pre style="margin:0em;"> 					dist, </pre>
<pre style="margin:0em;"> 					hand, </pre>
<pre style="margin:0em;"> 					ext, </pre>
<pre style="margin:0em;"> 					threeVertices, </pre>
<pre style="margin:0em;"> 					threeFaceInfo, </pre>
<pre style="margin:0em;"> 					entColor));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 tr.Commit();</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  sols;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Helper function to build a JSON string containing a</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// sorted extents list</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">internal</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  string SolidsString(</pre>
<pre style="margin:0em;">     List&lt;MeshData&gt; lst)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     var sb = <span style="color:#0000ff">new</span><span style="color:#000000">  StringBuilder(</pre>
<pre style="margin:0em;"> 		<span style="color:#a31515">&quot;<span style="color:#000000">{</span>\\&quot;retCode\\&quot;:0, \\&quot;result\\&quot;:[&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;">     var first = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     foreach (var data in lst)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (!first)</pre>
<pre style="margin:0em;">         sb.Append(<span style="color:#a31515">&quot;,&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;">     first = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     var hand = data._handle;</pre>
<pre style="margin:0em;">     var ext = data._exts;</pre>
<pre style="margin:0em;">     var vertices = data._vertices;</pre>
<pre style="margin:0em;">     var faces = data._faces;</pre>
<pre style="margin:0em;">     var color = data._color;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     sb.AppendFormat(</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;<span style="color:#000000">{</span><span style="color:#000000">{</span>\\&quot;min\\&quot;:<span style="color:#000000">{</span>0<span style="color:#000000">}</span>,\\&quot;max\\&quot;:<span style="color:#000000">{</span>1<span style="color:#000000">}</span>,</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		\\<span style="color:#a31515">&quot;handle\\&quot;:\\&quot;<span style="color:#000000">{</span>2<span style="color:#000000">}</span>\\&quot;,\\&quot;vertices\\&quot;:<span style="color:#000000">{</span>3<span style="color:#000000">}</span>,</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		\\<span style="color:#a31515">&quot;faces\\&quot;:<span style="color:#000000">{</span>4<span style="color:#000000">}</span>,\\&quot;color\\&quot;:\\&quot;<span style="color:#000000">{</span>5<span style="color:#000000">}</span>\\&quot;<span style="color:#000000">}</span><span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         JsonConvert.SerializeObject(ext.MinPoint),</pre>
<pre style="margin:0em;">         JsonConvert.SerializeObject(ext.MaxPoint),</pre>
<pre style="margin:0em;">         hand,</pre>
<pre style="margin:0em;">         JsonConvert.SerializeObject(vertices),</pre>
<pre style="margin:0em;">         JsonConvert.SerializeObject(faces),</pre>
<pre style="margin:0em;">         color</pre>
<pre style="margin:0em;">     );</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     sb.Append(<span style="color:#a31515">&quot;]<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  sb.ToString();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
</p>
<p>Changes to threesolids2.js</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> function addSolidsToPalette(args) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   var obj = JSON.parse(args);</pre>
<pre style="margin:0em;">   var sols = obj.result;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   var needRender = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">   <span style="color:#0000ff">if</span><span style="color:#000000">  (sols != null) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">for</span><span style="color:#000000">  (var sol in sols) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">       var s = sols[sol];</pre>
<pre style="margin:0em;">       var obj = root.getObjectByName(s.handle, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">       <span style="color:#0000ff">if</span><span style="color:#000000">  (obj == null) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         var verticesarray = s.vertices;</pre>
<pre style="margin:0em;">         var facesarray = s.faces;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (verticesarray.length != 0) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             createMesh(s);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         root.add(box);</pre>
<pre style="margin:0em;">         needRender = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">       <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">   <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">   <span style="color:#0000ff">if</span><span style="color:#000000">  (needRender) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     render();</pre>
<pre style="margin:0em;">   <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> function refresh() <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#008000">// Clear any previous geometry</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#0000ff">if</span><span style="color:#000000">  (root != null) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     scene.remove(root);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">delete</span><span style="color:#000000">  root;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Get the geometry info from AutoCAD again</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     sols = getThreeSolidsFromAutoCAD();</pre>
<pre style="margin:0em;">   <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#008000">// Create the materials</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   var dark = </pre>
<pre style="margin:0em;"> 	  <span style="color:#0000ff">new</span><span style="color:#000000">  MeshLambertMaterial(<span style="color:#000000">{</span> color: 0x000000 <span style="color:#000000">}</span>);</pre>
<pre style="margin:0em;">   light </pre>
<pre style="margin:0em;"> 	  = <span style="color:#0000ff">new</span><span style="color:#000000">  MeshLambertMaterial(<span style="color:#000000">{</span> color: 0xFFFFFF <span style="color:#000000">}</span>);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#008000">// Create our root object</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   boxGeom =</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">new</span><span style="color:#000000">  BoxGeometry(rootDim, rootDim, </pre>
<pre style="margin:0em;"> 	rootDim, segs, segs, segs);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#008000">// Create the mesh from the geometry</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   root = <span style="color:#0000ff">new</span><span style="color:#000000">  Mesh(boxGeom, dark);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   scene.add(root);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#0000ff">for</span><span style="color:#000000">  (var sol in sols) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     var s = sols[sol];</pre>
<pre style="margin:0em;">     var verticesarray = s.vertices;</pre>
<pre style="margin:0em;">     var facesarray = s.faces;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (verticesarray.length != 0) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         var mesh = createMesh(s);</pre>
<pre style="margin:0em;"> 		root.add(mesh);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">   <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   <span style="color:#008000">// Draw!</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">   renderer.render(scene, camera);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> function createMesh(s) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     var verticesarray = s.vertices;</pre>
<pre style="margin:0em;">     var vertices = [];</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (verticesarray != null) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         var numOfVertices = verticesarray.length;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">for</span><span style="color:#000000">  (var v = 0; v &lt; numOfVertices; v++) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             var vertex = verticesarray[v];</pre>
<pre style="margin:0em;">             vertices.push(</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">new</span><span style="color:#000000">  THREE.Vector3(</pre>
<pre style="margin:0em;"> 				vertex.X, vertex.Y, vertex.Z));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     var facesarray = s.faces;</pre>
<pre style="margin:0em;">     var faces = [];</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (facesarray != null) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         var numOfFaces = facesarray.length / 3;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">for</span><span style="color:#000000">  (var f = 0; f &lt; numOfFaces; f = f + 1) <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             faces.push(</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">new</span><span style="color:#000000">  THREE.Face3(</pre>
<pre style="margin:0em;"> 				facesarray[f * 3], </pre>
<pre style="margin:0em;"> 				facesarray[f * 3 + 1], </pre>
<pre style="margin:0em;"> 				facesarray[f * 3 + 2]));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     var geom = <span style="color:#0000ff">new</span><span style="color:#000000">  THREE.Geometry();</pre>
<pre style="margin:0em;">     geom.vertices = vertices;</pre>
<pre style="margin:0em;">     geom.faces = faces;</pre>
<pre style="margin:0em;">     geom.mergeVertices();</pre>
<pre style="margin:0em;">     geom.computeFaceNormals();</pre>
<pre style="margin:0em;">     geom.computeVertexNormals();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     var material = </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">new</span><span style="color:#000000">  THREE.MeshLambertMaterial(</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span> color: 0x00FF00, shading: THREE.FlatShading <span style="color:#000000">}</span>);</pre>
<pre style="margin:0em;">     material.color.setHex(parseInt(s.color, 16));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     var threemesh = <span style="color:#0000ff">new</span><span style="color:#000000">  THREE.Mesh(geom, material);</pre>
<pre style="margin:0em;">     threemesh.name = s.handle;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  threemesh;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
</p>
<p>Here is a screenshot of the AutoCAD solids imported into Three.js :</p>
<p><a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c72ebc76970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c72ebc76970b img-responsive" alt="ThreeJSPalette" title="ThreeJSPalette" src="/assets/image_759296.jpg" style="margin: 0px 5px 5px 0px;" /></a></p>
<p>The modified files can be downloaded here :</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2bc4a970d img-responsive"><a href="http://adndevblog.typepad.com/files/utils.cs">Download Utils.cs</a></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07d2bc7b970d img-responsive"><a href="http://adndevblog.typepad.com/files/threesolids2.js">Download Threesolids2.js</a></span></p>
