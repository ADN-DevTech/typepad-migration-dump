---
layout: "post"
title: "An automatic numbering system for AutoCAD blocks using .NET - Part 4"
date: "2008-05-16 17:17:34"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Dimensions"
  - "Documents"
  - "Notification / Events"
  - "Selection"
original_url: "https://www.keanw.com/2008/05/an-automatic--3.html "
typepad_basename: "an-automatic--3"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/an-automatic-nu.html">the original post in this series</a>, we introduced a basic application to number AutoCAD objects, specifically blocks with attributes. In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/an-automatic--1.html">the second post</a> we extended this to make use of a generic numbering system for drawing-resident AutoCAD objects, and in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/05/an-automatic--2.html">the third post</a> we implemented additional commands to take advantage of this new &quot;kernel&quot;.</p>

<p>In this post we're going to extend the application in a few ways: firstly we're going to support duplicates, so that the LNS command which parses the current drawing to understand its numbers will support automatic and semi-automatic renumbering of objects with duplicate numbers. In addition there are a number of new event handlers that have been introduced to automatically renumber objects on creation/insertion/copy, and also to clear the numbering system when a user undoes any action in the drawing (just to be safe :-).</p>

<p>While introducing these event handlers I decide to switch the approach for associating data with a drawing: rather than declaring the variables at a class level and assuming they would be duplicated instantiated appropriately per-document, as shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/perdocument_dat_1.html">this previous post</a>, I decided to encapsulate the variables in a class and specifically instantiate that class and store it per-document, as shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/perdocument_dat_2.html">this previous post</a>.</p>

