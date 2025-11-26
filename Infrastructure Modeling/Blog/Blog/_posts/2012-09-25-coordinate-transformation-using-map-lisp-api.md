---
layout: "post"
title: "Coordinate Transformation using Map LISP API"
date: "2012-09-25 23:40:21"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "AutoLISP"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/coordinate-transformation-using-map-lisp-api.html "
typepad_basename: "coordinate-transformation-using-map-lisp-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>How to use
Map LISP function <strong>ade_projptforward</strong> to achieve coordinate transformation?</p>
<pre>&#0160;</pre>
<p>Here is a
LISP code snippet which demonstrates the same :</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">
</span></p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><br />
<span style="background-color: #e6e6e6; color: #800080;">;<span style="background-color: #e6e6e6; color: #800080;">;Coordinate&#0160;transform&#0160;with&#0160;LISP.<br /></span></span>
<br />
<span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span>&#0160;c:test&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">/</span>&#0160;result&#0160;pt<span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">(</span>ade_projsetsrc&#0160;<span style="color: #ff00ff;">&quot;LL&quot;</span><span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">(</span>ade_projsetdest&#0160;<span style="color: #ff00ff;">&quot;ITALY-W-ROME&quot;</span><span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span>&#0160;result<span style="color: #ff0000;">(</span>ade_projptforward&#0160;&#39;<span style="color: #ff0000;">(</span>10.0&#0160;20.0&#0160;0.0<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">(</span><span style="color: #0000ff;">if</span><span style="color: #ff0000;">(</span>null&#0160;result<span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">(</span>prompt&#0160;<span style="color: #ff00ff;">&quot;\nError&#0160;in Transformation &quot;</span><span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span>&#0160;result<span style="color: #ff0000;">)</span><br />
<span style="color: #ff0000;">)</span><span style="background-color: #e6e6e6; color: #800080;">;</span></span></p>
