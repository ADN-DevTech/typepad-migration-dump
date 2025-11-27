---
layout: "post"
title: "Get ItemNumber from a DrawingCurve"
date: "2013-11-22 11:03:23"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/get-itemnumber-from-the-drawingcurve.html "
typepad_basename: "get-itemnumber-from-the-drawingcurve"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to find out which <strong>DrawingBOMRow</strong> / <strong>BOMRow</strong> a <strong>DrawingCurve</strong> belongs to in order to get e.g. the <strong>ItemNumber</strong> associated with the component that the <strong>DrawingCurve</strong> represents, the most straightforward way might be to let the <strong>Balloon</strong> object figure that out for us.&#0160;</p>
<p>We can temporarily create a <strong>Balloon</strong> that would reference that <strong>DrawingCurve</strong>. If it&#39;s done inside a <strong>Transaction</strong> that is <strong>Aborted</strong>, then it will look as if the <strong>Balloon</strong> has never been created at all.</p>
<p>Let&#39;s say we have a <strong>SketchedSymbol</strong> whose first <strong>TextBox</strong> is a <strong>prompted entry</strong> we want to set and an instance of it is already attached to the <strong>DrawingCurve</strong>, then we could update it using this code:</p>
<pre>Public Sub SetSketchedSymbolNumber()
  Dim oDwg As DrawingDocument
  Set oDwg = ThisApplication.ActiveDocument
  
  &#39; The SketchedSymbol needs to be selected
  Dim oSymbol As SketchedSymbol
  Set oSymbol = oDwg.SelectSet(1)
  
  &#39; The symbol&#39;s leader needs to be
  &#39; attached to the Component
  Dim oNode As LeaderNode
  Set oNode = oSymbol.Leader.AllLeafNodes(1)
  
  Dim oCurve As DrawingCurve
  Set oCurve = oNode.AttachedEntity.Geometry
  
  Dim oSheet As Sheet
  Set oSheet = oDwg.ActiveSheet
  
  Dim oTO As TransientObjects
  Set oTO = ThisApplication.TransientObjects
  
  Dim oTG As TransientGeometry
  Set oTG = ThisApplication.TransientGeometry
  
  Dim oLeaderPoints As ObjectCollection
  Set oLeaderPoints = oTO.CreateObjectCollection
  &#39; Does not matter what we add as first point
  Call oLeaderPoints.Add(oTG.CreatePoint2d(0, 0))

  &#39; Get item number from the temporary balloon
  Dim itemNumber As String
  
  Dim oTM As TransactionManager
  Set oTM = ThisApplication.TransactionManager
  
  Dim oTransaction As Transaction
  Set oTransaction = oTM.StartTransaction(oDwg, &quot;TempTransaction&quot;)

  Dim oGeometryIntent As GeometryIntent
  Set oGeometryIntent = oSheet.CreateGeometryIntent(oCurve)
  Call oLeaderPoints.Add(oGeometryIntent)
  
  Dim oBalloon As Balloon
  Set oBalloon = oSheet.Balloons.Add(oLeaderPoints, , kStructured)
  
  &#39; We could also get the DrawingBOMRow and BOMRow
  &#39; oBalloon.BalloonValueSets(1).ReferencedRow.BOMRow
  &#39; but this time we just need the ItemNumber
  itemNumber = oBalloon.BalloonValueSets(1).itemNumber
  
  &#39; This transaction will not show up in the Undo/Redo stack
  &#39; and it will look as if the above balloon never existed
  Call oTransaction.Abort
  
  &#39; Update the first textbox in the sketched symbol
  &#39; with the item number
  Dim oBox As TextBox
  Set oBox = oSymbol.Definition.sketch.TextBoxes(1)
              
  Call oSymbol.SetPromptResultText( _
    oBox, itemNumber)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b017d908e970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchedSymbol" class="asset  asset-image at-xid-6a0167607c2431970b019b017d908e970d" src="/assets/image_b795ca.jpg" style="width: 450px;" title="SketchedSymbol" /></a></p>
<p>There is a more comprehensive sample on creating a <strong>Balloon</strong> in the <strong>API Help file</strong> &quot;C:\Program Files\Autodesk\Inventor 2014\Local Help\admapi_18_0.chm&quot;&#0160;</p>
