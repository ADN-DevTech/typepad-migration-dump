---
layout: "post"
title: "PnPCommonMgd.dll missing in the Plant3D 2011 SDK"
date: "2012-05-25 10:56:55"
author: "Wayne Brill"
categories:
  - "2011"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/pnpcommonmgddll-missing-in-the-plant3d-2011-sdk.html "
typepad_basename: "pnpcommonmgddll-missing-in-the-plant3d-2011-sdk"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>I am trying to use PnPXDbDatasource as in the &quot;Data Objects Extensions&quot; example from the Plant SDK 2011 Developer Guide.&#160; I am getting errors about a missing reference to an assembly named PnPCommonMgd for the type Autodesk.ProcessPower.Common.PnPCollectionItem. I am unable to find PnPCommonMgd.dll in the Plant3D SDK where the other assemblies are located. Is this assembly available?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>PnPCommonMgd.dll is missing from the Plant3D SDK. It does however install with Plant3D P&amp;ID product and can be referenced from the install directory: </p>  <p>C:\Program Files\Autodesk\AutoCAD Plant 3D 2011 </p>
