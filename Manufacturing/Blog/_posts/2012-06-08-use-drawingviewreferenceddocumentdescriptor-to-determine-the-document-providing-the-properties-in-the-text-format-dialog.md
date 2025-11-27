---
layout: "post"
title: "Use DrawingView.ReferencedDocumentDescriptor to determine the Document providing the properties in the Text Format dialog"
date: "2012-06-08 07:33:21"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/use-drawingviewreferenceddocumentdescriptor-to-determine-the-document-providing-the-properties-in-the-text-format-dialog.html "
typepad_basename: "use-drawingviewreferenceddocumentdescriptor-to-determine-the-document-providing-the-properties-in-the-text-format-dialog"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>In the Format Text dialog you can select the Properties of the Model. I need&#0160; to find out programmatically which&#0160; referenced document these iProperties are coming from. I tried using the last ReferencedDocument of the drawing but this is not always the right document. Is there a way to determine the document providing these properties for the text?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The document that supplies the iProperties in the Format Text dialog is the document that is referenced by the first view on the sheet. Here is an example that shows how to get this document using the views ReferencedDocumentDescriptor:</p>
<p>&#0160;</p>
<p>In VB.Net</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">Sub</span></span><span style="color: #000000;"> test()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">If</span></span><span style="color: #000000;"> m_inventorApp.ActiveDocument.DocumentType &lt;&gt;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="font-size: 10pt;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-family: Consolas;"><span style="font-size: 10pt;"><span style="color: #000000;">DocumentTypeEnum.kDrawingDocumentObject </span></span><span><span style="color: #0000ff; font-size: 10pt;">Then</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span style="color: #0000ff; font-size: 10pt;">Exit Sub</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">End</span></span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff; font-size: 10pt;">If</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000; font-size: 10pt;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> oDrawDoc </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> DrawingDocument =</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="font-size: 10pt;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp.ActiveDocument</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> oView </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> DrawingView</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000; font-size: 10pt;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000; font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oView = oDrawDoc.ActiveSheet.DrawingViews(1)</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000; font-size: 10pt;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> oRefDocDesc2 </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> DocumentDescriptor =</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="font-size: 10pt;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oView.ReferencedDocumentDescriptor</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Diagnostics.</span></span><span style="font-size: 10pt;"><span><span style="color: #2b91af;">Debug</span></span><span style="color: #000000;">.Write(oRefDocDesc2.FullDocumentName)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">End</span></span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff; font-size: 10pt;">Sub</span></span></span></p>
</div>
