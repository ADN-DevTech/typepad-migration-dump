---
layout: "post"
title: "Using F# Asynchronous Workflows to simplify concurrent programming in AutoCAD"
date: "2008-01-25 09:12:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Concurrent programming"
  - "F#"
  - "Weblogs"
original_url: "https://www.keanw.com/2008/01/harnessing-f-as.html "
typepad_basename: "harnessing-f-as"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/turning-autocad.html">the last post</a> we saw some code that downloaded data - serially - from a number of websites via RSS and created AutoCAD entities linking to the various posts.</p>

<p>As promised, in today's post we take that code and enable it to query the same data in parallel by using <a href="http://blogs.msdn.com/dsyme/archive/2007/10/11/introducing-f-asynchronous-workflows.aspx">Asynchronous Workflows in F#</a>. Asynchronous Workflows are an easy-to-use yet powerful mechanism for enabling concurrent programming in F#.</p>

<p>Firstly, a little background as to why this type of technique is important. As many - if not all - of you are aware, the days of raw processor speed doubling every couple of years are over. The technical innovations that enabled <a href="http://en.wikipedia.org/wiki/Moore_Law">Moore's Law</a> to hold true for half a century are - at least in the area of silicon-based microprocessor design - hitting a wall (it's apparently called the Laws of Physics :-). Barring some disruptive technological development, the future gains in computing performance are to be found in the use of parallel processing, whether via multiple cores, processors or distributed clouds of computing resources. </p>

<p>Additionally, with an increasing focus on distributed computing and information resources, managing tasks asynchronously becomes more important, as information requests across a network inevitably introduce a latency that can be mitigated by the tasks being run in parallel.</p>

<p>The big problem is that concurrent programming is - for the most-part - extremely difficult to do, and even harder to retro-fit into existing applications. Traditional lock-based parallelism (where locks are used to control access to shared computing resources) is both unwieldy and prone to blocking. New technologies, such as Asynchronous Workflows and <a href="http://cs.hubfs.net/blogs/hell_is_other_languages/archive/2008/01/16/4565.aspx">Software Transactional Memory</a>, provide considerable hope (and this is a topic I have on my list to cover at some future point).</p>

<p>Today's post looks at a relatively simple scenario, in the sense that we want to perform a set of discrete tasks in parallel, harnessing those fancy multi-core systems for those of you lucky enough to have them (I'm hoping to get one when I next replace my notebook, sometime in March), but that these tasks are indeed independent: we want to wait until they are all complete, but we do not have the additional burden of them communicating amongst themselves or using shared resources (e.g. accessing shared memory) during their execution.</p>

<p>We are also going to be very careful only to run parallel tasks unrelated to AutoCAD. Any access made into AutoCAD's database, for instance, needs to be performed in series: AutoCAD is not thread-safe when it comes to the vast majority of its programmatically-accessible functionality. So we're going to run a set of asynchronous, parallel tasks to query our various RSS feeds, and combine the results before creating the corresponding geometry in AutoCAD. This all sounds very complex, but the good (actually great) news is that Asynchronous Workflows does all the heavy lifting. Phew.</p>

