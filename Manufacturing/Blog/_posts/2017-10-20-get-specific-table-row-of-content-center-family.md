---
layout: "post"
title: "Get Specific Table Row of Content Center Family"
date: "2017-10-20 21:38:01"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/10/get-specific-table-row-of-content-center-family.html "
typepad_basename: "get-specific-table-row-of-content-center-family"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In general, we call ContentFamily.CreateMember to create the instance of a content family row by inputting index of this row. However, sometimes, we would need to pick one row which has specific cell value such as diameter, radius, length etc. I do not find a direct way to get specific table row. while we could still iterate the table rows and find out the specific item by the cell value(s).</p>
<p>In the <a href="http://adndevblog.typepad.com/manufacturing/2013/06/insert-content-center-part.html">other blog</a>, my colleague Adam shared a code demo on how to get specific family. I produced another code based on that. The code tries to get the table row with one column: &#39;radius is 0.25&quot;.&#0160;</p>
<p>Note: the method ContentTableRow.GetCellValue accepts the column index, or its internal name. e.g. in this case, the internal name of column &#39;radius&#39; is &#39;RR&#39;. In addition,&#0160; in this demo code, I input one column only, however obviously, in family &#39;Bolt GB/T 35&#39;, there are many rows whose radius is also &#39;0.25&#39;.&#0160; So&#0160;you would need to input other columns and cell values, in order to get the unique row.</p>
<p>One more thing: if you have known the member id of a row, you could also get ContentTableRow by a direct method:ContentCenter.GetContentObject. In t<a href="http://modthemachine.typepad.com/my_weblog/2012/02/getting-data-from-content-center.html">he blog</a>, my colleague Wayne introduced it.&#0160;</p>
<p>&#0160;</p>
<pre><code>
Public Function GetFamily( _
name As String, node As ContentTreeViewNode) _
As ContentFamily
  Dim cc As ContentCenter
  Set cc = ThisApplication.ContentCenter
    
  If node Is Nothing Then Set node = cc.TreeViewTopNode
  
  Dim cf As ContentFamily
  For Each cf In node.Families
     
    
    If cf.DisplayName = name Then
      Set GetFamily = cf
      Exit Function
    End If
  Next
  
  Dim child As ContentTreeViewNode
  For Each child In node.ChildNodes
    Set cf = GetFamily(name, child)
    If Not cf Is Nothing Then
      Set GetFamily = cf
      Exit Function
    End If
  Next
End Function

&#39;input: specific content family
&#39;       internal name of specific ContentTableColumn (ContentTableColumn.internalName)
&#39;       the cell value of this column

&#39;Note: you would need to input other columns and cell values, in order to get the unique row.
Public Function GetSpecificRow(cf As ContentFamily, colInternalName As String, cellValue As Variant)

    Dim oRow As ContentTableRow
    Dim oCol As ContentTableColumn
    
    Dim oFound As Boolean
    oFound = False
    For Each oRow In cf.TableRows

        &#39;check the cell value of the specific column
        Dim oCellV As String
        oCellV = oRow.GetCellValue(colInternalName)
        If oCellV = cellValue Then
            oFound = True
            Exit For
        End If
    Next

    If oFound Then
      Set GetSpecificRow = oRow
    Else
      Set GetSpecificRow = Nothing
    End If
    
    
End Function

Public Sub CcTest()
   Dim asm As AssemblyDocument
   Set asm = ThisApplication.ActiveDocument
    
   &#39;get specific family
   Dim cf As ContentFamily
   Set cf = GetFamily(&quot;Bolt GB/T 35&quot;, Nothing)
   
   &#39;get specific table row
   Dim oOneTableRow As ContentTableRow
   Set oOneTableRow = GetSpecificRow(cf, &quot;RR&quot;, &quot;0.25&quot;)
   
   &#39;create this member
   Dim member As String
   Dim ee As MemberManagerErrorsEnum
   member = cf.CreateMember(oOneTableRow, ee, &quot;Problem&quot;)
   
   Dim tg As TransientGeometry
   Set tg = ThisApplication.TransientGeometry
   
   &#39;inser the member to the assembly
   Call asm.ComponentDefinition.Occurrences.Add( _
    member, tg.CreateMatrix())
End Sub


</code></pre>
