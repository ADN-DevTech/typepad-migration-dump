---
layout: "post"
title: "Selecting the edge of a nested solid in ObjectARX"
date: "2006-08-25 08:00:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Selection"
original_url: "https://www.keanw.com/2006/08/selecting_the_e.html "
typepad_basename: "selecting_the_e"
typepad_status: "Publish"
---

<p><em>This entry was contributed by Adam Nagy, a member of DevTech EMEA based in Prague.</em></p>

<p>The question is really a combination of two problems:</p>

<ol><li>Selecting a subentity</li>

<li>Selecting a nested entity</li></ol>

<p><strong>Selecting a subentity</strong></p>

<p>First we should clarify what a subentity is:</p>

<p>A subentity is a pseudo entity which is a logical part of a real entity. In our sample drawing the solid is the real entity [AcDb3dSolid] and the edge is the pseudo entity (there is no AcDbEdge class). In AutoCAD we rely on IDs called GS (short for graphics system) markers to get to these subentities. </p>

<p>The ObjectARX help file contains a small piece of sample code showing how to highlight one of a solid's internal edges. Unfortunately, it does not work in shaded mode – always the face will be selected - and it will not find the subentity in the case of a nested entity.</p>

<p><strong>Selecting a nested entity</strong></p>

<p>Again, let’s start by defining things:</p>

<p>For the purposes of this article, we're defining a nested entity as an entity that is placed inside a block [AcDbBlockTableRecord] other than one of the internal layout blocks [e.g. Model Space].</p>

<p>We can use a standard function such as acedNEntSelP() to select a nested entity, which gives back the bottommost selected entity and all its nesting entities – you can find information about it in the ObjectARX help file. But what about the GS markers we need?</p>

<p><strong>The two combined</strong></p>

<p>We really need a function that can select the bottommost nested entity and also gives back the [correct] GS marker of the subentity.</p>

<p>Fortunately, there is such a function, the undocumented brother of acedNEntSelP(). It does exactly what we need and is called acedNEntSelPEx(). It gives back the correct GS marker (even in shaded mode), provides the list of nesting entities and gives back the selected bottommost nested entity. Not only does it select edges and return the GS marker as we have seen, it also picks through spaces. e.g. say you are in paper space with no model space selected, from that mode you can use this function to select an entity in model space.</p>

<p>In the attached drawing we have the following structure:</p>

<p>SolidBlock2 -&gt; SolidBlock -&gt; Solid</p>

<p>[SolidBlock2 contains a reference to SolidBlock, which contains the Solid entity].</p>

<p>When using acedNEntSelPEx() [or acedNEntSelP()], we will get back a result buffer that contains the selected entity’s nesting entities in the same order as getSubentPathsAtGsMarker() needs them – starting from the directly nesting entity going upwards until we reach the top-most nesting entity that contains all the others. In case where we select the solid in SolidBlock2 in our sample drawing, the path is:</p>

<ol><li>SolidBlock reference</li>

<li>SolidBlock2 reference [which contains SolidBlock]</li></ol>

<p>Now we just have to create an array filled with the above entities in the same order, but starting with the solid entity itself:</p>

<ol><li>Solid entity</li>

<li>SolidBlock reference</li>

<li>SolidBlock2 reference</li></ol>

<p>We call the getSubentPathsAtGsMarker() function of the topmost nesting entity [SolidBlock2 reference] with the GS marker and the array we just created, and voilá, we get the correct subentity path that we need to highlight the subentity.</p>

<p>The attached project for Visual Studio 2005 (using ObjectARX 2007) contains both the help file code [in the &quot;HelpfileSelect&quot; command] and the rewritten code [in the &quot;MySelect&quot; command]:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/files/ArxSelectEdge.zip">Download ArxSelectEdge.zip</a> </p>
