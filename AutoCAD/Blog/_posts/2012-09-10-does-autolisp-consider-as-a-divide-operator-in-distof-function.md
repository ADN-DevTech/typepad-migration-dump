---
layout: "post"
title: "Does AutoLISP consider '/' as a divide operator in distof function?"
date: "2012-09-10 03:57:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/does-autolisp-consider-as-a-divide-operator-in-distof-function.html "
typepad_basename: "does-autolisp-consider-as-a-divide-operator-in-distof-function"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong></p>
<p>Does the distof function interpret the / divide operator and not any other arithmetic operator?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>In this context, / is not interpreted as a divide operator.&#0160; It actually    <br />represents the separator for the fraction.&#0160; The manner in which the distof function uses the divide operator in this situation is as designed.&#0160; For example:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">Command: <span style="color: #ff0000;">(</span>distof &quot;100<span style="color: #0000ff;">/</span>2&quot; <span style="color: #008000;">2</span><span style="color: #ff0000;">)</span>      <br />50.0</span></p>
The distof function in this case will not return nil, and the value the function returns is correct because &quot;100/2&quot; is a valid fraction.
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">Command: <span style="color: #ff0000;">(</span>distof &quot;100 100<span style="color: #0000ff;">/</span>2&quot; <span style="color: #008000;">4</span><span style="color: #ff0000;">)</span>      <br />nil</span></p>
In this case, because &quot;100 100/2&quot; is not a valid architectural fraction, the distof function returns nil.
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">Command: <span style="color: #ff0000;">(</span>distof &quot;100 2<span style="color: #0000ff;">/</span>100&quot; <span style="color: #008000;">4</span><span style="color: #ff0000;">)</span>      <br />100.02</span></p>
Because &quot;100 2/100&quot; is a valid fraction, the distof function returns 100.02.
