---
layout: "post"
title: "Toggle suppress state of FEA element"
date: "2015-07-17 08:29:58"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/07/toggle-suppress-state-of-fea-element.html "
typepad_basename: "toggle-suppress-state-of-fea-element"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This requires a technique similar to that shown here:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2014/04/run-command-on-browser-item.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2014/04/run-command-on-browser-item.html</a></p>
<p>In this case will have to work on another Browser Pane, the one created by the FEA addin:&#0160;&quot;<strong>Stress Analysis</strong>&quot;</p>
<p>We just have to find the component we want to toggle the <strong>Suppress</strong> state of, then execute the &quot;<strong>FeaSuppressCmD</strong>&quot; command on it:</p>
<pre>Function FindNode( _
node As BrowserNode, label As String) As BrowserNode
  Dim subNode As BrowserNode
  For Each subNode In node.BrowserNodes
    If subNode.BrowserNodeDefinition.label = label Then
      Set FindNode = subNode
      Exit Function
    End If
    Dim foundNode As BrowserNode
    Set foundNode = FindNode(subNode, label)
    If Not foundNode Is Nothing Then
      Set FindNode = foundNode
      Exit Function
    End If
  Next
End Function

Sub SuppressFeaElement()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument

  &#39; Get the control definition we need
  Dim cm As CommandManager
  Set cm = ThisApplication.CommandManager
  
  Dim cds As ControlDefinitions
  Set cds = cm.ControlDefinitions
  
  Dim cd As ControlDefinition
  Set cd = cds(&quot;FeaSuppressCmD&quot;)
  
  &#39; Find the item in the browser tree
  Dim elemName As String
  elemName = &quot;Force:1&quot;
  
  &#39; Stress Analysis browser pane
  Dim bp As BrowserPane
  Set bp = doc.BrowserPanes( _
    &quot;{B3D04494-EDD2-4FDC-9EC2-30BAF8D6B77B}&quot;)
  
  Dim node As BrowserNode
  Set node = FindNode(bp.topNode, elemName)
  
  &#39; Select it
  node.EnsureVisible &#39; might be needed
  node.DoSelect
  
  &#39; Run the command on it
  cd.Execute
End Sub</pre>
<p>The code in action:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13982c1970c-pi" style="display: inline;"><img alt="FEA" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d13982c1970c image-full img-responsive" src="/assets/image_a10efe.jpg" title="FEA" /></a></p>
<p>&#0160;</p>
