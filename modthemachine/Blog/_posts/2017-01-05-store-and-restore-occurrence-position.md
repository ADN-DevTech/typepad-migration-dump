---
layout: "post"
title: "Store and restore occurrence position"
date: "2017-01-05 14:11:39"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Attributes"
  - "Inventor"
  - "Visual Basic for Applications (VBA)"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/01/store-and-restore-occurrence-position.html "
typepad_basename: "store-and-restore-occurrence-position"
typepad_status: "Publish"
---

<p>If you want to store the position of a&#0160;<strong>ComponentOccurrence</strong> in order to be able to restore it later on, then you could simply store the <strong>Transformation</strong> values of the <strong>occurrence</strong>. They could be saved in a separate file, in <strong>Attributes</strong>, some other place - up to you where.</p>
<p>This <strong>VBA</strong> sample will store the values in an <strong>AttributeSet</strong>:</p>
<pre>Sub StorePosition()
  Const kAttSetName = &quot;Adam.OccurrencePosition&quot;
  Const kCellNamePrefix = &quot;cell&quot;
  
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
  
  Dim occ As ComponentOccurrence
  Set occ = asm.SelectSet(1)
  
  Dim cells() As Double
  Call occ.Transformation.GetMatrixData(cells)
  
  Dim attValues(15) As Variant
  Dim attNames(15) As String
  Dim attTypes(15) As ValueTypeEnum
  
  Dim i As Integer
  For i = LBound(cells) To UBound(cells)
    attValues(i) = cells(i)
    attNames(i) = kCellNamePrefix + Trim(str(i))
    attTypes(i) = kDoubleType
  Next
  
  Dim attSet As AttributeSet
  If occ.AttributeSets.NameIsUsed(kAttSetName) Then
    Set attSet = occ.AttributeSets(&quot;Adam.OccurrencePosition&quot;)
  Else
    Set attSet = occ.AttributeSets.Add(&quot;Adam.OccurrencePosition&quot;)
  End If
  
  Dim attEnum As AttributesEnumerator
  &#39; If the name was not Trim()-ed this would give an error
  Set attEnum = attSet.AddAttributes(attNames, attTypes, attValues, True)
End Sub

Sub RestorePosition()
  Const kAttSetName = &quot;Adam.OccurrencePosition&quot;
  Const kCellNamePrefix = &quot;cell&quot;
  
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
  
  Dim occ As ComponentOccurrence
  Set occ = asm.SelectSet(1)
  
  Dim attSet As AttributeSet
  If occ.AttributeSets.NameIsUsed(kAttSetName) Then
    Set attSet = occ.AttributeSets(&quot;Adam.OccurrencePosition&quot;)
  Else
    Call MsgBox(&quot;Position data was not stored for this occurrence!&quot;)
    Exit Sub
  End If
  
  Dim cells(15) As Double
  Dim i As Integer
  For i = 0 To 15
    Dim cellName As String
    cellName = kCellNamePrefix + Trim(str(i))
    If Not attSet.NameIsUsed(cellName) Then
      Call MsgBox(&quot;Not all position data stored for this occurrence!&quot;)
      Exit Sub
    End If

    cells(i) = attSet(cellName).value
  Next
  
  Dim mx As Matrix
  Set mx = occ.Transformation
  
  Call mx.PutMatrixData(cells)
  
  occ.Transformation = mx
End Sub</pre>
<p>The code in action:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c8c423eb970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="StorePosition1" class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c8c423eb970b img-responsive" src="/assets/image_916477.jpg" title="StorePosition1" /></a></p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d24df568970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="StorePosition2" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d24df568970c img-responsive" src="/assets/image_536262.jpg" title="StorePosition2" /></a></p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d24df575970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="StorePosition3" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d24df575970c img-responsive" src="/assets/image_54536.jpg" title="StorePosition3" /></a></p>
<p>-Adam</p>
