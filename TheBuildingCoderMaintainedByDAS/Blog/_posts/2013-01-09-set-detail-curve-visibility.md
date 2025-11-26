---
layout: "post"
title: "Set Detail Curve Visibility"
date: "2013-01-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Parameters"
  - "RST"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/01/set-detail-curve-visibility.html "
typepad_basename: "set-detail-curve-visibility"
typepad_status: "Publish"
---

<p>Mostly, the Revit API limits an add-in to pretty high-level operations, encapsulating and protecting the parametric BIM from reckless modifications.
For the nonce, here is a little bit of nitty-gritty bit manipulation anyway, in a utility method provided by Scott Conover:</p>

<p><strong>Question:</strong> Why is there no DetailCurve.SetVisibility method exposed, similar to ModelCurves.SetVisibility?

<p>The UI has this functionality, and I would find very good use for it in reinforcement detailing.

<p>I want to programmatically create a detail family representing a certain rebar shape.
The family instance should reflect the visibility states, single line in coarse, double line in fine.

<p><strong>Answer:</strong> The ModelCurve.SetVisibility method taking a FamilyElementVisibility argument is available for visibility of curve elements in families when the families are placed.
Similarly we have a SymbolicCurve.SetVisibility method.
Detail curves don't have the exact same functionality in the UI.
Generally, detail curves are visible only in a single view in which they are placed.
The access you describe is partially enabled for detail families, however.

<p>Here is a workaround to access this functionality:  the visibility settings for the curve are stored in the integer GEOM_VISIBILITY_PARAM built-in parameter as bit flags.
You can therefore request the desired display by setting the appropriate bits in that parameter value.

<p>The SetFamilyVisibility method presented below turns off the modes you do not want for visibility.
It can be called either from an external command or a macro.

<p>It makes use of a simple selection filter to restrict the user selection to detail curves:

<pre class="code">
<span class="blue">class</span> <span class="teal">DetailCurveSelectionFilter</span> : <span class="teal">ISelectionFilter</span>
{
&nbsp; <span class="blue">public</span> <span class="blue">bool</span> AllowElement( <span class="teal">Element</span> e )
&nbsp; {
&nbsp; &nbsp; <span class="teal">CurveElementFilter</span> filter
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">CurveElementFilter</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">CurveElementType</span>.DetailCurve );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> filter.PassesFilter( e );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">bool</span> AllowReference( <span class="teal">Reference</span> r, <span class="teal">XYZ</span> p )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp; }
}
</pre>

<p>Here is the method implementation itself:

<pre class="code">
<span class="blue">public</span> <span class="blue">void</span> SetFamilyVisibility( <span class="teal">UIDocument</span> uidoc )
{
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; <span class="teal">Reference</span> r = uidoc.Selection.PickObject(
&nbsp; &nbsp; <span class="teal">ObjectType</span>.Element,
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">DetailCurveSelectionFilter</span>(),
&nbsp; &nbsp; <span class="maroon">&quot;Select detail curve&quot;</span> );
&nbsp;
&nbsp; <span class="teal">Element</span> elem = doc.GetElement( r );
&nbsp;
&nbsp; <span class="teal">Parameter</span> visParam = elem.get_Parameter(
&nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.GEOM_VISIBILITY_PARAM );
&nbsp;
&nbsp; <span class="blue">int</span> vis = visParam.AsInteger();
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> t = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; {
&nbsp; &nbsp; t.Start( <span class="maroon">&quot;Set curve visibility&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="green">// Turn off the bit corresponding</span>
&nbsp; &nbsp; <span class="green">// to the unwanted modes</span>
&nbsp;
&nbsp; &nbsp; vis = vis &amp; ~( 1 &lt;&lt; 13 ); <span class="green">// Coarse</span>
&nbsp; &nbsp; <span class="green">//vis = vis &amp; ~(1 &lt;&lt; 14); // Medium</span>
&nbsp; &nbsp; <span class="green">//vis = vis &amp; ~(1 &lt;&lt; 15); // Fine</span>
&nbsp;
&nbsp; &nbsp; visParam.Set( vis );
&nbsp;
&nbsp; &nbsp; t.Commit();
&nbsp; }
}
</pre>

<p>Remember that all three modes cannot be turned off simultaneously &ndash; a posted error will result.

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee7216981970d"><a href="http://thebuildingcoder.typepad.com/files/setdetailcurvevisibility.zip">SetDetailCurveVisibility.zip</a></span> 
containing

the complete source code, Visual Studio solution and add-in manifest for an external command implementation of this.</p>

<p>Many thanks to Scott for this tricky hint!</p>
