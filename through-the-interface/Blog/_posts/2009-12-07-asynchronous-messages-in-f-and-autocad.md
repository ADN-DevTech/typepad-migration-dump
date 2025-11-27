---
layout: "post"
title: "Asynchronous messages in F# and AutoCAD"
date: "2009-12-07 08:48:22"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Concurrent programming"
  - "F#"
  - "XML"
original_url: "https://www.keanw.com/2009/12/asynchronous-messages-in-f-and-autocad.html "
typepad_basename: "asynchronous-messages-in-f-and-autocad"
typepad_status: "Publish"
---

<p>I’ve now crossed the international date line (giving up a big portion of my weekend, but that’s life) and landed in Tokyo. Tomorrow I head on to Seoul and then to Beijing for the end of the week. In many ways a change of pace from the week in Vegas, but in other ways it’s more of the same (fun, that is :-).</p>
<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2009/11/using-erlang-style-message-passing-from-f-to-coordinate-asynchronous-tasks-in-autocad.html">this previous post</a> we looked at some code to retrieve and process RSS information from various blogs using an agent-based message passing architecture. The code wasn’t completely asynchronous or parallelised, though, as we fired off each message to start the download synchronously (although the “processing” would have launched quite quickly, yielding control back to the loop which would then launch other downloads). This post shows an even more asynchronous approach, as it fires off the downloads in parallel. We’ll see if it makes much difference. :-)</p>
<p>Here’s the updated F# code, with the new/modified lines in <font color="#ff0000">red</font>. If you’d like a complete version of the application, with the <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/turning-autocad.html">four</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/harnessing-f-as.html">different</a>&#0160;<a href="http://through-the-interface.typepad.com/through_the_interface/2009/11/using-erlang-style-message-passing-from-f-to-coordinate-asynchronous-tasks-in-autocad.html">implementations</a> we’ve seen, you can <a href="http://through-the-interface.typepad.com/files/rss.zip">get it from here</a>.</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 1</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Declare a specific namespace and module name</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 2</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160;&#0160; 3</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">module</span><span style="LINE-HEIGHT: 140%"> AgentAsyncRssReader.Commands</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 4</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 5</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Import managed namespaces</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 6</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 7</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 8</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 9</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 10</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 11</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> System.Xml</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 12</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> System.IO</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 13</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> System.Net</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 14</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Microsoft.FSharp.Control.WebExtensions</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 15</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 16</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// The RSS feeds we wish to get. The first two values are</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 17</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// only used if our code is not able to parse the feed&#39;s XML</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 18</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 19</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> feeds =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 20</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; [ (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;Through the Interface&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 21</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 22</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://through-the-interface.typepad.com/&quot;</span><span style="LINE-HEIGHT: 140%"> +</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 23</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;through_the_interface/rss.xml&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 24</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 25</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;Don Syme&#39;s F# blog&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 26</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 27</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/rss.xml&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 28</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 29</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;Shaan Hurley&#39;s Between the Lines&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 30</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 31</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines/rss.xml&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 32</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 33</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;Scott Sheppard&#39;s It&#39;s Alive in the Lab&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 34</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://blogs.autodesk.com/labs&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 35</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://labs.blogs.com/its_alive_in_the_lab/rss.xml&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 36</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 37</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;Volker Joseph&#39;s Beyond the Paper&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 38</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://blogs.autodesk.com/beyond_the_paper&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 39</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;http://dwf.blogs.com/beyond_the_paper/rss.xml&quot;</span><span style="LINE-HEIGHT: 140%">) ]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 40</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 41</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Fetch the contents of a web page, asynchronously</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 42</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 43</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> httpAsync(url:string) = </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 44</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; async { </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> req = WebRequest.Create(url) </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 45</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use!</span><span style="LINE-HEIGHT: 140%"> resp = req.AsyncGetResponse()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 46</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use</span><span style="LINE-HEIGHT: 140%"> stream = resp.GetResponseStream() </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 47</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use</span><span style="LINE-HEIGHT: 140%"> reader = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> StreamReader(stream) </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 48</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> reader.ReadToEnd() }</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 49</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 50</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Load an RSS feed&#39;s contents into an XML document object</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 51</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// and use it to extract the titles and their links</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 52</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Hopefully these always match (this could be coded more</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 53</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// defensively)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 54</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 55</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> titlesAndLinks (name, url, xml) =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 56</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 57</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> xdoc = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> XmlDocument()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 58</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; xdoc.LoadXml(xml)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 59</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 60</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> titles =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 61</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; [ </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> n </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> xdoc.SelectNodes(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;//*[name()=&#39;title&#39;]&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 62</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> n.InnerText ]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 63</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> links =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 64</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; [ </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> n </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> xdoc.SelectNodes(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;//*[name()=&#39;link&#39;]&quot;</span><span style="LINE-HEIGHT: 140%">) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 65</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> inn = n.InnerText</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 66</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%">&#0160; inn.Length &gt; 0 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 67</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; inn</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 68</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 69</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> href = n.Attributes.GetNamedItem(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;href&quot;</span><span style="LINE-HEIGHT: 140%">).Value</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 70</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> rel = n.Attributes.GetNamedItem(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;rel&quot;</span><span style="LINE-HEIGHT: 140%">).Value</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 71</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> List.exists</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 72</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160;&#0160;&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> x </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> href.Contains(x))</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 73</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160;&#0160;&#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;feedburner&quot;</span><span style="LINE-HEIGHT: 140%">;</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;feedproxy&quot;</span><span style="LINE-HEIGHT: 140%">;</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;hubbub&quot;</span><span style="LINE-HEIGHT: 140%">] </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 74</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;&quot;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 75</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 76</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160; href ]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 77</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 78</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> descs =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 79</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; [ </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> n </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> xdoc.SelectNodes</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 80</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;//*[name()=&#39;description&#39; or name()=&#39;subtitle&#39;&quot;</span><span style="LINE-HEIGHT: 140%"> +</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 81</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot; or name()=&#39;summary&#39;]&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 82</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> n.InnerText ]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 83</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 84</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// A local function to filter out duplicate entries in</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 85</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// a list, maintaining their current order.</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 86</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Another way would be to use:</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 87</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">//&#0160;&#0160;&#0160; Set.of_list lst |&gt; Set.to_list</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 88</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// but that results in a sorted (probably reordered) list.</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 89</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 90</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">rec</span><span style="LINE-HEIGHT: 140%"> nub lst =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 91</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">match</span><span style="LINE-HEIGHT: 140%"> lst </span><span style="LINE-HEIGHT: 140%; COLOR: blue">with</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 92</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; | a::[] </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> [a]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 93</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; | a::b </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 94</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> a = List.head b </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 95</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; nub b</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 96</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 97</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; a::nub b</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 98</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; | [] </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> []</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 99</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 100</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Filter the links to get (hopefully) the same number</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 101</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// and order as the titles and descriptions</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 102</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 103</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> real = List.filter (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> (x:string) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> x.Length &gt; 0)&#0160; </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 104</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> lnks = real links |&gt; nub</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 105</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 106</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Return a link to the overall blog, if we don&#39;t have</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 107</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// the same numbers of titles, links and descriptions</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 108</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 109</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> lnum = List.length lnks</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 110</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> tnum = List.length titles</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 111</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> dnum = List.length descs</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 112</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 113</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> tnum = 0 || lnum = 0 || lnum &lt;&gt; tnum ||</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 114</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; dnum &lt;&gt; tnum </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 115</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; [(name,url,url)]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 116</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 117</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; List.zip3 titles lnks descs</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 118</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">with</span><span style="LINE-HEIGHT: 140%"> _ </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> []</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 119</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 120</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// For a particular (name,url) pair,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 121</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// create an AutoCAD HyperLink object</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 122</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 123</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> hyperlink (name,url,desc) =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 124</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> hl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> HyperLink()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 125</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; hl.Name &lt;- url</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 126</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; hl.Description &lt;- desc</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 127</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; (name, hl)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 128</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 129</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Use asynchronous workflows in F# to download</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 130</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// an RSS feed and return AutoCAD HyperLinks</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 131</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// corresponding to its posts</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 132</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 133</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> hyperlinksAsync (name, url, feed) =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 134</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; async { </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let!</span><span style="LINE-HEIGHT: 140%"> xml = httpAsync feed</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 135</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> tl = titlesAndLinks (name, url, xml)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 136</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> List.map hyperlink tl }</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 137</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 138</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: green">// Now we declare our command</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 139</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 140</span>&#0160;<span style="LINE-HEIGHT: 140%">[&lt;CommandMethod(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;amrss&quot;</span><span style="LINE-HEIGHT: 140%">)&gt;]</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 141</span>&#0160;<span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> createHyperlinksFromRssAsyncViaMailbox() =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 142</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 143</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> starttime = System.DateTime.Now</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 144</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 145</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s get the usual helpful AutoCAD objects</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 146</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 147</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 148</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 149</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 150</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> db = doc.Database</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 151</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 152</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 153</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 154</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 155</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 156</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 157</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 158</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 159</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> bt =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 160</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 161</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; (db.BlockTableId,OpenMode.ForRead)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 162</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; :?&gt; BlockTable</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 163</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ms =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 164</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 165</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; (bt.[BlockTableRecord.ModelSpace],</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 166</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; OpenMode.ForWrite)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 167</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; :?&gt; BlockTableRecord</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 168</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 169</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add text objects linking to the provided list of</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 170</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// HyperLinks, starting at the specified location</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 171</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 172</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Note the valid use of tr and ms, as they are in scope</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 173</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 174</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> addTextObjects (pt : Point3d) lst =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 175</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Use a for loop, as we care about the index to</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 176</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// position the various text items</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 177</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 178</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> len = List.length lst</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 179</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> index = 0 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">to</span><span style="LINE-HEIGHT: 140%"> len - 1 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">do</span><span style="LINE-HEIGHT: 140%"> </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 180</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> txt = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> DBText()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 181</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> (name:string,hl:HyperLink) = List.nth lst index</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 182</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; txt.TextString &lt;- name</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 183</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> offset =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 184</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> index = 0 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 185</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; 0.0</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 186</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">else</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 187</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; 1.0</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 188</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 189</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// This is where you can adjust:</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 190</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">//&#0160; the initial outdent (x value)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 191</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">//&#0160; and the line spacing (y value)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 192</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 193</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> vec =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 194</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Vector3d</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 195</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; (1.0 * offset,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 196</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; -0.5 * (float index),</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 197</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; 0.0)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 198</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pt2 = pt + vec</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 199</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; txt.Position &lt;- pt2</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 200</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; ms.AppendEntity(txt) |&gt; ignore</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 201</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; tr.AddNewlyCreatedDBObject(txt,</span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 202</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; txt.Hyperlinks.Add(hl) |&gt; ignore</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 203</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 204</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Define our agent to process messages regarding</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 205</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// hyperlinks to gather and process</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 206</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 207</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> agent =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 208</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; MailboxProcessor.Start(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> inbox </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 209</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">rec</span><span style="LINE-HEIGHT: 140%"> loop() = async {</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 210</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 211</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// An asynchronous operation to receive the message</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 212</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 213</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let!</span><span style="LINE-HEIGHT: 140%"> (i, tup, reply :</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 214</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; AsyncReplyChannel&lt;(string * HyperLink) list&gt;) =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 215</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; inbox.Receive()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 216</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 217</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And another to collect the hyperlinks for a feed</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 218</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 219</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let!</span><span style="LINE-HEIGHT: 140%"> res = hyperlinksAsync tup</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 220</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 221</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And then we reply with the results</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 222</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (the list of hyperlinks)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 223</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 224</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; reply.Reply(res)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 225</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 226</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Recurse to process more messages</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 227</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 228</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return!</span><span style="LINE-HEIGHT: 140%"> loop()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 229</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 230</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 231</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start the loop</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 232</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 233</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; loop()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 234</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; )</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 235</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 236</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Map our list of feeds to a set of asynchronous tasks</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 237</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// that we then execute in parallel</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 238</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 239</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; feeds</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 240</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; |&gt; List.mapi (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> i item </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 241</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; async {</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 242</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let!</span><span style="LINE-HEIGHT: 140%"> res =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 243</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; agent.PostAndAsyncReply(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> rep </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> (i, item, rep))</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 244</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 245</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Once we have the response (asynchronously), create</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 246</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// the corresponding AutoCAD text objects</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 247</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 248</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pt =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 249</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Point3d</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 250</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160; (15.0 * (float i),</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 251</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160; 30.0,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 252</span>&#0160;<span style="LINE-HEIGHT: 140%">			&#0160; 0.0)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 253</span>&#0160;<span style="LINE-HEIGHT: 140%">		&#0160; addTextObjects pt res |&gt; ignore</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 254</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160;&#0160;&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 255</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; )</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 256</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; |&gt; Async.Parallel</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 257</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; |&gt; Async.RunSynchronously</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 258</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; |&gt; ignore</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 259</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 260</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; tr.Commit()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 261</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 262</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> elapsed =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 263</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; System.DateTime.op_Subtraction</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 264</span>&#0160;<span style="LINE-HEIGHT: 140%">	&#0160; (System.DateTime.Now, starttime)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 265</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 266</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; ed.WriteMessage(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nElapsed time: &quot;</span><span style="LINE-HEIGHT: 140%"> + elapsed.ToString())</span></p></div>
<p>I executed the four different versions of the commands ten times each and averaged them to get the following results:</p>
<table border="0" cellpadding="2" cellspacing="0" width="431">
<tbody>
<tr>
<td valign="top" width="291"><strong>Implementation approach (command)</strong></td>
<td align="middle" valign="top" width="138"><strong>Average time (secs)</strong></td></tr>
<tr>
<td valign="top" width="289">Synchronous (RSS)</td>
<td align="middle" valign="top" width="140">16.47</td></tr>
<tr>
<td valign="top" width="288">Asynchronous (ARSS)</td>
<td align="middle" valign="top" width="141">8.45</td></tr>
<tr>
<td valign="top" width="287">Message-passing synchronous (MRSS)</td>
<td align="middle" valign="top" width="142">15.19</td></tr>
<tr>
<td valign="top" width="287">Message-passing asynchronous (AMRSS)</td>
<td align="middle" valign="top" width="142">14.14</td></tr></tbody></table>
<p>These values are from when running the code from our Tokyo office: I remember having quite different results when running them from home in Switzerland, for instance. As the network lag here appears larger (probably not helped by the fact I’m connecting via WiFi), the difference is more pronounced which definitely helps us when comparing and contrasting the results.</p>
<p>We see that complexity of this particular problem probably doesn’t merit the overhead of implementing a message-passing approach, although if we do choose to do so the network lag might make it worth the additional overhead of launching the tasks asynchronously rather than one after the other. This balance will shift depending on the local network lag, the processing time and the synchronization overhead: in general doing some kind of benchmark on a simplified version of your problem is likely to be a worthwhile investment before implementing a complete, complex system.</p>
