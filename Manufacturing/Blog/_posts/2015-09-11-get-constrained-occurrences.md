---
layout: "post"
title: "Get constrained occurrences"
date: "2015-09-11 10:23:57"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/get-constrained-occurrences.html "
typepad_basename: "get-constrained-occurrences"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you select a component occurrence in the <strong>UI</strong> the context menu provides you with the <strong>Constrained To</strong> option to select all constrained occurrences:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d156319d970c-pi" style="display: inline;"><img alt="Constraints1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d156319d970c image-full img-responsive" src="/assets/image_a06e8a.jpg" title="Constraints1" /></a></p>
<p>If you want to retrieve the same occurrences programmatically, then you can iterate through the constraints of the selected occurrence and store all the occurrences those constraints reference:</p>
<pre>Sub ConstrainedTo()
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument

  Dim occSel As ComponentOccurrence
  Set occSel = asm.SelectSet(1)
  
  Dim tr As TransientObjects
  Set tr = ThisApplication.TransientObjects
  
  Dim coll As ObjectCollection
  Set coll = tr.CreateObjectCollection()
  
  Dim ac As AssemblyConstraint
  For Each ac In occSel.Constraints
    coll.Add ac.OccurrenceOne
    coll.Add ac.OccurrenceTwo
  Next
  
  &#39; If you want to ignore the originally
  &#39; selected occurrence then you can remove it
  &#39;coll.RemoveByObject occSel
  
  &#39; Now we have all the occurrences
  &#39; Let&#39;s list their names
  Dim text As String
  Dim occ As ComponentOccurrence
  For Each occ In coll
    text = text + occ.Name + vbCrLf
  Next
  
  MsgBox text
End Sub</pre>
<p>The result:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7cc440d970b-pi" style="display: inline;"><img alt="Constraints2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7cc440d970b img-responsive" src="/assets/image_40addc.jpg" title="Constraints2" /></a></p>
<p>&#0160;</p>
