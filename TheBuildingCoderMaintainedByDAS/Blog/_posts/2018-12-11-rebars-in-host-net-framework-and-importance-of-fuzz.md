---
layout: "post"
title: "Rebars in Host, .NET Framework, Importance of Fuzz"
date: "2018-12-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2019"
  - "Dimensioning"
  - "Element Relationships"
  - "Geometry"
  - "Migration"
  - "RST"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/12/rebars-in-host-net-framework-and-importance-of-fuzz.html "
typepad_basename: "rebars-in-host-net-framework-and-importance-of-fuzz"
typepad_status: "Publish"
---

<p>Here are a couple
more <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> threads
well worth highlighting:</p>

<ul>
<li><a href="#2">Retrieve rebars attached to structural element</a> </li>
<li><a href="#3">Installing the .NET framework 4.7 for Revit 2019.1 add-ins</a> </li>
<li><a href="#4">Importance of fuzz for curtain wall dimensioning</a> </li>
</ul>

<h4><a name="2"></a> Retrieve Rebars Attached to Structural Element</h4>

<p><strong>Question:</strong> How can I get all associated rebars which attach to a structural element such as a column by picking that?</p>

<p><strong>Answer:</strong> Jeremy initially
suggested <a href="https://thebuildingcoder.typepad.com/blog/2018/12/using-an-intersection-filter-for-linked-elements.html#3">workarounds making use of filtered element collectors</a>.
Unfortunately, that was not very helpful in this case.</p>

<p>Happily, Einar Raknes came to the rescue pointing out the real solution for this:</p>

<p>You can use the <code>RebarHostData</code> class and the <code>GetRebarsInHost</code> method to retrieve all rebars associated with a rebar host.</p>

<p>To make sure you pick a valid Rebar Host; you can optionally create a selection filter for it like this:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">RebarHostSelectionFilter</span>&nbsp;:&nbsp;<span style="color:#2b91af;">ISelectionFilter</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;AllowElement(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;<span style="color:#2b91af;">RebarHostData</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetRebarHostData(&nbsp;e&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;AllowReference(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r,&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>Pick a rebar host, and retrieve the list of rebars from it like this:</p>

