---
layout: "post"
title: "Bottom Face of a Wall"
date: "2009-08-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/08/bottom-face-of-a-wall.html "
typepad_basename: "bottom-face-of-a-wall"
typepad_status: "Publish"
---

<p>We already went into quite a bit of detail analysing the geometry of walls and other elements, but the topic keeps cropping up again anyway.
An overview of the preceding posts on this topic is given in the discussion of 

<a href="http://thebuildingcoder.typepad.com/blog/2009/04/cylindrical-column.html">
cylindrical columns</a>.

Here is a case handled by Joe Ye which I thought might complement the previous posts on this topic, since it is so simple and minimal.

<p><strong>Question:</strong>
How can I find the bottom face of a wall?</p>

<p><strong>Answer:</strong>
We can query the wall for its solid geometry using the Element.Geometry property, which is accessed by the get_Geometry method in C#.
From the returned solid, all faces can be accessed. 
The bottom face of the wall has a normal vector equal to (0,0,-1), and in most cases this is the unique for the bottom face. 
So we just iterate over all faces, and stop when we find one whose normal vector is vertical and has a negative Z coordinate.</p>

<p>I implemented a new external command named CmdWallBottomFace to demonstrate this.
Here is the code of its Execute method.
As always, it returns Failed even if it did in fact succeed, so that no changes are registered in the BIM, since the command does nothing to modify the model:</p>

<pre class="code">
<span class="teal">Application</span> app = commandData.Application;
<span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
<span class="blue">string</span> s = <span class="maroon">&quot;a wall, to retrieve its bottom face&quot;</span>;
&nbsp;
<span class="teal">Wall</span> wall = <span class="teal">Util</span>.SelectSingleElementOfType(
&nbsp; doc, <span class="blue">typeof</span>( <span class="teal">Wall</span> ), s ) <span class="blue">as</span> <span class="teal">Wall</span>;
&nbsp;
<span class="blue">if</span>( <span class="blue">null</span> == wall )
{
&nbsp; message = <span class="maroon">&quot;Please select a wall.&quot;</span>;
}
<span class="blue">else</span>
{
&nbsp; <span class="teal">Options</span> opt = app.Create.NewGeometryOptions();
&nbsp; <span class="teal">GeoElement</span> e = wall.get_Geometry( opt );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> e.Objects )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Solid</span> solid = obj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> solid.Faces )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">PlanarFace</span> pf = face <span class="blue">as</span> <span class="teal">PlanarFace</span>;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != pf )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="teal">Util</span>.IsVertical( pf.Normal, _tolerance )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; pf.Normal.Z &lt; 0 )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg( <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;The bottom face area is {0},&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; and its origin is at {1}.&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.RealString( pf.Area ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.PointString( pf.Origin ) ) );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
}
<span class="blue">return</span> <span class="teal">CmdResult</span>.Failed;
</pre>

<p>When the command is executed and a wall selected, the area and origin point of its bottom face is reported in a message box and in the Visual Studio debug output window:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a4f38d89970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330120a4f38d89970b" alt="Wall bottom face area and origin" title="Wall bottom face area and origin" src="/assets/image_934538.jpg" border="0"  /></a>

</center>

<pre>
The bottom face area is 265.82, 
and its origin is at (30.76,20.57,-9.84).
</pre>

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e168978833012876b76858970c"><a href="http://thebuildingcoder.typepad.com/files/bc11044-3.zip">version 1.1.0.44</a></span>

of the complete Visual Studio solution including the new command.</p>

<p>Thank you very much Joe for this answer!
