---
layout: "post"
title: "FindInserts Determines Void Instances Cutting Floor"
date: "2017-06-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
  - "Family"
  - "Filters"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/06/findinserts-determines-void-instances-cutting-a-floor.html "
typepad_basename: "findinserts-determines-void-instances-cutting-a-floor"
typepad_status: "Publish"
---

<p>Is it hot enough for you?</p>

<p>It sure is for this guy:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb09a837e1970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb09a837e1970d img-responsive" alt="Melted candle" title="Melted candle" src="/assets/image_7edb41.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Time for some rest and recuperation, meseems...</p>

<p>Before that, let me share another brilliant and super succinct solution provided by Fair59, answering
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread on how
to <a href="https://forums.autodesk.com/t5/revit-api-forum/get-cutting-void-instances-in-the-floor/m-p/7170237">get cutting void instances in the floor</a> using 
the <a href="http://www.revitapidocs.com/2017/56a32e0b-df65-a6ba-40bd-8f50a1f31dcd.htm"><code>HostObject</code></a>
<a href="http://www.revitapidocs.com/2017/58990230-38cb-3af7-fd25-96ed3215a43d.htm"><code>FindInserts</code></a> method:</p>

<p><strong>Question:</strong> I have a floor on which a family instance is inserted on the face of the floor (the instance host is also the floor).</p>

<p>I checked in the family the "Cut with Void When Loaded" parameter, so that the void is created in the floor.</p>

<p>Now, I want to retrieve all the instances that create voids in the floor.</p>

<p>I did some research, and found the discussion
of <a href="http://thebuildingcoder.typepad.com/blog/2011/06/boolean-operations-and-instancevoidcututils.html">Boolean operations and <code>InstanceVoidCutUtils</code></a>.</p>

<p>But when I use the <code>InstanceVoidCutUtils</code> <code>GetCuttingVoidInstances</code> method, it returns an empty list.</p>

<p>I also looked
at the <a href="http://thebuildingcoder.typepad.com/blog/2015/07/intersect-solid-filter-avf-and-directshape-for-debugging.html#2"><code>ElementIntersectsSolidFilter</code> problem and solution</a> and
tried <code>ElementIntersectsElementFilter</code> and <code>ElementIntersectsSolidFilter</code>.</p>

<p>Those filters do not return the expected result for me to deduce the voids in the floor either; in fact, they say that no elements intersect.</p>

<p>First case &ndash; area = 607.558m2 and Volume = 243.023m3:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c9051032970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c9051032970b image-full img-responsive" alt="Void instances cutting floor" title="Void instances cutting floor" src="/assets/image_e63abf.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Second case &ndash; area = 607.558m2 and Volume = 243.023m3:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d28f5fd7970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d28f5fd7970c image-full img-responsive" alt="Void instances cutting floor" title="Void instances cutting floor" src="/assets/image_9f6331.jpg" border="0" /></a><br /></p>

<p></center></p>

<p><code>Family</code> parameter "Cut with Voids When Loaded":</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d28f5fdd970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d28f5fdd970c image-full img-responsive" alt="Void instances cutting floor" title="Void instances cutting floor" src="/assets/image_93f3d2.jpg" border="0" /></a><br /></p>

<p></center></p>

<p><code>FamilyInstance</code> cutting host:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb09a837f6970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb09a837f6970d image-full img-responsive" alt="Void instances cutting floor" title="Void instances cutting floor" src="/assets/image_6cdad0.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Here is the code I use:</p>

<pre class="code">
  <span style="color:#2b91af;">Solid</span>&nbsp;solid&nbsp;=&nbsp;floor.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;)
  &nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;()
  &nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">Solid</span>&gt;(&nbsp;s&nbsp;=&gt;&nbsp;(<span style="color:blue;">null</span>&nbsp;!=&nbsp;s)&nbsp;&amp;&amp;&nbsp;(!s.Edges.IsEmpty)&nbsp;)
  &nbsp;&nbsp;.FirstOrDefault();

  <span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;intersectingInstances&nbsp;
  &nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsSolidFilter</span>(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solid&nbsp;)&nbsp;);

  <span style="color:blue;">int</span>&nbsp;n1&nbsp;=&nbsp;intersectingInstances.Count&lt;<span style="color:#2b91af;">Element</span>&gt;();

  intersectingInstances&nbsp;
  &nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsElementFilter</span>(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floor&nbsp;)&nbsp;);

  <span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;intersectingInstances.Count&lt;<span style="color:#2b91af;">Element</span>&gt;();
</pre>

<p>Here, both <code>n</code> and <code>n1</code> are equal to 0.</p>

<p><strong>Answer:</strong> Try using 
the <a href="http://www.revitapidocs.com/2017/56a32e0b-df65-a6ba-40bd-8f50a1f31dcd.htm"><code>HostObject</code></a>
<a href="http://www.revitapidocs.com/2017/58990230-38cb-3af7-fd25-96ed3215a43d.htm"><code>FindInserts</code></a> method instead:</p>

<pre class="code">
  <span style="color:#2b91af;">HostObject</span>&nbsp;floor;
  <span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;intersectingInstanceIds&nbsp;
  &nbsp;&nbsp;=&nbsp;floor.FindInserts(&nbsp;<span style="color:blue;">false</span>,&nbsp;<span style="color:blue;">false</span>,&nbsp;<span style="color:blue;">false</span>,&nbsp;<span style="color:blue;">true</span>&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.ToList();
</pre>

<p><strong>Response:</strong> I have done some tests and here are my results:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d28f5fe3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d28f5fe3970c image-full img-responsive" alt="Void instances cutting floor" title="Void instances cutting floor" src="/assets/image_f02ffa.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Situation:</p>

<ul>
<li><code>Fl_1</code> is hosted by Level 3 and intersects the floor.</li>
<li><code>Fl_2</code> is hosted by the floor and intersects it.</li>
</ul>

<p>Results:</p>

<ol>
<li>Do not cut geometry:
<ul>
<li><code>InstanceVoidCutUtils.GetCuttingVoidInstances(floor)</code> returns <code>void</code></li>
<li><code>floor.FindInserts(false,false,false,true)</code> returns <code>Fl_2</code></li>
</ul></li>
<li>Cut geometry:
<ul>
<li><code>InstanceVoidCutUtils.GetCuttingVoidInstances(floor)</code> returns <code>Fl_1</code></li>
<li><code>floor.FindInserts(false,false,false,true)</code> returns both <code>Fl_1</code> and <code>Fl_2</code></li>
</ul></li>
</ol>

<p>In summary, <code>FindInserts</code> returns <code>FI_1</code> even if its host (Level 3) is not the floor.</p>

<p>It's good.</p>

<p>I think we can say that the problem is solved.</p>

<p>Thank you FAIR59 ;)</p>
