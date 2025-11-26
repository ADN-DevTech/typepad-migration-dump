---
layout: "post"
title: "Retrieving Schedules on a Sheet"
date: "2013-02-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "Data Access"
  - "Element Relationships"
  - "Filters"
  - "Schedule"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/02/retrieving-schedules-on-a-sheet.html "
typepad_basename: "retrieving-schedules-on-a-sheet"
typepad_status: "Publish"
---

<p><a href="http://redbolts.com">Guy Robinson</a> recently

explained an easy way to

<a href="http://thebuildingcoder.typepad.com/blog/2012/12/accessing-all-element-in-a-schedule.html">
access all elements in a schedule</a>.

<p><a href="http://www.facebook.com/profile.php?id=100003616852588">
Victor Chekalin</a> picked up and expanded on that.

<p>Here is his solution to retrieve all schedules in a view and elements from a schedule.
In his own words:</p>

<p>One wonderful day I had to retrieve schedules which are presented on a
sheet:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c36e456f6970b-pi"><img class="asset  asset-image at-xid-6a00e553e168978833017c36e456f6970b" alt="Schedules on a sheet" title="Schedules on a sheet" src="/assets/image_e3c636.jpg" /></a><br />

</center>

<p>At first I looked at the Revit API Help but unfortunately there is no method such as GetSchdulesOnView in the ViewSheet class.
There is no explicit way to get it.
After a couple of hours trying to achieve that I need I encountered Guy's simple solution to

<a href="http://thebuildingcoder.typepad.com/blog/2012/12/accessing-all-element-in-a-schedule.html">
access all elements in a schedule</a>.

The solution is use FilteredElementCollector and pass in the element id of the view in which you want to collect elements.

<p>I created the following simple extension method GetSchedules to get schedules on view:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">class</span> <span class="teal">ViewSheetExtensions</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="teal">IEnumerable</span>&lt;<span class="teal">ViewSchedule</span>&gt;
&nbsp; &nbsp; &nbsp; GetSchedules( <span class="blue">this</span> <span class="teal">ViewSheet</span> viewSheet )&nbsp; &nbsp;
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> doc = viewSheet.Document;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> collector =
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, viewSheet.Id );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> scheduleSheetInstances =
&nbsp; &nbsp; &nbsp; &nbsp; collector
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">ScheduleSheetInstance</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ToElements()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OfType&lt;<span class="teal">ScheduleSheetInstance</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="blue">var</span> scheduleSheetInstance <span class="blue">in</span>
&nbsp; &nbsp; &nbsp; &nbsp; scheduleSheetInstances )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">var</span> scheduleId =
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; scheduleSheetInstance
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ScheduleId;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( scheduleId == <span class="teal">ElementId</span>.InvalidElementId )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">var</span> viewSchedule =
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc.GetElement( scheduleId )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">ViewSchedule</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( viewSchedule != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">yield</span> <span class="blue">return</span> viewSchedule;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>It can be used like this:

<pre class="code">
&nbsp; <span class="blue">var</span> schedules = viewSheet
&nbsp; &nbsp; .GetSchedules()
&nbsp; &nbsp; .ToList();

&nbsp; <span class="blue">foreach</span>( <span class="blue">var</span> viewSchedule <span class="blue">in</span> schedules )
&nbsp; {
&nbsp; &nbsp; <span class="green">// Do something</span>
&nbsp; }
</pre>

<p>Here is the demo result:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d4113a395970c-pi"><img class="asset  asset-image at-xid-6a00e553e168978833017d4113a395970c" alt="Demo result" title="Demo result" src="/assets/image_0f32e6.jpg" /></a><br />

</center>

<p>But I decided not to stop and go deeper.
The next step is to get all elements which are involved in a schedule.
The approach is the same as with schedules. Using FiltereredElementCollector but instead of the ViewSheet element id we pass in the ViewSchedule id.

<p>The extension method is similar:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">class</span> <span class="teal">ViewScheduleExtensions</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="teal">IEnumerable</span>&lt;<span class="teal">ElementId</span>&gt;
&nbsp; &nbsp; &nbsp; GetElementIdsInSchedule(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">this</span> <span class="teal">ViewSchedule</span> viewSchedule )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> doc = viewSchedule.Document;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> collector =
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, viewSchedule.Id );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> elementIds = collector
&nbsp; &nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType()
&nbsp; &nbsp; &nbsp; &nbsp; .ToElementIds();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> elementIds;
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>But there is an additional little nuance.
If I retrieve elements from a material takeoff schedule, the method will return all materials in the project together with other elements. The solution is to just skip all elements which are materials:</p>

<pre class="code">
&nbsp; <span class="blue">foreach</span>( var id <span class="blue">in</span> elementIds )
&nbsp; {
&nbsp; &nbsp; var element = doc.GetElement( id );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( element <span class="blue">is</span> <span class="teal">Material</span> )
&nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp;
&nbsp; &nbsp; <span class="green">// Do something</span>
&nbsp; }
</pre>

<p>Here is the result of my demo project:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d4113a2f8970c-pi"><img class="asset  asset-image at-xid-6a00e553e168978833017d4113a2f8970c" alt="Demo result" title="Demo result" src="/assets/image_90b65a.jpg" /></a><br />

</center>

<p>I attached the

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee88785bb970d"><a href="http://thebuildingcoder.typepad.com/files/vcscheduleapidemo.zip">demo Visual Studio solution and Revit project</a></span>.

<p>I hope this information will useful for developers.

<p>Have a nice day,
<br>Victor.
