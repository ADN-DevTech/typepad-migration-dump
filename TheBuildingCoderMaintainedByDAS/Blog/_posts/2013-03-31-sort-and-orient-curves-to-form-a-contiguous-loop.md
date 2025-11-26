---
layout: "post"
title: "Sort and Orient Curves to Form a Contiguous Loop"
date: "2013-03-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Desktop"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/03/sort-and-orient-curves-to-form-a-contiguous-loop.html "
typepad_basename: "sort-and-orient-curves-to-form-a-contiguous-loop"
typepad_status: "Publish"
---

<p>Continuing the research and development for my

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/cloud-mobile-extensible-storage-data-use-in-schedules.html#3">
cloud-based round-trip 2D Revit model editing project</a>,

I need to determine the boundary loop polygons to represent the furniture and equipment family instances for manipulation on the mobile device.

<p>To display a polygon using SVG in the mobile device browser, I obviously need a set of contiguous and sorted curve elements forming a closed loop.</p>

<p>I mentioned using the Revit API ExtrusionAnalyzer class to determine the plan view boundary outline for the family instances.
On testing that approach, I discovered that the results it returns are unsorted.</p>

<p>For instance, I analysed the ExtrusionAnalyzer output for the standard Revit furniture content 'Desk 1525 x 762mm'.
In plan view, it can appear like this:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee9dedafe970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee9dedafe970d image-full" alt="Plan view of room with furniture" title="Plan view of room with furniture" src="/assets/image_2219fe.jpg" border="0" /></a><br />

</center>

<p>The desk includes lots of internal geometry that is not visible in plan view:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee9dede96970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee9dede96970d" alt="Desk_and_chair" title="Desk_and_chair" src="/assets/image_5750ef.jpg" /></a><br />

</center>

<p>As a result, the information returned by the ExtrusionAnalyzer is much more complex than the simple rectangle one might expect.
In fact, it returns ten separate closed loops, and the first one consists of eight curves.</p>

<p>To check whether they are contiguous and correctly sorted, here is a list of their end points:</p>

<pre>
  (2.74,8.38,0) --&gt; (2.74,8.46,0)
  (2.74,8.38,0) --&gt; (2.76,8.38,0)
  (2.76,8.38,0) --&gt; (2.76,8.44,0)
  (2.76,8.44,0) --&gt; (3.05,8.44,0)
  (3.05,8.44,0) --&gt; (3.05,8.38,0)
  (3.05,8.38,0) --&gt; (3.08,8.38,0)
  (3.08,8.46,0) --&gt; (3.08,8.38,0)
  (2.74,8.46,0) --&gt; (3.08,8.46,0)
</pre>

<p>You can see quite easily that they are not contiguous.
For instance, the end point at (2.74,8.46) of the first curve equals the start point of the last.</p>

<p>If you look more carefully still, you will notice that some of the curves require reversing to connect to their neighbours.</p>

<p>These observations and considerations led to the implementation of the following curve sorting and orientation method.</p>

<p>Its end point matching comparison relies on the standard Revit precision, which is around one sixteenth of an inch.
Since the built-in Revit database length unit is feet, I define the following fuzz factor for that:</p>

<pre class="code">
&nbsp; <span class="blue">const</span> <span class="blue">double</span> _inch = 1.0 / 12.0;
&nbsp; <span class="blue">const</span> <span class="blue">double</span> _sixteenth = _inch / 16.0;
</pre>

<p>Further, the curve reversal is implemented by creating a completely new curve.
Therefore, the creation application has to be provided for generating these:</p>

<pre class="code">
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Create a new curve with the same </span>
<span class="gray">///</span><span class="green"> geometry in the reverse direction.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;orig&quot;&gt;</span><span class="green">The original curve.</span><span class="gray">&lt;/param&gt;</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;returns&gt;</span><span class="green">The reversed curve.</span><span class="gray">&lt;/returns&gt;</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;throws cref=&quot;NotImplementedException&quot;&gt;</span><span class="green">If the </span>
<span class="gray">///</span><span class="green"> curve type is not supported by this utility.</span><span class="gray">&lt;/throws&gt;</span>
<span class="blue">static</span> <span class="teal">Curve</span> CreateReversedCurve(
&nbsp; Autodesk.Revit.Creation.<span class="teal">Application</span> creapp,
&nbsp; <span class="teal">Curve</span> orig )
{
&nbsp; <span class="blue">if</span>( !IsSupported( orig ) )
&nbsp; {
&nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">NotImplementedException</span>(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;CreateReversedCurve for type &quot;</span>
&nbsp; &nbsp; &nbsp; + orig.GetType().Name );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">if</span>( orig <span class="blue">is</span> <span class="teal">Line</span> )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> creapp.NewLineBound(
&nbsp; &nbsp; &nbsp; orig.GetEndPoint( 1 ),
&nbsp; &nbsp; &nbsp; orig.GetEndPoint( 0 ) );
&nbsp; }
&nbsp; <span class="blue">else</span> <span class="blue">if</span>( orig <span class="blue">is</span> <span class="teal">Arc</span> )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> creapp.NewArc( orig.GetEndPoint( 1 ),
&nbsp; &nbsp; &nbsp; orig.GetEndPoint( 0 ),
&nbsp; &nbsp; &nbsp; orig.Evaluate( 0.5, <span class="blue">true</span> ) );
&nbsp; }
&nbsp; <span class="blue">else</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">Exception</span>(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;CreateReversedCurve - Unreachable&quot;</span> );
&nbsp; }
}
</pre>

