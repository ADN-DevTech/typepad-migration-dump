---
layout: "post"
title: "Set context menu for OrdinateDimensionSet member"
date: "2015-04-08 09:47:36"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/04/set-context-menu-for-ordinatedimensionset-member.html "
typepad_basename: "set-context-menu-for-ordinatedimensionset-member"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you right-click on an <strong>OrdinateDimensionSet</strong> then the context menu pops up offering among other things the <strong>Edit</strong> menu item. This will then edit the specific member that was closest to the cursor when the ordinate dimension set got pre-highlighted:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://a6.typepad.com/6a0112791b8fe628a401b7c774ffa6970b-pi"><img class="asset  asset-image at-xid-6a0112791b8fe628a401b7c774ffa6970b img-responsive" title="OrdinateDimensionSet" src="/assets/image_b46851.jpg" alt="OrdinateDimensionSet" /></a></p>
<p>Which member was closest when the selection took place does not matter, because if we select the dimension set by clicking the first member, but then right-click on the second member, still the second member will be edited.</p>
<p>Based on this we could create our own context menu item which then will edit the member that we right-clicked on. We just need to listen to two events of the <strong>UserInputEvents</strong> object:<br />- <strong>OnPreSelect</strong>: here we'll find the member closest to the cursor and store the reference to that so that we can modify it later<br />- <strong>OnLinearMarkingMenu</strong>: here we'll add our own context menu item that can be used to edit the <strong>OrdinateDimensionSet</strong> member&nbsp;</p>
<p>The below <strong>VBA</strong> code works for both&nbsp;<strong>OrdinateDimension</strong> and <strong>OrdinateDimensionSet</strong> member.</p>
<p>We need a <strong>class</strong> that can handle events - I named it <strong>clsLinearMarkingMenu</strong>:</p>
<pre>Dim WithEvents uiEvents As UserInputEvents
Dim WithEvents btnDef As ButtonDefinition

' Selected / pre-highlighted dimension
Dim selDim As OrdinateDimension

Private Sub btnDef_OnExecute(ByVal Context As NameValueMap)
  ' We're just adding a plus sign to the selected
  ' dimension's text
  selDim.Text.FormattedText = selDim.Text.FormattedText + "+"
End Sub

Private Sub CreateCommand(dispText As String)
  Dim cmdMgr As CommandManager
  Set cmdMgr = ThisApplication.CommandManager

  ' Make sure our command is deleted
  ' so that we can create it new
  On Error Resume Next
  Set btnDef = cmdMgr.ControlDefinitions("DimTextPlus")
  If Not btnDef Is Nothing Then btnDef.Delete
  On Error GoTo 0
    
  Set btnDef = cmdMgr.ControlDefinitions.AddButtonDefinition( _
    dispText, "DimTextPlus", kNonShapeEditCmdType)
End Sub

Private Sub Class_Initialize()
  Dim cmdMgr As CommandManager
  Set cmdMgr = ThisApplication.CommandManager
  
  Set uiEvents = cmdMgr.UserInputEvents
End Sub

Private Sub uiEvents_OnLinearMarkingMenu( _
ByVal SelectedEntities As ObjectsEnumerator, _
ByVal SelectionDevice As SelectionDeviceEnum, _
ByVal LinearMenu As CommandControls, _
ByVal AdditionalInfo As NameValueMap)
  ' We only add our context menu item if the
  ' selected entity was an ordinate dimension
  If Not selDim Is Nothing Then
    ' We can also set the text of the menu
    ' item based on the selected dimension
    ' That's what our CreateCommand does
    Call CreateCommand(selDim.Text.Text)
    
    Call LinearMenu.AddButton(btnDef)
  End If
End Sub

Function GetClosestDimensionInSet( _
ByVal ordDimSet As OrdinateDimensionSet, _
ByVal pt As Point2d) As OrdinateDimension
  Dim tr As TransientGeometry
  Set tr = ThisApplication.TransientGeometry

  Dim ordDim As OrdinateDimension
  Dim min As Double
  min = 1000000
  
  For Each ordDim In ordDimSet.Members
    Dim poly As Polyline2d
    Set poly = ordDim.DimensionLine

    Dim i As Integer
    For i = 1 To poly.PointCount - 1
      Dim line As LineSegment2d
      Set line = tr.CreateLineSegment2d( _
        poly.PointAtIndex(i), poly.PointAtIndex(i + 1))

      Dim d As Double
      d = line.DistanceTo(pt)
      If d &lt; min Then
        min = d
        Set GetClosestDimensionInSet = ordDim
      End If
    Next
  Next
End Function

Private Sub uiEvents_OnPreSelect( _
PreSelectEntity As Object, _
DoHighlight As Boolean, _
MorePreSelectEntities As ObjectCollection, _
ByVal SelectionDevice As SelectionDeviceEnum, _
ByVal ModelPosition As Point, _
ByVal ViewPosition As Point2d, _
ByVal View As View)
  ' Initialize our reference
  Set selDim = Nothing
  
  If TypeOf PreSelectEntity Is OrdinateDimension Then
    If PreSelectEntity.OrdinateDimensionSet Is Nothing Then
      ' It's not a dimension set, but just a single dimension
      Set selDim = PreSelectEntity
    Else
      Set selDim = GetClosestDimensionInSet( _
        PreSelectEntity.OrdinateDimensionSet, _
        ViewPosition)
    End If

    Debug.Print selDim.Text.Text
  Else
    Debug.Print TypeName(PreSelectEntity)
  End If
End Sub</pre>
<p>Then we can instantiate this <strong>class</strong> from a <strong>module</strong>:</p>
<pre>Dim lmm As clsLinearMarkingMenu

Sub TestLinearMarkingMenu()
  Set lmm = New clsLinearMarkingMenu
End Sub</pre>
<p>Here it is in action:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://a2.typepad.com/6a0112791b8fe628a401b7c774ff92970b-pi"><img class="asset  asset-image at-xid-6a0112791b8fe628a401b7c774ff92970b img-responsive" title="OrdinateDimensionSet2" src="/assets/image_0e4215.jpg" alt="OrdinateDimensionSet2" /></a></p>
<p>&nbsp;</p>
