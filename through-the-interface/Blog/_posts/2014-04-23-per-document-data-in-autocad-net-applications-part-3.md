---
layout: "post"
title: "Per-document data in AutoCAD .NET applications &ndash; Part 3"
date: "2014-04-23 13:03:41"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Documents"
  - "Runtime"
original_url: "https://www.keanw.com/2014/04/per-document-data-in-autocad-net-applications-part-3.html "
typepad_basename: "per-document-data-in-autocad-net-applications-part-3"
typepad_status: "Publish"
---

<p>I almost named this post to make it clear it’s about a new API capability in AutoCAD 2015, but then I wouldn’t have had the slightly perverse satisfaction of resurrecting a series of posts after 7.5 years. Here are the <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/perdocument_dat_1.html" target="_blank">first</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/perdocument_dat_2.html" target="_blank">parts</a> in this series, in case you’ve forgotten them (and you’d be forgiven for doing so, after all ;-).</p>
<p>This post is introducing the new PerDocumentClassAttribute capability provided in AutoCAD 2015’s .NET API. We’re going to look at it over two posts: the first showing how it can be used with your own “manager” class to link the data to a Document and the second will show how the Document’s UserData property might also be used to take care of that.</p>
<p><em>[Thanks to the ADN team for providing the sample I used as a basis for today’s post (it may well have come from the AutoCAD team before then, but I first saw it on the ADN web-site).]</em></p>
<p>So what does this attribute do? Let’s see what the documentation says:</p>
<blockquote>
<p><em>This custom attribute class is used to mark a type so that an instance of that type will be instantiated for each document open/opened in AutoCAD. </em></p>
<p><em>An application may designate as many types as desired with this attribute. When the application is loaded into AutoCAD, a new instance of the type will be instantiated for each document currently open and a reference to the document, and a new instance will be instantiated for each document opened thereafter. When a document is closed, the associated type instance will be disposed if it derives from IDisposable. </em></p>
<p><em>The type associated with a PerDocumentClass attribute must provide either a public constructor that takes a Document argument, or a public static Create method that takes a Document argument and returns an instance of the type. If the Create method exists, it will be used, otherwise the constructor will be used. The Document that the type instance is being created for will be passed as the Document argument so that the type instance knows which Document it is associated with.</em></p>
</blockquote>
<p>To paraphrase, this attribute provides a way to specify that a particular type of object needs to be created – and optionally disposed of – for any Documents loaded in the editor (currently loaded Documents have objects created as the application loads, additional objects will be instantiated as Documents get opened or created).</p>
<p>It’s important to note that the mechanism doesn’t specify how the data should be held in memory: as we’ll see in this post and the next, you can choose to manage this via your own manager class or a Document’s UserData map.</p>
<p>To use the mechanism you need to define your class and point to it from an assembly-level attribute. Here’s some C# code showing how a DateTime can be associated with each Document via our custom PerDocData class:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// The all-important attribute telling AutoCAD to instantiate</span></p>
<p style="margin: 0px;"><span style="color: green;">// the PerDocData class for each open or new document</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">[<span style="color: blue;">assembly</span>: <span style="color: #2b91af;">PerDocumentClass</span>(<span style="color: blue;">typeof</span>(PerDocSample.<span style="color: #2b91af;">PerDocData</span>))]</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> PerDocSample</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// A simple command to write the contents of our per-document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// data to the command-line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;PPDD&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> PrintPerDocData()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> perDocData = <span style="color: #2b91af;">PerDocDataManager</span>.GetData(doc);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.Editor.WriteMessage(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; perDocData == <span style="color: blue;">null</span> ?</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;\nNo user data found.&quot;</span> :</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; perDocData.OpenDateTime.ToString()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// We need a manager class that will keep our data mapped to</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// a document. This class can optionally care about when</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// documents are closed via the Document&#39;s BeginDocumentClose</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// event</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">PerDocDataManager</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create a mapping between documents and our data objects</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">static</span> <span style="color: #2b91af;">Dictionary</span>&lt;<span style="color: #2b91af;">Document</span>, <span style="color: #2b91af;">PerDocData</span>&gt; _data =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">Dictionary</span>&lt;<span style="color: #2b91af;">Document</span>, <span style="color: #2b91af;">PerDocData</span>&gt;();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// We call this static method from our class&#39; constructor,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// so we get added to the map</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> AddPerDoc(<span style="color: #2b91af;">Document</span> doc, <span style="color: #2b91af;">PerDocData</span> att)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _data.Add(doc, att);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.BeginDocumentClose +=</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (s, e) =&gt;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> d = (<span style="color: #2b91af;">Document</span>)s;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (_data.ContainsKey(d))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _data.Remove(d);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// A static method to return the data associated with a</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Document</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">PerDocData</span> GetData(<span style="color: #2b91af;">Document</span> doc)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> _data.ContainsKey(doc) ? _data[doc] : <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Our per-document data class. This will get instanciated for</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// each existing or new document: we get the creation</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// notification via either the static Create() method or via</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// the public constructor that takes a Document argument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">PerDocData</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// We will store the time the document was opened or created</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">DateTime</span> _openDateTime;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Provide a public read-only property for that</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">DateTime</span> OpenDateTime</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _openDateTime; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Public constructor taking a Document</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> PerDocData(<span style="color: #2b91af;">Document</span> doc)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; _openDateTime = <span style="color: #2b91af;">DateTime</span>.Now;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">PerDocDataManager</span>.AddPerDoc(doc, <span style="color: blue;">this</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Static Create method: this is the first approach tried</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// (to differentiate we&#39;re adding an hour to the current</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// time, so it&#39;s clear this method is being called)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">PerDocData</span> Create(<span style="color: #2b91af;">Document</span> doc)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pdd = <span style="color: blue;">new</span> <span style="color: #2b91af;">PerDocData</span>(doc);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pdd._openDateTime += <span style="color: #2b91af;">TimeSpan</span>.FromHours(1);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> pdd;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>One change I made to the sample was to flesh out the two approaches for creating new instances of your tagged class. The first is to provide a Create() factory function that returns a new instance, the second is to provide a constructor: both of these take a Document as an argument. I’ve implemented the Create() function to call through to the constructor – which sets the DateTime property to the current time – but then to add an hour to the time before returning it. This lets us see that the Create() method is called if it is found (you can comment simply it out to see the behaviour when the constructor is used instead).</p>
<p>Otherwise the sample is pretty much the same as the one on the ADN site. The separate manager is used to associate a Document with the created instance of our custom class as well as to retrieve the object associated with a particular document. If your class derives from IDisposable then its Dispose() method will be called when the associated Document gets destroyed (this isn’t a feature we need to implement in our manager, it happens automagically).</p>
<p>To try the code out, NETLOAD the module that includes the code into AutoCAD and run the PPDD command in a number of different documents, both existing and new.</p>
<p>In the next post, we’ll take a look at swapping out the custom manager to use the built-in UserData capability.</p>
