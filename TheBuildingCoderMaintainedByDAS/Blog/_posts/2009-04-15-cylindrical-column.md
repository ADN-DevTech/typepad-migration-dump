---
layout: "post"
title: "Cylindrical Column"
date: "2009-04-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2010"
  - "Debugging"
  - "Element Relationships"
  - "Family"
  - "Geometry"
  - "RST"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/04/cylindrical-column.html "
typepad_basename: "cylindrical-column"
typepad_status: "Publish"
---

<P><strong>Question:</strong> How can I detect whether a column is round in Revit Structure 2010?</P>
<P>I implemented a solution that worked in Revit Structure 2009, but it cannot be used in the 2010 SDK, because the SolidForms and CurveLoop methods no longer exist:</P>

<pre class="code">
<span class="blue">bool</span> IsColumnRound(
&nbsp; <span class="teal">FamilySymbol</span> symbol )
{
&nbsp; <span class="teal">GenericFormSet</span> solid = symbol.Family.SolidForms;
&nbsp; <span class="teal">GenericFormSetIterator</span> i = solid.ForwardIterator();
&nbsp; i.MoveNext();
&nbsp; <span class="teal">Extrusion</span> extr = i.Current <span class="blue">as</span> <span class="teal">Extrusion</span>;
&nbsp; <span class="teal">CurveArray</span> cr = extr.Sketch.CurveLoop;
&nbsp; <span class="teal">CurveArrayIterator</span> i2 = cr.ForwardIterator();
&nbsp; i2.MoveNext();
&nbsp; <span class="teal">String</span> s = i2.Current.GetType().ToString();
&nbsp; <span class="blue">return</span> s.Contains( <span class="maroon">"Arc"</span> );
}
</pre>

<P><strong>Answer:</strong> I can think of several different approaches to analyse the cross section of a column family symbol:</P>
<ol>
<li>The Family.SolidForms property.</li>
<li>The family instance solid geometry.</li>
<li>The analytical model swept profile.</li>
</ol>
<P>The last of these, number 3, can be used in Revit Structure, but not in the other flavours of Revit.</P>
<P>The one you describe is the first approach, which can be used in Revit 2009, but not 2010. It makes use of the Revit 2009 API Family.SolidForms property:</P>
<P><em>SolidForms, a set of all solid forms that belong to a Family. Returns a read only set of all the solid forms that belong to a particular family.</em></P>
<P>In the Revit 2010 API help file, the section on the Family Creation API in the What's New chapter lists a host of new methods for working with families and states:</P>
<P><em>These methods can be used to examine the contents of the family in terms of its elements, parameters and types; as a result, the properties of Family which access the contents have been removed (Family.SolidForms, Family.VoidForms, Family.Components, Family.LoadedSymbols, Family.Others).</em></P>
<P>We could probably rewrite your original code using the new Revit 2010 family API to analyse the shapes defined in the family symbol.</P>
<P>I find the approach number 2 the simplest, because it operates directly on the visible family instance geometry, and this approach can be reused in a variety of situations. We have already discussed several such applications, in the posts on 
<ul>
<li><A href="http://thebuildingcoder.typepad.com/blog/2008/10/slab-boundary.html">Slab boundary</A>.</li>
<li><A href="http://thebuildingcoder.typepad.com/blog/2008/11/slab-side-faces.html">Slab side faces</A>.</li>
<li><A href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-elevation-profile.html">Wall elevation profile</A>.</li>
<li><A href="http://thebuildingcoder.typepad.com/blog/2008/12/2d-polygon-areas-and-outer-loop.html">2D polygon areas and outer loop</A>.</li>
<li><A href="http://thebuildingcoder.typepad.com/blog/2008/12/3d-polygon-areas.html">3D polygon areas</A>.</li>
</ul>
<P>Before doing anything further, it is always useful to analyse the structure and data of a Revit element using <A href="http://thebuildingcoder.typepad.com/blog/2009/02/rvtmgddbg.html">RvtMgdDbg</A>. It can be used to analyse the database contents and the element data, including its geometry. I do not see any immediate access to the family symbol geometry through the API, but looking at a column instance, the geometry is right there. For example, I loaded and inserted an instance of the round steel column family, 76.1x3.2CHS. Selecting the instance in the user interface and using Add-Ins &gt; RvtMgdDbg &gt; Options &gt; Snoop Current Selection &gt; Geometry &gt; Objects &gt; Instance &gt; Symbol Geometry &gt; Objects &gt; Faces brings me to a list of two planar and four cylindrical faces. There are four cylindrical ones for two reasons: first, there column is hollow, so there is an inner and an outer face. Secondly, the cross section is defined by two arcs instead of a circle, so each of the inner and outer faces is split into two. Here is a screen snapshot of exploring a round structural column in Revit Architecture 2010:</P><A style="DISPLAY: inline" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115701f66fe970b-popup"><img  class="at-xid-6a00e553e1689788330115701f66fe970b image-full " title="Round column faces" alt="Round column faces" src="/assets/image_3279cb.jpg" border=0></A> 
<P>I implemented a sample command CmdColumnRound which prompts you to select a column instance in the model and reports back to you whether it is cylindrical or not. To decide whether this is the case, it uses a very simple approach that occurred to me during the implementation: in all cases that I know of, if the solid of a column includes one single cylindrical face, the entire column is cylindrical. As long as this assumption holds, we can simplify the criteria for deciding whether the column is cylindrical to simply iterating over all its faces and deciding 'Yes' as soon as a cylindrical face is encountered. Obviously, if you have more complex columns in your model which include a cylindrical face in a non-cylindrical column, this assumption no longer holds.</P>
<P>First, here is a helper predicate to determine whether a given Revit element 'e' looks like it might be a column family instance:</P>

