---
layout: "post"
title: "Retrieving Materials"
date: "2011-08-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Data Access"
  - "Filters"
  - "RST"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/retrieving-materials.html "
typepad_basename: "retrieving-materials"
typepad_status: "Publish"
---

<p>Some people have reported issues using the Document.Settings.Materials collection.
For instance, its Contains method may throw an exception when used with Revit Structure 2012, while it works fine with Revit Architecture 2012. 

<p>The problem actually probably has nothing to do with the flavour of Revit being used, but is caused by the template file that the project is based on.
If you retrieve materials in a project based on the architectural template in Revit Structure, it also works fine.

<p>In any case, here is a workaround you can use instead.
Simply retrieve the materials using a filtered element collector instead of the Materials collection, for example like this:

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span> collector 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( document )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Material</span> ) );
&nbsp;
&nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Material</span>&gt; materialsEnum 
&nbsp; &nbsp; = collector.ToElements().Cast&lt;<span class="teal">Material</span>&gt;();
&nbsp;
&nbsp; <span class="blue">var</span> materialReturn1 
&nbsp; &nbsp; = <span class="blue">from</span> materialElement <span class="blue">in</span> materialsEnum
&nbsp; &nbsp; &nbsp; <span class="blue">where</span> materialElement.Name == <span class="maroon">&quot;Default&quot;</span>
&nbsp; &nbsp; &nbsp; <span class="blue">select</span> materialElement;
</pre>
