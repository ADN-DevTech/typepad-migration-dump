---
layout: "post"
title: "Timliner API-part3"
date: "2012-10-31 02:53:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/10/timliner-api-part3.html "
typepad_basename: "timliner-api-part3"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>continued with Timeliner API-part2</p>
<p><strong>Programmatically Refreshing Data Source</strong></p>
<p>If we want to refresh a data source as we do in the UI, the relevant source code would be something like the following. Notice methods, DocumentTimeliner.TaskMergeRebuild(), TaskMergeSynchronize(), DataSourceReplaceWithCopy() toward the end of the code.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Refresh Data Source </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Get the timeliner object </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Nw.</span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc = Nw.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Nw.DocumentParts.</span><span style="line-height: 140%; color: #2b91af;">IDocumentTimeliner</span><span style="line-height: 140%;"> tl = doc.Timeliner;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Tl.</span><span style="line-height: 140%; color: #2b91af;">DocumentTimeliner</span><span style="line-height: 140%;"> tl_doc = (Tl.</span><span style="line-height: 140%; color: #2b91af;">DocumentTimeliner</span><span style="line-height: 140%;">)tl;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> name = </span><span style="line-height: 140%; color: #a31515;">&quot;MyTLDSName&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Find the relevant data source and its provider. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;"> ds = tl_doc.DataSourceFindByDisplayName(name).CreateCopy(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSourceProvider</span><span style="line-height: 140%;"> provider = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSourceProvider</span><span style="line-height: 140%;">.FindDataSourceProvider(data_source).</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">LoadedPlugin </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSourceProvider</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;"> != provider &amp;&amp; provider.IsAvailable)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// call ImportTasks on that copy.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerImportTasksResult</span><span style="line-height: 140%;"> result = provider.ImportTasks(ds);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// get tasks from data source</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;"> task = result.RootTask;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// merge with existing tasks&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Rebuild ：oReplace the hierarchy of tasks relating to the specified hierarchy, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// but retain any Navisworks TimelinerTask data such as attached selections, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// scripts, animations etc.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tl_doc.TaskMergeRebuild(task, result.TaskTypeWasSet.Value);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">//Synchronize</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tl_doc.TaskMergeSynchronize(task, result.TaskTypeWasSet.Value);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">//replace with updated data source - update sync time&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tl_doc.DataSourceReplaceWithCopy(tl_doc.DataSources.IndexOfDisplayName(name), ds); </span></p>
</div>
