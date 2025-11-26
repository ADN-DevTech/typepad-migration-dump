---
layout: "post"
title: "Family Instance Missing Level Property"
date: "2011-01-26 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
  - "Family"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html "
typepad_basename: "family-instance-missing-level-property"
typepad_status: "Publish"
---

<p>Here is another contribution by Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a>.

He says:

<p>"And now for something completely different" ... after all the UI Automation topics:

<p>I often need to collect all items on a specific level, for instance window family instances, and use a method like the following to do so:

<pre class="code">
<span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; GetWindowsByLevel( 
&nbsp; <span class="teal">Document</span> doc, 
&nbsp; <span class="teal">Level</span> level )
{
&nbsp; <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt; elementList = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Element</span>&gt;();
&nbsp;
&nbsp; <span class="teal">FilteredElementCollector</span> collector 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfCategory( <span class="teal">BuiltInCategory</span>.OST_Windows );
&nbsp;
&nbsp; <span class="teal">ParameterValueProvider</span> provider 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ParameterValueProvider</span>( 
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">ElementId</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.FAMILY_LEVEL_PARAM ) );
&nbsp;
&nbsp; <span class="teal">FilterNumericRuleEvaluator</span> evaluator 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilterNumericEquals</span>();
&nbsp;
&nbsp; <span class="teal">ElementId</span> idRuleValue = level.Id;
&nbsp;
&nbsp; <span class="teal">FilterElementIdRule</span> rule 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilterElementIdRule</span>( 
&nbsp; &nbsp; &nbsp; provider, evaluator, idRuleValue );
&nbsp;
&nbsp; <span class="teal">ElementParameterFilter</span> filter 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ElementParameterFilter</span>( rule );
&nbsp;
&nbsp; elementList.AddRange( 
&nbsp; &nbsp; collector.WherePasses( filter ).ToElements() );
&nbsp;
&nbsp; <span class="blue">return</span> elementList;
}
</pre>

<p>Strangely enough, it may occur that some elements are not found by this method, because their level property has not been correctly set.

<p>Here are the element properties of a window with its level property (Ebene in German) properly set:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e1fad280970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e1fad280970b" alt="Window with Level property properly set" title="Window with Level property properly set" src="/assets/image_f8fda7.jpg" border="0" /></a> <br />

</center>
 
<p>Other window instances, however, may be lacking this property:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c803f9ef970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c803f9ef970c" alt="Window with Level property missing" title="Window with Level property missing" src="/assets/image_a48b06.jpg" border="0" /></a> <br />

</center>

<p>If we take a look at the second window instance parameters using the RevitLookup 'snoop built-in enums' functionality, we see that it has no FAMILY_LEVEL_PARAM parameter:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e1fad546970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e1fad546970b image-full" alt="Window lacking FAMILY_LEVEL_PARAM parameter" title="Window lacking FAMILY_LEVEL_PARAM parameter" src="/assets/image_778ede.jpg" border="0" /></a> <br />

</center>

<p>This window instance also has a null value for its Level <strong>property</strong>:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e1fad647970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e1fad647970b image-full" alt="Window has null Level property" title="Window has null Level property" src="/assets/image_db524b.jpg" border="0" /></a> <br />

</center>

<p>Sometimes, you can set a parameter to affect a property.
Similarly, you can move or rotate an element to change its location point or curve.

<p>Sometimes a property and the parameter that should relate to this property are inconsistent with each other, but that's another point.

<p>In the element property page, you cannot set the property, because it is not displayed there at all.

<p>Therefore, this is one of the rare occasions where you can achieve something via the API that cannot be done through the user interface. 

<p>In this case, you can use this little workaround:

<ul>
<li>Retrieve all the levels, e.g. sorted by elevation or by name.
<li>Display their names in a dialog and provide an opportunity to the user to select the desired one:
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c803fd1e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c803fd1e970c" alt="Level selector" title="Level selector" src="/assets/image_f9b078.jpg" border="0" /></a> <br />

</center>

<ul>
<li>Set the built-in parameter value of the family instance lacking the level property to the selected level element id:
</ul>

<pre class="code">
&nbsp; windowWithoutLevelParam.get_Parameter( 
&nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.FAMILY_LEVEL_PARAM )
&nbsp; &nbsp; .Set( levels[levelDialogSelectedIndex].Id );
</pre>

<p>Once assigned programmatically, the level property also becomes visible to the user:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c803fe4b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c803fe4b970c" alt="Window Level property displayed" title="Window Level property displayed" src="/assets/image_ac4a46.jpg" border="0" /></a> <br />

</center>

<p>After this adjustment, the GetWindowsByLevel method returns the results it is designed for as expected.

<p>Many thanks to Rudolf for this detailed analysis and workaround!
