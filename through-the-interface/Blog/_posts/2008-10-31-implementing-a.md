---
layout: "post"
title: "Implementing a custom AutoCAD object snap mode using .NET"
date: "2008-10-31 06:35:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Graphics system"
original_url: "https://www.keanw.com/2008/10/implementing-a.html "
typepad_basename: "implementing-a"
typepad_status: "Publish"
---

<p><em>Thanks again to Augusto Gonçalves, from our DevTech Americas team, for providing the original VB.NET code for this sample, as well as helping investigate an issue I faced during implementation.</em></p>
<p>When I saw a recent reply to a developer, showing how to implement a custom object snap in AutoCAD using .NET, I had a really strong sense of nostalgia: it reminded me of a couple of early samples I contributed to the ObjectARX SDK: the "third" sample, which showed how to create a custom osnap that snapped to a third of the way along a curve, and "divisor" which generalised the approach to fractions of any size and was my first real attempt at using C++ templates. Ah, the memories. The samples were retired from this year's SDK, but were still included up to and including the ObjectARX SDK for AutoCAD 2008.</p>
<p>Anyway, the code Augusto sent was very familiar, and it turns out he based it on some documentation that was probably, in turn, based on my C++ sample. So it has come full circle. :-)</p>
<p>One thing I hadn't realised until I saw Augusto's email was that the ability to define custom object snaps had been exposed through .NET.</p>
<p>Here's the C# code that implements a new "quarter" object snap, which snaps to 1/4 and 3/4 along the length of a curve.</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.GraphicsInterface;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">[assembly:</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ExtensionApplication</span></span><span style="line-height: 140%;">(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(OsnapApp.</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomOSnapApp</span></span><span style="line-height: 140%;">))</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">]</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> OsnapApp</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// Register and unregister custom osnap</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomOSnapApp</span></span><span style="line-height: 140%;"> : </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span></span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">QuarterOsnapInfo</span></span><span style="line-height: 140%;"> _info =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">QuarterOsnapInfo</span></span><span style="line-height: 140%;">();</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">QuarterGlyph</span></span><span style="line-height: 140%;"> _glyph =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">QuarterGlyph</span></span><span style="line-height: 140%;">();</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomObjectSnapMode</span></span><span style="line-height: 140%;"> _mode;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Initialize()</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Register custom osnap on initialize</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;_mode =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomObjectSnapMode</span></span><span style="line-height: 140%;">(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #a31515;"><span style="color: #a31515; line-height: 140%;">"Quarter"</span></span><span style="line-height: 140%;">,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #a31515;"><span style="color: #a31515; line-height: 140%;">"Quarter"</span></span><span style="line-height: 140%;">,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #a31515;"><span style="color: #a31515; line-height: 140%;">"Quarter of length"</span></span><span style="line-height: 140%;">,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; _glyph</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Which kind of entity will use the osnap</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;_mode.ApplyToEntityType(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">RXObject</span></span><span style="line-height: 140%;">.GetClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Polyline</span></span><span style="line-height: 140%;">)),</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">AddObjectSnapInfo</span></span><span style="line-height: 140%;">(_info.SnapInfoPolyline)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;_mode.ApplyToEntityType(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">RXObject</span></span><span style="line-height: 140%;">.GetClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Curve</span></span><span style="line-height: 140%;">)),</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">AddObjectSnapInfo</span></span><span style="line-height: 140%;">(_info.SnapInfoCurve)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;_mode.ApplyToEntityType(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">RXObject</span></span><span style="line-height: 140%;">.GetClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Entity</span></span><span style="line-height: 140%;">)),</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">AddObjectSnapInfo</span></span><span style="line-height: 140%;">(_info.SnapInfoEntity)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Activate the osnap</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomObjectSnapMode</span></span><span style="line-height: 140%;">.Activate(</span><span style="color: #a31515;"><span style="color: #a31515; line-height: 140%;">"_Quarter"</span></span><span style="line-height: 140%;">);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Unregister custom osnap on terminate</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Terminate()</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {&nbsp; &nbsp;&nbsp; &nbsp;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomObjectSnapMode</span></span><span style="line-height: 140%;">.Deactivate(</span><span style="color: #a31515;"><span style="color: #a31515; line-height: 140%;">"_Quarter"</span></span><span style="line-height: 140%;">);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// Create new quarter object snap</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">QuarterGlyph</span></span><span style="line-height: 140%;"> : </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Glyph</span></span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Point3d</span></span><span style="line-height: 140%;"> _pt;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SetLocation(</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Point3d</span></span><span style="line-height: 140%;"> point)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; _pt = point;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ViewportDraw(</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ViewportDraw</span></span><span style="line-height: 140%;"> vd)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> glyphPixels =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">CustomObjectSnapMode</span></span><span style="line-height: 140%;">.GlyphSize;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Point2d</span></span><span style="line-height: 140%;"> glyphSize =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vd.Viewport.GetNumPixelsInUnitSquare(_pt);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Calculate the size of the glyph in WCS</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; (use for text height factor)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// We'll add 20% to the size, as otherwise</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; it looks a little too small</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> glyphHeight =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (glyphPixels / glyphSize.Y) * 1.2;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> text = </span><span style="color: #a31515;"><span style="color: #a31515; line-height: 140%;">"¼"</span></span><span style="line-height: 140%;">;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Translate the X-axis of the DCS to WCS</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; (for the text direction) and the snap</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; point itself (for the text location)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span></span><span style="line-height: 140%;"> e2w = vd.Viewport.EyeToWorldTransform;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span></span><span style="line-height: 140%;"> dir = </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span></span><span style="line-height: 140%;">.XAxis.TransformBy(e2w);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Point3d</span></span><span style="line-height: 140%;"> pt = _pt.TransformBy(e2w);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; Draw the centered text representing the glyph</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vd.Geometry.Text(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pt,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vd.Viewport.ViewDirection,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; dir,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; glyphHeight,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 1,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 0,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; text</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// OSnap info</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">QuarterOsnapInfo</span></span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SnapInfoEntity(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ObjectSnapContext</span></span><span style="line-height: 140%;"> context,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ObjectSnapInfo</span></span><span style="line-height: 140%;"> result)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Nothing here</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SnapInfoCurve(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ObjectSnapContext</span></span><span style="line-height: 140%;"> context,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ObjectSnapInfo</span></span><span style="line-height: 140%;"> result</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// For any curve</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Curve</span></span><span style="line-height: 140%;"> cv = context.PickedObject </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Curve</span></span><span style="line-height: 140%;">;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cv == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> startParam = cv.StartParam;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> endParam = cv.EndParam;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Add osnap at first quarter</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> param =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; startParam + ((endParam - startParam) * 0.25);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Point3d</span></span><span style="line-height: 140%;"> pt = cv.GetPointAtParameter(param);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;result.SnapPoints.Add(pt);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Add osnap at third quarter</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;param =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; startParam + ((endParam - startParam) * 0.75);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;pt = cv.GetPointAtParameter(param);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;result.SnapPoints.Add(pt);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cv.Closed)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pt = cv.StartPoint;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; result.SnapPoints.Add(pt);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SnapInfoPolyline(</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ObjectSnapContext</span></span><span style="line-height: 140%;"> context,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">ObjectSnapInfo</span></span><span style="line-height: 140%;"> result)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// For polylines</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Polyline</span></span><span style="line-height: 140%;"> pl = context.PickedObject </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;">&nbsp;</span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Polyline</span></span><span style="line-height: 140%;">;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pl == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Get the overall start and end parameters</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> plStartParam = pl.StartParam;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> plEndParam = pl.EndParam;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: green; line-height: 140%;">// Get the local </span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> startParam = plStartParam;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> endParam = startParam + 1.0;</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (endParam &lt;= plEndParam)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Calculate the snap point per vertex...</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Add osnap at first quarter</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> param =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; startParam + ((endParam - startParam) * 0.25);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="color: #2b91af; line-height: 140%;">Point3d</span></span><span style="line-height: 140%;"> pt = pl.GetPointAtParameter(param);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; result.SnapPoints.Add(pt);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Add osnap at third quarter</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; param =</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; startParam + ((endParam - startParam) * 0.75);</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pt = pl.GetPointAtParameter(param);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; result.SnapPoints.Add(pt);</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; startParam = endParam;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; endParam += 1.0;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Some comments on the implementation:</p>
<ul>
<li>There's a blank callback that is the base implementation for entities</li>
<li>We then override that for all Curve objects, using some code to divide a curve into quarters</li>
<li>We do yet another implementation for all Polyline objects (which are Curves, but we want to treat them as a special case)
<ul>
<li>For Polylines we snap within segments
<ul>
<li>We could have implemented this by retrieving each segment and dividing that into quarters</li>
<li>Instead I chose to rely on the fact that a Polyline's parameter is a "whole number" at each vertex, which means the code is the same for any kind of segment</li>
</ul>
</li>
</ul>
</li>
<li>In my original sample I adjusted the position of the text, to centre it on the snap point
<ul>
<li>In this example I haven't done this, as when I looked at the code it wasn't accurate - when you zoomed in the text appeared in the wrong position</li>
<li>As we're just using a single character (¼) as our glyph, this isn't a significant problem</li>
</ul>
</li>
</ul>
<p>Here's what happens when we load our module and try snapping to a line inside AutoCAD:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Custom%20quarter%20object%20snap.png"><img style="border-width: 0px;" src="/assets/Custom%20quarter%20object%20snap_thumb.png" alt="Custom quarter object snap" width="438" height="221" border="0" /></a></p>
<p><em><strong>Update:</strong></em></p>
<p>I've just made a few minor changes to the above code to update it to work with AutoCAD 2012 (and maybe this is needed for prior versions, too - I'm not sure when the behaviour changed).</p>
<p>Here's the updated C# code:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> AcGi = Autodesk.AutoCAD.GraphicsInterface;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">[<span style="color: blue;">assembly</span>:<span style="color: #2b91af;">ExtensionApplication</span>(</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">typeof</span>(OsnapApp.<span style="color: #2b91af;">CustomOSnapApp</span>))</p>
<p style="margin: 0px;">]</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> OsnapApp</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp; <span style="color: green;">// Register and unregister custom osnap</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">CustomOSnapApp</span> : <span style="color: #2b91af;">IExtensionApplication</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: #2b91af;">QuarterOsnapInfo</span> _info =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">QuarterOsnapInfo</span>();</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: #2b91af;">QuarterGlyph</span> _glyph =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">QuarterGlyph</span>();</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: #2b91af;">CustomObjectSnapMode</span> _mode;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Initialize()</p>
<p style="margin: 0px;">&nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Register custom osnap on initialize</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; _mode =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">CustomObjectSnapMode</span>(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #a31515;">"Quarter"</span>,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #a31515;">"Quarter"</span>,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #a31515;">"Quarter of length"</span>,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _glyph</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; );</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Which kind of entity will use the osnap</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; _mode.ApplyToEntityType(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">RXObject</span>.GetClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Polyline</span>)),</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">AddObjectSnapInfo</span>(_info.SnapInfoPolyline)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; );</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; _mode.ApplyToEntityType(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">RXObject</span>.GetClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Curve</span>)),</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">AddObjectSnapInfo</span>(_info.SnapInfoCurve)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; );</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; _mode.ApplyToEntityType(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">RXObject</span>.GetClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Entity</span>)),</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">AddObjectSnapInfo</span>(_info.SnapInfoEntity)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; );</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Activate the osnap</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">CustomObjectSnapMode</span>.Activate(<span style="color: #a31515;">"_Quarter"</span>);</p>
<p style="margin: 0px;">&nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: green;">// Unregister custom osnap on terminate</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Terminate()</p>
<p style="margin: 0px;">&nbsp; &nbsp; {&nbsp; &nbsp; &nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">CustomObjectSnapMode</span>.Deactivate(<span style="color: #a31515;">"_Quarter"</span>);</p>
<p style="margin: 0px;">&nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; <span style="color: green;">// Create new quarter object snap</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">QuarterGlyph</span> : AcGi.<span style="color: #2b91af;">Glyph</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">private</span> <span style="color: #2b91af;">Point3d</span> _pt;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> SetLocation(<span style="color: #2b91af;">Point3d</span> point)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; _pt = point;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">protected</span> <span style="color: blue;">override</span> <span style="color: blue;">void</span> SubViewportDraw(AcGi.<span style="color: #2b91af;">ViewportDraw</span> vd)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">int</span> glyphPixels =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">CustomObjectSnapMode</span>.GlyphSize;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Point2d</span> glyphSize =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vd.Viewport.GetNumPixelsInUnitSquare(_pt);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">// Calculate the size of the glyph in WCS</span></p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">//&nbsp; (use for text height factor)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">// We'll add 20% to the size, as otherwise</span></p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">//&nbsp; it looks a little too small</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> glyphHeight =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (glyphPixels / glyphSize.Y) * 1.2;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">string</span> text = <span style="color: #a31515;">"¼"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">// Translate the X-axis of the DCS to WCS</span></p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">//&nbsp; (for the text direction) and the snap</span></p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">//&nbsp; point itself (for the text location)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Matrix3d</span> e2w = vd.Viewport.EyeToWorldTransform;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Vector3d</span> dir = <span style="color: #2b91af;">Vector3d</span>.XAxis.TransformBy(e2w);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Point3d</span> pt = _pt.TransformBy(e2w);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">//&nbsp; Draw the centered text representing the glyph</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; vd.Geometry.Text(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pt,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vd.Viewport.ViewDirection,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dir,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; glyphHeight,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 1,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 0,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; text</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; );</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; <span style="color: green;">// OSnap info</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">QuarterOsnapInfo</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SnapInfoEntity(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">ObjectSnapContext</span> context,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">ObjectSnapInfo</span> result)</p>
<p style="margin: 0px;">&nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Nothing here</span></p>
<p style="margin: 0px;">&nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SnapInfoCurve(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">ObjectSnapContext</span> context,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">ObjectSnapInfo</span> result</p>
<p style="margin: 0px;">&nbsp; &nbsp; )</p>
<p style="margin: 0px;">&nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// For any curve</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Curve</span> cv = context.PickedObject <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">if</span> (cv == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> startParam = cv.StartParam;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> endParam = cv.EndParam;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Add osnap at first quarter</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> param =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; startParam + ((endParam - startParam) * 0.25);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Point3d</span> pt = cv.GetPointAtParameter(param);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; result.SnapPoints.Add(pt);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Add osnap at third quarter</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; param =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; startParam + ((endParam - startParam) * 0.75);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; pt = cv.GetPointAtParameter(param);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; result.SnapPoints.Add(pt);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">if</span> (cv.Closed)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; pt = cv.StartPoint;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; result.SnapPoints.Add(pt);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> SnapInfoPolyline(</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">ObjectSnapContext</span> context,</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">ObjectSnapInfo</span> result)</p>
<p style="margin: 0px;">&nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// For polylines</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Polyline</span> pl = context.PickedObject <span style="color: blue;">as</span> <span style="color: #2b91af;">Polyline</span>;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">if</span> (pl == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Get the overall start and end parameters</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> plStartParam = pl.StartParam;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> plEndParam = pl.EndParam;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: green;">// Get the local </span></p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> startParam = plStartParam;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> endParam = startParam + 1.0;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; <span style="color: blue;">while</span> (endParam &lt;= plEndParam)</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; {</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">// Calculate the snap point per vertex...</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">// Add osnap at first quarter</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: blue;">double</span> param =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; startParam + ((endParam - startParam) * 0.25);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #2b91af;">Point3d</span> pt = pl.GetPointAtParameter(param);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; result.SnapPoints.Add(pt);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; <span style="color: green;">// Add osnap at third quarter</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; param =</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; startParam + ((endParam - startParam) * 0.75);</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; pt = pl.GetPointAtParameter(param);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; result.SnapPoints.Add(pt);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; startParam = endParam;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; &nbsp; endParam += 1.0;</p>
<p style="margin: 0px;">&nbsp; &nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp; &nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p>
</div>
<p><em><strong>Update 2:</strong></em></p>
<p>Please see <a href="http://through-the-interface.typepad.com/through_the_interface/2014/04/implementing-a-custom-autocad-object-snap-mode-using-net-redux.html" target="blank">this more recent post</a> for an updated solution to this problem.</p>
