---
layout: "post"
title: "Revit Library Shape Type Catalogue Parameters"
date: "2009-06-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Family"
  - "Geometry"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/06/revit-library-shape-type-catalogue-parameters.html "
typepad_basename: "revit-library-shape-type-catalogue-parameters"
typepad_status: "Publish"
---

<p>A query on the property sets used by Revit on IFC export of steel beams led to the unearthing of some useful documentation of the parameters and dimensions used to define some commonly used shapes in the Revit type library.</p>

<p><strong>Question:</strong>
I am analysing an IFC export file created by Revit and trying to retrieve the exact dimensions of steel beams in the model.
IFC has a standard specification for these which is publicly available.
Revit apparently uses some own property sets and assigns them to standard IFC beam objects.
These custom property sets are obviously not part of the IFC specification.
What are the exact specification of these parameters and their meanings?</p>

<p><strong>Answer:</strong>
The Revit beam parameters are indeed exported in a Revit specific property set.
No standard parameters have been defined for the different structural profiles.
Here is some documentation including pictures and parameter values for some structural profiles and other shapes.
These documents were created back in early 2006 illustrating the parameters of the most commonly used shapes in the Revit library.
Please note that a lot of new content files have been created since then:</p>

<ul>
<li>

<span class="at-xid-6a00e553e16897883301157114a026970b"><a href="http://thebuildingcoder.typepad.com/files/concrete_and_precast_concrete_shapes.zip">Concrete and Precast Concrete Shapes.zip</a></span>

<li>

<span class="at-xid-6a00e553e1689788330115701f7496970c"><a href="http://thebuildingcoder.typepad.com/files/steel_and_wood_shapes.zip">Steel and Wood Shapes.doc</a></span>

</ul>

<p>The former is a set of images, the latter a document describing the parameter format used in the part catalogue text files.</p>

<p>The images for the concrete and precast concrete shapes describes the dimensions and parameters used in each of the following families:</p>

<ul>
<li>Concrete-Rectangular Beam.rfa
<li>Precast-Double Tee.rfa
<li>Precast-Inverted Tee.rfa
<li>Precast-L Shaped Beam.rfa
<li>Precast-Rectangular Beam.rfa
<li>Precast-Single Tee.rfa
</ul>

<p>The images included are simply screen snapshots of two different views of the geometry and dimensioning and one of the Family Types dialogue listing the corresponding dimensions and parameters of a specific type.</p>

<p>The steel and wood shapes document is more comprehensive and explains the format of the text files used to specify type parameters:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115701f753d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115701f753d970c" alt="Type catalogue text file" title="Type catalogue text file" src="/assets/image_7137c7.jpg" border="0"  /></a>

<p>Here is a list of the families and corresponding text files described in this document:</p>

<ul>
<li>I-Shape(W Shape): M_W-Wide Flange.txt
<li>M-Shape (Miscellaneous Wide Flange): M_M-Miscellaneous Wide Flange.txt
<li>HP-Shape (HP-Bearing Pile): M_HP-Bearing Pile.txt
<li>C-Shape (Channel): M_HP-Bearing Pile.txt
<li>MC-Shape (Miscellaneous Channel): M_MC-Miscellaneous Channel.txt
<li>ST- Structural Tee: M_ST-Structural Tee.txt
<li>MT- Structural Tee: M_MT-Structural Tee.txt
<li>WT- Structural Tee: M_WT-Structural Tee.txt
<li>L-Angle: M_L-Angle.txt
<li>LL-Double Angle: M_L-Angle.txt
<li>Hollow Tube: M_HSS-Hollow Structural Section.txt
<li>Circular Hollow Tube: M_HSS-Round Structural Tubing.txt
</ul>

<ul>
<li>Pipe Column: M_Pipe-Column.rfa
<li>Open Web Steel Joist: M_K-Series Bar Joist-Angle Web.rfa, M_K-Series Bar Joist-Rod Web.rfa, M_DLH-Series Bar Joist.rfa, M_LH-Series Bar Joist.rfa
<li>Columns- Lumber: M_Dimension Lumber-Column.rfa
<li>Columns- Timber: M_Timber-Column.rfa
<li>Beam- Lumber: M_Dimension Lumber.rfa
<li>Plywood Web Joist: M_Plywood Web Joist.rfa
<li>Beam- Timber: M_Timber.rfa
<li>Open Web Wood Joist: M_Wood Open Web Joist.rfa
</ul>

<p>Most of the information included describes the parameters by showing their association with dimensioning on the model geometry, for instance like this for the miscellaneous wide flange:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115701f75c0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115701f75c0970c" alt="Miscellaneous wide flange dimensions" title="Miscellaneous wide flange dimensions" src="/assets/image_0cc16a.jpg" border="0"  /></a>
