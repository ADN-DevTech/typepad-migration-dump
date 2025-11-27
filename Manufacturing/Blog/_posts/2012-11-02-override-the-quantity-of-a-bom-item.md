---
layout: "post"
title: "Override the Quantity of a BOM item"
date: "2012-11-02 07:13:46"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/11/override-the-quantity-of-a-bom-item.html "
typepad_basename: "override-the-quantity-of-a-bom-item"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m trying to override the Qty column of an McadBOMItem, but it does not work. <br />McadBOMItem.Quantity = 10 only seems to work if you first override the value through the user interface.</p>
<p><strong>Solution</strong></p>
<p>You need to make sure that&#0160;AutoCalculateEnabled is set to False before trying to override it:</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 9.5px; color: #b4261a;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #000000;">[</span><span style="color: #33a2bd;">CommandMethod</span><span style="color: #000000;">(</span>&quot;OverrideQuantity&quot;<span style="color: #000000;">)]</span></span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> OverrideQuantity()</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">{</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; <span style="color: #33a2bd;">AcadApplication </span><span style="color: #000000;">acadApp =&#0160;</span><span style="line-height: 120%; color: #000000;">(</span><span style="color: #33a2bd;">AcadApplication</span><span style="line-height: 120%; color: #000000;">)</span><span style="line-height: 120%; color: #000000;"><br />&#0160; &#0160; Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #33a2bd;">Application</span><span style="line-height: 120%; color: #000000;">.AcadApplication;</span></span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; <span style="color: #33a2bd;">McadSymbolBBMgr</span> symMgr = <br />&#0160; &#0160; acadApp.GetInterfaceObject(<span style="color: #b4261a;">&quot;SymBBAuto.McadSymbolBBMgr&quot;</span>);</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; <span style="color: #33a2bd;">McadBOMs</span> symBOMs = symMgr.BOMMgr.GetAllBOMTables(<span style="color: #0433ff;">true</span>);</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; <span style="color: #0433ff;">foreach</span> (<span style="color: #33a2bd;">McadBOM</span> symBOM <span style="color: #0433ff;">in</span> symBOMs)</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; {</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; &#0160; <span style="color: #0433ff;">foreach</span> (<span style="color: #33a2bd;">McadBOMItem</span> symBOMItem <span style="color: #0433ff;">in</span> symBOM.get_Items(<span style="color: #0433ff;">true</span>))</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; &#0160; &#0160; symBOMItem.AutoCalculateEnabled = <span style="color: #0433ff;">false</span>;</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; &#0160; &#0160; symBOMItem.Quantity = 20;</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">&#0160; }</span></p>
<p style="margin: 0px; font-size: 9.5px; font-family: Consolas;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">}</span></p>
</span>
