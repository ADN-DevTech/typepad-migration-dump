---
layout: "post"
title: "Remove OLE Links using ReferencedOLEFileDescriptor"
date: "2017-12-19 19:27:16"
author: "Xiaodong Liang"
categories:
  - "iLogic"
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/12/remove-ole-links-using-referencedolefiledescriptor.html "
typepad_basename: "remove-ole-links-using-referencedolefiledescriptor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>A few years ago, my colleague Wayne composed <a href="http://adndevblog.typepad.com/manufacturing/2014/04/remove-missing-ole-links-using-referencedolefiledescriptor-1.html">a blog</a> on how to remove missing OLE links. Recently, a customer wanted to remove any OLE links when checking in to Vault. Ideally, he needs an iLogic code. So I refactored a little with the code Wayne demoed in VBA and iLogic:</p>

<pre><code>
Sub Main()
 
    Dim oDoc As Document
    oDoc = ThisApplication.ActiveDocument
    
    
    If TypeOf oDoc Is AssemblyDocument Then
            Call compositeDoc(oDoc)
    ElseIf TypeOf oDoc Is PartDocument Then
             Call singleDoc(oDoc)
    ElseIf TypeOf oDoc Is DrawingDocument Then
             Call singleDoc(oDoc)
    Else
    	Call MsgBox("wrong file type!") 
    End If
    
    Call oDoc.Update2(True)
     
  
End Sub
 

Sub singleDoc(doc)
        
    Dim oEachOLEDesc As ReferencedOLEFileDescriptor
    For Each oEachOLEDesc In doc.ReferencedOLEFileDescriptors
        oEachOLEDesc.Delete
        doc.Dirty = True
    Next
     
   
End Sub
 
Sub compositeDoc(parentAssDoc)
 
     'remove links of top document
     Call singleDoc(parentAssDoc)

    'remove links of sub document
     For Each oEachDesc In parentAssDoc.ReferencedDocumentDescriptors
        Dim doc As Document
        doc = oEachDesc.ReferencedDocument
        
        Dim oEachOLEDesc As ReferencedOLEFileDescriptor
        For Each oEachOLEDesc In doc.ReferencedOLEFileDescriptors
            oEachOLEDesc.Delete
            doc.Dirty = True
        Next
       
        If TypeOf doc Is AssemblyDocument Then
            Call compositeDoc(doc)
        End If
    Next
   
End Sub

</code></pre>
The below code is written in VBA.

<pre><code>
Sub breakLinks()
 
    Dim oDoc As Document
    Set oDoc = ThisApplication.ActiveDocument
    
    
    If TypeOf oDoc Is AssemblyDocument Then
            
            
            Call compositeDoc(oDoc)
    ElseIf TypeOf oDoc Is PartDocument Then
             Call singleDoc(oDoc)
    ElseIf TypeOf oDoc Is DrawingDocument Then
             Call singleDoc(oDoc)
    Else
    
        Call MsgBox("wrong file type!")
            
    End If
    
    Call oDoc.Update2(True)
     
  
End Sub
 

Sub singleDoc(doc)
        
    Dim oEachOLEDesc As ReferencedOLEFileDescriptor
    For Each oEachOLEDesc In doc.ReferencedOLEFileDescriptors
        oEachOLEDesc.Delete
        doc.Dirty = True
    Next
     
   
End Sub
 
Sub compositeDoc(parentAssDoc)
 
      'remove links of top document
     Call singleDoc(parentAssDoc)

      'remove links of sub document
     For Each oEachDesc In parentAssDoc.ReferencedDocumentDescriptors
        Dim doc As Document
        Set doc = oEachDesc.ReferencedDocument
        
        Dim oEachOLEDesc As ReferencedOLEFileDescriptor
        For Each oEachOLEDesc In doc.ReferencedOLEFileDescriptors
            oEachOLEDesc.Delete
            doc.Dirty = True
        Next
       
        If TypeOf doc Is AssemblyDocument Then
            Call compositeDoc(doc)
        End If
    Next
   
End Sub

</code></pre>
