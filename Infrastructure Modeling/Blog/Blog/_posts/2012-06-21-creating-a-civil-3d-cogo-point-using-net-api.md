---
layout: "post"
title: "Creating a Civil 3D COGO Point using .NET API"
date: "2012-06-21 00:25:22"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/creating-a-civil-3d-cogo-point-using-net-api.html "
typepad_basename: "creating-a-civil-3d-cogo-point-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AutoCAD Civil 3D 2013 .NET API has exposed the COGO Points (<strong>CogoPoint</strong>) and associated functionalities which enables us creating, updating or removing COGO Points. In next few series of posts I am going to share some basics of COGO Points and related .NET API which are new in Civil 3D 2013. In this post we will see how to create a Civil 3D COGO Point at a specified location.</p>
<p>For those, who didn&#39;t have to deal with <strong>COGO</strong> Points earlier, some basics of COGO Points : COGO is short form of &quot;Coordinate Geometry&quot;. COGO Points in Civil 3D are more complex than AutoCAD points, which have only coordinate data. A CogoPoint object, in addition to a location, also has properties such as a unique ID number, name, raw (field) description, and full (expanded) description.</p>
<p>Here is a C# code snippet which demonstrates creation of Civil 3D COGO Point at a specified location :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Select the location for COGO Point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;"> ppo = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect the location to Create a COGO Point :&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> location = ppr.Value;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// All points in a document are held in a CogoPointCollection object </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We can access CogoPointCollection through the CivilDocument.CogoPoints property</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CogoPointCollection</span><span style="line-height: 140%;"> cogoPoints = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument.CogoPoints;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Adds a new CogoPoint at the given location with the specified description information</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pointId = cogoPoints.Add(location, </span><span style="color: #a31515; line-height: 140%;">&quot;Survey Point&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;"> cogoPoint = pointId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Set Some Properties</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cogoPoint.PointName = </span><span style="color: #a31515; line-height: 140%;">&quot;Survey_Base_Point&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cogoPoint.RawDescription = </span><span style="color: #a31515; line-height: 140%;">&quot;This is Survey Base Point&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p>Hope this helps!</p>
</div>
