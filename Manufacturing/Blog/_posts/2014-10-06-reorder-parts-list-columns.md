---
layout: "post"
title: "Reorder parts list columns"
date: "2014-10-06 12:00:32"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/reorder-parts-list-columns.html "
typepad_basename: "reorder-parts-list-columns"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to change the order of the columns of the <strong>PartsList</strong> object then you can use the <strong>PartsListColumn</strong>.<strong>Reposition</strong> function for that. Here is a <strong>VBA</strong> sample that reverses the order of the parts list columns:</p>
<pre>Sub ReorderPartsList()
  &#39; The parts list whose columns you want to reorder 
  &#39; needs to be selected in the User Interface
  Dim doc As DrawingDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim pl As PartsList
  Set pl = doc.SelectSet(1)
  
  Dim plcs As PartsListColumns
  Set plcs = pl.PartsListColumns
  
  &#39; We&#39;ll simply reverse the order of the columns
  Dim i As Integer
  For i = 1 To plcs.count - 1
    Dim plc As PartsListColumn
    Set plc = plcs(1)
    
    Call plc.Reposition(plcs.count - i + 1, False)
  Next
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6ee680e970b-pi" style="display: inline;"><img alt="Reorder" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6ee680e970b image-full img-responsive" src="/assets/image_52bafe.jpg" title="Reorder" /></a></p>
<p>&#0160;</p>
