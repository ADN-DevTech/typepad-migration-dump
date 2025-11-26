---
layout: "post"
title: "Changing the DWG Title, DWG Number, etc for a Plant3d/P&amp;ID Drawing"
date: "2013-01-22 11:21:50"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "Fenton Webb"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/changing-the-dwg-title-dwg-number-etc-for-a-plant3dpid-drawing.html "
typepad_basename: "changing-the-dwg-title-dwg-number-etc-for-a-plant3dpid-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>I had a question come up about how to set the DWG Title, DWG Number, etc when adding new files to the project via the API. </p>  <p>You can add the files using:</p>  <p><strong>Autodesk.ProcessPower.ProjectManager.Project.AddPnPDrawingFile()</strong></p>  <p>once added, you can utilize these functions to do what you want with the properties…</p>  <p><strong>Autodesk.ProcessPower.ProjectManager.Project.GetDrawingPropertyValue()Autodesk.ProcessPower.ProjectManager.Project.SetDrawingPropertyValue()</strong></p> <strong>   <p><strong>Autodesk.ProcessPower.ProjectManager.Project.GetDrawingProperties()</strong></p>   <strong>Autodesk.ProcessPower.ProjectManager.Project.SetDrawingProperties()</strong></strong>  <p>then call this function to commit the changes…</p>  <p><strong>Autodesk.ProcessPower.ProjectManager.Project.AcceptDrawingProperties()</strong></p>
