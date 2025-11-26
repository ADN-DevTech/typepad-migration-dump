---
layout: "post"
title: "User warning: assignment to protected symbol - when using vlax-import-type-library"
date: "2012-06-24 16:16:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/user-warning-assignment-to-protected-symbol-when-using-vlax-import-type-library.html "
typepad_basename: "user-warning-assignment-to-protected-symbol-when-using-vlax-import-type-library"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When I load my dll then I get this warning:</p>
<p><span style="background-color: #e6e6e6;">; User warning: assignment to protected symbol: CLOSE &lt;- #</span></p>
<p>Also, when loading the dll for the second time, I get similar warnings for all the functions the dll contains.</p>
<p>Shall I just ignore these warnings?</p>
<p><strong>Solution</strong></p>
<p>You can and should avoid these warnings by using prefixes for the imported dll methods/properties/constants.</p>
<p>The best things is to use your registered developer symbol plus m/p/c depending on if it&#39;s for methods, properties or constants.</p>
<p>Also you can find out if the dll has already been loaded by checking if any of the methods/properties/constants are available (i.e. does not equal nil), and if they are not, only then load your dll.</p>
<p>Here is a sample code using my registered developer symbol, AEN1:</p>
<p style="line-height: 120%; font-family: &#39;courier new&#39;, courier;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span> c:loadmydll <span style="color: #ff0000;">( ) </span><br /><span><span style="color: #ff0000;">&#0160;(</span><span style="color: #0000ff;">vl-load-com</span><span style="color: #ff0000;">)</span></span><br /><span>&#0160;</span><span style="color: #bf00bf; background-color: #e6e6e6;">; Check if a specific function that is part of the dll</span><br />&#0160;<span style="color: #bf00bf; background-color: #e6e6e6;">; if not, then the dll is not yet loaded</span><br />&#0160;<span style="color: #bf00bf; background-color: #e6e6e6;">; so we load it now</span><span style="color: #bf00bf;"><span>&#0160;<br />&#0160;<span style="background-color: #e6e6e6;">; This specific dll contains a function called close,</span>&#0160;<br /></span></span><span style="color: #bf00bf;"><span>&#0160;</span></span><span style="color: #bf00bf; background-color: #e6e6e6;">; so I&#39;m checking for that</span><span style="color: #ff0000;">&#0160; <br />&#0160;(</span><span style="color: #0000ff;">if</span> <span style="color: #ff0000;">(</span><span style="color: #0000ff;">equal nil</span> aen1m-close<span style="color: #ff0000;">)</span><br /><span><span style="color: #ff0000;">&#0160; (</span><span style="color: #0000ff;">vlax-import-type-library</span></span><br /><span><span style="color: #0000ff;">&#0160; &#0160;:tlb-filename</span> <span style="color: #ff40ff;">&quot;C:/Test/lispCOM.dll&quot;</span></span><br /><span><span style="color: #0000ff;">&#0160; &#0160;:methods-prefix</span> <span style="color: #ff40ff;">&quot;aen1m-&quot;</span></span><br /><span><span style="color: #0000ff;">&#0160; &#0160;:properties-prefix</span> <span style="color: #ff40ff;">&quot;aen1p-&quot;</span></span><br /><span><span style="color: #0000ff;">&#0160; &#0160;:constants-prefix</span> <span style="color: #ff40ff;">&quot;aen1c-&quot;</span></span><br /><span style="color: #ff0000;">&#0160; )</span><br /><span style="color: #ff0000;">&#0160;) &#0160;&#0160;</span><br /><span style="color: #ff0000;"> )</span></p>
