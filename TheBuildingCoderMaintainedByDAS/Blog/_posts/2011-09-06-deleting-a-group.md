---
layout: "post"
title: "Deleting a Group"
date: "2011-09-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Group"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/09/deleting-a-group.html "
typepad_basename: "deleting-a-group"
typepad_status: "Publish"
---

<p>I discussed various aspects of programmatically handling Revit element groups in the past, such as how to 

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-group-and-how-to-fish.html">
create</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/rename-a-group.html">
rename</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2010/08/editing-elements-inside-groups.html">
edit the contents</a> of 

an existing group.

Here is another question on the topic of groups that helps clarify the relationship between the group and its type:

<p><strong>Question:</strong> I am deleting a model group using the following code:

<pre class="code">
&nbsp; <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc );
&nbsp; trans.Start( <span class="maroon">&quot;test&quot;</span> );
&nbsp;
&nbsp; <span class="teal">ElementSet</span> sel = uidoc.Selection.Elements;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> sel )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Group</span> group = e <span class="blue">as</span> <span class="teal">Group</span>;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != group )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; doc.Delete( group );
&nbsp; &nbsp; }
&nbsp; }
&nbsp; trans.Commit();
</pre>

<p>This seems to delete the model group including the elements contained in it. 
But still the group remains visible in the project browser:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8b4e3db3970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8b4e3db3970d" alt="Group in browser" title="Group in browser" src="/assets/image_6eb2e2.jpg" border="0" /></a> <br />

</center>

<p>How can I delete it from the browser, please?

<p>By the way, I see the same behaviour when I delete it using the Delete button in the user interface


<p><strong>Answer:</strong> If you want to delete the definition of the group, then the GroupType also needs to be deleted, for example as follows:

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span> collector 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( ( <span class="blue">typeof</span>( <span class="teal">GroupType</span> ) ) );
&nbsp;
&nbsp; <span class="blue">var</span> groupTypes 
&nbsp; &nbsp; = <span class="blue">from</span> element <span class="blue">in</span> collector 
&nbsp; &nbsp; &nbsp; <span class="blue">where</span> element.Name == <span class="maroon">&quot;Group 1&quot;</span> 
&nbsp; &nbsp; &nbsp; <span class="blue">select</span> element;
&nbsp;
&nbsp; <span class="teal">Element</span> groupType = groupTypes.First();
&nbsp;
&nbsp; doc.Delete( groupType );
</pre>

<p>You can also access the group type directly and more easily from the group itself using the Group.GroupType property.

<p><strong>Response:</strong> Thank you, it does indeed remove the group from the browser.
