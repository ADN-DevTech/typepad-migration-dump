---
layout: "post"
title: "Remove insertion grip point using overrule"
date: "2012-11-23 09:15:30"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/remove-insertion-grip-point-using-overrule.html "
typepad_basename: "remove-insertion-grip-point-using-overrule"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you think that the insertion grip point of a block reference is in the way then you can use GripOverrule to remove it. <br />The following sample only removes the insertion grip point of dynamic block references:</p>
<span style="font-family: &#39;courier new&#39;, courier; line-height: 120%; font-size: 11px;">
<p style="margin: 0px;"><span style="color: #0433ff;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="margin: 0px; min-height: 12px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #0433ff;">namespace</span> ClassLibrary1</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px; color: #0433ff;"><span style="color: #000000;">&#0160; </span>public<span style="color: #000000;"> </span>class<span style="color: #000000;"> </span><span style="color: #33a2bd;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px; color: #33a2bd;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #0433ff;">public</span><span style="color: #000000;"> </span><span style="color: #0433ff;">class</span><span style="color: #000000;"> </span>MyGripOverrule<span style="color: #000000;"> : </span>GripOverrule</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">override</span> <span style="color: #0433ff;">void</span> GetGripPoints(</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Entity</span> entity, <span style="color: #33a2bd;">GripDataCollection</span> grips,&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">double</span> curViewUnitSize, <span style="color: #0433ff;">int</span> gripSize,&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Vector3d</span> curViewDir, <span style="color: #33a2bd;">GetGripPointsFlags</span> bitFlags)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; color: #008f00;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// It should not be anything else, since we are</p>
<p style="margin: 0px; color: #008f00;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// filtering for block references</p>
<p style="margin: 0px; color: #33a2bd;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>BlockReference<span style="color: #000000;"> br = (</span>BlockReference<span style="color: #000000;">)entity;</span></p>
<p style="margin: 0px; min-height: 12px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">base</span>.GetGripPoints(entity, grips, <br />&#0160; &#0160; &#0160; &#0160; &#0160; curViewUnitSize, gripSize,</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; curViewDir, bitFlags);&#0160;</p>
<p style="margin: 0px; min-height: 12px;">&#0160;</p>
<p style="margin: 0px; color: #008f00;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// We&#39;ll only remove it for dynamic blocks</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (br.IsDynamicBlock)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">GripData</span> toRemove = <span style="color: #0433ff;">null</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">foreach</span> (<span style="color: #33a2bd;">GripData</span> gd <span style="color: #0433ff;">in</span> grips)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (gd.GripPoint == br.Position)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; toRemove = gd;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">break</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; min-height: 12px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (toRemove != <span style="color: #0433ff;">null</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; grips.Remove(toRemove);&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px; min-height: 12px;">&#0160;</p>
<p style="margin: 0px; color: #b4261a;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #33a2bd;">CommandMethod</span><span style="color: #000000;">(</span>&quot;RemoveInsertionPoint&quot;<span style="color: #000000;">)]</span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">static</span> <span style="color: #0433ff;">void</span> RemoveInsertionPoint()</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #33a2bd;">Overrule</span>.AddOverrule(</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">RXClass</span>.GetClass(<span style="color: #0433ff;">typeof</span>(<span style="color: #33a2bd;">BlockReference</span>)),&#0160;</p>
<p style="margin: 0px; color: #33a2bd;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #0433ff;">new</span><span style="color: #000000;"> </span>MyGripOverrule<span style="color: #000000;">(), </span><span style="color: #0433ff;">true</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; );</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; <span style="color: #33a2bd;">Overrule</span>.Overruling = <span style="color: #0433ff;">true</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</span>
