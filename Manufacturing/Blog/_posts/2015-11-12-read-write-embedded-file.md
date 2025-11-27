---
layout: "post"
title: "Read / Write embedded file"
date: "2015-11-12 11:11:41"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/read-write-embedded-file.html "
typepad_basename: "read-write-embedded-file"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When <a href="https://forums.autodesk.com/t5/inventor-customization/how-do-i-programmatically-access-an-embedded-excel-document-from/td-p/3379409" target="_self">reading e.g. an embedded <strong>Excel</strong> file</a> then you can simply use the <strong>Excel API</strong> to interact with the document. However, if it&#39;s e.g. a text file then the default text editor application will be brought up (usually <strong>Notepad</strong>) and you have no programmatic access to the text file content.</p>
<p><strong>1)</strong> The easiest solution would probably be to use a linked text file instead of an embedded one, but it might not be an option.</p>
<p><strong>2)</strong> Since <strong>Inventor</strong> documents are using <strong>Structured Storage</strong> you might be able to use that <strong>API</strong> to access the text file. I found <a href="http://stackoverflow.com/questions/8981118/how-can-i-save-the-contents-of-an-oleobject-as-a-file-in-an-automated-way" target="_self">this</a> info which could be useful for this. There is also a cool program that enables you to see what&#39;s inside the documents called <strong><a href="http://www.mitec.cz/ssv.html" target="_self">Structured Storage Viewer</a>&#0160;</strong>and this is what a document would look like in it - highlighted in red is the content of the embedded text file:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088fc599970d-pi" style="display: inline;"><img alt="StructuredStorageViewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb088fc599970d image-full img-responsive" src="/assets/image_c7b582.jpg" title="StructuredStorageViewer" /></a></p>
<p>One issue I found though is that the embedded object&#39;s name that you find in the <strong>Inventor</strong>&#0160;Model Browser might not be in sync with the name used inside the <strong>Structured Storage</strong>.</p>
<p>We also have <a href="http://adndevblog.typepad.com/manufacturing/2014/04/save-extra-data-in-inventor-file-without-inventor-api.html" target="_self">this</a> article on using<strong> Structured Storage</strong> to store information in <strong>Inventor</strong> documents, which could also come handy.</p>
<p>A colleague also pointed out this article about using this <strong>API</strong> from <strong>.NET</strong>:&#0160;<a href="http://www.developerfusion.com/article/84406/com-structured-storage-from-net/" target="_self" title="">http://www.developerfusion.com/article/84406/com-structured-storage-from-net/</a>&#0160;and this project:&#0160;<a href="http://sourceforge.net/projects/openmcdf/" target="_self" title="">http://sourceforge.net/projects/openmcdf/</a></p>
<p><strong>3)</strong> Another option is to hook into the edit mechanism of the embedded file. We&#39;ll register our program in the registry as the editor for <strong>txt</strong> files so that will be called with the path of the temporary <strong>txt</strong> file that <strong>Inventor</strong> creates when the document with the embedded text file is opened. Then we edit the file, and when our program exits the modified <strong>txt</strong> file content will be stored in the <strong>Inventor</strong> document. I checked and if I modified the <strong>txt</strong> file after my app closed, then those changes did not get saved into the <strong>Inventor</strong> document.</p>
<p>For this solution I created a sample project that can be used to modify an embedded text file programmatically:&#0160;<a href="https://github.com/adamenagy/Inventor-ReadWriteEmbeddedFile" target="_self" title="">https://github.com/adamenagy/Inventor-ReadWriteEmbeddedFile</a></p>
<p>Once the program is compiled you can use it like this from <strong>VBA</strong>:</p>
<pre>Sub ModifyEmbeddedTextFile()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument

  &#39; Before running this code the embedded text file
  &#39; needs to be selected in the Inventor Model Browser 
  Dim obj As ReferencedOLEFileDescriptor
  Set obj = doc.SelectSet(1)

  &#39; Register our reader for text files
  Call Shell( _
    &quot;&quot;&quot;&quot; &amp; _
    &quot;C:\Temp\TextReaderCS.exe&quot; &amp; _
    &quot;&quot;&quot; &quot;&quot;&quot; &amp; _
    &quot;/r&quot; &amp; _
    &quot;&quot;&quot;&quot;, vbNormalFocus)

  &#39; Get the embedded object opened
  &#39; Note: this will use the temporary file with
  &#39; the content of the embedded object
  &#39; the path of which will be passed to the editor
  &#39; program
  Call obj.Activate(kShowOLEVerb, Nothing)

  &#39; Unregister our editor for text files
  Call Shell( _
    &quot;&quot;&quot;&quot; &amp; _
    &quot;C:\Temp\TextReaderCS.exe&quot; &amp; _
    &quot;&quot;&quot; &quot;&quot;&quot; &amp; _
    &quot;/u&quot; &amp; _
    &quot;&quot;&quot;&quot;, vbNormalFocus)
End Sub</pre>
<p><strong>4)</strong> If you just want to read the file then maybe you could just have a File System monitor checking any new file creation in the temp folder, so when <strong>Inventor</strong> creates the temp files for the embedded files during the opening of the <strong>Inventor</strong> document then you could be notified about that and could store their paths and read them if needed: <a href="https://msdn.microsoft.com/en-us/library/system.io.filesystemwatcher(v=vs.110).aspx" target="_self"><strong>System.IO.FileSystemWatcher</strong></a></p>
<p>If you have multiple embedded documents then it might not be easy to figure out which temp file is for which one.</p>
