---
layout: "post"
title: "Change Alignment Curve Label style using Civil 3D .NET API"
date: "2012-03-22 09:50:51"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/03/change-alignment-curve-label-style-using-civil-3d-net-api.html "
typepad_basename: "change-alignment-curve-label-style-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Do you want to change the Label style of an Alignment curve element using Civil 3D .NET AP ? Here is the C# code snippet which demonstrates how to do this :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ChangeAlignmentCurveLblStyle()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> pops = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">"Select Alignment Curve Label : "</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pops.AllowNone = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> pres = ed.GetEntity(pops);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pres.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr = </span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">AlignmentCurveLabel</span><span style="line-height: 140%;"> alignCurveLbl = (</span><span style="color: #2b91af; line-height: 140%;">AlignmentCurveLabel</span><span style="line-height: 140%;">)tr.GetObject(pres.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"\nAlignment Curve Label Style Name (before Change) : "</span><span style="line-height: 140%;"> + alignCurveLbl.StyleName.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// The following Style Name is specific to Tutorial DWG file - "Labels-6a.dwg"</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; alignCurveLbl.StyleName = </span><span style="color: #a31515; line-height: 140%;">"Design Data"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"\nAlignment Curve Label Style Name (after Change) : "</span><span style="line-height: 140%;"> + alignCurveLbl.StyleName.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">MessageBox</span><span style="line-height: 140%;">.Show(e.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #0000ff;"><span style="line-height: 15px;"><br /></span></span></p>
</div>
<p>After executing the command in AutoCAD Civil 3D you will see a result similar to the screenshot below –&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e918fb77970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168e918fb77970c image-full" title="C3D_Alignment_CurveLbl" src="/assets/image_cc4f59.jpg" border="0" alt="C3D_Alignment_CurveLbl" /></a><br /><br /></p>
<p><br /><br /></p>
