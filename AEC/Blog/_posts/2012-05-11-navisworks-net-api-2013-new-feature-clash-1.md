---
layout: "post"
title: "Navisworks .NET API 2013 new feature - Clash 1"
date: "2012-05-11 02:20:49"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-1.html "
typepad_basename: "navisworks-net-api-2013-new-feature-clash-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Autodesk® Navisworks® 2013 software has exposed some new .NET APIs. The Clash Detection API is exposed. The TimeLiner API has continued to be enhanced since Navisworks 2013 TimeLiner feature includes Cost and thus provides 5D simulations. On the product side, Navisworks can not only open the file of the native Autodesk® Revit® format, but also import Grid and Level from a Revit file and the relevant API methods have been provided as well.&#0160; In the main product, you will also find the ability to drag the Current Selection from the main window and selection tree which we plan to allow third party applications to use in Viewer controls. We also exposed Current Viewpoint, Saved Viewpoints and Camera. NWCreate now supports the creation of Grids and Levels. In addition, if you need to make use of the COM API, you might be glad to see the COM samples migrated from VB6 to C#.</p>
<p>This article series will introduce the new APIs. In the first section, we take a look at Clash Detection. If you are professional with Navisworks API, you could just go ahead to enjoy the SDK sample in &lt;Navisworks Manage 2013&gt;\api\net\examples\PlugIns\ClashDetective.</p>
<p>The Clash API allows you to access Clash Test, Clash Result and relevant information. First, you need to add the assembly &lt;Navisworks Path&gt;\Autodesk.Navisworks.Clash.dll to your plug-in and import the namespace: Autodesk.Navisworks.Api.Clash.</p>
<p>Following are the main classes of Clash .NET API:<br />•&#0160;DocumentClash: provides access to the various document-parts associated with a clash. <br />•&#0160;DocumentClashTests: the document-part that holds the currently defined clash tests to access clash tests also provides the methods to modify tests or results.<br />•&#0160;ClashTest: a clash-test, including all its settings and results.&#0160; <br />•&#0160;ClashResult: represents a single geometry clash detected in a clash test. <br />•&#0160;ClashResultGroup: permits grouping of multiple clash results together which can represent different aspects of the same issues</p>
<p><strong>Get Clash </strong></p>
<p>The Document class has a method called GetClash() which provides access to the DocumentClash object. DocumentClash.TestsData in turn, provides access to an object named DocumentClashTests which allows us to access all the clash tests. The Tests property on this DocumentClashTests helps get each test-related information like display name, tolerance, simulation type, etc. The Children property of each ClashTest helps further drill down to ClashResultGroup and further into each ClashResult. The code sample below shows how to get some information of the tests and drill down to the results and write the information to a text file.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows.Forms;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.Navisworks.Api;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.Navisworks.Api.DocumentParts;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.Navisworks.Api.Clash;</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">&#0160;</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;<span style="color: green;">// Sample: dump clash</span></p>
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">void</span> dumpClash()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// create a stream to dump clash information</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;will dump Clash to c:\\dumpClash.txt&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">StreamWriter</span> sw = <span style="color: #2b91af;">File</span>.CreateText(<span style="color: #a31515;">&quot;c:\\dumpClash.txt&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Document</span> document =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Autodesk.Navisworks.Api.<span style="color: #2b91af;">Application</span>.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DocumentClash</span> documentClash = document.GetClash();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DocumentClashTests</span> oDCT = documentClash.TestsData;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//dump clash tests</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ClashTest</span> test <span style="color: blue;">in</span> oDCT.Tests)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;***Test: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.DisplayName + <span style="color: #a31515;">&quot;***&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; Status: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.Status.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; TestType: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.TestType.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (test.LastRun != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; LastRun: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.LastRun.Value.ToShortDateString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; tolerance: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.Tolerance);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; comments: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.Comments);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; SimulationType: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.SimulationType.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; SimulationStep: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; test.SimulationStep.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160;&#0160;&#0160; ---Results---&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// dump Clash Result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">SavedItem</span> issue <span style="color: blue;">in</span> test.Children)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ClashResultGroup</span> group =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; issue <span style="color: blue;">as</span> <span style="color: #2b91af;">ClashResultGroup</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">null</span> != group)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160; test result group: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; group.DisplayName);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160; group status: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; group.Status.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">SavedItem</span> issue1 <span style="color: blue;">in</span> group.Children)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ClashResult</span> rt1 = issue <span style="color: blue;">as</span> <span style="color: #2b91af;">ClashResult</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">null</span> != rt1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; writeClashResult(rt1, sw);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ClashResult</span> rt = issue <span style="color: blue;">as</span> <span style="color: #2b91af;">ClashResult</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">null</span> != rt)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; writeClashResult(rt, sw);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }<span style="color: green;">//Result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; } <span style="color: green;">//Test</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(<span style="color: #a31515;">&quot;done!&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; sw.Close();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">void</span> writeClashResult(<span style="color: #2b91af;">ClashResult</span> rt,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">StreamWriter</span> sw)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//dump some information of the result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160;&#0160;&#0160; &#0160; test result: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rt.DisplayName);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (rt.CreatedTime != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160; CreatedTime: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rt.CreatedTime.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160; Status: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rt.Status.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot;&#0160; ApprovedBy: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rt.ApprovedBy);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (rt.Center != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; Center[{0},{1},{2}]: &quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rt.Center.X, rt.Center.Y, rt.Center.Z);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; sw.WriteLine(<span style="color: #a31515;">&quot; SimulationType: &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; rt.SimulationType.ToString());</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>ClashTest configures the selections which are involved in a test. ClashTestResult returns the selections which are collisions. The code snippet below gets the selections from one test and one result, and highlights the selection2 of one result.&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">//Sample: Get selection information of Clash Test and Clash Result</span></p>
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">void</span> getSelection()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Document</span> document = <span style="color: #2b91af;">Application</span>.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DocumentClash</span> documentClash = document.GetClash();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DocumentClashTests</span> oDCT = documentClash.TestsData;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ClashTest</span> t = documentClash.TestsData.Tests[0] <span style="color: blue;">as</span> <span style="color: #2b91af;">ClashTest</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (t != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">//get SelectionA and SelectionB of one Clash Test</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelItemCollection</span> oSelA = t.SelectionA.Selection.GetSelectedItems();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelItemCollection</span> oSelB = t.SelectionB.Selection.GetSelectedItems();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ClashResult</span> rt = t.Children[0] <span style="color: blue;">as</span> <span style="color: #2b91af;">ClashResult</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (rt != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">//get Clash Selections of the result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelItemCollection</span> oSel1 = rt.Selection1;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ModelItemCollection</span> oSel2 = rt.Selection2;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">//highlight Selection2 of the result</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; document.CurrentSelection.CopyFrom(oSel2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
<p>There are two methods to run all tests or specific test.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DocumentClashTests</span> oDCT = documentClash.TestsData;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//run all tests</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// oDCT.TestsRunAllTests();</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//or run one test</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ClashTest</span> oFirstTest = oDCT.Tests[0] <span style="color: blue;">as</span> <span style="color: #2b91af;">ClashTest</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDCT.TestsRunTest(oFirstTest);</p>
</div>
<p>&#0160;</p>
<p>Next, we will look at how to update clash.</p>
<p>(to be continued)</p>
