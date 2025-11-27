---
layout: "post"
title: "Save RevisionTable content to CustomTable"
date: "2014-03-06 13:51:35"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/03/save-revisiontable-content-to-customtable.html "
typepad_basename: "save-revisiontable-content-to-customtable"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may want to save the current values of the revision table before doing some modifications to the revision numbers - e.g. as a result of a Vault merger.</p>
<p>Here is a VBA sample showing how to do that:</p>
<pre style="line-height: 120%">Sub RevisionTableToCustomTable()
  Dim tg As TransientGeometry
  Set tg = ThisApplication.TransientGeometry

  Dim dd As DrawingDocument
  Set dd = ThisApplication.ActiveDocument
  
  Dim s As Sheet
  Set s = dd.ActiveSheet
  
  ' Get revision table
  Dim rt As RevisionTable
  Set rt = s.RevisionTables(1)
  
  ' Get dimensions
  Dim c As Integer
  Dim r As Integer
  c = rt.RevisionTableColumns.count
  r = rt.RevisionTableRows.count
  
  ' Counter
  Dim i As Integer, j As Integer
    
  ' Get headers and column widths
  ReDim headers(1 To c) As String
  ReDim widths(1 To c) As Double
  Dim rtc As RevisionTableColumn
  i = 1
  For Each rtc In rt.RevisionTableColumns
    headers(i) = rtc.Title
    widths(i) = rtc.Width
    i = i + 1
  Next
  
  ' Get contents and row heights
  ReDim contents(1 To c * r) As String
  ReDim heights(1 To r) As Double
  Dim rtr As RevisionTableRow
  i = 1: j = 1
  For Each rtr In rt.RevisionTableRows
    Dim rtcell As RevisionTableCell
    For Each rtcell In rtr
      contents(i) = rtcell.Text
      i = i + 1
    Next
    heights(j) = rtr.Height
    j = j + 1
  Next
  
  ' Create custom table with the content
  Dim ct As CustomTable
  Set ct = s.CustomTables.Add( _
    rt.Title + " (old)", _
    tg.CreatePoint2d(), _
    c, _
    r, _
    headers, _
    contents, _
    widths, _
    heights)
  
  ' Position it e.g. on top of the revision table
  Dim pt As Point2d
  Set pt = rt.Position
  Call pt.TranslateBy(tg.CreateVector2d( _
    0, Abs(ct.RangeBox.MaxPoint.Y - ct.RangeBox.MinPoint.Y)))
  ct.Position = pt
End Sub</pre>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d894ff5970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73d894ff5970d img-responsive" style="width: 450px;" title="Revisiontable" src="/assets/image_21defd.jpg" alt="Revisiontable" /></a></p>
<p>You could also do it with fewer calls by using <strong>Export()</strong> on the <strong>RevisionTable</strong> to save contents to a <strong>CSV</strong> file and then create a <strong>CustomTable</strong> using&nbsp;<strong>AddCSVTable()</strong>, but in that case the table will be bound to the <strong>CSV</strong> file and that needs to exist along with the drawing file.</p>
