---
layout: "post"
title: "Run command on browser item"
date: "2014-04-18 04:03:43"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/04/run-command-on-browser-item.html "
typepad_basename: "run-command-on-browser-item"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Some functionality of an object or sometimes the whole object may not be exposed directly through the API. In this case one workaround could be selecting the object and then running commands on it. This is the combination of <a href="http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html" target="_self">running a commmand</a> (this blog post also shows how to get all the command names)&#0160;and <a href="http://adndevblog.typepad.com/manufacturing/2013/01/get-items-selected-in-the-browser-pane-.html" target="_self">finding an item in the browser</a>.&#0160;</p>
<p>E.g. the <strong>WeldBead</strong> object does not have a <strong>Suppress</strong> function, and the <strong>WeldmentComponentDefinition.SuppressFeatures</strong> function does not seem to work on it either. It&#39;s also not hooked up to the selection system, i.e. when a bead is selected in the <strong>UI</strong> then <strong>ThisApplication.ActiveDocument.SelectSet(1)</strong> will return <strong>Nothing</strong>.</p>
<p>So the workaround is to select the object through the <strong>BrowserPane</strong> object and then run the <strong>AssemblySuppressFeatureCtxCmd</strong> command. In order to avoid this message, we&#39;ll use <strong>SilentOperation = True</strong>.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511a2683d970c-pi" style="display: inline;"><img alt="Warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511a2683d970c img-responsive" src="/assets/image_a31ad9.jpg" title="Warning" /></a></p>
<pre>Enum SuppressOption
  kSuppress
  kUnsuppress
  kToggle
End Enum

Sub SuppressBead( _
wcd As WeldmentComponentDefinition, _
wbn As BrowserNode, _
so As SuppressOption)
  Dim name As String
  name = wbn.BrowserNodeDefinition.Label

  &#39; We only need to check the current state of the bead if
  &#39; we are not just toggling its state but want a specific one
  If so &lt;&gt; kToggle Then
    &#39; If the bead is already suppressed then its
    &#39; BeadFaces.Count will be 0
    Dim bfs As Faces
    Set bfs = wcd.Welds.WeldBeads(name).BeadFaces
    If bfs.Count = 0 Xor so = kUnsuppress Then Exit Sub
  End If
  
  &#39; Get the CommandManager object
  Dim cm As CommandManager
  Set cm = ThisApplication.CommandManager
  
  &#39; Get the collection of control definitions
  Dim cds As ControlDefinitions
  Set cds = cm.ControlDefinitions
      
  &#39; Run the &quot;Suppress&quot; command
  ThisApplication.SilentOperation = True
  Call wbn.DoSelect
  Call cds(&quot;AssemblySuppressFeatureCtxCmd&quot;).Execute2(True)
  ThisApplication.SilentOperation = False
End Sub

Sub SuppressWeldBead()
  Dim weldName As String
  weldName = &quot;Fillet Weld 1&quot;

  &#39; Get document
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim wcd As WeldmentComponentDefinition
  Set wcd = doc.ComponentDefinition
  
  &#39; Get &quot;Model&quot; browser
  Dim bp As BrowserPane
  Set bp = doc.BrowserPanes(&quot;AmBrowserArrangement&quot;)
  
  &#39; Get &quot;Welds&quot; node
  Dim wbn As BrowserNode
  For Each wbn In bp.TopNode.BrowserNodes
    If wbn.BrowserNodeDefinition.Label = &quot;Welds&quot; Then
      Exit For
    End If
  Next
  
  &#39; Get &quot;Beads&quot; node
  Dim bbn As BrowserNode
  For Each bbn In wbn.BrowserNodes
    If bbn.BrowserNodeDefinition.Label = &quot;Beads&quot; Then
      Exit For
    End If
  Next
  
  &#39; Get the Beads we want to suppress
  For Each wbn In bbn.BrowserNodes
    If wbn.BrowserNodeDefinition.Label = weldName Then
      Call SuppressBead(wcd, wbn, kSuppress)
    End If
  Next
End Sub</pre>
<p>Here is the result of the program:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcf2bf7a970b-pi" style="display: inline;"><img alt="SuppressWeld" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcf2bf7a970b img-responsive" src="/assets/image_524402.jpg" title="SuppressWeld" /></a></p>
<p>&#0160;</p>
