---
layout: "post"
title: "Use AutoCAD API in DWG file"
date: "2015-05-12 03:16:04"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/use-autocad-api-in-dwg-file.html "
typepad_basename: "use-autocad-api-in-dwg-file"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When interacting with a <strong>DWG</strong> type drawing document, you can also use the <strong>AutoCAD</strong> <strong>API</strong> in it through the <strong>ObjectDBX</strong> type library.</p>
<p>From the <strong>DrawingDocument</strong>.<strong>ContainingDWGDocument</strong> you&#39;ll get back an <strong>AcadDatabase</strong> object that you can use to drill down into the drawing&#39;s contents.</p>
<p>As an example for showing how to achieve something using the <strong>Inventor API</strong> vs the <strong>AutoCAD API</strong>, here are two functions which are supposed to be doing the same thing: iterating through the <strong>Sheets</strong> (<strong>Layouts</strong>&#0160;in AutoCAD) and set the <strong>Prompt Text</strong> values (<strong>Attribute Text</strong> in AutoCAD) for a given <strong>AutoCADBlock</strong> (<strong>Block Reference</strong> in AutoCAD).</p>
<p><strong>Inventor</strong> code:</p>
<pre>Sub SetAttributesOnSheets()

  Dim oDrawDoc As DrawingDocument
  Set oDrawDoc = ThisApplication.ActiveDocument
    
  Dim Blockname As String
  Blockname = &quot;Testblock&quot;

  Dim oSheet As Sheet
  For Each oSheet In oDrawDoc.Sheets
    Dim oBlock As AutoCADBlock
    For Each oBlock In oSheet.AutoCADBlocks
      If oBlock.Name = Blockname Then
        Dim tagStrings() As String
        Dim textStrings() As String
    
        oBlock.GetPromptTextValues tagStrings, textStrings
    
        Dim i As Integer
        For i = LBound(tagStrings) To UBound(tagStrings)
          If tagStrings(i) = &quot;TEXT1&quot; Then
            textStrings(i) = &quot;text1text&quot;
          ElseIf tagStrings(i) = &quot;TEXT2&quot; Then
            textStrings(i) = &quot;text2text&quot;
          End If
        Next
    
        &#39; If multiple tag strings are the same then it will
        &#39; only set the value for the first one of them
        &#39; That&#39;s one of the reasons for this article
        &#39; to show how to do it using the AutoCAD API instead
        &#39; which seems to be working fine and can be used as
        &#39; a workaround
        oBlock.SetPromptTextValues tagStrings, textStrings
      End If
    Next
  Next
End Sub</pre>
<p><strong>AutoCAD</strong>/<strong>ObjectDBX</strong> code:&#0160;</p>
<pre>Sub SetAttributesOnLayouts()

  Dim oDrawDoc As DrawingDocument
  Set oDrawDoc = ThisApplication.ActiveDocument
   
  Dim Blockname As String
  Blockname = &quot;Testblock&quot;
    
  &#39; Using AutoCAD API
  &#39; If you want to declare variables using their
  &#39; exact type, which will provide you with intelli-sense
  &#39; as well, then you need to reference the
  &#39; &quot;AutoCAD/ObjectDBX Common  Type Library&quot;
  &#39; through &quot;Tools &gt;&gt; References&quot;
  &#39; You can also declare the variables as &quot;Object&quot; instead, in
  &#39; which case you are doing so called &quot;late-binding&quot;
  &#39; which does not require you to reference the type library:
  &#39; e.g.: Dim oDb As Object
  Dim oDb As AcadDatabase
  Set oDb = oDrawDoc.ContainingDWGDocument
  
  &#39; This way we can set the attribute values even inside 
  &#39; the model space layout which does not seem to be 
  &#39; possible even through the Inventor User Interface 
  Dim oLayout As AcadLayout
  For Each oLayout In oDb.Layouts
    Dim oBlock As AcadBlock
    Set oBlock = oLayout.Block
  
    Dim oEnt As AcadEntity
    For Each oEnt In oBlock
      If TypeOf oEnt Is AcadBlockReference Then
        Dim oBR As AcadBlockReference
        Set oBR = oEnt
      
        If oBR.Name = Blockname Then
          Dim oAtts As Variant
          oAtts = oBR.GetAttributes()
        
          Dim i As Integer
          For i = LBound(oAtts) To UBound(oAtts)
            Dim oRef As AcadAttributeReference
            Set oRef = oAtts(i)
          
            If oRef.TagString = &quot;TEXT1&quot; Then
              oRef.TextString = &quot;text1text&quot;
            ElseIf oRef.TagString = &quot;TEXT2&quot; Then
              oRef.TextString = &quot;text2text&quot;
            End If
          Next
        End If
      End If
    Next
  Next
  
  Call oDrawDoc.Update
End Sub</pre>
<p>Result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11283d8970c-pi" style="display: inline;"><img alt="Attributes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11283d8970c image-full img-responsive" src="/assets/image_8e72ab.jpg" title="Attributes" /></a></p>
<p>&#0160;</p>
