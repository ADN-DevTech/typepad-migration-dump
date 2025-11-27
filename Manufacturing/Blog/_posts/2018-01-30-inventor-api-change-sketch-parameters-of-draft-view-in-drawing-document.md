---
layout: "post"
title: "Inventor API: Change sketch parameters of draft view in drawing document"
date: "2018-01-30 19:58:11"
author: "Xiaodong Liang"
categories:
  - "iLogic"
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/01/inventor-api-change-sketch-parameters-of-draft-view-in-drawing-document.html "
typepad_basename: "inventor-api-change-sketch-parameters-of-draft-view-in-drawing-document"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In drawing document, draft view also contains sketch with the similar functionalities of genetic sketch in part or assembly. While it looks the dimension constraint of the sketch entities are not managed by Parameter in the document.&#0160;</p>
<p>With API, we can still access these parameters and manipulate them directly.&#0160; The VBA code demos a workflow to change a specific parameter of a specific sketch of a specific draft view. Since draft view could have the same name, the code uses index as a unique identification.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2d574d9970c-pi" style="display: inline;"><img alt="Screen Shot 2018-01-31 at 11.47.03 AM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2d574d9970c image-full img-responsive" src="/assets/image_d7ec2f.jpg" title="Screen Shot 2018-01-31 at 11.47.03 AM" /></a><br />&#0160;</p>
<p>&#0160;</p>
<pre><code>

Sub test()

  Call changeDraftSketchConstaintParam(1, &quot;d0&quot;, &quot;200 in&quot;)
  
End Sub
Sub changeDraftSketchConstaintParam(draftIndex, paramName, newV)

    Dim oDoc As DrawingDocument
    Set oDoc = ThisApplication.ActiveDocument
    
    Dim oSheet As Sheet
    Set oSheet = oDoc.ActiveSheet
    
    Dim oView As DrawingView
    Set oView = oSheet.DrawingViews(1)
    
    If oView.ViewType = kDraftDrawingViewType Then
        Dim oDraftSketch As Sketch
        Set oDraftSketch = oView.Sketches(draftIndex)
        
        If Not oDoc.ActivatedObject Is oDraftSketch Then
           oDraftSketch.Edit
        End If
        
        Dim oDim As DimensionConstraint
        For Each oDim In oDraftSketch.DimensionConstraints
            Dim oParam As Parameter
            Set oParam = oDim.Parameter
            
            If oParam.Name = paramName Then
                 oParam.Expression = newV
                  
            End If
        Next
        oDraftSketch.Solve
        oDraftSketch.ExitEdit
    End If
    oDoc.Update
End Sub . 

</code></pre>
<p>iLogic does not provide a direct module to access these parameters, but the same VBA code could be used in iLogic with just a little bit modification:</p>
<pre><code>
Sub Main()

  Call changeDraftSketchConstaintParam(1, &quot;d0&quot;, &quot;200 in&quot;)
  
End Sub

Sub changeDraftSketchConstaintParam(draftIndex, paramName, newV)

    Dim oDoc As DrawingDocument
    oDoc = ThisApplication.ActiveDocument
    
    Dim oSheet As Sheet
    oSheet = oDoc.ActiveSheet
    
    Dim oView As DrawingView
    oView = oSheet.DrawingViews(1)
    
    If oView.ViewType = kDraftDrawingViewType Then
        Dim oDraftSketch As Sketch
         oDraftSketch = oView.Sketches(draftIndex)
        
        If Not oDoc.ActivatedObject Is oDraftSketch Then
           Call oDraftSketch.Edit()
        End If
        
        Dim oDim As DimensionConstraint
        For Each oDim In oDraftSketch.DimensionConstraints
            Dim oParam As Parameter
            oParam = oDim.Parameter
            
            If oParam.Name = paramName Then
                 oParam.Expression = newV
                  
            End If
        Next
        Call oDraftSketch.Solve()

        Call oDraftSketch.ExitEdit()
    End If
    Call oDoc.Update()
End Sub

  
</code></pre>