<p>Here's the updated C# code, with the changed &amp; new lines in <span style="COLOR: red">red</span>, and here is <a href="http://through-the-interface.typepad.com/through_the_interface/files/auto-bubble-creation-part4.cs">the complete source file</a> to save you having to strip line numbers:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 1</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 2</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 3</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 4</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 5</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 6</span> <span style="COLOR: blue">using</span> System.Collections.Generic;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp; 7</span> <span style="COLOR: blue">using</span> System.Collections;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 8</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 9</span> <span style="COLOR: blue">namespace</span> AutoNumberedBubbles</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;10</span> {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;11</span>&nbsp; &nbsp;<span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">Commands</span> : <span style="color: #2b91af;">IExtensionApplication</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;12</span>&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;13</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Strings identifying the block</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;14</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and the attribute name to use</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;15</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;16</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> blockName = <span style="color: #a31515;">&quot;BUBBLE&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;17</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> attbName = <span style="color: #a31515;">&quot;NUMBER&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;18</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;19</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// A string to identify our application's</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;20</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// data in per-document UserData</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;21</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;22</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> dataKey = <span style="color: #a31515;">&quot;TTIFBubbles&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;23</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;24</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Define a class for our custom data</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;25</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;26</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">BubbleData</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;27</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;28</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// A separate object to manage our numbering</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;29</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;30</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">NumberedObjectManager</span> m_nom;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;31</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">NumberedObjectManager</span> Nom</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;32</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;33</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_nom; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;34</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;35</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;36</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// A &quot;base&quot; index (for the start of the list)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;37</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;38</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">int</span> m_baseNumber;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;39</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> BaseNumber</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;40</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;41</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_baseNumber; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;42</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">set</span> { m_baseNumber = <span style="COLOR: blue">value</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;43</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;44</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;45</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// A list of blocks added to the database</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;46</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// which we will then renumber</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;47</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;48</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; m_blocksAdded;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;49</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; BlocksToRenumber</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;50</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;51</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_blocksAdded; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;52</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;53</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;54</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Constructor</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;55</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;56</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> BubbleData()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;57</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;58</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_baseNumber = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;59</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_nom = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">NumberedObjectManager</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;60</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_blocksAdded = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;61</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;62</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;63</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Method to clear the contents</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;64</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;65</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Reset()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;66</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;67</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_baseNumber = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;68</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_nom.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;69</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_blocksAdded.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;70</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;71</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;72</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;73</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Constructor</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;74</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;75</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> Commands()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;76</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;77</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;78</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;79</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Functions called on initialization &amp; termination</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;80</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;81</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;82</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;83</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;84</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;85</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DocumentCollection</span> dm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;86</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;87</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc = dm.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;88</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;89</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;90</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;91</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;92</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nLNS&nbsp; Load numbering settings by analyzing the current drawing&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;93</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nDMP&nbsp; Print internal numbering information&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;94</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nBAP&nbsp; Create bubbles at points&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;95</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nBIC&nbsp; Create bubbles at the center of circles&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;96</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nMB&nbsp; Move a bubble in the list&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;97</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nDB&nbsp; Delete a bubble&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;98</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nRBS&nbsp; Reorder the bubbles, to close gaps caused by deletion&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;99</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nHLB&nbsp; Highlight a particular bubble&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 100</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 101</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 102</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Hook into some events, to detect and renumber</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 103</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// blocks added to the database</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 104</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 105</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.ObjectAppended +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 106</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">ObjectEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 107</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db_ObjectAppended</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 108</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 109</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;dm.DocumentCreated +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 110</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">DocumentCollectionEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 111</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; dm_DocumentCreated</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 112</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 113</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;dm.DocumentLockModeWillChange +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 114</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">DocumentLockModeWillChangeEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 115</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; dm_DocumentLockModeWillChange</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 116</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 117</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 118</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;doc.CommandEnded +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 119</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">delegate</span>(<span style="COLOR: blue">object</span> sender, <span style="color: #2b91af;">CommandEventArgs</span> e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 120</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 121</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (e.GlobalCommandName == <span style="color: #a31515;">&quot;UNDO&quot;</span> ||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 122</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; e.GlobalCommandName == <span style="color: #a31515;">&quot;U&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 123</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 124</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 125</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nUndo invalidates bubble numbering: call&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 126</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot; LNS to reload the numbers for this drawing&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 127</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 128</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData((<span style="color: #2b91af;">Document</span>)sender).Reset();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 129</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 130</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; };</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 131</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 132</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">catch</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 133</span>&nbsp; &nbsp;&nbsp; &nbsp; { }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 134</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 135</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 136</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 137</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 138</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 139</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 140</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Method to retrieve (or create) the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 141</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// BubbleData object for a particular</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 142</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// document</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 143</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 144</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">BubbleData</span> GetBubbleData(<span style="color: #2b91af;">Document</span> doc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 145</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 146</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Hashtable</span> ud = doc.UserData;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 147</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 148</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ud[dataKey] <span style="COLOR: blue">as</span> <span style="color: #2b91af;">BubbleData</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 149</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 150</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bd == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 151</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 152</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> obj = ud[dataKey];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 153</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (obj == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 154</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 155</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Nothing there</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 156</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 157</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bd = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">BubbleData</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 158</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ud.Add(dataKey, bd);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 159</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 160</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 161</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 162</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Found something different instead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 163</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 164</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 165</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 166</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;Found an object of type \&quot;&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 167</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; obj.GetType().ToString() +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 168</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\&quot; instead of BubbleData.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 169</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 170</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 171</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> bd;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 172</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 173</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 174</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Do the same for a particular database</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 175</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 176</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">BubbleData</span> GetBubbleData(<span style="color: #2b91af;">Database</span> db)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 177</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 178</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">DocumentCollection</span> dm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 179</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 180</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 181</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;dm.GetDocument(db);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 182</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 183</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 184</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 185</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// When a new document is created, attach our</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 186</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// ObjectAppended event handler to the new</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 187</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// database</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 188</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 189</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">void</span> dm_DocumentCreated(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 190</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 191</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">DocumentCollectionEventArgs</span> e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 192</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 193</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 194</span>&nbsp; &nbsp;&nbsp; &nbsp; e.Document.Database.ObjectAppended +=</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 195</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">ObjectEventHandler</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 196</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db_ObjectAppended</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 197</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 198</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 199</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 200</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// When an object is appended to a database,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 201</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// add it to a list we care about if it's a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 202</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// BlockReference</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 203</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 204</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">void</span> db_ObjectAppended(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 205</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 206</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectEventArgs</span> e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 207</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 208</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 209</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BlockReference</span> br =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 210</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;e.DBObject <span style="COLOR: blue">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 211</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (br != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 212</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 213</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 214</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; GetBubbleData(e.DBObject.Database);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 215</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.BlocksToRenumber.Add(br.ObjectId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 216</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 217</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 218</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 219</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// When the command (or action) is over,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 220</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// take the list of blocks to renumber and</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 221</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// go through them, renumbering each one</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 222</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 223</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">void</span> dm_DocumentLockModeWillChange(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 224</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">object</span> sender,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 225</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">DocumentLockModeWillChangeEventArgs</span> e</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 226</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 227</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 228</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc = e.Document;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 229</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 230</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 231</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 232</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bd.BlocksToRenumber.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 233</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 234</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 235</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 236</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 237</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 238</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 239</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> bid <span style="COLOR: blue">in</span> bd.BlocksToRenumber)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 240</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 241</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 242</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 243</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockReference</span> br =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 244</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(bid, <span style="color: #2b91af;">OpenMode</span>.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 245</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 246</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (br != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 247</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 248</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 249</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 250</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;br.BlockTableRecord,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 251</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 252</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 253</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (btr.Name == blockName)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 254</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 255</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">AttributeCollection</span> ac =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 256</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;br.AttributeCollection;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 257</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 258</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> aid <span style="COLOR: blue">in</span> ac)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 259</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 260</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 261</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(aid, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 262</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeReference</span> ar =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 263</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj <span style="COLOR: blue">as</span> <span style="color: #2b91af;">AttributeReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 264</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 265</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ar.Tag == attbName)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 266</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 267</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Change the one we care about</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 268</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 269</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ar.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 270</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 271</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> bubbleNumber =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 272</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.BaseNumber +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 273</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Nom.NextObjectNumber(bid);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 274</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ar.TextString =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 275</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bubbleNumber.ToString();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 276</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 277</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 278</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 279</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 280</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 281</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 282</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 283</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">catch</span> { }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 284</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 285</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 286</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bd.BlocksToRenumber.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 287</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 288</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 289</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 290</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 291</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to extract and display information</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 292</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// about the internal numbering</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 293</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 294</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DMP&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 295</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> DumpNumberingInformation()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 296</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 297</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 298</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 299</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 300</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 301</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 302</span>&nbsp; &nbsp;&nbsp; &nbsp; bd.Nom.DumpInfo(ed);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 303</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 304</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 305</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to analyze the current document and</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 306</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// understand which indeces have been used and</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 307</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// which are currently free</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 308</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 309</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;LNS&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 310</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> LoadNumberingSettings()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 311</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 312</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 313</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 314</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 315</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 316</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 317</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 318</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 319</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// We need to clear any internal state</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 320</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// already collected</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 321</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 322</span>&nbsp; &nbsp;&nbsp; &nbsp; bd.Reset();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 323</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 324</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Select all the blocks in the current drawing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 325</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 326</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">TypedValue</span>[] tvs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 327</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">TypedValue</span>[1] {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 328</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">TypedValue</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 329</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">int</span>)<span style="color: #2b91af;">DxfCode</span>.Start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 330</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;INSERT&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 331</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 332</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; };</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 333</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">SelectionFilter</span> sf =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 334</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">SelectionFilter</span>(tvs);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 335</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 336</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">PromptSelectionResult</span> psr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 337</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.SelectAll(sf);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 338</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 339</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// If it succeeded and we have some blocks...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 340</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 341</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (psr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 342</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; psr.Value.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 343</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 344</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 345</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 346</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 347</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 348</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// First get the modelspace and the ID</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 349</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// of the block for which we're searching</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 350</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 351</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">BlockTableRecord</span> ms;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 352</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">ObjectId</span> blockId;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 353</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 354</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (GetBlock(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 355</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db, tr, <span style="COLOR: blue">out</span> ms, <span style="COLOR: blue">out</span> blockId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 356</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 357</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 358</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// For each block reference in the drawing...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 359</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 360</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">SelectedObject</span> o <span style="COLOR: blue">in</span> psr.Value)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 361</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 362</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 363</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(o.ObjectId, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 364</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockReference</span> br = obj <span style="COLOR: blue">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 365</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (br != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 366</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 367</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// If it's the one we care about...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 368</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 369</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (br.BlockTableRecord == blockId)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 370</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 371</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Check its attribute references...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 372</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 373</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> pos = -1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 374</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">AttributeCollection</span> ac =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 375</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;br.AttributeCollection;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 376</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 377</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="COLOR: blue">in</span> ac)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 378</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 379</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 380</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 381</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeReference</span> ar =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 382</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj2 <span style="COLOR: blue">as</span> <span style="color: #2b91af;">AttributeReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 383</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 384</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// When we find the attribute</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 385</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// we care about...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 386</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 387</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ar.Tag == attbName)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 388</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 389</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 390</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 391</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Attempt to extract the number from</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 392</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// the text string property... use a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 393</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// try-catch block just in case it is</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 394</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// non-numeric</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 395</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 396</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 397</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span>.Parse(ar.TextString);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 398</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 399</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Add the object at the appropriate</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 400</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 401</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 402</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Nom.NumberObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 403</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;o.ObjectId, pos, <span style="COLOR: blue">false</span>, <span style="COLOR: blue">true</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 404</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 405</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 406</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">catch</span> { }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 407</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 408</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 409</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 410</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 411</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 412</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 413</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 414</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 415</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 416</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Once we have analyzed all the block references...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 417</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 418</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> start = bd.Nom.GetLowerBound(<span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 419</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 420</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// If the first index is non-zero, ask the user if</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 421</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// they want to rebase the list to begin at the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 422</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// current start position</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 423</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 424</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (start &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 425</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 426</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 427</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nLowest index is {0}. &quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 428</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; start</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 429</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 430</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PromptKeywordOptions</span> pko =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 431</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 432</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;Make this the start of the list?&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 433</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 434</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.AllowNone = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 435</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;Yes&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 436</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;No&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 437</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Default = <span style="color: #a31515;">&quot;Yes&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 438</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 439</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PromptResult</span> pkr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 440</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.GetKeywords(pko);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 441</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 442</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (pkr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 443</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Reset();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 444</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 445</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 446</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pkr.StringResult == <span style="color: #a31515;">&quot;Yes&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 447</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 448</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// We store our own base number</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 449</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (the object used to manage objects</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 450</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// always uses zero-based indeces)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 451</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 452</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.BaseNumber = start;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 453</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.Nom.RebaseList(bd.BaseNumber);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 454</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 455</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 456</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 457</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 458</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// We found duplicates in the numbering...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 459</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 460</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (bd.Nom.HasDuplicates())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 461</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 462</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Ask how to fix the duplicates</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 463</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 464</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PromptKeywordOptions</span> pko =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 465</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 466</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;Blocks contain duplicate numbers. &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 467</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;How do you want to renumber?&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 468</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 469</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.AllowNone = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 470</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;Automatically&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 471</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;Individually&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 472</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Add(<span style="color: #a31515;">&quot;Not&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 473</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pko.Keywords.Default = <span style="color: #a31515;">&quot;Automatically&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 474</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 475</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">PromptResult</span> pkr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 476</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.GetKeywords(pko);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 477</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 478</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">bool</span> bAuto = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 479</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">bool</span> bManual = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 480</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 481</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (pkr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 482</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Reset();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 483</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 484</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 485</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pkr.StringResult == <span style="color: #a31515;">&quot;Automatically&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 486</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bAuto = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 487</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span> <span style="COLOR: blue">if</span> (pkr.StringResult == <span style="color: #a31515;">&quot;Individually&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 488</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bManual = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 489</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 490</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Whether fixing automatically or manually</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 491</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// we will iterate through the duplicate list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 492</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 493</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bAuto || bManual)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 494</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 495</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectIdCollection</span> idc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 496</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 497</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 498</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get each entry in the duplicate list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 499</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 500</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">SortedDictionary</span>&lt;<span style="COLOR: blue">int</span>,<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt; dups =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 501</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bd.Nom.Duplicates;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 502</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 503</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">KeyValuePair</span>&lt;<span style="COLOR: blue">int</span>,<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt; dup <span style="COLOR: blue">in</span> dups</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 504</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 505</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 506</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// The position is the key in the entry</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 507</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and the list of IDs is the value</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 508</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (we take a copy, so we can modify it</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 509</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// without affecting the original)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 510</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 511</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> pos = dup.Key;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 512</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 513</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;(dup.Value);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 514</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 515</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// For automatic renumbering there's no</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 516</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// user interaction</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 517</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 518</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (bAuto)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 519</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 520</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="COLOR: blue">in</span> ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 521</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 522</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.Nom.NextObjectNumber(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 523</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;idc.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 524</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 525</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 526</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span> <span style="COLOR: green">// bManual</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 527</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 528</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// For manual renumbering we ask the user</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 529</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// to select the block to keep, then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 530</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// we renumber the rest automatically</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 531</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 532</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.UpdateScreen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 533</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 534</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ids.Add(bd.Nom.GetObjectId(pos));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 535</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; HighlightBubbles(db, ids, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 536</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 537</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 538</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;\n\nHighlighted blocks &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 539</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;with number {0}. &quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 540</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pos + bd.BaseNumber</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 541</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 542</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 543</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">bool</span> finished = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 544</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">while</span> (!finished)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 545</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 546</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">PromptEntityOptions</span> peo =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 547</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 548</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;Select block to keep (others &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 549</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;will be renumbered automatically): &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 550</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 551</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;peo.SetRejectMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 552</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nEntity must be a block.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 553</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 554</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;peo.AddAllowedClass(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 555</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">typeof</span>(<span style="color: #2b91af;">BlockReference</span>), <span style="COLOR: blue">false</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 556</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">PromptEntityResult</span> per =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 557</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetEntity(peo);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 558</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 559</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (per.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 560</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 561</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bd.Reset();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 562</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 563</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 564</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 565</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 566</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// A block has been selected, so we</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 567</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// make sure it is one of the ones</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 568</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// we highlighted for the user</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 569</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 570</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (ids.Contains(per.ObjectId))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 571</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 572</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Leave the selected block alone</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 573</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// by removing it from the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 574</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 575</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ids.Remove(per.ObjectId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 576</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 577</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// We then renumber each block in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 578</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 579</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 580</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="COLOR: blue">in</span> ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 581</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 582</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.Nom.NextObjectNumber(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 583</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;idc.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 584</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 585</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; RenumberBubbles(db, idc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 586</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; idc.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 587</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 588</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Let's unhighlight our selected</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 589</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// block (renumbering will do this</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 590</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// for the others)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 591</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 592</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; redraw =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 593</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;(1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 594</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; redraw.Add(per.ObjectId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 595</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; HighlightBubbles(db, redraw, <span style="COLOR: blue">false</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 596</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 597</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; finished = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 598</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 599</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 600</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 601</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 602</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;\nBlock selected is not &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 603</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;numbered with {0}. &quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 604</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pos + bd.BaseNumber</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 605</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 606</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 607</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 608</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 609</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 610</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 611</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;RenumberBubbles(db, idc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 612</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 613</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Nom.Duplicates.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 614</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.UpdateScreen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 615</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 616</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 617</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 618</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 619</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 620</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Take a list of objects and either highlight</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 621</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// or unhighlight them, depending on the flag</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 622</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 623</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> HighlightBubbles(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 624</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db, <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; ids, <span style="COLOR: blue">bool</span> highlight)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 625</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 626</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 627</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 628</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 629</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 630</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="COLOR: blue">in</span> ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 631</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 632</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Entity</span> ent =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 633</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="color: #2b91af;">Entity</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 634</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;id,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 635</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 636</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 637</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (highlight)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 638</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Highlight();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 639</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 640</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Draw();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 641</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 642</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 643</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 644</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 645</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 646</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to create bubbles at points selected</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 647</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// by the user - loops until cancelled</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 648</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 649</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;BAP&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 650</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> BubblesAtPoints()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 651</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 652</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 653</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 654</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 655</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 656</span>&nbsp; &nbsp;&nbsp; &nbsp; Autodesk.AutoCAD.ApplicationServices.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 657</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">TransactionManager</span> tm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 658</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;doc.TransactionManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 659</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 660</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 661</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tm.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 662</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 663</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 664</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get the information about the block</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 665</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// and attribute definitions we care about</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 666</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 667</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockTableRecord</span> ms;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 668</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectId</span> blockId;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 669</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeDefinition</span> ad;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 670</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 671</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 672</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (GetBlock(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 673</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db, tr, <span style="COLOR: blue">out</span> ms, <span style="COLOR: blue">out</span> blockId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 674</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 675</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 676</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; GetBlockAttributes(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 677</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr, blockId, <span style="COLOR: blue">out</span> ad, <span style="COLOR: blue">out</span> other</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 678</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 679</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 680</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// By default the modelspace is returned to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 681</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// us in read-only state</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 682</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 683</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ms.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 684</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 685</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Loop until cancelled</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 686</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 687</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">bool</span> finished = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 688</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">while</span> (!finished)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 689</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 690</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">PromptPointOptions</span> ppo =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 691</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptPointOptions</span>(<span style="color: #a31515;">&quot;\nSelect point: &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 692</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ppo.AllowNone = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 693</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 694</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">PromptPointResult</span> ppr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 695</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.GetPoint(ppo);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 696</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (ppr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 697</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;finished = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 698</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 699</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Call a function to create our bubble</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 700</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;CreateNumberedBubbleAtPoint(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 701</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db, ms, tr, ppr.Value,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 702</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; blockId, ad, other</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 703</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 704</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tm.QueueForGraphicsFlush();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 705</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tm.FlushGraphics();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 706</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 707</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 708</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 709</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 710</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 711</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 712</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to create a bubble at the center of</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 713</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// each of the selected circles</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 714</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 715</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;BIC&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 716</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> BubblesInCircles()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 717</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 718</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 719</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 720</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 721</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 722</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 723</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Allow the user to select circles</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 724</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 725</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">TypedValue</span>[] tvs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 726</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">TypedValue</span>[1] {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 727</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">TypedValue</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 728</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">int</span>)<span style="color: #2b91af;">DxfCode</span>.Start,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 729</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;CIRCLE&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 730</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 731</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; };</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 732</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">SelectionFilter</span> sf =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 733</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">SelectionFilter</span>(tvs);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 734</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 735</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">PromptSelectionResult</span> psr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 736</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.GetSelection(sf);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 737</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 738</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (psr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 739</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; psr.Value.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 740</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 741</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 742</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 743</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 744</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 745</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Get the information about the block</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 746</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and attribute definitions we care about</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 747</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 748</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">BlockTableRecord</span> ms;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 749</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">ObjectId</span> blockId;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 750</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">AttributeDefinition</span> ad;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 751</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 752</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 753</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (GetBlock(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 754</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db, tr, <span style="COLOR: blue">out</span> ms, <span style="COLOR: blue">out</span> blockId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 755</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 756</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 757</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; GetBlockAttributes(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 758</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr, blockId, <span style="COLOR: blue">out</span> ad, <span style="COLOR: blue">out</span> other</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 759</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 760</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 761</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// By default the modelspace is returned to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 762</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// us in read-only state</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 763</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 764</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ms.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 765</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 766</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">SelectedObject</span> o <span style="COLOR: blue">in</span> psr.Value)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 767</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 768</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// For each circle in the selected list...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 769</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 770</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 771</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(o.ObjectId, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 772</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Circle</span> c = obj <span style="COLOR: blue">as</span> <span style="color: #2b91af;">Circle</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 773</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (c == <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 774</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 775</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nObject selected is not a circle.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 776</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 777</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 778</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Call our numbering function, passing the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 779</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// center of the circle</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 780</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; CreateNumberedBubbleAtPoint(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 781</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db, ms, tr, c.Center,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 782</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; blockId, ad, other</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 783</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 784</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 785</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 786</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 787</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 788</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 789</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 790</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 791</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to delete a particular bubble</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 792</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// selected by its index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 793</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 794</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;MB&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 795</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> MoveBubble()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 796</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 797</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 798</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 799</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 800</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 801</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 802</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 803</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Use a helper function to select a valid bubble index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 804</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 805</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 806</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleNumber(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 807</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed, bd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 808</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nEnter number of bubble to move: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 809</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 810</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 811</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pos &gt;= bd.BaseNumber)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 812</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 813</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> from = pos - bd.BaseNumber;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 814</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 815</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 816</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; GetBubbleNumber(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 817</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed, bd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 818</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nEnter destination position: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 819</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 820</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 821</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pos &gt;= bd.BaseNumber)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 822</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 823</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> to = pos - bd.BaseNumber;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 824</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 825</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 826</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Nom.MoveObject(from, to);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 827</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 828</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RenumberBubbles(doc.Database, ids);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 829</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 830</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 831</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 832</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 833</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to delete a particular bubbler,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 834</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// selected by its index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 835</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 836</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DB&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 837</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> DeleteBubble()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 838</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 839</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 840</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 841</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 842</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 843</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 844</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 845</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 846</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Use a helper function to select a valid bubble index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 847</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 848</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 849</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleNumber(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 850</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed, bd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 851</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nEnter number of bubble to erase: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 852</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 853</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 854</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pos &gt;= bd.BaseNumber)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 855</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 856</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Remove the object from the internal list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 857</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (this returns the ObjectId stored for it,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 858</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// which we can then use to erase the entity)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 859</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 860</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectId</span> id =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 861</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bd.Nom.RemoveObject(pos - bd.BaseNumber);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 862</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 863</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 864</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 865</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 866</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 867</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForWrite);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 868</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj.Erase();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 869</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 870</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 871</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 872</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 873</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 874</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to reorder all the bubbles in the drawing,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 875</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// closing all the gaps between numbers but maintaining</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 876</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the current numbering order</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 877</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 878</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;RBS&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 879</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ReorderBubbles()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 880</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 881</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 882</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 883</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 884</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 885</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 886</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Re-order the bubbles - the IDs returned are</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 887</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// of the objects that need to be renumbered</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 888</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 889</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 890</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.Nom.ReorderObjects();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 891</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 892</span>&nbsp; &nbsp;&nbsp; &nbsp; RenumberBubbles(doc.Database, ids);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 893</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 894</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 895</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Command to highlight a particular bubble</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 896</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 897</span>&nbsp; &nbsp;&nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;HLB&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 898</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> HighlightBubble()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 899</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 900</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 901</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 902</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 903</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 904</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 905</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(doc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 906</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 907</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Use our function to select a valid bubble index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 908</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 909</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> pos =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 910</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleNumber(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 911</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed, bd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 912</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nEnter number of bubble to highlight: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 913</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 914</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 915</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pos &gt;= bd.BaseNumber)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 916</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 917</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectId</span> id =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 918</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bd.Nom.GetObjectId(pos - bd.BaseNumber);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 919</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 920</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (id == <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 921</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 922</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 923</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot;\nNumber is not currently used -&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 924</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #a31515;">&quot; nothing to highlight.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 925</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 926</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 927</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 928</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 929</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;(1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 930</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ids.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 931</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;HighlightBubbles(db, ids, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 932</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 933</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 934</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 935</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Internal helper function to open and retrieve</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 936</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the model-space and the block def we care about</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 937</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 938</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">bool</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 939</span>&nbsp; &nbsp;&nbsp; &nbsp; GetBlock(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 940</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 941</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 942</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">out</span> <span style="color: #2b91af;">BlockTableRecord</span> ms,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 943</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">out</span> <span style="color: #2b91af;">ObjectId</span> blockId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 944</span>&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 945</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 946</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 947</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="color: #2b91af;">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 948</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 949</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 950</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 951</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 952</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (!bt.Has(blockName))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 953</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 954</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 955</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 956</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 957</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 958</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nCannot find block definition \&quot;&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 959</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; blockName +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 960</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\&quot; in the current drawing.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 961</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 962</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 963</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;blockId = <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 964</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ms = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 965</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 966</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 967</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 968</span>&nbsp; &nbsp;&nbsp; &nbsp; ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 969</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 970</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 971</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 972</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 973</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 974</span>&nbsp; &nbsp;&nbsp; &nbsp; blockId = bt[blockName];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 975</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 976</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 977</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 978</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 979</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Internal helper function to retrieve</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 980</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// attribute info from our block</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 981</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (we return the main attribute def</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 982</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and then all the &quot;others&quot;)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 983</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 984</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 985</span>&nbsp; &nbsp;&nbsp; &nbsp; GetBlockAttributes(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 986</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 987</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectId</span> blockId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 988</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">out</span> <span style="color: #2b91af;">AttributeDefinition</span> ad,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 989</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">out</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 990</span>&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 991</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 992</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BlockTableRecord</span> blk =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 993</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 994</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; blockId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 995</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 996</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 997</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 998</span>&nbsp; &nbsp;&nbsp; &nbsp; ad = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 999</span>&nbsp; &nbsp;&nbsp; &nbsp; other =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1000</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1001</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1002</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> attId <span style="COLOR: blue">in</span> blk)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1003</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1004</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1005</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="color: #2b91af;">DBObject</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1006</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; attId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1007</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1008</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1009</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeDefinition</span> ad2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1010</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj <span style="COLOR: blue">as</span> <span style="color: #2b91af;">AttributeDefinition</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1011</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1012</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ad2 != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1013</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1014</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (ad2.Tag == attbName)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1015</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1016</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (ad2.Constant)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1017</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1018</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1019</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1020</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1021</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1022</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1023</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nAttribute to change is constant!&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1024</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1025</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1026</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1027</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ad = ad2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1028</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1029</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1030</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (!ad2.Constant)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1031</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;other.Add(ad2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1032</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1033</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1034</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1035</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1036</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Internal helper function to create a bubble</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1037</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// at a particular point</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1038</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1039</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">Entity</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1040</span>&nbsp; &nbsp;&nbsp; &nbsp; CreateNumberedBubbleAtPoint(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1041</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1042</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockTableRecord</span> btr,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1043</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Transaction</span> tr,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1044</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Point3d</span> pt,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1045</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectId</span> blockId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1046</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeDefinition</span> ad,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1047</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">AttributeDefinition</span>&gt; other</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1048</span>&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1049</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1050</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1051</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(db);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1052</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1053</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; Create a new block reference</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1054</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1055</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BlockReference</span> br =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1056</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">BlockReference</span>(pt, blockId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1057</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1058</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Add it to the database</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1059</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1060</span>&nbsp; &nbsp;&nbsp; &nbsp; br.SetDatabaseDefaults();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1061</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectId</span> blockRefId = btr.AppendEntity(br);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1062</span>&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(br, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1063</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1064</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create an attribute reference for our main</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1065</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// attribute definition (where we'll put the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1066</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// bubble's number)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1067</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1068</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">AttributeReference</span> ar =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1069</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">AttributeReference</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1070</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1071</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Add it to the database, and set its position, etc.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1072</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1073</span>&nbsp; &nbsp;&nbsp; &nbsp; ar.SetDatabaseDefaults();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1074</span>&nbsp; &nbsp;&nbsp; &nbsp; ar.SetAttributeFromBlock(ad, br.BlockTransform);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1075</span>&nbsp; &nbsp;&nbsp; &nbsp; ar.Position =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1076</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ad.Position.TransformBy(br.BlockTransform);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1077</span>&nbsp; &nbsp;&nbsp; &nbsp; ar.Tag = ad.Tag;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1078</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1079</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Set the bubble's number</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1080</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1081</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> bubbleNumber =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1082</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.BaseNumber +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1083</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.Nom.NextObjectNumber(blockRefId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1084</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1085</span>&nbsp; &nbsp;&nbsp; &nbsp; ar.TextString = bubbleNumber.ToString();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1086</span>&nbsp; &nbsp;&nbsp; &nbsp; ar.AdjustAlignment(db);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1087</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1088</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Add the attribute to the block reference</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1089</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1090</span>&nbsp; &nbsp;&nbsp; &nbsp; br.AttributeCollection.AppendAttribute(ar);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1091</span>&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(ar, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1092</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1093</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Now we add attribute references for the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1094</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// other attribute definitions</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1095</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1096</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">AttributeDefinition</span> ad2 <span style="COLOR: blue">in</span> other)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1097</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1098</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeReference</span> ar2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1099</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="color: #2b91af;">AttributeReference</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1100</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1101</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ar2.SetAttributeFromBlock(ad2, br.BlockTransform);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1102</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ar2.Position =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1103</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ad2.Position.TransformBy(br.BlockTransform);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1104</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ar2.Tag = ad2.Tag;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1105</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ar2.TextString = ad2.TextString;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1106</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ar2.AdjustAlignment(db);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1107</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1108</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;br.AttributeCollection.AppendAttribute(ar2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1109</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.AddNewlyCreatedDBObject(ar2, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1110</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1111</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> br;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1112</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1113</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1114</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Internal helper function to have the user</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1115</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// select a valid bubble index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1116</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1117</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">int</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1118</span>&nbsp; &nbsp;&nbsp; &nbsp; GetBubbleNumber(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1119</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1120</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BubbleData</span> bd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1121</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">string</span> prompt</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1122</span>&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1123</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1124</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> upper = bd.Nom.GetUpperBound();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1125</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (upper &lt;= 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1126</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1127</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1128</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nNo bubbles are currently being managed.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1129</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1130</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> upper;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1131</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1132</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1133</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">PromptIntegerOptions</span> pio =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1134</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">PromptIntegerOptions</span>(prompt);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1135</span>&nbsp; &nbsp;&nbsp; &nbsp; pio.AllowNone = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1136</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1137</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Get the limits from our manager object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1138</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1139</span>&nbsp; &nbsp;&nbsp; &nbsp; pio.LowerLimit =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1140</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.BaseNumber +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1141</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.Nom.GetLowerBound(<span style="COLOR: blue">false</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1142</span>&nbsp; &nbsp;&nbsp; &nbsp; pio.UpperLimit =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1143</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bd.BaseNumber +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1144</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;upper;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1145</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1146</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">PromptIntegerResult</span> pir =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1147</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.GetInteger(pio);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1148</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pir.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1149</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> pir.Value;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1150</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1151</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> -1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1152</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1153</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1154</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Internal helper function to open up a list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1155</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// of bubbles and reset their number to the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1156</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// position in the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1157</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1158</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> RenumberBubbles(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1159</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Database</span> db, <span style="color: #2b91af;">ObjectIdCollection</span> ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1160</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1161</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">BubbleData</span> bd =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1162</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;GetBubbleData(db);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1163</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1164</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1165</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1166</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1167</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get the block information</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1168</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1169</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockTableRecord</span> ms;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1170</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ObjectId</span> blockId;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1171</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1172</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (GetBlock(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1173</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db, tr, <span style="COLOR: blue">out</span> ms, <span style="COLOR: blue">out</span> blockId</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1174</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1175</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1176</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Open each bubble to be renumbered</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1177</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1178</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> bid <span style="COLOR: blue">in</span> ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1179</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1180</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bid != <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1181</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1182</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1183</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(bid, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1184</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">BlockReference</span> br = obj <span style="COLOR: blue">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1185</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (br != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1186</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1187</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (br.BlockTableRecord == blockId)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1188</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1189</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">AttributeCollection</span> ac =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1190</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;br.AttributeCollection;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1191</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1192</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Go through its attributes</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1193</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1194</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> aid <span style="COLOR: blue">in</span> ac)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1195</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1196</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">DBObject</span> obj2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1197</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.GetObject(aid, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1198</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">AttributeReference</span> ar =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1199</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; obj2 <span style="COLOR: blue">as</span> <span style="color: #2b91af;">AttributeReference</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1200</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1201</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (ar.Tag == attbName)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1202</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1203</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Change the one we care about</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1204</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1205</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ar.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1206</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1207</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> bubbleNumber =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1208</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.BaseNumber +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1209</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bd.Nom.GetNumber(bid);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1210</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ar.TextString =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1211</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bubbleNumber.ToString();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1212</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1213</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1214</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1215</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1216</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1217</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1218</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1219</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1220</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1221</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1222</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1223</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1224</span>&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1225</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1226</span>&nbsp; &nbsp;<span style="COLOR: green">// A generic class for managing groups of</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1227</span>&nbsp; &nbsp;<span style="COLOR: green">// numbered (and ordered) objects</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1228</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1229</span>&nbsp; &nbsp;<span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">NumberedObjectManager</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1230</span>&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1231</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Store the IDs of the objects we're managing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1232</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1233</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; m_ids;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1234</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1235</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// A list of free positions in the above list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1236</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (allows numbering gaps)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1237</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1238</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">List</span>&lt;<span style="COLOR: blue">int</span>&gt; m_free;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1239</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1240</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// A map of duplicates - blocks detected with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1241</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the number of an existing block</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1242</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1243</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="color: #2b91af;">SortedDictionary</span>&lt;<span style="COLOR: blue">int</span>,<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt; m_dups;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1244</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">SortedDictionary</span>&lt;<span style="COLOR: blue">int</span>, <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt; Duplicates</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1245</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1246</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">get</span> { <span style="COLOR: blue">return</span> m_dups; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1247</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1248</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1249</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Constructor</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1250</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1251</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> NumberedObjectManager()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1252</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1253</span>&nbsp; &nbsp;&nbsp; &nbsp; m_ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1254</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1255</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1256</span>&nbsp; &nbsp;&nbsp; &nbsp; m_free =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1257</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="COLOR: blue">int</span>&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1258</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1259</span>&nbsp; &nbsp;&nbsp; &nbsp; m_dups =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1260</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">SortedDictionary</span>&lt;<span style="COLOR: blue">int</span>, <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1261</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1262</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1263</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Clear the internal lists</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1264</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1265</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Clear()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1266</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1267</span>&nbsp; &nbsp;&nbsp; &nbsp; m_ids.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1268</span>&nbsp; &nbsp;&nbsp; &nbsp; m_free.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1269</span>&nbsp; &nbsp;&nbsp; &nbsp; m_dups.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1270</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1271</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1272</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Does the duplicate list contain anything?</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1273</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1274</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> HasDuplicates()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1275</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1276</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> m_dups.Count &gt; 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1277</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1278</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1279</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Return the first entry in the ObjectId list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1280</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (specify &quot;true&quot; if you want to skip</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1281</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// any null object IDs)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1282</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1283</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> GetLowerBound(<span style="COLOR: blue">bool</span> ignoreNull)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1284</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1285</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (ignoreNull)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1286</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Define an in-line predicate to check</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1287</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// whether an ObjectId is null</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1288</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1289</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ids.FindIndex(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1290</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">delegate</span>(<span style="color: #2b91af;">ObjectId</span> id)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1291</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1292</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> id != <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1293</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1294</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1295</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1296</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1297</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1298</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1299</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Return the last entry in the ObjectId list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1300</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1301</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> GetUpperBound()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1302</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1303</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> m_ids.Count - 1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1304</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1305</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1306</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Store the specified ObjectId in the next</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1307</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// available location in the list, and return</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1308</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// what that is</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1309</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1310</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> NextObjectNumber(<span style="color: #2b91af;">ObjectId</span> id)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1311</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1312</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> pos;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1313</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (m_free.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1314</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1315</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get the first free position, then remove</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1316</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// it from the &quot;free&quot; list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1317</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1318</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pos = m_free[0];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1319</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_free.RemoveAt(0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1320</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids[pos] = id;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1321</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1322</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1323</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1324</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// There are no free slots (gaps in the numbering)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1325</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// so we append it to the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1326</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1327</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pos = m_ids.Count;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1328</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1329</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1330</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> pos;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1331</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1332</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1333</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Go through the list of objects and close any gaps</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1334</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// by shuffling the list down (easy, as we're using a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1335</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// List&lt;&gt; rather than an array)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1336</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1337</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">ObjectIdCollection</span> ReorderObjects()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1338</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1339</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create a collection of ObjectIds we'll return</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1340</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// for the caller to go and update</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1341</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// (so the renumbering will gets reflected</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1342</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// in the objects themselves)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1343</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1344</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1345</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1346</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1347</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// We'll go through the &quot;free&quot; list backwards,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1348</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// to allow any changes made to the list of</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1349</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// objects to not affect what we're doing</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1350</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1351</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">List</span>&lt;<span style="COLOR: blue">int</span>&gt; rev =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1352</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="COLOR: blue">int</span>&gt;(m_free);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1353</span>&nbsp; &nbsp;&nbsp; &nbsp; rev.Reverse();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1354</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1355</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: blue">int</span> pos <span style="COLOR: blue">in</span> rev)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1356</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1357</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// First we remove the object at the &quot;free&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1358</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// position (in theory this should be set to</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1359</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// ObjectId.Null, as the slot has been marked</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1360</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// as blank)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1361</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1362</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids.RemoveAt(pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1363</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1364</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Now we go through and add the IDs of any</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1365</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// affected objects to the list to return</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1366</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1367</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = pos; i &lt; m_ids.Count; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1368</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1369</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">ObjectId</span> id = m_ids[pos];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1370</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1371</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Only add non-null objects</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1372</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// not already in the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1373</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1374</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (!ids.Contains(id) &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1375</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;id != <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1376</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ids.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1377</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1378</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1379</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1380</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Our free slots have been filled, so clear</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1381</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1382</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1383</span>&nbsp; &nbsp;&nbsp; &nbsp; m_free.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1384</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1385</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> ids;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1386</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1387</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1388</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Get the ID of an object at a particular position</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1389</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1390</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">ObjectId</span> GetObjectId(<span style="COLOR: blue">int</span> pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1391</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1392</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pos &lt; m_ids.Count)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1393</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> m_ids[pos];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1394</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1395</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1396</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1397</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1398</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Get the position of an ObjectId in the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1399</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1400</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> GetNumber(<span style="color: #2b91af;">ObjectId</span> id)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1401</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1402</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (m_ids.Contains(id))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1403</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> m_ids.IndexOf(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1404</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1405</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> -1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1406</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1407</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1408</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Store an ObjectId in a particular position</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1409</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (shuffle == true will &quot;insert&quot; it, shuffling</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1410</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the remaining objects down,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1411</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// shuffle == false will replace the item in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1412</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// that slot)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1413</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1414</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> NumberObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1415</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectId</span> id, <span style="COLOR: blue">int</span> index, <span style="COLOR: blue">bool</span> shuffle, <span style="COLOR: blue">bool</span> dups)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1416</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1417</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// If we're inserting into the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1418</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1419</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (index &lt; m_ids.Count)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1420</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1421</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (shuffle)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1422</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Insert takes care of the shuffling</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1423</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ids.Insert(index, id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1424</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1425</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1426</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// If we're replacing the existing item, do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1427</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// so and then make sure the slot is removed</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1428</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// from the &quot;free&quot; list, if applicable</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1429</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1430</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (!dups ||</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1431</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids[index] == <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1432</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1433</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_ids[index] = id;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1434</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (m_free.Contains(index))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1435</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_free.Remove(index);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1436</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1437</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1438</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1439</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// If we're tracking duplicates, add our new</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1440</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// object to the duplicate list for that index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1441</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1442</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (dups)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1443</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1444</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; ids;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1445</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_dups.ContainsKey(index))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1446</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1447</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ids = m_dups[index];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1448</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_dups.Remove(index);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1449</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1450</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1451</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ids = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1452</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1453</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ids.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1454</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_dups.Add(index, ids);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1455</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1456</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1457</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1458</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1459</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1460</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1461</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// If we're appending, shuffling is irrelevant,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1462</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// but we may need to add additional &quot;free&quot; slots</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1463</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// if the position comes after the end</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1464</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1465</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">while</span> (m_ids.Count &lt; index)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1466</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1467</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ids.Add(<span style="color: #2b91af;">ObjectId</span>.Null);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1468</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_free.Add(m_ids.LastIndexOf(<span style="color: #2b91af;">ObjectId</span>.Null));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1469</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_free.Sort();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1470</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1471</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids.Add(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1472</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1473</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1474</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1475</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Move an ObjectId already in the list to a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1476</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// particular position</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1477</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (ObjectIds between the two positions will</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1478</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// get shuffled down automatically)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1479</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1480</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">ObjectIdCollection</span> MoveObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1481</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> from, <span style="COLOR: blue">int</span> to)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1482</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1483</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectIdCollection</span> ids =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1484</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1485</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1486</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (from &lt; m_ids.Count &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1487</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; to &lt; m_ids.Count)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1488</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1489</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (from != to)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1490</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1491</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">ObjectId</span> id = m_ids[from];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1492</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ids.RemoveAt(from);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1493</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ids.Insert(to, id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1494</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1495</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> start = (from &lt; to ? from : to);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1496</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> end = (from &lt; to ? to : from);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1497</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1498</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = start; i &lt;= end; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1499</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1500</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ids.Add(m_ids[i]);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1501</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1502</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1503</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Now need to adjust/recreate &quot;free&quot; list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1504</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_free.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1505</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> j = 0; j &lt; m_ids.Count; j++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1506</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1507</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (m_ids[j] == <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1508</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_free.Add(j);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1509</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1510</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1511</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> ids;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1512</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1513</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1514</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Remove an ObjectId from the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1515</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1516</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> RemoveObject(<span style="color: #2b91af;">ObjectId</span> id)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1517</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1518</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Check it's non-null and in the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1519</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1520</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (id != <span style="color: #2b91af;">ObjectId</span>.Null &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1521</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_ids.Contains(id))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1522</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1523</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> pos = m_ids.IndexOf(id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1524</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;RemoveObject(pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1525</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> pos;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1526</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1527</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> -1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1528</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1529</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1530</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Remove the ObjectId at a particular position</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1531</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1532</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="color: #2b91af;">ObjectId</span> RemoveObject(<span style="COLOR: blue">int</span> pos)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1533</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1534</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Get the ObjectId in the specified position,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1535</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// making sure it's non-null</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1536</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1537</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="color: #2b91af;">ObjectId</span> id = m_ids[pos];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1538</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (id != <span style="color: #2b91af;">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1539</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1540</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Null out the position and add it to the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1541</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// &quot;free&quot; list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1542</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1543</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids[pos] = <span style="color: #2b91af;">ObjectId</span>.Null;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1544</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_free.Add(pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1545</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_free.Sort();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1546</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1547</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> id;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1548</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1549</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1550</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Dump out the object list information</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1551</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// as well as the &quot;free&quot; slots</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1552</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1553</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> DumpInfo(<span style="color: #2b91af;">Editor</span> ed)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1554</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1555</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (m_ids.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1556</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1557</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="color: #a31515;">&quot;\nIdx ObjectId&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1558</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1559</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">int</span> index = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1560</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="COLOR: blue">in</span> m_ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1561</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: #a31515;">&quot;\n{0} {1}&quot;</span>, index++, id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1562</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1563</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1564</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (m_free.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1565</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1566</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="color: #a31515;">&quot;\n\nFree list: &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1567</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1568</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: blue">int</span> pos <span style="COLOR: blue">in</span> m_free)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1569</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: #a31515;">&quot;{0} &quot;</span>, pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1570</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1571</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (HasDuplicates())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1572</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1573</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="color: #a31515;">&quot;\n\nDuplicate list: &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1574</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1575</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1576</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">KeyValuePair</span>&lt;<span style="COLOR: blue">int</span>, <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt; dup</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1577</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">in</span> m_dups</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1578</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1579</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1580</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">int</span> pos = dup.Key;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1581</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; ids = dup.Value;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1582</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1583</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: #a31515;">&quot;\n{0}&nbsp; &nbsp; &quot;</span>, pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1584</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1585</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="COLOR: blue">in</span> ids)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1586</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1587</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(<span style="color: #a31515;">&quot;{0} &quot;</span>, id);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1588</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1589</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1590</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1591</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1592</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1593</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Remove the initial n items from the list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1594</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1595</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> RebaseList(<span style="COLOR: blue">int</span> start)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1596</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1597</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// First we remove the ObjectIds</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1598</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1599</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i=0; i &lt; start; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1600</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_ids.RemoveAt(0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1601</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1602</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Then we go through the &quot;free&quot; list...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1603</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1604</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> idx = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1605</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">while</span> (idx &lt; m_free.Count)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1606</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1607</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m_free[idx] &lt; start)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1608</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Remove any that refer to the slots</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1609</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// we've removed </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1610</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_free.RemoveAt(idx);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1611</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1612</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1613</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Subtracting the number of slots</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1614</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// we've removed from the other items</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1615</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_free[idx] -= start;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1616</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; idx++;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1617</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1618</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1619</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1620</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// The duplicate list is more tricky, as we store</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1621</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// our list of objects against the index</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1622</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// (we need to remove and re-add each entry)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1623</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1624</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (HasDuplicates())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1625</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1626</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// First we get all the indeces (which we use</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1627</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// to iterate through the list)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1628</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1629</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">SortedDictionary</span>&lt;<span style="COLOR: blue">int</span>, <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt;&gt;.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1630</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">KeyCollection</span> kc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1631</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_dups.Keys;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1632</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1633</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Copy the KeyCollection into a list of ints,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1634</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// to make it easier to iterate</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1635</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (this also allows us to modify the m_dups</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1636</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// object while we're iterating)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1637</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1638</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">List</span>&lt;<span style="COLOR: blue">int</span>&gt; idxs = <span style="COLOR: blue">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="COLOR: blue">int</span>&gt;(kc.Count);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1639</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: blue">int</span> pos <span style="COLOR: blue">in</span> kc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1640</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; idxs.Add(pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1641</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1642</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: blue">int</span> pos <span style="COLOR: blue">in</span> idxs)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1643</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1644</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">ObjectId</span>&gt; ids;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1645</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; m_dups.TryGetValue(pos, <span style="COLOR: blue">out</span> ids);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1646</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1647</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (m_dups.ContainsKey(pos - start))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1648</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">throw</span> <span style="COLOR: blue">new</span> <span style="color: #2b91af;">Exception</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1649</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">ErrorStatus</span>.DuplicateKey,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1650</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;\nClash detected - &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1651</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #a31515;">&quot;duplicate list may be corrupted.&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1652</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1653</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1654</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1655</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Remove the old entry and add the new one</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1656</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1657</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_dups.Remove(pos);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1658</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; m_dups.Add(pos - start, ids);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1659</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1660</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">1661</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1662</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1663</span>&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">1664</span> }</p></div>

