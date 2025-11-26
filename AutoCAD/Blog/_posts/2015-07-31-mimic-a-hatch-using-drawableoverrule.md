---
layout: "post"
title: "Mimic a hatch using DrawableOverrule"
date: "2015-07-31 03:21:01"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/mimic-a-hatch-using-drawableoverrule.html "
typepad_basename: "mimic-a-hatch-using-drawableoverrule"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you need an entity to appear hatched temporarily, drawable overrule can be used for this purpose. Drawing the hatch pattern from an WorldDraw / ViewportDraw can be simple or a little complex depending on the pattern that you want to use. To let the hatch pattern correctly fill the boundary, a clip boundary can be created in your overrule. This simplifies the overrule implementation as the pattern can be created to cover the entity while the clip boundary takes care of clipping it to the actual entity bounds.</p>
<p>Here are two screenshots before and after clipping</p>
</br>
<a class="asset-img-link"   href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b7b9eb970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b7b9eb970b img-responsive" alt="BeforeClip" title="BeforeClip" src="/assets/image_782134.jpg" style="margin: 0px 5px 5px 0px;" /></a>
</br>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b7b9f2970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b7b9f2970b img-responsive" alt="AfterClip" title="AfterClip" src="/assets/image_732697.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
</br>
<p>Here is a sample code to hatch a circle with the cross pattern using DrawableOverrule</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  <span style="color:#2b91af">HatchedCircleOverrule</span><span style="color:#000000">  </pre>
<pre style="margin:0em;">                 : DrawableOverrule</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">override</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  IsApplicable(RXObject subject)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Circle circle = subject <span style="color:#0000ff">as</span><span style="color:#000000">  Circle;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (circle.Database == <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">override</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ViewportDraw</pre>
<pre style="margin:0em;">         (Drawable drawable, ViewportDraw vd)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">base</span><span style="color:#000000"> .ViewportDraw(drawable, vd);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Circle circle = (Circle)drawable;</pre>
<pre style="margin:0em;">         Point3d cenPt = circle.Center;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ClipBoundary cb = <span style="color:#0000ff">new</span><span style="color:#000000">  ClipBoundary();</pre>
<pre style="margin:0em;">         cb.DrawBoundary = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         FrontAndBackClipping clipping </pre>
<pre style="margin:0em;">             = vd.Viewport.FrontAndBackClipping;</pre>
<pre style="margin:0em;">         cb.ClippingBack = clipping.ClipBack;</pre>
<pre style="margin:0em;">         cb.ClippingFront = clipping.ClipFront;</pre>
<pre style="margin:0em;">         cb.NormalVector = vd.Viewport.ViewDirection;</pre>
<pre style="margin:0em;">         cb.BackClipZ = clipping.Back;</pre>
<pre style="margin:0em;">         cb.FrontClipZ = clipping.Front;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Point2dCollection clipPts </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  Point2dCollection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">double</span><span style="color:#000000">  paramIncr = </pre>
<pre style="margin:0em;">             (circle.EndParam - circle.StartParam) / 100;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">double</span><span style="color:#000000">  param = circle.StartParam;</pre>
<pre style="margin:0em;">         Plane pln = <span style="color:#0000ff">new</span><span style="color:#000000">  Plane(cenPt, circle.Normal);</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  cnt = 0; cnt &lt; 100; cnt++)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             clipPts.Add(</pre>
<pre style="margin:0em;">             circle.GetPointAtParameter(param).Convert2d(pln));</pre>
<pre style="margin:0em;">             param += paramIncr;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         clipPts.Add(</pre>
<pre style="margin:0em;">         circle.GetPointAtParameter(circle.EndParam).Convert2d(pln));</pre>
<pre style="margin:0em;">         cb.SetAptPoints(clipPts);</pre>
<pre style="margin:0em;">         vd.Geometry.PushClipBoundary(cb);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         vd.SubEntityTraits.TrueColor </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  EntityColor(0, 255, 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Point3dCollection vertices </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  Point3dCollection();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">int</span><span style="color:#000000">  divs = 8;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">double</span><span style="color:#000000">  delX = circle.Diameter / divs;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">double</span><span style="color:#000000">  delY = circle.Diameter / divs;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  row = 0; row &lt;= divs; row++)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Point3d currPt = </pre>
<pre style="margin:0em;">                 cenPt </pre>
<pre style="margin:0em;">                 - Vector3d.XAxis * circle.Radius </pre>
<pre style="margin:0em;">                 - Vector3d.YAxis * circle.Radius </pre>
<pre style="margin:0em;">                 + Vector3d.YAxis * row * delY;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  col = 0; col &lt;= divs; col++)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 vertices.Clear();</pre>
<pre style="margin:0em;">                 vertices.Add(</pre>
<pre style="margin:0em;">                     currPt + Vector3d.XAxis * delX * 0.25);</pre>
<pre style="margin:0em;">                 vertices.Add(</pre>
<pre style="margin:0em;">                     currPt - Vector3d.XAxis * delX * 0.25);</pre>
<pre style="margin:0em;">                 vd.Geometry.Polyline(</pre>
<pre style="margin:0em;">                     vertices, Vector3d.ZAxis, <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">IntPtr</span><span style="color:#000000"> (-1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 vertices.Clear();</pre>
<pre style="margin:0em;">                 vertices.Add(</pre>
<pre style="margin:0em;">                     currPt + Vector3d.YAxis * delY * 0.25);</pre>
<pre style="margin:0em;">                 vertices.Add(</pre>
<pre style="margin:0em;">                     currPt - Vector3d.YAxis * delY * 0.25);</pre>
<pre style="margin:0em;">                 vd.Geometry.Polyline(</pre>
<pre style="margin:0em;">                     vertices, Vector3d.ZAxis, <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">IntPtr</span><span style="color:#000000"> (-1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 currPt = currPt + Vector3d.XAxis * delX;</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         vd.Geometry.PopClipBoundary();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">override</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  WorldDraw</pre>
<pre style="margin:0em;">         (Drawable drawable, WorldDraw wd)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">base</span><span style="color:#000000"> .WorldDraw(drawable, wd);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// call viewportdraw</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  <span style="color:#2b91af">Commands</span><span style="color:#000000">  : IExtensionApplication</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#2b91af">HatchedCircleOverrule</span><span style="color:#000000">  _hco;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">void</span><span style="color:#000000">  IExtensionApplication.Initialize() <span style="color:#000000">{</span> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">void</span><span style="color:#000000">  IExtensionApplication.Terminate()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (_hco != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Overrule.RemoveOverrule</pre>
<pre style="margin:0em;">                 (RXClass.GetClass(<span style="color:#0000ff">typeof</span><span style="color:#000000"> (Circle)), _hco);</pre>
<pre style="margin:0em;">             _hco.Dispose();</pre>
<pre style="margin:0em;">             _hco = <span style="color:#0000ff">null</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [CommandMethod(<span style="color:#a31515">&quot;StartOR&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StartORMethod()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Document doc </pre>
<pre style="margin:0em;">             = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (_hco == <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             _hco = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">HatchedCircleOverrule</span><span style="color:#000000"> ();</pre>
<pre style="margin:0em;">             _hco.SetCustomFilter();</pre>
<pre style="margin:0em;">             Overrule.AddOverrule</pre>
<pre style="margin:0em;">                 (RXClass.GetClass(<span style="color:#0000ff">typeof</span><span style="color:#000000"> (Circle)), </pre>
<pre style="margin:0em;">                 _hco, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             Overrule.Overruling = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         doc.Editor.WriteMessage(<span style="color:#a31515">&quot;\nOverruling started !&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         doc.Editor.Regen();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [CommandMethod(<span style="color:#a31515">&quot;StopOR&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StopORMethod()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Document doc </pre>
<pre style="margin:0em;">             = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (_hco != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Overrule.RemoveOverrule(</pre>
<pre style="margin:0em;">                 RXClass.GetClass(<span style="color:#0000ff">typeof</span><span style="color:#000000"> (Circle)), _hco);</pre>
<pre style="margin:0em;">             Overrule.Overruling = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             _hco.Dispose();</pre>
<pre style="margin:0em;">             _hco = <span style="color:#0000ff">null</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         doc.Editor.WriteMessage(<span style="color:#a31515">&quot;\nOverruling stopped !&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         doc.Editor.Regen();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
