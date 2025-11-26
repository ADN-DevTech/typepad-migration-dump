---
layout: "post"
title: "Identify view change in AutoCAD"
date: "2016-05-05 03:35:38"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "2016"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/05/identify-view-change-in-autocad.html "
typepad_basename: "identify-view-change-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>I have received an&#0160;query from an ADN partner recently on identifying the view change in AutoCAD, like when user pan or zoom using mouse. <br />To identify the view change developers can use the editor rector viewChanged() (AcEditorReactor:: viewChanged()) in ObjectARX . However, in AutoCAD.NET API, the equivalent API is provided in document class Document::ViewChanged</p>
<pre>[CommandMethod(&quot;ViewChnage&quot;)]
public void ViewChnage()
{
    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
    doc.ViewChanged += doc_ViewChanged;
}

void doc_ViewChanged(object sender, EventArgs e)
{
    //
} 
</pre>
