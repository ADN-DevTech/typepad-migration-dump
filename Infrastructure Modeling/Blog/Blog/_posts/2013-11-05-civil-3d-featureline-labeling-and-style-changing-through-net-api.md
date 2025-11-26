---
layout: "post"
title: "Civil 3D FeatureLine Labeling and Style changing through .NET API"
date: "2013-11-05 02:55:35"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/civil-3d-featureline-labeling-and-style-changing-through-net-api.html "
typepad_basename: "civil-3d-featureline-labeling-and-style-changing-through-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are
looking for an API to change AutoCAD Civil 3D FeatureLine object&#39;s style you
can use <strong>FeatureLine.StyleName</strong> property which <em><strong>gets</strong></em> or<em><strong> sets</strong></em> the Featureline&#39;s
style name.&#0160;</p>
<p>And using
<strong>Autodesk.Civil.DatabaseServices.GeneralSegmentLabel.Create</strong>() method we can add
Label to a FeatureLine.&#0160;</p>
<p>In the
screenshot below we can see a Civil 3D FeatureLine before changing it&#39;s style -</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00af8bc3970b-pi" style="display: inline;"><img alt="Civil3D_FeatureLine_Style" class="asset  asset-image at-xid-6a0167607c2431970b019b00af8bc3970b" src="/assets/image_8eb535.jpg" title="Civil3D_FeatureLine_Style" /></a></p>
<p>&#0160;</p>
<p>And using the
following C# .NET code we can change the style and label it - </p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> ts = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">FeatureLine</span><span style="line-height: 140%;"> featureLine = ts.GetObject(ftrLineID, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">FeatureLine</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Change the Style to &quot;Flowline&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Style name used abobe is specific to a DWG file </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// FeatureLine.StyleName // Gets or sets the Featureline&#39;s style name.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; featureLine.StyleName = </span><span style="color: #a31515; line-height: 140%;">&quot;Flowline&quot;</span><span style="line-height: 140%;">;</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Use Autodesk.Civil.DatabaseServices.GeneralSegmentLabel.Create to add label to a feature line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="background-color: #ffff00;"><span style="color: #2b91af; line-height: 140%;">GeneralSegmentLabel</span><span style="line-height: 140%;">.Create(featureLine.ObjectId, 0.5);</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ts.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>FeatureLine
style after change -</p>
<p>&#0160;</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00af8cdc970b-pi" style="display: inline;"><img alt="Civil3D_FeatureLine_Style_Changed" class="asset  asset-image at-xid-6a0167607c2431970b019b00af8cdc970b" src="/assets/image_987e99.jpg" title="Civil3D_FeatureLine_Style_Changed" /></a></p>
<p>&#0160;</p>
<p>and
FeatureLine with label -</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00af574d970c-pi" style="display: inline;"><img alt="Civil3D_FeatureLine_Label_using_API" class="asset  asset-image at-xid-6a0167607c2431970b019b00af574d970c" src="/assets/image_ecce49.jpg" title="Civil3D_FeatureLine_Label_using_API" /></a></p>
Hope this is useful
to you.
