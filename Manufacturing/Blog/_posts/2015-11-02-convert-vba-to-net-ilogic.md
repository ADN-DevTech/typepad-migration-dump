---
layout: "post"
title: "Convert VBA to .NET / iLogic"
date: "2015-11-02 05:58:39"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/convert-vba-to-net-ilogic.html "
typepad_basename: "convert-vba-to-net-ilogic"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Most of the sample code I publish is in <strong>VBA</strong> because that is by far the best place to test things out: it's part of <strong>Inventor</strong>, it has <a href="https://msdn.microsoft.com/en-us/library/hcw1s69b.aspx" target="_self">intelli-sense</a>, lets you step through code, you can place break points, use&nbsp;<strong>Watches</strong> window to check variable values, see all the objects and their functions and properties in the <strong>Object Browser</strong>, etc.&nbsp;</p>
<p>When you try to convert&nbsp;<strong>VBA</strong> code to .<strong>NET</strong> / <strong>iLogic&nbsp;</strong>then the 2 main things you need to be aware of are these. I'll show them through trying to run this <strong>VBA</strong> code from an <strong>iLogic</strong> rule:</p>
<pre>Sub MySubMethod()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  Call MsgBox(doc.DisplayName)
End Sub

Sub MyMainMethod()
  ' Do something
  
  ' Then call another method
  Call MySubMethod
End Sub</pre>
<p><strong>1) Main function needs to be the first</strong></p>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088a2f0f970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb088a2f0f970d img-responsive" title="ILogicError1" src="/assets/image_f45b09.jpg" alt="ILogicError1" /></a></p>
<p>If you have multiple methods then the first one should be the one where the whole code execution starts and should be named <strong>Sub</strong>&nbsp;<strong>Main()</strong>. In case of my sample I need to move <strong>Sub</strong>&nbsp;<strong>MyMainMethod()</strong> to the top of the code and rename it to <strong>Sub</strong>&nbsp;<strong>Main()</strong>.&nbsp;</p>
<p><strong>2) No Let/Set</strong></p>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088a2f4d970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb088a2f4d970d img-responsive" title="ILogicError2" src="/assets/image_3ac2bf.jpg" alt="ILogicError2" /></a></p>
<p>You simply have to delete the "<strong>Set</strong>" keywords from your code</p>
<p>The final <strong>iLogic</strong> code:</p>
<pre>Sub Main()
  ' Do something
  
  ' Then call another method
  Call MySubMethod
End Sub

Sub MySubMethod()
  Dim doc As Document
  doc = ThisApplication.ActiveDocument

  Call MsgBox(doc.DisplayName)
End Sub</pre>
<p>One more thing which is not necessary but I think worth changing is moving from <strong>On Error Resume Next</strong> to <strong>Try/Catch</strong>. So instead of doing this: &nbsp;</p>
<pre>On Error Resume Next
Dim doc As PartDocument
doc = ThisApplication.ActiveDocument
If Err.Number = 0 Then
  Call MsgBox(doc.DisplayName)
Else
  Call MsgBox("No active part document")
End If
On Error Goto 0</pre>
<p>... you would do this:</p>
<pre>Dim doc As PartDocument
Try
  doc = ThisApplication.ActiveDocument
  Call MsgBox(doc.DisplayName)
Catch
  Call MsgBox("No active part document")
End Try</pre>
