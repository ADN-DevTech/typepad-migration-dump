---
layout: "post"
title: "Adding a web-page as a document tab in AutoCAD 2015 using .NET"
date: "2014-04-28 14:00:00"
author: "Kean Walmsley"
categories:
  - "Async"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Documents"
  - "HTML"
  - "JavaScript"
  - "SaaS"
  - "Web/Tech"
original_url: "https://www.keanw.com/2014/04/adding-a-web-page-as-a-document-tab-in-autocad-2015-using-net.html "
typepad_basename: "adding-a-web-page-as-a-document-tab-in-autocad-2015-using-net"
typepad_status: "Publish"
---

<p>This was a fun one. It was really only a single line of code but I decided to embellish it a bit to make it a bit more useful.</p>
<p>The “task” I set myself was to open a web-page – this blog, in fact – inside AutoCAD as an MDI child. AutoCAD can now host web-pages directly inside its MDI frame – the New Tab Page is a great example of that – and this mechanism can be used for external applications, too. You can also load non-HTML documents into the frame, but it’s quick and easy to use HTML.</p>
<p>The “embellishments” consisted of a couple of things, really…</p>
<p>Firstly, I wanted to check whether the chosen URL was actually loadable (not just valid, but loadable) before having AutoCAD load it. If we don’t do that then we risk a rather inelegant 404 in AutoCAD’s main frame. We check this both by validating the URL and <a href="http://stackoverflow.com/questions/924679/c-sharp-how-can-i-check-if-a-url-exists-is-valid" target="_blank">by issuing a “HEAD” request</a> – rather than a full “GET” – as this is significantly cheaper.</p>
<p>Secondly, I wanted to do all this asynchronously, so AutoCAD wouldn’t risk blocking while doing it. As AutoCAD 2015 is using .NET 4.5, it’s safe to assume that async/await is available to applications using the new APIs, so we used that to keep things nice and clean.</p>
<p>Here’s the C# code that loads this blog into AutoCAD:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Net;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Threading.Tasks;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> NewDocumentWindow</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// To check whether a page exists, we can use a HEAD request</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// rather than a full GET. Derive a custom client to do that</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">class</span> <span style="color: #2b91af;">HeadClient</span> : <span style="color: #2b91af;">WebClient</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">protected</span> <span style="color: blue;">override</span> <span style="color: #2b91af;">WebRequest</span> GetWebRequest(<span style="color: #2b91af;">Uri</span> address)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> req = <span style="color: blue;">base</span>.GetWebRequest(address);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (req.Method == <span style="color: #a31515;">&quot;GET&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; req.Method = <span style="color: #a31515;">&quot;HEAD&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> req;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Asynchronous helper that checks whether a URL exists</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// (i.e. that the URL is valid and can be loaded)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">async</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Task</span>&lt;<span style="color: blue;">bool</span>&gt; PageExists(<span style="color: blue;">string</span> url)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// First check whether the URL is valid</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Uri</span> uriResult;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; !<span style="color: #2b91af;">Uri</span>.TryCreate(url, <span style="color: #2b91af;">UriKind</span>.Absolute, <span style="color: blue;">out</span> uriResult) ||</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; uriResult.Scheme != <span style="color: #2b91af;">Uri</span>.UriSchemeHttp</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Then we try to peform a HEAD request on the page</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// (a WebException will be fired if it doesn&#39;t exist)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span>(<span style="color: blue;">var</span> client = <span style="color: blue;">new</span> <span style="color: #2b91af;">HeadClient</span>())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">await</span> client.DownloadStringTaskAsync(url);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (<span style="color: #2b91af;">WebException</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;BLOG&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">async</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> OpenBlog()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> url =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// As we&#39;re calling an async function, we need to await</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// (and mark the command itself as async)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">await</span> PageExists(url))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Now that we&#39;ve validated the URL, we can call the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// new API in AutoCAD 2015 to load our page</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Application</span>.DocumentWindowCollection.AddDocumentWindow(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;Kean&#39;s blog&quot;</span>, <span style="color: blue;">new</span> System.<span style="color: #2b91af;">Uri</span>(url)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Print a helpful message if the URL wasn&#39;t loadable</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;\nCould not load url: \&quot;{0}\&quot;.&quot;</span>, url</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>When we run the BLOG command, here’s what we see:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a73db41b0d970d-pi" target="_blank"><img alt="AutoCAD 2015 hosting this blog" border="0" height="363" src="/assets/image_302819.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="AutoCAD 2015 hosting this blog" width="474" /></a></p>
<p>To make this application more interesting, we could use AutoCAD’s JavaScript API to execute commands etc. inside AutoCAD directly from the hosted web-page (just as the New Tab Page does). This opens a host of interesting possibilities from an application development perspective!</p>
