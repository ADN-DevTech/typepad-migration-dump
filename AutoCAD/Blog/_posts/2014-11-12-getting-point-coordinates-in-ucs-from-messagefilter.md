---
layout: "post"
title: "Getting point coordinates in UCS from MessageFilter"
date: "2014-11-12 04:19:47"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/11/getting-point-coordinates-in-ucs-from-messagefilter.html "
typepad_basename: "getting-point-coordinates-in-ucs-from-messagefilter"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a code snippet to convert the mouse coordinates retreived from the Windows message in a message filter to an AutoCAD point coordinate in UCS.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">class</span><span style="color:#000000">  <span style="color:#2b91af">MyMessageFilter</span><span style="color:#000000">  : System.Windows.Forms.<span style="color:#2b91af">IMessageFilter</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">const</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  WM_LBUTTONDBLCLK = 0x203;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">bool</span><span style="color:#000000">  System.Windows.Forms.<span style="color:#2b91af">IMessageFilter</span><span style="color:#000000"> .PreFilterMessage</pre>
<pre style="margin:0em;">                         (<span style="color:#0000ff">ref</span><span style="color:#000000">  System.Windows.Forms.<span style="color:#2b91af">Message</span><span style="color:#000000">  m)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (m.Msg == WM_LBUTTONDBLCLK)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Document doc = </pre>
<pre style="margin:0em;">                <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">             Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Double clicked point coordinates in pixels</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">int</span><span style="color:#000000">  x = m.LParam.ToInt32() &amp; 0xffff;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">int</span><span style="color:#000000">  y = (m.LParam.ToInt32() &gt;&gt; 16);</pre>
<pre style="margin:0em;">             System.Drawing.<span style="color:#2b91af">Point</span><span style="color:#000000">  p = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">Point</span><span style="color:#000000"> (x, y);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Pixel to device independent coordinates</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             System.Windows.Point p1 = <span style="color:#0000ff">new</span><span style="color:#000000">  System.Windows.Point();</pre>
<pre style="margin:0em;">             </pre>
<pre style="margin:0em;">             System.Windows.Vector s = </pre>
<pre style="margin:0em;">             Autodesk.AutoCAD.Windows.Window.GetDeviceIndependentScale</pre>
<pre style="margin:0em;">             (<span style="color:#2b91af">IntPtr</span><span style="color:#000000"> .Zero);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             p1.X = (<span style="color:#0000ff">int</span><span style="color:#000000"> )(p.X / s.X);</pre>
<pre style="margin:0em;">             p1.Y = (<span style="color:#0000ff">int</span><span style="color:#000000"> )(p.Y / s.Y);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// Device independent coordinates to WCS</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">short</span><span style="color:#000000">  vpNum = </pre>
<pre style="margin:0em;">                 (<span style="color:#0000ff">short</span><span style="color:#000000"> )<span style="color:#2b91af">Application</span><span style="color:#000000"> .GetSystemVariable(<span style="color:#a31515">&quot;CVPORT&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             Point3d bp = ed.PointToWorld(p1, vpNum);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// WCS to UCS</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             bp = bp.TransformBy(</pre>
<pre style="margin:0em;">                         ed.CurrentUserCoordinateSystem.Inverse());</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(<span style="color:#2b91af">String</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;\\n<span style="color:#000000">{</span>0<span style="color:#000000">}</span> <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , bp.X, bp.Y));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Please note that there could be slight variation from the coordinates displayed by AutoCAD in its status bar. This is a similar behavior when using "acedCoordFromPixelToWorld" to do such conversion. As AutoCAD uses&nbsp;some internal functions that may be different from what is exposed in the API, so the reply from our engineering to a similar query in the past, was that whatever approach we use, we may never be able to retrieve the exact same values as that of what is displayed in the status bar of AutoCAD UI.&nbsp;</p>
