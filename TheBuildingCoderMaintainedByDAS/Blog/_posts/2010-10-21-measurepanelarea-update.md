---
layout: "post"
title: "MeasurePanelArea Update"
date: "2010-10-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Filters"
  - "Installation"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/10/measurepanelarea-update.html "
typepad_basename: "measurepanelarea-update"
typepad_status: "Publish"
---

<p>Iffat Mai of 

<a href="http://www.perkinswill.com">
Perkins + Will</a> brought 

up an issue with the MeasurePanelArea Revit SDK sample, handled by Joe Ye:

<p><strong>Question:</strong> As we reviewed the Massing &gt; MeasurePanelArea SDK Sample, the following error message was displayed while trying to run it:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f53c6f2b970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f53c6f2b970b image-full" alt="MeasurePanelArea error message" title="MeasurePanelArea error message" src="/assets/image_953d88.jpg" border="0" /></a> <br />

</center>

<p>It says: "Input type is of an element type that exists in the API, but not in Revit's native object model. 
Try using Autodesk.Revit.DB.FamilyInstance instead, and then post-processing the results to find the elements of interest." 

<p><strong>Answer:</strong> This is due to the fact that the Panel class cannot be accepted by the FilteredElementCollector OfClass method, i.e. when calling

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span>
&nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Panel</span> ) );
</pre>

<p>This call is made in the GetElements template method:

<pre class="code">
&nbsp; <span class="blue">protected</span> <span class="teal">List</span>&lt;T&gt; 
&nbsp; &nbsp; GetElements&lt;T&gt;() <span class="blue">where</span> T : <span class="teal">Element</span>
</pre>

<p>GetElements in turn is called by the method BuildPanelTypeList:

<pre class="code">
&nbsp; <span class="blue">private</span> <span class="blue">void</span> BuildPanelTypeList()
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">Panel</span>&gt; list = GetElements&lt;<span class="teal">Panel</span>&gt;();
&nbsp; &nbsp; . . .
&nbsp; } 
</pre>

<p>We already discussed this issue of 

<a href="http://thebuildingcoder.typepad.com/blog/2010/08/filtering-for-a-nonnative-class.html">
filtering for a non-native class</a> in

some depth. 
It applies to AnnotationSymbol, Panel, Room and several other classes.

<p>I changed the sample slightly to add another non-template method to get all panels in current document instead:

<pre class="code">
<span class="blue">protected</span> <span class="teal">IList</span>&lt;<span class="teal">Element</span>&gt; GetPanels()
{
&nbsp; <span class="teal">FilteredElementCollector</span> collector 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( 
&nbsp; &nbsp; &nbsp; m_uiDoc.Document );
&nbsp;
&nbsp; <span class="teal">BuiltInCategory</span> bic 
&nbsp; &nbsp; = <span class="teal">BuiltInCategory</span>.OST_CurtainWallPanels;
&nbsp;
&nbsp; collector.OfClass( <span class="blue">typeof</span>( <span class="teal">FamilyInstance</span> ) )
&nbsp; &nbsp; .OfCategory( bic );
&nbsp;
&nbsp; <span class="teal">IList</span>&lt;<span class="teal">Element</span>&gt; panels = collector.ToElements();
&nbsp;
&nbsp; <span class="blue">return</span> panels;
}
</pre>

<p>Here is my modified version of 

<span class="asset  asset-generic at-xid-6a00e553e1689788330134885c3dce970c"><a href="http://thebuildingcoder.typepad.com/files/formpanelarea-1.cs">FormPanelArea.cs</a></span> with these changes.

Please replace the existing version of the file with it.

<p>Many thanks to Joe for this fix!

<p>The newly released 

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/subscription-release-and-updated-sdk.html">
updated SDK</a> for the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/09/revit-2011-web-update-2.html">
web update 2</a> and 

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/subscription-release-and-updated-sdk.html">
subscription releases</a> includes 

several other 

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/subscription-release-and-updated-sdk.html#7">
SDK sample updates</a>.

Unfortunately the MeasurePanelArea fix was not made in time to be included in the SDK update.
We also recently discussed a fix to the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/09/doorswing-fix.html">
DoorSwing SDK sample</a> which

does not seem to have been included either.
