---
layout: "post"
title: "Show up-to-date loop length"
date: "2013-03-26 14:57:44"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/03/show-up-to-date-loop-length.html "
typepad_basename: "show-up-to-date-loop-length"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Through the user interface you can query the length of an entity loop using the <strong>Measure Loop</strong> tool (Tools &gt;&gt; Measure &gt;&gt; Loop)</p>
<p>If you want to keep showing the length of a specific loop, e.g. in the sketch environment, while editing the geometry, you can do that from your AddIn or VBA. In this case I created a form class in VBA called <strong>LengthForm</strong>, with a <strong>Label</strong> (named <strong>LengthValue</strong>) and a <strong>Button</strong> (named <strong>MarkEntity</strong>) on it and used the following code.<br />Note that this part of the code is for the form that you&#39;ve just created. Right-click on the <strong>LengthForm</strong> in the <strong>Project</strong>&#0160;window&#39;s tree and select <strong>View Code</strong> - that&#39;s where this code should be pasted.</p>
<pre>Private WithEvents docEvents As DocumentEvents
Private selEnt As Object

Private Sub docEvents_OnChange( _
ByVal ReasonsForChange As CommandTypesEnum, _
ByVal BeforeOrAfter As EventTimingEnum, _
ByVal Context As NameValueMap, _
HandlingCode As HandlingCodeEnum)
    If BeforeOrAfter &lt;&gt; kAfter Then Exit Sub
    
    LengthValue.Caption = GetLength(selEnt)
End Sub

Private Sub MarkEntity_Click()
    Dim ss As SelectSet
    Set ss = ThisApplication.ActiveDocument.SelectSet
    
    If ss.Count = 0 Then
        Set docEvents = Nothing
        
        LengthValue.Caption = &quot;&quot;
    Else
        Set docEvents = _
            ThisApplication.ActiveDocument.DocumentEvents
            
        Set selEnt = ss(1)
        LengthValue.Caption = GetLength(selEnt)
    End If
End Sub

Function GetLength(ent As Object) As String
    Dim l As Double
    &#39; Try this
    &#39;l = ThisApplication.MeasureTools.GetLoopLength(ent)
    &#39; Or this
    l = GetFirstLoopLength(ent)
    
    &#39; Internal length unit is cm
    GetLength = str(l) + &quot; cm&quot;
End Function

&#39; This is using own logic
Function GetFirstLoopLength(ByVal ent As SketchEntity) As Double
    If ent Is Nothing Then Exit Function
    
    Dim ents As ObjectCollection
    Set ents = ThisApplication.TransientObjects.CreateObjectCollection
    
    Call ents.Add(ent)
    
    Dim success As Boolean
    success = GetFirstLoopRec(ent, ent.EndSketchPoint, ents)
    
    If success Then
        Dim l As Double
        For Each ent In ents
            l = l + ent.Length
        Next
        
        GetFirstLoopLength = l
    End If
End Function

&#39; Return True means we&#39;re done
Function GetFirstLoopRec(prev As SketchEntity, sp As SketchPoint, _
ents As ObjectCollection) As Boolean
     Dim first As Object
     Set first = ents(1)
     
     Dim ent As SketchLine
     For Each ent In sp.OwnedBy
        &#39; If it&#39;s not the same as the previuos
        &#39; entity
        If Not ent Is prev Then
            &#39; If it&#39;s the same as the first entity
            &#39; then we made a loop
            If ent Is first Then
                GetFirstLoopRec = True
                Exit Function
            End If
            
            Call ents.Add(ent)
            &#39; Get the sketch point on the other
            &#39; side of this entity
            If ent.StartSketchPoint Is sp Then
                If GetFirstLoopRec( _
                ent, ent.EndSketchPoint, ents) Then
                    GetFirstLoopRec = True
                    Exit Function
                End If
            Else
                If GetFirstLoopRec( _
                ent, ent.StartSketchPoint, ents) Then
                    GetFirstLoopRec = True
                    Exit Function
                End If
            End If
        End If
     Next
     
     GetFirstLoopRec = False
End Function</pre>
<p>Now I can show it modelessly, so that it is always visible.&#0160;</p>
<pre>Dim lf As LengthForm

Sub ShowLengthForm()
    Set lf = New LengthForm
    lf.Show vbModeless
End Sub</pre>
<p>When testing the code, first select a sketch entity that is part of a loop, and then click <strong>Mark Entity</strong>.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d424fb76d970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="LoopLength" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d424fb76d970c image-full" src="/assets/image_34cedf.jpg" title="LoopLength" /></a><br />Note: the&#0160;ThisApplication.MeasureTools.GetLoopLength(ent) function did not always find the loop. My own function seems to work fine, but I did not thoroughly test it.</p>