<p>With this support in hand, we can go ahead and implement the SortCurvesContiguous method:</p>

<pre class="code">
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Sort a list of curves to make them correctly </span>
<span class="gray">///</span><span class="green"> ordered and oriented to form a closed loop.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> SortCurvesContiguous(
&nbsp; Autodesk.Revit.Creation.<span class="teal">Application</span> creapp,
&nbsp; <span class="teal">IList</span>&lt;<span class="teal">Curve</span>&gt; curves,
&nbsp; <span class="blue">bool</span> debug_output )
{
&nbsp; <span class="blue">int</span> n = curves.Count;
&nbsp;
&nbsp; <span class="green">// Walk through each curve (after the first) </span>
&nbsp; <span class="green">// to match up the curves in order</span>
&nbsp;
&nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; n; ++i )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Curve</span> curve = curves[i];
&nbsp; &nbsp; <span class="teal">XYZ</span> endPoint = curve.GetEndPoint( 1 );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( debug_output )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;{0} endPoint {1}&quot;</span>, i,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.PointString( endPoint ) );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> p;
&nbsp;
&nbsp; &nbsp; <span class="green">// Find curve with start point = end point</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">bool</span> found = (i + 1 &gt;= n);
&nbsp;
&nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> j = i + 1; j &lt; n; ++j )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; p = curves[j].GetEndPoint( 0 );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// If there is a match end-&gt;start, </span>
&nbsp; &nbsp; &nbsp; <span class="green">// this is the next curve</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( _sixteenth &gt; p.DistanceTo( endPoint ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( debug_output )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{0} start point, swap with {1}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; j, i + 1 );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( i + 1 != j )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Curve</span> tmp = curves[i + 1];
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curves[i + 1] = curves[j];
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curves[j] = tmp;
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; found = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; p = curves[j].GetEndPoint( 1 );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// If there is a match end-&gt;end, </span>
&nbsp; &nbsp; &nbsp; <span class="green">// reverse the next curve</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( _sixteenth &gt; p.DistanceTo( endPoint ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( i + 1 == j )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( debug_output )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{0} end point, reverse {1}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; j, i + 1 );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curves[i + 1] = CreateReversedCurve(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; creapp, curves[j] );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( debug_output )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{0} end point, swap with reverse {1}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; j, i + 1 );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Curve</span> tmp = curves[i + 1];
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curves[i + 1] = CreateReversedCurve(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; creapp, curves[j] );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curves[j] = tmp;
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; found = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">if</span>( !found )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">Exception</span>( <span class="maroon">&quot;SortCurvesContiguous:&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; non-contiguous input curves&quot;</span> );
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>This is obviously not the most effective sorting algorithm in the world, but it should do for the hopefully simple cases I expect to encounter.</p>

<p>This method includes an option for verbose logging to the Visual Studio debug output window.
This is what it produces for the unsorted list of input points provided above:</p>

<pre>
  0 endPoint (2.74,8.46,0)
  7 start point, swap with 1
  1 endPoint (3.08,8.46,0)
  6 start point, swap with 2
  2 endPoint (3.08,8.38,0)
  5 end point, swap with reverse 3
  3 endPoint (3.05,8.38,0)
  4 end point, reverse 4
  4 endPoint (3.05,8.44,0)
  5 end point, reverse 5
  5 endPoint (2.76,8.44,0)
  6 end point, reverse 6
  6 endPoint (2.76,8.38,0)
  7 end point, reverse 7
  7 endPoint (2.74,8.38,0)
</pre>

<p>The complete output for the entire desk looks like this after sorting and rearranging all ten loops using the SortCurvesContiguous method and converting the results from Revit XYZ coordinates to my

<a href="http://thebuildingcoder.typepad.com/blog/2013/03/revit-2014-api-and-room-plan-view-boundary-polygon-loops.html">
2D integer-based loop</a> representation in millimetres:</p>

<pre>
FamilyInstance Furniture Desk &lt;212646 1525 x 762mm&gt; has 10 loops:
  0: (836,2555), (836,2580), (937,2580), (937,2555), (931,2555), (931,2574), (842,2574), (842,2555)
  1: (1954,2580), (1954,2555), (1961,2555), (1961,2574), (2050,2574), (2050,2555), (2056,2555), (2056,2580)
  2: (683,2542), (683,1780), (2208,1780), (2208,2542), (1802,2542), (1802,1831), (1090,1831), (1090,2542)
  3: (664,2561), (664,1761), (2227,1761), (2227,2561)
  4: (683,2440), (785,2440), (785,2542), (683,2542)
  5: (785,1780), (785,1882), (683,1882), (683,1780)
  6: (2107,1882), (2107,1780), (2208,1780), (2208,1882)
  7: (2107,2542), (2107,2440), (2208,2440), (2208,2542)
  8: (702,2542), (702,2555), (1071,2555), (1071,2542)
  9: (1821,2542), (1821,2555), (2189,2555), (2189,2542)
</pre>

<p>I hope you can make good use of this method in your own add-ins as well.</p>

<p>Happy Easter!</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d426ab988970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d426ab988970c" alt="Happy Easter!" title="Happy Easter!" src="/assets/image_3ad5c6.jpg" border="0" /></a><br />

</center>