<p>Here's the modified F# code, with the modified/new lines coloured in red:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 1</span> <span style="COLOR: green">// Use lightweight F# syntax</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 2</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 3</span> #light</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 4</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 5</span> <span style="COLOR: green">// Declare a specific namespace and module name</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 6</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 7</span> <span style="COLOR: blue">module</span> MyNamespace.MyApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 8</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 9</span> <span style="COLOR: green">// Import managed assemblies</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;10</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;11</span> #I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2008&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;12</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;13</span> #r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;14</span> #r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;15</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;16</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;17</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;18</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;19</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;20</span> <span style="COLOR: blue">open</span> System.Xml</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;21</span> <span style="COLOR: blue">open</span> System.Collections</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;22</span> <span style="COLOR: blue">open</span> System.Collections.Generic</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;23</span> <span style="COLOR: blue">open</span> System.IO</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;24</span> <span style="COLOR: blue">open</span> System.Net</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;25</span> <span style="COLOR: blue">open</span> Microsoft.FSharp.Control.CommonExtensions</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;26</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;27</span> <span style="COLOR: green">// The RSS feeds we wish to get. The first two values are</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;28</span> <span style="COLOR: green">// only used if our code is not able to parse the feed's XML</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;29</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;30</span> <span style="COLOR: blue">let</span> feeds =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;31</span>&nbsp; &nbsp;[ (<span style="COLOR: maroon">&quot;Through the Interface&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;32</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;33</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://through-the-interface.typepad.com/through_the_interface/rss.xml&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;34</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;35</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Don Syme's F# blog&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;36</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;37</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/rss.xml&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;38</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;39</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Shaan Hurley's Between the Lines&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;40</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;41</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines/rss.xml&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;42</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;43</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Scott Sheppard's It's Alive in the Lab&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;44</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/labs&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;45</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://labs.blogs.com/its_alive_in_the_lab/rss.xml&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;46</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;47</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Lynn Allen's Blog&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;48</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/lynn&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;49</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://lynn.blogs.com/lynn_allens_blog/index.rdf&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;50</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;51</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Heidi Hewett's AutoCAD Insider&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;52</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/autocadinsider&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;53</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://heidihewett.blogs.com/my_weblog/index.rdf&quot;</span>) ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;54</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;55</span> <span style="COLOR: green">// Fetch the contents of a web page, asynchronously</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;56</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;57</span> <span style="COLOR: blue">let</span> httpAsync(url:string) = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;58</span>&nbsp; &nbsp;async { <span style="COLOR: blue">let</span> req = WebRequest.Create(url) </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;59</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">use!</span> resp = req.GetResponseAsync()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;60</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">use</span> stream = resp.GetResponseStream() </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;61</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">use</span> reader = <span style="COLOR: blue">new</span> StreamReader(stream) </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;62</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> reader.ReadToEnd() }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;63</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;64</span> <span style="COLOR: green">// Load an RSS feed's contents into an XML document object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;65</span> <span style="COLOR: green">// and use it to extract the titles and their links</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;66</span> <span style="COLOR: green">// Hopefully these always match (this could be coded more</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;67</span> <span style="COLOR: green">// defensively)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;68</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;69</span> <span style="COLOR: blue">let</span> titlesAndLinks (name, url, xml) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;70</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> xdoc = <span style="COLOR: blue">new</span> XmlDocument()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;71</span>&nbsp; &nbsp;xdoc.LoadXml(xml)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;72</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;73</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> titles =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;74</span>&nbsp; &nbsp;&nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes(<span style="COLOR: maroon">&quot;//*[name()='title']&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;75</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">-&gt;</span> n.InnerText ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;76</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> links =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;77</span>&nbsp; &nbsp;&nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes(<span style="COLOR: maroon">&quot;//*[name()='link']&quot;</span>) <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;78</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> inn = n.InnerText</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;79</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span>&nbsp; inn.Length &gt; 0 <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;80</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; inn</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;81</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;82</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> href = n.Attributes.GetNamedItem(<span style="COLOR: maroon">&quot;href&quot;</span>).Value</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;83</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> rel = n.Attributes.GetNamedItem(<span style="COLOR: maroon">&quot;rel&quot;</span>).Value</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;84</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> href.Contains(<span style="COLOR: maroon">&quot;feedburner&quot;</span>) <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;85</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;86</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;87</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; href ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;88</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;89</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> descs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;90</span>&nbsp; &nbsp;&nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;91</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: maroon">&quot;//*[name()='description' or name()='content' or name()='subtitle']&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;92</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">-&gt;</span> n.InnerText ]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;93</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;94</span>&nbsp; &nbsp;<span style="COLOR: green">// A local function to filter out duplicate entries in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;95</span>&nbsp; &nbsp;<span style="COLOR: green">// a list, maintaining their current order.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;96</span>&nbsp; &nbsp;<span style="COLOR: green">// Another way would be to use:</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;97</span>&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; &nbsp; Set.of_list lst |&gt; Set.to_list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;98</span>&nbsp; &nbsp;<span style="COLOR: green">// but that results in a sorted (probably reordered) list.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;99</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 100</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> nub lst =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 101</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">match</span> lst <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 102</span>&nbsp; &nbsp;&nbsp; | a::[] <span style="COLOR: blue">-&gt;</span> [a]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 103</span>&nbsp; &nbsp;&nbsp; | a::b <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 104</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> a = List.hd b <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 105</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;nub b</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 106</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 107</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;a::nub b</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 108</span>&nbsp; &nbsp;&nbsp; | [] <span style="COLOR: blue">-&gt;</span> []</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 109</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 110</span>&nbsp; &nbsp;<span style="COLOR: green">// Filter the links to get (hopefully) the same number</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 111</span>&nbsp; &nbsp;<span style="COLOR: green">// and order as the titles and descriptions</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 112</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 113</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> real = List.filter (<span style="COLOR: blue">fun</span> (x:string) <span style="COLOR: blue">-&gt;</span> x.Length &gt; 0)&nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 114</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> lnks = real links |&gt; nub</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 115</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 116</span>&nbsp; &nbsp;<span style="COLOR: green">// Return a link to the overall blog, if we don't have</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 117</span>&nbsp; &nbsp;<span style="COLOR: green">// the same numbers of titles, links and descriptions</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 118</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 119</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> lnum = List.length lnks</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 120</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> tnum = List.length titles</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 121</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> dnum = List.length descs</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 122</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 123</span>&nbsp; &nbsp;<span style="COLOR: blue">if</span> tnum = 0 || lnum = 0 || lnum &lt;&gt; tnum || dnum &lt;&gt; tnum <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 124</span>&nbsp; &nbsp;&nbsp; [(name,url,url)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 125</span>&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 126</span>&nbsp; &nbsp;&nbsp; List.zip3 titles lnks descs</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 127</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 128</span> <span style="COLOR: green">// For a particular (name,url) pair,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 129</span> <span style="COLOR: green">// create an AutoCAD HyperLink object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 130</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 131</span> <span style="COLOR: blue">let</span> hyperlink (name,url,desc) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 132</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> hl = <span style="COLOR: blue">new</span> HyperLink()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 133</span>&nbsp; &nbsp;hl.Name &lt;- url</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 134</span>&nbsp; &nbsp;hl.Description &lt;- desc</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 135</span>&nbsp; &nbsp;(name, hl)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 136</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 137</span> <span style="COLOR: green">// Use asynchronous workflows in F# to download</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 138</span> <span style="COLOR: green">// an RSS feed and return AutoCAD HyperLinks</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 139</span> <span style="COLOR: green">// corresponding to its posts</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 140</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 141</span> <span style="COLOR: blue">let</span> hyperlinksAsync (name, url, feed) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 142</span>&nbsp; &nbsp;async { <span style="COLOR: blue">let!</span> xml = httpAsync feed</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 143</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> tl = titlesAndLinks (name, url, xml)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 144</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> List.map hyperlink tl }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 145</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 146</span> <span style="COLOR: green">// Now we declare our command</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 147</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 148</span> [&lt;CommandMethod(<span style="COLOR: maroon">&quot;rss&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 149</span> <span style="COLOR: blue">let</span> createHyperlinksFromRss() =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 150</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 151</span>&nbsp; &nbsp;<span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 152</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 153</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 154</span>&nbsp; &nbsp;&nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 155</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> db = doc.Database</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 156</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 157</span>&nbsp; &nbsp;<span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 158</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 159</span>&nbsp; &nbsp;<span style="COLOR: blue">use</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 160</span>&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 161</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 162</span>&nbsp; &nbsp;<span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 163</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 164</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 165</span>&nbsp; &nbsp;&nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 166</span>&nbsp; &nbsp;&nbsp; &nbsp; (db.BlockTableId,OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 167</span>&nbsp; &nbsp;&nbsp; :?&gt; BlockTable</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 168</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 169</span>&nbsp; &nbsp;&nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 170</span>&nbsp; &nbsp;&nbsp; &nbsp; (bt.[BlockTableRecord.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 171</span>&nbsp; &nbsp;&nbsp; &nbsp; OpenMode.ForWrite)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 172</span>&nbsp; &nbsp;&nbsp; :?&gt; BlockTableRecord</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 173</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 174</span>&nbsp; &nbsp;<span style="COLOR: green">// Add text objects linking to the provided list of</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 175</span>&nbsp; &nbsp;<span style="COLOR: green">// HyperLinks, starting at the specified location</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 176</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 177</span>&nbsp; &nbsp;<span style="COLOR: green">// Note the valid use of tr and ms, as they are in scope</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 178</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 179</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> addTextObjects pt lst =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 180</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Use a for loop, as we care about the index to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 181</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// position the various text items</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 182</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 183</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> len = List.length lst</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 184</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> index = 0 <span style="COLOR: blue">to</span> len - 1 <span style="COLOR: blue">do</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 185</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> txt = <span style="COLOR: blue">new</span> DBText()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 186</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> (name:string,hl:HyperLink) = List.nth lst index</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 187</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.TextString &lt;- name</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 188</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> offset =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 189</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> index = 0 <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 190</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0.0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 191</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 192</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 1.0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 193</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 194</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// This is where you can adjust:</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 195</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; the initial outdent (x value)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 196</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; and the line spacing (y value)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 197</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 198</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> vec =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 199</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> Vector3d</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 200</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (1.0 * offset,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 201</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; -0.5 * (Int32.to_float index),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 202</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 203</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> pt2 = pt + vec</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 204</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.Position &lt;- pt2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 205</span>&nbsp; &nbsp;&nbsp; &nbsp; ms.AppendEntity(txt) |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 206</span>&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(txt,<span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 207</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.Hyperlinks.Add(hl) |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 208</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 209</span>&nbsp; &nbsp;<span style="COLOR: green">// Here's where we do the real work, by firing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 210</span>&nbsp; &nbsp;<span style="COLOR: green">// off - and coordinating - asynchronous tasks</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 211</span>&nbsp; &nbsp;<span style="COLOR: green">// to create HyperLink objects for all our posts</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 212</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 213</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> links =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 214</span>&nbsp; &nbsp;&nbsp; Async.Run</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 215</span>&nbsp; &nbsp;&nbsp; &nbsp; (Async.Parallel</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 216</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;[ <span style="COLOR: blue">for</span> (name,url,feed) <span style="COLOR: blue">in</span> feeds <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 217</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; hyperlinksAsync (name,url,feed) ]) </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 218</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 219</span>&nbsp; &nbsp;<span style="COLOR: green">// Add the resulting objects to the model-space&nbsp; </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 220</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 221</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> len = Array.length links</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 222</span>&nbsp; &nbsp;<span style="COLOR: blue">for</span> index = 0 <span style="COLOR: blue">to</span> len - 1 <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 223</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 224</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// This is where you can adjust:</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 225</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; the column spacing (x value)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 226</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; the vertical offset from origin (y axis)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 227</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 228</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> pt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 229</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Point3d</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 230</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(15.0 * (Int32.to_float index),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 231</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;30.0,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 232</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 233</span>&nbsp; &nbsp;&nbsp; addTextObjects pt (Array.get links index)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 234</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 235</span>&nbsp; &nbsp;tr.Commit()</p></div>

<p>You can download the new F# source file from <a href="http://through-the-interface.typepad.com/through_the_interface/files/async-workflows-rss-to-hyperlinks.fs">here</a>.</p>

<p>A few comments on the changes:</p>

<p>Lines 57-62 define our new httpAsync() function, which uses GetResponseAsync() - a function exposed in F# 1.9.3.7 - to download the contents of a web-page asynchronously [and which I stole shamelessly from Don Syme, who presented the code last summer at Microsoft's TechEd].</p>

<p>Lines 141-144 define another asynchronous function, hyperlinksAsync(), which calls httpAsync() and then - as before - extracts the feed information and creates a corresponding list of HyperLinks. This is significant: creation of AutoCAD HyperLink objects will be done on parallel; it is the addition of these objects to the drawing database that needs to be performed serially.</p>

<p>Lines 214-217 replace our very simple &quot;map&quot; with something slightly more complex: this code runs a list of tasks in parallel and waits for them all to complete before continuing. What is especially cool about this implementation is the fact that exceptions in individual tasks result in the overall task failing (a good thing, believe it or not :-), and the remaining tasks being terminated gracefully.</p>

<p>Lines 221 and 233 change our code to handle an array, rather than a list (while &quot;map&quot; previously returned a list, Async.Run returns an array).</p>

<p>When run, the code creates exactly the same thing as last time (although there are a few more posts in some of the blogs ;-)</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=249,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2008/01/23/autocad_does_rss_3.png"><img title="AutoCAD does RSS" height="93" alt="Autocad_does_rss_3" src="/assets/autocad_does_rss_3.png" width="300" border="0" /></a></p>

<p>A quick word on timing: I used &quot;F# Interactive&quot; to do a little benchmarking on my system, and even though it's single-core, single-processor, there was a considerable difference between the two implementations. I'll talk more about F# Interactive at some point, but think of it to F# in Visual Studio as the command-line is to LISP in AutoCAD: you can very easily test out fragments of F#, either by entering them directly into the F# Interactive window or highlighting them in Visual Studio's text editor and hitting Alt-Enter.</p>

<p>To enable function timing I entered &quot;#time;;&quot; (without the quotations marks) in the F# Interactive window. I then selected and loaded the supporting functions needed for each test - not including the code that adds the DBText objects with their HyperLinks to the database, as we're only in Visual Studio, not inside AutoCAD - and executed the &quot;let links = ...&quot; assignment in our two implementations of the createHyperlinksFromRss() function (i.e. the RSS command). These functions do create lists of AutoCAD HyperLinks, but that's OK: this is something works even outside AutoCAD, although we wouldn't be able to do anything much with them. Also, the fact we're not including the addition of the entities to the AutoCAD database is not relevant: by then we should have identical data in both versions, which would be added in exactly the same way.</p>

<p>Here are the results:</p>

<p>I executed the code for serial querying and parallel querying twice (to make sure there were no effects from page caching on the measurement):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">val links : (string * HyperLink) list list</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Real: 00:00:14.636, CPU: 00:00:00.15, GC gen0: 5, gen1: 1, gen2: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">val links : (string * HyperLink) list array</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Real: 00:00:06.245, CPU: 00:00:00.31, GC gen0: 3, gen1: 0, gen2: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">val links : (string * HyperLink) list list</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Real: 00:00:15.45, CPU: 00:00:00.46, GC gen0: 5, gen1: 1, gen2: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">val links : (string * HyperLink) list array</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Real: 00:00:03.832, CPU: 00:00:00.62, GC gen0: 2, gen1: 1, gen2: 0</p></div></div>

<p>So the serial execution took 14.5 to 15.5 seconds, while the parallel execution took 3.8 to 6.3 seconds.</p>
