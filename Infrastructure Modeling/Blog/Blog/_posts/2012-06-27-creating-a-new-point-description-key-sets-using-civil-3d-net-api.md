---
layout: "post"
title: "Creating a new \"Point Description Key sets\" using Civil 3D .Net API"
date: "2012-06-27 03:13:16"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/creating-a-new-point-description-key-sets-using-civil-3d-net-api.html "
typepad_basename: "creating-a-new-point-description-key-sets-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In this post, I will show you how to create a new &quot;Point Description Key sets&quot; and add Point code using wildcards &quot;*&quot;.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Access the collection of all&#0160; Description key sets in a document&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PointDescriptionKeySetCollection</span><span style="line-height: 140%;"> pointDescKeySetColl = </span><span style="color: #2b91af; line-height: 140%;">PointDescriptionKeySetCollection</span><span style="line-height: 140%;">.GetPointDescriptionKeySets(db);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Add a new Descp. Key Sets</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> PointDescKeySetsId = pointDescKeySetColl.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;ADN_Point_Desc_Key&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PointDescriptionKeySet</span><span style="line-height: 140%;"> pointDescKeySet = trans.GetObject(PointDescKeySetsId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PointDescriptionKeySet</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Create a new key in the set with code = &quot;TR*&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// The wildcards “?” and “*” are allowed in the code. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pointDescKeyId = pointDescKeySet.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;TR*&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PointDescriptionKey</span><span style="line-height: 140%;"> pointDescKey = trans.GetObject(pointDescKeyId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PointDescriptionKey</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// set a specific Style and Label style</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// in the following line we are presuming a Point Style named &quot;Tree&quot; exists in the DWG file&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointDescKey.StyleId = civilDoc.Styles.PointStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Tree&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointDescKey.ApplyStyleId = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// in the following line we are presuming a Point Label Style </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// named &quot;Point#-Elevation-Description&quot; exists in the DWG file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointDescKey.LabelStyleId = civilDoc.Styles.LabelStyles.PointLabelStyles.LabelStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Point#-Elevation-Description&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointDescKey.ApplyLabelStyleId = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Once you build your application using the above code snippet and run the custom command in Civil 3D 2013, you should see a new Point Description Key sets added to the collection like the screenshot shown below -</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017742c305f5970d-pi" style="display: inline;"><img alt="Point_Desc_Key" class="asset  asset-image at-xid-6a0167607c2431970b017742c305f5970d" src="/assets/image_9a57a9.jpg" title="Point_Desc_Key" /></a></p>
<p>&#0160;</p>
<p>Hope this helps!</p>
<p>&#0160;</p>
