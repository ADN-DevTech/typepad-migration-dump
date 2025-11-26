---
layout: "post"
title: "Duplicate Legend Component"
date: "2010-05-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/05/duplicate-legend-component.html "
typepad_basename: "duplicate-legend-component"
typepad_status: "Publish"
---

<p>A number of Revit elements can still not be created programmatically, because the API does not provide the appropriate creation methods for them.

<p>Joe Ye just handled a case asking how to create a new legend component, and Harry Mattison suggested an exciting new possibility that we were previously not aware of.

<p>This method allows us to duplicate many kinds of elements with an existing instance inserted. Here is the original question as a starting point:

<p><strong>Question:</strong> Is it possible to add a new Legend Component to the Legend programmatically?
I have found no API for the Legend Component.
Is there some workaround &ndash; for example invoking a keyboard shortcut for it?

<p>Is it possible to copy an existing Legend Component and change the family it is referring to?

<p><strong>Answer:</strong> The Revit API does not currently expose any methods that allow us to add a new legend component.

<p>The good news is that there is a workaround to create a new element by copying an existing one.

<p>The solution is to put the element that you would like to copy into a group, place an instance of the group in the document, and then ungroup the two groups.

<p>The result is that the existing element remains and a new element is created in addition.

<p>Here is a new Building Coder sample command CmdDuplicateElements which demonstrates this, and also provides a first example of a command modifying the Revit model using manual transaction mode, which requires it to start and commit its own transaction:

<pre class="code">
[<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Manual )]
[<span class="teal">Regeneration</span>( <span class="teal">RegenerationOption</span>.Manual )]
<span class="blue">class</span> <span class="teal">CmdDuplicateElements</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> app = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = app.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Duplicate Elements&quot;</span> );
&nbsp;
&nbsp; &nbsp; trans.Start();
&nbsp;
&nbsp; &nbsp; <span class="teal">Group</span> group = doc.Create.NewGroup(
&nbsp; &nbsp; &nbsp; uidoc.Selection.Elements );
&nbsp;
&nbsp; &nbsp; <span class="teal">LocationPoint</span> location = group.Location
&nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">LocationPoint</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> p = location.Point;
&nbsp; &nbsp; <span class="teal">XYZ</span> newPoint = <span class="blue">new</span> <span class="teal">XYZ</span>( p.X, p.Y + 10, p.Z );
&nbsp;
&nbsp; &nbsp; <span class="teal">Group</span> newGroup = doc.Create.PlaceGroup(
&nbsp; &nbsp; &nbsp; newPoint, group.GroupType );
&nbsp;
&nbsp; &nbsp; group.Ungroup();
&nbsp;
&nbsp; &nbsp; <span class="teal">ElementSet</span> eSet = newGroup.Ungroup();
&nbsp;
&nbsp; &nbsp; <span class="green">// change the property or parameter values </span>
&nbsp; &nbsp; <span class="green">// of elements in eSet as required...</span>
&nbsp;
&nbsp; &nbsp; trans.Commit();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<p>To test this command, you can select a legend component and launch the command.
A new legend component will be created.
You can then change the new legend's parameter
Component Type to change the legend type.

<p>I created three legend components and three other detail objects, selected them, and lauched the command, which produced the six copied elements ten feet higher up like this:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330134824f8855970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330134824f8855970c" alt="Duplicated elements" title="Duplicated elements" src="/assets/image_4bfff9.jpg" border="0"  /></a> <br />

</center>

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e1689788330134824f87c2970c"><a href="http://thebuildingcoder.typepad.com/files/bc_11_70.zip">version 2011.0.70.0</a></span>

of The Building Coder sample source code and Visual Studio solution including the new command.

<p>Many thanks to Joe for handling the case and Harry for suggesting this approach.
