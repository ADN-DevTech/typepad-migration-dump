---
layout: "post"
title: "Creating transparent hatches in AutoCAD using .NET"
date: "2010-06-23 05:40:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
  - "Hatches"
original_url: "https://www.keanw.com/2010/06/creating-transparent-hatches-in-autocad-using-net.html "
typepad_basename: "creating-transparent-hatches-in-autocad-using-net"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2010/06/tracing-a-boundary-defined-by-autocad-geometry-using-net.html" target="_blank">the last post</a> we used an API introduced in AutoCAD 2011 to trace boundaries defined by geometry in a drawing. In this post we’re going to use these boundaries to define the limits of solid hatch objects which we will – using another new capability in AutoCAD 2011 - make transparent.</p>

<p>Here’s the updated C# code to define our TBH command with new/changed lines in <font color="#ff0000">red</font> (you can also get <A href="http://through-the-interface.typepad.com/files/trace-boundary-transparent-hatch.cs">the source file without line numbers here</A>):</p>

<div style="font-family: courier new; background: white; color: black; font-size: 8pt">
  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 1</span>&#160;<span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 2</span>&#160;<span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 3</span>&#160;<span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 4</span>&#160;<span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.AutoCAD.Runtime;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; <font color="#ff0000">5</font></span>&#160;<span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> Autodesk.AutoCAD.Colors;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 6</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; <font color="#ff0000">7</font></span>&#160;<span style="line-height: 140%; color: blue">namespace</span><span style="line-height: 140%"> TraceBoundaryWithHatch</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 8</span>&#160;<span style="line-height: 140%">{</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160;&#160; 9</span>&#160;<span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">class</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Commands</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 10</span>&#160;<span style="line-height: 140%">&#160; {</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 11</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">int</span><span style="line-height: 140%"> _index = 1;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 12</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; <font color="#ff0000">13</font></span>&#160;<span style="line-height: 140%">&#160;&#160;&#160; [</span><span style="line-height: 140%; color: #2b91af">CommandMethod</span><span style="line-height: 140%">(</span><span style="line-height: 140%; color: #a31515">&quot;TBH&quot;</span><span style="line-height: 140%">)]</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; <font color="#ff0000">14</font></span>&#160;<span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> TraceBoundaryAndHatch()</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 15</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160; {</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 16</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Document</span><span style="line-height: 140%"> doc =</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 17</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Application</span><span style="line-height: 140%">.DocumentManager.MdiActiveDocument;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 18</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Database</span><span style="line-height: 140%"> db = doc.Database;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 19</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Editor</span><span style="line-height: 140%"> ed = doc.Editor;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 20</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 21</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Select a seed point for our boundary</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 22</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 23</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">PromptPointResult</span><span style="line-height: 140%"> ppr =</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 24</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; ed.GetPoint(</span><span style="line-height: 140%; color: #a31515">&quot;\nSelect internal point: &quot;</span><span style="line-height: 140%">);</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 25</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 26</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (ppr.Status != </span><span style="line-height: 140%; color: #2b91af">PromptStatus</span><span style="line-height: 140%">.OK)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 27</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%">;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 28</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 29</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Get the objects making up our boundary</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 30</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 31</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">DBObjectCollection</span><span style="line-height: 140%"> objs =</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; <font color="#ff0000">32</font></span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; ed.TraceBoundary(ppr.Value, </span><span style="line-height: 140%; color: blue">false</span><span style="line-height: 140%">);</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 33</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 34</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (objs.Count &gt; 0)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 35</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; {</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 36</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Transaction</span><span style="line-height: 140%"> tr =</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 37</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; doc.TransactionManager.StartTransaction();</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 38</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">using</span><span style="line-height: 140%"> (tr)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 39</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 40</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// We'll add the objects to the model space</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 41</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 42</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">BlockTable</span><span style="line-height: 140%"> bt =</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 43</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (</span><span style="line-height: 140%; color: #2b91af">BlockTable</span><span style="line-height: 140%">)tr.GetObject(</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 44</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; doc.Database.BlockTableId,</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 45</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">OpenMode</span><span style="line-height: 140%">.ForRead</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 46</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; );</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 47</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 48</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">BlockTableRecord</span><span style="line-height: 140%"> btr =</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 49</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (</span><span style="line-height: 140%; color: #2b91af">BlockTableRecord</span><span style="line-height: 140%">)tr.GetObject(</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 50</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; bt[</span><span style="line-height: 140%; color: #2b91af">BlockTableRecord</span><span style="line-height: 140%">.ModelSpace],</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 51</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">OpenMode</span><span style="line-height: 140%">.ForWrite</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 52</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; );</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 53</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 54</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Add our boundary objects to the drawing and</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 55</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// collect their ObjectIds for later use</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 56</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 57</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">ObjectIdCollection</span><span style="line-height: 140%"> ids = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ObjectIdCollection</span><span style="line-height: 140%">();</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 58</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">foreach</span><span style="line-height: 140%"> (</span><span style="line-height: 140%; color: #2b91af">DBObject</span><span style="line-height: 140%"> obj </span><span style="line-height: 140%; color: blue">in</span><span style="line-height: 140%"> objs)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 59</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 60</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Entity</span><span style="line-height: 140%"> ent = obj </span><span style="line-height: 140%; color: blue">as</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Entity</span><span style="line-height: 140%">;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 61</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%"> (ent != </span><span style="line-height: 140%; color: blue">null</span><span style="line-height: 140%">)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 62</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 63</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Set our boundary objects to be of</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 64</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// our auto-incremented colour index</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 65</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 66</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ent.ColorIndex = _index;</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 67</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; <font color="#ff0000">68</font></span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Set our transparency to 50% (=127)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; <font color="#ff0000">69</font></span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Alpha value is Truncate(255 * (100-n)/100)</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 70</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; <font color="#ff0000">71</font></span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ent.Transparency = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Transparency</span><span style="line-height: 140%">(127);</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 72</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 73</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Add each boundary object to the modelspace</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 74</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// and add its ID to a collection</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 75</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 76</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ids.Add(btr.AppendEntity(ent));</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 77</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; tr.AddNewlyCreatedDBObject(ent, </span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%">);</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 78</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 79</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160;&#160; 80</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 81</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Create our hatch</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 82</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 83</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Hatch</span><span style="line-height: 140%"> hat = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Hatch</span><span style="line-height: 140%">();</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 84</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 85</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Solid fill of our auto-incremented colour index</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 86</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 87</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; hat.SetHatchPattern(</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 88</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">HatchPatternType</span><span style="line-height: 140%">.PreDefined,</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 89</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #a31515">&quot;SOLID&quot;</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 90</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; );</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 91</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; hat.ColorIndex = _index++;</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 92</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 93</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Set our transparency to 50% (=127)</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 94</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Alpha value is Truncate(255 * (100-n)/100)</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 95</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 96</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; hat.Transparency = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Transparency</span><span style="line-height: 140%">(127);</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 97</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 98</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Add the hatch to the modelspace &amp; transaction</span></p>

  <p style="margin: 0px"><span style="color: red">&#160;&#160; 99</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160; 100</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">ObjectId</span><span style="line-height: 140%"> hatId = btr.AppendEntity(hat);</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 101</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; tr.AddNewlyCreatedDBObject(hat, </span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%">);</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 102</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160; 103</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Add the hatch loops and complete the hatch</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 104</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160; 105</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; hat.Associative = </span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%">;</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 106</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; hat.AppendLoop(</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 107</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">HatchLoopTypes</span><span style="line-height: 140%">.Default,</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 108</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ids</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 109</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; );</span></p>

  <p style="margin: 0px"><span style="color: red">&#160; 110</span>&#160;</p>

  <p style="margin: 0px"><span style="color: red">&#160; 111</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; hat.EvaluateHatch(</span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%">);</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 112</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 113</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Commit the transaction</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 114</span>&#160;</p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 115</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; tr.Commit();</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 116</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 117</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; }</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 118</span>&#160;<span style="line-height: 140%">&#160;&#160;&#160; }</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 119</span>&#160;<span style="line-height: 140%">&#160; }</span></p>

  <p style="margin: 0px"><span style="color: #2b91af">&#160; 120</span>&#160;<span style="line-height: 140%">}</span></p>
