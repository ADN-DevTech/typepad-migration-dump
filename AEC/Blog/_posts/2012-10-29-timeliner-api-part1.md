---
layout: "post"
title: "Timeliner API-part1"
date: "2012-10-29 02:43:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/10/timeliner-api-part1.html "
typepad_basename: "timeliner-api-part1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Navisworks 2012 .NET API provides the ability to access TimeLiner information. Using the TimeLiner API, you can access TimeLiner document data, and create/query/modify tasks and task types as well as simulation appearances. You can also query model items attached to tasks, and define your own data source for tasks. In this article, we will look at these functionalities with Navisworks TimeLiner .NET API.</p>
<p><strong>Getting TimeLiner Information      <br /></strong></p>
<p>The top most objects of the TimeLiner API are IDocumentTimeliner and DocumentTimeliner.&#0160; IDocumentTimeliner is an intermediary interface and is defined in the Autodesk.Navisworks.Api namespace. We use it to access Timeliner object from a given document.&#0160; DocumentTimeliner class is defined in the Autodesk.Navisworks.Timeliner namespace. We use this to further access the information in the Timeliner object.</p>
<p>To access a Timeliner object, you first obtain IDocumentTimeliner object from the document, we then cast to the DocumentTimeliner class. Alternatively, we can access Timeliner object from a document using Timeliner.TimelinerDocumentExtensions.GetTimeliner(Document) method. We can then further access for more information from the properties and methods of the DocumentTimeliner object; for example, for further drilling down the data sources.</p>
<p>The code below shows a sample code to dump the data sources and tasks to the debug window:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// get Timeliner of the document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Nw = Autodesk.Navisworks.Api;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Tl = Autodesk.Navisworks.Api.Timeliner;</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> DumpTimeLinerInfo()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// get Timeliner of the document</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Nw.</span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc = Nw.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Nw.DocumentParts.</span><span style="line-height: 140%; color: #2b91af;">IDocumentTimeliner</span><span style="line-height: 140%;"> tl = doc.Timeliner;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">DocumentTimeliner</span><span style="line-height: 140%;"> tl_doc = (Tl.</span><span style="line-height: 140%; color: #2b91af;">DocumentTimeliner</span><span style="line-height: 140%;">)tl;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// alternatively, you can also use a static method that slightly </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// simplifies the access to the DocumentTimeLiner slightly. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//Tl.DocumentTimeliner tl_doc2 = Tl.TimelinerDocumentExtensions.GetTimeliner(doc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// dump some information of each data source</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;"> oDS </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> tl_doc.DataSources)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Debug.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;\n DisplayName: &quot;</span><span style="line-height: 140%;"> + oDS.DisplayName +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\n ProviderName: &quot;</span><span style="line-height: 140%;"> + oDS.DataSourceProviderName);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSourceField</span><span style="line-height: 140%;"> oTlF </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> oDS.AvailableFields)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; Debug.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;\n DisplayName:&#0160; &quot;</span><span style="line-height: 140%;"> + oTlF.DisplayName);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; } </span><span style="line-height: 140%; color: green;">// end dump data sources&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// dump some information of each task</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;"> oTask </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> tl_doc.Tasks)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Debug.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&#0160; DisplayName: &quot;</span><span style="line-height: 140%;"> + oTask.DisplayName +&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\n&#0160; Planned Start: &quot;</span><span style="line-height: 140%;"> + oTask.PlannedStartDate.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// you can also access selection of a given task </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (although we aren’ˉt printing out here.) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerSelection</span><span style="line-height: 140%;"> oTlSel = oTask.Selection;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (oTlSel != </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(oTlSel.HasExplicitSelection) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Nw.</span><span style="line-height: 140%; color: #2b91af;">ModelItemCollection</span><span style="line-height: 140%;"> oExplicitSel = oTlSel.ExplicitSelection;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (oTlSel.HasSearch) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Nw.</span><span style="line-height: 140%; color: #2b91af;">Search</span><span style="line-height: 140%;"> oSearch = oTlSel.Search; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; } </span><span style="line-height: 140%; color: green;">//end dump tasks</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p><strong>Attaching a Selection to a Task</strong></p>
<p>To attach the selection to a task, you first need to make a copy of the task, modify the copy and replace the original with the copy. The code below shows an example of attaching the current selection to all the tasks at the root level:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// attach a current selection to the time liner tasks at the root level </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AttachSelection()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get the Timeliner document part from an active document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> main_doc = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">DocumentTimeliner</span><span style="line-height: 140%;"> doc_tl = Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDocumentExtensions</span><span style="line-height: 140%;">.GetTimeliner(main_doc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// copy the current set of tasks. get the current selection. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">GroupItem</span><span style="line-height: 140%;"> root_copy = doc_tl.TasksRoot.CreateCopy() </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">GroupItem</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Selection</span><span style="line-height: 140%;"> current = main_doc.CurrentSelection;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// walk through the tasks at the root level and attach the current selection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;"> task </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> root_copy.Children)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {&#0160; </span><span style="line-height: 140%; color: green;">// attach the selection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; task.Selection.CopyFrom(current);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// replace the timeliner tasks</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; doc_tl.TasksCopyFrom(root_copy.Children);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
Note: the code above does not go through the hierarchy. We also assume that the tasks have valid TaskTypes, DataSources and ExternalId, which should be the case if you haven’t touched them yet.
