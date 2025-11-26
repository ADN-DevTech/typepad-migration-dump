---
layout: "post"
title: "Retrieving Detailed Wall Layer Geometry"
date: "2011-10-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Algorithm"
  - "Data Access"
  - "Element Creation"
  - "Geometry"
  - "Parts"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/10/retrieving-detailed-wall-layer-geometry.html "
typepad_basename: "retrieving-detailed-wall-layer-geometry"
typepad_status: "Publish"
---

<p>We start off this week with a bang. I find the following topic especially exciting, for several reasons:</p>
<ul>
<li>It deals with geometry. I like that. </li>
<li>It implements something that cannot be achieved out of the box. I like that. </li>
<li>It makes use of the Revit 2012 parts functionality and the PartUtils class, which we never looked at in detail before. I like that. </li>
<li>It uses a temporary transaction to make changes to the model to create and extract information that would otherwise not be available, and then discards the changes to the model. I like that. </li>
</ul>
<p>I hope you like it too!</p>
<p>Starting off this week with something special is good in several ways, because I will be leaving for a ten-day vacation in southern Spain on Wednesday. I like that too, by the way. I hope this helps keep you occupied while I am gone :-)</p>
<p>Here goes:</p>
<p>It is easy to determine the wall layer locations and their corner points individually for unconnected walls, as I demonstrated by analysing and drawing model lines to represent the <a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-compound-layers.html">wall compound layers</a> way back in 2008. Some additional information on working with wall layers was provided when discussing <a href="http://thebuildingcoder.typepad.com/blog/2009/02/compound-wall-layer-volumes.html">compound wall layer volumes</a> and the <a href="http://thebuildingcoder.typepad.com/blog/2009/06/core-structural-layer.html">core structural layer</a>.</p>
<p>However, it is a bit harder to determine the exact wall layer corners for connected walls, where the join type affects the layer geometry, e.g. for &#39;abut&#39; or &#39;miter&#39;.</p>
<p>How can we obtain the wall layer corner points in plan view taking the wall join into account?</p>
<p>This is a pretty advanced question on detailed geometry access which was raised by Marcelo Quevedo of <a href="http://www.hsb-cad.com">hsbSOFT</a>.</p>
<p>Solving this task is not achievable using the functionality provided by the Revit API out of the box, but can be addressed by making use of two interesting tricks:</p>
<ul>
<li>Splitting the wall into parts, which automatically take the wall layers into account. </li>
<li>Embedding the split into a temporary transaction so that it can be rolled back without affecting the model afterwards. </li>
</ul>
<p>This provides a welcome first opportunity to present a practical use of the Revit 2012 parts functionality and the PartUtils class providing API access to it.</p>
<p>The technique of using a temporary transaction for geometry analysis purposes was originally suggested by Scott Conover of the Revit API development team and already mentioned several times here in the past, e.g. for <a href="http://thebuildingcoder.typepad.com/blog/2010/02/material-quantity-extraction.html">gross material quantity extraction</a>.</p>
<p><strong>Question:</strong> I need to get the geometric information of the layers of a wall. Specifically, I need to retrieve the four corner points of a layer to know its position within the wall.</p>
<p>For example, if two walls are connected in a &quot;Butt&quot; type join, their layers are also connected using the same type join. I would like to obtain this geometry information about layers.</p>
<p>It would be very good if I can obtain the four points of a layer, or the two points. Here is an image explaining the problem:</p>
<p><a href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8c4e85cd970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Compound wall layers" border="0" class="asset  asset-image at-xid-6a00e553e168978833014e8c4e85cd970d image-full" src="/assets/image_dd0dd7.jpg" title="Compound wall layers" /></a></p>
<p>I can see from the blog posts you listed above how to retrieve the layers of a wall. However, when two or more walls are joined, e.g. using abut, miter etc., also their layers are joined in the same fashion. I want to get the four points of each layer:</p>
<p><a href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153925a6003970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Compound wall layer joins" border="0" class="asset  asset-image at-xid-6a00e553e1689788330153925a6003970b image-full" src="/assets/image_f4cea0.jpg" title="Compound wall layer joins" /></a></p>
<p>The four points are different depending on the wall join.</p>
<p>I looked into the Revit API help document, and I did find anything to access this information which is really necessary for us.</p>
<p>Could you please advise me how to achieve this?</p>
<p><strong>Answer:</strong> The Revit API does not provide access to the geometrical information you seek out of the box. On the other hand, though, here is the good news:</p>
<p>You could try splitting the wall into parts. That will split it by layers. Then you can access the geometry of each part individually.</p>
<p>If you do so inside a temporary transaction that is rolled back afterwards, the model will remain unchanged.</p>
<p>Marcelo went ahead and implemented this, and the result works perfectly.</p>
<p>Here is a simple example of two walls with a compound internal layer structure:</p>
<p><a href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8c4e84b9970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Compound walls" border="0" class="asset  asset-image at-xid-6a00e553e168978833014e8c4e84b9970d image-full" src="/assets/image_f03814.jpg" title="Compound walls" /></a></p>
<p>Selecting the walls one at a time shows you that the individual wall layers have different lengths:</p>
<p><a href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154362e3ae1970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Compound wall layers" border="0" class="asset  asset-image at-xid-6a00e553e1689788330154362e3ae1970c image-full" src="/assets/image_8a7a44.jpg" title="Compound wall layers" /></a></p>
<p>The arrangement of the wall layer corner points is non-trivial:</p>
<p><a href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154362e3a96970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Compound wall layers" border="0" class="asset  asset-image at-xid-6a00e553e1689788330154362e3a96970c" src="/assets/image_cce0ea.jpg" title="Compound wall layers" /></a></p>
<p>Marcelo&#39;s sample application GeometryPartsAnalyzer implements the suggestion above, determines the geometry of each wall layer by converting it into an individual temporary part, determines the bottom face of each part, and creates model line geometry to display it:</p>
<p><a href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8c4e8362970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Model lines representing compound wall layers" border="0" class="asset  asset-image at-xid-6a00e553e168978833014e8c4e8362970d image-full" src="/assets/image_a89167.jpg" title="Model lines representing compound wall layers" /></a></p>
<p>The implementation includes several useful utility classes:</p>
<ul>
<li>FaceExtractor </li>
<li>ModelLineCreator </li>
<li>WallSelectionFilter </li>
<li>CmdGeometryParts </li>
</ul>
<p>The FaceExtractor queries a Revit element for its geometry, finds its solid, and extracts and returns all its faces.</p>
<p>Marcelo&#39;s ModelLineCreator is a simplified version of the <a href="http://thebuildingcoder.typepad.com/blog/2010/05/model-curve-creator.html">model curve creator</a> I maintain in The Building Coder samples and last used to display model lines representing the <a href="http://thebuildingcoder.typepad.com/blog/2011/07/top-faces-of-wall.html">top faces of all walls</a>.</p>
<p>The WallSelectionFilter is a trivial selection filter to ensure that only walls can be selected.</p>
<p>CmdGeometryParts implements the external command. It makes use of the helper method GetBottomFacePoints to retrieve a list of points representing the bottom face of the given Revit element:</p>
<pre class="code"><span class="blue">public</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; GetBottomFacePoints( <span class="teal">Element</span> e )
{
&#0160; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; resultingPts = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;();
&#0160;
&#0160; <span class="teal">FaceExtractor</span> faceExtractor 
&#0160; &#0160; = <span class="blue">new</span> <span class="teal">FaceExtractor</span>( e );
&#0160;
&#0160; <span class="teal">FaceArray</span> faces = faceExtractor.Faces;
&#0160;
&#0160; <span class="blue">if</span>( faces.Size == 0 ) { <span class="blue">return</span> resultingPts; }
&#0160;
&#0160; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> faces )
&#0160; {
&#0160; &#0160; <span class="teal">PlanarFace</span> pf = face <span class="blue">as</span> <span class="teal">PlanarFace</span>;
&#0160;
&#0160; &#0160; <span class="blue">if</span>( pf == <span class="blue">null</span> ) { <span class="blue">continue</span>; }
&#0160;
&#0160; &#0160; <span class="blue">if</span>( pf.Normal.IsAlmostEqualTo( -<span class="teal">XYZ</span>.BasisZ ) )
&#0160; &#0160; {
&#0160; &#0160; &#0160; <span class="teal">EdgeArrayArray</span> edgeLoops = face.EdgeLoops;
&#0160;
&#0160; &#0160; &#0160; <span class="blue">foreach</span>( <span class="teal">EdgeArray</span> edgeArray <span class="blue">in</span> edgeLoops )
&#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; <span class="blue">foreach</span>( <span class="teal">Edge</span> edge <span class="blue">in</span> edgeArray )
&#0160; &#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; &#0160; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; points 
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; = edge.Tessellate() <span class="blue">as</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;;
&#0160;
&#0160; &#0160; &#0160; &#0160; &#0160; resultingPts.AddRange( points );
&#0160; &#0160; &#0160; &#0160; }
&#0160; &#0160; &#0160; }
&#0160; &#0160; }
&#0160; }
&#0160; <span class="blue">return</span> resultingPts;
}
</pre>
<p>Here is the CmdGeometryParts mainline Execute method, which performs the following steps:</p>
<ul>
<li>Prompt the user to select the walls to process. </li>
<li>Use the PartUtils class to check whether the selected walls can be split into parts. </li>
<li>Start a temporary transaction for the part creation, so it can be rolled back later. </li>
<li>Create the parts and regenerate the model so the part geometry becomes accessible. </li>
<li>Extract the bottom faces of each part using the GetBottomFacePoints method. </li>
<li>Roll back the temporary transaction to undo the splitting of walls into parts. </li>
<li>Start a new transaction to create persistent model lines to display the part edges saved in the bottom face information. </li>
<li>Create the model lines. </li>
<li>Commit the second transaction. </li>
</ul>
<p>Enjoy:</p>
<pre class="code"><span class="blue">public</span> <span class="teal">Result</span> Execute( 
&#0160; <span class="teal">ExternalCommandData</span> cmdData, 
&#0160; <span class="blue">ref</span> <span class="blue">string</span> msg, 
&#0160; <span class="teal">ElementSet</span> elems )
{
&#0160; <span class="teal">Result</span> result = <span class="teal">Result</span>.Failed;
&#0160;
&#0160; <span class="teal">UIApplication</span> uiApp = cmdData.Application;
&#0160; <span class="teal">UIDocument</span> uiDoc = uiApp.ActiveUIDocument;
&#0160; <span class="teal">Document</span> doc = uiDoc.Document;
&#0160;
&#0160; <span class="teal">Transaction</span> transaction = <span class="blue">new</span> <span class="teal">Transaction</span>( doc );
&#0160;
&#0160; <span class="blue">try</span>
&#0160; {
&#0160; &#0160; <span class="blue">string</span> strMsg = <span class="maroon">&quot;Select walls&quot;</span>;
&#0160;
&#0160; &#0160; <span class="teal">ISelectionFilter</span> filter 
&#0160; &#0160; &#0160; = <span class="blue">new</span> <span class="teal">WallSelectionFilter</span>();
&#0160;
&#0160; &#0160; <span class="teal">IList</span>&lt;<span class="teal">Reference</span>&gt; walls 
&#0160; &#0160; &#0160; = uiDoc.Selection.PickObjects( 
&#0160; &#0160; &#0160; &#0160; <span class="teal">ObjectType</span>.Element, filter, strMsg );
&#0160;
&#0160; &#0160; <span class="blue">if</span>( walls.Count == 0 ) { <span class="blue">return</span> result; }
&#0160;
&#0160; &#0160; <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt; ids = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt;();
&#0160;
&#0160; &#0160; <span class="blue">foreach</span>( <span class="teal">Reference</span> reference <span class="blue">in</span> walls )
&#0160; &#0160; &#0160; ids.Add( reference.ElementId );
&#0160;
&#0160; &#0160; <span class="blue">if</span>( !<span class="teal">PartUtils</span>.AreElementsValidForCreateParts(
&#0160; &#0160; &#0160; doc, ids ) )
&#0160; &#0160; {
&#0160; &#0160; &#0160; <span class="blue">return</span> result;
&#0160; &#0160; }
&#0160;
&#0160; &#0160; transaction.Start( <span class="maroon">&quot;parts&quot;</span> );
&#0160;
&#0160; &#0160; <span class="green">// Split walls into parts</span>
&#0160;
&#0160; &#0160; <span class="teal">PartUtils</span>.CreateParts( doc, ids );
&#0160;
&#0160; &#0160; <span class="green">// Regenerate document to get the part geometry</span>
&#0160;
&#0160; &#0160; doc.Regenerate();
&#0160;
&#0160; &#0160; <span class="green">// Retrieve points from bottom faces of parts</span>
&#0160;
&#0160; &#0160; <span class="teal">List</span>&lt;<span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;&gt; bottomFacesPts 
&#0160; &#0160; &#0160; = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;&gt;();
&#0160;
&#0160; &#0160; <span class="blue">foreach</span>( <span class="teal">ElementId</span> id <span class="blue">in</span> ids )
&#0160; &#0160; {
&#0160; &#0160; &#0160; <span class="blue">if</span>( !<span class="teal">PartUtils</span>.HasAssociatedParts( doc, id ) )
&#0160; &#0160; &#0160; { 
&#0160; &#0160; &#0160; &#0160; <span class="blue">continue</span>; 
&#0160; &#0160; &#0160; }
&#0160;
&#0160; &#0160; &#0160; <span class="teal">ICollection</span>&lt;<span class="teal">ElementId</span>&gt; partIds 
&#0160; &#0160; &#0160; &#0160; = <span class="teal">PartUtils</span>.GetAssociatedParts( 
&#0160; &#0160; &#0160; &#0160; &#0160; doc, id, <span class="blue">true</span>, <span class="blue">true</span> );
&#0160;
&#0160; &#0160; &#0160; <span class="blue">foreach</span>( <span class="teal">ElementId</span> partId <span class="blue">in</span> partIds )
&#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; <span class="teal">Element</span> part = doc.get_Element( partId );
&#0160;
&#0160; &#0160; &#0160; &#0160; bottomFacesPts.Add( 
&#0160; &#0160; &#0160; &#0160; &#0160; GetBottomFacePoints( part ) );
&#0160; &#0160; &#0160; }
&#0160; &#0160; }
&#0160;
&#0160; &#0160; <span class="green">// Do not affect the original state of walls</span>
&#0160;
&#0160; &#0160; transaction.RollBack();
&#0160;
&#0160; &#0160; <span class="green">// Draw lines to show the bottom faces of parts</span>
&#0160;
&#0160; &#0160; transaction.Start();
&#0160;
&#0160; &#0160; <span class="teal">ModelLineCreator</span> model 
&#0160; &#0160; &#0160; = <span class="blue">new</span> <span class="teal">ModelLineCreator</span>( doc );
&#0160;
&#0160; &#0160; <span class="blue">foreach</span>( <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; bottomFacePts <span class="blue">in</span> 
&#0160; &#0160; &#0160; bottomFacesPts )
&#0160; &#0160; {
&#0160; &#0160; &#0160; <span class="blue">for</span>( <span class="blue">int</span> i = 1; i &lt; bottomFacePts.Count; ++i )
&#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; model.CreateLine( bottomFacePts[i - 1], 
&#0160; &#0160; &#0160; &#0160; &#0160; bottomFacePts[i], <span class="blue">true</span> );
&#0160; &#0160; &#0160; }
&#0160;
&#0160; &#0160; &#0160; <span class="blue">if</span>( bottomFacePts.Count &gt; 3 )
&#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; model.CreateLine( bottomFacePts[0], 
&#0160; &#0160; &#0160; &#0160; &#0160; bottomFacePts[bottomFacePts.Count - 1], 
&#0160; &#0160; &#0160; &#0160; &#0160; <span class="blue">true</span> );
&#0160; &#0160; &#0160; }
&#0160; &#0160; }
&#0160; &#0160; transaction.Commit();
&#0160;
&#0160; &#0160; result = <span class="teal">Result</span>.Succeeded;
&#0160; }
&#0160; <span class="blue">catch</span>( System.<span class="teal">Exception</span> e )
&#0160; {
&#0160; &#0160; msg = e.Message;
&#0160; &#0160; result = <span class="teal">Result</span>.Failed;
&#0160; }
&#0160; <span class="blue">return</span> result;
}
</pre>
<p>Here is <span class="asset  asset-generic at-xid-6a00e553e168978833014e8c4e8264970d"><a href="http://thebuildingcoder.typepad.com/files/geometrypartsanalyzer.zip">GeometryPartsAnalyzer.zip</a></span> including the complete source code and Visual Studio solution of this command.</p>
<p>Many thanks to Marcelo for his exploration and nice implementation!</p>
