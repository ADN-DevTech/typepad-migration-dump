---
layout: "post"
title: "File vs Document"
date: "2014-06-24 06:56:12"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/file-vs-document.html "
typepad_basename: "file-vs-document"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Note: this information is from&#0160;<a href="https://github.com/ADN-DevTech/Inventor-Training-Material/tree/master/Module%2014%20-%20Assembly%20Advanced" target="_self" title="">https://github.com/ADN-DevTech/Inventor-Training-Material/tree/master/Module%2014%20-%20Assembly%20Advanced</a></p>
<p>The <strong>File</strong> object represents a file on disk and a <strong>File</strong> can contain multiple <strong>Documents</strong>. Each <strong>Level of Detail</strong> corresponds to a specific <strong>Document</strong> within the same <strong>File</strong>. All files have a default document so treating a <strong>Document</strong> as a file on disk will work in most cases.<br /> <strong>Document.FullFilename</strong> property returns the filename of the file the document is contained within. <strong>Document.FullDocumentName</strong> property returns a document name which is the filename concatenated with the level of detail name, e.g.</p>
<pre>C:\Temp\Assembly1.iam&lt;MyLevelOfDetail&gt;</pre>
<p>A special case is the <strong>Master</strong> level of detail. Supplying or getting back a full document name that consists of only the filename implies the <strong>“Master”</strong> level of detail:</p>
<pre>oAsmDoc = oDocuments.Open(<strong>&quot;C:\Temp\Assembly1.iam&quot;</strong>)
&#39;Is equivalent to:
oAsmDoc = oDocuments.Open(<strong>&quot;C:\Temp\Assembly1.iam&lt;Master&gt;&quot;</strong>)
</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd24712c970b-pi" style="display: inline;"><img alt="Filevsdoc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd24712c970b image-full img-responsive" src="/assets/image_0b1f13.jpg" title="Filevsdoc" /></a></p>
<p>This also explains the difference between <strong>Document.ReferencedDocumentDescriptors</strong> and <strong>Document.File.ReferencedFileDescriptors</strong>.<br />Note:&#0160;<strong>Document.ReferencedFileDescriptors&#0160;</strong>is deprecated/hidden and should not be used, instead use <strong>Document.File.ReferencedFileDescriptors</strong>.&#0160;</p>
<p>A <strong>Document</strong> may reference multiple&#0160;<strong>Documents</strong> from the same <strong>File</strong>. So e.g. in case of this Derived Part the following code would provide this result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd2472dd970b-pi" style="display: inline;"><img alt="Lods" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd2472dd970b image-full img-responsive" src="/assets/image_01f446.jpg" title="Lods" /></a></p>
<p>VBA code:</p>
<pre>Sub FilesAndDocs()
  Dim d As Document
  Set d = ThisApplication.ActiveDocument

  Debug.Print &quot;ReferencedDocumentDescriptors = &quot; + _
    str(d.ReferencedDocumentDescriptors.count)
  
  Debug.Print &quot;File.ReferencedFileDescriptors = &quot; + _
    str(d.File.ReferencedFileDescriptors.count)
End Sub</pre>
<p>Output:</p>
<pre>ReferencedDocumentDescriptors =  2
File.ReferencedFileDescriptors =  1</pre>
