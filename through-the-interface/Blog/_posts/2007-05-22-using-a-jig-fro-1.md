---
layout: "post"
title: "Using a jig from .NET to multiply insert AutoCAD blocks - Part 2"
date: "2007-05-22 04:53:02"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Jigs"
  - "User interface"
original_url: "https://www.keanw.com/2007/05/using_a_jig_fro_1.html "
typepad_basename: "using_a_jig_fro_1"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2007/05/using_a_jig_fro.html">the last post</a> we looked at a jig that can be used to add block references to an AutoCAD drawing. This post extends that code to support annotative block definitions (available from AutoCAD 2008) and blocks with attributes. Thanks once again to Holger Steiner for the jig class and to Roland Feletic for posting the code to support annotative blocks.</p>
<p>A comment on the previous post asked about having attributes visible during the jig process: unfortunately that&#39;s not currently possible, as the existing managed AttributeCollection implementation wraps the version of the <span face="Arial">AcDbBlockReference::appendAttribute() </span>ObjectARX function that requires the block reference to already have been added to the drawing. So for now you will need to live with the fact that the attributes don&#39;t display during the jig, but do display as soon as the block has been added to the drawing.</p>
<p>Below is the modified C# code, with line numbers. And here&#39;s the <a href="http://through-the-interface.typepad.com/through_the_interface/files/block-jig-attribute-support.cs">source file for download.</a></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 1</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 2</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 3</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 4</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 5</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 6</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Internal;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 7</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 8</span> <span style="COLOR: blue">namespace</span> BlockJigTest</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160; 9</span> {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;10</span>&#0160; &#0160;<span style="COLOR: blue">class</span> <span style="COLOR: teal">BlockJig</span> : <span style="COLOR: teal">EntityJig</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;11</span>&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;12</span>&#0160; &#0160;&#0160; <span style="COLOR: teal">Point3d</span> mCenterPt, mActualPoint;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;13</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;14</span>&#0160; &#0160;&#0160; <span style="COLOR: blue">public</span> BlockJig(<span style="COLOR: teal">BlockReference</span> br)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;15</span>&#0160; &#0160;&#0160; &#0160; : <span style="COLOR: blue">base</span>(br)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;16</span>&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;17</span>&#0160; &#0160;&#0160; &#0160; mCenterPt = br.Position;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;18</span>&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;19</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;20</span>&#0160; &#0160;&#0160; <span style="COLOR: blue">protected</span> <span style="COLOR: blue">override</span> <span style="COLOR: teal">SamplerStatus</span> Sampler(<span style="COLOR: teal">JigPrompts</span> prompts)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;21</span>&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;22</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">JigPromptPointOptions</span> jigOpts =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;23</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">new</span> <span style="COLOR: teal">JigPromptPointOptions</span>();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;24</span>&#0160; &#0160;&#0160; &#0160; jigOpts.UserInputControls =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;25</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: teal">UserInputControls</span>.Accept3dCoordinates</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;26</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;| <span style="COLOR: teal">UserInputControls</span>.NoZeroResponseAccepted</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;27</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;| <span style="COLOR: teal">UserInputControls</span>.NoNegativeResponseAccepted);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;28</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;29</span>&#0160; &#0160;&#0160; &#0160; jigOpts.Message =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;30</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: maroon">&quot;\nEnter insert point: &quot;</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;31</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;32</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">PromptPointResult</span> dres =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;33</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;prompts.AcquirePoint(jigOpts);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;34</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;35</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (mActualPoint == dres.Value)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;36</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;37</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">return</span> <span style="COLOR: teal">SamplerStatus</span>.NoChange;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;38</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;39</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">else</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;40</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;41</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;mActualPoint = dres.Value;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;42</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;43</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">return</span> <span style="COLOR: teal">SamplerStatus</span>.OK;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;44</span>&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;45</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;46</span>&#0160; &#0160;&#0160; <span style="COLOR: blue">protected</span> <span style="COLOR: blue">override</span> <span style="COLOR: blue">bool</span> Update()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;47</span>&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;48</span>&#0160; &#0160;&#0160; &#0160; mCenterPt = mActualPoint;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;49</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">try</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;50</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;51</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;((<span style="COLOR: teal">BlockReference</span>)Entity).Position = mCenterPt;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;52</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;53</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">catch</span> (System.<span style="COLOR: teal">Exception</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;54</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;55</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;56</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;57</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">return</span> <span style="COLOR: blue">true</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;58</span>&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;59</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;60</span>&#0160; &#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: teal">Entity</span> GetEntity()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;61</span>&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;62</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">return</span> Entity;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;63</span>&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;64</span>&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;65</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;66</span>&#0160; &#0160;<span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;67</span>&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;68</span>&#0160; &#0160;&#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;BJIG&quot;</span>)]</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;69</span>&#0160; &#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> CreateBlockWithJig()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;70</span>&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;71</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">Document</span> doc =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;72</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;73</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">Database</span> db = doc.Database;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;74</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;75</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;76</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// First let&#39;s get the name of the block</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;77</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">PromptStringOptions</span> opts =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;78</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptStringOptions</span>(<span style="COLOR: maroon">&quot;\nEnter block name: &quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;79</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">PromptResult</span> pr = ed.GetString(opts);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;80</span>&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;81</span>&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;82</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Transaction</span> tr =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;83</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; doc.TransactionManager.StartTransaction();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;84</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">using</span> (tr)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;85</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;86</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Then open the block table and check the</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;87</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// block definition exists</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;88</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">BlockTable</span> bt =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;89</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;90</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db.BlockTableId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;91</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;92</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;93</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">if</span> (!bt.Has(pr.StringResult))</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;94</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;95</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nBlock not found.&quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;96</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;97</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">else</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;98</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; &#0160;99</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">ObjectId</span> bdId = bt[pr.StringResult];</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 100</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 101</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// We loop until the jig is cancelled</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 102</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">while</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 103</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 104</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Create the block reference and</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 105</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// add it to the jig</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 106</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Point3d</span> pt = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Point3d</span>(0, 0, 0);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 107</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">BlockReference</span> br =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 108</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">BlockReference</span>(pt, bdId);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 109</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 110</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">BlockJig</span> entJig = <span style="COLOR: blue">new</span> <span style="COLOR: teal">BlockJig</span>(br);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 111</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 112</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Perform the jig operation</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 113</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;pr = ed.Drag(entJig);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 114</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 115</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 116</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// If all is OK, let&#39;s go and add the</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 117</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// entity to the modelspace</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 118</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">BlockTableRecord</span> ms =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 119</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 120</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 121</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">OpenMode</span>.ForWrite</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 122</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 123</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ms.AppendEntity(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 124</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; entJig.GetEntity()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 125</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 126</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.AddNewlyCreatedDBObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 127</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; entJig.GetEntity(),</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 128</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 129</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 130</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 131</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Start attrib/annot-scale support code</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 132</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">BlockTableRecord</span> bd =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 133</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 134</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;bdId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 135</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 136</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 137</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">if</span> (bd.Annotative == <span style="COLOR: teal">AnnotativeStates</span>.True)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 138</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 139</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">ObjectContextManager</span> ocm =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 140</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db.ObjectContextManager;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 141</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">ObjectContextCollection</span> occ =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 142</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ocm.GetContextCollection(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 143</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: maroon">&quot;ACDB_ANNOTATIONSCALES&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 144</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 145</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">ObjectContexts</span>.AddContext(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 146</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;br,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 147</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;occ.CurrentContext</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 148</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 149</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 150</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 151</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Add the attributes</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 152</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> attId <span style="COLOR: blue">in</span> bd)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 153</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 154</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">Entity</span> ent =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 155</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: teal">Entity</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 156</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; attId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 157</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 158</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 159</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (ent <span style="COLOR: blue">is</span> <span style="COLOR: teal">AttributeDefinition</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 160</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 161</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">AttributeDefinition</span> ad =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 162</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="COLOR: teal">AttributeDefinition</span>)ent;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 163</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">AttributeReference</span> ar =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 164</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">AttributeReference</span>();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 165</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ar.SetAttributeFromBlock(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 166</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ad,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 167</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; br.BlockTransform</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 168</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 169</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;br.AttributeCollection.AppendAttribute(ar);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 170</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.AddNewlyCreatedDBObject(ar, <span style="COLOR: blue">true</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 171</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 172</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&#0160; 173</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// End attrib/annot-scale support code</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 174</span> </p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 175</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Call a function to make the graphics display</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 176</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// (otherwise it will only do so when we Commit)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 177</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; doc.TransactionManager.QueueForGraphicsFlush();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 178</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 179</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 180</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 181</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 182</span>&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 183</span>&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 184</span>&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 185</span>&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 186</span> }</p></div>
<p>You&#39;ll notice the lines in <span style="COLOR: red">red</span>, from lines 131 to 173, have been added to the previous version of the code. The lines from 137 to 149 will only work with versions since AutoCAD 2008, as they&#39;re related to annotation scaling. As mentioned <a href="http://through-the-interface.typepad.com/through_the_interface/2007/04/making_autocad_.html">previously</a>, this section of code relies on AcMgdInternal.dll, an unsupported assembly which is liable to change in future releases.</p>
<p><strong><em>Update:</em></strong></p>
<p>Subsequent to the comment from Roland, I&#39;ve modified the above code to attach the annotation scale earlier, before the jig starts. This allows the jig to properly represent the block being placed (as otherwise annotative blocks will not display during the jig operation):</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Internal;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> BlockJigTest</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; <span style="COLOR: blue">class</span> <span style="COLOR: teal">BlockJig</span> : <span style="COLOR: teal">EntityJig</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: teal">Point3d</span> mCenterPt, mActualPoint;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">public</span> BlockJig(<span style="COLOR: teal">BlockReference</span> br)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;: <span style="COLOR: blue">base</span>(br)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;mCenterPt = br.Position;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">protected</span> <span style="COLOR: blue">override</span> <span style="COLOR: teal">SamplerStatus</span> Sampler(<span style="COLOR: teal">JigPrompts</span> prompts)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">JigPromptPointOptions</span> jigOpts =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">JigPromptPointOptions</span>();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;jigOpts.UserInputControls =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="COLOR: teal">UserInputControls</span>.Accept3dCoordinates</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; | <span style="COLOR: teal">UserInputControls</span>.NoZeroResponseAccepted</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; | <span style="COLOR: teal">UserInputControls</span>.NoNegativeResponseAccepted);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;jigOpts.Message =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: maroon">&quot;\nEnter insert point: &quot;</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">PromptPointResult</span> dres =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; prompts.AcquirePoint(jigOpts);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (mActualPoint == dres.Value)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span> <span style="COLOR: teal">SamplerStatus</span>.NoChange;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">else</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; mActualPoint = dres.Value;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">return</span> <span style="COLOR: teal">SamplerStatus</span>.OK;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">protected</span> <span style="COLOR: blue">override</span> <span style="COLOR: blue">bool</span> Update()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;mCenterPt = mActualPoint;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">try</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; ((<span style="COLOR: teal">BlockReference</span>)Entity).Position = mCenterPt;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">catch</span> (System.<span style="COLOR: teal">Exception</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">return</span> <span style="COLOR: blue">true</span>;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: teal">Entity</span> GetEntity()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">return</span> Entity;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;BJIG&quot;</span>)]</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> CreateBlockWithJig()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Document</span> doc =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Database</span> db = doc.Database;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// First let&#39;s get the name of the block</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">PromptStringOptions</span> opts =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptStringOptions</span>(<span style="COLOR: maroon">&quot;\nEnter block name: &quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">PromptResult</span> pr = ed.GetString(opts);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Transaction</span> tr =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; doc.TransactionManager.StartTransaction();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">using</span> (tr)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Then open the block table and check the</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// block definition exists</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">BlockTable</span> bt =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.BlockTableId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (!bt.Has(pr.StringResult))</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nBlock not found.&quot;</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">else</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectId</span> bdId = bt[pr.StringResult];</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// We loop until the jig is cancelled</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">while</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Create the block reference and</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// add it to the jig</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Point3d</span> pt = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Point3d</span>(0, 0, 0);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">BlockReference</span> br =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">BlockReference</span>(pt, bdId);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Start annot-scale support code</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">BlockTableRecord</span> bd =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;bdId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Using will dispose of the block definition</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// when no longer needed</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">using</span> (bd)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (bd.Annotative == <span style="COLOR: teal">AnnotativeStates</span>.True)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContextManager</span> ocm =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.ObjectContextManager;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContextCollection</span> occ =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ocm.GetContextCollection(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: maroon">&quot;ACDB_ANNOTATIONSCALES&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">ObjectContexts</span>.AddContext(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; br,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; occ.CurrentContext</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// End annot-scale support code</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">BlockJig</span> entJig = <span style="COLOR: blue">new</span> <span style="COLOR: teal">BlockJig</span>(br);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: green">// Perform the jig operation</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; pr = ed.Drag(entJig);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// If all is OK, let&#39;s go and add the</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// entity to the modelspace</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">BlockTableRecord</span> ms =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">OpenMode</span>.ForWrite</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ms.AppendEntity(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;entJig.GetEntity()</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.AddNewlyCreatedDBObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;entJig.GetEntity(),</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">true</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Start attribute support code</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; bd =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; bdId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Add the attributes</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> attId <span style="COLOR: blue">in</span> bd)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Entity</span> ent =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; (<span style="COLOR: teal">Entity</span>)tr.GetObject(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; attId,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">OpenMode</span>.ForRead</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (ent <span style="COLOR: blue">is</span> <span style="COLOR: teal">AttributeDefinition</span>)</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">AttributeDefinition</span> ad =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="COLOR: teal">AttributeDefinition</span>)ent;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">AttributeReference</span> ar =</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">new</span> <span style="COLOR: teal">AttributeReference</span>();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; ar.SetAttributeFromBlock(</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ad,</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; br.BlockTransform</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; br.AttributeCollection.AppendAttribute(ar);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; tr.AddNewlyCreatedDBObject(ar, <span style="COLOR: blue">true</span>);</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// End attribute support code</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Call a function to make the graphics display</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// (otherwise it will only do so when we Commit)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; doc.TransactionManager.QueueForGraphicsFlush();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.Commit();</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; &#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160; }</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>
<p><strong><em>Update 2:</em></strong></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2009/03/jigging-an-autocad-block-with-attributes-using-net.html">This more recent post</a> shows how to jig blocks with attributes.</p>
