---
layout: "post"
title: "Timeliner API-part2"
date: "2012-10-30 02:48:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/10/timeliner-api-part2.html "
typepad_basename: "timeliner-api-part2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>continued with Timeliner API-part1</p>
<p><strong>Custom Data Source and Task</strong></p>
<p>Using TimeLiner API, you can customize the data source and import your own tasks. This is done through the TimeLiner plug-in. The following shows how to define a custom data source importer, step by step:</p>
<ol>
<li>First, we need to define a plug-in implementing TimelinerDataSourceProvider. (At this point, only the skeleton of the class is shown. We’ll fill in more details later.) :</li>
</ol>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// define a timeliner data source provider </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">PluginAttribute</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;TestTimeLiner&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;ADSK&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">Interface</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;TimelinerDataSourceProvider&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;Navisworks&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">myTLDataSourceClass</span><span style="line-height: 140%;"> : Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSourceProvider</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// implement abstract property IsAvailable</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> IsAvailable { </span><span style="line-height: 140%; color: blue;">get</span><span style="line-height: 140%;"> { </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">; } }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// implement abstract method ValidateSettings. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// You do not need to implement the details.&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerValidateSettingsResult</span><span style="line-height: 140%;"> ValidateSettings(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;"> dataSource)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerValidateSettingsResult</span><span style="line-height: 140%;"> result =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerValidateSettingsResult</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> result;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>2. Next, override CreateDataSource(). This method provides access to the data source, e.g. showing logging dialog boxes or a file open dialog box. This data source should contain details of the available fields for the Field Selector Dialog box, and added to the AvailableFields collection. In the sample below, we ask the user to select a CSV file. (We assume that before running this sample code, we have created some tasks on the sample model gatehouse.nwd and exported its Timeliner):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Import data source from a custom csv file </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;"> CreateDataSource(</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> displayName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// create Data Source with a unique name. If the name isn&#39;t set, the plugin</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// framework provides default name: &quot;New Data Source&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;"> newDS = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;">(displayName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; newDS.DisplayName = </span><span style="line-height: 140%; color: #a31515;">&quot;MyTLDSName&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// prompt to select a CSV file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">OpenFileDialog</span><span style="line-height: 140%;"> dlg = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">OpenFileDialog</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; dlg.Title = </span><span style="line-height: 140%; color: #a31515;">&quot;select file&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; dlg.Filter = </span><span style="line-height: 140%; color: #a31515;">&quot;csv files (*.csv)|*.csv&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// set data source&#39;s identifier for future reconnect </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (dlg.ShowDialog() == </span><span style="line-height: 140%; color: #2b91af;">DialogResult</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newDS.ProjectIdentifier = dlg.FileName;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// populate the AvailableFields we wish people to map. Theoretically you </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// could provide no field mappings and always map the same fields. In this </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// sample, we ignore the fields </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// newDS.AvailableFields.Add(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//&#0160;&#0160; new Tl.TimelinerDataSourceField(&quot;Task Name&quot;, &quot;Task Name&quot;));&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// must have Plugin.Id which should be a property on the subclass</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TimelinerDataSourceProvider. The plugin framework finds the plugin to </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// automate for the data source&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; newDS.DataSourceProviderId = </span><span style="line-height: 140%; color: blue;">base</span><span style="line-height: 140%;">.Id;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// version, whatever you like</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; newDS.DataSourceProviderVersion = 1.0;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// some user friendly name, ideally matching the name of the plugin itself&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; newDS.DataSourceProviderName = </span><span style="line-height: 140%; color: #a31515;">&quot;TestTimeLiner&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> newDS;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>3. Override ImportTasksCore(). This should do the actual work of importing the tasks. Autodesk® Navisworks® software will call it when Rebuild/Synchronize the custom data source. We need to create and return a TimelinerImportTasksResult and implement its RootTask. RootTask is the entire hierarchy of tasks and its children we want to import:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// implementing ImportTasksCore()&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// This is the part which does the actual work importing.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerImportTasksResult</span><span style="line-height: 140%;"> ImportTasksCore(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerDataSource</span><span style="line-height: 140%;"> dataSource)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// create a TimelinerImportTasksResult and root task</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerImportTasksResult</span><span style="line-height: 140%;"> tr = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerImportTasksResult</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr.RootTask = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Set true if the data source was configured to set </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the SimulationTaskTypeName on the tasks imported</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr.TaskTypeWasSet = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//The Name of the simulation type to which this belongs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr.RootTask.SimulationTaskTypeName = </span><span style="line-height: 140%; color: #a31515;">&quot;TestTimeLiner&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The ExternalId should be set&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; tr.RootTask.ExternalId = Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;">.DataSourceRootTaskIdentifier;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//start to enumerate the hierarchy of the tasks from the external source </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//and add children task below the RootTask. For each task look at the </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//mappings provided on the supplied datasource and obtain the field </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//specified by that mapping by looking at its id</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Or you may know the mapping and never want the user to set </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//read the source data. In this sampl, ProjectIdentifier is the CSV file </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; StreamReader sr = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> StreamReader(dataSource.ProjectIdentifier);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//ignore the header row of CSV file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> strline = sr.ReadLine();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (!sr.EndOfStream)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strline = sr.ReadLine(); </span><span style="line-height: 140%; color: green;">//each row</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// suppose the CSV file is separated by comma</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">[] _values = strline.Split(</span><span style="line-height: 140%; color: #a31515;">&#39;,&#39;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//create sub task</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;"> oSubTask = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Tl.</span><span style="line-height: 140%; color: #2b91af;">TimelinerTask</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//we have known the sequence of each column </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.DisplayName = _values[1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.IsActualEnabled = _values[3] == </span><span style="line-height: 140%; color: #a31515;">&quot;1&quot;</span><span style="line-height: 140%;"> ? </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">.IsNullOrEmpty(_values[4]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.ActualStartDate = </span><span style="line-height: 140%; color: #2b91af;">Convert</span><span style="line-height: 140%;">.ToDateTime(_values[4]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">.IsNullOrEmpty(_values[5]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.ActualEndDate = </span><span style="line-height: 140%; color: #2b91af;">Convert</span><span style="line-height: 140%;">.ToDateTime(_values[5]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.IsPlannedEnabled = _values[6] == </span><span style="line-height: 140%; color: #a31515;">&quot;1&quot;</span><span style="line-height: 140%;"> ? </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">.IsNullOrEmpty(_values[7]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.PlannedStartDate = </span><span style="line-height: 140%; color: #2b91af;">Convert</span><span style="line-height: 140%;">.ToDateTime(_values[7]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">.IsNullOrEmpty(_values[8]))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.PlannedEndDate = </span><span style="line-height: 140%; color: #2b91af;">Convert</span><span style="line-height: 140%;">.ToDateTime(_values[8]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.SimulationTaskTypeName = _values[9];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSubTask.ExternalId = _values[12];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//add the task to root task</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; newTR.RootTask.Children.Add(oSubTask);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; sr.Close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> newTR;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Once the plug-in is loaded, we will see a new data source in the drop down menu. Add one CSV file and build the hierarchy. The tasks are imported.</p>
<p>&#0160;</p>
<p><img alt="" height="257" src="/assets/newsfall2011_2.jpg" width="491" /></p>
