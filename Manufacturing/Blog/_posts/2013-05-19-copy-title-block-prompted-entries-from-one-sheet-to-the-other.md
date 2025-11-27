---
layout: "post"
title: "Copy title block prompted entries from one sheet to the other"
date: "2013-05-19 20:57:00"
author: "Xiaodong Liang"
categories:
  - "iLogic"
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/copy-title-block-prompted-entries-from-one-sheet-to-the-other.html "
typepad_basename: "copy-title-block-prompted-entries-from-one-sheet-to-the-other"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>:    <br />The title blocks that we use have prompted entries. I would like to find a way using iLogic to copy the entries from one title block to another within the same drawing.&#160; </p>  <p><strong>Solution</strong>:    <br />In API, every title block has the corresponding title block definition which defines the prompt text. When the end user tries to insert a title block, the mechanism behind is inserting an instance of the title block definition. The user will be asked to input the final text for the prompt text.&#160; the API object TitleBlock provides the method GetResultText to know the final text. Conversely, if you want to set the final text, you can call TitleBlock.SetPromptResultText. </p>  <p>Following iLogic code assumes the drawing has two sheets which use the same title block. In the title block, there is one prompt text named &quot;MY_PROMPT&quot;. It will get the result string in sheet1 and copies to sheet2.</p>  <pre style="border-bottom: rgb(218,218,218) 1px dotted; text-align: left; border-left: rgb(218,218,218) 1px dotted; padding-bottom: 5px; line-height: 14px; overflow-x: scroll; text-transform: none; background-color: rgb(247,247,247); font-variant: normal; font-style: normal; text-indent: 0px; margin: 0px 10px 10px; padding-left: 5px; width: 486px; padding-right: 5px; white-space: pre-wrap; letter-spacing: normal; height: 672px; color: rgb(35,35,35); font-size: 10px; border-top: rgb(218,218,218) 1px dotted; font-weight: normal; border-right: rgb(218,218,218) 1px dotted; word-spacing: 0px; padding-top: 5px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px"> ' assume sheet1 and sheet use the same title block definition
    
    Dim oDoc  
      oDoc = ThisApplication.ActiveDocument
    
    'get first sheet
    Dim oSheet1  
      oSheet1 = oDoc.Sheets(1)
    
    'get titleblock of sheet1
    Dim oTB1  
      oTB1 = oSheet1.TitleBlock
    
    ' search the textbox in definition
    
    Dim oPromptText  
    Dim oEachText 
    Dim I 
    For I = 1 To oTB1.Definition.Sketch.TextBoxes.Count
          oEachText = oTB1.Definition.Sketch.TextBoxes(I)
        If (oEachText.Text = &quot;MY_PROMPT&quot;) Then
            ' found the prompt text we want to copy
              oPromptText = oEachText
            Exit For
        End If
    Next I
    
    'get the result string of the prompt text
    Dim oPromptEntry  
    oPromptEntry = oTB1.GetResultText(oPromptText)
    
    'get sheet2
    Dim oSheet2 As Sheet
      oSheet2 = oDoc.Sheets(2)
    
      'get titleblock of sheet2
    Dim oTB2 As TitleBlock
      oTB2 = oSheet2.TitleBlock
    
    ' copy the result string of the prompt text to the prompt text in sheet2
      oTB2.SetPromptResultText(oEachText, oPromptEntry)</pre>
