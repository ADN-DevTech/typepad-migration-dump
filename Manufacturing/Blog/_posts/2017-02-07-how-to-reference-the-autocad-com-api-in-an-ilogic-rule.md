---
layout: "post"
title: "How to reference the AutoCAD COM API in an iLogic rule"
date: "2017-02-07 10:06:24"
author: "Wayne Brill"
categories:
  - "iLogic"
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/02/how-to-reference-the-autocad-com-api-in-an-ilogic-rule.html "
typepad_basename: "how-to-reference-the-autocad-com-api-in-an-ilogic-rule"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p> <p>You can use AddReference to reference the AutoCAD COM Interops. Below is an iLogic rule to test this. (It just Dims a couple of types) iLogic needs to be able to find the AutoCAD Interop dlls. The path where it looks for the interop dlls is set in the Advanced iLogic Configuration. (tools tab Options). You can copy Autodesk.AutoCAD.Interop.dll and Autodesk.AutoCAD.Interop.Common.dll to the “iLogic AddIn DLLs directory” to avoid the error with the AddReference line of code. This screenshot shows the path where iLogic will look for the interop DLLs.&nbsp; </p> <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d25d904b970c-pi"><img title="iLogic_Configuration_WB" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="iLogic_Configuration_WB" src="/assets/image_e7e77b.jpg" width="431" height="453"></a></p> <p>&nbsp; <p>Here is the iLogic rule that references the AutoCAD Interops. It shows declaring types from the AutoCAD COM API. AcadBlockReference and AcadCircle. It also creates or gets a running session of AutoCAD. <pre>AddReference "Autodesk.AutoCAD.Interop"
AddReference "Autodesk.AutoCAD.Interop.Common"

Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.Interop

Sub Main
Dim oAcadApp As AcadApplication
oAcadApp = CreateObject("AutoCAD.Application")
'If AutoCAD is already running use GetObject
'oAcadApp = GetObject(,"AutoCAD.Application")
oAcadApp.Visible = True

Dim myBlockRef As AcadBlockReference
Dim oAcadCircl as AcadCircle

MessageBox.Show(oAcadApp.Caption, "Title")

End Sub</pre>
