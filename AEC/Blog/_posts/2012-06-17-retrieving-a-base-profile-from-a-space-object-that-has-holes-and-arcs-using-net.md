---
layout: "post"
title: "Retrieving a base profile from a space object that has holes and arcs using .NET"
date: "2012-06-17 21:01:08"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/06/retrieving-a-base-profile-from-a-space-object-that-has-holes-and-arcs-using-net.html "
typepad_basename: "retrieving-a-base-profile-from-a-space-object-that-has-holes-and-arcs-using-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I have a Space object in AutoCAD Architecture and I need to extract its contour to export to another data format. The problems appear when the Space object has &quot;holes&quot;.&#0160; I also don&#39;t know how to calculate the points on an arc.&#0160;</p>
<p><strong>Solution</strong></p>
<p>A profile is simply a collection&#0160; of closed outlines called Ring, which is defined in 2D.&#0160;&#0160; The first one is the outer most ring.&#0160; Others are holes in the profile.&#0160; To obtain the profile information, follow the steps:</p>
<ol>
<li>From the Space object, GetProfile with Baseline option&#0160; </li>
<li>Iterate through Rings collection </li>
<li>For each Ring, get the Segments or Segment2dCollection.&#0160; (AecGeSegment2d) </li>
<li>For each Segment, you should be able to get the segment info, such as start point, end point and bulge. </li>
</ol>
<p>In case a segment is an arc, you can get bulge information from each segment or use EvaluatePoint() method to obtain a point on an arc if needed.&#0160; Below is a sample code written in VB.NET that demonstrates this.</p>
<p>&#0160;<span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> ShowSpaceProfile()</span></p>
<div style="font-family: Consolas; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; first, get the basic objects </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> = </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> db </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Database</span><span style="line-hight: 140%;"> = </span><span style="color: #2b91af; line-hight: 140%;">HostApplicationServices</span><span style="line-hight: 140%;">.WorkingDatabase</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> tm </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">TransactionManager</span><span style="line-hight: 140%;"> = db.TransactionManager</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> tr </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Transaction</span><span style="line-hight: 140%;"> = tm.StartTransaction()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; select a space </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> selOpt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> Autodesk.AutoCAD.EditorInput.</span><span style="color: #2b91af; line-hight: 140%;">PromptEntityOptions</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;Select a space&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; selOpt.SetRejectMessage(</span><span style="color: #a31515; line-hight: 140%;">&quot;Not a space&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; selOpt.AddAllowedClass(</span><span style="color: blue; line-hight: 140%;">GetType</span><span style="line-hight: 140%;">(</span><span style="color: #2b91af; line-hight: 140%;">Space</span><span style="line-hight: 140%;">), </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> selRes </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">PromptEntityResult</span><span style="line-hight: 140%;"> = ed.GetEntity(selOpt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> selRes.Status &lt;&gt; </span><span style="color: #2b91af; line-hight: 140%;">PromptStatus</span><span style="line-hight: 140%;">.OK </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-hight: 140%;">&quot;failed to get a space&quot;</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; we got a space. Open it for read. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> pSpace </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Space</span><span style="line-hight: 140%;"> = tr.GetObject(selRes.ObjectId, AcDb2.</span><span style="color: #2b91af; line-hight: 140%;">OpenMode</span><span style="line-hight: 140%;">.ForRead)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; get a base profile </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> pProfile </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Profile</span><span style="line-hight: 140%;"> = pSpace.BaseProfile</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ecsEnt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Matrix3d</span><span style="line-hight: 140%;"> = pSpace.Ecs</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; loop through the rings.&#0160; The first one is outer most rings. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">For</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Each</span><span style="line-hight: 140%;"> aRing </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Ring</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">In</span><span style="line-hight: 140%;"> pProfile.Rings</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">For</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Each</span><span style="line-hight: 140%;"> aSeg </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Segment2d</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">In</span><span style="line-hight: 140%;"> aRing.Segments</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; once you come to this point, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; you can get information about each line segment.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; here we draw a temporary drawings.&#0160; see the helper function below.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; displaySegment(aSeg, ecsEnt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Commit()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Catch</span><span style="line-hight: 140%;"> ex </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-hight: 140%;">&quot;Error in ShowSpaceProfile: &quot;</span><span style="line-hight: 140%;"> + ex.Message + vbLf)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Abort()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Finally</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; tr.Dispose()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; a helper function to display a segment in a temporary graphics </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">Sub</span><span style="line-hight: 140%;"> displaySegment(</span><span style="color: blue; line-hight: 140%;">ByVal</span><span style="line-hight: 140%;"> aSeg </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Segment2d</span><span style="line-hight: 140%;">, </span><span style="color: blue; line-hight: 140%;">ByVal</span><span style="line-hight: 140%;"> ecsEnt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Matrix3d</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> ed </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Editor</span><span style="line-hight: 140%;"> = </span><span style="color: #2b91af; line-hight: 140%;">Application</span><span style="line-hight: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> pt1 </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Point3d</span><span style="line-hight: 140%;">(aSeg.StartPoint.X, aSeg.StartPoint.Y, 0.0)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> pt2 </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Point3d</span><span style="line-hight: 140%;">(aSeg.EndPoint.X, aSeg.EndPoint.Y, 0.0)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; pt1 = pt1.TransformBy(ecsEnt)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; draw a temporary graphics depending on if the given a segment is a line or curve. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">&#39;&#39;&#0160; if you have an arc, you can use bulge, or evaluate sub-divided points.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">If</span><span style="line-hight: 140%;"> aSeg.IsCurveCircularArc2d </span><span style="color: blue; line-hight: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> n </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Integer</span><span style="line-hight: 140%;"> = 10 </span><span style="color: green; line-hight: 140%;">&#39;&#39; number of divisions to draw arc as a collection of lines.&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> dt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Double</span><span style="line-hight: 140%;"> = (aSeg.EndParameter - aSeg.StartParameter) / n</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">For</span><span style="line-hight: 140%;"> i </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Integer</span><span style="line-hight: 140%;"> = 1 </span><span style="color: blue; line-hight: 140%;">To</span><span style="line-hight: 140%;"> n</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Dim</span><span style="line-hight: 140%;"> pt </span><span style="color: blue; line-hight: 140%;">As</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Point2d</span><span style="line-hight: 140%;"> = aSeg.EvaluatePoint(i * dt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; pt2 = </span><span style="color: blue; line-hight: 140%;">New</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">Point3d</span><span style="line-hight: 140%;">(pt.X, pt.Y, 0.0)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; pt2 = pt2.TransformBy(ecsEnt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; ed.DrawVector(pt1, pt2, 1, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; pt1 = pt2</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">Else</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; pt2 = pt2.TransformBy(ecsEnt)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; ed.DrawVector(pt1, pt2, 5, </span><span style="color: blue; line-hight: 140%;">True</span><span style="line-hight: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">End</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">Sub</span></p>
</div>
