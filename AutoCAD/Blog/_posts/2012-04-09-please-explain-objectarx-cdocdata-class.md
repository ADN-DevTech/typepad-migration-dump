---
layout: "post"
title: "Explaining the ObjectARX CDocData class"
date: "2012-04-09 14:34:37"
author: "Fenton Webb"
categories:
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/please-explain-objectarx-cdocdata-class.html "
typepad_basename: "please-explain-objectarx-cdocdata-class"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html" target="_self">Fenton Webb</a></p>
<p>If you are new to ObjectARX or have never seen it, the CDocData class may appear a little confusing to some. We use this class in some of our ObjectARX sample code to handle document specific data.</p>
<p>What is document specific data? The best way to explain it is to show you some code and the results of running the code…</p>
<p>First, lets input an integer and have a static variable that offers a default value…</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>static</span></span></span><span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">int</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;"> = 0;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>void</span></span></span><span><span style="color: #000000;"> </span><span><span style="color: #010001;">myCmd</span></span><span style="color: #000000;">()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">TCHAR</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">printBuffer</span></span><span style="color: #000000;">[256];</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">_stprintf</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">printBuffer</span></span><span style="color: #000000;">, </span><span><span style="color: #010001;">_T</span></span><span style="color: #000000;">(</span><span><span style="color: #a31515;">"\nEnter value &lt;%d&gt; : "</span></span><span style="color: #000000;">), </span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">acedGetInt</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">printBuffer</span></span><span style="color: #000000;">, &amp;</span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></p>
<p>If you load this code into AutoCAD and run it, it will first offer a default value of 0, and then offer any value you type in as the following default. If you create a new drawing, that default value is shown for each drawing.</p>
</div>
<p>Now to create some document specific data…</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>class</span></span></span><span><span style="color: #000000;"> </span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> {</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>public</span></span></span><span style="color: #000000;">:</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> ()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp;&nbsp;&nbsp; </span></span><span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;"> = 0;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp; };</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> (</span><span><span style="color: #0000ff;">const</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> &amp;</span><span><span style="color: #010001;">data</span></span><span style="color: #000000;">)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp;&nbsp;&nbsp; </span></span><span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;"> = </span><span><span style="color: #010001;">data</span></span><span style="color: #000000;">.</span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp; }</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; ~</span></span><span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> () ;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #0000ff;">int</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">} ;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span>AcApDataManager</span></span></span><span><span style="color: #000000;">&lt;</span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">&gt; </span><span><span style="color: #010001;">DocVars</span></span><span style="color: #000000;"> ;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>void</span></span></span><span><span style="color: #000000;"> </span><span><span style="color: #010001;">myCmd</span></span><span style="color: #000000;">()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">TCHAR</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">printBuffer</span></span><span style="color: #000000;">[256];</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">_stprintf</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">printBuffer</span></span><span style="color: #000000;">, </span><span><span style="color: #010001;">_T</span></span><span style="color: #000000;">(</span><span><span style="color: #a31515;">"\nEnter value &lt;%d&gt; : "</span></span><span style="color: #000000;">), </span><span><span style="color: #010001;">DocVars.</span></span><span><span style="color: #010001;">docData</span></span><span style="color: #000000;">().</span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">acedGetInt</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">printBuffer</span></span><span style="color: #000000;">, &amp;</span><span><span style="color: #010001;">DocVars.</span></span><span><span style="color: #010001;">docData</span></span><span style="color: #000000;">().</span><span><span style="color: #010001;">defaultValue</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
</div>
<p>If you load this code into AutoCAD and run it, it will first offer a default value of 0, and then offer any value you type in as the following default. However, this time, if you create a new drawing, you will be offered a 0 default and any value that you type in will be stored separately to any other document. Pretty cool.</p>
<p>As you can see this data is not persisted, and my example is very basic – where would this class really become useful? The answer is for document specific reactors, here’s an example..</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>class</span></span></span><span><span style="color: #000000;"> </span></span><span><span style="color: #010001;">CDocData</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>public</span></span></span><span style="color: #000000;">:</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">(</span><span><span style="color: #0000ff;">const</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> &amp;</span><span><span style="color: #010001;">data</span></span><span style="color: #000000;">) ;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; ~</span></span><span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">asdkMyDatabaseReactor</span></span><span style="color: #000000;">* </span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">};</span></span></p>
<div style="background: white;">
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="color: #008000;">//</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="color: #008000;">// Implementation of the document data class.</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="color: #008000;">//</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span>CDocData</span></span></span><span><span style="color: #000000;">::</span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;"> = </span><span><span style="color: #010001;">NULL</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span>CDocData</span></span></span><span><span style="color: #000000;">::</span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">(</span><span><span style="color: #0000ff;">const</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;"> &amp;</span><span><span style="color: #010001;">data</span></span><span style="color: #000000;">)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;"> = </span><span><span style="color: #010001;">NULL</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span>CDocData</span></span></span><span><span style="color: #000000;">::~</span><span><span style="color: #010001;">CDocData</span></span><span style="color: #000000;">()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span>&nbsp; </span></span><span><span><span style="color: #0000ff;">if</span></span><span style="color: #000000;"> (</span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;">) </span><span><span style="color: #0000ff;">delete</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></p>
<div style="background: white;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="color: #008000;">//</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="color: #008000;">// setting up the reactor – now it is contained by the document data, don’t need to worry about it anymore</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="color: #008000;">//</span></span></span></p>
<p style="margin: 0px;">DocVars</p>
</span></span><span><span style="color: #000000;">.</span><span><span style="color: #010001;">docData</span></span><span style="color: #000000;">().</span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;"> = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #010001;">asdkMyDatabaseReactor</span></span><span style="color: #000000;">();</span></span></span></span>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span>cuDoc</span></span></span><span><span style="color: #000000;">()-&gt;</span><span><span style="color: #010001;">database</span></span><span style="color: #000000;">()-&gt;</span><span><span style="color: #010001;">addReactor</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">DocVars</span></span><span style="color: #000000;">.</span><span><span style="color: #010001;">docData</span></span><span style="color: #000000;">().</span><span><span style="color: #010001;">m_pasdkMyDatabaseReactor</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;">&nbsp;</span></span></p>
</div>
</div>
</div>
