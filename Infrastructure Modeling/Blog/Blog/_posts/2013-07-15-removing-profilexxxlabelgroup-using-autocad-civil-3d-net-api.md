---
layout: "post"
title: "Removing ProfileXXXLabelGroup using AutoCAD Civil 3D .NET API"
date: "2013-07-15 04:23:32"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/07/removing-profilexxxlabelgroup-using-autocad-civil-3d-net-api.html "
typepad_basename: "removing-profilexxxlabelgroup-using-autocad-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD
Civil 3D, User Interface (UI) , we can select a Profile LabelGroup in the
Profile View and from the &quot;Edit Label Group&quot; tool / command we can
bring-up the &quot;Profile Label&quot; dialog box. In that dialog box, each
Type of Profile Label Groups are listed and using the UI tools we can Add or
Remove (X) a particular Type.</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e45ad16970b-pi" style="display: inline;"><img alt="ProfileLabelGroup" class="asset  asset-image at-xid-6a0167607c2431970b01901e45ad16970b" src="/assets/image_bb743b.jpg" title="ProfileLabelGroup" /></a></p>
<p>&#0160;</p>
<p>If you want
to programmatically remove / erase a particular Profile Label Group, the first
task is to identify the Profile Label Group Type. In the following example, we
can see <strong>ProfileCrestCurveLabelGroup</strong> Type which is listed as
<strong>AECC_VALIGNMENT_CRESTCURVE_LABEL_GROUP</strong> when we use the LIST command is selected
first and later removed just by calling the <strong>Erase()</strong> method.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// select a Label Object </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> peo = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select A Profile Label Group : &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">peo.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nOnly Label is allowed&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">peo.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(Autodesk.Civil.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">ProfileCrestCurveLabelGroup</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(peo);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK) </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%; background-color: #ffff00;">ProfileCrestCurveLabelGroup</span><span style="line-height: 140%;"> profCrestCurveLabelGroup = trans.GetObject(per.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ProfileCrestCurveLabelGroup</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; background-color: #ffff00;">&#0160; profCrestCurveLabelGroup.Erase();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>
