---
layout: "post"
title: "Change selection from a mini contextual toolbar command"
date: "2015-08-03 05:23:16"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/08/change-selection-from-a-mini-contextual-toolbar-command.html "
typepad_basename: "change-selection-from-a-mini-contextual-toolbar-command"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>It seems that if you change the selection from a command invoked from the <strong>mini contextual toolbar</strong> then the mini toolbar will disappear. If you start the same command from e.g. the <strong>Ribbon</strong> then all is fine and the mini toolbar stays visible.</p>
<p>The above gave me the idea that perhaps starting our command asynchronously from the mini toolbar could work - and it seems to :)</p>
<p>We have our command &quot;<strong>MyCommand</strong>&quot; that changes the selection and we create another command &quot;<strong>MyCommand_MiniToolbar</strong>&quot; for the mini toolbar that does not do anything apart from starting our real command <strong>asynchronously</strong>: <a href="http://adndevblog.typepad.com/manufacturing/2015/02/execute-vs-execute2-of-controldefinition.html" target="_self">see difference between synchronous and asynchronous execution</a>.</p>
<p>Here is our <strong>VBA</strong> class &quot;<strong>clsMiniToolbar</strong>&quot; that creates our commands and subscribes to the&#0160;<strong>OnContextualMiniToolbar</strong> event where we can add our command to the mini toolbar:</p>
<pre>Dim WithEvents oEvents As UserInputEvents
Dim WithEvents oBD As ButtonDefinition
Dim WithEvents oBD_MiniToolbar As ButtonDefinition

Private Sub Class_Initialize()
    Dim oCM As CommandManager
    Set oCM = ThisApplication.CommandManager
    Set oEvents = oCM.UserInputEvents
    Call AddCommands
End Sub

Sub AddCommands()
    Dim oCM As CommandManager
    Set oCM = ThisApplication.CommandManager
    
    Dim oCDs As ControlDefinitions
    Set oCDs = oCM.ControlDefinitions
    
    On Error Resume Next
    oCDs(&quot;MyCommand&quot;).Delete
    oCDs(&quot;MyCommand_MiniToolbar&quot;).Delete
    On Error GoTo 0
    
    Dim oImage As Object
    &#39; Make sure the path is correct
    Set oImage = LoadPicture(&quot;C:\temp\32.bmp&quot;)

    Set oBD = oCDs.AddButtonDefinition( _
        &quot;MyCommand&quot;, &quot;MyCommand&quot;, _
        kNonShapeEditCmdType, _
        &quot;MyClientid&quot;, &quot;My Description&quot;, _
        &quot;My Tooltip&quot;, oImage, oImage)
        
    Set oBD_MiniToolbar = oCDs.AddButtonDefinition( _
        &quot;MyCommand_MiniToolbar&quot;, &quot;MyCommand_MiniToolbar&quot;, _
        kNonShapeEditCmdType, _
        &quot;MyClientid&quot;, &quot;My Description&quot;, _
        &quot;My Tooltip&quot;, oImage, oImage)
End Sub

Private Sub oBD_MiniToolbar_OnExecute(ByVal Context As NameValueMap)
    Dim oCM As CommandManager
    Set oCM = ThisApplication.CommandManager
    
    &#39; Just run the other command asynchronously
    Call oCM.ControlDefinitions(&quot;MyCommand&quot;).Execute2(False)
End Sub

Private Sub oBD_OnExecute(ByVal Context As NameValueMap)
    &#39; Select the edges of a face
    Dim oPD As PartDocument
    Set oPD = ThisApplication.ActiveDocument
    
    Dim oF As Face
    Set oF = oPD.ComponentDefinition.SurfaceBodies(1).Faces(1)
    
    Dim oE As Edge
    For Each oE In oF.Edges
        Call oPD.SelectSet.Select(oE)
    Next
End Sub

Private Sub oEvents_OnContextualMiniToolbar( _
ByVal SelectedEntities As ObjectsEnumerator, _
ByVal DisplayedCommands As NameValueMap, _
ByVal AdditionalInfo As NameValueMap)
  &#39; Add the toolbar command
  Call DisplayedCommands.Add(&quot;MyCommand_MiniToolbar&quot;, 0)
End Sub
</pre>
<p>Now we can instantiate our class from a module:</p>
<pre>Dim oMT As clsMiniToolbar

Sub AddToMiniToolbar()
    Set oMT = New clsMiniToolbar
End Sub</pre>
<p>This way the mini toolbar stays visible after running our command which selects extra edges in the part document:</p>
<p><a class="asset-img-link" href="http://a4.typepad.com/6a0112791b8fe628a401bb085d412c970d-pi" style="display: inline;"><img alt="MiniToolbar" class="asset  asset-image at-xid-6a0112791b8fe628a401bb085d412c970d img-responsive" src="/assets/image_aaae4e.jpg" title="MiniToolbar" /></a></p>
<p><strong>Note</strong>: you need a <strong>32x32 pixel bitmap</strong>&#0160;for the command to be able to add it to the mini toolbar. You can simply use the attached <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d142f929970c img-responsive"><a href="http://adndevblog.typepad.com/files/32.bmp">32.bmp</a></span>&#0160;- just place it inside <strong>C:\temp</strong> before running the above code.&#0160;</p>
