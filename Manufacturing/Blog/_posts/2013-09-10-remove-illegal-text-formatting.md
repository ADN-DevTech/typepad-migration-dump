---
layout: "post"
title: "Remove illegal text formatting"
date: "2013-09-10 05:20:08"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/remove-illegal-text-formatting.html "
typepad_basename: "remove-illegal-text-formatting"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Not sure how you end up with text formatting in <strong>DrawingNote&#39;s</strong> where the <strong>Font</strong> is set to nothing, i.e. the <strong>DrawingNote.FormattedText</strong> contains things like this:</p>
<pre>&lt;StyleOverride Font=&#39;&#39;&gt;</pre>
<p>Maybe the Font type being used on the system where the drawing was created does not exist on the other system where the drawing has been opened?&#0160;</p>
<p>Unfortunately, this seems to cause issues when exporting your drawing to other formats like PDF or DWF and you will get a message like the following:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff4ca780970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Fontformatting2" class="asset  asset-image at-xid-6a0167607c2431970b019aff4ca780970b" src="/assets/image_d90fd3.jpg" title="Fontformatting2" /></a></p>
<p>... and the exported document might contain illegible text:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff4d1d33970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Fontformatting" class="asset  asset-image at-xid-6a0167607c2431970b019aff4d1d33970d" src="/assets/image_ef3816.jpg" title="Fontformatting" /></a></p>
<p>If you know the exact problem you are looking for - in our case <strong>Font=&#39;&#39;</strong> - then you could simply remove these from the <strong>FormattedText</strong> of the notes. In case of VBA you could use the <strong>Microsoft VBScript Regular Expressions</strong> COM library. If you are using .NET or C++ there would be other options.</p>
<pre>&#39; Using &#39;Microsoft VBScript Regular Expressions 5.5&#39; COM library
&#39; C:\Windows\System32\vbscript.dll\3
Public Sub RemoveInvalidNoteFormatting(ByRef note As DrawingNote)
    &#39; Remove bad formatting in tags like &lt;StyleOverride Font=&#39;&#39;&gt;
    &#39; where Font is set to nothing &#39;&#39;
    Dim r As New RegExp
    r.Global = True
    r.IgnoreCase = True
    r.Pattern = &quot;Font=&#39;&#39;&quot;
    
    note.formattedText = r.Replace(note.formattedText, &quot;&quot;)
End Sub

Public Sub RemoveInvalidFormatting()
    Dim doc As DrawingDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim sht As Sheet
    For Each sht In doc.Sheets
        Dim note As DrawingNote
        For Each note In sht.DrawingNotes
            Call RemoveInvalidNoteFormatting(note)
        Next
    Next
End Sub</pre>
<p>Once I&#39;ve run the above code on the drawing, I could export it without any problem.</p>
