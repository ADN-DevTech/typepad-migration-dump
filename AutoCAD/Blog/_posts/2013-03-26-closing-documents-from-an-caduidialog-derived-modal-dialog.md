---
layout: "post"
title: "Closing documents from an CAdUiDialog derived modal dialog"
date: "2013-03-26 02:44:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/closing-documents-from-an-caduidialog-derived-modal-dialog.html "
typepad_basename: "closing-documents-from-an-caduidialog-derived-modal-dialog"
typepad_status: "Publish"
---

<p><strong>Issue     <br /></strong>I can&#39;t close documents using the closeDocument API from an CAdUiDialog derived dialog because AutoCAD reports that the documents are busy. Why does this occur?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>Make sure that you call the closeDocument API from the application context (see the executeInApplicationContext API). Once you do that, you must also enable document activation (enableDocumentActivation API). CAdUiDialog disables    <br />document activation in its DoModal override. As a result, AutoCAD cannot close documents when document activation is disabled.</p>
<p>The reason CAcUiDialog does this is that when you prompt for user input from your dialog then you re-enable the main frame thus giving a chance to the user to change the active document... CAcUiDialog tries to prevent this scenario.</p>
