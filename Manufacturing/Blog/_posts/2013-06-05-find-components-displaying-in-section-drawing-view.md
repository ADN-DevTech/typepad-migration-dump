---
layout: "post"
title: "find components displaying in section drawing view"
date: "2013-06-05 22:36:15"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/06/find-components-displaying-in-section-drawing-view.html "
typepad_basename: "find-components-displaying-in-section-drawing-view"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>I noticed one forum post my colleague Philippe has helped. I think it may help some customers. This question is to traverse the components which display in the section drawing view. </p>  <p>There is not a direct way, but possible with API. </p>  <p>You could either iterate through all occurrences of your top level assembly and see if &quot;DrawingView.DrawingCurves(occurrence)&quot; returns something, if yes the component is part of the view.</p>  <p>Or the other way around, iterate through all the DrawingCurves in the specific view and check &quot;DrawingCurve.ModelGeometry&quot; to see which occurrence this belong to.</p>  <p>The code below is a small demo. It assumes the second view of the sheet is a section view.</p>  <pre>Public Sub GetViewComponents()

    Dim doc As DrawingDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim sectionView As SectionDrawingView
    Set sectionView = doc.ActiveSheet.DrawingViews(2)
    
    Dim docDesc As DocumentDescriptor
    Set docDesc = sectionView.ReferencedDocumentDescriptor

    Dim asm As AssemblyDocument
    Set asm = docDesc.ReferencedDocument
    
    Debug.Print &quot;Occurrences in View: &quot; &amp; sectionView.name
    
    On Error Resume Next
    
    Dim occurrence As ComponentOccurrence
    For Each occurrence In asm.ComponentDefinition.Occurrences
    
        Dim curves As DrawingCurvesEnumerator
        Set curves = sectionView.DrawingCurves(occurrence)
        
        If Err Then
            'DrawingCurves fails if no curves...
            Err.Clear
        ElseIf curves Is Nothing Or curves.count = 0 Then
            'Component not in view...
        Else
            'Component is in section view
            Debug.Print occurrence.name
        End If
    
    Next
    
End Sub</pre>
