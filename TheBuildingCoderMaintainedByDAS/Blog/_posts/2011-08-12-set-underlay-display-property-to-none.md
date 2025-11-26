---
layout: "post"
title: "Set Underlay Display Property to None"
date: "2011-08-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/set-underlay-display-property-to-none.html "
typepad_basename: "set-underlay-display-property-to-none"
typepad_status: "Publish"
---

<p>Here is a short and sweet question handled by Joe Ye:

<p><strong>Question:</strong> I am creating new levels using the API.
All my existing levels have their underlay display property set to None, but the new ones all have it set to the previous level.

<p>How can I programmatically change this setting to None as well?

<p><strong>Answer:</strong> This property is stored in the "Underlay" parameter on the level.
You can set it to None by assigning a null ElementId. 
Here is a sample code snippet showing how to create a null ElementId and assign it to the parameter:

<pre class="code">
&nbsp; <span class="teal">ElementId</span> id = <span class="blue">new</span> <span class="teal">ElementId</span>( -1 );
&nbsp; e.get_Parameter( <span class="maroon">&quot;Underlay&quot;</span> ).Set( id ); 
</pre>

<p>For production code, I would suggest using the built-in parameter instead of the user visible display name to identify the parameter.
Furthermore, the ElementId class defines a static property InvalidElementId which returns the invalid ElementId whose IntegerValue is -1.
Using these two improvements, the language independent code would look like this:

<pre class="code">
&nbsp; e.get_Parameter( <span class="teal">BuiltInParameter</span>.VIEW_UNDERLAY_ID )
&nbsp; &nbsp; .Set( <span class="teal">ElementId</span>.InvalidElementId ); 
</pre>

<p>ElementId.InvalidElementId is commonly used in the Revit API to denote an invalid or void element reference, for instance to mark the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/06/extensible-storage-features.html#7">
deletion of an element referenced in extensible storage</a>.
