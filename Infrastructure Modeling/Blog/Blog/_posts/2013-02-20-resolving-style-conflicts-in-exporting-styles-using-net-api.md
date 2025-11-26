---
layout: "post"
title: "Resolving Style conflicts in exporting Civil 3D Object styles using .NET API"
date: "2013-02-20 06:22:50"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/resolving-style-conflicts-in-exporting-styles-using-net-api.html "
typepad_basename: "resolving-style-conflicts-in-exporting-styles-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>StyleConflictResolverType
</strong>Enumeration specifies how to resolve conflicts (the same name for an existing
style and a new imported style) when exporting styles to another drawing using
StyleBase::<em><strong>ExportTo()</strong></em>.&#0160;</p>
<p>Members
of&#0160; StyleConflictResolverType Enumeration
-&#0160;</p>
<pre>CancelRemaining<br />Rename<br />Ignore<br />Override</pre>
<p>Here is a C#
.NET code snippet which demonstrates how to use
Autodesk.Civil.<strong>StyleConflictResolverType</strong> in resolving conflicts -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">LabelStyleCollection</span><span style="line-height: 140%;"> lStyles = civilDoc.Styles.LabelStyles.PointLabelStyles.LabelStyles;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> stId </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> lStyles)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;"> pSt = trans.GetObject(stId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Export the style</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff40;"><span style="line-height: 140%;">&#0160; pSt.ExportTo(outDb, Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">StyleConflictResolverType</span><span style="line-height: 140%;">.Override);</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Now export its children</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; pSt.ChildrenCount; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;"> chSt = trans.GetObject(pSt[i], </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff40;"><span style="line-height: 140%;">&#0160; &#0160; chSt.ExportTo(outDb, Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">StyleConflictResolverType</span><span style="line-height: 140%;">.Override);</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Hope this is useful to you!</p>
