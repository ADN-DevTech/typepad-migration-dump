---
layout: "post"
title: "Creating a Sloped Floor"
date: "2014-03-28 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Git"
  - "SDK Samples"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html "
typepad_basename: "creating-a-sloped-floor"
typepad_status: "Publish"
---

<p>Today, let's look at an interesting and overdue topic raised by the query on

<a href="http://forums.autodesk.com/t5/Revit-API/Can-t-create-sloped-floors/m-p/4895322">
creating a sloped floor</a> in

the Revit API discussion forum, addressed with help from Jaap van der Weide and Joe Ye.</p


<p>Before getting to that, let me quickly mention some other happy and important news: </p>


<a name="0"></a>

<h4>Revit 2015 is Coming Soon</h4>

<p>The 

<a href="http://inthefold.autodesk.com/in_the_fold/2014/03/autodesk-unveils-2015-suites-for-building-and-civil-infrastructure-industries.html">
Autodesk 2015 software portfolio</a> for

the building and civil infrastructure industries has now been officially announced.</p>

<p>Here is a direct link to the

<a href="http://inthefold.autodesk.com/files/building-design-suite-2015-backgrounder.pdf">
Autodesk Building Design Suite 2015 backgrounder</a> providing

a full rundown of the Autodesk Building Design Suite 2015 enhancements.<p>
  
<p>I am really looking forward to all the exciting good new stuff coming along, especially in the Revit API!</p>



<a name="1"></a>

<h4>Creating a Sloped Floor</h4>

<p><strong>Question:</strong> Is it possible to programmatically create a sloped floor?</p>


<p><strong>Answer:</strong> The answer grew and grew...
Here is an overview:</p>

<ul>
<li><a href="#2">Creating a sloped floor through the user interface</a></li>
<li><a href="#3">Modifying floor slope programmatically &ndash; or not</a></li>
<li><a href="#4">Creating a sloped floor programmatically: NewFloor</a></li>
<li><a href="#5">Creating a sloped floor programmatically: NewSlab</a></li>
<li><a href="#6">Download</a></li>
</ul>

<a name="2"></a>

<h4>Creating a Sloped Floor through the User Interface</h4>

<p>As always, before diving into the Revit API possibilities, it is worthwhile looking at the functionality provided by the user interface.</p>

<p>A sloped floor can be created in three different ways in the UI:</p>

<ol>
<li>Slab shape editor</li>
<li>Slope arrow</li>
<li>Slope defining boundary edges</li>
</ol>

<p>You are presumably asking about creating a floor with a uniform slope.</p>

<p>You can achieve that by editing the sketch of the floor and making one of the lines 'slope defining'.</p>

<p>This generates a floor with a single slope and with the correct thickness.</p>

<p>Another way, providing more control over the direction of the slope, is to sketch a 'slope arrow'.</p>

<p>Yet another method is the 'shape edit' command that allows you to create facets in the floor, each with their own slope.</p>

<p>Note that these three methods are mutually exclusive.</p>

<p>Here are snapshots of each of these three pieces of functionality:</p>

<p>1. Shape editor tool:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fce0e4da970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fce0e4da970b img-responsive" alt="Make a boundary slope defining" title="Make a boundary slope defining" src="/assets/image_df39d9.jpg" border="0" /></a><br />

</center>

<p>2. Using a slope arrow:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73d9bbd2a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73d9bbd2a970d img-responsive" alt="Use a slope arrow" title="Use a slope arrow" src="/assets/image_eb2205.jpg" border="0" /></a><br />

</center>

<p>3. Making a boundary edge slope defining:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511908f8c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511908f8c970c image-full img-responsive" alt="Make a boundary slope defining" title="Make a boundary slope defining" src="/assets/image_c5e5dd.jpg" border="0" /></a><br />

</center>

<p>Looking at the code provided in the

<a href="http://forums.autodesk.com/t5/Revit-API/Can-t-create-sloped-floors/m-p/4895322">
original query</a>,

