---
layout: "post"
title: "Get the Civil 3D Label Expression using .NET API"
date: "2014-01-08 01:51:40"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/01/get-the-civil-3d-label-expression-using-net-api.html "
typepad_basename: "get-the-civil-3d-label-expression-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D, by using expressions, we can set up separate mathematical formulas using the existing properties. Expressions are unique to a particular label style type. Only those properties that are relevant to the label style type are available in the Expressions dialog box.&#0160;</p>
<p>After you set up expressions, they are available in the Properties list in the Text Component Editor so that you can add them to label styles. In effect, expressions become new properties that you can use to compose a label style.</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fbdf4dfc970b-pi" style="display: inline;"><img alt="Lable_Expression" class="asset  asset-image at-xid-6a0167607c2431970b01a3fbdf4dfc970b" src="/assets/image_8627b6.jpg" title="Lable_Expression" /></a><br />&#0160;</p>
<p>&#0160;</p>
<p>Now if you want to access the expression formula / string used for a particular Label style using <a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">API</a>, its simple, you need to get the <strong>ExpressionCollection</strong> for that Label Styles collection.</p>
<p>Here is a C# .NET code snippet demonstrating how to access an expression formula used for AlignmentLabelStyles :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get the desired Lable Styles collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">LabelStyleCollection</span><span style="line-height: 140%;"> lblStyleColl = civilDoc.Styles.LabelStyles.AlignmentLabelStyles.GeometryPointLabelStyles;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Now get the Expression collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Expressions are unique to a particular label style type. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ExpressionCollection</span><span style="line-height: 140%;"> expColl = lblStyleColl.Expressions;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Expression</span><span style="line-height: 140%;"> lblExp </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> expColl)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Expression Name : &quot;</span><span style="line-height: 140%;"> + lblExp.Name);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Expression String : &quot;</span><span style="line-height: 140%;"> + lblExp.ExpressionString);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Exception message :&quot;</span><span style="line-height: 140%;"> + ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b046badc7970d-pi" style="display: inline;"><img alt="Lable_Expression_API" class="asset  asset-image at-xid-6a0167607c2431970b019b046badc7970d" src="/assets/image_408e6d.jpg" title="Lable_Expression_API" /></a><br /></span></p>
</div>
