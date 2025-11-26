---
layout: "post"
title: "How to set Pipe Network Part's Data Fields (PartDataField)?"
date: "2012-09-24 02:56:11"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/how-to-set-pipe-network-parts-data-fields-partdatafield.html "
typepad_basename: "how-to-set-pipe-network-parts-data-fields-partdatafield"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><em>I am trying
to use Civil 3D .NET API to set the Pipe Object&#39;s PartDataField e.g. Pipe&#39;s
Manning Coefficient. Could you help me with a code snippet to do this?</em></p>
<p>&#0160;</p>
<p>Civil 3D .NET
API allows us to set / edit the <strong>PartDataField</strong> value. First you need to open the
Part (Pipe or Structure) for write, get a reference to the Part object&#39;s
PartData property (type <strong>PartDataRecord</strong>), modify the <strong>PartDatafield</strong> value, and
then re-set the Part&#39;s PartData. </p>
<p>&#0160;</p>
<p>Here is a C#
code snippet which demonstrates updating Pipe&#39;s Part Data values (Manning
Coefficient):</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> ts = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Pipe</span><span style="line-height: 140%;"> PipeEle = ts.GetObject(pipeID, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Pipe</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PartDataRecord</span><span style="line-height: 140%;"> record = PipeEle.PartData;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PartDataField</span><span style="line-height: 140%;"> PartDataFld = record.GetDataFieldBy(</span><span style="color: #2b91af; line-height: 140%;">PartContextType</span><span style="line-height: 140%;">.FlowAnalysisManning);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nManning Coefficient Value Before Change : &quot;</span><span style="line-height: 140%;"> + PartDataFld.Value.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Change the inner data of the copy:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; PartDataFld.Value = 0.014;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Re-set the PartData property.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//The data will not be updated without this </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; PipeEle.PartData = record;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nManning Coefficient Value After Change : &quot;</span><span style="line-height: 140%;"> + PartDataFld.Value.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ts.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee3bce214970d-pi" style="display: inline;"><img alt="Pipe_PartDataUpdate" class="asset  asset-image at-xid-6a0167607c2431970b017ee3bce214970d" src="/assets/image_39142e.jpg" title="Pipe_PartDataUpdate" /></a><br /><br /></span></p>
</div>
