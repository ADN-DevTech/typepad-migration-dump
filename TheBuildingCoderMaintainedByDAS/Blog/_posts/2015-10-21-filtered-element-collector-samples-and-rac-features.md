---
layout: "post"
title: "Filtering Samples and RAC 2016 Features"
date: "2015-10-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2016"
  - "Filters"
  - "Getting Started"
  - "News"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/10/filtered-element-collector-samples-and-rac-features.html "
typepad_basename: "filtered-element-collector-samples-and-rac-features"
typepad_status: "Publish"
---

<p>The issue of <a href="#2">simple filtered element collector samples</a> was raised in a private message.</p>

<p>I do not like to receive private messages and avoid answering them in private.</p>

<p>I always prefer to discuss everything I do in public and enable the entire community to contribute and share when possible.</p>

<p>In this case, such a message led to the discussion <a href="#2">below</a>.</p>

<p>I'll also point to an overview of <a href="#3">Revit Architecture 2016 Features</a>.</p>

<h4><a name="2"></a>Simple Filtered Element Collector Samples</h4>

<p><strong>Question:</strong>
I am trying to write C# code to use the Revit API.</p>

<p>The nice samples in the SDK are rather complex :-)</p>

<p>A simple thing I think would be a help for many is some simple samples, e.g.:</p>

<ul>
<li>How to make a filtered element collector with a type and an instance parameter for the different categories? Maybe how to write to them as well?</li>
<li>How to make a filter for system families such as walls, floors, ceilings, to get one type and one instance parameter. This could be one single sample, if the method can be used for several categories.</li>
<li>Host sweeps</li>
<li>Ramps and stairs</li>
</ul>

<pre class="code">
&nbsp; <span class="green">// Floors, Walls, Ceilings, Roofs etc...</span>
&nbsp; <span class="teal">FilteredElementCollector</span> FMFloorCollector
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; FMFloorCollector.OfClass( <span class="blue">typeof</span>( <span class="teal">Floor</span> ) );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Floor</span> FMFloor <span class="blue">in</span> FMFloorCollector )
&nbsp; {
&nbsp; &nbsp; <span class="green">// Type</span>
&nbsp; &nbsp; <span class="teal">Element</span> el = FMFloor <span class="blue">as</span> <span class="teal">Element</span>;
&nbsp; &nbsp; <span class="teal">ElementId</span> typeid = el.GetTypeId();
&nbsp; &nbsp; <span class="teal">Element</span> floortype = doc.GetElement( typeid );
&nbsp;
&nbsp; &nbsp; <span class="teal">Parameter</span> pKeynote = floortype.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.KEYNOTE_PARAM );
&nbsp;
&nbsp; &nbsp; <span class="green">// Instance</span>
&nbsp; &nbsp; <span class="teal">Parameter</span> pArea = FMFloor.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.HOST_AREA_COMPUTED );
&nbsp; }
</pre>

<p>What people want to do with the data forms and so on is more or less standard C#.</p>

<p>But simply how to set/get could be nice...</p>

<p><strong>Answer:</strong>
Have you looked at <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder</a> samples?</p>

<p>Especially,
the <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs">CmdCollectorPerformance module</a> provides
a large number of such samples and probably covers all of the examples you list.</p>

<p>Please let me know if anything is missing.</p>

<p>Or, better still, fork the repository, add them yourself, and let me know so I can merge your update back in again.</p>

<p>Thank you!</p>

<h4><a name="3"></a>Revit Architecture 2016 Features</h4>

<p>Nick Bowley and Carl Storms published a nice overview and video recordings of several Revit Architecture 2016 Features:</p>

<ul>
<li><p><a href="http://www.engineering.com/Education/EducationArticles/ArticleID/10636/Autodesk-Revit-Introduces-New-Features-in-2016.aspx">Part 1</a> &ndash; 3.5 min</p>

<ul>
<li>Right click view on a sheet, Open Sheet, Selection Box</li>
<li>Revisions</li>
<li>Energy Settings, Analysis menu, Use Conceptual Masses and Building Elements</li>
<li>Show Energy Model</li>
</ul></li>
<li><p><a href="http://www.engineering.com/BIM/ArticleID/10696/The-Many-Features-of-Autodesk-Revit-2016.aspx">Part 2</a> &ndash; 15 min</p>

<ul>
<li>Revit Core Features (Multi-Discipline)
<ul>
<li>Allowing navigation during redraw</li>
<li>Remembering view states</li>
<li>Rotate Project North</li>
<li>Multiline text</li>
</ul></li>
<li>Architecture
<ul>
<li>Placing rooms automatically</li>
<li>Floor elevations</li>
<li>New door content</li>
</ul></li>
</ul></li>
<li><p><a href="http://www.engineering.com/BIM/ArticleID/10832/More-of-Whats-New-in-Autodesk-Revit-2016.aspx">Part 3</a> &ndash; 15 min</p>

<ul>
<li>Search in combo box (type selector and ribbon)</li>
<li>Link positioning</li>
<li>Select host for tags</li>
<li>Model upgrade interface improvements</li>
<li>PDF hyperlinks</li>
<li>Resetting camera target in perspective view</li>
<li>Toggle perspective</li>
<li>And more...</li>
</ul></li>
</ul>
