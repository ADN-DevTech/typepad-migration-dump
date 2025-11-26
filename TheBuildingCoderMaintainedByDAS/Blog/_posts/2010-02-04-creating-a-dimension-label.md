---
layout: "post"
title: "Creating a Dimension Label"
date: "2010-02-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Geometry"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/02/creating-a-dimension-label.html "
typepad_basename: "creating-a-dimension-label"
typepad_status: "Publish"
---

<p>In a comment on the discussion of the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/the-revit-family-api.html">
family API</a>,

Nadim asked how to create a label in a family document, and Harry Mattison of Autodesk very friendlily provided some sample source code to generate a new dimension label.
I hope this is what you were looking for.

<p><strong>Question:</strong> I am trying to use the FamilyItemFactory to create a label inside the family document. 
I can see all kinds of creation functions like NewDimension, NewSweep, NewTextNote, but I can't find any function to do NewLabel. 
You can do that in the family editor user interface, so it should be possible through the API as well, shouldn't it?
The label might be the most common element used in Annotation Families.

<p><strong>Answer:</strong> As said, Harry provided a code snippet to create a dimension label in a family document, which I used to implement a new Building Coder sample command CmdNewDimensionLabel.

<p>It creates two model lines which provide the references required to define a dimension and sets up the appropriate reference array.
The reference array is used when calling NewLinearDimension to create a new dimension entity between the two lines.
Finally, a new family parameter named "length" is created and associated with the dimension label.

<p>When I first tried to run this in a new family document based on the annotation family, I was told by Revit that the creation of a new sketch plane is not allowed in that context, so I added a helper method findSketchPlane which returns a sketch plane from the given document with the specified normal vector, if one exists, or else null:

<pre class="code">
<span class="blue">static</span> <span class="teal">SketchPlane</span> findSketchPlane( 
&nbsp; <span class="teal">Document</span> doc, 
&nbsp; <span class="teal">XYZ</span> normal )
{
&nbsp; <span class="teal">SketchPlane</span> result = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; a = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;();
&nbsp;
&nbsp; <span class="blue">int</span> n = doc.get_Elements( 
&nbsp; &nbsp; <span class="blue">typeof</span>( <span class="teal">SketchPlane</span> ), a );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">SketchPlane</span> e <span class="blue">in</span> a )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( e.Plane.Normal.AlmostEqual( normal ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; result = e;
&nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> result;
}
</pre>

<p>Unfortunately, in the next step, I noticed that the model lines used in this sample are not allowed in an annotation  family either, so I just ran the command in a column family document instead.

<p>Here is the mainline of the CmdNewDimensionLabel external command Execute method which performs the following steps:

<ul>
<li>Find or create an appropriate sketch plane to work on, with a normal vector of (0,0,1).
<li>Create two vertical geometry lines.
<li>Create two vertical model curves from the lines.
<li>Collect the two references to the model lines in a reference array.
<li>Create a dimension entity between the two lines.
<li>Create a family parameter named "length" for the dimension label.
<li>Associate the dimension label with the "length" parameter.
</ul>

<pre class="code">
<span class="teal">Application</span> app = commandData.Application;
<span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
<span class="teal">SketchPlane</span> skplane = findSketchPlane( doc, <span class="teal">XYZ</span>.BasisZ );
&nbsp;
<span class="blue">if</span>( <span class="blue">null</span> == skplane )
{
&nbsp; <span class="teal">Plane</span> geometryPlane = app.Create.NewPlane(
&nbsp; &nbsp; <span class="teal">XYZ</span>.BasisZ, <span class="teal">XYZ</span>.Zero );
&nbsp;
&nbsp; skplane = doc.FamilyCreate.NewSketchPlane(
&nbsp; &nbsp; geometryPlane );
}
&nbsp;
<span class="blue">double</span> length = 1.23;
&nbsp;
<span class="teal">XYZ</span> start = <span class="teal">XYZ</span>.Zero;
<span class="teal">XYZ</span> end = app.Create.NewXYZ( 0, length, 0 );
&nbsp;
<span class="teal">Line</span> line = app.Create.NewLine( 
&nbsp; start, end, <span class="blue">true</span> );
&nbsp;
<span class="teal">ModelCurve</span> modelCurve
&nbsp; = doc.FamilyCreate.NewModelCurve(
&nbsp; &nbsp; line, skplane );
&nbsp;
<span class="teal">ReferenceArray</span> ra = <span class="blue">new</span> <span class="teal">ReferenceArray</span>();
&nbsp;
ra.Append( modelCurve.GeometryCurve.Reference );
&nbsp;
start = app.Create.NewXYZ( length, 0, 0 );
end = app.Create.NewXYZ( length, length, 0 );
&nbsp;
line = app.Create.NewLine( start, end, <span class="blue">true</span> );
&nbsp;
modelCurve = doc.FamilyCreate.NewModelCurve( 
&nbsp; line, skplane );
&nbsp;
ra.Append( modelCurve.GeometryCurve.Reference );
&nbsp;
start = app.Create.NewXYZ( 0, 0.2 * length, 0 );
end = app.Create.NewXYZ( length, 0.2 * length, 0 );
&nbsp;
line = app.Create.NewLine( start, end, <span class="blue">true</span> );
&nbsp;
<span class="teal">Dimension</span> dim 
&nbsp; = doc.FamilyCreate.NewLinearDimension( 
&nbsp; &nbsp; doc.ActiveView, line, ra );
&nbsp;
<span class="teal">FamilyParameter</span> familyParam
&nbsp; = doc.FamilyManager.AddParameter(
&nbsp; &nbsp; <span class="maroon">&quot;length&quot;</span>,
&nbsp; &nbsp; <span class="teal">BuiltInParameterGroup</span>.PG_IDENTITY_DATA,
&nbsp; &nbsp; <span class="teal">ParameterType</span>.Length, <span class="blue">false</span> );
&nbsp;
dim.Label = familyParam;
&nbsp;
<span class="blue">return</span> <span class="teal">CmdResult</span>.Succeeded;
</pre>

<p>Here is the dimension and the associated dimension label created in a new family document based on the metric column template:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a858265d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a858265d970b" alt="Dimension with dimension label" title="Dimension with dimension label" src="/assets/image_24579f.jpg" border="0"  /></a> <br />

</center>

<p>The length displayed in millimetres, and corresponds to the 1.23 feet specified by the variable 'length', since 1.23 * 12 * 25.4 = 374.904.

<p>As noted above, this code will not work unchanged in an annotation family, because you are prohibited from creating model curves there.
To run it in an annotation family, you will have to convert the code to work with 

<a href="http://thebuildingcoder.typepad.com/blog/2009/09/detail-lines.html">
detail curves</a>

instead.
You also cannot create a new sketch plane in an annotation family document, which was what prompted me to implement the findSketchPlane helper method.

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e1689788330128775a858e970c"><a href="http://thebuildingcoder.typepad.com/files/bc11061.zip">version 1.1.0.61</a></span>

of the complete Building Coder source code and Visual Studio solution including the new command.</p>

<p>Many thanks to Harry for this solution!
