---
layout: "post"
title: "Transform imported geometry"
date: "2015-02-16 06:24:27"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/transform-imported-geometry.html "
typepad_basename: "transform-imported-geometry"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When importing geometry from another <strong>CAD</strong> format like <strong>*.step</strong> then you cannot specify the transformation for it in the <strong>UI</strong>. The only thing you can do is transform the geometry in another part document derived from the one that has the imported geometry.<br />However, using <a href="http://modthemachine.typepad.com/my_weblog/about-the-author.html" target="_self">Brian</a>&#39;s solution you can achieve that through the <strong>API</strong> directly in the part document that has the imported geometry. It also takes care of keeping the look of the component the same by copying the <strong>Appearance</strong> of the original faces:</p>
<pre>Public Sub ReorientBaseBody()
  Dim partDoc As PartDocument
  Set partDoc = ThisApplication.ActiveDocument
  
  Dim pcd As PartComponentDefinition
  Set pcd = partDoc.ComponentDefinition
  
  Dim npbfs As NonParametricBaseFeatures
  Set npbfs = pcd.Features.NonParametricBaseFeatures

  &#39; Get the base feature that was created as a result of 
  &#39; the translation.
  Dim baseFeature As NonParametricBaseFeature
  Set baseFeature = npbfs(1)

  &#39; Get the body created by the translation.
  Dim originalBody As SurfaceBody
  Set originalBody = baseFeature.InputSurfaceBodies(1)

  &#39; Create a transient copy of the body.
  Dim newBody As SurfaceBody
  Set newBody = ThisApplication.TransientBRep.Copy(originalBody)

  Dim tg As TransientGeometry
  Set tg = ThisApplication.TransientGeometry
 
  Dim tr As TransientObjects
  Set tr = ThisApplication.TransientObjects

  &#39; Define the transform to apply to the body.
  Dim trans As Matrix
  Set trans = tg.CreateMatrix
  Call trans.SetCoordinateSystem( _
    tg.CreatePoint(2, 2, 2), tg.CreateVector(0, 1, 0), _
    tg.CreateVector(-1, 0, 0), tg.CreateVector(0, 0, 1))

  &#39; Apply the transform to the transient body.
  Call ThisApplication.TransientBRep.Transform(newBody, trans)

  &#39; Create a new base feature.
  Dim baseFeatureDef As NonParametricBaseFeatureDefinition
  Set baseFeatureDef = npbfs.CreateDefinition()
  Dim inputBodies As ObjectCollection
  Set inputBodies = tr.CreateObjectCollection
  Call inputBodies.Add(newBody)
  baseFeatureDef.BRepEntities = inputBodies
  baseFeatureDef.OutputType = kSolidOutputType
  Dim newFeature As NonParametricBaseFeature
  Set newFeature = npbfs.AddByDefinition(baseFeatureDef)

  Dim realBody As SurfaceBody
  Set realBody = newFeature.SurfaceBodies(1)

  Dim i As Integer
  For i = 1 To originalBody.Faces.count
    Dim originalFace As Face
    Set originalFace = originalBody.Faces(i)
    
    If originalFace.AppearanceSourceType = kOverrideAppearance Then
      Dim newFace As Face
      Set newFace = realBody.Faces(i)
        
      newFace.Appearance = originalFace.Appearance
    End If
  Next

  &#39; Delete the original feature.
  baseFeature.Delete
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c74dfe96970b-pi" style="display: inline;"><img alt="Transform" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c74dfe96970b image-full img-responsive" src="/assets/image_2921f5.jpg" title="Transform" /></a></p>
<p>&#0160;</p>
