---
layout: "post"
title: "Store Project Data"
date: "2009-07-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Family"
  - "Filters"
  - "Parameters"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/07/store-project-data.html "
typepad_basename: "store-project-data"
typepad_status: "Publish"
---

<p>We recently discussed 

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/store-structured-data.html">
storing structured data</a> 

in a Revit file, and received a number of very helpful additional 

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/store-structured-data.html#comments">
comments</a>

on the topic.
To store custom application data in a Revit project, one needs to attach a shared parameter to some Revit elements.
As we have seen in the discussion on 

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/model-group-shared-parameter.html">
model group shared parameters</a>, 

almost any element can be used to host the shared parameter.
Here is a common related question:</p>

<p><strong>Question:</strong>
Where can I store project-wide information in a Revit file, data which is not related to any specific individual element?

<p><strong>Answer:</strong>
First of all, there is no need to separate the two issues of element-specific versus global data, because the only way to store application data in Revit is to use shared parameters on certain elements. 
Luckily, there is one object which occurs exactly once in the entire project and therefore constitutes a suitable repository for global document data, also known as a singleton object: the project information or ProjectInfo element. 
It can be retrieved using Revit 2009 filtering by searching for the "Project Information" category with the built-in category OST_ProjectInformation. 
This is discussed in the standard Revit programming introduction presentation and demonstrated by the labs 4-4-1 and 4-4-2 in the 

<span class="at-xid-6a00e553e168978833011571559597970c"><a href="http://thebuildingcoder.typepad.com/files/rac_labs_2009-07-30.zip">Revit API introduction labs</a></span>.

<p>The ProjectInfo singleton element is not present in a family file, so we have to resort to some other method in that case.</p>

<h4>Per-document shared parameter in a family document</h4>

<p><strong>Question:</strong>
How can we add a per-document shared parameter to a family document?
The lab 4-4-1 contains code to create and bind shared parameters to the Project Information category, or basically to anything which appears as a singleton in the document. 
How can we implement the same for a family document? 
It does not contain a Project Information element.</p>

<p><strong>Answer:</strong>
Looking at a new family file using RvtMgdDbg, I can see one likely candidate, the ProjectUnit object. 
I imagine that element also only occurs exactly once. 
There are some other elements which may or may not also be unique: the default material, the front view, the basic reference level, the solid fill FillPattern, the Work Plane Grid element, etc. 
I suggest you pick an element, any element. 
Build a filter to select all such elements. 
Include an assertion to verify that exactly one is returned in all cases. 
Any element will do, it just has to be easily selectable and unique.</p>

<p>Actually, in a family document, you can probably also use a hidden family parameter instead of attaching the data to any specific element contained within the document.</p>
