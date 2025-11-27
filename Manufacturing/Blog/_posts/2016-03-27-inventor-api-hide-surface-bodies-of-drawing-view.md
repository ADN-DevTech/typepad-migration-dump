---
layout: "post"
title: "Inventor API: Hide Surface Bodies of Drawing View"
date: "2016-03-27 21:56:24"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/inventor-api-hide-surface-bodies-of-drawing-view.html "
typepad_basename: "inventor-api-hide-surface-bodies-of-drawing-view"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Question</strong>: <br />It is possible to hide Surface Bodies by right clicking on the [Surface Bodies] node in the "Model Tree" and then toggle visibility by clicking on [Visibility]. I want to make a custom function using the inventor API that enables us to programmatically hide all Surface Bodies in a drawing. </p>
<p><strong>Solution</strong>: </p>
<p>I found some customers are using the two ways:</p>
<p>1. toggle the visibility of the surface body of the source parts</p>
<p>2. find out the browser node in the model tree and execute command [visibility].</p>
<p>Re#1, it is to modify the status of the source parts, that means it will affect not only the source parts, but also other drawing views which references this model, while in UI, one drawing view is one representation of the surface bodies. Different drawingviews can represent different surface bodies, even though they are from the same model.</p>
<p>Re#2, it will have to iterate the model tree to find out the correct node. In addition, in some scenarios, it would not take effect at once when executing a command. And you will also need to manage selection set.</p>
<p>Actually, however API has provided the direct way DrawingView.<strong>SetVisibility</strong>( <strong><em>Object</em></strong> As Object, <strong><em>Visible</em></strong> As Boolean ). Valid objects are 2d and 3d sketches, work features, surface features, occurrences and proxies for all of these. The object needs to be supplied in the context of the document referenced by the drawing view. Once the objects are input, API can toggle their visibility like UI does.</p>
<p>This demo below assumes there is a drawing which references one assembly. The assembly contains two parts. In each part, there are two surface bodies respectively. Now, we just want to make view2&gt;&gt;part1&gt;&gt;surface body1 invisible. The code is:</p>
<pre>
Sub toggleSBVisibleinDrawing()

    Dim oDoc As DrawingDocument 
    Set oDoc = ThisApplication.ActiveDocument 
    
    'assume we only want to toggle the surface bodies presenation 
    Dim oView As DrawingView 
    Set oView = oDoc.SelectSet(1) 
    
    'assume the reference document is an assembly 
    Dim oRefDoc As AssemblyDocument 
    Set oRefDoc = oView.ReferencedDocumentDescriptor.ReferencedDocument     
     
    'check one part 
    Dim oOnePartOcc As ComponentOccurrence 
    Set oOnePartOcc = oRefDoc.ComponentDefinition.Occurrences(1)     
     
    Dim oPartDef As PartComponentDefinition 
    Set oPartDef = oOnePartOcc.Definition 
    
    'assume we want to make one surface body invisible. 
    'Note: one part can have more than 1 surface bodies 
    
    Dim oSB1 As SurfaceBody 
    Set oSB1 = oPartDef.SurfaceBodies(1) 
    
    Dim oSB1Proxy As SurfaceBodyProxy 
    Call oOnePartOcc.CreateGeometryProxy(oSB1, oSB1Proxy) 
    
    'toggle the visibility of this object 
    Call oView.SetVisibility(oSB1Proxy, False) 
End Sub
</pre>
<p>&nbsp;</p>
<p>after the code, you can see only the surface body in view2 is invisible. and it does not either affect the source part.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08cf5120970d-pi"><img style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" src="/assets/image_8358bf.jpg" alt="image" width="244" height="231" border="0" /></a></p>
