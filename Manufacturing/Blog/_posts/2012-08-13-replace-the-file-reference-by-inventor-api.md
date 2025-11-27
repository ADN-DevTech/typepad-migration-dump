---
layout: "post"
title: "Replace the File Reference by Inventor API"
date: "2012-08-13 20:19:51"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/replace-the-file-reference-by-inventor-api.html "
typepad_basename: "replace-the-file-reference-by-inventor-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p>Before Inventor 10 (including) the ReferenceFileDescriptor.PutCustomLogicalUsingFull was the method used to replace the file reference and this method was only allowed in Apprentice on non-migrating data sets. </p>  <p>But after Inventor 10, there is a new object called file object is introduced, where each File object will contain one or more documents. Now document is not the same as file anymore where the documents are still represented by the various document objects. So, hereafter instead of using ReferenceFileDescriptor.PutLogicalFilenameUsingFull we need to use FileDescriptor.ReplaceReference. </p>  <p><em>Significantly, FileDescriptor.ReplaceReference is available inside Inventor as well as in Apprentice.</em></p>  <p>Following is a part of VBA code which shows how to use the FileDescriptor.ReplaceReference with apprentice API:</p>  <pre><p><font size="1">Dim oApprentice As ApprenticeServerComponent
</font></p><p><font size="1">Set oApprentice = New ApprenticeServerComponent</font>
</p><p><font size="1">Dim oADoc As ApprenticeServerDocument
</font></p><p><font size="1">Set oADoc = oApprentice.Open(&quot;C:\Assembly1.iam&quot;)</font>
</p><p><font size="1">Dim oFD As FileDescriptor
</font></p><p><font size="1">Set oFD = oADoc.File.ReferencedFileDescriptors(1)</font>
</p><p><font size="1">Call oFD.ReplaceReference(&quot;C:\Part2.ipt&quot;)</font></p>
<p><font size="1">Call oApprentice.FileSaveAs.AddFileToSave(oADoc, oADoc.FullFileName)
</font></p><p><font size="1">Call oApprentice.FileSaveAs.ExecuteSave</font>
</p><p>&#160;</p></pre>

<p>And this is a part of VBA code that does the same with Inventor API:</p>

<p><font size="1" face="Courier New">Dim oDoc As Inventor.DrawingDocument 
    <br />Set oDoc = ThisApplication.Documents.Open(&quot;C:\Assembly1.iam&quot;) </font></p>

<p><font size="1" face="Courier New">oDoc.File.ReferencedFileDescriptors(1).ReplaceReference(&quot;C:\Part2.ipt&quot;)</font></p>

<p>Note: For a file to be valid as a replacement it must have the same heritage as the file being replaced. You cannot replace a file with a brand new file. It has to be copied or modified from the already existing file - they must have shared the same ancestor in their past.</p>
