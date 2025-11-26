---
layout: "post"
title: "Nested Family"
date: "2009-11-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2009"
  - "Element Creation"
  - "Family"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/11/nested-family.html "
typepad_basename: "nested-family"
typepad_status: "Publish"
---

<p>Here is another interesting question that came up on the creation of a nested family.

<p><strong>Question:</strong> Can you help me to create a nested family via the API?
I have a column family and a shelf family stored in RFA files, and I would like to implement a command which creates a new family with column and the shelf, and load it into a project.
How can this be achieved?

<p><strong>Answer:</strong> The solution to this task is quite easy.
I assume you have two existing families named column and shelf, and wish to create a third family hosting instances of these two.
The two families can be defined either in external RFA files or as in-memory documents.

<p>Inserting instances of each of these into a third new family document means that we are working in the family context instead of the project context.
However, the steps to load and insert the family instances are almost identical in both environments:

<ul>
<li>Load the family into the target document.
<li>Select the symbol to insert.
<li>Create a new family instance.
</ul>

<p>Here is a helper method InsertFamilySymbolFromRfa which implements these three steps:

<pre class="code">
<span class="teal">StructuralType</span> _non_rst = <span class="teal">StructuralType</span>.NonStructural;
&nbsp;
<span class="teal">FamilyInstance</span> InsertFamilySymbolFromRfa(
&nbsp; <span class="blue">string</span> filename,
&nbsp; <span class="teal">Document</span> doc )
{
&nbsp; <span class="teal">FamilyInstance</span> fi = <span class="blue">null</span>;
&nbsp; <span class="green">//</span>
&nbsp; <span class="green">// load family file:</span>
&nbsp; <span class="green">//</span>
&nbsp; <span class="teal">Family</span> f;
&nbsp; <span class="blue">if</span>( doc.LoadFamily( filename, <span class="blue">out</span> f ) )
&nbsp; {
&nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; <span class="green">// retrieve family symbol:</span>
&nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; <span class="teal">FamilySymbol</span> symbol = <span class="blue">null</span>;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">FamilySymbol</span> s <span class="blue">in</span> f.Symbols )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; symbol = s;
&nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; <span class="green">// create family instance:</span>
&nbsp; &nbsp; <span class="green">//</span>
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != symbol )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( doc.IsFamilyDocument )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; fi = doc.FamilyCreate.NewFamilyInstance(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span>.Zero, symbol, _non_rst );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; fi = doc.Create.NewFamilyInstance(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span>.Zero, symbol, _non_rst );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> fi;
}
</pre>

<p>It accesses the NewFamilyInstance method on either the creation document returned by doc.Create in the project context, or on the family item factory returned by doc.FamilyCreate in the family one.

<p>The NewFamilyInstance method provides several different overloads, and which one to use depends on the details of the families you are working with. In this case, we simply used the one that was easiest to call in this context.

<p>Here is an example of a simple external command Execute method mainline with no error checking whatsoever calling this method to insert the two column and shelf families you mention:

<pre class="code">
<span class="blue">public</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span> Execute(
&nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="teal">Application</span> app = commandData.Application;
&nbsp; <span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
&nbsp; <span class="teal">FamilyInstance</span> a = InsertFamilySymbolFromRfa(
&nbsp; &nbsp; <span class="maroon">&quot;C:/tmp/column.rfa&quot;</span>, doc );
&nbsp;
&nbsp; <span class="teal">FamilyInstance</span> b = InsertFamilySymbolFromRfa(
&nbsp; &nbsp; <span class="maroon">&quot;C:/tmp/shelf.rfa&quot;</span>, doc );
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">IExternalCommand</span>.<span class="teal">Result</span>.Succeeded;
}
</pre>

<p>I created both column.rfa and shelf.rfa from the metric column template with rather arbitrarily defined geometry for the column and shelf symbols. Here is the result of running the command in a new third target family document, which is also based on the metric column template:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833012875b89f2f970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833012875b89f2f970c" alt="Nested column and shelf family instances" title="Column_and_shelf" src="/assets/image_c0e4ac.jpg" border="0"  /></a> <br />

</center>

<p>Here is an

<span class="asset  asset-generic at-xid-6a00e553e168978833012875b89c9a970c"><a href="http://thebuildingcoder.typepad.com/files/rfa_labs_20091028-3.zip">updated version</a></span>

of the

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/revit-family-creation-api-labs.html">
Family API Labs</a>

that I will be using for my Family API virtual session at

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/au-2009.html">
Autodesk University 2009</a>

including this new command as Lab 5, though only in the C# project.
