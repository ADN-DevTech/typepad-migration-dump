---
layout: "post"
title: "Using Reflector to diagnose tail call optimization in F#"
date: "2008-02-27 13:13:21"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Visual Studio"
original_url: "https://www.keanw.com/2008/02/using-reflector.html "
typepad_basename: "using-reflector"
typepad_status: "Publish"
---

<p>I've talked about <a href="http://www.aisto.com/roeder/dotnet/">Lutz Roeder's Reflector tool</a> a <a href="http://through-the-interface.typepad.com/through_the_interface/2007/01/reflecting_on_a.html">couple</a> of <a href="http://through-the-interface.typepad.com/through_the_interface/2007/01/one_for_the_bit.html">times</a> and it's proven to be very useful to me, once again.</p>

<p>I mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/more-random-mus.html">my last post</a> about some problems I was having with tail recursion, and my choice to replace certain recursive functions with iterative versions. Today we're going to use Reflector to take a look under the hood of some compiled assemblies, to determine which recursive functions have been optimised correctly and which have not.</p>

<p>Let's start by taking a look at the two recursive functions I mentioned last time, starting with the most simple:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// A recursive function to show the contents of a list</span></p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> drawPointList (x:Point3d list) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">match</span> x <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">| [] <span style="COLOR: blue">-&gt;</span> ()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; | h :: t <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ed.DrawVector(h,h,1,<span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; drawPointList t</p></div>

<p>Here's what we see when we open the compiled assembly in Reflector and disassemble the equivalent function into C#:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[Serializable]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">internal</span> <span style="COLOR: blue">class</span> drawPointList@140 : FastFunc&lt;List&lt;Point3d&gt;, Unit&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Fields</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> Editor ed;</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Methods</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> drawPointList@140(Editor ed)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">this</span>.ed = ed;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">override</span> Unit Invoke(List&lt;Point3d&gt; x)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">while</span> (<span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;List&lt;Point3d&gt; list = x;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (!(list <span style="COLOR: blue">is</span> List&lt;Point3d&gt;._Cons))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;List&lt;Point3d&gt; list2 = ((List&lt;Point3d&gt;._Cons) list)._Cons2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Point3d from = ((List&lt;Point3d&gt;._Cons) list)._Cons1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">this</span>.ed.DrawVector(from, from, 1, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;x = list2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p>

<br/>

<br/></div>

<p>You can see that the implementation of Invoke contains a while loop, which loops forever (until the list is empty), rather than calling recursively into itself.</p>

<p>Now let's take a look at the other, more complex function, which generates a list of points via recursion:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// a list of random points on its surface</span></p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> getNPoints n (sol:Solid3d) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> n &lt;= 0 <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; []</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> mp = sol.MassProperties</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> pl = <span style="COLOR: blue">new</span> Plane()</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pl.Set(mp.Centroid,randomVector3d sol.ObjectId.OldId)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> reg = sol.GetSection(pl)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> ray = <span style="COLOR: blue">new</span> Ray()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ray.BasePoint &lt;- mp.Centroid</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ray.UnitDir &lt;- randomVectorOnPlane pl</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> pts = <span style="COLOR: blue">new</span> Point3dCollection()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; reg.IntersectWith</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(ray,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; Intersect.OnBothOperands,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; pts,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; 0, 0)</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pl.Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; reg.Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ray.Dispose()</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> ptlist = Seq.untyped_to_list pts</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ptlist @ getNPoints (n - pts.Count) sol</p></div>

<p>This function, when compiled by the F# compiler integrated into Visual Studio and disassembled by Reflector, equates to this C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[Serializable]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">internal</span> <span style="COLOR: blue">class</span> getNPoints@101T&lt;T&gt; : OptimizedClosures.FastFunc2&lt;<span style="COLOR: blue">int</span>, Solid3d, List&lt;T&gt;&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Fields</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> TypeFunc _self0;</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Methods</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> getNPoints@101T(TypeFunc _self0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">this</span>._self0 = _self0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">override</span> List&lt;T&gt; Invoke(<span style="COLOR: blue">int</span> n, Solid3d sol)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; TypeFunc func = <span style="COLOR: blue">this</span>._self0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">int</span> num = n;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">int</span> t = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> ((num &gt; t) ^ <span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> List&lt;T&gt;.Nil_uniq;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Solid3dMassProperties h = sol.MassProperties;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Plane plane = <span style="COLOR: blue">new</span> Plane();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; plane.Set(h.Centroid, MyApplication.randomVector3d&lt;<span style="COLOR: blue">int</span>&gt;(sol.ObjectId.OldId));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Region section = sol.GetSection(plane);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Ray entityPointer = <span style="COLOR: blue">new</span> Ray();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; entityPointer.BasePoint = h.Centroid;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; entityPointer.UnitDir = MyApplication.randomVectorOnPlane&lt;Plane&gt;(plane);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Point3dCollection points = <span style="COLOR: blue">new</span> Point3dCollection();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; section.IntersectWith(entityPointer, Intersect.OnBothOperands, points, 0, 0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; plane.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; section.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; entityPointer.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; List&lt;T&gt; list = Seq.untyped_to_list&lt;Point3dCollection, T&gt;(points);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Solid3d u = sol;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">int</span> num2 = n - points.Count;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> Pervasives.op_Append&lt;T&gt;(FastFunc&lt;<span style="COLOR: blue">int</span>, Solid3d&gt;.InvokeFast2&lt;List&lt;T&gt;&gt;((FastFunc&lt;<span style="COLOR: blue">int</span>, FastFunc&lt;Solid3d, List&lt;T&gt;&gt;&gt;) func.Specialize&lt;T&gt;(), num2, u), list);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>While a little harder to follow (please don't worry about the details), we can see that the recursive tail call has not been optimised out, as we don't have an iterative construct (e.g. a while loop). Instead we see a call to InvokeFast2 (which will result in Invoke being called recursively), prior to the value being returned.</p>

