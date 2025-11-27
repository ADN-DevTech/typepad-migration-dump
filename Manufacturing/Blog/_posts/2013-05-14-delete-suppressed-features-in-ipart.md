---
layout: "post"
title: "Delete suppressed features in iPart"
date: "2013-05-14 09:51:43"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/delete-suppressed-features-in-ipart.html "
typepad_basename: "delete-suppressed-features-in-ipart"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you ended up with features in your iPart factory which are suppressed in each iPart member then you could delete those like this:&#0160;</p>
<pre>Sub DeleteSuppressedFeatures()
    &#39; Open the iPart Factory document before
    &#39; running this
    Dim doc As PartDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim fact As iPartFactory
    Set fact = doc.ComponentDefinition.iPartFactory
    
    Dim col As iPartTableColumn
    For Each col In fact.TableColumns
        &#39; If the column is about suppression
        If col.ReferencedDataType = kExclusionColumn Then
            Dim del As Boolean: del = True
            Dim cell As iPartTableCell
            For Each cell In col
                &#39; If it&#39;s not suppressed in each row
                &#39; then we won&#39;t delete it
                If cell.value &lt;&gt; &quot;Suppress&quot; Then
                    del = False
                    Exit For
                End If
            Next
            
            If del Then
                col.ReferencedObject.Delete
            End If
        End If
    Next
End Sub</pre>
