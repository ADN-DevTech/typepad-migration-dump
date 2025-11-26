---
layout: "post"
title: "Detach Beam from Plane"
date: "2014-03-13 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Element Relationships"
  - "Parameters"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/detach-beam-from-plane.html "
typepad_basename: "detach-beam-from-plane"
typepad_status: "Publish"
---

<p>Here is a long-standing question raised once again now by Miroslav Schonauer of Autodesk Consulting and solved with help from Sasha Crotty of the Revit development team:</p>

<p><strong>Question:</strong> Is there a way to programmatically replicate the 'Detach from Plane' functionality accessible in the user interface through the beam context menu?</p>

<p>I can right click on a structural beam and select 'Detach from Plane'.
Revit removes the parameter 'Work plane', i.e. BuiltInParameter.SKETCH_PLANE_PARAM, and enables the 'Reference Level' parameter, which is read-only otherwise.
I would like to replicate this behaviour using the API.</p>

<p>I tried to delete the Work plane parameter but that did not help.
Even the FamilyInstance.Host for a beam is returned as read-only.</p>

<p>Is there a way to achieve this using the API?</p>


<p><strong>Answer:</strong> Changing the end elevation of the beam using the end elevation parameters will force it to go out of plane.
That should also detach it from the reference plane.</p>


<p><strong>Response:</strong> I need the beam to remain in the same physical position, so should I do it like this, then?

<ul>
<li>First transaction: move the end elevations +dv in plane normal direction.</li>
<li>Second transaction: move it back by –dv.</li>
</ul>

<p><strong>Answer:</strong> I think you may be able to get away without two transactions.
Move the beam and then move it back in the same transaction.</p>

<p>If that doesn't work, you could use a TransactionGroup and assimilate the two transactions into one.</p>


<p><strong>Response:</strong> Thanks all a lot.
It works like a charm, all from a single transaction!
Here is some sample code:</p>

<pre class="code">
&nbsp; <span class="blue">class</span> <span class="teal">SelectionFilterBeam</span> : <span class="teal">ISelectionFilter</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">bool</span> AllowElement( <span class="teal">Element</span> e )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !(e <span class="blue">is</span> <span class="teal">FamilyInstance</span>) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( e.Category.Id.IntegerValue != (<span class="blue">int</span>)
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_StructuralFraming )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// In theory could still be a Brace, but </span>
&nbsp; &nbsp; &nbsp; <span class="green">// Structural Usage is sometimes NOT set and </span>
&nbsp; &nbsp; &nbsp; <span class="green">// cannot be relied upon!</span>
&nbsp; &nbsp; &nbsp; <span class="green">// So, good enough as &quot;Beam&quot; if here.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">bool</span> AllowReference( <span class="teal">Reference</span> r, <span class="teal">XYZ</span> p )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; [<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Automatic )]
&nbsp; <span class="blue">public</span> <span class="blue">class</span> <span class="teal">CmdDetachBeamFromPlane</span> : <span class="teal">IExternalCommand</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// Pick a beam</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">SelectionFilterBeam</span> selFilterBeam
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">SelectionFilterBeam</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Reference</span> r = uidoc.Selection.PickObject(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ObjectType</span>.Element, selFilterBeam,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Select a Beam to 'Detach From Plane'&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">FamilyInstance</span> beam = doc.GetElement( r )
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// Check if it has 'Work Plane' to detach</span>
&nbsp; &nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; &nbsp; <span class="green">// One would expect that it is simplest to </span>
&nbsp; &nbsp; &nbsp; <span class="green">// check .Host as commented below, BUT there </span>
&nbsp; &nbsp; &nbsp; <span class="green">// are some strange situations where Host IS </span>
&nbsp; &nbsp; &nbsp; <span class="green">// null and Revit STILL displays (as read-only):</span>
&nbsp; &nbsp; &nbsp; <span class="green">// &quot;Work Plane = &lt;not associated&gt;&quot; !?</span>
&nbsp; &nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; &nbsp; <span class="green">//if (null == beam.Host)</span>
&nbsp; &nbsp; &nbsp; <span class="green">//{</span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; MessageBox.Show(&quot;Selected Family Instance of 'Structural Framing' Category has NO 'Work Plane'!&quot;);</span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; return Result.Cancelled;</span>
&nbsp; &nbsp; &nbsp; <span class="green">//}</span>
&nbsp; &nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; &nbsp; <span class="green">// So, must check if that read-only SKETCH_PLANE_PARAM param exists:</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.SKETCH_PLANE_PARAM ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; MessageBox.Show( <span class="maroon">&quot;Selected Family Instance &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;of 'Structural Framing' Category has NO &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;'Work Plane'!&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Cancelled;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// In theory, the plane could be non-horizontal </span>
&nbsp; &nbsp; &nbsp; <span class="green">// but in 99% should be and in 99.99% for beams </span>
&nbsp; &nbsp; &nbsp; <span class="green">// it would NOT be vertical which is the only </span>
&nbsp; &nbsp; &nbsp; <span class="green">// case that would not work using the following </span>
&nbsp; &nbsp; &nbsp; <span class="green">// (adjusting the elevation):</span>
&nbsp; &nbsp; &nbsp; <span class="green">// As .Host is RO property, the workaround is </span>
&nbsp; &nbsp; &nbsp; <span class="green">// to move the END ELEVATIONs outside the plane</span>
&nbsp; &nbsp; &nbsp; <span class="green">// which will internally &quot;detach&quot; it in Revit, </span>
&nbsp; &nbsp; &nbsp; <span class="green">// then simply move back!</span>
&nbsp; &nbsp; &nbsp; <span class="green">// Note that Moving the element would not work </span>
&nbsp; &nbsp; &nbsp; <span class="green">// as it is constrained to the plane.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">double</span> elevOldSta = beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END0_ELEVATION )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .AsDouble();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">double</span> elevOldEnd = beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END1_ELEVATION )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .AsDouble();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">double</span> elevTmpSta = elevOldSta + 1.0;
&nbsp; &nbsp; &nbsp; <span class="blue">double</span> elevTmpEnd = elevOldEnd + 1.0;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// This will &quot;detach from plane&quot;...</span>
&nbsp;
&nbsp; &nbsp; &nbsp; beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END0_ELEVATION )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Set( elevTmpSta );
&nbsp;
&nbsp; &nbsp; &nbsp; beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END1_ELEVATION )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Set( elevTmpEnd );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// ...and this move back to the </span>
&nbsp; &nbsp; &nbsp; <span class="green">// same original position</span>
&nbsp;
&nbsp; &nbsp; &nbsp; beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END0_ELEVATION )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Set( elevOldSta );
&nbsp;
&nbsp; &nbsp; &nbsp; beam.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.STRUCTURAL_BEAM_END1_ELEVATION )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Set( elevOldEnd );
&nbsp;
&nbsp; &nbsp; &nbsp; MessageBox.Show( <span class="maroon">&quot;Successfully removed the &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;'Work Plane' constraint&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>Please note the numerous valuable hints included in the code comments.</p>

<p>Also note that a slight performance improvement might be achievable by adjusting just one end of the beam instead of both.</p>

<p>Finally, note that the code could be made more readable by defining shorthand variables for the lengthy built-in parameter enumeration values :-)</p>

<p>Many thanks to Miro and Sascha for this useful solution!</p>
