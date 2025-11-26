---
layout: "post"
title: "Rename View by Matching Elevation Tag with Room"
date: "2013-03-21 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Filters"
  - "Fun"
  - "Geometry"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/03/rename-view-by-matching-elevation-tag-with-room.html "
typepad_basename: "rename-view-by-matching-elevation-tag-with-room"
typepad_status: "Publish"
---

<p>Today is the last morning meeting with my European DevTech colleagues here in Brittany, and time to travel back to Switzerland.</p>

<p>Before leaving, here is a useful real-world productivity tool by Trevor Taylor of ZGF,

<a href="http://www.zgf.com">Zimmer Gunsul Frasca Architects LLP</a>,

with his own description of the task and its solution:</p>

<p>The task I want to address is to match interior elevation tags with the rooms they fall inside.

<p>This is used to track back and rename the corresponding views.

<p>Naming interior elevation views is a major time-burner as there can be thousands of views in a project to rename, and they have to be coordinated when the room numbers  or names change.
If we don’t tie view names to rooms, we have no hope of ever keeping track of so many views.

<p>I originally attacked  this by trying to determine the location of the view tags in the model, but had to give up on that one.</p>

<p>Ben Bishoff of <a href="http://www.ideate.com">Ideate</a> gave me the idea to approach the problem from the view rather than from the interior elevation tag, so I worked backwards from the view cropbox property using the RevitLookup app and arrived at this simple solution:

<p><strong>1.</strong> Collect all views of class ‘ViewSection’, then filter down to those of ‘Interior Elevation’ type:</p>

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span> collector
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( m_doc )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">ViewSection</span> ) );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">ViewSection</span> current <span class="blue">in</span> collector )
&nbsp; {
&nbsp; &nbsp; <span class="teal">ViewFamilyType</span> vft = m_doc.GetElement(
&nbsp; &nbsp; &nbsp; current.GetTypeId() ) <span class="blue">as</span> <span class="teal">ViewFamilyType</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != vft
&nbsp; &nbsp; &nbsp; &amp;&amp; vft.Name == <span class="maroon">&quot;Interior Elevation&quot;</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; m_int_elevation_views.Add( current );
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p><strong>2.</strong> Construct a point at the midpoint of the front bottom edge of each interior elevation view's CropBox.
The CropBox is conveniently relative to the projection plane of the view.
X is to right, Y is up, and Z is the view depth:

<pre class="code">
&nbsp; <span class="green">// Construct a point at the midpoint of the </span>
&nbsp; <span class="green">// front bottom edge of the elev view cropbox</span>
&nbsp;
&nbsp; <span class="blue">double</span> xmax = v.CropBox.Max.X;
&nbsp; <span class="blue">double</span> xmin = v.CropBox.Min.X;
&nbsp; <span class="blue">double</span> zmax = v.CropBox.Max.Z;
&nbsp;
&nbsp; <span class="teal">XYZ</span> pt = <span class="blue">new</span> <span class="teal">XYZ</span>(
&nbsp; &nbsp; xmax - 0.5 * ( xmax - xmin ),
&nbsp; &nbsp; 1.0,
&nbsp; &nbsp; zmin );
</pre>

<p><strong>3.</strong> Get pt's translation to project's coordinate system:

<pre class="code">
&nbsp; <span class="green">// Get pt's translation to </span>
&nbsp; <span class="green">// project coordinate system</span>
&nbsp;
&nbsp; pt = v.CropBox.Transform.OfPoint( pt );
&nbsp;
</pre>

<p>That takes care of the heavy lifting. Pass pt to the GetRoomAtPoint method and you obtain the room, if there is one to get:

<p><strong>4.</strong> Retrieve the room:

<pre class="code">
&nbsp; Autodesk.Revit.DB.Architecture.<span class="teal">Room</span> rm
&nbsp; &nbsp; = m_doc.GetRoomAtPoint( pt );
</pre>

<p>It does exactly what I want it to now and will save our teams countless hours on large projects by organizing the names of interior elevation views by room name:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c37f9ae96970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c37f9ae96970b image-full" alt="Elevation renaming map" title="Elevation renaming map" src="/assets/image_9b78be.jpg" border="0" /></a><br />

</center>

<p>Here is a

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee99cc93c970d"><a href="http://thebuildingcoder.typepad.com/files/interior_elevation_view_organizer_2.zip">complete sample project</a></span> including 

a test model in case you’d like to check it out yourself.

<p>Many thanks to Trevor for this useful tool, his research, implementation, and generous sharing.</p>

<p>Before I sign off, here are two other nice pointers, not related to Revit:</p>


<a name="2"></a>

<h4>Au Bout du Monde and Super-Sonic Stereo</h4>

<p>Brittany is at the end of the earth, or Finisterre, au bout du monde, and I have seen many restaurants, a parking place, and a number of other establishments named after that around here.</p>

<p>That reminded me of one of my favourite humorous YouTube films, a Russian cartoon also named

<a href="http://www.youtube.com/watch?v=jsoKbk6GXQc">Au Bout du Monde</a>,

by Konstantin Bronzit:</p>

<center>
<iframe width="420" height="315" src="http://www.youtube.com/embed/jsoKbk6GXQc" frameborder="0" allowfullscreen></iframe>
</center>

<p>It was very fitting to share with my colleagues, since two of us are Russian as well.
We enjoyed it a lot, and I hope you like it as much as I do.</p>

<p>While we are at it, yesterday, my son Christopher pointed out another quite nice blog with explanations of what-if questions, the most recent one featuring sound sources, asking

<a href="http://what-if.xkcd.com/37">what if my stereo flew around at super-sonic speed?</a>

I enjoyed that as well, and so might you.</p>

<p>Last but not least, today is the first day of spring and

<a href="http://en.wikipedia.org/wiki/Equinox">
vernal equinox</a>!</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d4229178b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d4229178b970c image-full" alt="Spring and vernal equinox" title="Spring and vernal equinox" src="/assets/image_e2edd9.jpg" border="0" /></a><br />

</center>

<p>Off we go to the airport...</p>
