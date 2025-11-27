---
layout: "post"
title: "Hide component in DrawingView using ControlDefinition"
date: "2014-10-03 04:21:22"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/hide-component-in-drawingview-using-controldefinition.html "
typepad_basename: "hide-component-in-drawingview-using-controldefinition"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Sometimes if the <strong>API</strong> is not working, one workaround could be selecting the component in the <strong>BrowserPane</strong> and executing the relevant <strong>ControlDefinition</strong> on it:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2012/09/parse-browsernodes-for-a-specified-node-and-perform-userinterface-commands.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2012/09/parse-browsernodes-for-a-specified-node-and-perform-userinterface-commands.html</a></p>
<p>Some components are not hooked up to the browser so you cannot get to the associated node using&#0160;<strong>GetBrowserNodeFromObject</strong>, or it selects the wrong object:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2013/04/get-browsernode-of-an-occurrence.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/04/get-browsernode-of-an-occurrence.html</a></p>
<p>Here is a <strong>VBA</strong> solution that shows how you might get to the object you need and then execute a command on it:</p>
<pre>Function GetOccurrenceNode( _
name As String, nodes As BrowserNodesEnumerator) _
As BrowserNode
  Dim node As BrowserNode
  For Each node In nodes
    If node.BrowserNodeDefinition.Label = name Then
      Set GetOccurrenceNode = node
      Exit Function
    End If
        
    Set GetOccurrenceNode = _
      GetOccurrenceNode(name, node.BrowserNodes)
    If Not GetOccurrenceNode Is Nothing Then
      Exit Function
    End If
  Next
End Function

Function GetNode( _
obj As Object, nodes As BrowserNodesEnumerator) As BrowserNode
  Dim node As BrowserNode
  For Each node In nodes
    If node.NativeObject Is obj Then
      Set GetNode = node
      Exit Function
    End If
        
    Set GetNode = GetNode(obj, node.BrowserNodes)
    If Not GetNode Is Nothing Then
      Exit Function
    End If
  Next
End Function

Sub HideSubComponent()
  Dim dwg As DrawingDocument
  Set dwg = ThisApplication.ActiveDocument
    
  Dim dv As DrawingView
  &#39; Get &quot;B:MainAsm.iam&quot; Detail View
  Set dv = dwg.ActiveSheet.DrawingViews(2)
    
  Dim bp As BrowserPane
  Set bp = dwg.BrowserPanes(&quot;DlHierarchy&quot;)
        
  Dim asm As AssemblyDocument
  Set asm = dv.ReferencedDocumentDescriptor.ReferencedDocument
    
  Dim occ As ComponentOccurrence
  Set occ = asm.ComponentDefinition.Occurrences(1)
    
  &#39; This throws an error in Inventor 2012,
  &#39; that&#39;s the reason for the below workaround
  &#39;Call dv.SetVisibility(occ, False)
    
  &#39; If it&#39;s hidden already then we&#39;re done
  If Not dv.GetVisibility(occ) Then Exit Sub
    
  &#39; We have to find the BrowserNode of the
  &#39; occurrence under the DrawingView
  Dim node As BrowserNode
  &#39; This does not work well for dv.ParentView,
  &#39; and does not work at all for dv:
  &#39;Set node = bp.GetBrowserNodeFromObject(dv.parent)
  &#39; So we use this instead
  Set node = GetNode(dv, bp.TopNode.BrowserNodes)
    
  &#39; Then select the occurrence under the view node
  Set node = GetOccurrenceNode(occ.name, node.BrowserNodes)
  Call node.DoSelect
    
  &#39; Then run the command on it
  Dim cm As CommandManager
  Set cm = ThisApplication.CommandManager
    
  Dim cd As ControlDefinition
  Set cd = cm.ControlDefinitions(&quot;DrawingEntityVisibilityCtxCmd&quot;)
    
  Call cd.Execute
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07615a9970c-pi" style="display: inline;"><img alt="Hide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d07615a9970c image-full img-responsive" src="/assets/image_afe096.jpg" title="Hide" /></a></p>
<p>&#0160;</p>
