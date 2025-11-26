---
layout: "post"
title: "ViewPlan.Create method"
date: "2012-05-29 21:49:10"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/05/viewplancreate-method.html "
typepad_basename: "viewplancreate-method"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>In&#0160; Revit 2013, view types can be copied and edited as new types. ViewFamilyType class is added for this change. <br />You need to use ViewPlan.Create method instead of Create.NewViewPlan method which is now obsolete .&#0160; You can find a view type in very similar ways to find types of other families.</p>
<p>Here is an code snippet creating a plan view called “Level Test”. It finds a view type called “Floor Plan” using an element filter and LINQ query and passing its element Id.</p>
<p><span style="color: #60bf00;">// Using a filter and LINQ clause to find a view type called &quot;Floor Plan&quot;</span><br /><span style="color: #60bf00;">//</span><br />IEnumerable&lt;ViewFamilyType&gt; viewFamilyTypes = <span style="color: #0000bf;">from </span>elem in <span style="color: #0000bf;">new </span><br />&#0160; FilteredElementCollector(activeDoc).OfClass(typeof(ViewFamilyType))<br />&#0160;&#0160;&#0160; <span style="color: #0000bf;">let </span>type = elem <span style="color: #0000bf;">as </span>ViewFamilyType<br />&#0160;&#0160;&#0160; <span style="color: #0000bf;">where </span>type.ViewFamily == ViewFamily.FloorPlan<br />&#0160;&#0160;&#0160; <span style="color: #0000bf;">select </span>type;</p>
<p>ViewFamilyType viewFamilyType = <span style="color: #0000bf;">null</span>;<br /><span style="color: #0000bf;">foreach</span>(ViewFamilyType familyType <span style="color: #0000bf;">in </span>viewFamilyTypes)<br />{<br />&#0160; if (familyType.Name == <span style="color: #c00000;">&quot;Floor Plan&quot;</span>)<br />&#0160;&#0160;&#0160; viewFamilyType = familyType;<br />&#0160;&#0160;&#0160; <span style="color: #0000bf;">break</span>;<br />}</p>
<p><span style="color: #60bf00;">// Creates a plan view for the view type found</span><br /><span style="color: #60bf00;">// </span><br />Autodesk.Revit.DB.ViewPlan viewPlan = <br />&#0160; Autodesk.Revit.DB.ViewPlan.Create(activeDoc, viewFamilyType.Id, level.Id);<br />viewPlan.Name = <span style="color: #c00000;">&quot;Level Test&quot;</span>;</p>
<p>&#0160;</p>
