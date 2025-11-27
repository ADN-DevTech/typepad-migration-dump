---
layout: "post"
title: "Synchronize Prompted Entry TextBox values  "
date: "2013-04-18 07:43:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/synchronize-prompted-entry-textbox-values-.html "
typepad_basename: "synchronize-prompted-entry-textbox-values-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you use the same Title Block definition for all Sheet&#39;s then you can go through its Definition, check for the Prompted Entry TextBox&#39;s and keep track of what each TextBox had as value in the main/master sheet where all these values are correct. Now you can update all the other sheets with those values. You could easily modify this code to also update Prompted Entry&#39;s on sheets which are using a different Title Block definition - if they use the same TextBox.Text.</p>
<p>Here is a VBA sample</p>
<pre>&#39; &quot;Dictionary&quot; coming from
&#39; &quot;Microsoft Sctipting Runtime&quot; library
&#39; You could simply use an array instead
Function GetPromptedEntries(block As TitleBlock) As Dictionary
    Set GetPromptedEntries = New Dictionary
    
    Dim boxes As TextBoxes
    Set boxes = block.Definition.Sketch.TextBoxes
    
    Dim box As TextBox
    For Each box In boxes
        &#39; Check if it&#39;s a Prompted Entry
        If InStr(1, box.FormattedText, &quot;&lt;Prompt&quot;) Then
            Dim value As String
            value = block.GetResultText(box)
            
            Call GetPromptedEntries.Add(box, value)
        End If
    Next
End Function

Sub SetPromptedEntries(block As TitleBlock, dict As Dictionary)
    If block Is Nothing Then Exit Sub

    If dict.Count &lt; 1 Then Exit Sub
    
    Dim firstBox As TextBox
    Set firstBox = dict.Keys(0)
    
    Dim blockSketch As DrawingSketch
    Set blockSketch = block.Definition.Sketch
    
    &#39; At the moment we only do the update for sheets
    &#39; using the same block definition, because we are using
    &#39; the TextBox objects as keys, which would be different
    &#39; for different title block definitions
    If Not firstBox.parent Is blockSketch Then Exit Sub
    
    Dim box As TextBox
    Dim value As String
    For Each box In dict
        value = dict(box)
        Call block.SetPromptResultText(box, value)
    Next
End Sub

Sub SyncPromptedEntries()
    Dim dwg As DrawingDocument
    Set dwg = ThisApplication.ActiveDocument
    
    Dim curSheet As Sheet
    Dim entries As Dictionary
    For Each curSheet In dwg.Sheets
        If entries Is Nothing Then
            Set entries = GetPromptedEntries( _
                curSheet.TitleBlock)
        Else
            Call SetPromptedEntries( _
                curSheet.TitleBlock, _
                entries)
        End If
    Next
End Sub</pre>
