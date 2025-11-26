---
layout: "post"
title: "Revit STL Exporter Released as Open Source"
date: "2011-06-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Data Access"
  - "External"
  - "Family"
  - "Geometry"
  - "News"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/06/revit-2012-stl-exporter-released-as-open-source.html "
typepad_basename: "revit-2012-stl-exporter-released-as-open-source"
typepad_status: "Publish"
---

<p>Here is one of the most surprising and promising recent news items for Revit API developers, published yesterday by Emile Kfouri on 

<a href="http://bimapps.typepad.com">BIM Apps</a>:

The 

<a href="http://bimapps.typepad.com/bim-apps/2011/06/stl-exporter-for-revit-2012-is-open-source.html">Revit STL exporter is now open source</a>.

<p>Look at Emile's post for the complete story, including:

<ul>
<li>Introduction, motivation, and explanation.
<li>Definitions of STL, stereolithography, and open source.
<li>STL exporter history.
<li>User feedback.
<li>Future.
<li>Open source license and participating parties.
<li>How to get started.
</ul>

<p>Exciting news!


<a name="2"></a>

<h4>Adding a Parameter to a Family</h4>

<p>To add an unrelated technical note, here is a very quick little question that crops up from time to time:

<p><strong>Question:</strong> How can I a parameter to an existing family?

<p><strong>Answer:</strong> You can add a real family parameter directly using the FamilyManager AddParameter method taking the arguments string, BuiltInParameterGroup, ParameterType, Boolean.

<p>Alternatively, you can create a shared parameter definition and add it to family using the FamilyManager AddParameter method taking the arguments ExternalDefinition, BuiltInParameterGroup, Boolean:

<ul>
<li>AddParameter(String, BuiltInParameterGroup, ParameterType, Boolean) adds a new family parameter with a given name. 
<li>AddParameter(ExternalDefinition, BuiltInParameterGroup, Boolean) adds a new shared parameter to the family.
</ul>
