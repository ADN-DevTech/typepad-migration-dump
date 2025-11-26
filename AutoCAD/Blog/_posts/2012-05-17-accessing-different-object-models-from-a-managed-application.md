---
layout: "post"
title: "Accessing different object models from a managed application"
date: "2012-05-17 14:36:06"
author: "Philippe Leefsma"
categories:
  - ".NET"
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/accessing-different-object-models-from-a-managed-application.html "
typepad_basename: "accessing-different-object-models-from-a-managed-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>Can I access C++ or COM object models from a .NET managed application?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>Yes, you can use 3 object models simultaneously:&#160; You can use the new .NET object model of AutoCAD, the COM object model (via COM interop) or the C++ object model (via P/Invoke). The attached sample shows how.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:246d67ef-0f37-4504-aede-fd42ac63a720" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/mgdarx-1.zip" target="_blank">MgdArx.zip</a></p></div>
