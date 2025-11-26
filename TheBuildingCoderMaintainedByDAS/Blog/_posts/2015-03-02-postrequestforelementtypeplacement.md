---
layout: "post"
title: "PostRequestForElementTypePlacement Sample"
date: "2015-03-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2015"
  - "Element Creation"
  - "Family"
  - "Git"
  - "Transaction"
  - "User Interface"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/03/postrequestforelementtypeplacement.html "
typepad_basename: "postrequestforelementtypeplacement"
typepad_status: "Publish"
---

<p>I am back from my vacation in the snow, full of new energy, and up to both ears in hot Revit API cases again instead of that frozen stuff  :-)</p>

<p>The creation document NewFamilyInstance method provides the traditional way to programmatically create a new family instance within a project, or a nested instance within a family document.</p>

<p>It does not support any user interaction whatsoever.</p>

<p>A little bit of user interaction is provided by the PromptForFamilyInstancePlacement method introduced in the

<a href="http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2011-api.html">Revit 2011 API</a>.</p>

<p>At least it shows a preview of the item to be placed and prompts the user for its location.</p>

<p>Numerous samples using the PromptForFamilyInstancePlacement method are available, most recently here, including sample code to execute it and

<a href="http://thebuildingcoder.typepad.com/blog/2015/02/sending-escape-to-terminate-a-family-instance-placement-loop.html">
terminate its family instance placement loop</a>,

and in the Revit API discussion forum, e.g.:</p>

<ul>

<li><a href="http://forums.autodesk.com/t5/Revit-API/promptforfamilyinstanceplacement-spacebar-to-rotate-causes/m-p/4794701">
PromptForFamilyInstancePlacement spacebar to rotate</a></li>

<li><a href="http://forums.autodesk.com/t5/revit-api/promptforf-amilyinsta-nceplaceme-nt-to-allow-adding-in-an/m-p/5439737">
PromptForFamilyInstancePlacement in an enclosed space</a></li>

</ul>

<p>The PromptForFamilyInstancePlacement method does not offer all the possible user interaction functionality provided by the manual Revit user interface either, though.</p>

<p>The Revit 2015 API introduced the PostRequestForElementTypePlacement method to access the full Revit user interaction functionality, at the cost of giving up full programmatic control and passing the baton from the add-in code back to Revit, cf.

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html">What's New in the Revit 2015 API</a> &gt;

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#4">Minor API additions</a> &gt;

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#4.02">Document API additions</a>.</p>


<p>Another interesting Revit API forum discussion on

<a href="http://forums.autodesk.com/t5/Revit-API/Change-the-placement-option-for-PromptForFamilyInstancePlacement/m-p/3187174">
changing the placement option for PromptForFamilyInstancePlacement</a> also

includes a snippet of sample code in VB.NET by PhillipM for using PostRequestForElementTypePlacement.</p>

<p>Its usage is extremely simple, since the add-in is asking Revit to do all the work and passing over control as well.</p>

<p>All you need to do is supply an element type for the user to place:</p>

<pre class="code">
[<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">class</span> <span class="teal">CmdPostRequestInstancePlacement</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="teal">ElementType</span> elementType
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfCategory( <span class="teal">BuiltInCategory</span>.OST_Walls )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">ElementType</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .FirstElement() <span class="blue">as</span> <span class="teal">ElementType</span>;
&nbsp;
&nbsp; &nbsp; uidoc.PostRequestForElementTypePlacement(
&nbsp; &nbsp; &nbsp; elementType );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

<p>There is no need to encapsulate the call in a transaction.</p>

<p>In spite of this, posting the request cannot be triggered in a read-only command, or an invalid operation exception will be triggered saying:</p>

<blockquote>
<p>Revit encountered a Autodesk.Revit.Exceptions.InvalidOperationException: Starting a new transaction is not permitted. It could be because another transaction already started and has not been completed yet, or the document is in a state in which it cannot start a new transaction.</p>
</blockquote>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7574732970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7574732970b img-responsive" style="width: 417px; " alt="PostRequestForElementTypePlacement requires a transaction" title="PostRequestForElementTypePlacement requires a transaction" src="/assets/image_8009e0.jpg" /></a><br />

</center>

<p>I implemented this code in a new external command named CmdPostRequestInstancePlacement in

<a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a>

<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2015.0.119.0">version 2015.0.119.0</a>.</p>


<a name="2"></a>

<h4>The Fear of Fear</h4>

<p>Before signing off for today, here are some sweet and wise thoughts by Mr Ramesh on

<a href="https://www.youtube.com/watch?v=CDlILJoYA3E">fear and aliveness</a>:</p>

<center>
<iframe width="420" height="236" src="https://www.youtube.com/embed/CDlILJoYA3E" frameborder="0" allowfullscreen></iframe>
</center>

<p>Mr Ramesh' invitation to become curious about experiencing your fears is so attractive and sensible that it could revolutionize the world! ... Becoming curious towards our fear...</p>
