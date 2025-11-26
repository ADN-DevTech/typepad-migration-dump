---
layout: "post"
title: "Curve.Intersect Return Values"
date: "2011-03-21 04:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2009"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/03/curveintersect-return-values.html "
typepad_basename: "curveintersect-return-values"
typepad_status: "Publish"
---

<p>The first day of the Saudi Arabian Revit API training completed yesterday.
This training will go on for three days instead of the usual two, so that we have more time to deal with the installation and basics as well as various advanced topics.
Yesterday we dealt with installation and setup issues, of Revit, Visual Studio, and the Revit SDK with its tools and utilities.
We started looking at Hello World style samples, i.e. the basic skeletal structure and installation of Revit add-ins, and more basic material remains to be covered.

<p>In lack of a 

<a href="http://www.lonelyplanet.com/saudi-arabia/hejaz/jeddah/sights/beach/beaches">
beach</a>,

I spent an hour after the training on the sea-side.
The hotel is on the 

<a href"http://en.wikipedia.org/wiki/Jeddah_Corniche">
corniche</a> right 

in front of 

<a href="http://en.wikipedia.org/wiki/King_Fahd%27s_Fountain">
King Fahd's fountain</a>,

so I got to see the sun set through that.

I went out again later on to enjoy the moon, which was full yesterday and is starting to wane now.

<p>Way back in 2009, Scott Conover discussed the Curve.Intersect method when looking at 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/curves.html">
curves</a> in

his AU 2009 class on 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html">
analysing building geometry</a>. 

<p>Here is a slightly more detailed question by Katsuaki Takamizawa and clarification by Scott on the results returned in the case of overlapping curves:

<p><strong>Question:</strong> Does anyone know the exact meanings of SetComparisonResult.Subset and SetComparisonResult.Superset returned by the Curve.Intersect method?

<p>The API reference explains SetComparisonResult.Subset like this:

<p>1. <cite>The inputs are parallel lines with only one common intersection point...</cite></p>
 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e86d8b517970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e86d8b517970d image-full" alt="Subset one endpoint" title="Subset one endpoint" src="/assets/image_af18c0.jpg" border="0" /></a> <br />

</center>

<p>Does this mean that the curves are parallel and connected at one of their end points as shown above?

<p>2. <cite>... the curve used to invoke the intersection check is a line entirely within the unbound line passed as argument curve.</cite></p>
 
<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e5ffdd5d0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e5ffdd5d0970c image-full" alt="Subset fully contained" title="Subset fully contained" src="/assets/image_0ad438.jpg" border="0" /></a> <br />

</center>

<p>Would this be returned by curve.Intersect(curve1, resultArray), if 'curve' is the highlighted curve above? 
Also, could the two lines be completely overlapped?

<p>SetComparisonResult.Superset is explained like this:

<p>3. <cite>The input curve is entirely within the unbound line used to invoke the intersection check.</cite></p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e358977c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e358977c970b image-full" alt="Superset fully contained" title="Superset fully contained" src="/assets/image_a9f510.jpg" border="0" /></a> <br />

</center>

<p>Would this be returned by curve.Intersect(curve1, resultArray), if 'curve1' is the highlighted curve above? 
And again, could the two lines also be completely overlapped?

<p>I would appreciate if anyone knows about the exact meanings and could confirm them.

<p><strong>Answer:</strong> I think you've got it right:

<ul>
<li>Subset &ndash; the two curves meet at one endpoint, or the input curve is a bound line which lies within the extents of the invoking curve, the 'this' curve, which is the unbound line.
<li>Superset &ndash; the reverse of the second condition above, the 'this' curve is a bound line, the input curve is unbound and overlapping.
</ul>

<p>In either case, according to the documentation, one of the curves must be unbound.  
So two curves which are bound and which overlap would return Overlap instead, unless they were identical, in which case they return Equal.
