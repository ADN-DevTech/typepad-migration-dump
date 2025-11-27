---
layout: "post"
title: "Creating a layer group inside AutoCAD using .NET"
date: "2008-09-24 06:54:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Object properties"
original_url: "https://www.keanw.com/2008/09/creating-a-laye.html "
typepad_basename: "creating-a-laye"
typepad_status: "Publish"
---

<p>The following question came in as a comment on <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/adding-and-remo.html">this previous post</a>:</p>
<blockquote>
<p><em>Your example above shows how to create a property type filter, how do you add layers to a group type filter (LayerGroup class)? I can create the group filter but can&#39;t figure out how to add layers to the group. The filterexpression method is available but doesn&#39;t seem to work (at least not like with the LayerFilter object)</em></p></blockquote>
<p>To tackle this, let&#39;s start by looking at the documentation on the LayerGroup class in the .NET Reference (currently part of the ObjectARX Reference):</p>
<blockquote>
<p>LayerGroup is derived from LayerFilter and serves as the access to layer group filters. It allows the client to specify and retrieve a set of layer IDs. The filter() method returns true if the object ID of the given layer is contained in the set of layer IDs for the LayerGroup. </p>
<p>Specifying the filter criteria is done solely by using LayerId. LayerGroup doesn&#39;t use a filter expression string, so FilterExpression and FilterExpressionTree return a null pointer. </p>
<p>The recommended way of identifying LayerGroup filters in a group of layer filters is to query the IsIdFilter property, which returns true for an LayerGroup.</p></blockquote>
<p>So, at the root of the question, we need to create a LayerGroup and add the ObjectIds of the LayerTableRecords we wish to include in the group to its LayerIds property.</p>
<p>I started by taking the code in the post referred to above and extended it to contain a new CLG command to Create a Layer Group. This command lists the available layers for the user to select from, and creates a layer group containing the selected layers.</p>
<p>Here&#39;s the updated C# code - pay particular attention to the new CLG command, of course:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.LayerManager;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Collections.Generic;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> LayerFilters</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LLFS&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ListLayerFilters()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// List the nested layer filters</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterCollection</span></span><span style="LINE-HEIGHT: 140%"> lfc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; db.LayerFilters.Root.NestedFilters;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> i = 0; i &lt; lfc.Count; ++i)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%"> lf = lfc[i];</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\n{0} - {1} (can{2} be deleted)&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; i + 1,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; lf.Name,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (lf.AllowDelete ? </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;&quot;</span></span><span style="LINE-HEIGHT: 140%"> : </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;not&quot;</span></span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;CLFS&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> CreateLayerFilters()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the existing layer filters</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (we will add to them and set them back)</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterTree</span></span><span style="LINE-HEIGHT: 140%"> lft =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; db.LayerFilters;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterCollection</span></span><span style="LINE-HEIGHT: 140%"> lfc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; lft.Root.NestedFilters;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create three new layer filters</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%"> lf1 = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lf1.Name = </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Unlocked Layers&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lf1.FilterExpression = </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LOCKED==\&quot;False\&quot;&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%"> lf2 = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lf2.Name = </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;White Layers&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lf2.FilterExpression = </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;COLOR==\&quot;7\&quot;&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%"> lf3 = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lf3.Name = </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Visible Layers&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lf3.FilterExpression =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;OFF==\&quot;False\&quot; AND FROZEN==\&quot;False\&quot;&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add them to the collection</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lfc.Add(lf1);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lfc.Add(lf2);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; lfc.Add(lf3);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Set them back on the Database</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; db.LayerFilters = lft;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// List the layer filters, to see the new ones</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; ListLayerFilters();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Exception</span></span><span style="LINE-HEIGHT: 140%"> ex)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nException: {0}&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ex.Message</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;CLG&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> CreateLayerGroup()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// A list of the layers&#39; names &amp; IDs contained</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// in the current database, sorted by layer name</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SortedList</span></span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">, </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span></span><span style="LINE-HEIGHT: 140%">&gt; ld =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SortedList</span></span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">, </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span></span><span style="LINE-HEIGHT: 140%">&gt;();</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// A list of the selected layers&#39; IDs</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectIdCollection</span></span><span style="LINE-HEIGHT: 140%"> lids =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectIdCollection</span></span><span style="LINE-HEIGHT: 140%">();</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start by populating the list of names/IDs</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// from the LayerTable</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span></span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerTable</span></span><span style="LINE-HEIGHT: 140%"> lt =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerTable</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db.LayerTableId,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span></span><span style="LINE-HEIGHT: 140%"> lid </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> lt)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerTableRecord</span></span><span style="LINE-HEIGHT: 140%"> ltr =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerTableRecord</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; lid,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ld.Add(ltr.Name, lid);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// Display a numbered list of the available layers</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nLayers available for group:&quot;</span></span><span style="LINE-HEIGHT: 140%">);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> i = 1;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">KeyValuePair</span></span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">,</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span></span><span style="LINE-HEIGHT: 140%">&gt; kv </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> ld)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\n{0} - {1}&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; i++,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; kv.Key</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// We will ask the user to select from the list</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptIntegerOptions</span></span><span style="LINE-HEIGHT: 140%"> pio =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptIntegerOptions</span></span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nEnter number of layer to add: &quot;</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;pio.LowerLimit = 1;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;pio.UpperLimit = ld.Count;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;pio.AllowNone = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// And will do so in a loop, waiting for</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// Escape or Enter to terminate</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptIntegerResult</span></span><span style="LINE-HEIGHT: 140%"> pir;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">do</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Select one from the list</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; pir = ed.GetInteger(pio);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (pir.Status == </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStatus</span></span><span style="LINE-HEIGHT: 140%">.OK)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the layer&#39;s name</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> ln =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ld.Keys[pir.Value-1];</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And then its ID</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span></span><span style="LINE-HEIGHT: 140%"> lid;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ld.TryGetValue(ln, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> lid);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add the layer&#39;d ID to the list, is it&#39;s not</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// already on it</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (lids.Contains(lid))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nLayer \&quot;{0}\&quot; has already been selected.&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ln</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;lids.Add(lid);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nAdded \&quot;{0}\&quot; to selected layers.&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ln</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;} </span><span style="LINE-HEIGHT: 140%; COLOR: blue">while</span><span style="LINE-HEIGHT: 140%"> (pir.Status == </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStatus</span></span><span style="LINE-HEIGHT: 140%">.OK);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now we&#39;ve selected our layers, let&#39;s create the group</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (lids.Count &gt; 0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the existing layer filters</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (we will add to them and set them back)</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterTree</span></span><span style="LINE-HEIGHT: 140%"> lft =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db.LayerFilters;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterCollection</span></span><span style="LINE-HEIGHT: 140%"> lfc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;lft.Root.NestedFilters;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create a new layer group</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerGroup</span></span><span style="LINE-HEIGHT: 140%"> lg = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerGroup</span></span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; lg.Name = </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;My Layer Group&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add our layers&#39; IDs to the list</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ObjectId</span></span><span style="LINE-HEIGHT: 140%"> id </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> lids)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;lg.LayerIds.Add(id);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add the group to the collection</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; lfc.Add(lg);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Set them back on the Database</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; db.LayerFilters = lft;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\n\&quot;{0}\&quot; group created containing {1} layers.\n&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;lg.Name,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;lids.Count</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// List the layer filters, to see the new group</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ListLayerFilters();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Exception</span></span><span style="LINE-HEIGHT: 140%"> ex)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nException: {0}&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ex.Message</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;DLF&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> DeleteLayerFilter()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;ListLayerFilters();</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the existing layer filters</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (we will add to them and set them back)</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterTree</span></span><span style="LINE-HEIGHT: 140%"> lft =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; db.LayerFilters;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilterCollection</span></span><span style="LINE-HEIGHT: 140%"> lfc =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; lft.Root.NestedFilters;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Prompt for the index of the filter to delete</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptIntegerOptions</span></span><span style="LINE-HEIGHT: 140%"> pio =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%">&#0160;</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptIntegerOptions</span></span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\n\nEnter index of filter to delete&quot;</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; pio.LowerLimit = 1;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; pio.UpperLimit = lfc.Count;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptIntegerResult</span></span><span style="LINE-HEIGHT: 140%"> pir =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.GetInteger(pio);</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the selected filter</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">LayerFilter</span></span><span style="LINE-HEIGHT: 140%"> lf = lfc[pir.Value - 1];</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If it&#39;s possible to delete it, do so</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (!lf.AllowDelete)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;</span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nLayer filter cannot be deleted.&quot;</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; lfc.Remove(lf);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; db.LayerFilters = lft;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ListLayerFilters();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af"><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Exception</span></span><span style="LINE-HEIGHT: 140%"> ex)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; </span><span style="COLOR: #a31515"><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nException: {0}&quot;</span></span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ex.Message</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;&#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160;&#0160; &#0160;}</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>Here&#39;s what happens when we run the code on a drawing containing a number of layers (all of which have the default properties for new layers):</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Command: <span style="COLOR: #ff0000">CLG</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Layers available for group:</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">1 - 0</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">2 - Layer1</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">3 - Layer2</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">4 - Layer3</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">5 - Layer4</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">6 - Layer5</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">7 - Layer6</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">8 - Layer7</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">9 - Layer8</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">10 - Layer9</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">11 - Test1</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">12 - Test2</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">13 - Test3</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">14 - Test4</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">15 - Test5</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add: <span style="COLOR: #ff0000">2</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Added &quot;Layer1&quot; to selected layers.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add: <span style="COLOR: #ff0000">4</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Added &quot;Layer3&quot; to selected layers.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add: <span style="COLOR: #ff0000">6</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Added &quot;Layer5&quot; to selected layers.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add: <span style="COLOR: #ff0000">8</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Added &quot;Layer7&quot; to selected layers.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add: <span style="COLOR: #ff0000">11</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Added &quot;Test1&quot; to selected layers.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add: <span style="COLOR: #ff0000">15</span></span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Added &quot;Test5&quot; to selected layers.</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Enter number of layer to add:</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;My Layer Group&quot; group created containing 6 layers.</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">1 - My Layer Group (can be deleted)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">2 - All Used Layers (cannot be deleted)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">3 - Unreconciled New Layers (cannot be deleted)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">4 - Viewport Overrides (cannot be deleted)</span></p></div>
<p>I&#39;ve hard-coded the name of the new layer group to be &quot;My Layer Group&quot;. It&#39;s a trivial exercise to modify the code to ask the user to enter a name, each time.</p>
<p>Here is how our new group looks in AutoCAD&#39;s Layer dialog:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Layer%20Group.png"><img alt="Layer Group" border="0" height="188" src="/assets/Layer%20Group_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="459" /></a></p>
<p><strong><em>Update</em></strong></p>
<p>See <a href="http://through-the-interface.typepad.com/through_the_interface/2010/01/creating-a-nested-layer-group-inside-autocad-using-net.html">this post</a> for a tidied-up and extended version of the above code.</p>
