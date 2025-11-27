---
layout: "post"
title: "Cable & Harness component names"
date: "2016-07-18 05:41:50"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/cable-harness-component-names.html "
typepad_basename: "cable-harness-component-names"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When iterating through the components of a&#0160;<strong>Cable &amp; Harness</strong> assembly you&#39;ll find <strong>VirtualComponentDefinition</strong>&#39;s named like &quot;HA_VC_xxx&quot;:</p>
<pre>...
Hard Drive:2
CD Ribbon Cable
  CD Ribbon Cable
  Ribbon Cable Connector:1
  Ribbon Cable Connector:2
  Ribbon Cable Connector:3
  <strong>HA_VC_100:1</strong>
Power Supply Harness
  Power Supply Connector:1
...</pre>
<p>This does not line up with what you see in the <strong>Browser Tree</strong>. It&#39;s because what&#39;s listed there are not actually the components but just placeholders. If you select them and run a code like this then you can see that those parts are just <strong>ClientBrowserNodeDefinitionObject</strong>&#39;s:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2068263970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Selectset" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2068263970c img-responsive" src="/assets/image_07c020.jpg" title="Selectset" /></a></p>
<p>The <strong>Part Number</strong> of the <strong>VirtualComponentDefinition</strong>&#39;s resemble more how those components are represented in the <strong>Browser Tree</strong>, so you could use that instead:</p>
<pre>Sub PrintHierarchyTreeRecursive(occs, indent)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    Dim pss As PropertySets
    If TypeOf occ.Definition Is VirtualComponentDefinition Then
      Set pss = occ.Definition.PropertySets
    Else
      Set pss = occ.Definition.Document.PropertySets
    End If
    
    Dim partNumber As String
    partNumber = pss(&quot;Design Tracking Properties&quot;)(&quot;Part Number&quot;).value
    
    Write #1, Spc(indent); occ.Name + &quot;, &quot; + partNumber
    
    Call PrintHierarchyTreeRecursive(occ.SubOccurrences, indent + 2)
  Next
End Sub

Sub PrintHierarchyTree()
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
  
  Open &quot;C:\temp\assemblyparts.txt&quot; For Output As #1
  Call PrintHierarchyTreeRecursive(asm.ComponentDefinition.Occurrences, 0)
  Close #1
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c87cac0d970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PartNumber" class="asset  asset-image at-xid-6a0167607c2431970b01b7c87cac0d970b img-responsive" src="/assets/image_0ba34f.jpg" title="PartNumber" /></a></p>
<p>&#0160;</p>
