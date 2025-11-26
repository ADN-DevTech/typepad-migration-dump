---
layout: "post"
title: "Creating a Curved Beam"
date: "2009-06-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2010"
  - "Element Creation"
  - "Geometry"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/06/creating-a-curved-beam.html "
typepad_basename: "creating-a-curved-beam"
typepad_status: "Publish"
---

<p>Here is a solution from my colleague Joe Ye on how to create a curved beam.
This is also the first and so far only Building Coder sample that demonstrates the use of the Revit API DoubleArray and NurbSpline classes.</p>

<p><strong>Question:</strong>
I am creating curved beams using 

<a href="http://en.wikipedia.org/wiki/NURBS">
NURBS</a>.

When the spline in parallel to the XY plane, all works well:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301157012bc40970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e16897883301157012bc40970c image-full" alt="Curved beam in XY plane" title="Curved beam in XY plane" src="/assets/image_abbe76.jpg" border="0"  /></a>

<p>When the curve is lying in the YZ or XZ plane, however, the resulting beam is straight:</p>


<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330264e2e4f3ef200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330264e2e4f3ef200d img-responsive" alt="Curved beam remains straight" title="Curved beam remains straight" src="/assets/image_1407c9.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />

<p>How can I create the correct curved beam in all orientations?</p>

<p><strong>Answer:</strong>
This was actually a known issue in Revit 2009, and has been fixed in Revit 2010. 
I tested creating a nurbs-defined curved beam in the XZ plane in 2010 and it works well.
Here is the resulting shape in front view:</p>


<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330264e2e4f3f6200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330264e2e4f3f6200d img-responsive" alt="Curved beam is curved" title="Curved beam is curved" src="/assets/image_2a6cff.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />

<p>This is the code of the Execute method used to create this beam, using a set of points in the XZ plane:</p>

<pre class="code">
<span class="teal">Application</span> app = commandData.Application;
<span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
<span class="teal">Level</span> level = doc.ActiveView.Level;
&nbsp;
<span class="teal">FamilySymbol</span> symbol = <span class="blue">null</span>;
&nbsp;
<span class="blue">string</span> path = <span class="maroon">&quot;C:/Documents and Settings&quot;</span>
&nbsp; + <span class="maroon">&quot;/All Users/Application Data/Autodesk&quot;</span>
&nbsp; + <span class="maroon">&quot;/RST 2009/Metric Library/Structural&quot;</span>
&nbsp; + <span class="maroon">&quot;/Framing/Steel/&quot;</span>;
&nbsp;
<span class="blue">string</span> family = <span class="maroon">&quot;M_WWF-Welded Wide Flange&quot;</span>;
&nbsp;
<span class="blue">string</span> ext = <span class="maroon">&quot;.rfa&quot;</span>;
&nbsp;
<span class="blue">string</span> filename = path + family + ext;
&nbsp;
<span class="blue">string</span> symbolName = <span class="maroon">&quot;WWF600x460&quot;</span>;
&nbsp;
<span class="blue">if</span> ( doc.LoadFamilySymbol( filename, symbolName, <span class="blue">out</span> symbol ) )
{
&nbsp; <span class="teal">Curve</span> c = CreateNurbSpline( app );
&nbsp;
&nbsp; <span class="teal">FamilyInstance</span> inst 
&nbsp; &nbsp; = doc.Create.NewFamilyInstance( 
&nbsp; &nbsp; &nbsp; c, symbol, level, <span class="teal">StructuralType</span>.Beam );
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span>.Succeeded;
}
<span class="blue">else</span>
{
&nbsp; message = <span class="maroon">&quot;Couldn't load &quot;</span> + filename;
&nbsp; <span class="blue">return</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span>.Failed;
}
</pre>

<p>The important step is the call to the CreateNurbSpline method, which generates the required input curve from a set of hard-coded points.
Here is the definition of that method:</p>

<pre class="code">
<span class="teal">NurbSpline</span> CreateNurbSpline( <span class="teal">Application</span> app )
{
&nbsp; <span class="teal">XYZArray</span> ctrPoints = app.Create.NewXYZArray();
&nbsp;
&nbsp; <span class="teal">XYZ</span> xyz1 = <span class="blue">new</span> <span class="teal">XYZ</span>( -41.8 * 1, 0, -9.02 * 1 );
&nbsp; <span class="teal">XYZ</span> xyz2 = <span class="blue">new</span> <span class="teal">XYZ</span>( -9.2 * 2, 0, 0.82 * 50 );
&nbsp; <span class="teal">XYZ</span> xyz3 = <span class="blue">new</span> <span class="teal">XYZ</span>( 9.2 * 2, 0, -0.82 * 50 );
&nbsp; <span class="teal">XYZ</span> xyz4 = <span class="blue">new</span> <span class="teal">XYZ</span>( 41.8 * 1, 0, 9.02 * 1 );
&nbsp;
&nbsp; ctrPoints.Append( xyz1 ); 
&nbsp; ctrPoints.Append( xyz2 ); 
&nbsp; ctrPoints.Append( xyz3 );
&nbsp; ctrPoints.Append( xyz4 );
&nbsp;
&nbsp; <span class="teal">DoubleArray</span> weights = <span class="blue">new</span> <span class="teal">DoubleArray</span>();
&nbsp;
&nbsp; <span class="blue">double</span> w1 = 1, w2 = 1, w3 = 1, w4 = 1;
&nbsp;
&nbsp; weights.Append( <span class="blue">ref</span> w1 ); 
&nbsp; weights.Append( <span class="blue">ref</span> w2 ); 
&nbsp; weights.Append( <span class="blue">ref</span> w3 );
&nbsp; weights.Append( <span class="blue">ref</span> w4 );
&nbsp;
&nbsp; <span class="teal">DoubleArray</span> knots = <span class="blue">new</span> <span class="teal">DoubleArray</span>();
&nbsp;
&nbsp; <span class="blue">double</span> k0 = 0, k1 = 0, k2 = 0, k3 = 0, 
&nbsp; &nbsp; k4 = 34.425128, k5 = 34.425128, 
&nbsp; &nbsp; k6 = 34.425128, k7 = 34.425128;
&nbsp;
&nbsp; knots.Append( <span class="blue">ref</span> k0 ); 
&nbsp; knots.Append( <span class="blue">ref</span> k1 ); 
&nbsp; knots.Append( <span class="blue">ref</span> k2 ); 
&nbsp; knots.Append( <span class="blue">ref</span> k3 );
&nbsp; knots.Append( <span class="blue">ref</span> k4 ); 
&nbsp; knots.Append( <span class="blue">ref</span> k5 ); 
&nbsp; knots.Append( <span class="blue">ref</span> k6 );
&nbsp; knots.Append( <span class="blue">ref</span> k7 );
&nbsp;
&nbsp; <span class="teal">NurbSpline</span> detailNurbSpline 
&nbsp; &nbsp; = app.Create.NewNurbSpline( 
&nbsp; &nbsp; ctrPoints, weights, knots, 3, <span class="blue">false</span>, <span class="blue">true</span> );
&nbsp;
&nbsp; <span class="blue">return</span> detailNurbSpline;
}
</pre>

<p>Many thanks to Joe for providing this solution!</p>
