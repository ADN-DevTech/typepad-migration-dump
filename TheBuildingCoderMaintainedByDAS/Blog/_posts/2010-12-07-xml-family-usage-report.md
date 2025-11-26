---
layout: "post"
title: "XML Family Usage Report"
date: "2010-12-07 02:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "AU 2010"
  - "Family"
  - "Filters"
  - "Travel"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/12/xml-family-usage-report.html "
typepad_basename: "xml-family-usage-report"
typepad_status: "Publish"
---

<p>I spent a pleasant weekend here in Tel Aviv, mostly preparing for the upcoming developer conferences.
I did take time for a walk along the beach to Jaffa and an early dinner in a neat little restaurant in the port, 

<a href="http://www.container.org.il">Container</a>.

Here it is, behind the fishing boats, seen from the other side of the port, with old Jaffa in the background:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e070e689970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e070e689970b image-full" alt="Restaurant Container in Jaffa" title="Restaurant Container in Jaffa" src="/assets/image_f50e73.jpg" border="0" /></a> <br />

</center>

<p>I also found out that you can get really good coffee in Israel, and wonderful ice cream, for instance in 'tita', and 
in 'Anita, la Mamma del Gelato'.
Anita apparently won prizes for the best ice cream here three years running, which is quite something in view of the competition.
I really like the atmosphere of this city!

<p>Today we move from one extreme to the next, from plus 25 degrees temperature here in Tel Aviv to a similar negative temperature in Moscow...


<p>Meanwhile, here is another little item from Kevin Vandecar's 


<a href="http://thebuildingcoder.typepad.com/blog/2010/10/filtered-element-collectors.html">
filtering and optimisation</a> class

at the AEC DevCamp in June, which I also used in my  

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/autodesk-university-2010-class-materials.html">
AU class CP234-2</a> on

the same topic.


<p>As an example of using Revit filtering to retrieve family symbols and instances in conjunction with effective use of LINQ and a .NET framework XML library, Kevin presents an external command which creates a pretty comprehensive family usage report in very few lines of code.

<p>It uses the ElementClassFilter, FamilySymbolFilter, and FamilyInstanceFilter classes to gather information about the families in a project.
For each family, it iterates over each symbol within the family, and finally retrieves all instances of each symbol. 
These filters have not previously been discussed here.

<p>This produces an abundance of information in a typical project, so it processes the results using the LINQ to XML functional or "DOM free" approach to produce a family inventory of the model in a nice XML format.



<p>The report also includes the location of each family instance, and makes use of this helper method to return a string representing it.
The string consists of the endpoints of the given family instance location curve, if it has one, otherwise its location point, if it has one, or "&lt;none&gt;" if all else fails:



<pre class="code">
<span class="blue">static</span> <span class="blue">string</span> LocationString( <span class="teal">FamilyInstance</span> fi )
{
&nbsp; <span class="teal">LocationPoint</span> p = fi.Location <span class="blue">as</span> <span class="teal">LocationPoint</span>;
&nbsp; <span class="teal">LocationCurve</span> c = fi.Location <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp; <span class="blue">return</span> ( <span class="blue">null</span> == p
&nbsp; &nbsp; ? ( ( <span class="blue">null</span> == c
&nbsp; &nbsp; &nbsp; ? <span class="maroon">&quot;&lt;none&gt;&quot;</span>
&nbsp; &nbsp; &nbsp; : <span class="teal">Util</span>.PointString( c.Curve.get_EndPoint( 0 ) )
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; to &quot;</span> 
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="teal">Util</span>.PointString( c.Curve.get_EndPoint( 1 ) ) ) )
&nbsp; &nbsp; : <span class="teal">Util</span>.PointString( p.Point ) );
}
</pre>

<!--


<p>I tried to figure out a way to make my first use of the first of the  











<a href="http://thebuildingcoder.typepad.com/blog/2010/11/c-and-net-little-wonders.html">
C# and .NET little wonders</a> ,

the null coalescing operator '??', to rewrite this more concisely, but the best I can come up with is actually less concise and the use of the operator does not help at all:

