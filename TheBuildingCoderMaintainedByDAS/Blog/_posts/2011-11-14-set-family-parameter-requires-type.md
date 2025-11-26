---
layout: "post"
title: "Set Family Parameter Requires Type"
date: "2011-11-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/11/set-family-parameter-requires-type.html "
typepad_basename: "set-family-parameter-requires-type"
typepad_status: "Publish"
---

<p>I was running down to town to have tea with my friend Otto on Saturday morning, and happened to see this autumn leaf lying on the ground. 
Turned back, picked it up, and made a picture of it in the sunshine on Otto's veranda. 
It is completely unretouched, I promise!

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015436d3040b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015436d3040b970c image-full" alt="Autumn leaf" title="Autumn leaf" src="/assets/image_7a6b13.jpg" border="0" /></a><br />
</center>

<p>Back to the Revit API, here is a short note on another issue run into by Ishwar Nagwani, Technical Consultant in our Bangalore organisation, who recently contributed the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/11/pick-corners-and-create-floor.html">
pick points to create a new a floor</a> sample, and answered by Harry Mattison of the Revit development team.

<p><strong>Question:</strong> I am trying to update an existing family parameter and set its value, but it is always throwing an InvalidOperationException saying "There is no current type".
What is wrong?

<p>Here is some minimal sample code to reproduce the issue.
Only the family file name is hardcoded.

<pre class="code">
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
{
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp;
&nbsp; &nbsp; uiapp.OpenAndActivateDocument( 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;C:\\Projects\\FY12\\GE Revit\\SWBD-AV2.rfa&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Application</span> app = uiapp.Application;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="teal">FamilyManager</span> familyMgr = doc.FamilyManager;
&nbsp;
&nbsp; &nbsp; <span class="teal">FamilyParameter</span> param = familyMgr.get_Parameter( 
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Width&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == param )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; param = doc.FamilyManager.AddParameter( 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Width&quot;</span>, <span class="teal">BuiltInParameterGroup</span>.PG_GEOMETRY, 
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ParameterType</span>.Length, <span class="blue">true</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; familyMgr.Set( param, 0.2 ); <span class="green">// set the value</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>


<p><strong>Answer:</strong> The exception message actually says it all, albeit rather succinctly: "There is no current type".

<p>FamilyManager.Set is used to set the value of a parameter for the current family type. 
The family you are working with had no types defined, therefore Set could not work. 
You can remedy this by adding a family type manually or with the following two lines:

<pre class="code">
&nbsp; <span class="blue">if</span>( doc.FamilyManager.Types.Size == 0 )
&nbsp; &nbsp; doc.FamilyManager.NewType( <span class="maroon">&quot;Type 1&quot;</span> );

&nbsp; familyMgr.Set( param, 0.2 );
</pre>

<p>Thanks to Ishwar and Harry for sharing this!
