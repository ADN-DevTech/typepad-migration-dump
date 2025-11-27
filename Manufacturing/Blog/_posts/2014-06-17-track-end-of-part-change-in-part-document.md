---
layout: "post"
title: "Track End Of Part change in part document"
date: "2014-06-17 05:38:18"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/track-end-of-part-change-in-part-document.html "
typepad_basename: "track-end-of-part-change-in-part-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to track how the &#39;<strong>End Of Part</strong>&#39; symbol is moved around the part then you could listen to the <strong>DocumentEvents.OnChange</strong> event and store it&#39;s current position.</p>
<p><strong> clsEndOfPart</strong>:</p>
<pre>Dim WithEvents de As DocumentEvents
Dim wasAt As String

Private Sub Class_Initialize()
    Set de = ThisApplication.ActiveDocument.DocumentEvents
End Sub

Private Sub de_OnChange( _
ByVal ReasonsForChange As CommandTypesEnum, _
ByVal BeforeOrAfter As EventTimingEnum, _
ByVal Context As NameValueMap, _
HandlingCode As HandlingCodeEnum)

    If Context.Value(&quot;InternalName&quot;) = &quot;Reorder Feature&quot; Then
        Dim doc As PartDocument
        Set doc = ThisApplication.ActiveDocument
        
        Dim a As Object, b As Object
        Call doc.ComponentDefinition.GetEndOfPartPosition(a, b)
        
        Dim nowAt As String
        If b Is Nothing Then
            nowAt = &quot;at the very end&quot;
        Else
            nowAt = &quot;before &quot; + b.Name
        End If
        
        If BeforeOrAfter = kBefore Then
            wasAt = nowAt
        Else
            MsgBox (&quot;&#39;End Of Part&#39; was &quot; + wasAt + _
                &quot;, but now it&#39;s &quot; + nowAt + &quot;.&quot;)
        End If
    End If
End Sub</pre>
<p>Then call it like this:</p>
<pre>Dim eop As clsEndOfPart

Sub TestEndOfPart()
    Set eop = New clsEndOfPart
End Sub</pre>
<p>This is how it works:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd1fc0cd970b-pi" style="display: inline;"><img alt="Eop" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd1fc0cd970b image-full img-responsive" src="/assets/image_cb8bd8.jpg" title="Eop" /></a></p>
<p>&#0160;</p>