<p>Why exactly this case hasn't been handled isn't clear to me - in fact I'm pretty sure it's a bug in F#, which I will go ahead and report - but it is clear that (at least for now) we need to change the implementation if we're to avoid a stack overflow. And that's fine: the last post contains an iterative version we can use.</p>

<p>Finallly we're going to take a look at parts of the assembly created by building the code in the recent <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/robotic-hatch-1.html">Robotic Hatching</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/parallelizing-r.html">posts</a>. As I mentioned previously, I ended up hitting problems when creating 400 or so segments in the polyline hatch when using my F# implementation. Rather than automatically changing each of the recursive functions (those prefixed by &quot;let rec&quot;) to iterative versions, I checked the disassembly listings one by one until I found the problematic one:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><span style="COLOR: blue"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// A recursive function to get the points</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// for our path</span></p>

<br/></div>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">let</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> times &lt;= 0 <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; []</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> pt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; nextBoundaryPoint cur start doTrace</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(pt :: definePath pt (times-1))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">with</span> exn <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> exn.Message = <span style="COLOR: maroon">&quot;Could not get point&quot;</span> <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; definePath start times</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; failwith exn.Message</p></span><span style="COLOR: blue">rec</span> definePath start times =</div>

<p>Here's how the compiled (and then disassembled/reassembled) function looks in C#:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[Serializable]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">internal</span> <span style="COLOR: blue">class</span> definePath@281 : OptimizedClosures.FastFunc2&lt;Point3d, <span style="COLOR: blue">int</span>, List&lt;Point3d&gt;&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Fields</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> Curve cur;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> doTrace;</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Methods</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> definePath@281(<span style="COLOR: blue">bool</span> doTrace, Curve cur)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">this</span>.doTrace = doTrace;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">this</span>.cur = cur;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">override</span> List&lt;Point3d&gt; Invoke(Point3d start, <span style="COLOR: blue">int</span> times)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">int</span> num = times;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">int</span> num2 = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> ((num &gt; num2) ^ <span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> List&lt;Point3d&gt;.Nil_uniq;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Point3d pt = Commands.nextBoundaryPoint(<span style="COLOR: blue">this</span>.cur, start, <span style="COLOR: blue">this</span>.doTrace);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: blue">new</span> List&lt;Point3d&gt;._Cons(pt, FastFunc&lt;Point3d, <span style="COLOR: blue">int</span>&gt;.InvokeFast2&lt;List&lt;Point3d&gt;&gt;(<span style="COLOR: blue">this</span>, pt, times - 1));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">catch</span> (<span style="COLOR: blue">object</span> obj1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Exception exn = (Exception) obj1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">string</span> message = exn.Message;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">string</span> b = <span style="COLOR: maroon">&quot;Could not get point&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (<span style="COLOR: blue">string</span>.Equals(message, b))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> FastFunc&lt;Point3d, <span style="COLOR: blue">int</span>&gt;.InvokeFast2&lt;List&lt;Point3d&gt;&gt;(<span style="COLOR: blue">this</span>, start, times);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> Operators.failwith&lt;List&lt;Point3d&gt;&gt;(exn.Message);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>This one is actually only recurses when we catch an exception we expect to occur (and actually have thrown ourselves elsewhere in the code) occasionally during run-time. A better implementation - one that F#'s compiler is able to tail call optimise and meets my aesthetic requirements - is here:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// An iterative function to get the points</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// for our path</span></p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> definePath start times =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> pts = <span style="COLOR: blue">new</span> Point3dCollection()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; pts.Add(start) |&gt; ignore</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">mutable</span> i = 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">while</span> i &lt; times <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> pt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; nextBoundaryPoint</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cur pts.[pts.Count-1] doTrace runAsync</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pts.Add(pt) |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;i &lt;- i + 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">with</span> exn <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> exn.Message &lt;&gt; <span style="COLOR: maroon">&quot;Could not get point&quot;</span> <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; failwith exn.Message</p>

<br/>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Seq.untyped_to_list pts</p></div>

<p>I won't bother showing this function's C# equivalent, as determined by Reflector, you should just trust me that it's better. :-)</p>

<p>I did change the implementation slightly, so we no longer add our first vertex when we create the polyline - we simply add it to the array of vertices, which is cleaner - and we also have to pass a start index of 0 into the definePath function, rather than 1. Very minor changes, but they actually do make the code slightly simpler.</p>

<p>Anyway, this kind of analysis and control is made possible by the fact that F# compiles to IL, just like the other .NET languages. This allows us to take advantage of existing tools for code analysis &amp; obfuscation, and ultimately give us better, more consistent control than we would get from a functional programming implementation outside of .NET.</p>
