---
layout: "post"
title: "Creating an Offset Wall"
date: "2013-09-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/09/creating-an-offset-wall.html "
typepad_basename: "creating-an-offset-wall"
typepad_status: "Publish"
---

<a class="asset-img-link"  style="float: right;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff51c330970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019aff51c330970d" alt="Offset wall half on and half off slab" title="Offset wall half on and half off slab" src="/assets/image_5bbd5a.jpg" style="margin: 0px 0px 5px 5px;" /></a>

<p>Here is an issue we have not looked at in a long time, brought up again by the following query:</p>

<p><strong>Question:</strong> I am creating a tool to generate a BIM from CAD data.</p>

<p>One of the steps involves generating a floor slab, and then placing exterior walls along its edge.</p>

<p>The tool begins by using model lines and arcs as a profile to generate the floor.</p>

<p>Unfortunately, that generates walls that resting half on and half off the slab:</p>

<!--

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff4da629970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019aff4da629970c" alt="" title="Offset wall half on and half off slab" src="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff4da629970c-120wi" /></a><br />

</center>

-->

<p>How can I fix this, please?</p>

<p><strong>Answer:</strong> The wall creation will always be based on the wall centre line.</p>

<p>After creating the wall, you can adjust the wall's "Location Line" parameter to one of the following options:</p>

<ol start="0">
<li>Wall Centerline</li>
<li>Core Centerline</li>
<li>Finish Face: Exterior</li>
<li>Finish Face: Interior</li>
<li>Core Face: Exterior</li>
<li>Core Face: Interior</li>
</ol>

<p>Then the wall will align according to the location line you specified.</p>

<p>In this way, the wall exterior face can be aligned to the floor profile edge after creating the wall using the floor profile edges.</p>

<p>A little note on how to determine and assign one of the options to the "Location Line" parameter:</p>

<p>Each option is mapped to an integer value.
You can determine the specific values by looking them up using the RevitLookup tool after changing the settings back and forth manually in the user interface.</p>

<p>And then set the "Location Line" value.</p>

<p>For example:</p>

<pre class="code">
&nbsp; <span class="teal">Wall</span> wall = ...
&nbsp;
&nbsp; Parameter param = wall.get_Parameter(
&nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.WALL_KEY_REF_PARAM);
&nbsp;
&nbsp; param.Set(2); <span class="green">// Finish Face: Exterior</span>
&nbsp; param.Set(1); <span class="green">// Core Centerline</span>
</pre>

<p>Note that this is an application of the principles we discussed way back in the year 2008, explaining the old

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-compound-layers.html">
wall compound layers</a> and

brought up in a comment by Berria on that post:</p>

<p><strong>Question:</strong> How can I specify that my walls Line Location are 'Finish Face: Exterior'.</p>

<p><strong>Answer:</strong> You need to be very precise with what you mean by wall location line.
As

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-compound-layers.html">
mentioned</a>,

there is a difference between the wall.Location line that you see in the API and the 'Location Line' visible in the user interface.
The former cannot be changed, it is always in the middle of the wall.
The position of the latter can be adjusted using the WALL_KEY_REF_PARAM parameter.</p>

<p>A bit later, Mike posted a similar

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html?cid=6a00e553e1689788330115722cf638970b#comment-6a00e553e1689788330115722cf638970b">
query</a> on

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html">
creating a wall with a sloped profile</a>:</p>

<p><strong>Question:</strong> I am struggling to figure out how to set a wall's 'Location Line' to 'Finish Face: Interior' in code prior to drawing the wall.
I would like the interior face of the wall to be at the coordinate points I supply instead of the default 'Wall Centerline'.</p>

<p><strong>Answer:</strong> You can explore this as follows:</p>

<p>Create a new wall and look at its parameters, e.g. using RevitLookup or the Revit API Introduction labs

<a href="http://thebuildingcoder.typepad.com/blog/2013/01/built-in-parameter-enumeration-duplicates-and-bipchecker-update.html">
built-in parameter checker</a>.

One of the parameters is</p>

<ul>
<li>WALL_KEY_REF_PARAM &ndash; Location Line &ndash; int read-write</p>
</ul>

<p>It has an initial value of zero.
Looking at the wall properties in the user interface, I see that the location line property is set to centre line.
If I manually change the property to 'Finish Face: Interior' and then look at the parameter again, its value has now changed to 3.</p>

<p>You can invert this process, i.e. set the built-in parameter WALL_KEY_REF_PARAM to have a value of 3 through the API, which  corresponds to 'Finish Face: Interior'.</p>
