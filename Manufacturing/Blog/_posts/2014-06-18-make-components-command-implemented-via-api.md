---
layout: "post"
title: "Make Components command implemented via API"
date: "2014-06-18 06:20:53"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/make-components-command-implemented-via-api.html "
typepad_basename: "make-components-command-implemented-via-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to customize the behaviour of <strong>Manage &gt;&gt; Layout &gt;&gt; Make Components</strong> command then doing the whole thing via the API might be a good alternative. Here is the code which seems to be doing the same - as far as I can tell. This VBA code also sets the <strong>Project &gt;&gt; Description</strong> iProperty of each created part to the name of the Solid Body that it is referencing from the original part:</p>
<pre>' Run this inside a Multi-Solid part
Sub MakeComponentsProgrammatically()
  ' Folder to place the new components:
  ' assembly and subcomponents
  Dim f As String: f = "C:\temp\test1\"
  
  ' Make sure the folder exists
  Dim fso As Object
  Set fso = ThisApplication.FileManager.FileSystemObject
  If Not fso.FolderExists(f) Then Call fso.CreateFolder(f)
  
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  ' Create the assembly
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.Documents.Add(kAssemblyDocumentObject)
  
  Dim sb As SurfaceBody
  For Each sb In doc.ComponentDefinition.SurfaceBodies
    ' Create part for each body
    Dim prt As PartDocument
    Set prt = ThisApplication.Documents.Add(kPartDocumentObject)
    
    ' Set iProperties &gt;&gt; Project &gt;&gt; Description
    ' It's inside "Design Tracking Properties"
    Dim p As Property
    Set p = prt.PropertySets( _
      "{32853F0F-3444-11D1-9E93-0060B03C1CA6}")("Description")
    p.Expression = sb.name
    
    Dim dpcs As DerivedPartComponents
    Set dpcs = prt.ComponentDefinition.ReferenceComponents. _
      DerivedPartComponents
    
    Dim dpd As DerivedPartUniformScaleDef
    Set dpd = dpcs.CreateUniformScaleDef(doc.FullDocumentName)
       
    ' Exclude the other solid bodies
    Dim dpe As DerivedPartEntity
    For Each dpe In dpd.Solids
      ' Have to set it for all entities as the default value
      ' can differ in various cases 
      dpe.IncludeEntity = dpe.ReferencedEntity Is sb
    Next
    
    Call dpcs.Add(dpd)
    
    ' Could have any name but we use the solid body's name
    Call prt.SaveAs(f + sb.name + ".ipt", False)
        
    ' Place an instance of it inside the assembly
    Dim mx As Matrix
    Set mx = ThisApplication.TransientGeometry.CreateMatrix()
    Call asm.ComponentDefinition.Occurrences. _
      AddByComponentDefinition(prt.ComponentDefinition, mx)
    
    ' Don't need it anymore
    Call prt.Close
  Next
  
  Call asm.SaveAs( _
    f + Left(doc.DisplayName, Len(doc.DisplayName) - 4) + _
    ".iam", False)
  Call asm.Close
End Sub</pre>
<p>This is what we start off with:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511d014f2970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511d014f2970c img-responsive" title="MC1" src="/assets/image_4245a7.jpg" alt="MC1" border="0" /></a></p>
<p>And this is what we get after running the code:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511d0150c970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511d0150c970c image-full img-responsive" title="MC2" src="/assets/image_70c9dc.jpg" alt="MC2" border="0" /></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
