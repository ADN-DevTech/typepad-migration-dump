---
layout: "post"
title: "Use Excel API to export PartsList content"
date: "2016-02-08 12:01:07"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/use-excel-api-to-export-partslist-content.html "
typepad_basename: "use-excel-api-to-export-partslist-content"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In case <strong>PartsList</strong>.<strong>Export</strong>() does not do exactly what you need, you can take full control of what gets exported and how by using the<strong> Excel API</strong> directly.&#0160;</p>
<p>The following sample exports the content of the first <strong>PartsList</strong> of the active sheet.&#0160;</p>
<p><strong>VBA</strong></p>
<pre>Sub ExportPartsListContent()
  Dim oExcel As Object
  Set oExcel = CreateObject(&quot;Excel.Application&quot;)
  
  &#39; For debugging
  &#39;oExcel.Visible = True
    
  Dim oWB As Object
  Set oWB = oExcel.Workbooks.Open(&quot;C:\temp\test.xlsm&quot;)
  
  Dim oWS As Object
  Set oWS = oWB.ActiveSheet
  
  Dim oDoc As DrawingDocument
  Set oDoc = ThisApplication.ActiveDocument
  
  Dim oSheet As Sheet
  Set oSheet = oDoc.ActiveSheet
  
  &#39; Export the first PartsList
  Dim oPL As PartsList
  Set oPL = oSheet.PartsLists(1)
  
  &#39; Starting cell position on the Excel sheet
  Dim iRowStart As Integer: iRowStart = 1
  Dim iColStart As Integer: iColStart = 1
  
  &#39; Export headers
  Dim iRow As Integer: iRow = iRowStart
  Dim iCol As Integer: iCol = iColStart
  Dim oCol As PartsListColumn
  For Each oCol In oPL.PartsListColumns
    oWS.Cells(iRow, iCol).Value = oCol.Title
    iCol = iCol + 1
  Next
  iRow = iRow + 1
  
  &#39; Export content
  Dim oRow As PartsListRow
  For Each oRow In oPL.PartsListRows
    iCol = iColStart
    Dim oCell As PartsListCell
    For Each oCell In oRow
      oWS.Cells(iRow, iCol).Value = oCell.Value
      iCol = iCol + 1
    Next
    iRow = iRow + 1
  Next
  
  &#39; Save it
  &#39; We disable the confirmation dialog
  &#39; in case the file already exists and
  &#39; needs to be overwritten
  oExcel.DisplayAlerts = False
  Call oWB.SaveAs(&quot;C:\temp\test2.xlsm&quot;)
  
  &#39; Close excel
  Call oExcel.Quit
End Sub</pre>
<p><strong>iLogic</strong></p>
<pre>AddReference &quot;Microsoft.Office.Interop.Excel&quot;
Dim oExcel As New Microsoft.Office.Interop.Excel.Application

&#39; For debugging
&#39;oExcel.Visible = True

Dim oWB As Object
oWB = oExcel.Workbooks.Open(&quot;C:\temp\test.xlsm&quot;)

Dim oWS As Object
oWS = oWB.ActiveSheet

Dim oDoc As DrawingDocument
oDoc = ThisApplication.ActiveDocument

Dim oSheet As Sheet
oSheet = oDoc.ActiveSheet

&#39; Export the first PartsList
Dim oPL As PartsList
oPL = oSheet.PartsLists(1)

&#39; Starting cell position on the Excel sheet
Dim iRowStart As Integer: iRowStart = 1
Dim iColStart As Integer: iColStart = 1

&#39; Export headers
Dim iRow As Integer: iRow = iRowStart
Dim iCol As Integer: iCol = iColStart
Dim oCol As PartsListColumn
For Each oCol In oPL.PartsListColumns
  oWS.Cells(iRow, iCol).Value = oCol.Title
  iCol = iCol + 1
Next
iRow = iRow + 1

&#39; Export content
Dim oRow As PartsListRow
For Each oRow In oPL.PartsListRows
  iCol = iColStart
  Dim oCell As PartsListCell
  For Each oCell In oRow
    oWS.Cells(iRow, iCol).Value = oCell.Value
    iCol = iCol + 1
  Next
  iRow = iRow + 1
Next

&#39; Save it
&#39; We disable the confirmation dialog
&#39; in case the file already exists and
&#39; needs to be overwritten
oExcel.DisplayAlerts = False
Call oWB.SaveAs(&quot;C:\temp\test2.xlsm&quot;)

&#39; Close excel
Call oExcel.Quit</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19cfcec970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PartsListExcel" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19cfcec970c img-responsive" src="/assets/image_073f8f.jpg" title="PartsListExcel" /></a></p>
<p>&#0160;</p>
