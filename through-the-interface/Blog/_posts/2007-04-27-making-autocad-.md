---
layout: "post"
title: "Making AutoCAD objects annotative using .NET"
date: "2007-04-27 03:01:25"
author: "Kean Walmsley"
categories:
  - "Annotation scaling"
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2007/04/making_autocad_.html "
typepad_basename: "making_autocad_"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2007/04/adding_a_new_an.html">the last post</a> we looked at how to add a new annotative scale to an AutoCAD drawing.</p>
<p>In this post we&#39;ll look at what&#39;s needed to make an object annotative - providing it&#39;s of a type that supports annotation scaling, of course. Once again, this post is based on functionality introduced in AutoCAD 2008.</p>
<p>I hit my head against the problem for a while, having tried my best to convert the technique shown in the AnnotationScaling ObjectARX sample (which uses a protocol extension to access objects stored in an annotative entity&#39;s extension dictionary) to .NET. I finally ended up asking our Engineering team: Ravi Pothineni came back with some code that uses an internal assembly (but one that ships with AutoCAD) to do this. Apparently this will become standard functionality in a future release of AutoCAD - it&#39;ll just be available directly from DBObject, rather than being in a separate &quot;helper&quot; - but in the meantime you will have to use slightly more cumbersome code, such as that shown below, and add a reference to AcMgdInternal.dll for it to work. As indicated by the name, this functionality is unsupported and to be used at your own risk.</p>
<p>Here&#39;s the C# code - the &quot;ADS&quot; command is basically the one shown previously (renamed from &quot;AS&quot;) and the &quot;ATS&quot; function is the new command that makes an object annotative, attaching annotation scales to it:</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Internal;</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> AnnotationScaling</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;ADS&quot;</span>)]</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> addScale()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Document</span> doc =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Database</span> db = doc.Database;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">try</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">ObjectContextManager</span> ocm =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; db.ObjectContextManager;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">if</span> (ocm != <span style="COLOR: blue">null</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Now get the Annotation Scaling context collection</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// (named ACDB_ANNOTATIONSCALES_COLLECTION)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">ObjectContextCollection</span> occ =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ocm.GetContextCollection(<span style="COLOR: maroon">&quot;ACDB_ANNOTATIONSCALES&quot;</span>);</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (occ != <span style="COLOR: blue">null</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Create a brand new scale context</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">AnnotationScale</span> asc = <span style="COLOR: blue">new</span> <span style="COLOR: teal">AnnotationScale</span>();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;asc.Name = <span style="COLOR: maroon">&quot;MyScale 1:28&quot;</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;asc.PaperUnits = 1;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;asc.DrawingUnits = 28;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Add it to the drawing&#39;s context collection</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;occ.AddContext(asc);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">catch</span> (System.<span style="COLOR: teal">Exception</span> ex)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(ex.ToString());</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;ATS&quot;</span>)]</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> attachScale()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Document</span> doc =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Database</span> db = doc.Database;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContextManager</span> ocm =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; db.ObjectContextManager;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContextCollection</span> occ =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; ocm.GetContextCollection(<span style="COLOR: maroon">&quot;ACDB_ANNOTATIONSCALES&quot;</span>);</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Transaction</span> tr =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; doc.TransactionManager.StartTransaction();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">using</span> (tr)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">PromptEntityOptions</span> opts =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptEntityOptions</span>(<span style="COLOR: maroon">&quot;\nSelect entity: &quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; opts.SetRejectMessage(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: maroon">&quot;\nEntity must support annotation scaling.&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; opts.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">DBText</span>), <span style="COLOR: blue">false</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; opts.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">MText</span>), <span style="COLOR: blue">false</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; opts.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">Dimension</span>), <span style="COLOR: blue">false</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; opts.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">Leader</span>), <span style="COLOR: blue">false</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; opts.AddAllowedClass(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">Hatch</span>), <span style="COLOR: blue">false</span>);</p><br />
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">PromptEntityResult</span> per = ed.GetEntity(opts);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">if</span> (per.ObjectId != <span style="COLOR: teal">ObjectId</span>.Null)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">DBObject</span> obj =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.GetObject(per.ObjectId, <span style="COLOR: teal">OpenMode</span>.ForRead);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (obj != <span style="COLOR: blue">null</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;obj.UpgradeOpen();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;obj.Annotative = <span style="COLOR: teal">AnnotativeStates</span>.True;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContexts</span>.AddContext(obj, occ.GetContext(<span style="COLOR: maroon">&quot;1:1&quot;</span>));</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContexts</span>.AddContext(obj, occ.GetContext(<span style="COLOR: maroon">&quot;1:2&quot;</span>));</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContexts</span>.AddContext(obj, occ.GetContext(<span style="COLOR: maroon">&quot;1:10&quot;</span>));</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContext</span> oc = occ.GetContext(<span style="COLOR: maroon">&quot;MyScale 1:28&quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (oc != <span style="COLOR: blue">null</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">ObjectContexts</span>.AddContext(obj, oc);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>
<p>You&#39;ll notice that if you run the ADS command before ATS, you&#39;ll also get the newly-added annotation scale in the selected object&#39;s list:</p>
<p><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/04/26/attached_annotation_scale.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=381,height=314,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Attached_annotation_scale" border="0" height="247" src="/assets/attached_annotation_scale.png" title="Attached_annotation_scale" width="300" /></a></p>
<p><strong><em></em></strong>&#0160;</p>
<p><strong><em>Update:</em></strong></p>
<p>From AutoCAD 2009 onwards, it&#39;s now possible to modify annotation scales on an object directly without&#0160;relying on&#0160;unsupported funcitonality in acmgdinternal.dll.</p>
<p>Here&#39;s the modified C# code:</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> AnnotationScaling</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;ADS&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> addScale()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">try</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectContextManager</span><span style="LINE-HEIGHT: 140%"> ocm =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; db.ObjectContextManager;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (ocm != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Now get the Annotation Scaling context collection</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// (named ACDB_ANNOTATIONSCALES_COLLECTION)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectContextCollection</span><span style="LINE-HEIGHT: 140%"> occ =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ocm.GetContextCollection(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;ACDB_ANNOTATIONSCALES&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (occ != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Create a brand new scale context</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AnnotationScale</span><span style="LINE-HEIGHT: 140%"> asc = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AnnotationScale</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; asc.Name = </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;MyScale 1:28&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; asc.PaperUnits = 1;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; asc.DrawingUnits = 28;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Add it to the drawing&#39;s context collection</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; occ.AddContext(asc);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">catch</span><span style="LINE-HEIGHT: 140%"> (System.</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Exception</span><span style="LINE-HEIGHT: 140%"> ex)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(ex.ToString());</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;ATS&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> attachScale()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectContextManager</span><span style="LINE-HEIGHT: 140%"> ocm =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; db.ObjectContextManager;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectContextCollection</span><span style="LINE-HEIGHT: 140%"> occ =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ocm.GetContextCollection(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;ACDB_ANNOTATIONSCALES&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span><span style="LINE-HEIGHT: 140%"> opts =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nSelect entity: &quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.SetRejectMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nEntity must support annotation scaling.&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DBText</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">MText</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Dimension</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Leader</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Table</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; opts.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Hatch</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityResult</span><span style="LINE-HEIGHT: 140%"> per = ed.GetEntity(opts);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (per.ObjectId != </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectId</span><span style="LINE-HEIGHT: 140%">.Null)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DBObject</span><span style="LINE-HEIGHT: 140%"> obj =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tr.GetObject(per.ObjectId, </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (obj != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; obj.UpgradeOpen();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; obj.Annotative = </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AnnotativeStates</span><span style="LINE-HEIGHT: 140%">.True;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; obj.AddContext(occ.GetContext(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;1:1&quot;</span><span style="LINE-HEIGHT: 140%">));</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; obj.AddContext(occ.GetContext(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;1:2&quot;</span><span style="LINE-HEIGHT: 140%">));</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; obj.AddContext(occ.GetContext(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;1:10&quot;</span><span style="LINE-HEIGHT: 140%">));</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectContext</span><span style="LINE-HEIGHT: 140%"> oc = occ.GetContext(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;MyScale 1:28&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (oc != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; obj.AddContext(oc);&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
