---
layout: "post"
title: "Introducing Warnamo, a Dynamo package for managing warnings and errors"
date: "2018-07-30 00:06:54"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Autodesk Research"
  - "Debugging"
  - "Dynamo"
  - "Generative design"
original_url: "https://www.keanw.com/2018/07/introducing-warnamo-a-dynamo-package-for-managing-warnings-and-errors.html "
typepad_basename: "introducing-warnamo-a-dynamo-package-for-managing-warnings-and-errors"
typepad_status: "Publish"
---

<p>One of the highlights of <a href="http://keanw.com/2018/07/aec-hackathon-berlin-2018.html" target="_blank">last weekend’s AEC Hackathon in Berlin</a> was getting to meet <a href="http://twitter.com/dewb" target="_blank">Michael</a> and <a href="https://twitter.com/keith_alfaro" target="_blank">Keith</a> from the <a href="https://twitter.com/DynamoBIM" target="_blank">Dynamo</a> team. They’d delivered a pre-event workshop on the Friday and stayed on to tutor the many Dynamo-centric teams participating in the weekend’s Hackathon.</p><p>On afternoon on Saturday Keith and Michael presented an additional session that briefly mentioned Refinery (the optimization engine for Dynamo that’s currently in Beta) but focused mainly on the API infrastructure that enabled its integration into Dynamo: the <a href="http://developer.dynamobim.org/03-Development-Options/3-6-extensions.html" target="_blank">View Extension API</a>. It was really helpful for me to see how it’s possible to extend Dynamo with packages that contain binary components that extend the standard UI.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad384dd21200d-pi" target="_blank"><img width="500" height="375" title="Michael and Keith talking Dynamo at the AEC Hackathon in Berlin" style="margin: 30px auto; border-image: none; float: none; display: block; background-image: none;" alt="Michael and Keith talking Dynamo at the AEC Hackathon in Berlin" src="/assets/image_844922.jpg" border="0"></a></p><p>At the end of the weekend, I gave Michael a list of issues that we’d come across during the project Autodesk Research had worked on for Van Wijnen. One of these issues related to more easily finding issues in the graph: the Van Wijnen project resulted in a huge graph which was often quite challenging to navigate. One frustration I regularly had was scanning through the graph to find the “first” problem in the flow of data… in a data-flow environment you almost always need to fix the earliest nodes that causes a problem. Once that’s fixed you’ll need to look to see if there are still any later warnings, of course.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3a4a319200b-pi" target="_blank"><img width="500" height="312" title="Finding the first warning in this graph - a proverbial needle in a haystack" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Finding the first warning in this graph - a proverbial needle in a haystack" src="/assets/image_955083.jpg" border="0"></a></p><p>Anyway, in a huge graph – such as the one above – it’s sometimes really hard to find out which problem is the first that needs addressing. My thinking was that it’d be great to have a “take me to the earliest problem” button inside Dynamo. Keith and Michael both suggested this would be a great feature to implement via the View Extension API. This was music to my ears… not only an opportunity to dig into the Dynamo API, but a way to get a feature implemented much more quickly that it would otherwise be (given the need for it to be prioritised among other outstanding features). Oh, and a great topic to blog about and share the code for, of course. :-)</p><p>So it was that this weekend – as we had a very welcome rainy day in Switzerland on Saturday – I dug into the Dynamo API and created my first working Dynamo package (at least the first that contains binaries and not just custom nodes). It was actually really fun to have my own private Hackathon…. after spending last weekend helping people solve problems and deliver their own cool projects, I was itching to work on something similar.</p><p>At Keith’s suggestion, the starting point for my work was the <a href="https://github.com/DynamoDS/DynamoSamples/tree/master/src/SampleViewExtension" target="_blank">SampleViewExtension</a> project in the <a href="https://github.com/DynamoDS" target="_blank">Dynamo GitHub repo</a>. It was really easy to create a simple View Extension and get it loaded into Dynamo Sandbox. Keither also suggested some other great resources, such as the material from a <a href="http://dynamobim.org/extensions-workshop-materials-now-available/" target="_blank">recent Dynamo workshop in the UK</a> (<a href="http://dynamobim.org/learn/#workshop" target="_blank">the various Dynamo workshop material can be found here</a>).</p><p>After that I managed, feature by feature (and with a bit more guidance from Keith), to work out how to select nodes in the graph, zoom to fit them into the current view and then display their warning messages, driving all of this from a WPF DataGrid you could use to navigate through the various problem nodes that had been sorted by location. My logic for the sorting was fairly rudimentary: as data in Dynamo flows from left to right, I just take the X coordinate of the node’s top-left corner. With a cluttered graph this could easily not be the first problem node, but hey: at some point I may extend the implementation to follow connectors and really understand the graph’s layout, flowing from inputs to outputs.</p><p>Given the need to keep large graphs cleanly structured, the current implementation worked very well for our recent projects, at least.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad35ece66200c-pi" target="_blank"><img width="500" height="312" title="Finding the problem node in a huge graph" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Finding the problem node in a huge graph" src="/assets/image_295777.jpg" border="0"></a></p><p>By the end of the weekend I had a first cut of the package that I uploaded to the <a href="https://dynamopackages.com/#" target="_blank">Dynamo Package Manager</a> under the name “Warnamo”. (I almost went with “DynaWarn” but figured that “Walmsley’s Wonderful Warnamo” had a better ring to it. ;-)</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad35ece6e200c-pi" target="_blank"><img width="500" height="339" title="Warnamo - the newest package in town" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="Warnamo - the newest package in town" src="/assets/image_949779.jpg" border="0"></a></p><p>Here’s a quick video of how it works. You can go through the list of nodes with the up and down arrow keys, which makes navigating warnings really easy.</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3a4a321200b-pi" target="_blank"><img width="500" height="312" title="Warnamo" style="margin: 30px auto; float: none; display: block;" alt="Warnamo" src="/assets/image_885292.jpg"></a></p><p>I’ve now <a href="https://github.com/KeanW/Warnamo" target="_blank">uploaded the code to GitHub</a>, in case you’d like to check it out (or even extend it and submit a pull request). As it’s been ages since I’ve posted C# code to my blog, here’s one of the main source files (as it stands today, anyway):</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.Core;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.Extensions;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.Graph.Nodes;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.Graph.Workspaces;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.Models;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.ViewModels;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Dynamo.Wpf.ViewModels.Core;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.ObjectModel;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.ComponentModel;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Linq;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> WarningsViewExtension</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">WarningsWindowViewModel</span> : <span style="color: rgb(43, 145, 175);">NotificationObject</span>, <span style="color: rgb(43, 145, 175);">IDisposable</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: rgb(43, 145, 175);">ObservableCollection</span>&lt;<span style="color: rgb(43, 145, 175);">NodeInfo</span>&gt; _warningNodes;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: rgb(43, 145, 175);">ReadyParams</span> _readyParams;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: rgb(43, 145, 175);">DynamoViewModel</span> _dynamoViewModel;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: rgb(43, 145, 175);">NodeViewModel</span> _displaying;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: rgb(43, 145, 175);">ObservableCollection</span>&lt;<span style="color: rgb(43, 145, 175);">NodeInfo</span>&gt; WarningNodes</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _warningNodes = getWarningNodes();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> _warningNodes;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _warningNodes = <span style="color: blue;">value</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: rgb(43, 145, 175);">ObservableCollection</span>&lt;<span style="color: rgb(43, 145, 175);">NodeInfo</span>&gt; getWarningNodes()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_displaying != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HideTooltip(_displaying);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _displaying = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Collect error/warning nodes, sorting by X position</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// The second Select replaces the blank (0) ID with the row number</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> nodeList =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (<span style="color: blue;">from</span> n <span style="color: blue;">in</span> _readyParams.CurrentWorkspaceModel.Nodes</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">where</span> n.State != <span style="color: rgb(43, 145, 175);">ElementState</span>.Active &amp;&amp; n.State != <span style="color: rgb(43, 145, 175);">ElementState</span>.Dead</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">orderby</span> n.Rect.TopLeft.X <span style="color: blue;">ascending</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">select</span> <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">NodeInfo</span>(0, n.Name, n.GUID)).Select(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (item, index) =&gt; <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">NodeInfo</span>(index + 1, item.Name, item.GUID)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Return a bindable collection</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">ObservableCollection</span>&lt;<span style="color: rgb(43, 145, 175);">NodeInfo</span>&gt;(nodeList);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Construction &amp; disposal</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> WarningsWindowViewModel(<span style="color: rgb(43, 145, 175);">ReadyParams</span> p, <span style="color: rgb(43, 145, 175);">DynamoViewModel</span> dynamoVM)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _readyParams = p;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _dynamoViewModel = dynamoVM;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _readyParams.CurrentWorkspaceChanged +=</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ReadyParams_CurrentWorkspaceChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AddEventHandlers(_readyParams.CurrentWorkspaceModel);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Dispose()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _readyParams.CurrentWorkspaceChanged -=</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ReadyParams_CurrentWorkspaceChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RemoveEventHandlers(_readyParams.CurrentWorkspaceModel);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Event handlers</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">void</span> ReadyParams_CurrentWorkspaceChanged(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dynamo.Graph.Workspaces.<span style="color: rgb(43, 145, 175);">IWorkspaceModel</span> obj</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_readyParams != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RemoveEventHandlers(_readyParams.CurrentWorkspaceModel);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AddEventHandlers(obj);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RaisePropertyChanged(<span style="color: rgb(163, 21, 21);">"WarningNodes"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> CurrentWorkspaceModel_NodeAdded(<span style="color: rgb(43, 145, 175);">NodeModel</span> node)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; node.PropertyChanged += node_PropertyChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> CurrentWorkspaceModel_NodeRemoved(<span style="color: rgb(43, 145, 175);">NodeModel</span> node)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; node.PropertyChanged -= node_PropertyChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> CurrentWorkspaceModel_NodesCleared()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> node <span style="color: blue;">in</span> _readyParams.CurrentWorkspaceModel.Nodes)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; node.PropertyChanged -= node_PropertyChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RaisePropertyChanged(<span style="color: rgb(163, 21, 21);">"WarningNodes"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> node_PropertyChanged(<span style="color: blue;">object</span> sender, <span style="color: rgb(43, 145, 175);">PropertyChangedEventArgs</span> e)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (e.PropertyName == <span style="color: rgb(163, 21, 21);">"State"</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RaisePropertyChanged(<span style="color: rgb(163, 21, 21);">"WarningNodes"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Attach and remove handlers</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> AddEventHandlers(<span style="color: rgb(43, 145, 175);">IWorkspaceModel</span> model)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> node <span style="color: blue;">in</span> model.Nodes)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; node.PropertyChanged += node_PropertyChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; model.NodeAdded += CurrentWorkspaceModel_NodeAdded;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; model.NodeRemoved += CurrentWorkspaceModel_NodeRemoved;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; model.NodesCleared += CurrentWorkspaceModel_NodesCleared;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> RemoveEventHandlers(<span style="color: rgb(43, 145, 175);">IWorkspaceModel</span> model)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> node <span style="color: blue;">in</span> model.Nodes)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; node.PropertyChanged -= node_PropertyChanged;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; model.NodeAdded -= CurrentWorkspaceModel_NodeAdded;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; model.NodeRemoved -= CurrentWorkspaceModel_NodeRemoved;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; model.NodesCleared -= CurrentWorkspaceModel_NodesCleared;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ZoomToPosition(<span style="color: rgb(43, 145, 175);">NodeInfo</span> nodeInfo)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> node <span style="color: blue;">in</span> _readyParams.CurrentWorkspaceModel.Nodes)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (node.GUID == nodeInfo.GUID)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// node.Select();</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> cmd = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">DynamoModel</span>.<span style="color: rgb(43, 145, 175);">SelectInRegionCommand</span>(node.Rect, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _readyParams.CommandExecutive.ExecuteCommand(cmd, <span style="color: blue;">null</span>, <span style="color: blue;">null</span>);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Call this twice as otherwise the zoom level altertnates been close</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// and far</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _dynamoViewModel.FitViewCommand.Execute(<span style="color: blue;">null</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _dynamoViewModel.FitViewCommand.Execute(<span style="color: blue;">null</span>);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Display the error/warning message</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> hsvm = (<span style="color: rgb(43, 145, 175);">HomeWorkspaceViewModel</span>)_dynamoViewModel.HomeSpaceViewModel;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> nodeModel <span style="color: blue;">in</span> hsvm.Nodes)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (nodeModel.Id == node.GUID)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// First hide the previously displayed one if there was one</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_displaying != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HideTooltip(_displaying);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowTooltip(nodeModel);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _displaying = nodeModel;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Is the state a warning?</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">bool</span> IsWarning(<span style="color: rgb(43, 145, 175);">ElementState</span> state)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; state == <span style="color: rgb(43, 145, 175);">ElementState</span>.Warning ||</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; state == <span style="color: rgb(43, 145, 175);">ElementState</span>.PersistentWarning;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Expand the warning bubble for the provided NodeViewModel</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> ShowTooltip(<span style="color: rgb(43, 145, 175);">NodeViewModel</span> nvm)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> data = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">InfoBubbleDataPacket</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; data.Style =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IsWarning(nvm.State) ?</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">InfoBubbleViewModel</span>.<span style="color: rgb(43, 145, 175);">Style</span>.Warning :</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">InfoBubbleViewModel</span>.<span style="color: rgb(43, 145, 175);">Style</span>.Error;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; data.ConnectingDirection = <span style="color: rgb(43, 145, 175);">InfoBubbleViewModel</span>.<span style="color: rgb(43, 145, 175);">Direction</span>.Bottom;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; nvm.ErrorBubble.ShowFullContentCommand.Execute(data);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Collapse he warning bubble for the provided NodeViewModel</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> HideTooltip(<span style="color: rgb(43, 145, 175);">NodeViewModel</span> nvm)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> data = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">InfoBubbleDataPacket</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; data.Style =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IsWarning(nvm.State) ?</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">InfoBubbleViewModel</span>.<span style="color: rgb(43, 145, 175);">Style</span>.WarningCondensed :</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">InfoBubbleViewModel</span>.<span style="color: rgb(43, 145, 175);">Style</span>.ErrorCondensed;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; data.ConnectingDirection = <span style="color: rgb(43, 145, 175);">InfoBubbleViewModel</span>.<span style="color: rgb(43, 145, 175);">Direction</span>.Bottom;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; nvm.ErrorBubble.ShowCondensedContentCommand.Execute(data);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><br></p>
</div>
<p>I’m thinking of generalizing the code to create a core, generic “node manager” implementation that could be used (among other things) to search through a graph for all the “Preview” nodes in a graph and make sure they’re only located in a specified region. The fact that “Preview” is set to true by default on newly created nodes often causes problems when you’re trying to make sure only certain layers of the graph contribute preview graphics (other than during development and debugging). But maybe that’s just something that frustrates me? It’s quite possible.</p><p>Anyway, if you’d like to check out Warnamo, please install it from the package manager and let me know how you get on with it. This is my first attempt at publishing something for Dynamo, so I’m really keen to get your feedback!</p>
