---
layout: "post"
title: "View Filter API"
date: "2013-08-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Data Access"
  - "Export"
  - "Filters"
  - "Parameters"
  - "SDK Samples"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/08/view-filter-api.html "
typepad_basename: "view-filter-api"
typepad_status: "Publish"
---

<p>One long-standing developer wish list item that was fulfilled by the Revit 2014 API is the ability to access, add and control the view filters listed in the visibility dialogue.</p>

<p>The Revit API now provides the following methods supporting this:</p>

<ul>
<li>View.AddFilter()</li>
<li>View.SetFilterVisibility()</li>
<li>View.SetFilterOverrides()</li>
</ul>


<p>Here are several variations of the request for this functionality that help understand the need and new possibilities by highlighting the numerous ways in which it can be used:</p>

<p><strong>Question 1:</strong> We want to programmatically achieve what can be done using the Filter tab of Visibility/Graphic Override dialogue.</p>

<p>For example, it enables setting the colour of elements in a view that match a filter criterion, e.g., all elements in a view that have a shared parameter value in a certain range can be set to blue.
Other elements in that view with different values can be set to red, all without applying any permanent changes to the model or element attributes.
All the filter, colour and fill pattern objects for achieving this already exist in the API.</p>

<p>We want to hide certain elements as well.
This is also possible in the Filter tab of the Visibility dialogue.
The API provides the View.ProjColorOverrideByElement and View.ProjLineWeightOverrideByElement properties that we currently use, but we still cannot hide elements.</p>

<p>The ElementFilter ViewFilters SDK sample shows how to achieve all the required steps except the final one of assigning a newly created view filter to a view.
In Revit 2013 and before, that can only be done manually through the Graphics/Visibility dialogue and its Filter tab.</p>


<p><strong>Question 2:</strong> Benson submitted a

<a href="http://thebuildingcoder.typepad.com/blog/2009/11/visible-elements.html?cid=6a00e553e1689788330133f558bdcc970b#comment-6a00e553e1689788330133f558bdcc970b">
comment</a> on

<a href="http://thebuildingcoder.typepad.com/blog/2009/11/visible-elements.html">
visible elements</a> asking for the same thing:</p>

<p>I can select an element, right click it, select Hide in View &gt; By Filter from the pop-up menu and add a filter to hide it.</p>

<p>The method IsHiddenElementOrCategory will still return true for this element, i.e., the element hiding filter is ignored.</p>

<p>I want to obtain all the elements hidden using a filter like this in addition to the global Elements and Category settings.</p>

<p>The classes ParameterFilterElement and FilterRule initially looked as if they might be useful here.
The method FilterRule.ElementPasses can be used to determine if the element is hidden "By Filter", but the filter can be disabled by unchecking it in the view Visibility/Graphic dialogue.</p>

<p>Is it possible to retrieve the filter visibility in a selected view?</p>

<p>I can iterate all the filters and all the rules, but I still see no way to get the visibility of the filters.</p>

<p><strong>Question 3:</strong> I would like to export only visible objects.
Using the element IsHidden method reports whether it was hidden using the Hide in View &gt; Elements menu option, but ignores Hide in View filters, and so does the

<a href="http://thebuildingcoder.typepad.com/blog/2009/11/visible-elements.html">
IsHiddenElementOrCategory</a> method.</p>

<p>How can my application determine exactly what the user actually sees on the screen?
Our customers obviously expect this functionality for the export.</p>


<p><strong>Question 4:</strong> I need to programmatically define colour and line type definitions for localisation purposes. This can be achieved through the user interface using Visibility/Graphic Overrides &gt; Filters.</p>


<p><strong>Question 5:</strong> I need programmatic access to the Visibility/Graphic Overrides filters  specifically for Legend views.</p>




<p><strong>Answer:</strong> The ParameterFilterElement and related classes were renovated to align with the ElementFilter interfaces for element iteration and DMU.</p>

<p>There was initially no direct interface added to support assignment of a filter to a view.</p>

<p>The SCHEDULE_FILTER_PARAM and VIS_GRAPHICS_FILTERS built-in parameters are possibly used to store these settings, but they cannot be read or modified through the API since they are used to store multiple values, and there is no API support for that kind of value type.</p>

<p>This gap was addressed in the Revit 2014 API, and the documentation highlights the new functionality like this in the

<a href="http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html">
What's New</a> section:</p>

<h4 style="color: darkblue">View Filters</h4>

<p style="color: darkblue">A new set of methods on the View class allow getting, setting, adding, and removing filters.
Filters can be created using the ParameterFilterElement class and its Create method that existed in previous versions of the Revit API.</p>

<p>As said, the concrete methods providing access to this include:</p>

<ul>
<li>View.AddFilter()</li>
<li>View.SetFilterVisibility()</li>
<li>View.SetFilterOverrides()</li>
</ul>

<a name="2"></a>

<p><strong>Addendum</strong> by Arno&scaron;t L&ouml;bel: This information is very correct.
Using filters and visibility settings may give somehow adequate results, but widespread opinion in the Revit graphics team considers the only way of truly figuring out what is or is not visible in a view running it through an export context, e.g. via a Custom Exporter.</p>

<p>However, you do need to keep in mind and note that the current Custom Exporter only processes (and sends to an export context) items that would be rendered, which may not include all objects that are actually visible in a 3D view.
For example, model lines are not processed, because they do not render.</p>
