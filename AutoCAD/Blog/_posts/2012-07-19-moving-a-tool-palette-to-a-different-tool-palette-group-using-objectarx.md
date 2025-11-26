---
layout: "post"
title: "Moving a tool palette to a different tool palette group using ObjectARX"
date: "2012-07-19 05:01:46"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/moving-a-tool-palette-to-a-different-tool-palette-group-using-objectarx.html "
typepad_basename: "moving-a-tool-palette-to-a-different-tool-palette-group-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is&nbsp;a sample ObjectARX code to move a palette belonging to a tool palette group to another group.</p>
<p>This code assumes that you have a palette called "MyPalette" that belongs to the "Annotation and Design" tool palette group.&nbsp;This palette will be moved&nbsp;to the "Parametric Design" tool palette group.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">CAcTcUiToolPaletteSet *pTPSet = AcTcUiGetToolPaletteWindow(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CAcTcUiToolPaletteGroup *pPaletteGroups </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = pTPSet-&gt;GetToolPaletteGroup(</span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Find the &quot;Annotation and Design&quot; tool palette group</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CAcTcUiToolPaletteGroup *pAnnotationDesignGrp </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = pPaletteGroups-&gt;FindGroup(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Annotation and Design&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Find the &quot;Parametric Design&quot; tool palette group</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CAcTcUiToolPaletteGroup *pParametricDesignGrp </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = pPaletteGroups-&gt;FindGroup(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Parametric Design&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pAnnotationDesignGrp != NULL &amp;&amp; pParametricDesignGrp != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Find the tool palette called &quot;MyPalette&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; CAcTcUiToolPalette *pMyPalette </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = pAnnotationDesignGrp-&gt;FindPalette</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;MyPalette&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; NULL</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pMyPalette != NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Remove the palette from the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// &quot;Annotation and Design&quot; tool palette group</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pAnnotationDesignGrp-&gt;RemoveItem(pMyPalette, TRUE);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Add the palette to the </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// &quot;Parametric Design&quot; tool palette group</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pParametricDesignGrp-&gt;AddItem(pMyPalette);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p></p>
<p>Unfortunately, at present it is not possible to do this using the AutoCAD .Net API since the class "CAcTcUiToolPaletteGroup" does not have an equivalent.</p>
