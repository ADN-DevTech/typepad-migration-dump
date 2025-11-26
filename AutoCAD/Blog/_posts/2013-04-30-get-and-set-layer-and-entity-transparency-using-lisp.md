---
layout: "post"
title: "Get and set layer and entity transparency using LISP"
date: "2013-04-30 03:12:10"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/get-and-set-layer-and-entity-transparency-using-lisp.html "
typepad_basename: "get-and-set-layer-and-entity-transparency-using-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Why doesn&#39;t the following code work? vla-get-transparency highlights as blue in the VLIDE, but I get this error:</p>
<p>&quot;Error: ActiveX Server returned the error: unknown name: Transparency&quot;</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">(setq ENT (entsel))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(if ENT</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; (progn</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; (setq VLA_OBJ_NAME (vlax-ename-&gt;vla-object (car ENT))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; VLA_OBJ_LAYER (vla-get-layer VLA_OBJ_NAME)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; VLA_ACT_DOC (vla-get-ActiveDocument (vlax-get-Acad-Object))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; LAYERNAME (vla-Item (vla-get-Layers VLA_ACT_DOC) VLA_OBJ_LAYER)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; (if (&gt;= (atof (substr (getvar &quot;acadver&quot;) 1 4)) 18.1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; (setq PROPTRAN (vla-get-transparency LAYERNAME))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
<p><strong>Solution</strong></p>
<p>The properties that you access when using the COM helpers of LISP are based on the ActiveX API.</p>
<p>If you look for the Transparency property in the ActiveX API, then you’ll find that it is only available for AcadRasterImage and AcadWipeout entities.</p>
<p>The Transparency for entities (which is only available in AutoCAD 2011) is provided by new interfaces like IAcadEntity2 and they are called EntityTransparency.</p>
<p>In case of layers however, there is no property like that.</p>
<p>That transparency value can be calculated from the layer’s XData. If it’s not there, then it’s the default 0%</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="background-color: #e6e6e6; color: #800080;">;&#0160;gets&#0160;transparency&#0160;in&#0160;percentage&#0160;<br /></span>
<span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span>&#0160;getLayerTransparency&#0160;<span style="color: #ff0000;">(</span>layerName&#0160;<span style="color: #0000ff;">/</span>&#0160;layer&#0160;transparency<span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;layer&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">tblobjname</span>&#0160;<span style="color: #ff00ff;">&quot;LAYER&quot;</span>&#0160;layerName<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;get&#0160;the&#0160;XData&#0160;of&#0160;AcCmTransparency&#0160;&#0160;&#0160;&#0160;<br /></span>
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;transparency&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">cdr</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">assoc</span>&#0160;<span style="color: #008000;">1071</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">cdar</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">cdr</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">assoc</span>&#0160;-3&#0160;<br />
&#0160;&#0160;&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">entget</span>&#0160;layer&#0160;&#39;<span style="color: #ff0000;">(</span><span style="color: #ff00ff;">&quot;AcCmTransparency&quot;</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">if</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">=</span>&#0160;transparency&#0160;<span style="color: #0000ff;">nil</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;&#0160;&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;if&#0160;we&#0160;did&#0160;not&#0160;get&#0160;a&#0160;value&#0160;it&#0160;must&#0160;be&#0160;the&#0160;default&#0160;0%&#0160;<br /></span>
&#0160;&#0160;&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;transparency&#0160;<span style="color: #008000;">0</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;&#0160;&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;if&#0160;we&#0160;got&#0160;a&#0160;value&#0160;then&#0160;calculate&#0160;from&#0160;it&#0160;<br /></span>
&#0160;&#0160;&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">progn</span>&#0160;<br />
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;get&#0160;the&#0160;lower&#0160;byte&#0160;of&#0160;the&#0160;value&#0160;0..255&#0160;<br /></span>
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;<span style="color: #ff0000;">(</span>100%..0%&#0160;in&#0160;the&#0160;AutoCAD&#0160;user&#0160;interface<span style="color: #ff0000;">)</span>&#0160;<br /></span>
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;transparency&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">lsh</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">lsh</span>&#0160;transparency&#0160;<span style="color: #008000;">24</span><span style="color: #ff0000;">)</span>&#0160;<span style="color: #008000;">-24</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;convert&#0160;the&#0160;value&#0160;to&#0160;a&#0160;percentage&#0160;<br /></span>
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;transparency&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">fix</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">-</span>&#0160;<span style="color: #008000;">100</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">/</span>&#0160;transparency&#0160;<span style="color: #008080;">2.55</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;&#0160;&#0160;&#0160;<br />
&#0160;&#0160;&#0160;&#0160;<span style="color: #ff0000;">)</span>&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">progn</span>&#0160;<br /></span>
&#0160;&#0160;<span style="color: #ff0000;">)</span>&#0160;<span style="background-color: #e6e6e6; color: #800080;">;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">if</span>&#0160;&#0160;<br /></span>
<span style="color: #ff0000;">)</span><br />
<br />
<span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span>&#0160;c:testGet&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">/</span>&#0160;ent&#0160;layerName&#0160;transparency<span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;ent&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">car</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">entsel</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;layerName&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">cdr</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">assoc</span>&#0160;<span style="color: #008000;">8</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">entget</span>&#0160;ent<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;transparency&#0160;<span style="color: #ff0000;">(</span>getLayerTransparency&#0160;layerName<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span>&#0160;transparency<span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span><span style="color: #ff0000;">)</span>&#0160;<br />
<span style="color: #ff0000;">)</span></span></p>
<p>It does not seem possible to set the value the same way - by adjusting the XData value -, but you can use the _LAYER command for that:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span>&#0160;c:testSet&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">/</span>&#0160;ent&#0160;layerName&#0160;transparency<span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;ent&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">car</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">entsel</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;transparency&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">getint</span>&#0160;<span style="color: #ff00ff;">&quot;Transparency&#0160;value&quot;</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;layerName&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">cdr</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">assoc</span>&#0160;<span style="color: #008000;">8</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">entget</span>&#0160;ent<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;_LAYER&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;_TR&quot;</span>&#0160;transparency&#0160;layerName&#0160;<span style="color: #ff00ff;">&quot;&quot;</span><span style="color: #ff0000;">)</span>&#0160;<br />
&#0160;&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span><span style="color: #ff0000;">)</span>&#0160;<br />
<span style="color: #ff0000;">)</span></span></p>
<p>Code has been updated based on BlackBox&#39;s comments. Now it is not using unnecessary ActiveX function calls.</p>
