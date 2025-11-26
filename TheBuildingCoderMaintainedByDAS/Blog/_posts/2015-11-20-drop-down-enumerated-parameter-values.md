---
layout: "post"
title: "Drop-down Enumerated Parameter Values"
date: "2015-11-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Parameters"
  - "Selection"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/11/drop-down-enumerated-parameter-values.html "
typepad_basename: "drop-down-enumerated-parameter-values"
typepad_status: "Publish"
---

<p>I repeatedly hear from developers who wish to define a specific enumerated set of parameter values for their add-ins and limit the selection to these values in the element property palette user interface.</p>

<p>Internally, Revit does implement a system to handle this, for instance by
using <a href="http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html#5">negative element ids for element property drop-down list enumerations</a>.</p>

<p>This has also been a long-standing wish list item, and unfortunately still remains in that state, currently incorporated in the issue CF-3498 <em>API wish: drop-down enumeration parameters for combo box</em>.</p>

<ul>
<li><a href="#2">Drop-down Combo of Enumerated Parameter Values</a></li>
<li><a href="#3">1. Workaround using Nested Families and Types</a></li>
<li><a href="#4">2. Workaround using Integer Values and Tooltips</a></li>
<li><a href="#5">3. Workaround using Family Instance in a Design Option</a></li>
</ul>

<p>I would not bore you with this, except that Marcelo Quevedo of <a href="http://hsbcad.com">hsbcad</a> recently brought it up again and also kindly provided suggestions for two workarounds:</p>

<h4><a name="2"></a>Drop-down Combo of Enumerated Parameter Values</h4>

<p>In Marcelo's own words:</p>

<p>We need to create drop-down parameters for our families such as Enums in C#.
For instance, a set of enumerated values such as this:</p>

<p>Parameter 1: <code>Orientation</code> with following drop-down values:</p>

<ul>
<li>Parallel to mortise bm</li>
<li>Perpendicular to mortise bm</li>
<li>Parallel to projected Y axis of tenon bm</li>
<li>Perpendicular to projected Y axis of tenon bm</li>
<li>Parallel to projected Z axis of tenon bm</li>
<li>Perpendicular to projected Z axis of tenon bm</li>
</ul>

<p>Parameter 2: <code>Shape</code>:</p>

<ul>
<li>Square</li>
<li>Round</li>
<li>Rounded</li>
</ul>

<p>We found two workarounds, but they aren’t perfect.</p>

<h4><a name="3"></a>1. Workaround using Nested Families and Types</h4>

<p>For the first workaround, we created nested Generic Model families for each drop-down (one nested family for orientation, and other for Shape). We created types for these families named according to the desired drop-down values. In addition, we added two 'Generic Model Family Type' parameters: one to link the Orientation nested family, and one for the Shape nested family. However, the issue is that the 'Generic Model Family Type' parameter links the Category and displays all nested Generic Model family types as available options.</p>

<p>It would be better if the family type parameter would link one single family instead of the entire category.</p>

<p>Here is a screen snapshot of the result:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7ef189e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7ef189e970b image-full img-responsive" alt="Drop-down parameter enum combo" title="Drop-down parameter enum combo" src="/assets/image_6d8df1.jpg" border="0" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a>2. Workaround using Integer Values and Tooltips</h4>

<p>For the second workaround, we created Integer parameters and defined a tooltip to explain what each integer value represents.</p>

<p>For example, for <code>Orientation</code>, we created an integer parameter called <code>Orientation</code> and specified the following tooltip:</p>

<ol>
<li>Parallel to mortise bm</li>
<li>Perpendicular to mortise bm</li>
<li>Parallel to projected Y axis of tenon bm</li>
<li>Perpendicular to projected Y axis of tenon bm</li>
<li>Parallel to projected Z axis of tenon bm</li>
<li>Perpendicular to projected Z axis of tenon bm</li>
</ol>

<p>The result looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb089339a0970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb089339a0970d image-full img-responsive" alt="Drop-down parameter enum combo" title="Drop-down parameter enum combo" src="/assets/image_ffbd7c.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>I hope this helps.</p>

<p>Many thanks to Marcelo for sharing these two creative workarounds!</p>

<h4><a name="5"></a>3. Workaround using Family Instance in a Design Option</h4>

<p>Proposed by Matt Taylor in his <a href="http://thebuildingcoder.typepad.com/blog/2015/11/drop-down-enumerated-parameter-values.html#comment-2843954426">comment below</a>:</p>

<p>I've just come up with another way of doing this.</p>

<p>It's more of a model template solution than a family solution.</p>

<p>Place a family instance with each value in a design option, then just make sure that design option is omitted from all views and schedules.</p>

<p>Because all values for those parameters exist somewhere in the model, they still appear on the pull-down list.</p>

<p>Many thanks to Matt for this nice idea!</p>
