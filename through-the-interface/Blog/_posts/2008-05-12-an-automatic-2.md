---
layout: "post"
title: "An automatic numbering system for AutoCAD blocks using .NET - Part 3"
date: "2008-05-12 07:30:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Dimensions"
  - "Selection"
original_url: "https://www.keanw.com/2008/05/an-automatic--2.html "
typepad_basename: "an-automatic--2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/an-automatic--1.html">the last post</a> we introduced some additional features to <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/an-automatic-nu.html">the original post in this series</a>. In this post we take advantage of - and further extend - those features, by allowing deletion, movement and compaction of the numbered objects.</p>
<p>Here&#39;s the modified C# code, with changed/new lines in <span style="color: #ff0000;">red</span>, and here is <a href="http://through-the-interface.typepad.com/through_the_interface/files/auto-bubble-creation-part3.cs">the updated source file</a>:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: courier new;">
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 1</span> <span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 2</span> <span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 3</span> <span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 4</span> <span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 5</span> <span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 6</span> <span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 7</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 8</span> <span style="color: blue;">namespace</span> AutoNumberedBubbles</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160; 9</span> {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;10</span>&#0160; &#0160;<span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span> : <span style="color: #2b91af;">IExtensionApplication</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;11</span>&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;12</span>&#0160; &#0160;&#0160; <span style="color: green;">// Strings identifying the block</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;13</span>&#0160; &#0160;&#0160; <span style="color: green;">// and the attribute name to use</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;14</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;15</span>&#0160; &#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> blockName = <span style="color: #a31515;">&quot;BUBBLE&quot;</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;16</span>&#0160; &#0160;&#0160; <span style="color: blue;">const</span> <span style="color: blue;">string</span> attbName = <span style="color: #a31515;">&quot;NUMBER&quot;</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;17</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;18</span>&#0160; &#0160;&#0160; <span style="color: green;">// We will use a separate object to</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;19</span>&#0160; &#0160;&#0160; <span style="color: green;">// manage our numbering, and maintain a</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;20</span>&#0160; &#0160;&#0160; <span style="color: green;">// &quot;base&quot; index (the start of the list)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;21</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;22</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">NumberedObjectManager</span> m_nom;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;23</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">int</span> m_baseNumber = 0;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;24</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;25</span>&#0160; &#0160;&#0160; <span style="color: green;">// Constructor</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;26</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;27</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> Commands()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;28</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;29</span>&#0160; &#0160;&#0160; &#0160; m_nom = <span style="color: blue;">new</span> <span style="color: #2b91af;">NumberedObjectManager</span>();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;30</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;31</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;32</span>&#0160; &#0160;&#0160; <span style="color: green;">// Functions called on initialization &amp; termination</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;33</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;34</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Initialize()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;35</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;36</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">try</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;37</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;38</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;39</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;40</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;41</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;42</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;43</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nLNS&#0160; Load numbering settings by analyzing the current drawing&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;44</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nDMP&#0160; Print internal numbering information&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;45</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nBAP&#0160; Create bubbles at points&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; &#0160;46</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nBIC&#0160; Create bubbles at the center of circles&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; &#0160;47</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nMB&#0160; &#0160;Move a bubble in the list&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; &#0160;48</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nDB&#0160; &#0160;Delete a bubble&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; &#0160;49</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nRBS&#0160; Reorder the bubbles, to close gaps caused by deletion&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; &#0160;50</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nHLB&#0160; Highlight a particular bubble&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;51</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;52</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;53</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">catch</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;54</span>&#0160; &#0160;&#0160; &#0160; { }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;55</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;56</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;57</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Terminate()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;58</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;59</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;60</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;61</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to extract and display information</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;62</span>&#0160; &#0160;&#0160; <span style="color: green;">// about the internal numbering</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;63</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;64</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DMP&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;65</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> DumpNumberingInformation()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;66</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;67</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;68</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;69</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;70</span>&#0160; &#0160;&#0160; &#0160; m_nom.DumpInfo(ed);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;71</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;72</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;73</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to analyze the current document and</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;74</span>&#0160; &#0160;&#0160; <span style="color: green;">// understand which indeces have been used and</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;75</span>&#0160; &#0160;&#0160; <span style="color: green;">// which are currently free</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;76</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;77</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;LNS&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;78</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> LoadNumberingSettings()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;79</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;80</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;81</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;82</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;83</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;84</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;85</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// We need to clear any internal state</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;86</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// already collected</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;87</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;88</span>&#0160; &#0160;&#0160; &#0160; m_nom.Clear();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;89</span>&#0160; &#0160;&#0160; &#0160; m_baseNumber = 0;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;90</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;91</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Select all the blocks in the current drawing</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;92</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;93</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">TypedValue</span>[] tvs =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;94</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>[1] {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;95</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;96</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="color: blue;">int</span>)<span style="color: #2b91af;">DxfCode</span>.Start,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;97</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #a31515;">&quot;INSERT&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;98</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; &#0160;99</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; };</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 100</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">SelectionFilter</span> sf =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 101</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">SelectionFilter</span>(tvs);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 102</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 103</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">PromptSelectionResult</span> psr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 104</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.SelectAll(sf);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 105</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 106</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// If it succeeded and we have some blocks...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 107</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 108</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (psr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK &amp;&amp;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 109</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; psr.Value.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 110</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 111</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 112</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 113</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 114</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 115</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// First get the modelspace and the ID</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 116</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// of the block for which we&#39;re searching</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 117</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 118</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">BlockTableRecord</span> ms;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 119</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">ObjectId</span> blockId;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 120</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 121</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (GetBlock(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 122</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db, tr, <span style="color: blue;">out</span> ms, <span style="color: blue;">out</span> blockId</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 123</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 124</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 125</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// For each block reference in the drawing...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 126</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 127</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">SelectedObject</span> o <span style="color: blue;">in</span> psr.Value)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 128</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 129</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObject</span> obj =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 130</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.GetObject(o.ObjectId, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 131</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">BlockReference</span> br = obj <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 132</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (br != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 133</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 134</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// If it&#39;s the one we care about...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 135</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 136</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (br.BlockTableRecord == blockId)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 137</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 138</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Check its attribute references...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 139</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 140</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> pos = -1;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 141</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">AttributeCollection</span> ac =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 142</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;br.AttributeCollection;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 143</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 144</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="color: blue;">in</span> ac)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 145</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 146</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObject</span> obj2 =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 147</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 148</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">AttributeReference</span> ar =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 149</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; obj2 <span style="color: blue;">as</span> <span style="color: #2b91af;">AttributeReference</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 150</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 151</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// When we find the attribute</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 152</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// we care about...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 153</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 154</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (ar.Tag == attbName)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 155</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 156</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 157</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 158</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Attempt to extract the number from</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 159</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// the text string property... use a</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 160</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// try-catch block just in case it is</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 161</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// non-numeric</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 162</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 163</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; pos =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 164</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">int</span>.Parse(ar.TextString);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 165</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 166</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Add the object at the appropriate</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 167</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 168</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 169</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; m_nom.NumberObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 170</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;o.ObjectId, pos, <span style="color: blue;">false</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 171</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 172</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 173</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">catch</span> { }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 174</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 175</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 176</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 177</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 178</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 179</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 180</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 181</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 182</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 183</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Once we have analyzed all the block references...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 184</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 185</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">int</span> start = m_nom.GetLowerBound(<span style="color: blue;">true</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 186</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 187</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// If the first index is non-zero, ask the user if</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 188</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// they want to rebase the list to begin at the</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 189</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// current start position</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 190</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 191</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (start &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 192</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 193</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 194</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #a31515;">&quot;\nLowest index is {0}. &quot;</span>,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 195</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; start</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 196</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 197</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">PromptKeywordOptions</span> pko =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 198</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 199</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #a31515;">&quot;Make this the start of the list?&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 200</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 201</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; pko.AllowNone = <span style="color: blue;">true</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 202</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Add(<span style="color: #a31515;">&quot;Yes&quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 203</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Add(<span style="color: #a31515;">&quot;No&quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 204</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; pko.Keywords.Default = <span style="color: #a31515;">&quot;Yes&quot;</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 205</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 206</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">PromptResult</span> pkr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 207</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.GetKeywords(pko);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 208</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 209</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (pkr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 210</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 211</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (pkr.StringResult == <span style="color: #a31515;">&quot;Yes&quot;</span>)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 212</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 213</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// We store our own base number</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 214</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// (the object used to manage objects</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 215</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// always uses zero-based indeces)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 216</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 217</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_baseNumber = start;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 218</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_nom.RebaseList(m_baseNumber);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 219</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 220</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 221</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 222</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 223</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 224</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 225</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to create bubbles at points selected</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 226</span>&#0160; &#0160;&#0160; <span style="color: green;">// by the user - loops until cancelled</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 227</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 228</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;BAP&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 229</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> BubblesAtPoints()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 230</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 231</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 232</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 233</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 234</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 235</span>&#0160; &#0160;&#0160; &#0160; Autodesk.AutoCAD.ApplicationServices.</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 236</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">TransactionManager</span> tm =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 237</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;doc.TransactionManager;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 238</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 239</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 240</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tm.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 241</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 242</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 243</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Get the information about the block</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 244</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// and attribute definitions we care about</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 245</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 246</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">BlockTableRecord</span> ms;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 247</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">ObjectId</span> blockId;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 248</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">AttributeDefinition</span> ad;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 249</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 250</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 251</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (GetBlock(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 252</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db, tr, <span style="color: blue;">out</span> ms, <span style="color: blue;">out</span> blockId</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 253</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 254</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 255</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; GetBlockAttributes(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 256</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr, blockId, <span style="color: blue;">out</span> ad, <span style="color: blue;">out</span> other</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 257</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 258</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 259</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// By default the modelspace is returned to</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 260</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// us in read-only state</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 261</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 262</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ms.UpgradeOpen();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 263</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 264</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Loop until cancelled</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 265</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 266</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">bool</span> finished = <span style="color: blue;">false</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 267</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">while</span> (!finished)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 268</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 269</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">PromptPointOptions</span> ppo =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 270</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">PromptPointOptions</span>(<span style="color: #a31515;">&quot;\nSelect point: &quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 271</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ppo.AllowNone = <span style="color: blue;">true</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 272</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 273</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">PromptPointResult</span> ppr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 274</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.GetPoint(ppo);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 275</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (ppr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 276</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;finished = <span style="color: blue;">true</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 277</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 278</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Call a function to create our bubble</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 279</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;CreateNumberedBubbleAtPoint(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 280</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db, ms, tr, ppr.Value,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 281</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; blockId, ad, other</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 282</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 283</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tm.QueueForGraphicsFlush();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 284</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tm.FlushGraphics();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 285</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 286</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 287</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 288</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 289</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 290</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 291</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to create a bubble at the center of</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 292</span>&#0160; &#0160;&#0160; <span style="color: green;">// each of the selected circles</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 293</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 294</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;BIC&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 295</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> BubblesInCircles()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 296</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 297</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 298</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 299</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 300</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 301</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 302</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Allow the user to select circles</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 303</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 304</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">TypedValue</span>[] tvs =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 305</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>[1] {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 306</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 307</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="color: blue;">int</span>)<span style="color: #2b91af;">DxfCode</span>.Start,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 308</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #a31515;">&quot;CIRCLE&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 309</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 310</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; };</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 311</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">SelectionFilter</span> sf =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 312</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">SelectionFilter</span>(tvs);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 313</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 314</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">PromptSelectionResult</span> psr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 315</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.GetSelection(sf);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 316</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 317</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (psr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK &amp;&amp;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 318</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; psr.Value.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 319</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 320</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 321</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 322</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 323</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 324</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Get the information about the block</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 325</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// and attribute definitions we care about</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 326</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 327</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">BlockTableRecord</span> ms;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 328</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">ObjectId</span> blockId;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 329</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">AttributeDefinition</span> ad;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 330</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 331</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 332</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (GetBlock(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 333</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db, tr, <span style="color: blue;">out</span> ms, <span style="color: blue;">out</span> blockId</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 334</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 335</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 336</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; GetBlockAttributes(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 337</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr, blockId, <span style="color: blue;">out</span> ad, <span style="color: blue;">out</span> other</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 338</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 339</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 340</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// By default the modelspace is returned to</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 341</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// us in read-only state</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 342</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 343</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ms.UpgradeOpen();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 344</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 345</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">SelectedObject</span> o <span style="color: blue;">in</span> psr.Value)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 346</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 347</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// For each circle in the selected list...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 348</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 349</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObject</span> obj =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 350</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.GetObject(o.ObjectId, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 351</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Circle</span> c = obj <span style="color: blue;">as</span> <span style="color: #2b91af;">Circle</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 352</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (c == <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 353</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 354</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #a31515;">&quot;\nObject selected is not a circle.&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 355</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 356</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 357</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Call our numbering function, passing the</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 358</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// center of the circle</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 359</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; CreateNumberedBubbleAtPoint(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 360</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; db, ms, tr, c.Center,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 361</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; blockId, ad, other</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 362</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 363</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 364</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 365</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 366</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 367</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 368</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 369</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 370</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to delete a particular bubble</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 371</span>&#0160; &#0160;&#0160; <span style="color: green;">// selected by its index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 372</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 373</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;MB&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 374</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> MoveBubble()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 375</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 376</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 377</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 378</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 379</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 380</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Use a helper function to select a valid bubble index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 381</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 382</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> pos =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 383</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;GetBubbleNumber(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 384</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 385</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nEnter number of bubble to move: &quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 386</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 387</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 388</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (pos &gt;= m_baseNumber)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 389</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 390</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">int</span> from = pos - m_baseNumber;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 391</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 392</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;pos =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 393</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; GetBubbleNumber(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 394</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 395</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #a31515;">&quot;\nEnter destination position: &quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 396</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 397</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 398</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (pos &gt;= m_baseNumber)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 399</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 400</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">int</span> to = pos - m_baseNumber;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 401</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 402</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 403</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; m_nom.MoveObject(from, to);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 404</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 405</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; RenumberBubbles(doc.Database, ids);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 406</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 407</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 408</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 409</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 410</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to delete a particular bubbler,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 411</span>&#0160; &#0160;&#0160; <span style="color: green;">// selected by its index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 412</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 413</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DB&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 414</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> DeleteBubble()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 415</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 416</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 417</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 418</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 419</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 420</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 421</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Use a helper function to select a valid bubble index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 422</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 423</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> pos =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 424</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;GetBubbleNumber(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 425</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 426</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nEnter number of bubble to erase: &quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 427</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 428</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 429</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (pos &gt;= m_baseNumber)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 430</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 431</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Remove the object from the internal list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 432</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// (this returns the ObjectId stored for it,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 433</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// which we can then use to erase the entity)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 434</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 435</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">ObjectId</span> id =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 436</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_nom.RemoveObject(pos - m_baseNumber);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 437</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 438</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 439</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 440</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 441</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">DBObject</span> obj =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 442</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForWrite);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 443</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; obj.Erase();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 444</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 445</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 446</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 447</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 448</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 449</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to reorder all the bubbles in the drawing,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 450</span>&#0160; &#0160;&#0160; <span style="color: green;">// closing all the gaps between numbers but maintaining</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 451</span>&#0160; &#0160;&#0160; <span style="color: green;">// the current numbering order</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 452</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 453</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;RBS&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 454</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ReorderBubbles()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 455</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 456</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 457</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 458</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 459</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Re-order the bubbles - the IDs returned are</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 460</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// of the objects that need to be renumbered</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 461</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 462</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 463</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_nom.ReorderObjects();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 464</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 465</span>&#0160; &#0160;&#0160; &#0160; RenumberBubbles(doc.Database, ids);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 466</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 467</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 468</span>&#0160; &#0160;&#0160; <span style="color: green;">// Command to highlight a particular bubble</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 469</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 470</span>&#0160; &#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;HLB&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 471</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> HighlightBubble()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 472</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 473</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 474</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 475</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 476</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 477</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 478</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Use our function to select a valid bubble index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 479</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 480</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> pos =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 481</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;GetBubbleNumber(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 482</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 483</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nEnter number of bubble to highlight: &quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 484</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 485</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 486</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (pos &gt;= m_baseNumber)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 487</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 488</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 489</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 490</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 491</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 492</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Get the ObjectId from the index...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 493</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 494</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">ObjectId</span> id =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 495</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; m_nom.GetObjectId(pos - m_baseNumber);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 496</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 497</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (id == <span style="color: #2b91af;">ObjectId</span>.Null)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 498</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 499</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 500</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #a31515;">&quot;\nNumber is not currently used -&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 501</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #a31515;">&quot; nothing to highlight.&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 502</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 503</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 504</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 505</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 506</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// And then open &amp; highlight the entity</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 507</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 508</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Entity</span> ent =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 509</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="color: #2b91af;">Entity</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 510</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;id,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 511</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 512</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 513</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ent.Highlight();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 514</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 515</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 516</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 517</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 518</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 519</span>&#0160; &#0160;&#0160; <span style="color: green;">// Internal helper function to open and retrieve</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 520</span>&#0160; &#0160;&#0160; <span style="color: green;">// the model-space and the block def we care about</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 521</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 522</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">bool</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 523</span>&#0160; &#0160;&#0160; &#0160; GetBlock(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 524</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Database</span> db,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 525</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 526</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">out</span> <span style="color: #2b91af;">BlockTableRecord</span> ms,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 527</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">out</span> <span style="color: #2b91af;">ObjectId</span> blockId</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 528</span>&#0160; &#0160;&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 529</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 530</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">BlockTable</span> bt =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 531</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="color: #2b91af;">BlockTable</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 532</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.BlockTableId,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 533</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 534</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 535</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 536</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (!bt.Has(blockName))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 537</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 538</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 539</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 540</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 541</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 542</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nCannot find block definition \&quot;&quot;</span> +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 543</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; blockName +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 544</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\&quot; in the current drawing.&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 545</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 546</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 547</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;blockId = <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 548</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ms = <span style="color: blue;">null</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 549</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 550</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 551</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 552</span>&#0160; &#0160;&#0160; &#0160; ms =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 553</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 554</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace],</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 555</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 556</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 557</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 558</span>&#0160; &#0160;&#0160; &#0160; blockId = bt[blockName];</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 559</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 560</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 561</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 562</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 563</span>&#0160; &#0160;&#0160; <span style="color: green;">// Internal helper function to retrieve</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 564</span>&#0160; &#0160;&#0160; <span style="color: green;">// attribute info from our block</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 565</span>&#0160; &#0160;&#0160; <span style="color: green;">// (we return the main attribute def</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 566</span>&#0160; &#0160;&#0160; <span style="color: green;">// and then all the &quot;others&quot;)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 567</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 568</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 569</span>&#0160; &#0160;&#0160; &#0160; GetBlockAttributes(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 570</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 571</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">ObjectId</span> blockId,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 572</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">out</span> <span style="color: #2b91af;">AttributeDefinition</span> ad,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 573</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">out</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 574</span>&#0160; &#0160;&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 575</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 576</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">BlockTableRecord</span> blk =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 577</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 578</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; blockId,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 579</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 580</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 581</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 582</span>&#0160; &#0160;&#0160; &#0160; ad = <span style="color: blue;">null</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 583</span>&#0160; &#0160;&#0160; &#0160; other =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 584</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt;();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 585</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 586</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> attId <span style="color: blue;">in</span> blk)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 587</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 588</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObject</span> obj =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 589</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="color: #2b91af;">DBObject</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 590</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; attId,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 591</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 592</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 593</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">AttributeDefinition</span> ad2 =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 594</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; obj <span style="color: blue;">as</span> <span style="color: #2b91af;">AttributeDefinition</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 595</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 596</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (ad2 != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 597</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 598</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (ad2.Tag == attbName)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 599</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 600</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (ad2.Constant)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 601</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 602</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 603</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 604</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 605</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 606</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 607</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nAttribute to change is constant!&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 608</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 609</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 610</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 611</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ad = ad2;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 612</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 613</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 614</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (!ad2.Constant)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 615</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;other.Add(ad2);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 616</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 617</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 618</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 619</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 620</span>&#0160; &#0160;&#0160; <span style="color: green;">// Internal helper function to create a bubble</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 621</span>&#0160; &#0160;&#0160; <span style="color: green;">// at a particular point</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 622</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 623</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">Entity</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 624</span>&#0160; &#0160;&#0160; &#0160; CreateNumberedBubbleAtPoint(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 625</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Database</span> db,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 626</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">BlockTableRecord</span> btr,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 627</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 628</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Point3d</span> pt,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 629</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">ObjectId</span> blockId,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 630</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">AttributeDefinition</span> ad,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 631</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 632</span>&#0160; &#0160;&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 633</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 634</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">//&#0160; Create a new block reference</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 635</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 636</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">BlockReference</span> br =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 637</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">BlockReference</span>(pt, blockId);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 638</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 639</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Add it to the database</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 640</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 641</span>&#0160; &#0160;&#0160; &#0160; br.SetDatabaseDefaults();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 642</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">ObjectId</span> blockRefId = btr.AppendEntity(br);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 643</span>&#0160; &#0160;&#0160; &#0160; tr.AddNewlyCreatedDBObject(br, <span style="color: blue;">true</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 644</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 645</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Create an attribute reference for our main</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 646</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// attribute definition (where we&#39;ll put the</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 647</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// bubble&#39;s number)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 648</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 649</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">AttributeReference</span> ar =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 650</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">AttributeReference</span>();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 651</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 652</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Add it to the database, and set its position, etc.</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 653</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 654</span>&#0160; &#0160;&#0160; &#0160; ar.SetDatabaseDefaults();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 655</span>&#0160; &#0160;&#0160; &#0160; ar.SetAttributeFromBlock(ad, br.BlockTransform);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 656</span>&#0160; &#0160;&#0160; &#0160; ar.Position =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 657</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ad.Position.TransformBy(br.BlockTransform);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 658</span>&#0160; &#0160;&#0160; &#0160; ar.Tag = ad.Tag;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 659</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 660</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Set the bubble&#39;s number</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 661</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 662</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> bubbleNumber =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 663</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_baseNumber +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 664</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_nom.NextObjectNumber(blockRefId);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 665</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 666</span>&#0160; &#0160;&#0160; &#0160; ar.TextString = bubbleNumber.ToString();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 667</span>&#0160; &#0160;&#0160; &#0160; ar.AdjustAlignment(db);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 668</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 669</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Add the attribute to the block reference</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 670</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 671</span>&#0160; &#0160;&#0160; &#0160; br.AttributeCollection.AppendAttribute(ar);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 672</span>&#0160; &#0160;&#0160; &#0160; tr.AddNewlyCreatedDBObject(ar, <span style="color: blue;">true</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 673</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 674</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Now we add attribute references for the</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 675</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// other attribute definitions</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 676</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 677</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">AttributeDefinition</span> ad2 <span style="color: blue;">in</span> other)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 678</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 679</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">AttributeReference</span> ar2 =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 680</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">AttributeReference</span>();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 681</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 682</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ar2.SetAttributeFromBlock(ad2, br.BlockTransform);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 683</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ar2.Position =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 684</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ad2.Position.TransformBy(br.BlockTransform);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 685</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ar2.Tag = ad2.Tag;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 686</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ar2.TextString = ad2.TextString;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 687</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ar2.AdjustAlignment(db);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 688</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 689</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;br.AttributeCollection.AppendAttribute(ar2);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 690</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.AddNewlyCreatedDBObject(ar2, <span style="color: blue;">true</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 691</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 692</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> br;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 693</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 694</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 695</span>&#0160; &#0160;&#0160; <span style="color: green;">// Internal helper function to have the user</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 696</span>&#0160; &#0160;&#0160; <span style="color: green;">// select a valid bubble index</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 697</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 698</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">int</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 699</span>&#0160; &#0160;&#0160; &#0160; GetBubbleNumber(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 700</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Editor</span> ed,</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 701</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">string</span> prompt</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 702</span>&#0160; &#0160;&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 703</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 704</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> upper = m_nom.GetUpperBound();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 705</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (upper &lt;= 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 706</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 707</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 708</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nNo bubbles are currently being managed.&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 709</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 710</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> upper;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 711</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 712</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 713</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">PromptIntegerOptions</span> pio =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 714</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">PromptIntegerOptions</span>(prompt);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 715</span>&#0160; &#0160;&#0160; &#0160; pio.AllowNone = <span style="color: blue;">false</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 716</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 717</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Get the limits from our manager object</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 718</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 719</span>&#0160; &#0160;&#0160; &#0160; pio.LowerLimit =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 720</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_baseNumber +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 721</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_nom.GetLowerBound(<span style="color: blue;">false</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 722</span>&#0160; &#0160;&#0160; &#0160; pio.UpperLimit =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 723</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_baseNumber +</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 724</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;upper;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 725</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 726</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">PromptIntegerResult</span> pir =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 727</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.GetInteger(pio);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 728</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (pir.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 729</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> pir.Value;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 730</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 731</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> -1;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 732</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 733</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 734</span>&#0160; &#0160;&#0160; <span style="color: green;">// Internal helper function to open up a list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 735</span>&#0160; &#0160;&#0160; <span style="color: green;">// of bubbles and reset their number to the</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 736</span>&#0160; &#0160;&#0160; <span style="color: green;">// position in the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 737</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 738</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span> RenumberBubbles(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 739</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Database</span> db, <span style="color: #2b91af;">ObjectIdCollection</span> ids)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 740</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 741</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 742</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db.TransactionManager.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 743</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 744</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 745</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Get the block information</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 746</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 747</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">BlockTableRecord</span> ms;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 748</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">ObjectId</span> blockId;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 749</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 750</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (GetBlock(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 751</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db, tr, <span style="color: blue;">out</span> ms, <span style="color: blue;">out</span> blockId</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 752</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 753</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 754</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Open each bubble to be renumbered</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 755</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 756</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> bid <span style="color: blue;">in</span> ids)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 757</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 758</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (bid != <span style="color: #2b91af;">ObjectId</span>.Null)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 759</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 760</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObject</span> obj =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 761</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.GetObject(bid, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 762</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">BlockReference</span> br = obj <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 763</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (br != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 764</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 765</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (br.BlockTableRecord == blockId)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 766</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 767</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">AttributeCollection</span> ac =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 768</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;br.AttributeCollection;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 769</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 770</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Go through its attributes</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 771</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 772</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> aid <span style="color: blue;">in</span> ac)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 773</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 774</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObject</span> obj2 =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 775</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.GetObject(aid, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 776</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">AttributeReference</span> ar =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 777</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; obj2 <span style="color: blue;">as</span> <span style="color: #2b91af;">AttributeReference</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 778</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 779</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (ar.Tag == attbName)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 780</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 781</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Change the one we care about</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 782</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 783</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ar.UpgradeOpen();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 784</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 785</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">int</span> bubbleNumber =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 786</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; m_baseNumber + m_nom.GetNumber(bid);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 787</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ar.TextString =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 788</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; bubbleNumber.ToString();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 789</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 790</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">break</span>;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 791</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 792</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 793</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 794</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 795</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 796</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 797</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 798</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 799</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 800</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 801</span>&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 802</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 803</span>&#0160; &#0160;<span style="color: green;">// A generic class for managing groups of</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 804</span>&#0160; &#0160;<span style="color: green;">// numbered (and ordered) objects</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 805</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 806</span>&#0160; &#0160;<span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">NumberedObjectManager</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 807</span>&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 808</span>&#0160; &#0160;&#0160; <span style="color: green;">// We need to store a list of object IDs, but</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 809</span>&#0160; &#0160;&#0160; <span style="color: green;">// also a list of free positions in the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 810</span>&#0160; &#0160;&#0160; <span style="color: green;">// (this allows numbering gaps)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 811</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 812</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; m_ids;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 813</span>&#0160; &#0160;&#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: blue;">int</span>&gt; m_free;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 814</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 815</span>&#0160; &#0160;&#0160; <span style="color: green;">// Constructor</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 816</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 817</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> NumberedObjectManager()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 818</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 819</span>&#0160; &#0160;&#0160; &#0160; m_ids =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 820</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 821</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 822</span>&#0160; &#0160;&#0160; &#0160; m_free =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 823</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: blue;">int</span>&gt;();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 824</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 825</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 826</span>&#0160; &#0160;&#0160; <span style="color: green;">// Clear the internal lists</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 827</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 828</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Clear()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 829</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 830</span>&#0160; &#0160;&#0160; &#0160; m_ids.Clear();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 831</span>&#0160; &#0160;&#0160; &#0160; m_free.Clear();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 832</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 833</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 834</span>&#0160; &#0160;&#0160; <span style="color: green;">// Return the first entry in the ObjectId list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 835</span>&#0160; &#0160;&#0160; <span style="color: green;">// (specify &quot;true&quot; if you want to skip</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 836</span>&#0160; &#0160;&#0160; <span style="color: green;">// any null object IDs)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 837</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 838</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> GetLowerBound(<span style="color: blue;">bool</span> ignoreNull)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 839</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 840</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (ignoreNull)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 841</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Define an in-line predicate to check</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 842</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// whether an ObjectId is null</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 843</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 844</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids.FindIndex(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 845</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">delegate</span>(<span style="color: #2b91af;">ObjectId</span> id)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 846</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 847</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> id != <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 848</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 849</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 850</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 851</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> 0;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 852</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 853</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 854</span>&#0160; &#0160;&#0160; <span style="color: green;">// Return the last entry in the ObjectId list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 855</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 856</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> GetUpperBound()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 857</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 858</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> m_ids.Count - 1;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 859</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 860</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 861</span>&#0160; &#0160;&#0160; <span style="color: green;">// Store the specified ObjectId in the next</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 862</span>&#0160; &#0160;&#0160; <span style="color: green;">// available location in the list, and return</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 863</span>&#0160; &#0160;&#0160; <span style="color: green;">// what that is</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 864</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 865</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> NextObjectNumber(<span style="color: #2b91af;">ObjectId</span> id)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 866</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 867</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> pos;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 868</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (m_free.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 869</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 870</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Get the first free position, then remove</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 871</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// it from the &quot;free&quot; list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 872</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 873</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;pos = m_free[0];</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 874</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_free.RemoveAt(0);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 875</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_ids[pos] = id;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 876</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 877</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 878</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 879</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// There are no free slots (gaps in the numbering)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 880</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// so we append it to the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 881</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 882</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;pos = m_ids.Count;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 883</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_ids.Add(id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 884</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 885</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> pos;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 886</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 887</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 888</span>&#0160; &#0160;&#0160; <span style="color: green;">// Go through the list of objects and close any gaps</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 889</span>&#0160; &#0160;&#0160; <span style="color: green;">// by shuffling the list down (easy, as we&#39;re using a</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 890</span>&#0160; &#0160;&#0160; <span style="color: green;">// List&lt;&gt; rather than an array)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 891</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 892</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">ObjectIdCollection</span> ReorderObjects()</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 893</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 894</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Create a collection of ObjectIds we&#39;ll return</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 895</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// for the caller to go and update</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 896</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// (so the renumbering will gets reflected</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 897</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// in the objects themselves)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 898</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 899</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 900</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 901</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 902</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// We&#39;ll go through the &quot;free&quot; list backwards,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 903</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// to allow any changes made to the list of</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 904</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// objects to not affect what we&#39;re doing</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 905</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 906</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">List</span>&lt;<span style="color: blue;">int</span>&gt; rev =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 907</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: blue;">int</span>&gt;(m_free);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 908</span>&#0160; &#0160;&#0160; &#0160; rev.Reverse();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 909</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 910</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">int</span> pos <span style="color: blue;">in</span> rev)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 911</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 912</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// First we remove the object at the &quot;free&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 913</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// position (in theory this should be set to</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 914</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// ObjectId.Null, as the slot has been marked</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 915</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// as blank)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 916</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 917</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_ids.RemoveAt(pos);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 918</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 919</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Now we go through and add the IDs of any</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 920</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// affected objects to the list to return</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 921</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 922</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = pos; i &lt; m_ids.Count; i++)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 923</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 924</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">ObjectId</span> id = m_ids[pos];</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 925</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 926</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Only add non-null objects</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 927</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// not already in the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 928</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 929</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (!ids.Contains(id) &amp;&amp;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 930</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;id != <span style="color: #2b91af;">ObjectId</span>.Null)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 931</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ids.Add(id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 932</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 933</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 934</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 935</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Our free slots have been filled, so clear</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 936</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 937</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 938</span>&#0160; &#0160;&#0160; &#0160; m_free.Clear();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 939</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 940</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> ids;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 941</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 942</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 943</span>&#0160; &#0160;&#0160; <span style="color: green;">// Get the ID of an object at a particular position</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 944</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 945</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">ObjectId</span> GetObjectId(<span style="color: blue;">int</span> pos)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 946</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 947</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (pos &lt; m_ids.Count)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 948</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> m_ids[pos];</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 949</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 950</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 951</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 952</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 953</span>&#0160; &#0160;&#0160; <span style="color: green;">// Get the position of an ObjectId in the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 954</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 955</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> GetNumber(<span style="color: #2b91af;">ObjectId</span> id)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 956</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 957</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (m_ids.Contains(id))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 958</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> m_ids.IndexOf(id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 959</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 960</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> -1;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">&#0160; 961</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 962</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 963</span>&#0160; &#0160;&#0160; <span style="color: green;">// Store an ObjectId in a particular position</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 964</span>&#0160; &#0160;&#0160; <span style="color: green;">// (shuffle == true will &quot;insert&quot; it, shuffling</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 965</span>&#0160; &#0160;&#0160; <span style="color: green;">// the remaining objects down,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 966</span>&#0160; &#0160;&#0160; <span style="color: green;">// shuffle == false will replace the item in</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 967</span>&#0160; &#0160;&#0160; <span style="color: green;">// that slot)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 968</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 969</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> NumberObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 970</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">ObjectId</span> id, <span style="color: blue;">int</span> index, <span style="color: blue;">bool</span> shuffle)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 971</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 972</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// If we&#39;re inserting into the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 973</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 974</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (index &lt; m_ids.Count)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 975</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 976</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (shuffle)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 977</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Insert takes care of the shuffling</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 978</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids.Insert(index, id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 979</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 980</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 981</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// If we&#39;re replacing the existing item, do</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 982</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// so and then make sure the slot is removed</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 983</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// from the &quot;free&quot; list, if applicable</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 984</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 985</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids[index] = id;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 986</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (m_free.Contains(index))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 987</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; m_free.Remove(index);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 988</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 989</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 990</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 991</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 992</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// If we&#39;re appending, shuffling is irrelevant,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 993</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// but we may need to add additional &quot;free&quot; slots</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 994</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// if the position comes after the end</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 995</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 996</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">while</span> (m_ids.Count &lt; index)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 997</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 998</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids.Add(<span style="color: #2b91af;">ObjectId</span>.Null);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">&#0160; 999</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_free.Add(m_ids.LastIndexOf(<span style="color: #2b91af;">ObjectId</span>.Null));</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1000</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_free.Sort();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1001</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1002</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_ids.Add(id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1003</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1004</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1005</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1006</span>&#0160; &#0160;&#0160; <span style="color: green;">// Move an ObjectId already in the list to a</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1007</span>&#0160; &#0160;&#0160; <span style="color: green;">// particular position</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1008</span>&#0160; &#0160;&#0160; <span style="color: green;">// (ObjectIds between the two positions will</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1009</span>&#0160; &#0160;&#0160; <span style="color: green;">// get shuffled down automatically)</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1010</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1011</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">ObjectIdCollection</span> MoveObject(</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1012</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> from, <span style="color: blue;">int</span> to)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1013</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1014</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1015</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1016</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1017</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (from &lt; m_ids.Count &amp;&amp;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1018</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; to &lt; m_ids.Count)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1019</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1020</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (from != to)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1021</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1022</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">ObjectId</span> id = m_ids[from];</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1023</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids.RemoveAt(from);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1024</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids.Insert(to, id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1025</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1026</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">int</span> start = (from &lt; to ? from : to);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1027</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">int</span> end = (from &lt; to ? to : from);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1028</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1029</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = start; i &lt;= end; i++)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1030</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1031</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ids.Add(m_ids[i]);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1032</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1033</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1034</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Now need to adjust/recreate &quot;free&quot; list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1035</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_free.Clear();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1036</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">for</span> (<span style="color: blue;">int</span> j = 0; j &lt; m_ids.Count; j++)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1037</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1038</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (m_ids[j] == <span style="color: #2b91af;">ObjectId</span>.Null)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1039</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; m_free.Add(j);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1040</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1041</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1042</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> ids;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1043</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1044</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1045</span>&#0160; &#0160;&#0160; <span style="color: green;">// Remove an ObjectId from the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1046</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1047</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> RemoveObject(<span style="color: #2b91af;">ObjectId</span> id)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1048</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1049</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Check it&#39;s non-null and in the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1050</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1051</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (id != <span style="color: #2b91af;">ObjectId</span>.Null &amp;&amp;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1052</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_ids.Contains(id))</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1053</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1054</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">int</span> pos = m_ids.IndexOf(id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1055</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;RemoveObject(pos);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1056</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> pos;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1057</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1058</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> -1;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1059</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1060</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1061</span>&#0160; &#0160;&#0160; <span style="color: green;">// Remove the ObjectId at a particular position</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1062</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1063</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: #2b91af;">ObjectId</span> RemoveObject(<span style="color: blue;">int</span> pos)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1064</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1065</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Get the ObjectId in the specified position,</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1066</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// making sure it&#39;s non-null</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1067</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1068</span>&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">ObjectId</span> id = m_ids[pos];</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1069</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (id != <span style="color: #2b91af;">ObjectId</span>.Null)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1070</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1071</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Null out the position and add it to the</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1072</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// &quot;free&quot; list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1073</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1074</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_ids[pos] = <span style="color: #2b91af;">ObjectId</span>.Null;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1075</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_free.Add(pos);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1076</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_free.Sort();</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1077</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1078</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">return</span> id;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: red;">1079</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1080</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1081</span>&#0160; &#0160;&#0160; <span style="color: green;">// Dump out the object list information</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1082</span>&#0160; &#0160;&#0160; <span style="color: green;">// as well as the &quot;free&quot; slots</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1083</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1084</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> DumpInfo(<span style="color: #2b91af;">Editor</span> ed)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1085</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1086</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (m_ids.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1087</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1088</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(<span style="color: #a31515;">&quot;\nIdx ObjectId&quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1089</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1090</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">int</span> index = 0;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1091</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="color: blue;">in</span> m_ids)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1092</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\n{0} {1}&quot;</span>, index++, id);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1093</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1094</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1095</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (m_free.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1096</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1097</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(<span style="color: #a31515;">&quot;\n\nFree list: &quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1098</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1099</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">foreach</span> (<span style="color: blue;">int</span> pos <span style="color: blue;">in</span> m_free)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1100</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;{0} &quot;</span>, pos);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1101</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1102</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1103</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1104</span>&#0160; &#0160;&#0160; <span style="color: green;">// Remove the initial n items from the list</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1105</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1106</span>&#0160; &#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> RebaseList(<span style="color: blue;">int</span> start)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1107</span>&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1108</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// First we remove the ObjectIds</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1109</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1110</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i=0; i &lt; start; i++)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1111</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;m_ids.RemoveAt(0);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1112</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1113</span>&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Then we go through the &quot;free&quot; list...</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1114</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1115</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> idx = 0;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1116</span>&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">while</span> (idx &lt; m_free.Count)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1117</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1118</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (m_free[idx] &lt; start)</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1119</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Remove any that refer to the slots</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1120</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// we&#39;ve removed </span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1121</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_free.RemoveAt(idx);</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1122</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1123</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1124</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Subtracting the number of slots</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1125</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// we&#39;ve removed from the other items</span></p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1126</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; m_free[idx] -= start;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1127</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; idx++;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1128</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1129</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1130</span>&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1131</span>&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: #2b91af;">1132</span> }</p>
</div>
<p>The above code defines four new commands which move, delete and highlight a bubble, and reorder the bubble list. I could probably have used better terminology for some of the command-names - the MB (Move Bubble) command does not move the physical position of the block in the drawing, it moves the bubble inside the list (i.e. it changes the bubble&#39;s number while maintaining the consistency of the list). Similarly, RBS (Reorder BubbleS) actually just compacts the list, removing unnecessary gaps in the list created by deletion. Anyway, the user is notified of the additional commands by lines 46-50, and the commands themselves are implemented by lines 370-517. MB, DB (Delete Bubble) and HLB (HighLight Bubble) all use a new helper function, GetBubbleNumber(), defined by lines 695-733, which asks the user to select a valid bubble from the list, which will then get moved, deleted or highlighted, as appropriate.</p>
<p>The other new helper function which is defined outside the NumberedObjectManager class (as the function depends on the specific implementation of our object numbering, i.e. with the value stored in an attribute in a block), is RenumberBubbles(), defined by lines 734-800. This function opens up a list of bubbles and sets their visible number to the one stored in the NamedObjectManager object. It is used by both MB and RBS.</p>
<p>To support these new commands, the NamedObjectManager class has also been extended in two new sections of the above code. The first new chunk of code (lines 888-961) implements new methods ReorderObjects() which again, is really a list compaction function and then GetObjectId() and GetNumber(), which - as you&#39;d expect - return an ObjectId at a particular position and a position for a particular ObjectId. The next chunk (lines 1006-1079) implements MoveObject(), which moves an object from one place to another - shuffling the intermediate bubbles around, as needed - and two versions of RemoveObject(), depending on whether you wish to select the object by its ID or its position.</p>
<p>Something important to note about this implementation: so far we haven&#39;t dealt with what happens should the user choose to undo these commands: as the objects we&#39;re creating are not managed by AutoCAD (they are not stored in the drawing, for instance), their state is not captured in the undo filer, and so will not be affected by undo. But the geometry they refer to will, of course, so there is substantial potential for our list getting out of sync with reality. The easy (and arguably the best) way to get around this is to check for undo-related commands to be executed, and invalidate our list at that point (providing a suitable notification to the user, requesting that they run LNS again once done with their undoing &amp; redoing). The current implementation <u>does not do this</u>.</p>
<p>Let&#39;s now take our new commands for a quick spin...</p>
<p>We&#39;re going to take our <a href="http://through-the-interface.typepad.com/through_the_interface/files/bubbles_1.dwg">previously-created drawing</a>, as a starting point, and use our new commands on it.</p>
<p>Let&#39;s start with DB:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: courier new;">
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">LNS</span></p>
<p style="font-size: 8pt; margin: 0px;">Lowest index is 1. Make this the start of the list? [Yes/No] &lt;Yes&gt;: <span style="color: red;">Yes</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DB</span></p>
<p style="font-size: 8pt; margin: 0px;">Enter number of bubble to erase: <span style="color: red;">3</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DB</span></p>
<p style="font-size: 8pt; margin: 0px;">Enter number of bubble to erase: <span style="color: red;">5</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DB</span></p>
<p style="font-size: 8pt; margin: 0px;">Enter number of bubble to erase: <span style="color: red;">15</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DB</span></p>
<p style="font-size: 8pt; margin: 0px;">Enter number of bubble to erase: <span style="color: red;">16</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DMP</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">Idx ObjectId</p>
<p style="font-size: 8pt; margin: 0px;">0 (2129683752)</p>
<p style="font-size: 8pt; margin: 0px;">1 (2129683776)</p>
<p style="font-size: 8pt; margin: 0px;">2 (0)</p>
<p style="font-size: 8pt; margin: 0px;">3 (2129683824)</p>
<p style="font-size: 8pt; margin: 0px;">4 (0)</p>
<p style="font-size: 8pt; margin: 0px;">5 (2129683872)</p>
<p style="font-size: 8pt; margin: 0px;">6 (2129683896)</p>
<p style="font-size: 8pt; margin: 0px;">7 (2129683920)</p>
<p style="font-size: 8pt; margin: 0px;">8 (2129683944)</p>
<p style="font-size: 8pt; margin: 0px;">9 (2129683968)</p>
<p style="font-size: 8pt; margin: 0px;">10 (2129683992)</p>
<p style="font-size: 8pt; margin: 0px;">11 (2129684016)</p>
<p style="font-size: 8pt; margin: 0px;">12 (2129684040)</p>
<p style="font-size: 8pt; margin: 0px;">13 (2129684064)</p>
<p style="font-size: 8pt; margin: 0px;">14 (0)</p>
<p style="font-size: 8pt; margin: 0px;">15 (0)</p>
<p style="font-size: 8pt; margin: 0px;">16 (2129684136)</p>
<p style="font-size: 8pt; margin: 0px;">17 (2129684160)</p>
<p style="font-size: 8pt; margin: 0px;">18 (2129684184)</p>
<p style="font-size: 8pt; margin: 0px;">19 (2129684208)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">Free list: 2 4 14 15</p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
</div>
<p>As you can see, we&#39;ve ended up with a few free slots in our list (and you&#39;ll note you need to add our &quot;base number&quot; (1) to get to the visible number). Here&#39;s the state of the drawing at this point:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Deleted%20Bubbles.png"><img alt="Deleted Bubbles" border="0" height="119" src="/assets/Deleted%20Bubbles_thumb.png" style="border-width: 0px;" width="240" /></a></p>
<p>Now let&#39;s try MB:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: courier new;">
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">MB</span></p>
<p style="font-size: 8pt; margin: 0px;">Enter number of bubble to move: <span style="color: red;">7</span></p>
<p style="font-size: 8pt; margin: 0px;">Enter destination position: <span style="color: red;">2</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DMP</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">Idx ObjectId</p>
<p style="font-size: 8pt; margin: 0px;">0 (2129683752)</p>
<p style="font-size: 8pt; margin: 0px;">1 (2129683896)</p>
<p style="font-size: 8pt; margin: 0px;">2 (2129683776)</p>
<p style="font-size: 8pt; margin: 0px;">3 (0)</p>
<p style="font-size: 8pt; margin: 0px;">4 (2129683824)</p>
<p style="font-size: 8pt; margin: 0px;">5 (0)</p>
<p style="font-size: 8pt; margin: 0px;">6 (2129683872)</p>
<p style="font-size: 8pt; margin: 0px;">7 (2129683920)</p>
<p style="font-size: 8pt; margin: 0px;">8 (2129683944)</p>
<p style="font-size: 8pt; margin: 0px;">9 (2129683968)</p>
<p style="font-size: 8pt; margin: 0px;">10 (2129683992)</p>
<p style="font-size: 8pt; margin: 0px;">11 (2129684016)</p>
<p style="font-size: 8pt; margin: 0px;">12 (2129684040)</p>
<p style="font-size: 8pt; margin: 0px;">13 (2129684064)</p>
<p style="font-size: 8pt; margin: 0px;">14 (0)</p>
<p style="font-size: 8pt; margin: 0px;">15 (0)</p>
<p style="font-size: 8pt; margin: 0px;">16 (2129684136)</p>
<p style="font-size: 8pt; margin: 0px;">17 (2129684160)</p>
<p style="font-size: 8pt; margin: 0px;">18 (2129684184)</p>
<p style="font-size: 8pt; margin: 0px;">19 (2129684208)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">Free list: 3 5 14 15</p>
</div>
<p>This results in the item in internal slot 6 being moved to internal slot 1 (remember that base number :-) and the objects between being shuffled along. Here&#39;s what&#39;s on the screen at this point:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Moved%20Bubbles.png"><img alt="Moved Bubbles" border="0" height="118" src="/assets/Moved%20Bubbles_thumb.png" width="240" /></a></p>
<p>And finally we&#39;ll compact the list - removing those four free slots - with our RBS command:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: courier new;">
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">RBS</span></p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">DMP</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">Idx ObjectId</p>
<p style="font-size: 8pt; margin: 0px;">0 (2129683752)</p>
<p style="font-size: 8pt; margin: 0px;">1 (2129683896)</p>
<p style="font-size: 8pt; margin: 0px;">2 (2129683776)</p>
<p style="font-size: 8pt; margin: 0px;">3 (2129683824)</p>
<p style="font-size: 8pt; margin: 0px;">4 (2129683872)</p>
<p style="font-size: 8pt; margin: 0px;">5 (2129683920)</p>
<p style="font-size: 8pt; margin: 0px;">6 (2129683944)</p>
<p style="font-size: 8pt; margin: 0px;">7 (2129683968)</p>
<p style="font-size: 8pt; margin: 0px;">8 (2129683992)</p>
<p style="font-size: 8pt; margin: 0px;">9 (2129684016)</p>
<p style="font-size: 8pt; margin: 0px;">10 (2129684040)</p>
<p style="font-size: 8pt; margin: 0px;">11 (2129684064)</p>
<p style="font-size: 8pt; margin: 0px;">12 (2129684136)</p>
<p style="font-size: 8pt; margin: 0px;">13 (2129684160)</p>
<p style="font-size: 8pt; margin: 0px;">14 (2129684184)</p>
<p style="font-size: 8pt; margin: 0px;">15 (2129684208)</p>
</div>
<p>And here&#39;s how that looks:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Reordered%20Bubbles.png"><img alt="Reordered Bubbles" border="0" height="119" src="/assets/Reordered%20Bubbles_thumb.png" style="border-width: 0px;" width="240" /></a></p>
<p>I don&#39;t currently have any further enhancements planned for this application. Feel free to post a comment or send me an email if there&#39;s a particular direction in which you&#39;d like to see it go. For instance, is it interesting to see support for prefixes/suffixes...?</p>
