---
layout: "post"
title: "NewWall with Opening"
date: "2010-03-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Element Creation"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/03/newwall-with-opening.html "
typepad_basename: "newwall-with-opening"
typepad_status: "Publish"
---

<p>We recently discussed a comment from Mike King on how to

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/creating-a-nonrectangular-slab.html?cid=6a00e553e1689788330120a8a1ff3a970b#comment-6a00e553e1689788330120a8a1ff3a970b">
specify openings when constructing a slab</a>,

and that reminded me of some interesting sample code I received from Ning in a comment on using the NewWall method and passing in a CurveArray containing its edges to

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html">
create a wall with a sloped profile</a>.

Ning's issue was to create a new wall with a

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html?cid=6a00e553e1689788330120a6cd4ccf970b#comment-6a00e553e1689788330120a6cd4ccf970b">
CurveArray containing more than one loop</a>.

<p>What Ning's code does is to retrieve the elevation profile from an existing wall and then create a wall siding covering it.
Here is an example wall with an opening before processing:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301310f8116d5970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301310f8116d5970c" alt="Wall with an opening" title="Wall with an opening" src="/assets/image_71115d.jpg" border="0"  /></a> <br />

</center>

<p>When you launch Ning's command on it, it first asks you which type of wall siding you would like to add:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a91a7c81970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a91a7c81970b" alt="Wall siding selection" title="Wall siding selection" src="/assets/image_769386.jpg" border="0"  /></a> <br />

</center>

<p>The command then queries the existing wall for its profile including all the inner loops representing openings and converts them to a CurveArray in the proper format for calling the NewWall method to create the desired siding complete with all openings:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301310f811561970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301310f811561970c" alt="Wall siding with opening" title="Wall siding with opening" src="/assets/image_526236.jpg" border="0"  /></a> <br />

</center>

<p>I tried to adapt Ning's method to create a slab with openings for Mike, but could not get it to work right away and gave up again on that.
Also, Ning needed to apply a workaround to his code in some cases.
However, it does demonstrate that it is possible to create a new wall complete with openings in one single fell swoop, which is why I wanted to present it here.

<p>Ning implements a class WallProfiles which provides the following functionality:

<ul>
<li>Construct a list of wall siding types and prompt the user to select which one to use.
<li>Retrieve all walls or just the preselected ones.
<li>Determine their

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-elevation-profile.html">
wall elevation profiles</a> using the GetProfiles method.

</ul>

<p>GetProfiles creates a List&lt;List&lt;XYZ&gt;&gt; instance to represent each wall profile, where the inner lists represent the individual loops.
A wall with no openings will have just one single loop, and the first loop is always the outer one, if I remember correctly.

<p>In order to make the calls to NewWall, we need to convert these profile definitions to Revit API CurveArray instances.
This is achieved by the following algorithm:

<pre class="code">
<span class="teal">List</span>&lt;<span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;&gt; profile = wps.m_Profiles[ori];
&nbsp;
<span class="teal">CurveArray</span> ca = <span class="blue">new</span> <span class="teal">CurveArray</span>();
&nbsp;
<span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; profile.Count; i++ )
{
&nbsp; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; pts = profile[i];
&nbsp;
&nbsp; <span class="blue">for</span>( <span class="blue">int</span> j = 0; j &lt; pts.Count - 1; ++j )
&nbsp; {
&nbsp; &nbsp; ca.Append(
&nbsp; &nbsp; &nbsp; <span class="teal">Line</span>.get_Bound( pts[j], pts[j + 1] )
&nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">Curve</span> );
&nbsp; }
&nbsp; ca.Append(
&nbsp; &nbsp; <span class="teal">Line</span>.get_Bound( pts[pts.Count - 1], pts[0] )
&nbsp; &nbsp; <span class="blue">as</span> <span class="teal">Curve</span> );
}
</pre>

<p>Once converted, the resulting CurveArray instances are stored in a dictionary

<pre class="code">
&nbsp; <span class="teal">Dictionary</span>&lt;<span class="teal">XYZ</span>, <span class="teal">CurveArray</span>&gt; dictCa
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">Dictionary</span>&lt;<span class="teal">XYZ</span>, <span class="teal">CurveArray</span>&gt;();
</pre>

<p>The dictionary of CurveArray instances is processed to generate all the wall siding elements using the appropriate type and level:

<pre class="code">
<span class="teal">WallType</span> wt = wps.SelectedSidingType
&nbsp; <span class="blue">as</span> <span class="teal">WallType</span>;
&nbsp;
<span class="teal">Level</span> lv = wps.m_Level;
&nbsp;
<span class="blue">foreach</span>( <span class="teal">XYZ</span> ori <span class="blue">in</span> dictCa.Keys )
{
&nbsp; <span class="blue">try</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">Wall</span> wa = doc.Create.NewWall(
&nbsp; &nbsp; &nbsp; dictCa[ori], wt, lv, <span class="blue">false</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">double</span> ang = wa.Orientation
&nbsp; &nbsp; &nbsp; .Normalized.Angle( ori );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( ang &gt; PRECISION )
&nbsp; &nbsp; &nbsp; wa.flip();
&nbsp; }
&nbsp; <span class="blue">catch</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg( <span class="maroon">&quot;cannot create the wall siding&quot;</span> );
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">CmdResult</span>.Failed;
&nbsp; }
}
</pre>

<p>Here is the complete

<span class="asset  asset-generic at-xid-6a00e553e1689788330120a91a7acf970b"><a href="http://thebuildingcoder.typepad.com/files/wallsiding.zip">WallSiding</a></span>

source code and Visual Studio solution including both the sample model we used and a second similar one.
The second one can be used to reproduce an issue with the NewWall call that Ning encountered.
The model is almost identical to the first one in terms of wall and window location.
In spite of this, it requires an additional tweak in WallProfiles.cs, which is initially commented out.

<p>Many thanks to Ning for this very useful sample code which shows how elegantly we can query the model for existing wall geometry and reuse it to create new walls complete with openings!
