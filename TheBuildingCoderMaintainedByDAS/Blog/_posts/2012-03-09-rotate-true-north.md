---
layout: "post"
title: "Rotate True North"
date: "2012-03-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Geometry"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/03/rotate-true-north.html "
typepad_basename: "rotate-true-north"
typepad_status: "Publish"
---

<p>We have previously looked at how to handle the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/project-location.html">
project location</a> properties 

and more specifically how to handle the effect of a 

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/unrotate-north.html">
rotated Project North</a> on

an element azimuth.

<p>Here is a question on the 'opposite' topic, how to set up a project north rotation in the first place:

<p><strong>Question:</strong> Is there any way to rotate the truth north using the Revit API? 

<p>It can be done in the user interface using the Manage &gt; Project Location &gt; Position &gt; Rotate True North command:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330163029c42b4970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330163029c42b4970d" alt="Rotate True North in Japanese" title="Rotate True North in Japanese" src="/assets/image_c99c4b.jpg" border="0" /></a><br />

</center>

<p>Here is the English command with its expanded tooltip:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301676390db93970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301676390db93970b image-full" alt="Rotate True North in English" title="Rotate True North in English" src="/assets/image_d2caa5.jpg" border="0" /></a><br />

</center>

<p><strong>Answer:</strong> I believe the same effect can be achieved by rotating the <b>Project</b> North which is available in the API.

<p>If this fits the bill, then here are some working code snippets making use of the related classes and methods:

<pre class="code">
&nbsp; <span class="teal">ProjectLocation</span> plCurrent
&nbsp; &nbsp; = _doc.ActiveProjectLocation;
&nbsp;
&nbsp; <span class="green">// . . . </span>
&nbsp;
&nbsp; <span class="teal">ProjectPosition</span> newPosition
&nbsp; &nbsp; = _app.Create.NewProjectPosition(
&nbsp; &nbsp; &nbsp; scaleToFT * cs.OriginX,
&nbsp; &nbsp; &nbsp; scaleToFT * cs.OriginY,
&nbsp; &nbsp; &nbsp; scaleToFT * cs.OriginZ,
&nbsp; &nbsp; &nbsp; theRotationYouWant );
&nbsp;
&nbsp; plCurrent.set_ProjectPosition(
&nbsp; &nbsp; ptOrigin, newPosition );
</pre>

<p>You may have to do this for <b>all</b> project locations.
A bit of 3D maths and brainwork may be required, but it should be doable.

<p>Many thanks to Katsuaki Takamizawa and Miroslav Schonauer for this discussion and suggestion!
