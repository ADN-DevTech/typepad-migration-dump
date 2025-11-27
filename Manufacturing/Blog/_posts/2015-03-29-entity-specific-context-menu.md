---
layout: "post"
title: "Entity specific context menu"
date: "2015-03-29 12:08:48"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/03/entity-specific-context-menu.html "
typepad_basename: "entity-specific-context-menu"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can add items to the context menu inside the&#0160;<strong>OnLinearMarkingMenu</strong> event handler. This also provides the list of currently selected entities. You can easily check if they are of a given entity type or not - e.g. if they are <strong>Ordinate Dimensions&#0160;</strong>- and based on that decide if we add our own control definition or not.</p>
<p>This <strong>VBA</strong> sample shows how to do it. When the class (I named it <strong>clsLinearMarkingMenu</strong>) is initialized the&#0160;<strong>Class_Initialize</strong> function is called and there we check if our control definition has already been registered. If not, then we create it and register it. Inside the&#0160;<strong>OnExecute</strong> event handler of our control definition we can do the operation we want when our control (context menu item in this case) is clicked. In this case we&#39;ll add a plus sign to the dimension text.</p>
<p>Class <strong>clsLinearMarkingMenu</strong>:</p>
<pre>Dim WithEvents uie As UserInputEvents
Dim WithEvents bd1 As ButtonDefinition

Private Sub bd1_OnExecute(ByVal Context As NameValueMap)
  Dim ss As SelectSet
  Set ss = ThisApplication.ActiveDocument.SelectSet

  Dim ordDim As OrdinateDimension
  For Each ordDim In ss
    &#39; Change the text of the selected ordinate dimensions
    ordDim.Text.FormattedText = _
      ordDim.Text.FormattedText + &quot;+&quot;
  Next
End Sub

Private Sub Class_Initialize()
  Dim cm As CommandManager
  Set cm = ThisApplication.CommandManager
  
  Set uie = cm.UserInputEvents
      
  &#39; Make sure our control is available
  On Error Resume Next
  Set bd1 = cm.ControlDefinitions(&quot;DimTextPlus&quot;)
  If bd1 Is Nothing Then
    Set bd1 = cm.ControlDefinitions.AddButtonDefinition( _
      &quot;Dim Text +&quot;, &quot;DimTextPlus&quot;, kNonShapeEditCmdType)
  End If
End Sub

Function AreAllOrdinate(entities As ObjectsEnumerator) As Boolean
  Dim entity As Object
  For Each entity In entities
    If Not TypeOf entity Is OrdinateDimension Then
      AreAllOrdinate = False
      Exit Function
    End If
  Next
  AreAllOrdinate = True
End Function

Private Sub uie_OnLinearMarkingMenu( _
ByVal SelectedEntities As ObjectsEnumerator, _
ByVal SelectionDevice As SelectionDeviceEnum, _
ByVal LinearMenu As CommandControls, _
ByVal AdditionalInfo As NameValueMap)
  &#39; We only add our context menu item if the
  &#39; selected entities are all ordinate dimensions
  If AreAllOrdinate(SelectedEntities) Then
    Call LinearMenu.AddButton(bd1)
  End If
End Sub
</pre>
<p>Then we can instantiate our class in any of the <strong>Modules</strong>:</p>
<pre>Dim lmm As clsLinearMarkingMenu

Sub TestLinearMarkingMenu()
  Set lmm = New clsLinearMarkingMenu
End Sub</pre>
<p>Now when we right-click on an ordinate dimension our menu item will show up:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0811df82970d-pi" style="display: inline;"><img alt="Ordinate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0811df82970d img-responsive" src="/assets/image_9ce573.jpg" title="Ordinate" /></a></p>
<p>&#0160;&#0160;</p>
