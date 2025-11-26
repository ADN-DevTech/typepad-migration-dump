---
layout: "post"
title: "Wall Footing Relationship Revisited"
date: "2012-10-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "Element Relationships"
  - "Migration"
  - "RST"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/10/wall-footing-relationship-revisited.html "
typepad_basename: "wall-footing-relationship-revisited"
typepad_status: "Publish"
---

<p>In the far distant past, I looked at various ways to programmatically determine 

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/object-relationships.html">
relationships between associated objects</a>

(also in <a href="http://thebuildingcoder.typepad.com/blog/2010/03/object-relationships-in-vb.html">VB</a>),

including references between a host object and its hosted dependent elements, e.g. determining the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/host-reference.html">
relationship between a wall and its footing</a>.

<p>That was back in the year 2009, using Revit 2010.

<p>A few things have changed and been added since then, but the more things change, the more they stay the same, don't they?

<p>Anyway, this very question was raised again:


<p><strong>Question:</strong> Is there a way to obtain a reference to a wall footing from the wall attached to it?


<p><strong>Answer:</strong> Yes, sure, there are several different possibilities to achieve this.

<p>You could start off by determining the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/bottom-face-of-a-wall.html">
bottom face of the wall</a>.

<p>Using that, you could set up an ElementIntersectsSolidFilter for a filtered element collector to retrieve all foundation elements touching or intersecting that face, as described for 

<a href="http://thebuildingcoder.typepad.com/blog/2012/09/filter-for-touching-beams-using-solid-intersection.html">
retrieving all touching beams</a>.

You would obviously want to apply as many quick filters as possible before taking recourse to the slow geometric filtering, e.g. check for level, category, type, etc. first.

<p>Another way to determine adjacency of Revit BIM elements is to make use of the ray casting functionality provided by the FindReferencesByDirection method, FindReferencesWithContextByDirection method and the ReferenceIntersector class, e.g. cast a ray downwards from the wall and detect the first foundation hit by it.

<p>Last but not least, another way to determine relationships between associated objects such as a wall and its footing is to temporarily delete the host object, causing the dependent objects to be deleted as well. 
The deleted element ids can be determined, and the whole operation undone to leave the model unchanged.

<p>I described this principle several times, e.g. looking at

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/object-relationships.html">
object relationships</a>

(in <a href="http://thebuildingcoder.typepad.com/blog/2010/03/object-relationships-in-vb.html">VB</a>),

and specifically 

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/host-reference.html">
determining the wall footing of a given wall</a>.

<p>I cannot tell off-hand which of these methods is most useful and efficient in your specific case, although I would be very interested in seeing the results of some comparisons and benchmarks evaluating this.

<p>Anyway, I note that The Building Coder sample command CmdWallFooting implementing the latter approach is pretty much out of date, having been simply flat ported across several Revit API releases in the past few years.

<p>I therefore updated it for and tested it with Revit 2013, and can now confirm that it still works.

<p>Here is the updated implementation:

<pre class="code">
[<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">class</span> <span class="teal">CmdWallFooting</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> app = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = app.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="teal">Wall</span> wall = <span class="teal">Util</span>.SelectSingleElementOfType(
&nbsp; &nbsp; &nbsp; uidoc, <span class="blue">typeof</span>( <span class="teal">Wall</span> ), <span class="maroon">&quot;a wall&quot;</span>, <span class="blue">false</span> ) 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">Wall</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span> ( <span class="blue">null</span> == wall )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; message 
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="maroon">&quot;Please select a single wall element.&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">ICollection</span>&lt;<span class="teal">ElementId</span>&gt; delIds = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> t = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; t.Start( <span class="maroon">&quot;Temporary Wall Deletion&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; delIds = doc.Delete( wall );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; t.RollBack();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">catch</span> ( <span class="teal">Exception</span> ex )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; message = <span class="maroon">&quot;Deletion failed: &quot;</span> + ex.Message;
&nbsp; &nbsp; &nbsp; &nbsp; t.RollBack();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">ContFooting</span> footing = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span> ( <span class="teal">ElementId</span> id <span class="blue">in</span> delIds )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; footing = doc.GetElement( id ) <span class="blue">as</span> <span class="teal">ContFooting</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != footing )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> s = <span class="teal">Util</span>.ElementDescription( wall );
&nbsp;
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg( ( <span class="blue">null</span> == footing )
&nbsp; &nbsp; &nbsp; ? <span class="blue">string</span>.Format( <span class="maroon">&quot;No footing found for {0}.&quot;</span>, s )
&nbsp; &nbsp; &nbsp; : <span class="blue">string</span>.Format( <span class="maroon">&quot;{0} has {1}.&quot;</span>, s,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.ElementDescription( footing ) ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<p>I ran this command on a minimal sample model containing just one structural wall with its footing:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee408f1d2970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee408f1d2970d" alt="Structural wall and footing" title="Structural wall and footing" src="/assets/image_1a123f.jpg" border="0" /></a><br />

</center>

<p>The command reports the expected relationship:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3265325f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3265325f970b image-full" alt="Wall footing relationship" title="Wall footing relationship" src="/assets/image_11c318.jpg" border="0" /></a><br />

</center>

<p>It says:

<pre>
Wall Walls <164432 Generic - 200mm> has 
ContFooting Structural Foundations <164470 
Retaining Footing - 300 x 600 x 300>.
</pre>

<p>Here is 

<span class="asset  asset-generic at-xid-6a00e553e168978833017c326531b4970b"><a href="http://thebuildingcoder.typepad.com/files/bc_13_99_3.zip">version 2013.0.99.3</a></span> of 

The Building Coder samples including the updated CmdWallFooting external command.
