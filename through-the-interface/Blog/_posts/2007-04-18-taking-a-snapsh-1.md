---
layout: "post"
title: "Taking a snapshot of the AutoCAD model (take 2)"
date: "2007-04-18 03:37:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Graphics system"
original_url: "https://www.keanw.com/2007/04/taking_a_snapsh_1.html "
typepad_basename: "taking_a_snapsh_1"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2007/04/taking_a_snapsh.html">this previous post</a>, we looked at some code to do a programmatic snapshot of AutoCAD's modelspace, saving the results to an image file.</p>

<p>From the discussion that followed, I realised that the code had an undesired (and unnecessary) side-effect of creating a new 3D GS View and leaving the modelspace with that view active. GS Views in AutoCAD 2007 have grey backgrounds by default, and so this change can be quite disturbing for users. The only reason we created the GS View in the first place (if one didn't already exist), was to use it to query the view position/target/up vector/field width and height and apply it to our new view. Thankfully it seems this can also be determined directly from the viewport.</p>

<p>So rather than calling GetGSView() and using the returned view to get that information, we now simply call SetViewFromViewport() specifying the viewport number held in CVPORT, and the graphics system manager for that document handles the rest.</p>

<p>Here's the updated C# code, which appears to achieve the same goals without the side-effect. Check line 124 for the new code, a few extraneous lines around it having been removed:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 1</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 2</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 3</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 4</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.GraphicsInterface;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 5</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.GraphicsSystem;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 6</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 7</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Interop;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 8</span> <span style="COLOR: blue">using</span> System.Drawing;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 9</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;10</span> <span style="COLOR: blue">namespace</span> OffscreenImageCreation</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;11</span> {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;12</span>&nbsp; &nbsp;<span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;13</span>&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;14</span>&nbsp; &nbsp;&nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;OSS&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;15</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> OffscreenSnapshot()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;16</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;17</span>&nbsp; &nbsp;&nbsp; &nbsp; CreateSphere();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;18</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;19</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Wireframe2D.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;20</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Wireframe2D</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;21</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;22</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;23</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Hidden.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;24</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Hidden</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;25</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;26</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;27</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Basic.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;28</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Basic</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;29</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;30</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;31</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-ColorChange.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;32</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.ColorChange</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;33</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;34</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;35</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Conceptual.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;36</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Conceptual</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;37</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;38</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;39</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Flat.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;40</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Flat</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;41</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;42</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;43</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Gouraud.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;44</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Gouraud</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;45</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;46</span>&nbsp; &nbsp;&nbsp; &nbsp; SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;47</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\sphere-Realistic.png&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;48</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VisualStyleType</span>.Realistic</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;49</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;50</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;51</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;52</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> CreateSphere()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;53</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;54</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;55</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;56</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;57</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;58</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;59</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;60</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;doc.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;61</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;62</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;63</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;64</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;65</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;66</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;67</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;68</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;69</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;70</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;71</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;72</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;73</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Solid3d</span> sol = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Solid3d</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;74</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;sol.CreateSphere(10.0);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;75</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;76</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> matname =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;77</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Sitework.Paving - Surfacing.Riverstone.Mortared&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;78</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DBDictionary</span> matdict =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;79</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">DBDictionary</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;80</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.MaterialDictionaryId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;81</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;82</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;83</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (matdict.Contains(matname))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;84</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;85</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; sol.Material = matname;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;86</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;87</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;88</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;89</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;90</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nMaterial (&quot;</span> + matname + <span style="COLOR: maroon">&quot;) not found&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;91</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot; - sphere will be rendered without it.&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;92</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; matname</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;93</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;94</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;95</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;btr.AppendEntity(sol);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;96</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;97</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.AddNewlyCreatedDBObject(sol, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;98</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;99</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 100</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">AcadApplication</span> acadApp =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 101</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">AcadApplication</span>)<span style="COLOR: teal">Application</span>.AcadApplication;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 102</span>&nbsp; &nbsp;&nbsp; &nbsp; acadApp.ZoomExtents();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 103</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 104</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 105</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SnapshotToFile(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 106</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">string</span> filename,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 107</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">VisualStyleType</span> vst</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 108</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 109</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 110</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 111</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 112</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 113</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 114</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Manager</span> gsm = doc.GraphicsManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 115</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 116</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Get some AutoCAD system variables</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 117</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> vpn =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 118</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;System.<span style="COLOR: teal">Convert</span>.ToInt32(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 119</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.GetSystemVariable(<span style="COLOR: maroon">&quot;CVPORT&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 120</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 121</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 122</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (<span style="COLOR: teal">View</span> view = <span style="COLOR: blue">new</span> <span style="COLOR: teal">View</span>())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 123</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 124</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;gsm.SetViewFromViewport(view, vpn);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 125</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 126</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Set the visual style to the one passed in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 127</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;view.VisualStyle = <span style="COLOR: blue">new</span> <span style="COLOR: teal">VisualStyle</span>(vst);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 128</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 129</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Device</span> dev =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 130</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; gsm.CreateAutoCADOffScreenDevice();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 131</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (dev)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 132</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 133</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dev.OnSize(gsm.DisplaySize);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 134</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 135</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Set the render type and the background color</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 136</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dev.DeviceRenderType = <span style="COLOR: teal">RendererType</span>.Default;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 137</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dev.BackgroundColor = <span style="COLOR: teal">Color</span>.White;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 138</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 139</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Add the view to the device and update it</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 140</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dev.Add(view);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 141</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dev.Update();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 142</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 143</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">using</span> (<span style="COLOR: teal">Model</span> model = gsm.CreateAutoCADModel())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 144</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 145</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 146</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 147</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 148</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 149</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Add the modelspace to the view</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 150</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// It's a container but also a drawable</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 151</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 152</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 153</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 154</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 155</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 156</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 157</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 158</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 159</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 160</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 161</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;view.Add(btr, model);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 162</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 163</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 164</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Take the snapshot</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 165</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Rectangle</span> rect = view.Viewport;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 166</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (<span style="COLOR: teal">Bitmap</span> bitmap = view.GetSnapshot(rect))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 167</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 168</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bitmap.Save(filename);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 169</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 170</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nSnapshot image saved to: &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 171</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; filename</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 172</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 173</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Clean up</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 174</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;view.EraseAll();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 175</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;dev.Erase(view);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 176</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 177</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 178</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 179</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 180</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 181</span>&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 182</span> }</p></div>

<p>You can also download the .cs file from <a href="http://through-the-interface.typepad.com/through_the_interface/files/snapshot-to-image.cs">here</a>.</p>
