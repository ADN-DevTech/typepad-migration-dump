---
layout: "post"
title: "Do you want to programmatically convert polyline to Civil 3D Alignment object?"
date: "2012-06-20 01:32:37"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/do-you-want-to-programmatically-convert-polyline-to-civil-3d-alignment-object.html "
typepad_basename: "do-you-want-to-programmatically-convert-polyline-to-civil-3d-alignment-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you have a requirement to convert polyline to Civil 3D Alignment object programmatically i.e. using Civil 3D API, you are at the right place :)&#0160;</p>
<p>This VB.NET code snippet demonstrates how to create (or convert) a Polyline to a Civil 3D Alignment object.&#0160;</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Select the Polyline</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> promptEntOp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Select a Polyline : &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> promptEntRs </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; promptEntRs = ed.GetEntity(promptEntOp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> promptEntRs.Status &lt;&gt; </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Exiting! Try Again !&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> idEnt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> = promptEntRs.ObjectId&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> poptions </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Civil.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">PolylineOptions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; poptions.AddCurvesBetweenTangents = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; poptions.EraseExistingEntities = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; poptions.PlineId = idEnt</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Check the Alignment Style Name and labelSetName </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; To use the following default names you can use the &quot;Align-6A.dwg &quot; from Help\Civil Tutorials\Drawings\</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; and make sure to create a empty Site named &quot;Site 1&quot;(if it is already not there in the DWG file), also draw a polyline to convert it to Alignment.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> alignId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">&#0160; &#0160; &#0160; alignId = </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">.<span style="background-color: #ffff00;">Create</span>(civilDoc, poptions, </span><span style="color: #a31515; line-height: 140%;">&quot;Test_Alignment&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;Site 1&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;0&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;Design Style&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;All Labels&quot;</span><span style="line-height: 140%;">)</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; trans.Commit()</span></p>
</div>
<p>Once you build your application using the above code snippet and run the custom command in Civil 3D 2013, you should see the Alignment created (like below ) :</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016306bf0669970d-pi" style="display: inline;"><img alt="Alignment" class="asset  asset-image at-xid-6a0167607c2431970b016306bf0669970d" src="/assets/image_cb6f5a.jpg" title="Alignment" /></a></p>
<p>&#0160;</p>
<p>Hope this helps!</p>
<p>&#0160;</p>
