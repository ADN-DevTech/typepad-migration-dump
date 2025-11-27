---
layout: "post"
title: "Sorting lists of AutoCAD objects using LINQ &ndash; Part 1"
date: "2016-02-24 14:08:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "LINQ"
  - "Object properties"
  - "Selection"
original_url: "https://www.keanw.com/2016/02/sorting-lists-of-autocad-objects-using-linq-part-1.html "
typepad_basename: "sorting-lists-of-autocad-objects-using-linq-part-1"
typepad_status: "Publish"
---

<p>I received this interesting question by email from Henrik Ericson, last week.</p>
<blockquote>
<p><em>Is it possible to sort the selected objects (only 3dFaces in my case) in a SelectionSet by their layers? </em></p>
<p><em>I want to process all the objects, one layer at a time.</em></p>
<p><em>In other words, first all objects on layer A, and then all objects on layer B and then C and so on. In my case it isn&#39;t necessary to process the layers alphabetically.</em></p>
</blockquote>
<p>It’s especially interesting as I’d just been thinking of approaches for sorting toolbars based on an index property, to get the code in <a href="http://through-the-interface.typepad.com/through_the_interface/2016/02/disabling-autocads-toolbars-using-net.html" target="_blank">this post</a> to work properly. Henrik’s question gave me a solid reason to think the problem through and create a solution.</p>
<p>We’re going to take a look at the problem in three stages. In today’s post we’re going to look at a simple way to address this by using LINQ to sort on an object’s layer and (for bonus points) on its object type. As you’ll see the code for the two commands is very similar, so in the next post we’re going to abstract the implementation away to create an extension method that can sort based on any string property.</p>
<p>In the last (planned) post, we’re going to abstract things away even further, creating a static template class that will allow us (hopefully) to sort based on any object property, such as the color index of an object’s layer. Which should be pretty cool. :-)</p>
<p>To avoid a lot of hassle with transactions or manual open/close, we’re going to make heavy use of <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/dynamic-net-in-autocad-2013.html" target="_blank">dynamic .NET</a>: we’re going to declare ObjectId types as dynamic, so that we can simply access the object’s properties directly. Or those of its layer, for that matter. We’ll then add entries to a dictionary: the key will be the ObjectId and the value will be the property we want to sort on. Then we can just call LINQ’s OrderBy() extension method, passing in a selector lambda from which we return the value to sort on (the value of the key-value pair, in our case).</p>
<p>We can then simply loop through the sorted results, printing the contents to the command-line.</p>
<p>So here’s the basic implementation, showing how we can use LINQ to sort an ObjectId array and let us process the objects in sequence:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Linq;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> ProcessObjectsInOrder</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;OBL&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.UsePickSet)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ObjectsByLayer()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Select the objects to sort</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> psr = ed.GetSelection();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (psr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;ll sort them based on a string value (the layer name)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> map = <span style="color: blue;">new</span> <span style="color: #2b91af;">Dictionary</span>&lt;<span style="color: #2b91af;">ObjectId</span>, <span style="color: blue;">string</span>&gt;();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">dynamic</span> id <span style="color: blue;">in</span> psr.Value.GetObjectIds())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map.Add(id, id.Layer);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sorted = map.OrderBy(kv =&gt; kv.Value);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Print them in order to the command-line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> item <span style="color: blue;">in</span> sorted)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nObject {0} on layer {1}&quot;</span>, item.Key, item.Value);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;OBT&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.UsePickSet)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ObjectsByType()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Select the objects to sort</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> psr = ed.GetSelection();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (psr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Sort them based on a string value (the class name)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> map = <span style="color: blue;">new</span> <span style="color: #2b91af;">Dictionary</span>&lt;<span style="color: #2b91af;">ObjectId</span>, <span style="color: blue;">string</span>&gt;();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">dynamic</span> id <span style="color: blue;">in</span> psr.Value.GetObjectIds())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map.Add(id, id.ObjectClass.Name);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sorted = map.OrderBy(kv =&gt; kv.Value);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Print them in order to the command-line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> item <span style="color: blue;">in</span> sorted)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nObject {0} of type {1}&quot;</span>, item.Key, item.Value);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>The OBL and OBT commands sort based on layer and type, respectively. Here’s how they work on a simple drawing:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1a14ad6970c-pi" target="_blank"><img alt="Sorting based on object properties" height="479" src="/assets/image_678441.jpg" style="float: none; margin: 30px auto; display: block;" title="Sorting based on object properties" width="500" /></a></p>
<p>As mentioned already, there’s a great deal of commonality between the two commands. We’re going to take a shot at factoring that away, in the next post, creating an extension method to sort an array of ObjectIds.</p>
