---
layout: "post"
title: "Set Visibility of Sketch in sub assembly"
date: "2013-06-21 10:30:09"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/06/set-visibility-of-sketch-in-sub-assembly.html "
typepad_basename: "set-visibility-of-sketch-in-sub-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The following blog post talks about ComponentOccurrence contexts, which is relevant in this case as well: <a href="http://adndevblog.typepad.com/manufacturing/2012/12/componentoccurrence-contexts-and-reference-keys.html" target="_blank">http://adndevblog.typepad.com/manufacturing/2012/12/componentoccurrence-contexts-and-reference-keys.html</a></p>
<p>If you want to set the visibility of an object that can only be reached through a ComponentDefinition (PartComponentDefinition/AssemblyComponentDefinition) object and it resides in a sub assembly, then you need to bring it into the context of the main/top assembly using CreateGeometryProxy. This is what the following code does as well. This will make the sketch highlighted in the picture (Sketch 2) invisible.</p>
<p>
<a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019103a3361c970c-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b019103a3361c970c" style="width: 450px;" title="SketchVisibility" src="/assets/image_fddf3b.jpg" alt="SketchVisibility" /></a></p>
<pre>Sub HideSubAsmSketch()
    Dim dwg As DrawingDocument
    Set dwg = ThisApplication.ActiveDocument
    
    Dim baseView As DrawingView
    Set baseView = dwg.Sheets(1).DrawingViews(1)
    
    Dim mainAsm As AssemblyDocument
    Set mainAsm = _
      baseView.ReferencedDocumentDescriptor.ReferencedDocument
    
    Dim mainDef As AssemblyComponentDefinition
    Set mainDef = mainAsm.ComponentDefinition
    
    Dim subOcc As ComponentOccurrence
    Set subOcc = mainDef.Occurrences(1)
    
    Dim subDef As AssemblyComponentDefinition
    Set subDef = subOcc.Definition
    ' This does exactly the same as the below code. This
    ' way we'll get outside of the context of the main
    ' assembly and get inside the context of the sub assembly
    ' Set subDef = GetSubAsmDef()
    
    ' Anything we get through subDef will need to be
    ' brought into the context of mainAsm if
    ' that's where we want to set properties
    Dim sk As PlanarSketch
    Set sk = subDef.Sketches("Sketch 2")
    
    ' Let's bring sk into the mainAsm context through the
    ' occurrence object
    Dim skProxy As PlanarSketchProxy
    Call subOcc.CreateGeometryProxy(sk, skProxy)
    
    ' Now we can set the visibility for it
    Call baseView.SetVisibility(skProxy, False)
End Sub

Function GetSubAsmDef() As AssemblyComponentDefinition
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.Documents.Open("SubAsm.iam")
  
  Set GetSubAsmDef = asm.ComponentDefinition
End Function</pre>