<pre class="code">
<span class="blue">static</span> <span class="blue">string</span> LocationString( <span class="teal">FamilyInstance</span> fi )
{
&nbsp; <span class="blue">string</span> s = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="teal">LocationPoint</span> p = fi.Location <span class="blue">as</span> <span class="teal">LocationPoint</span>;
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != p )
&nbsp; {
&nbsp; &nbsp; s = <span class="teal">Util</span>.PointString( p.Point );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> == s )
&nbsp; {
&nbsp; &nbsp; <span class="teal">LocationCurve</span> lc = fi.Location <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Curve</span> c = lc.Curve;
&nbsp;
&nbsp; &nbsp; s = <span class="teal">Util</span>.PointString( c.get_EndPoint( 0 ) )
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; to &quot;</span> 
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="teal">Util</span>.PointString( c.get_EndPoint( 1 );
&nbsp; }
&nbsp; <span class="blue">return</span> s ?? <span class="maroon">&quot;&lt;none&gt;&quot;</span>;
}
</pre>

<p>Maybe the neatest thing to do would be to declare this method as an extension method to the Location class (another one of the little wonders), overloading the standard .NET ToString method:

-->

<p>Here is the external command implementation, performing the following steps:

<ul>
<li>Instantiate a stopwatch for benchmarking purposes.
<li>Create a top level "Family_Inventory" XML node.
<li>Retrieve all families in the document and iterate over them.
<li>For each family, create a family element node.
<li>Retrieve all symbols using a FamilySymbolFilter, and iterate over those.
<li>For each symbol, create a symbol node.
<li>Retrieve all symbol instances in the active view using a FamilyInstanceFilter.
<li>Create subnodes for all the instances.
<li>Create and display the XML document and the elapsed time.
</ul>

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> Execute( 
&nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; <span class="teal">Document</span> doc = uiapp.ActiveUIDocument.Document;
&nbsp;
&nbsp; <span class="teal">Stopwatch</span> sw = <span class="teal">Stopwatch</span>.StartNew();
&nbsp;
&nbsp; <span class="teal">XElement</span> xmlFamilyInstances = <span class="blue">new</span> <span class="teal">XElement</span>( 
&nbsp; &nbsp; <span class="maroon">&quot;Family_Inventory&quot;</span> );
&nbsp;
&nbsp; <span class="green">// retrieve all families. </span>
&nbsp; <span class="green">// use the ElementClassFilter shortcut </span>
&nbsp; <span class="green">// and filter all &quot;Family&quot; elements.</span>
&nbsp;
&nbsp; <span class="teal">FilteredElementCollector</span> families 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; families.OfClass( <span class="blue">typeof</span>( <span class="teal">Family</span> ) );
&nbsp;
&nbsp; <span class="blue">int</span> nFamily = 0;
&nbsp; <span class="blue">int</span> nSymbol = 0;
&nbsp; <span class="blue">int</span> nInstance= 0;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Family</span> family <span class="blue">in</span> families )
&nbsp; {
&nbsp; &nbsp; ++nFamily;
&nbsp;
&nbsp; &nbsp; <span class="green">// XML: Start by adding the Family element</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">XElement</span> temp = <span class="blue">new</span> <span class="teal">XElement</span>( 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;FamilyName&quot;</span>, family.Name );
&nbsp;
&nbsp; &nbsp; <span class="green">// use the FamilySymbolFilter for each Family</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">FamilySymbolFilter</span> filterFamSym 
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FamilySymbolFilter</span>( family.Id );
&nbsp;
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> famSymbols 
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; &nbsp; famSymbols.WherePasses( filterFamSym );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">FamilySymbol</span> famSymbol <span class="blue">in</span> famSymbols )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; ++nSymbol;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">FamilyInstanceFilter</span> filterFamilyInst 
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FamilyInstanceFilter</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, famSymbol.Id );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> collectorFamInstances 
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, doc.ActiveView.Id );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">FamilyInstance</span>&gt; famInstances 
&nbsp; &nbsp; &nbsp; &nbsp; = collectorFamInstances
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .WherePasses( filterFamilyInst )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OfType&lt;<span class="teal">FamilyInstance</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">int</span> nInstanceCount 
&nbsp; &nbsp; &nbsp; &nbsp; = famInstances.Count&lt;<span class="teal">FamilyInstance</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; nInstance += nInstanceCount;
&nbsp;
&nbsp; &nbsp; &nbsp; temp.Add( <span class="blue">new</span> <span class="teal">XElement</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;SymbolName&quot;</span>, 
&nbsp; &nbsp; &nbsp; &nbsp; famSymbol.Name,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">from</span> fi <span class="blue">in</span> famInstances
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">select</span> <span class="blue">new</span> <span class="teal">XElement</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Instance&quot;</span>, 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; fi.Id.ToString(),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XElement</span>( <span class="maroon">&quot;Type&quot;</span>, 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; fi.GetType().ToString() ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XElement</span>( <span class="maroon">&quot;Position&quot;</span>, 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; LocationString( fi ) ) ) ) );
&nbsp; &nbsp; }
&nbsp; &nbsp; xmlFamilyInstances.Add( temp );
&nbsp; }
&nbsp;
&nbsp; <span class="green">// Create the XML report document</span>
&nbsp;
&nbsp; <span class="teal">XDocument</span> xmldoc =
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XDocument</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XDeclaration</span>( <span class="maroon">&quot;1.0&quot;</span>, <span class="maroon">&quot;utf-8&quot;</span>, <span class="maroon">&quot;yes&quot;</span> ),
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XComment</span>( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Current Family Inventory of Revit project: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + doc.PathName ),
&nbsp; &nbsp; &nbsp; &nbsp; xmlFamilyInstances );
&nbsp;
&nbsp; <span class="blue">string</span> fileName = <span class="maroon">&quot;C:/FamilyInventory.xml&quot;</span>;
&nbsp; xmldoc.Save( fileName );
&nbsp;
&nbsp; <span class="teal">Util</span>.ShowElapsedTime( sw,
&nbsp; &nbsp; <span class="maroon">&quot;Linq Example 3 XML Report&quot;</span>,
&nbsp; &nbsp; <span class="blue">string</span>.Format( <span class="maroon">&quot;{0} families with {1} symbols and {2} instances&quot;</span>,
&nbsp; &nbsp; &nbsp; nFamily, nSymbol, nInstance ),
&nbsp; &nbsp; <span class="blue">string</span>.Empty );
&nbsp;
&nbsp; <span class="green">// We can use Internet Explorer or whatever </span>
&nbsp; <span class="green">// your favorite XML viewer is...</span>
&nbsp; <span class="teal">Process</span>.Start(
&nbsp; &nbsp; <span class="maroon">&quot;C:/Program Files/Internet Explorer/iexplore.exe&quot;</span>, 
&nbsp; &nbsp; fileName);
&nbsp;
&nbsp; <span class="green">// Here is one that is free and is a little more </span>
&nbsp; <span class="green">// robust than Internet Explorer. If interested, </span>
&nbsp; <span class="green">// download from here: </span>
&nbsp; <span class="green">// http://download.cnet.com/XML-Marker/3000-7241_4-10202365.html</span>
&nbsp; <span class="green">//Process.Start( @&quot;C:/Program Files (x86)/XML Marker/xmlmarker.exe&quot;, fileName );</span>
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
}
</pre>

<p>Running this in the ArchSample.rvt model produces the following elapsed time report:

<pre>
Linq Example 3 XML Report: 
729 milliseconds 57 families 
with 61 symbols and 327 instances
</pre>

<p>Looking at the XML file in the browser, here are the completely collapsed contents showing just the top level family inventory node:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c67a349b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c67a349b970c" alt="Family inventory top level node" title="Family inventory top level node" src="/assets/image_acf634.jpg" border="0" /></a> <br />

</center>

<p>Here are some opened family nodes, a few with no symbols defined, and the system panel family containing two symbols, one of which has no instances in the model:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e070e3a7970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e070e3a7970b" alt="Family inventory family nodes" title="Family inventory family nodes" src="/assets/image_52f5ef.jpg" border="0" /></a> <br />

</center>

<p>Finally, here are some expanded instance nodes for one of the rectangular mullion symbols:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c67a321c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c67a321c970c" alt="Family inventory instance nodes" title="Family inventory instance nodes" src="/assets/image_0ee207.jpg" border="0" /></a> <br />

</center>
