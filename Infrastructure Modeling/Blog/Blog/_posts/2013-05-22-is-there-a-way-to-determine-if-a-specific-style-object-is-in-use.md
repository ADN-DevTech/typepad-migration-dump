---
layout: "post"
title: "Is there a way to determine if a specific Style object is in-use?"
date: "2013-05-22 22:34:19"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/05/is-there-a-way-to-determine-if-a-specific-style-object-is-in-use.html "
typepad_basename: "is-there-a-way-to-determine-if-a-specific-style-object-is-in-use"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In Civil 3D
UI, if we expand the styles collection of a Civil 3D object, you will notice
the yellow triangle next to it which indicates this style is &#39;in-use&#39;, e.g. in
the screenshot we see the style collection of Surface styles which are in use -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aa37999e970d-pi" style="display: inline;"><img alt="Styles_01" class="asset  asset-image at-xid-6a0167607c2431970b0192aa37999e970d" src="/assets/image_4df37a.jpg" title="Styles_01" /></a></p>
<p>&#0160;</p>
<p>In Civil 3D
2014 .NET API, we have a very useful API <strong>StyleBase.IsUsed</strong> which will tell us if
a particular style is in-use. &#0160;Here is a
relevant C# code snippet :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceStyleId </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> civilDoc.Styles.SurfaceStyles)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//&#0160; Open each style object to check if it is &quot;in-use&quot;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SurfaceStyle</span><span style="line-height: 140%;"> csStyle = surfaceStyleId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SurfaceStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (<span style="background-color: #ffff00;">csStyle.IsUsed</span>)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; surfaceStyleNameColl = surfaceStyleNameColl + </span><span style="color: #a31515; line-height: 140%;">&quot;\n &quot;</span><span style="line-height: 140%;"> + csStyle.Name.ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191026f2318970c-pi" style="display: inline;"><img alt="Styles_02" class="asset  asset-image at-xid-6a0167607c2431970b0191026f2318970c" src="/assets/image_b84a38.jpg" title="Styles_02" /></a><br /></span></p>
<p style="margin: 0px;">&#0160;</p>
<p>&#0160;</p>
</div>
