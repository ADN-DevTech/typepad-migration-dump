---
layout: "post"
title: "Prompted Entry / Prompted Texts / PromptStrings"
date: "2016-03-02 08:02:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/prompted-entry-prompted-texts-promptstrings.html "
typepad_basename: "prompted-entry-prompted-texts-promptstrings"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can&#0160;add <strong>prompted text</strong> to a&#0160;<strong>title block</strong> or <strong>border</strong>&#0160;by placing&#0160;instances of &#0160;&quot;<strong>Prompted Entry</strong>&quot; in them:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a75cfe970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PromptedEntry" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a75cfe970c img-responsive" src="/assets/image_274977.jpg" title="PromptedEntry" /></a></p>
<p>When you are inserting such a <strong>title block</strong> or <strong>border</strong> (or a new <strong>sheet</strong> with one of those) then you&#39;ll be prompted with a dialog called &quot;<strong>Prompted Texts</strong>&quot; to provide the values for them:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81d3d01970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PromptedTexts1" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81d3d01970b img-responsive" src="/assets/image_fa599d.jpg" title="PromptedTexts1" /></a></p>
<p>When creating a new<strong> sheet</strong>, adding a new <strong>title block</strong> or <strong>border</strong>, you can specify the strings that should be used for the <strong>Prompted Entry</strong>&#39;s by filling in the&#0160;<strong>TitleBlockPromptStrings</strong>/<strong>BorderPromptStrings</strong> variables in <strong>Sheets</strong>.<strong>AddUsingSheetFormat&#0160;</strong>function, or&#0160;<strong>PromptStrings</strong>&#0160;variable in <strong>Sheet</strong>.<strong>AddBorder</strong> and <strong>Sheet</strong>.<strong>AddTitleBlock&#0160;</strong>functions.<strong>&#0160;</strong></p>
<p>The<strong> Inventor API&#0160;</strong>help file has a sample for setting the <strong>Prompted Entry</strong>&#39;s inside the&#0160;<strong>Sheet</strong>.<strong>AddTitleBlock</strong>&#0160;function. You have to use the other two functions in a similar fashion.</p>
<p>If you want to do things dynamically, i.e. your function is supposed to work with multiple different <strong>title block</strong>&#0160;and <strong>border</strong>&#0160;definitions and you have to check what <strong>Prompted Entry</strong>&#39;s they contain, then <a href="http://adndevblog.typepad.com/manufacturing/2012/05/read-prompted-entry-through-code.html">this blog post</a> will help.&#0160;</p>
<p>In my drawing the active <strong>sheet</strong> has my custom <strong>title block</strong> and <strong>border</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81d5b25970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CustomSheet" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81d5b25970b img-responsive" src="/assets/image_90a177.jpg" title="CustomSheet" /></a></p>
<p>This <strong>VBA</strong> code can&#0160;create a <strong>new</strong> <strong>sheet</strong> with the same <strong>title block</strong> and <strong>border</strong>&#0160;that the <strong>active</strong> <strong>sheet</strong> has, without <strong>Inventor</strong> popping up the &quot;<strong>Prompted Texts</strong>&quot; dialog:</p>
<pre>&#39;Before running this code, add reference to
&#39; &quot;Microsoft XML, v6.0&quot; in this VBA project
Function GetPromptedEntryNames(sk As Sketch) As String()
  On Error Resume Next
  Dim names() As String
  Dim tb As TextBox
  For Each tb In sk.TextBoxes
    Dim xml As DOMDocument
    Set xml = New DOMDocument
    &#39; FormattedText might not be available on
    &#39; all TextBox, that&#39;s why using
    &#39; error handling
    Call xml.loadXML(tb.FormattedText)
        
    &#39; Look for prompted entry
    Dim node As IXMLDOMNode
    Set node = xml.selectSingleNode(&quot;Prompt&quot;)
    If Not node Is Nothing Then
      If (Not names) = -1 Then
        ReDim names(0) As String
      Else
        ReDim Preserve names(UBound(names) + 1) As String
      End If
      
      names(UBound(names)) = node.text
    End If
  Next
  On Error GoTo 0
  
  GetPromptedEntryNames = names
End Function

Sub SetValueBasedOnName(strings() As String)
  Dim i As Integer
  For i = 0 To UBound(strings)
    Dim s As String: s = strings(i)
    If s = &quot;MyBorderPrompt&quot; Or s = &quot;MyTitlePrompt&quot; Then
      strings(i) = &quot;My Value&quot;
    Else
      strings(i) = &quot;Dunno&quot;
    End If
  Next
End Sub

Sub AddNewSheet()
  Dim dwg As DrawingDocument
  Set dwg = ThisApplication.ActiveDocument
  
  Dim sh As Sheet
  Set sh = dwg.ActiveSheet
  
  &#39; Arrays for the Prompted Entry&#39;s
  Dim psTB() As String
  Dim psB() As String
  
  psTB = GetPromptedEntryNames(sh.TitleBlock.Definition.Sketch)
  psB = GetPromptedEntryNames(sh.Border.Definition.Sketch)
  
  &#39; Fill them with values based on our logic
  Call SetValueBasedOnName(psTB)
  Call SetValueBasedOnName(psB)
  
  &#39; Create a new format based on active sheet
  Dim sf As SheetFormat
  Set sf = dwg.SheetFormats.Add(sh, &quot;Mine&quot;)
  
  &#39; Create a new sheet
  Dim s As Sheet
  Set s = dwg.Sheets.AddUsingSheetFormat( _
    sf, , &quot;My sheet&quot;, , psTB, psB)
  
  &#39; If you don&#39;t want to keep the format you can delete it
  Call sf.Delete
End Sub</pre>
<p>&#0160;</p>
