---
layout: "post"
title: "Change the Width and Height of a Sheet by Inventor API"
date: "2012-06-28 00:29:59"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/change-the-width-and-height-of-a-sheet-by-inventor-api.html "
typepad_basename: "change-the-width-and-height-of-a-sheet-by-inventor-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p>To set a sheet to a specific width and height, directly writing the width and height property of the sheet will not work if you don’t specify the size property kCustomDrawingSheetSize from DrawingSheetSizeEnum in advance. </p>  <p>Here is a VB.NET sample code demonstrating how to do that:</p>  <p> Sub setSheetSize()   <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim st As Sheet    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim dw As DrawingDocument    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; dw = Thisapplication.ActiveDocument</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160;&#160; st = dw.ActiveSheet   <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim w As Double    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; w = st.Width / 2</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160;&#160; Dim h As Double   <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; h = st.Height / 2</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160;&#160; st.Size = DrawingSheetSizeEnum.kCustomDrawingSheetSize</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160;&#160; st.Height = h   <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; st.Width = w    <br />&#160;&#160;&#160; End Sub</p>