I suspect that one problem is that the lines are not in a horizontal plane.</p>

<p>No matter how you slope the floor, the fundamental sketch of its boundary is always based on a level, and hence horizontal.</p>



<a name="3"></a>

<h4>Modifying Floor Slope Programmatically &ndash; or Not</h4>

<p>The shape edit functionality is programmatically accessible through the SlabShapeEditor class.
An instance of that class is provided by the property with the same name on the Floor and RoofBase classes.
The SDK sample SlabShapeEditing shows an example of using it.</p>

<p>Unfortunately, the other slope editing methods seem not to be accessible to programmatically modify the slope of an existing floor, because the built-in parameter CURVE_IS_SLOPE_DEFINING is read-only.</p>

<p>Slope defining edges can be used successfully to define the shape of a footprint roof, via the FootPrintRoof.SlopeAngle property.
It takes an edge and an angle as arguments, and sets a slope for the specified edge.</p>

<p>We set a 'slope defining' property on the roof of the simple little house created by the command Lab2_0_CreateLittleHouse of the ADN Revit API training material

<a href="http://thebuildingcoder.typepad.com/blog/2013/10/revit-2013-api-developer-guide-pdf.html#3">
Xtra labs</a>,

mentioned occasionally in the past:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/04/manual-regeneration-mode-danger.html">Manual regeneration option danger</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/08/slope-is-slope-not-radians.html">Slope is slope, not radians</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/08/validate-roof-type-and-view-obj-on-android.html">Validate roof type</a></li>
</ul>

<p>Here is an external command testing this for a floor slab.
It attempts to change the floor slope using the built-in parameters CURVE_IS_SLOPE_DEFINING and ROOF_SLOPE.
No cigar, I'm afraid:</p>

