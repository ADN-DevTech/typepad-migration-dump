---
layout: "post"
title: "Creating a Sweep with Multiple Closed Loops"
date: "2019-02-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Geometry"
  - "Philosophy"
  - "TED"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/02/creating-a-sweep-with-multiple-closed-loops.html "
typepad_basename: "creating-a-sweep-with-multiple-closed-loops"
typepad_status: "Publish"
---

<p>Until today, <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> lacked
a demonstration of using the <code>NewSweep</code> method.</p>

<p>That was very kindly provided in yet another helpful answer by 
Frank <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518">@Fair59</a> Aarssen to 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-sweep-with-multiple-closed-loops-in-profile/m-p/8477617">how to create a sweep with multiple closed loops in profile</a>.</p>

<p>Let me also highlight an interesting TED talk on the topic of poverty versus universal basic income:</p>

<ul>
<li><a href="#2">Creating a sweep with multiple closed loops</a> </li>
<li><a href="#3">Poverty versus universal basic income</a> </li>
</ul>

<h4><a name="2"></a> Creating a Sweep with Multiple Closed Loops</h4>

<p><strong>Question:</strong> I want to create a sweep with multiple closed loops in profile, like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3c2e26a200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3c2e26a200d img-responsive" style="width: 240px; display: block; margin-left: auto; margin-right: auto;" alt="Profile with multiple loops" title="Profile with multiple loops" src="/assets/image_fc0112.jpg" /></a><br /></p>

<p></center></p>

<p>I can draw the profile on the plane with model lines, but if I use it to create a sweep, it will report errors such as "cannot create sweep" without any other tips.</p>

<p>I have created sweeps using profiles with two loops successfully like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3e29040200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3e29040200b img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Profile with one hole" title="Profile with one hole" src="/assets/image_a7992d.jpg" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad39cd1c9200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad39cd1c9200c img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Profile with the other hole" title="Profile with the other hole" src="/assets/image_8b7ae3.jpg" /></a><br /></p>

<p></center></p>

<p>Is there any limitation, for example, the sweep profile can only have two closed loops at most, otherwise it will be wrong?</p>

<p>Later: After further testing, I still cannot create a sweep with three closed loops.</p>

<p>I tried orienting the two inner loops both clockwise and counterclockwise, but nothing helps.</p>

<p><strong>Answer:</strong> Every loop needs to be a separate <code>CurveArray</code>!</p>

<p>I added Frank's correction to the test code provided and integrated it
into <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> 
module <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdNewSweptBlend.cs">CmdNewSweptBlend.cs</a>:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Sweep</span>&nbsp;CreateSweepWithMultipleLoops(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Extrusion&nbsp;path</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CurveArray</span>&nbsp;path&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();

&nbsp;&nbsp;&nbsp;&nbsp;path.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;5,&nbsp;0&nbsp;)&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Profile&nbsp;vertices:&nbsp;rectangle&nbsp;with&nbsp;two</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;rectangular&nbsp;holes</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;15,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;15,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;3,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;3,&nbsp;10,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;10,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;5,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;7,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;7,&nbsp;10,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;5,&nbsp;10,&nbsp;0&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CurveArrArray</span>&nbsp;arrcurve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArrArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CurveArray</span>&nbsp;curve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p1,&nbsp;p2&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p2,&nbsp;p3&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p3,&nbsp;p4&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p4,&nbsp;p1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;arrcurve.Append(&nbsp;curve&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a1,&nbsp;a4&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a4,&nbsp;a3&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a3,&nbsp;a2&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a2,&nbsp;a1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;arrcurve.Append(&nbsp;curve&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b1,&nbsp;b4&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b4,&nbsp;b3&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b3,&nbsp;b2&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b2,&nbsp;b1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;arrcurve.Append(&nbsp;curve&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;doc.Application;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SweepProfile</span>&nbsp;profile&nbsp;=&nbsp;app.Create
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.NewCurveLoopsProfile(&nbsp;arrcurve&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;plane&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByNormalAndOrigin(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ,&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SketchPlane</span>&nbsp;sketchPlane&nbsp;=&nbsp;<span style="color:#2b91af;">SketchPlane</span>.Create(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;plane&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Sweep</span>&nbsp;sweep&nbsp;=&nbsp;doc.FamilyCreate.NewSweep(&nbsp;<span style="color:blue;">true</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path,&nbsp;sketchPlane,&nbsp;profile,&nbsp;0,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ProfilePlaneLocation</span>.Start&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;sweep;
&nbsp;&nbsp;}
</pre>

<p>Here is the result of running this code in a new family document:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3e2903a200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3e2903a200b img-responsive" style="width: 240px; display: block; margin-left: auto; margin-right: auto;" alt="Sweep with multiple loops" title="Sweep with multiple loops" src="/assets/image_7884db.jpg" /></a><br /></p>

<p></center></p>

<p>Many thanks to Frank for this solution!</p>

<h4><a name="3"></a> Poverty versus Universal Basic Income</h4>

<p>In case you are interested in the topic of universal basic income, you might find this TED talk quite illuminating:
<a href="https://youtu.be/ydKcaIE6O1k">Poverty isn't a lack of character; it's a lack of cash</a>, by historian Rutger Bregman, on June 13, 2017.</p>

<blockquote>
  <p>"Ideas can and do change the world," he says, sharing his case for a provocative one: guaranteed basic income. Learn more about the idea's 500-year history and a forgotten modern experiment where it actually worked &ndash; and imagine how much energy and talent we would unleash if we got rid of poverty once and for all.</p>
</blockquote>

<p><center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/ydKcaIE6O1k" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>
