---
layout: "post"
title: "Autodesk.Civil.DatabaseServices.PointGroupQueryParserException: Invalid value for property !"
date: "2012-09-11 02:00:54"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/autodeskcivildatabaseservicespointgroupqueryparserexception-invalid-value-for-property-.html "
typepad_basename: "autodeskcivildatabaseservicespointgroupqueryparserexception-invalid-value-for-property-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>While
creating a custom Point Group query using <strong>CustomPointGroupQuery</strong> class, when you
call the SetQuery() function with your custom query string sometimes you may
see the following exception message : <em><strong>&#0160;Invalid value
for property !</strong></em></p>
<p>Most likely
cause of this exception is improper query string formation. I will give you an example
here.</p>
<p>&#0160;</p>
<p>If we build a
<strong>StandardPointGroupQuery</strong> object, we can set a condition like the following and
all works fine :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">StandardPointGroupQuery</span><span style="line-height: 140%;"> standardPointGrpQry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">StandardPointGroupQuery</span><span style="line-height: 140%;">();&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">standardPointGrpQry.IncludeNumbers = </span><span style="color: #a31515; line-height: 140%;">&quot;550-560, 565-572&quot;</span><span style="line-height: 140%;">;</span></p>
</div>
<p>&#0160;</p>
<p>However, when
we build a <strong>CustomPointGroupQuery</strong>, and we try build a query string like the
following :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CustomPointGroupQuery</span><span style="line-height: 140%;"> customPointGrpQry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CustomPointGroupQuery</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> queryString = </span><span style="color: #a31515; line-height: 140%;">&quot;(RawDescription=&#39;TO*&#39;) AND (<span style="background-color: #ffff00;">PointNumber=&#39;600-1000&#39;</span>)&quot;</span><span style="line-height: 140%;">; </span><span style="color: green; line-height: 140%;">// Doesn&#39;t work</span></p>
</div>
<p>&#0160;</p>
<p>You would see
the excpetion message - &quot;<em><strong>Invalid value for property</strong></em> !&quot; when you try to
execute SetQuery(customPointGrpQry).</p>
<p>To include a
range of Points, you need to build query string like the following :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CustomPointGroupQuery</span><span style="line-height: 140%;"> customPointGrpQry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CustomPointGroupQuery</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> queryString = </span><span style="color: #a31515; line-height: 140%;">&quot;(RawDescription=&#39;TO*&#39;) AND (<span style="background-color: #80ff00;">PointNumber&gt;=600 AND PointNumber&lt;=1000</span>) &quot;</span><span style="line-height: 140%;">; </span><span style="color: green; line-height: 140%;">// this works</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is
useful to you !</p>
