---
layout: "post"
title: "Cable & Harness or Tube & Pipe assembly?"
date: "2016-02-26 07:48:26"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/cable-harness-or-tube-pipe-assembly.html "
typepad_basename: "cable-harness-or-tube-pipe-assembly"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Add-Ins</strong> that provide extra functionality without which the document might not function properly, add a <strong>DocumentInterest</strong> object to the document.&#0160;<br />These also supposed to have the same <strong>ClientId</strong> that the <strong>Add-In</strong>&#0160;that created the&#0160;<strong>DocumentInterest&#0160;</strong>has.</p>
<p>Here is a <strong>VBA</strong> sample showing how you could get all the interests from a given document and all its children:</p>
<pre>Sub ListInterests(doc As Document)
  Debug.Print doc.FullDocumentName

  Dim aas As ApplicationAddIns
  Set aas = ThisApplication.ApplicationAddIns
  
  Dim di As DocumentInterest
  For Each di In doc.DocumentInterests
    If di.InterestType = kInterested Then
      Dim aa As ApplicationAddIn
      Set aa = aas.ItemById(di.ClientId)
      Debug.Print &quot;  &gt;&gt; Interest: &quot; _
        + di.name + &quot; by add-in &quot; + aa.DisplayName
    End If
  Next
  
  Dim rd As Document
  For Each rd In doc.ReferencedDocuments
    Call ListInterests(rd)
  Next
End Sub

Sub ListAllInterests()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  Call ListInterests(doc)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bf766c970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DocumentInterest" class="asset  asset-image at-xid-6a0167607c2431970b01bb08bf766c970d img-responsive" src="/assets/image_f67671.jpg" title="DocumentInterest" /></a></p>
<p>&#0160;</p>
