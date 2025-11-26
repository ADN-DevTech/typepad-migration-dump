---
layout: "post"
title: "Adding a Rebar inside a Rebar Cover"
date: "2010-09-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Geometry"
  - "RST"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/09/adding-a-rebar-inside-a-rebar-cover.html "
typepad_basename: "adding-a-rebar-inside-a-rebar-cover"
typepad_status: "Publish"
---

<p>The 

<a href="http://www.classicsailing.de">
Pantagruel</a> arrived 

safely in G&ouml;teborg last night after our week-long tour of Norway, Denmark and the Swedish west coast, and our last day has arrived.
Time to clean up, disembark, go to the airport and return home again.

<p>Meanwhile, here is an interesting structural rebar issue raised by my colleagues Katsuaki Takamizawa and Tim Culver:

<p><strong>Question:</strong> Here are some rebars that were created using the Revit API NewRebar method:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f4115fb6970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f4115fb6970b" alt="Rebar and rebar cover" title="Rebar and rebar cover" src="/assets/image_be45bc.jpg" border="0" /></a> <br />

</center>

<p>In the user interface, rebars can only be added inside of a rebar cover represented by the green dotted line. 
If they are generated using the following code in a column where xVec and yVec fit the width and depth of the column profile, they can extend beyond the rebar cover.

<pre class="code">
  Rebar createdRebar 
    = doc.Create.NewRebar( barShape, barType, 
      column, origin, xVec, yVec );
</pre>

<p>Is it possible to automatically ensure the rebar placement inside of a rebar cover when generating it programmatically? 
Or does one have to adjust the location and scale whenever a rebar is added?

For instance, using ScaleToBox:

<pre>
  createdRebar.ScaleToBox( origin, xVec, yVec );
</pre>

<p><strong>Answer:</strong> Keep in mind that Revit does not enforce the cover when it is generated programmatically. 
However you create the bars, you need to account for the cover yourself.  
You can specify the location and scale when generating or adjust by ScaleToBox to place a rebar inside the cover. 
ScaleToBox is the same algorithm used internally by the UI. 

<p>However, Revit does perform one adjustment automatically: if you place the rebar so that the centre line is close to the cover (within a bar radius, I think) and parallel, the next call to the Regenerate method will move it to attach to the cover. 
You can see this by moving the rebar in the UI. 

<p>There are actually three ways to create and scale the rebar: using the origin and vectors, create from curves, and ScaleToBox, where the latter requires you to create the bar in one of the other ways first.

<p>No matter how you create the bars, Revit never enforces cover, except in case the rebar is very close and parallel.
I.e., when creating rebars programmatically, the cover cannot be enforced like it is in the user interface, the developer has to take care of that explicitly herself.

<p>Many thanks to Katsu-san and Tim for this explanation!</p>
