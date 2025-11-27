---
layout: "post"
title: "Control feature creation based on user defined boundary"
date: "2013-03-29 22:25:59"
author: "Wayne Brill"
categories:
  - "iLogic"
  - "Inventor"
  - "Parts"
  - "VB.NET"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/03/control-feature-creation-based-on-user-defined-boundary.html "
typepad_basename: "control-feature-creation-based-on-user-defined-boundary"
typepad_status: "Publish"
---

<p><em>Thanks to DevTech engineer Vladimir Ananyev for providing this post and great example.</em></p>
<p>Recently we were asked if the Inventor API could help by suppressing elements of holes in a rectangular pattern which are not completely inside a user-defined boundary.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee9d6d017970d-pi"><img alt="image" border="0" height="145" src="/assets/image_944291.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="488" /></a> </p>
<p>The algorithm needs to suppress holes that have centers located outside the boundary at a distance greater than the hole radius from the boundary. The boundary could be complex geometry and the most difficult problem was to determine if the hole was completely inside or outside the boundary. This problem was solved by using Inventor API objects involved in the creation of profiles for solid geometry.</p>
<p>In the screenshots below you see a sketch and the extrusion that is created using the profile created with the AddForSolid() method.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;<span style="color: blue;">Dim</span> oProfile <span style="color: blue;">As</span> <span style="color: #2b91af;">Profile</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; = oSketch.Profiles.AddForSolid(<span style="color: blue;">True</span>)</p>
</div>
<p>The result is shown in Fig. 2.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3833a603970b-pi"><img alt="image" border="0" height="172" src="/assets/image_3105.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="474" /></a></p>
<p>If a circular profile path crosses the ellipse (boundary) or is located completely outside then it adds material to the solid body. Otherwise it subtracts material.</p>
<p>Figures 3 and 4 show that this rule works for profile paths of any other geometry as well.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3833a616970b-pi"><img alt="image" border="0" height="169" src="/assets/image_264227.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="488" /></a></p>
<p><br />This powerful built-in topology analyzer of the AddForSolid method you can be used to easily identify pattern elements that are completely inside the specified boundary. (Fortunately there are not any restrictions on the geometry of the profile paths).</p>
<p>To demonstrate this approach I used an iLogic rule. (Any language that can access the Inventor API could have been used too). In figure 5 you see the full rectangular pattern. After the rule is run, the holes outside of the boundary are suppressed. (figure 6)</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3833a62c970b-pi"><img alt="image" border="0" height="226" src="/assets/image_553064.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="475" /></a></p>
<p><strong>The steps are as follows. </strong><strong> <br /></strong>1. The Main function projects the boundary geometry from the planar sketch “Boundary” and all the elements of the holes pattern into the created on-the-fly transient planar sketch. (For simplicity the rule works with the first rectangular pattern in its parent part document).&#0160; </p>
<p>2. It then calls the <em>AddForSolid</em> method to create a profile object based on the combined projected geometry in the transient sketch.&#0160; </p>
<p>3.&#0160; The rule analyses every profile path in the profile object. If the path represents a projected hole’s pattern element and its property <em>AddsMaterial</em> returns True, then the corresponding pattern element is suppressed. The search for <em>FeaturePatternElem</em>ent is based on the reference to the projected sketch circle is performed by the function <em>GetEltByGreenCircle</em>.</p>
<p><strong>Note</strong>:&#0160; The parent hole feature cannot be suppressed otherwise all the pattern will be suppressed as well, that is why our code ignores the parent feature.</p>
<p>An Inventor part file with the iLogic rule can be downloaded <a href="http://modthemachine.typepad.com/Pattern.ipt" target="_blank">here</a>.</p>
<p><strong>Here are the two iLogic rules</strong>:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Sub</span> Main()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">PartDocument</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">TryCast</span>(ThisDoc.Document, <span style="color: #2b91af;">PartDocument</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oDoc <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;This rule works with part documents only.&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;iLogic Rule&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxButtons</span>.OK,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxIcon</span>.Warning)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDef <span style="color: blue;">As</span> <span style="color: #2b91af;">PartComponentDefinition</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.ComponentDefinition</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;this rule works with only one rect. pattern only</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;provided directly by item number</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPattern <span style="color: blue;">As</span> <span style="color: #2b91af;">RectangularPatternFeature</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; oDef.Features.RectangularPatternFeatures.Item(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;restore all pattern elements in order to </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;get access to their faces</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oElt <span style="color: blue;">As</span> <span style="color: #2b91af;">FeaturePatternElement</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">In</span> oPattern.PatternElements</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oElt.Suppressed <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElt.Suppressed = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc.Update()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ThisApplication.ActiveView.Update()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Beep()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oBoundarySketch <span style="color: blue;">As</span> <span style="color: #2b91af;">PlanarSketch</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDef.Sketches.Item(<span style="color: #a31515;">&quot;Boundary&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;temporary sketch for profile</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oSketch <span style="color: blue;">As</span> <span style="color: #2b91af;">PlanarSketch</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; oDef.Sketches.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oBoundarySketch.PlanarEntity, <span style="color: blue;">False</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.Visible = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;project boundary</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oE <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchEntity</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">In</span> oBoundarySketch.SketchEntities</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oSketch.AddByProjectingEntity(oE)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;project parent hole</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oHole <span style="color: blue;">As</span> <span style="color: #2b91af;">HoleFeature</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">TryCast</span>(oPattern.ParentFeatures.Item(1),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">HoleFeature</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oHole <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show _</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;This rule works with hole feature pattern only.&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;iLogic Rule&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxButtons</span>.OK, <span style="color: #2b91af;">MessageBoxIcon</span>.Warning)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oEdge <span style="color: blue;">As</span> <span style="color: #2b91af;">Edge</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oHole.SideFaces.Item(1).Edges.Item(1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oSketch.AddByProjectingEntity(oEdge)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;project holes pattern</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oElt <span style="color: blue;">As</span> <span style="color: #2b91af;">FeaturePatternElement</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">In</span> oPattern.PatternElements</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oElt.Faces.Count &gt; 0 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdge = oElt.Faces.Item(1).Edges.Item(1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oSketch.AddByProjectingEntity(oEdge)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a profile.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oProfile <span style="color: blue;">As</span> <span style="color: #2b91af;">Profile</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSketch.Profiles.AddForSolid(<span style="color: blue;">True</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oPath <span style="color: blue;">As</span> <span style="color: #2b91af;">ProfilePath</span> <span style="color: blue;">In</span> oProfile</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oPath.Count = 1 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPE <span style="color: blue;">As</span> <span style="color: #2b91af;">ProfileEntity</span> = oPath.Item(1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oPE.CurveType =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Curve2dTypeEnum</span>.kCircleCurve2d <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oSkE <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchEntity</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPE.SketchEntity</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oGreenCircle <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchCircle</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">CType</span>(oSkE, <span style="color: #2b91af;">SketchCircle</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oFPE <span style="color: blue;">As</span> <span style="color: #2b91af;">FeaturePatternElement</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetEltByGreenCircle(oGreenCircle)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oFPE <span style="color: blue;">IsNot</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oPath.AddsMaterial = <span style="color: blue;">True</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;should be suppressed</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oFPE.Suppressed = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;delete temporary sketch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSketch.Delete()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc.Update()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Beep()</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Function</span> GetEltByGreenCircle( _</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">ByRef</span> oGreenCircle <span style="color: blue;">As</span> <span style="color: #2b91af;">SketchCircle</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">As</span> <span style="color: #2b91af;">FeaturePatternElement</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;returns FPE corresponding to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;given SketchCircle object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oGreenCircle <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span> <span style="color: blue;">Return</span> <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oEdge <span style="color: blue;">As</span> <span style="color: #2b91af;">Edge</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdge = <span style="color: blue;">CType</span>(oGreenCircle. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ReferencedEntity, <span style="color: #2b91af;">Edges</span>).Item(1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oFace <span style="color: blue;">As</span> <span style="color: #2b91af;">Face</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oEdge.Faces.Item(1).SurfaceType =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">SurfaceTypeEnum</span>.kCylinderSurface <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oFace = oEdge.Faces.Item(1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oFace = oEdge.Faces.Item(2)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oFace.CreatedByFeature.Type =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectTypeEnum</span>.kHoleFeatureObject <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> lKey <span style="color: blue;">As</span> <span style="color: blue;">Long</span> = oFace.TransientKey</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPattern <span style="color: blue;">As</span> <span style="color: #2b91af;">RectangularPatternFeature</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = <span style="color: blue;">CType</span>(oFace.CreatedByFeature,&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">RectangularPatternFeature</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oElt <span style="color: blue;">As</span> <span style="color: #2b91af;">FeaturePatternElement</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">In</span> oPattern.PatternElements</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oElt.Faces.Count &gt; 0 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> lKey = oElt.Faces.Item(1). _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientKey <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> oElt</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Function</span> <span style="color: green;">&#39;GetEltByGreenCircle</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>(Thanks! to Vladimir)</p>
<p>-Wayne</p>
