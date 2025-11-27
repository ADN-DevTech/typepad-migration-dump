---
layout: "post"
title: "Delete suppressed components"
date: "2014-02-11 05:24:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/02/delete-suppressed-components.html "
typepad_basename: "delete-suppressed-components"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may want to make the current LOD the master LOD in the assembly in which case you can delete the suppressed components in the assembly. Once I run the below VBA code the <strong>Master LOD</strong> of <strong>MainAsm</strong>&#0160;document will not contain the components suppressed in <strong>LevelOfDetail1 LOD </strong>so basically the content of<strong> Master LOD </strong>and<strong> LevelOfDetail1 LOD </strong>will be the same.&#0160;</p>
<pre>Sub DeleteSuppressedComponent(occs As ComponentOccurrences)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    If occ.Suppressed Then
      occ.Delete
    Else
      Call DeleteSuppressedComponent(occ.SubOccurrences)
    End If
  Next
End Sub

Sub DeleteSuppressedComponents()
    Dim doc As AssemblyDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim acd As AssemblyComponentDefinition
    Set acd = doc.ComponentDefinition
    
    Call DeleteSuppressedComponent(acd.Occurrences)
End Sub</pre>
<p>This is the result we&#39;d get in case of the below assembly structure:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d752421970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SuppressedComponents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d752421970d image-full img-responsive" src="/assets/image_d3f2f1.jpg" title="SuppressedComponents" /></a></p>
<p>&#0160;</p>
