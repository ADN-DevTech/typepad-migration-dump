---
layout: "post"
title: "AnyCAD structure with original file names"
date: "2015-06-30 17:54:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/anycad-structure-with-original-file-names.html "
typepad_basename: "anycad-structure-with-original-file-names"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Inside <strong>Inventor 2016</strong> you can reference other <strong>CAD</strong> file formats directly from an assembly. In this case you can also see the structure of the referenced assembly in the model tree. If you&#39;d also need to get back the structure using the original file names - i.e. going from <strong>Occurrence</strong> to the original file - that is currently not exposed in the <strong>API</strong>: no direct access from<strong> &quot;SubOccurrence1&quot; </strong>to<strong> &quot;AnyCAD Part1&quot;&#0160;</strong>in the blow picture.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a75593970b-pi" style="display: inline;"><img alt="Image001-6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7a75593970b image-full img-responsive" src="/assets/image_8972ce.jpg" title="Image001-6" /></a></p>
<p>One workaround could be finding the original files based on the <strong>Occurrence.Name</strong>, since both that and the <strong>Inventor</strong> document names created from the foreign file are based on the name of the original file. You can find the list of the original file names through <strong>Document.File.AllReferencedFiles</strong>:</p>
<pre>Function GetFileName(filePath As String) As String
  Dim i1 As Integer
  i1 = InStrRev(filePath, &quot;\&quot;)
   
  Dim i2 As Integer
  i2 = InStrRev(filePath, &quot;.&quot;)
  
  &#39; Check for occurrence name as well
  If i2 &lt; 1 Then i2 = InStrRev(filePath, &quot;:&quot;)
  
  GetFileName = Mid(filePath, i1 + 1, i2 - i1 - 1)
End Function

Function GetFullPath(fileName As String) As String
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
  
  Dim f As File
  For Each f In asm.File.AllReferencedFiles
    If GetFileName(f.FullFileName) = fileName Then
      GetFullPath = f.FullFileName
      Exit Function
    End If
  Next
End Function

Sub PrintOccurrences( _
occs As ComponentOccurrences, _
i As Integer)
  Dim occ As ComponentOccurrence
  For Each occ In occs
    Debug.Print Space(i); occ.name
    
    Dim doc As Document
    Set doc = occ.Definition.Document
    
    Dim fullPath As String
    
    If doc.IsEmbeddedDocument Then
      Dim fileName As String
      fileName = GetFileName(occ.name)
      fullPath = GetFullPath(fileName)
    Else
      fullPath = doc.FullFileName
    End If
    
    Debug.Print Space(i); &quot; - &quot; + fullPath
    
    Call PrintOccurrences( _
      occ.SubOccurrences, _
      i + 2)
  Next
End Sub

Sub PrintStructure()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim acd As AssemblyComponentDefinition
  Set acd = doc.ComponentDefinition
  
  Call PrintOccurrences(acd.Occurrences, 1)
End Sub</pre>
<p>I get this output in case of my assembly that is referencing a <strong>SolidWorks</strong> assembly file called <strong>gears.SLDASM</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d130bf87970c-pi" style="display: inline;"><img alt="Solidworksassembly" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d130bf87970c image-full img-responsive" src="/assets/image_953340.jpg" title="Solidworksassembly" /></a></p>
<pre> gears:1
  - C:\SolidWorks\Gears\gears.SLDASM
   frame:1
    - C:\SolidWorks\Gears\frame.SLDPRT
   Internal Spur Gear:1
    - C:\SolidWorks\Gears\Internal Spur Gear.sldprt
   Spur Gear:1
    - C:\SolidWorks\Gears\Spur Gear.sldprt
   Spur Gear:2
    - C:\SolidWorks\Gears\Spur Gear.sldprt
   Spur Gear:3
    - C:\SolidWorks\Gears\Spur Gear.sldprt
   Spur Gear:4
    - C:\SolidWorks\Gears\Spur Gear.sldprt
   shaft1:1
    - C:\SolidWorks\Gears\shaft1.SLDPRT
   shaft2:1
    - C:\SolidWorks\Gears\shaft2.SLDPRT
   shaft2:2
    - C:\SolidWorks\Gears\shaft2.SLDPRT
   shaft2:3
    - C:\SolidWorks\Gears\shaft2.SLDPRT
</pre>
