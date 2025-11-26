---
layout: "post"
title: "Performant Hardware"
date: "2011-04-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Installation"
  - "Performance"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/04/erformant-hardware.html "
typepad_basename: "erformant-hardware"
typepad_status: "Publish"
---

<p>I spent all day yesterday in trains, which was unfortunate, since it was the most beautiful Sunday, and springtime is in full bloom.
It also forced upon me some unfortunate comparisons of the Swiss and Italian train systems.
Needless to say, I arrived later than expected.

<p>Still, I ended up happily back in

<a href="http://en.wikipedia.org/wiki/Verona">
Verona, Italy</a>, where I started seriously learning Italian two years ago, for a 

<a href="http://thebuildingcoder.typepad.com/blog/2009/01/verona-revit-api-training.html">
repeat visit</a> to 

provide a Revit API training class to 

<a href="http://www.steel-graphics.com">
Steel & Graphics srl</a>.

<p>One funny thing about Verona is that it has almost exactly the same flag as Sweden, where I grew up:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8764e28e970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8764e28e970d" alt="Verona and Sweden's flags" title="Verona and Sweden's flags" src="/assets/image_4441b7.jpg" border="0" /></a> <br />

</center>

<p>It is also the city of Shakespeare's Romeo and Juliet.
Maybe I can manage to get to 

<a href="http://en.wikipedia.org/wiki/Sirmione">
Sirmione</a> and the

<a href="http://en.wikipedia.org/wiki/Sirmio">
Sirmio peninsula</a> today 

and complete the final training preparations there.

<p>Meanwhile, here is a non-API related question affecting all Revit users from Augusto Gon&ccedil;alves:

<p><strong>Question:</strong> To specify a new machine for Revit, what should I consider, i.e. spend more money on? 
Amount of memory? 
Speed of memory? 
Processor clock? 
Multi-processors? 
HD speed?

<p><strong>Answer:</strong> Anthony Hauck provides a very succinct answer: 

<p>In order, and dependent on how large the models you wish to edit are:

<ol>
<li>More RAM
<li>Faster Processor
<li>More Processers
<li>Faster RAM
<li>Faster HD
</ol>

<p>For a more complete and long-winded answer, you may want to peruse the Revit 

<a href="http://images.autodesk.com/adsk/files/revit_tech_note.pdf">
model performance technical note</a> pointed out by Martin Schmid.

<p>It has been around for quite a while now and includes some detailed software optimisation suggestions as well, which are well worth reading and understanding for add-in developers as well as end users, including:

<ul>
<li>Revit Platform Model Optimization and Best Practices
<ul>
<li>Arrays
<li>Constraints
<li>Design Options
<li>DWG Files
<li>Family Creation
<li>Importing & Linking
<li>Modeling Economically
<li>Project Templates
<li>Railings
<li>Raster Images
<li>Stairs
<li>Upgrading Linked Projects to a New Version of Revit
<li>Views
<li>Volumes - Rooms and Spaces
<li>Worksets
<li>Worksharing
</ul>
<li>Revit Structure 2010 Software Optimization and Best Practices
<li>Revit MEP 2010 Software Optimization and Best Practices
</ul>

<p>Note that this document applies to Revit 2010, although I am assuming that most remains valid for Revit 2012 as well.
