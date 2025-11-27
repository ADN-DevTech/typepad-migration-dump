---
layout: "post"
title: "Create Sketch Text on Face of Assembly"
date: "2016-09-27 01:23:36"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/09/create-sketch-text-on-face-of-assembly.html "
typepad_basename: "create-sketch-text-on-face-of-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>:     <br />In a assembly (or part), I want to select a face, on this face I want the Partnumber as a sketh text, (with a big font)</p>  <p>Is there a simple solution?</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb093b0a99970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="medium" border="0" alt="medium" src="/assets/image_b3977c.jpg" width="310" height="190" /></a></p>  <p><strong>Answer</strong>:</p>  <p>The code below assumes a face of assembly is selected, and a planar sketch will be created on the face, and a text will be added on the location of the center point of the face area.</p>  <p>VBA code:</p>  <pre><code>
Sub addTextToFace()

    Dim oAssDoc As AssemblyDocument
    Set oAssDoc = ThisApplication.ActiveDocument
        
    Dim oAssDef As AssemblyComponentDefinition
    Set oAssDef = oAssDoc.ComponentDefinition
    
    'assume a face has been selected in the context of assembly
    Dim oFace As FaceProxy
    Set oFace = oAssDoc.SelectSet(1)
    
    
    
    'make sure it is a planar face
    If oFace.SurfaceType = kPlaneSurface Then
        
        Dim oPlanarSurface As Plane
        Set oPlanarSurface = oFace.Geometry
        
        Dim oSketch As PlanarSketch
        Set oSketch = oAssDef.Sketches.Add(oFace)
        
        
        'trying to choose an appropriate point
        'assume this planar face has one edge loop only
        Dim oEdgeLoop As EdgeLoop
        Set oEdgeLoop = oFace.EdgeLoops(1)
        
        Dim oMinPt As Point
        Set oMinPt = oEdgeLoop.RangeBox.MinPoint
        
        
        Dim oMaxPt As Point
        Set oMaxPt = oEdgeLoop.RangeBox.MaxPoint
        
        Dim oCenterPt As Point
        Set CenterPt = ThisApplication.TransientGeometry.CreatePoint((oMaxPt.X + oMinPt.X) / 2#, (oMaxPt.Y + oMinPt.Y) / 2#, (oMaxPt.Z + oMinPt.Z) / 2#)
         

        
        'get one point on the face and transform to the point2d on the sketch
        Dim oTextPt As Point2d
        'Set oTextPt = oSketch.ModelToSketchSpace(oPlanarSurface.RootPoint)
        Set oTextPt = oSketch.ModelToSketchSpace(CenterPt)
        
        'add the textbox
        Dim oSketchText As TextBox
        Set oSketchText = oSketch.TextBoxes.AddFitted(oTextPt, &quot;MY TEST&quot;)
        
        
         
    Else
        MsgBox &quot;please select a planar face!&quot;
    End If
    
    

End Sub

</code></pre>

<p>&#160;</p>

<p>iLogic code:</p>

<pre><code>
  
oAssDoc = ThisApplication.ActiveDocument        

oAssDef = oAssDoc.ComponentDefinition

If oAssDoc.SelectSet.Count = 0 Then
	MsgBox(&quot;no face is selected!&quot;)
Else
	'assume a face has been selected in the context of assembly
	oFace = oAssDoc.SelectSet(1)
	
	'make sure it is a planar face
	If oFace.SurfaceType = SurfaceTypeEnum.kPlaneSurface Then
		
		oPlanarSurface = oFace.Geometry
		
			'add a sketch
		oSketch = oAssDef.Sketches.Add(oFace)    
			
		'trying to choose an appropriate point
		'assume this planar face has one edge loop only
		oEdgeLoop = oFace.EdgeLoops(1)
			
		oMinPt = oEdgeLoop.RangeBox.MinPoint 
		oMaxPt = oEdgeLoop.RangeBox.MaxPoint
			
		CenterPt = ThisApplication.TransientGeometry.CreatePoint((oMaxPt.X + oMinPt.X) / 2#, (oMaxPt.Y + oMinPt.Y) / 2#, (oMaxPt.Z + oMinPt.Z) / 2#)    
			
		'get one point on the face and transform to the point2d on the sketch 
		'Set oTextPt = oSketch.ModelToSketchSpace(oPlanarSurface.RootPoint)
		oTextPt = oSketch.ModelToSketchSpace(CenterPt)
			
			'add the textbox
		oSketchText = oSketch.TextBoxes.AddFitted(oTextPt, &quot;MY TEST&quot;) 
		
	Else
		MsgBox( &quot;please select a planar face!&quot;)
	End If
End If

     
</code></pre>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2216d21970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_497d3d.jpg" width="309" height="331" /></a></p>
