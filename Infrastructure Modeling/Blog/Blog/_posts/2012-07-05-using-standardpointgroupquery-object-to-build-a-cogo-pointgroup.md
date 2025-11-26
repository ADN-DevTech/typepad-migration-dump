---
layout: "post"
title: "Using StandardPointGroupQuery object to build a PointGroup"
date: "2012-07-05 01:30:50"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/using-standardpointgroupquery-object-to-build-a-cogo-pointgroup.html "
typepad_basename: "using-standardpointgroupquery-object-to-build-a-cogo-pointgroup"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D, Points can be selected and added to a PointGroup using either a standard (<strong>StandardPointGroupQuery</strong> object) or custom (<strong>CustomPointGroupQuery</strong> object) query. Both of these classes inherit from the <strong>PointGroupQuery</strong> base class.</p>
<p>Here is a C# code snippet which demonstrates how to create a PointGroup using <strong>StandardPointGroupQuery</strong> object.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Check if the pointGrpName already exists before trying to add it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pointGrpId = civilDoc.PointGroups.Add(pointGrpName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PointGroup</span><span style="line-height: 140%;"> pointGrp = pointGrpId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PointGroup</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Build a quesry using StandardPointGroupQuery object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Query conditions used below are specific to Civil 3D Tutorial DWG file &quot;Points-3.dwg&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">StandardPointGroupQuery</span><span style="line-height: 140%;"> standardPointGrpQry = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">StandardPointGroupQuery</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// One thing to note here :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// All of the Include* properties are OR-ed together, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// and all the Exclude* properties are OR- ed together </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; standardPointGrpQry.IncludeNumbers = </span><span style="color: #a31515; line-height: 140%;">&quot;550-560, 565-572&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; standardPointGrpQry.ExcludeElevations = </span><span style="color: #a31515; line-height: 140%;">&quot;&gt;100.00&quot;</span><span style="line-height: 140%;">;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//This method sets the query object in the point group</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointGrp.SetQuery(standardPointGrpQry);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177430825ab970d-pi" style="display: inline;"><img alt="PointGroup_StandardQ" class="asset  asset-image at-xid-6a0167607c2431970b0177430825ab970d" src="/assets/image_ee8694.jpg" title="PointGroup_StandardQ" /></a><br /><br /></p>
<p>Hope this is useful to you!</p>
