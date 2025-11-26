---
layout: "post"
title: "Read Material Asset Parameter"
date: "2013-01-30 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Material"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/01/read-material-asset-parameter.html "
typepad_basename: "read-material-asset-parameter"
typepad_status: "Publish"
---

<p>Even though it is still January, the days are already getting a little bit lighter, the sun has broken through now and then, sets half an hour later, some birds are starting to tweet around, and one can imagine spring coming...

<p>Meanwhile, in the Revit API, accessing materials tends to be tricky, so here is one little sample  that hopefully helps clarify by demonstrating how to read a parameter value from a PropertySetElement attached to a physical material asset.


<p><strong>Question:</strong> I'd like to know if there's an API to access the parameter called "Tension parallel to grain" in a wood material structural asset.
The specific material I'm looking at is Softwood, Lumber and is present in a default new structural project.

<p>There is a built-in parameter called PHY_MATERIAL_PARAM_TENSION_PARALLEL but that doesn't work.
The RevitLookup tool does not list the parameter, even though it is shown in the Revit GUI.

<p>The "Tension Parallel to Grain" parameter contains a Strength value, which is a double.</p>

<p>To access this through the GUI:</p>

<ul>
<li>Start a new model in Revit
<li>Go to Manage &gt; Materials
<li>Scroll to a material that belongs to the wood category and contains a Structural Asset. In a new model, you can scroll to Softwood, Lumber.
<li>Select the Structural Asset (Physical Aspect).
<li>Expand the Strength section and you'll see the parameter.
</ul>

<p>The parameter is attached to the StructuralAsset element.</p>

<p>Here is the Softwood, Lumber material listed and selected in the material browser:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3663f8a2970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3663f8a2970b" alt="Material browser" title="Material browser" src="/assets/image_dfd585.jpg" border="0" /></a><br />

</center>

<p>Selecting it and navigating to its physical aspect shows the tension parallel to grain value:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3663f788970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3663f788970b" alt="Material editor" title="Material editor" src="/assets/image_410cf8.jpg" border="0" /></a><br />

</center>

<p>How can I access that data programmatically, please?</p>


<p><strong>Answer:</strong> You can access this parameter using the PropertySetElement.

<p>Assuming softwood is assigned to a selected element, the following code achieves this, including extracting the single selected element from the current document selection set:</p>

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> ReadMaterialParam( <span class="teal">UIDocument</span> uidoc )
{
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; <span class="teal">Element</span> e = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="teal">Selection</span> sel = uidoc.Selection;
&nbsp;
&nbsp; <span class="blue">if</span>( 1 == sel.Elements.Size )
&nbsp; {
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e2 <span class="blue">in</span> sel.Elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; e = e2;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> == e )
&nbsp; {
&nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;Error&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Please select one single element.&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; }
&nbsp;
&nbsp; <span class="teal">Parameter</span> paramMaterial = e.get_Parameter(
&nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_MATERIAL_PARAM );
&nbsp;
&nbsp; <span class="teal">Material</span> material = doc.GetElement(
&nbsp; &nbsp; paramMaterial.AsElementId() ) <span class="blue">as</span> <span class="teal">Material</span>;
&nbsp;
&nbsp; <span class="teal">PropertySetElement</span> property = doc.GetElement(
&nbsp; &nbsp; material.StructuralAssetId ) <span class="blue">as</span> <span class="teal">PropertySetElement</span>;
&nbsp;
&nbsp; <span class="teal">Parameter</span> paramTensionParallel
&nbsp; &nbsp; = property.get_Parameter( <span class="teal">BuiltInParameter</span>
&nbsp; &nbsp; &nbsp; .PHY_MATERIAL_PARAM_TENSION_PARALLEL );
&nbsp;
&nbsp; <span class="teal">TaskDialog</span>.Show(
&nbsp; &nbsp; <span class="maroon">&quot;PHY_MATERIAL_PARAM_TENSION_PARALLEL&quot;</span>,
&nbsp; &nbsp; paramTensionParallel.AsValueString() );
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
}
</pre>

<p>This value cannot be read directly from the StructuralAsset properties.</p>

<p>To test run it, you can</p>

<ul>
<li>Create a new structural model.</li>
<li>Insert a structural element, e.g. a beam, by loading the structural framing family and selecting the symbol "Dimension Lumber" 38x64.</li>
<li>Assign "Softwood Lumber" to it via Properties &gt; Structural Material &gt; ...</li>
</ul>

<p>Selecting the beam and launching the command calling the ReadMaterialParam method displays the following message on my system:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3663f62a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3663f62a970b" alt="Material asset parameter value" title="Material asset parameter value" src="/assets/image_90effa.jpg" border="0" /></a><br />

</center>
