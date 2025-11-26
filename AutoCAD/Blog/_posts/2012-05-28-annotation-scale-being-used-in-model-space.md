---
layout: "post"
title: "Annotation scale being used in Model Space"
date: "2012-05-28 07:54:17"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/annotation-scale-being-used-in-model-space.html "
typepad_basename: "annotation-scale-being-used-in-model-space"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I'm trying to get back the annotation scale being used in Model Space.</p>
<p>The below picture shows what I'm looking for:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766df1e75970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016766df1e75970b" alt="Cannoscale" title="Cannoscale" src="/assets/image_132327.jpg" border="0" /></a><br />
<p><strong>Solution</strong></p>
<p>If you change the Annotation Scale value in Model Space, then you'll see that the _CANNOSCALE command is being used in the background. Now if you look for that name in the Visual Studio Object Browser, then you'll find <strong>Database.Cannoscale</strong>, which is the variable you are looking for:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[CommandMethod(</span><span style="color: #a31515; line-height: 140%;">"AEN1GetAnnotationScale"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AEN1GetAnnotationScale()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Database db = HostApplicationServices.WorkingDatabase; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">"Current annotation scale is: "</span><span style="line-height: 140%;"> + db.Cannoscale.Name + </span><span style="color: #a31515; line-height: 140%;">"\n"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