<p>As for the specific changes...</p>

<p>Lines 19-71 encapsulate the information we need to store per-document, with lines 140-183 allowing retrieval of this data.</p>

<p>Line 102-130 and 185-289 add event handlers to the application. Note that we watch Database.ObjectAppended event to find when numbered objects are added to a drawing, but we do the actually work of renumbering the objects during DocumentCollection.DocumentLockWillChange - the safe place to do so.</p>

<p>A lot of the additional line changes are simply to access the new per-document data via the BubbleData class: 300-302, 316-317, 322, 402, 418, 452-453, 800-801, 811, 813, 821, 823, 826, 843-844, 854,861, 883-884, 890, 904-905, 915, 1050-1051, 1083-1084, 1124, 1140-1141, 1143, 1161-1162, 1208-1209.</p>

<p>Lines 458-616 add the ability to renumber bubbles while scanning the drawing, whether automatically (with no user intervention) or semi-automatically (allowing the user to choose a specific bubble not to renumber). This latter process uses a new HighlightBubbles() function (lines 620-644) to highlight a list of bubbles (it's generic, so could be called HighlightObjects(), thinking about it). This is then also used by the HLB command, replacing the previous implementation (lines 917-931).</p>

<p>We now pass the BubbleData class through to the GetBubbleNumber() function, in line 1120. This is then used in lines 807, 817, 850 &amp; 911.</p>

<p>The NumberedObjectManager class has required infrastructure changes to support duplicates: 1240-1247, 1259-1260, 1269 &amp; 1272-1277. We're using a SortedDictionary to maintain a list of ObjectIds per position. This is a standard &quot;overflow&quot; data structure, and is only added to when a duplicate is found.</p>

<p>DumpInfo() now displays duplicate data in the DMP command (lines 1571-1590).</p>

<p>RebaseList() has been changed to move entries in the duplicate list. This ended up being quite complicated, as we're mapping a list of objects against a position (the dictionary key) and so it's the key that changes when we move the list's base.</p>

<p>To try out this additional functionality, try this:</p>

<ol><li>Open <a href="http://through-the-interface.typepad.com/through_the_interface/files/bubbles_1.dwg">a drawing with a bunch of bubbles</a>, or create new ones.</li>

<li>Copy &amp; paste these bubbles one or more times, to seed the drawing with duplicate-numbered objects.</li>

<li>Load the application and run the LNS command to understand the numbers in the drawing. Try the different options for renumbering duplicates (Automatically, Individually or Not).</li>

<li>Next try copying and pasting again, once the numbering system is active, and see how the copied numbers are changed.</li>

<li>INSERT a few bubbles: new blocks will also be renumbered - even if the user selects a particular number via the attribute editing dialog - but that's somewhat inevitable with this approach.</li></ol>

<p>OK - now that we're up at nearly 1700 lines of code, it's getting time to bring this series to a close... (or at least to stop including the entire code in each post. :-)</p>