<pre class="code">
&nbsp;&nbsp;ref1&nbsp;=&nbsp;uidoc.Selection.PickObject(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">RebarHostSelectionFilter</span>(),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Pick&nbsp;a&nbsp;rebar&nbsp;host&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;rebarHost&nbsp;=&nbsp;doc.GetElement(&nbsp;ref1&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Rebar</span>&gt;&nbsp;rebarsInHost&nbsp;=&nbsp;<span style="color:#2b91af;">RebarHostData</span>
&nbsp;&nbsp;&nbsp;&nbsp;.GetRebarHostData(&nbsp;rebarHost&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.GetRebarsInHost();
</pre>

<p>Many thanks to Einar for pointing this out!</p>

<h4><a name="3"></a> Installing the .NET Framework 4.7 for Revit 2019.1 Add-Ins</h4>

<p><strong>Question:</strong> I am walking through the Autodesk University 2018 course on Revit add-ins:</p>

<ul>
<li><a href="https://www.autodesk.com/autodesk-university/class/Pushing-Revit-Next-Level-Intro-Revit-Plugins-C-2018">Pushing Revit to the Next Level &ndash; an Intro to Revit Plugins with C#</a></li>
</ul>

<p>Revit 2019.1 add-in programming apparently requires the .NET framework 4.7.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3a687db200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3a687db200d image-full img-responsive" alt="Figure 20 Create Project" title="Figure 20 Create Project" src="/assets/image_12d6a9.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Figure 20 Create Project</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3c63818200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3c63818200b image-full img-responsive" alt="Class Library .NET Framework" title="Class Library .NET Framework" src="/assets/image_81ea8a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Class Library .NET Framework</p>

<p></center></p>

<p>However, I cannot seem to get access to .NET Framework 4.7.</p>

<p>I have turned this on in Windows Features:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3c63820200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3c63820200b image-full img-responsive" alt=".NET Framework 4.7 in Windows features" title=".NET Framework 4.7 in Windows features" src="/assets/image_fefb7b.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">.NET Framework 4.7 in Windows features</p>

<p></center></p>

<p>What am I missing?</p>

<p><strong>Answer:</strong> In Visual Studio, go to Tools &rarr; Get Tools and Features &rarr; Individual Components &rarr; tick the .NET version you want to install:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3808a53200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3808a53200c image-full img-responsive" alt="VS tools" title="VS tools" src="/assets/image_f03b63.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">VS Tools</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3c6382e200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3c6382e200b image-full img-responsive" alt="VS tools" title="VS tools" src="/assets/image_821571.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p></center></p>

<p>Many thanks to Salvatore Dragotta for pointing this out!</p>

<h4><a name="4"></a> Importance of Fuzz for Curtain Wall Dimensioning</h4>

<p>The Building Coder keeps on harping about the importance of fuzz, cf. the recent discussion
of <a href="https://thebuildingcoder.typepad.com/blog/2017/12/project-identifier-and-fuzzy-comparison.html#3">fuzzy comparison versus exact arithmetic for curve intersection</a>.</p>

<p>Here is yet another example underlining the importance of fuzz, described by Bram Weinreder in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/not-all-curtain-walls-behaving-equally/m-p/8457387">not all curtain walls behaving equally</a>:</p>

<p><strong>Question:</strong> I've made an add-in to dimension curtain walls, and in my test projects it was working fairly well. That's to say, tagging worked 100% (easy win), but elevation tags and dimensioning didn't always work (let's say 70% or 80% worked for me, but for some users and some projects 100% produced the same failure).</p>

<p>The problem is that I'm not getting the total widths of these windows, in rare cases the total heights, or the bottom reference for spot elevations. I'm getting the references based on physical mullion faces with a certain normal, for mullions that work in a certain direction (this works very well, generally, if I need all unfiltered references). It's probably where I filter out the exterior faces that I make a mistake.</p>

<p>Example of how I do my filtering:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;fa&nbsp;<span style="color:blue;">in</span>&nbsp;faces&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Mullion</span>&nbsp;m&nbsp;=&nbsp;doc.GetElement(&nbsp;fa.Reference&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Mullion</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">LocationPoint</span>&nbsp;lp&nbsp;=&nbsp;m.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationPoint</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r&nbsp;=&nbsp;fa.Reference;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;direction&nbsp;==&nbsp;Direction.horizontal&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.Z&nbsp;==&nbsp;bb.Min.Z&nbsp;&amp;&amp;&nbsp;!minAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.Z&nbsp;==&nbsp;bb.Max.Z&nbsp;&amp;&amp;&nbsp;!maxAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;direction&nbsp;==&nbsp;Direction.vertical&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.X&nbsp;==&nbsp;bb.Min.X&nbsp;&amp;&amp;&nbsp;!leftAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;leftAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.X&nbsp;==&nbsp;bb.Max.X&nbsp;&amp;&amp;&nbsp;!rightAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rightAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>The Normal is always relative to the mullion interior coordinates; in short this is filtering the faces that are looking out. But apparently, this is not an adequate method in all situations.</p>

<p>Does anyone know whether there can be a difference between curtain walls that makes them behave differently?</p>

<p>Side note: is there a more reliable way to get the exterior references (say, by bounding box)?</p>

<p>Thanks in advance.</p>

<p><strong>Answer:</strong> And I found my own answer.</p>

<p>What's vexing me is that these references don't have GlobalPoints, or I would've stumbled upon this quicker.</p>

<p>The error is in this part of the filtering:</p>

<pre class="code">
  <span style="color:blue;">if</span>(&nbsp;lp.Point.Z&nbsp;==&nbsp;bb.Min.Z&nbsp;*/...<span style="color:green;">/*)</span>
</pre>

<p>I'm not sure whether it's due to the conversion between imperial and metric, but I forgot the fundamental rule that you can't always directly compare two <code>XYZ</code> values.</p>

<p>I replaced the condition with this:</p>

<pre class="code">
  <span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Math</span>.Abs(&nbsp;lp.Point.Z&nbsp;-&nbsp;bb.Min.Z&nbsp;)&nbsp;&lt;&nbsp;0.005&nbsp;*/...<span style="color:green;">/*)</span>
</pre>

<p>This translates to a tolerance of about 1.5mm.</p>

<p>Could've probably added three more zeroes there, but this is precise enough for our case.</p>

<p>Many thanks to Bram for pointing this out!</p>
