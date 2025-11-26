---
layout: "post"
title: "Creating AutoCAD Civil 3D Pipe Label using .NET API"
date: "2014-03-03 03:05:12"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/03/creating-autocad-civil-3d-pipe-label-using-net-api.html "
typepad_basename: "creating-autocad-civil-3d-pipe-label-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D we can conveniently add labels to Pipe / PipeNetwork using UI tools / commands as shown in the screenshot below :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d8600b9970d-pi" style="display: inline;"><img alt="Civil3D_Pipe_Label_UI_Tools" class="asset  asset-image at-xid-6a0167607c2431970b01a73d8600b9970d img-responsive" src="/assets/image_35df7e.jpg" title="Civil3D_Pipe_Label_UI_Tools" /></a><br />&#0160;</p>
<p>&#0160;</p>
<p>If you want to add Labels to Civil 3D Pipe objects without any user interaction, you can use <strong>PipeLabel.Create()</strong> .NET API.</p>
<p>Here is a C# .NET code snippet demonstrating the how to add a Pipe Label :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pipeID = ed.GetEntity(opt).ObjectId;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> civildoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> ts = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// We will use PipeLabel.Create()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// public static ObjectId Create(&#0160; ObjectId pipeId,&#0160; double ratio,&#0160; ObjectId labelStyleId )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// pipeId - Type: ObjectId // The ObjectId of Pipe on which the label is located.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// ratio - Type: System.Double // The relative position of the label to the pipe.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// labelStyleId - Type: ObjectId // The ObjectId of a PipeLabel style to use.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> labelStyleId = civildoc.Styles.LabelStyles.PipeLabelStyles.PlanProfileLabelStyles[0];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">PipeLabel</span></strong></span><span style="line-height: 140%;"><span style="background-color: #ffff00;"><strong>.Create(pipeID, 0.5, labelStyleId);&#0160; &#0160; &#0160;</strong></span> &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ts.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>And the result in Civil 3D :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5117ac300970c-pi" style="display: inline;"><img alt="Civil3D_Pipe_Label_Using_API" class="asset  asset-image at-xid-6a0167607c2431970b01a5117ac300970c img-responsive" src="/assets/image_5fe111.jpg" title="Civil3D_Pipe_Label_Using_API" /></a></p>
