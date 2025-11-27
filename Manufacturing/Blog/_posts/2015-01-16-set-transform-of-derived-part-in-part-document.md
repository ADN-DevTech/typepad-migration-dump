---
layout: "post"
title: "Set transform of derived part in part document"
date: "2015-01-16 15:05:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/set-transform-of-derived-part-in-part-document.html "
typepad_basename: "set-transform-of-derived-part-in-part-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In theory you should be able to just move a derived part component inside the host part document using <strong>DerivedPartTransformDef.SetTransformation</strong>(). This does change the transformation of the <strong>DerivedPartTransformDef</strong> object but not the derived component itself. You can solve this issue by assigning the modified&#0160;<strong>DerivedPartTransformDef</strong> object back to <strong>DerivedPartComponent.Definition</strong>:</p>
<pre>Sub ChangeTranformation()
 
  Dim oApp As Inventor.Application
  Set oApp = ThisApplication
    
  Dim oDoc As PartDocument
  Set oDoc = oApp.ActiveDocument
    
  Dim oCD As PartComponentDefinition
  Set oCD = oDoc.ComponentDefinition
    
  Dim oFeat As PartFeature
  Set oFeat = oCD.Features(1)
    
  If oFeat.Type = kReferenceFeatureObject Then
      
    Dim oRef As ReferenceFeature
    Set oRef = oFeat
  
    Debug.Print oRef.name &amp; &quot;   &quot; &amp; oRef.Type

    Dim oRefComps As ReferenceComponents
    Set oRefComps = oCD.ReferenceComponents
   
    Dim oDeComp As DerivedPartComponent
    Set oDeComp = oRefComps.DerivedPartComponents(1)

    Dim oTrDef As DerivedPartTransformDef
    Set oTrDef = oDeComp.Definition
   
    Dim oMat As Matrix
    Call oTrDef.GetTransformation(oMat, False)
  
    Dim oGeom As TransientGeometry
    Set oGeom = ThisApplication.TransientGeometry
  
    Dim oMatDelta As Matrix
    Set oMatDelta = oGeom.CreateMatrix
  
    Dim oVect As Vector
    Set oVect = oGeom.CreateVector(2, 0, 0)
  
    Call oMatDelta.SetTranslation(oVect, False)
    Call oMat.PostMultiplyBy(oMatDelta)
  
    &#39; SetTransformation has no affect ...
    Call oTrDef.SetTransformation(oMat, False)
    &#39; ... until you assign it back ...
    oDeComp.Definition = oTrDef
    &#39; Updating the doc should not do any harm,
    &#39; but might not be needed
    oDoc.Update

  End If

End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c1b8ee970c-pi" style="display: inline;"><img alt="DerivedPart" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c1b8ee970c img-responsive" src="/assets/image_bab19a.jpg" title="DerivedPart" /></a></p>
<p>&#0160;</p>
