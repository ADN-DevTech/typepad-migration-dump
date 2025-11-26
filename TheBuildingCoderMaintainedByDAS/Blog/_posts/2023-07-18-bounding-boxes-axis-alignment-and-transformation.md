---
layout: "post"
title: "Bounding Boxes Axis Alignment and Transformation"
date: "2023-07-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Algorithm"
  - "ChatGPT"
  - "Geometry"
  - "HTML"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/07/bounding-boxes-axis-alignment-and-transformation.html "
typepad_basename: "bounding-boxes-axis-alignment-and-transformation"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<!--
Today we present ground-breaking research on accessing all built-in parameters and categories, thoughts on axis-aligned bounding boxes and AI news:
-->

<p>Today we look at some aspects axis-aligned bounding boxes, bounding box transformation and AI news:</p>

<!--
- [All categories and parameters](#2)
    - [Summary](#2.1)
    - [Solution](#2.2)
    - [Result](#2.3)
    - [Limitations](#2.4)
-->

<ul>
<li><a href="#3">BoundingBox is axis-aligned</a></li>
<li><a href="#4">BoundingBox transformation</a></li>
<li><a href="#5">Interactive explanation of SVG path</a></li>
<li><a href="#6">Claude.AI</a></li>
<li><a href="#7">Relativising the impact of AI</a></li>
</ul>

<!--
####<a name="2"></a> All Categories and Parameters

Roman [Nice3point](https://github.com/Nice3point), principle maintained of RevitLookup, presents a new discovery; in his own words:

I made a new discovery for me.
As far as I know, it is impossible to get a list of all built-in parameters using the public Revit API.
We are offered an `enum`, but we cannot get the `Parameter` itself from it.
By exploring private unmanaged code using reflection and pointers, I managed to do it, as described in
the [RevitLookup discussion 183 &ndash; retrieve all parameters and categories](https://github.com/jeremytammik/RevitLookup/discussions/183):

<center>
<img src="img/rk_get_all_bips.png" alt="Get all built-in parameters" title="Get all built-in parameters" width="600"/>
</center>

####<a name="2.1"></a> Summary

I recently came across a problem from my business partner who wanted to get all the built-in Revit parameters to extract metadata such as data type, units, storage type, etc. As we know, this is not possible using the Autodesk Revit public API.

Similar situation with categories; we can't get all the built-in categories; so far, the only known available way is to get them from the document settings, but it contains a very truncated list:

<center>
<img src="img/rk_get_all_bips_1_categories.png" alt="Only some categories" title="Only some categories" width="600"/>
</center>

By the way,
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/tree/master) include
some related naive attempts in
the [module `CmdCategories.cs`](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/CmdCategories.cs).

####<a name="2.2"></a> Solution

We can use private code with reflection and pointers; we only need a document.
The point of the method is not to get them but create it.

Example code to get all parameters:



38eee7e926fdef5ad8046b8d6bf6e7a6



Example code to get all categories:



6d806d448ebf5d8e657e0233380fd6af



####<a name="2.3"></a> Result

As a result, we have created all the parameters and all the categories of the entire `Enum`:

<center>
<img src="img/rk_get_all_bips_2_all_params.png" alt="All built-in parameters" title="All built-in parameters" width="600"/>


4396f96adfcacd78b3c8292717a01c20



<img src="img/rk_get_all_bips_3_all_cats.png" alt="All built-in categories" title="All built-in categories" width="600"/>


dfa7791780f87ff3a2b5657ef2c6ad37


</center>

####<a name="2.4"></a> Limitations

Created parameters have no binding to any element, and consequently have no value, only metadata.

Many thanks to Roman for this interesting in-depth research and documentation!

-->

<h4><a name="3"></a> BoundingBox is Axis-Aligned</h4>

<p>Let's move on to the more mundane question
of <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-boundingbox-that-corresponds-to-the-shape-of-the/m-p/12089970">how to get the <code>BoundingBox</code> that corresponds to the shape of the family</a>:</p>

<p><strong>Question:</strong> I would like to inquire about <code>BoundingBox</code>.
In the left image, the area of the BoundingBox is similar to the shape of the pipe;
however, in the right one, the BoundingBox is much larger than its pipe:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b751ac56ff200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b751ac56ff200c image-full img-responsive" alt="Pipe bounding box" title="Pipe bounding box" src="/assets/image_f31dea.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>It seems that this happens only for Pipes (probably for System Families).
For FamilyInstance, it seems that they always have a same size for BoundingBox, even if they are rotated.</p>

<p>Is there a way to know the area of the Family, which always corresponds to its shape, regardless of it being rotated or not?</p>

<p><strong>Answer:</strong>  No, this does not only happen for pipes.
This happens for all kinds of objects that have different dimensions in different directions and are placed at an angle to the cardinal axes.
For instance, a vertical wall at a 45-degree angle in the XY plane will exhibit the exact same behaviour.
This behaviour is intentional.
The Revit bounding box is always aligned with the cardinal axes.
That makes it extremely fast and efficient to work with.
Wikipedia calls this
an <a href="https://en.wikipedia.org/wiki/Minimum_bounding_box#Axis-aligned_minimum_bounding_box">axis-aligned bounding box</a>.</p>

<h4><a name="4"></a> BoundingBox Transformation</h4>

<p>That question leads over nicely to the explanation why
the <a href="https://forums.autodesk.com/t5/revit-api-forum/transform-of-linked-element-creates-an-empty-outline/m-p/12089717">transform of linked element creates an empty outline</a>,
solved by appropriately transforming the bounding box:</p>

<p><strong>Question:</strong> I want to create an outline from a linked element's bounding box to use in a <code>BoundingBoxIntersectsFilter</code>.
My approach below works for some cases, but where a link or element is rotated beyond a certain limit, the bounding box goes askew and the <code>Outline(XYZ, XYZ)</code> method returns an empty outline.
I've tried scaling the bounding box up with an offset which fixes some cases, but not all of them.
Some advice on solutions would be appreciated.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1b25aadb0200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1b25aadb0200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Non-rotated link" title="Non-rotated link"  src="/assets/image_421443.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Non-rotated link</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6cef2af200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6cef2af200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Rotated link" title="Rotated link"  src="/assets/image_ed26d8.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Rotated link</p>

<p></center></p>

<pre class="prettyprint">
# get the elements bounding box
s_BBox = element.get_BoundingBox(doc.ActiveView)

# apply the link documents transform
s_BBox_min = link_trans.OfPoint(s_BBox.Min)
s_BBox_max = link_trans.OfPoint(s_BBox.Max)

# make the outline
new_outline = Outline(s_BBox_min, s_BBox_max)

# make the filter
bb_filter = BoundingBoxIntersectsFilter(new_outline)
</pre>

<p><strong>Answer:</strong> Yes.
You can corrupt the bounding box by transforming it.
I would suggest the following:</p>

<ul>
<li>Extract all eight corner vertices of the bounding box</li>
<li>Transform all eight vertices as individual points</li>
<li>Create a new bounding box from the eight transformed results</li>
</ul>

<p><a href="https://github.com/jeremytammik/the_building_coder_samples/tree/master">The Building Coder samples</a> include
the <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/Util.cs#L2724-L2739">method <code>ExpandToContain</code></a> to
create and enlarge a bounding box point by point that will come in handy for the last step:</p>

<pre class="prettyprint">
  /// &lt;summary&gt;
  ///   Expand the given bounding box to include
  ///   and contain the given point.
  /// &lt;/summary&gt;
  public static void ExpandToContain(
    this BoundingBoxXYZ bb,
    XYZ p)
  {
    bb.Min = new XYZ(Math.Min(bb.Min.X, p.X),
      Math.Min(bb.Min.Y, p.Y),
      Math.Min(bb.Min.Z, p.Z));

    bb.Max = new XYZ(Math.Max(bb.Max.X, p.X),
      Math.Max(bb.Max.Y, p.Y),
      Math.Max(bb.Max.Z, p.Z));
  }
</pre>

<p><strong>Response:</strong> Hi Jeremy, this solution works well, thanks.</p>

<h4><a name="5"></a> Interactive Explanation of SVG Path</h4>

<p>I made good use of and learned to love SVG paths working on
the <a href="http://thebuildingcoder.typepad.com/blog/2012/10/room-polygon-and-furniture-picker-in-svg.html">room polygon and furniture picker in SVG</a> and
implementing <a href="http://thebuildingcoder.typepad.com/blog/2013/02/2d-svg-editing-on-mobile-device-with-rapha%C3%ABl.html">2D SVG editing on mobile device with Raphael</a>.</p>

<p>If you would like to enjoy a much nicer explanation of the concepts of SVG paths than I had access to back then, take a quick dive
into <a href="https://www.nan.fyi/svg-paths">understanding SVG paths</a>.</p>

<h4><a name="6"></a> Claude.AI</h4>

<p>ChatGPT has a new competitor,
<a href="https://claude.ai">claude.ai</a>.</p>

<p>Eric Boehlke of <a href="https://truevis.com">truevis BIM Consulting</a> pointed it out to me, saying:</p>

<blockquote>
  <p>You can create an account in the USA or UK, or use VPN to get there.
  A competitor to OpenAI. Can upload big files. Any better for coding?</p>
</blockquote>

<p>I tested it myself by asking it to summarise
my <a href="https://thebuildingcoder.typepad.com/blog/2023/07/rbp-materials-their-assets-and-the-visual-api.html">last blog post</a>,
evaluate a German architectural contract without providing any hint of what it might be, and discuss
the German <a href="https://de.wikipedia.org/wiki/Honorarordnung_f%C3%BCr_Architekten_und_Ingenieure">HOAI</a>.
The results were completely satisfying in all three cases:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6cef2b2200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6cef2b2200b image-full img-responsive" alt="Claude.AI analyses blog post" title="Claude.AI analyses blog post"  src="/assets/image_423538.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Claude.AI analyses blog post</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6cef2b8200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6cef2b8200b image-full img-responsive" alt="Claude.AI analyses a German architectural contract" title="Claude.AI analyses a German architectural contract" src="/assets/image_9972c8.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Claude.AI analyses a German architectural contract</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c1a6cef2bb200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c1a6cef2bb200b image-full img-responsive" alt="Claude.AI discusses the HOAI" title="Claude.AI discusses the HOAI"  src="/assets/image_60c108.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Claude.AI discusses the HOAI</p>

<p></center></p>

<p>Eric suggests:</p>

<blockquote>
  <p>You could batch that blog post analysis to summarise all of your blog posts programmatically for SEO.</p>
</blockquote>

<h4><a name="7"></a> Relativising the Impact of AI</h4>

<p>Let's close with some wise and pertinent thoughts &ndash; not following the hype &ndash; on the possible impact of AI on our life and economy, presented in the explanation
of <a href="https://zhengdongwang.com/2023/06/27/why-transformative-ai-is-really-really-hard-to-achieve.html">why transformative AI is really, really hard to achieve</a>.</p>