</div>
<!--EndFragment-->

<p>A few things to note about this implementation:</p>

<ul>
  <li>We choose not to detect islands when using the results to create a hatch 
    <ul>
      <li>Hatches clearly support islands, but we would make separate calls to AppendLoop() to add them </li>

      <li>It should be possible to detect islands whether geometrically or by diffing the results of two calls to TraceBoundary(), one detecting islands, one not </li>
    </ul>
  </li>

  <li>We’ve applied transparency both to the boundary objects and the hatch 
    <ul>
      <li>We no longer set the lineweight on the boundary objects </li>
    </ul>
  </li>

  <li>The Alpha-based transparency value isn’t as simple as using a straight percentage 
    <ul>
      <li>After doing some tests (setting the transparency as a percentage and running a simple command to print the Alpha value to the command-line), I found that the value stored actually seems to reflect “opacity” rather than transparency (i.e. it’s an inverse relationship), so we subtract our percentage from 100 before applying it to the total of 255 (and truncating – not rounding – the result) </li>

      <li>It also appears you can’t have transparency of higher than 90% (at least not according to my tests) 
        <ul>
          <li>In our case we’re just using 50% transparency </li>
        </ul>
      </li>
    </ul>
  </li>
</ul>

<p>Here’s what happens when we run our TBH command on the boundaries we used to test the code in the last post.</p>
<P>Before…</P>
<P><A href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2013484af4e6b970c-pi"><img  style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px"title="Our boundaries" border=0 alt="Our boundaries" src="/assets/image_349305.jpg" width=323 height=368 /></A> </P>
<P>After…</P>
<P></P>
<P><A href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f18775c8970b-pi"><img  style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px"title="Our transparently hatched boundaries" border=0 alt="Our transparently hatched boundaries" src="/assets/image_671251.jpg" width=323 height=368 /></A></P>