<pre class="code">
<span class="blue">bool</span> IsColumn( <span class="teal">RvtElement</span> e )
{
&nbsp; <span class="blue">return</span> e <span class="blue">is</span> <span class="teal">FamilyInstance</span>
&nbsp; &nbsp; &amp;&amp; <span class="blue">null</span> != e.Category
&nbsp; &nbsp; &amp;&amp; e.Category.Name.ToLower().Contains( <span class="maroon">"column"</span> );
}
</pre>

<P>Here is the code for the Execute method:</P>

<pre class="code">
<span class="teal">Application</span> app = commandData.Application;
<span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
<span class="teal">Selection</span> sel = doc.Selection;
<span class="teal">RvtElement</span> column = <span class="blue">null</span>;
&nbsp;
<span class="blue">if</span>( 1 == sel.Elements.Size )
{
&nbsp; <span class="blue">foreach</span>( <span class="teal">RvtElement</span> e <span class="blue">in</span> sel.Elements )
&nbsp; {
&nbsp; &nbsp; column = e;
&nbsp; }
&nbsp; <span class="blue">if</span>( !IsColumn( column ) )
&nbsp; {
&nbsp; &nbsp; column = <span class="blue">null</span>;
&nbsp; }
}
&nbsp;
<span class="blue">if</span>( <span class="blue">null</span> == column )
{
&nbsp; sel.Elements.Clear();
&nbsp; sel.StatusbarTip = <span class="maroon">"Please select a column"</span>;
&nbsp; <span class="blue">if</span>( sel.PickOne() )
&nbsp; {
&nbsp; &nbsp; <span class="teal">ElementSetIterator</span> i
&nbsp; &nbsp; &nbsp; = sel.Elements.ForwardIterator();
&nbsp; &nbsp; i.MoveNext();
&nbsp; &nbsp; column = i.Current <span class="blue">as</span> <span class="teal">RvtElement</span>;
&nbsp; }
&nbsp; <span class="blue">if</span>( !IsColumn( column ) )
&nbsp; {
&nbsp; &nbsp; message = <span class="maroon">"Please select a single column instance"</span>;
&nbsp; }
}
&nbsp;
<span class="blue">if</span>( <span class="blue">null</span> != column )
{
&nbsp; <span class="teal">Options</span> opt = app.Create.NewGeometryOptions();
&nbsp; <span class="teal">GeoElement</span> geo = column.get_Geometry( opt );
&nbsp; <span class="teal">GeometryObjectArray</span> objects = geo.Objects;
&nbsp; <span class="teal">GeoInstance</span> i = <span class="blue">null</span>;
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> objects )
&nbsp; {
&nbsp; &nbsp; i = obj <span class="blue">as</span> <span class="teal">GeoInstance</span>;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != i )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> == i )
&nbsp; {
&nbsp; &nbsp; message = <span class="maroon">"Unable to obtain geometry instance"</span>;
&nbsp; }
&nbsp; <span class="blue">else</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">bool</span> isCylindrical = <span class="blue">false</span>;
&nbsp; &nbsp; geo = i.SymbolGeometry;
&nbsp; &nbsp; objects = geo.Objects;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> objects )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solid = obj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> solid.Faces )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( face <span class="blue">is</span> <span class="teal">CylindricalFace</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; isCylindrical = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; message = <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; <span class="maroon">"Selected column instance is{0} cylindrical"</span>,
&nbsp; &nbsp; &nbsp; ( isCylindrical ? <span class="maroon">""</span> : <span class="maroon">" NOT"</span> ) );
&nbsp; }
}
<span class="blue">return</span> <span class="teal">CmdResult</span>.Failed;
</pre>


<P>The first part of the code demonstrates how to check whether a column has been preselected prior to running the command, and prompting the user to select one interactively otherwise.</P>
<P>Once the column instance has been determined, we obtain its geometry and extract the geometry instance from it. From that, we query the symbol geometry, search for its solid, query its faces, and decide that the column is round if a cylindrical face is encountered.</P>
<P>We make use of the message argument passed in to the Execute method to display the result of the analysis in the Revit user interface, and we return the command result Failed to ensure that the message is displayed and the Revit database is not marked as modified.</P>
<P>Here is <span class=at-xid-6a00e553e1689788330115701f63d6970b><A href="http://thebuildingcoder.typepad.com/files/bc10029.zip">version 1.0.0.29</A></span> of the complete Visual Studio solution with the new CmdColumnRound command.</P>