<pre class="code">
&nbsp; <span class="teal">UIApplication</span> uiapp = revit.Application;
&nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp; <span class="teal">Selection</span> sel = uidoc.Selection;
&nbsp;
&nbsp; <span class="teal">Reference</span> ref1 = sel.PickObject(
&nbsp; &nbsp; <span class="teal">ObjectType</span>.Element, <span class="maroon">&quot;Please pick a floor.&quot;</span> );
&nbsp;
&nbsp; <span class="teal">Floor</span> f = doc.GetElement( ref1 ) <span class="blue">as</span> <span class="teal">Floor</span>;
&nbsp;
&nbsp; <span class="blue">if</span>( f == <span class="blue">null</span> )
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp;
&nbsp; <span class="green">// Retrieve floor edge model line elements.</span>
&nbsp;
&nbsp; <span class="teal">ICollection</span>&lt;<span class="teal">ElementId</span>&gt; deleted_ids;
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tx = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; {
&nbsp; &nbsp; tx.Start( <span class="maroon">&quot;Temporarily Delete Floor&quot;</span> );
&nbsp;
&nbsp; &nbsp; deleted_ids = doc.Delete( f.Id );
&nbsp;
&nbsp; &nbsp; tx.RollBack();
&nbsp; }
&nbsp;
&nbsp; <span class="green">// Grab the first floor edge model line.</span>
&nbsp;
&nbsp; <span class="teal">ModelLine</span> ml = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> id <span class="blue">in</span> deleted_ids )
&nbsp; {
&nbsp; &nbsp; ml = doc.GetElement( id ) <span class="blue">as</span> <span class="teal">ModelLine</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != ml )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != ml )
&nbsp; {
&nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tx = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; tx.Start( <span class="maroon">&quot;Change Slope Angle&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// This parameter is read only. Therefore,</span>
&nbsp; &nbsp; &nbsp; <span class="green">// the change does not work and we cannot </span>
&nbsp; &nbsp; &nbsp; <span class="green">// change the floor slope angle after the </span>
&nbsp; &nbsp; &nbsp; <span class="green">// floor is created.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; ml.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.CURVE_IS_SLOPE_DEFINING )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Set( 1 );
&nbsp;
&nbsp; &nbsp; &nbsp; ml.get_Parameter(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.ROOF_SLOPE )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Set( 1.2 );
&nbsp;
&nbsp; &nbsp; &nbsp; tx.Commit();
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
</pre>


<a name="4"></a>

<h4>Creating a Sloped Floor Programmatically: NewFloor</h4>

<p>Looking at the API slab creation functionality, we have two methods at our disposal, NewFloor and

<a href="#5">NewSlab</a>.</p>

<p>The NewFloor methods provides three overloads taking the following lists of arguments:</p>

<ul>
<li>CurveArray, Boolean: Creates a floor within the project with the given horizontal profile using the default floor style.</li>
<li>CurveArray, FloorType, Level, Boolean: Creates a floor within the project with the given horizontal profile and floor style on the specified level.</li>
<li>CurveArray, FloorType, Level, Boolean, XYZ: Creates a floor within the project with the given horizontal profile and floor style on the specified level with the specified normal vector.</li>
</ul>

<p>The third overload includes a normal vector.
Optimistically, one might hope that it would enable the creation of a sloped floor.
Unfortunately, that is not the case.
It is not used to specify a slope vector, but to define the which side of the floor is considered upper and lower.
It can only take one of the two values (0,0,1) or (0,0,-1).</p>

<p>A sample using the NewFloor method to create a normal horizontal floor is provided in the discussion forum thread on

<a href="http://forums.autodesk.com/t5/Revit-API/Can-t-create-face/m-p/4416823#M4593">
can't create face</a>.</p>


<a name="5"></a>

<h4>Creating a Sloped Floor Programmatically: NewSlab</h4>

<p>The NewSlab method provides only one overload taking the following arguments:</p>

<ul>
<li>CurveArray profile</li>
<li>Level level</li>
<li>Line slopedArrow</li>
<li>double slope</li>
<li>bool isStructural</li>
</ul>

<p>A uniformly sloped slab can be created programmatically by setting the slope argument to the NewSlab method, as mentioned in the discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/creating-a-nonrectangular-slab.html">
creating a non-rectangular slab</a>.</p>

<p>Here is the entire code of an external command Execute method showing an example of creating a sloped floor using the NewSlab method:</p>

<pre class="code">
&nbsp; <span class="teal">UIApplication</span> uiapp = revit.Application;
&nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tx = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; {
&nbsp; &nbsp; tx.Start( <span class="maroon">&quot;Create Sloped Slab&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">double</span> width = 19.685039400;
&nbsp; &nbsp; <span class="blue">double</span> length = 59.055118200;
&nbsp; &nbsp; <span class="blue">double</span> height = 9.84251968503937;
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span>[] pts = <span class="blue">new</span> <span class="teal">XYZ</span>[] {
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 0.0, 0.0, height ),
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( width, 0.0, height ),
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( width, length, height ),
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 0, length, height )
&nbsp; &nbsp; };
&nbsp;
&nbsp; &nbsp; <span class="teal">CurveArray</span> profile
&nbsp; &nbsp; &nbsp; = uiapp.Application.Create.NewCurveArray();
&nbsp;
&nbsp; &nbsp; <span class="teal">Line</span> line = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> n = pts.GetLength( 0 );
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> q = pts[n - 1];
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">XYZ</span> p <span class="blue">in</span> pts )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; line = <span class="teal">Line</span>.CreateBound( q, p );
&nbsp; &nbsp; &nbsp; profile.Append( line );
&nbsp; &nbsp; &nbsp; q = p;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Level</span> level
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Level</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .Where&lt;<span class="teal">Element</span>&gt;(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; e =&gt; e.Name.Equals( <span class="maroon">&quot;CreateSlopedSlab&quot;</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .FirstOrDefault&lt;<span class="teal">Element</span>&gt;() <span class="blue">as</span> <span class="teal">Level</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == level )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; level = doc.Create.NewLevel( height );
&nbsp; &nbsp; &nbsp; level.Name = <span class="maroon">&quot;Sloped Slab&quot;</span>;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Floor</span> floor = doc.Create.NewSlab(
&nbsp; &nbsp; &nbsp; profile, level, line, 0.5, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; tx.Commit();
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
</pre>

<p>This command creates the following slab with a slope of 0.5.
Note that in this context,

<a href="http://thebuildingcoder.typepad.com/blog/2010/08/slope-is-slope-not-radians.html">
slope really does mean slope</a>,

not angle.</p>

<p>The generated slab is located on its own new level named "Sloped Slab".
Note the nice and succinct filtered element collector query used to check whether it already exists, aided and abetted by a LINQ Where clause.
If not found, it is created.</p>

<p>Due to the arrangement of the levels, after executing the command in a new empty default architectural model, the slab is not visible on Level 1.
It is partially visible and partially cut off in Level 2:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73d9bbd54970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73d9bbd54970d img-responsive" style="width: 203px; " alt="Sloped floor cut off on Level 2" title="Sloped floor cut off on Level 2" src="/assets/image_ce2655.jpg" /></a><br />

</center>

<p>It is displayed completely in the Site view:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511908fbe970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511908fbe970c img-responsive" style="width: 231px; " alt="Sloped floor site view" title="Sloped floor site view" src="/assets/image_2a0431.jpg" /></a><br />

</center>

<p>The elevation view shows the different levels and the slab slope of 0.5:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fce0e538970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fce0e538970b img-responsive" style="width: 374px; " alt="Sloped floor slope in elevation view" title="Sloped floor slope in elevation view" src="/assets/image_3aa13c.jpg" /></a><br />

</center>

<p>As you see, just like the man said: the floor definition profile is horizontal, and the slope is applied to that afterwards.</p>



<a name="6"></a>

<h4>Download</h4>

<p>In the end, I implemented a new external command CmdCreateSlopedSlab in The Building Coder samples to host this sample code.</p>

<p>You can grab it from

<a href="https://github.com/jeremytammik/the_building_coder_samples">
The Building Coder samples GitHub repository</a>.

<p>The version discussed above is

<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2014.0.108.0">
release 2014.0.108.0</a>.</p>



<a name="7"></a>

<h4>Good Answers</h4>

<p>Before wrapping up for the week, let me share this neat joke I just received from my brother Marcus, about a student who obtained 0% on an exam:</p>

<p>Why not 100%?</p>

<p>Q1. In which battle did Napoleon die?
<br/>&ndash; A. In his last battle.</p>

<p>Q2. Where was the Declaration of Independence signed?
<br/>&ndash; A. At the bottom of the  page.</p>

<p>Q3. River Ravi flows in which state?
<br/>&ndash; A. Liquid.</p>

<p>Q4. What is the main reason for divorce?
<br/>&ndash; A. Marriage.</p>

<p>Q5. What is the main reason for failure?
<br/>&ndash; A. Exams.</p>

<p>Q6. What can you never eat for breakfast?
<br/>&ndash; A. Lunch & dinner.</p>

<p>Q7. What looks like half an apple?
<br/>&ndash; A. The other half.</p>

<p>Q8. If you throw a red stone into the blue sea what it will become?
<br/>&ndash; A. It will become wet.</p>

<p>Q9. How can a man go eight days without sleeping ?
<br/>&ndash; A. He can sleep at night.</p>

<p>Q10. How can you lift an elephant with one hand?
<br/>&ndash; A. You will never find an elephant that has only one hand...</p>

<p>Q11. If you had three apples and four oranges in one hand and four apples and three oranges in the other, what would you have?
<br/>&ndash; A. Very large hands.</p>

<p>Q12. If it took eight men ten hours to build a  wall, how long would it take four men to build it?
<br/>&ndash; A. No time at all, the wall is already  built.</p>

<p>Q13. How can you drop a raw egg onto a concrete floor without cracking it?
<br/>&ndash; A. Any way you like, because concrete floors are very hard to crack.</p>
