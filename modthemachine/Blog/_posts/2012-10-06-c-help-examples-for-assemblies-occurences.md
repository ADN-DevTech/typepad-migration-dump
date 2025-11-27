---
layout: "post"
title: "C# Help Examples for Assemblies Occurences"
date: "2012-10-06 02:12:00"
author: "Wayne Brill"
categories:
  - "Assemblies"
  - "C#"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/10/c-help-examples-for-assemblies-occurences.html "
typepad_basename: "c-help-examples-for-assemblies-occurences"
typepad_status: "Publish"
---

<p>Here is another section of VBA examples converted to C#. These functions are related to using occurrences in assemblies. This group was fairly easy to migrate compared to some of the other sections I did for previous posts. Also there were several C# examples already in the help file. I added them to the project for completeness.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3c877425970c-pi"><img alt="image" border="0" height="379" src="/assets/image_851547.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="365" /></a></p>
<p>This project has the following functions.</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc68834017d3c8775e1970c"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_assembly_occ.zip">Download InventorHelpExamples_Assembly_Occ</a></span></p>
<p>AddOccurrencesToFolder <br />Demote <br />Promote <br />ReplaceContentCenterPart <br />AssemblyCount <br />MoveOccurrence <br />AddOccurrence <br />AddiAssemblyOccurrence <br />AddiPartOccurrence <br />iMateDuringOccurrencePlacementSample <br />AddOccurrenceWithRepresentations</p>
<p>&#0160;</p>
<p>Here is the AddOccurrencesToFolder function:&quot;</p>
<p><span style="color: green;">// Add assembly occurrences to a new folder API Sample </span></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//Demonstrates assembly occurrences to a new folder</span></p>
<p style="margin: 0px;"><span style="color: green;">//Have an assembly with at least one occurrence in it </span></p>
<p style="margin: 0px;"><span style="color: green;">//and run the sample.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> AddOccurrencesToFolder()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyDocument</span> oDoc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">AssemblyDocument</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = (<span style="color: #2b91af;">AssemblyDocument</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyComponentDefinition</span> oDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">AssemblyComponentDefinition</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDef = oDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">BrowserPane</span> oPane = <span style="color: blue;">default</span>(<span style="color: #2b91af;">BrowserPane</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPane = oDoc.BrowserPanes.ActivePane;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectCollection</span> oOccurrenceNodes =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">ObjectCollection</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oOccurrenceNodes = (<span style="color: #2b91af;">ObjectCollection</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.TransientObjects.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateObjectCollection();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;<span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ComponentOccurrence</span> oOcc</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">in</span> oDef.Occurrences)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BrowserNode</span> oNode = <span style="color: blue;">default</span>(<span style="color: #2b91af;">BrowserNode</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oNode = oPane.GetBrowserNodeFromObject(oOcc);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oOccurrenceNodes.Add(oNode);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">BrowserFolder</span> oFolder = <span style="color: blue;">default</span>(<span style="color: #2b91af;">BrowserFolder</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oFolder = oPane.AddBrowserFolder</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;My Occurrence Folder&quot;</span>, oOccurrenceNodes);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>
