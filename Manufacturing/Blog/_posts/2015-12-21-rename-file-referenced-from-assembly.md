---
layout: "post"
title: "Rename file referenced from assembly"
date: "2015-12-21 03:15:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/rename-file-referenced-from-assembly.html "
typepad_basename: "rename-file-referenced-from-assembly"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>There is already an article on changing the file reference to another file using the <strong>Inventor API</strong>:<br /><a href="http://adndevblog.typepad.com/manufacturing/2012/08/replace-the-file-reference-by-inventor-api.html">http://adndevblog.typepad.com/manufacturing/2012/08/replace-the-file-reference-by-inventor-api.html</a>&#0160;</p>
<p>If you want to combine this with the renaming of a referenced file then you can do it like so. It might be a good idea to keep the original file until the reference has been replaced:</p>
<pre>Sub ChangeFileNameReferencedFromAssembly()
  Dim asmDoc As AssemblyDocument
  Set asmDoc = ThisApplication.ActiveDocument
  
  Dim asmCompDef As AssemblyComponentDefinition
  Set asmCompDef = asmDoc.ComponentDefinition
  
  Dim asmOcc As ComponentOccurrence
  Set asmOcc = asmCompDef.Occurrences(1)
  
  Dim partDoc As PartDocument
  Set partDoc = asmOcc.Definition.Document
  
  Dim fileMgr As FileManager
  Set fileMgr = ThisApplication.FileManager
  
  Dim oldFileName As String
  oldFileName = partDoc.FullFileName
  
  &#39; Create new file with new file name
  Dim newFileName As String
  newFileName = oldFileName + &quot;_new.ipt&quot;
  
  Call fileMgr.CopyFile(oldFileName, newFileName)
  
  &#39; Change reference
  &#39; The file you replace the reference to needs
  &#39; to be the same file or one derived from the
  &#39; original file
  Call asmOcc.ReferencedDocumentDescriptor. _
    ReferencedFileDescriptor.ReplaceReference(newFileName)
      
  &#39; Delete the original file (if you want)
  Call fileMgr.DeleteFile(oldFileName)
End Sub</pre>
<p>&#0160;</p>
