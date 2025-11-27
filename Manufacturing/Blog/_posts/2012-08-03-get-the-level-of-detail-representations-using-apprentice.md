---
layout: "post"
title: "Get the Level Of Detail Representations Using Apprentice"
date: "2012-08-03 02:45:50"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/get-the-level-of-detail-representations-using-apprentice.html "
typepad_basename: "get-the-level-of-detail-representations-using-apprentice"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p><strong>Issue</strong>    <br />I would like to know the Level Of Detail Representations before opening the assembly document. How could I do it using Apprentice?</p>  <p><strong>Solution</strong>    <br />You can use the RepresentationsManager from Apprentice, and all the read functions for objects under LevelOfDetailRepresentations should work fine. However, you can query all the available LevelOfDetailRepresentations using the FileManager as well, so you do not even need to open the document.    <br />Below demonstrates two approaches:</p>  <p>1. Use FileManager:   <br /><font size="1" face="Courier New">Dim oASC As New ApprenticeServerComponent     <br />&#160; <br />' get the LODs without opening the document      <br />Dim oDVRs As Variant      <br />oDVRs = oASC.FileManager.GetLevelOfDetailRepresentations(&quot;C:\assy.iam&quot;)      <br />&#160; <br />Dim i As Integer      <br />Debug.Print &quot;Using FileManager&quot;      <br />For i = LBound(oDVRs) To UBound(oDVRs)      <br />&#160;&#160;&#160; Debug.Print oDVRs(i)      <br />Next</font></p>  <p>2. Open file and use RepresentationsManager:&#160; <br /><font size="1" face="Courier New">' open the document      <br />Dim oASD As ApprenticeServerDocument      <br />Set oASD = oASC.Open(&quot;C:\assy.iam&quot;)      <br />&#160;</font></p>  <p><font size="1" face="Courier New">Dim oACD As AssemblyComponentDefinition     <br />Set oACD = oASD.ComponentDefinition      <br />&#160; <br />Dim oRM As RepresentationsManager      <br />Set oRM = oACD.RepresentationsManager      <br />&#160; <br />Dim oLOD As LevelOfDetailRepresentation      <br />Debug.Print &quot;Using RepresentationsManager&quot;      <br />For Each oLOD In oRM.LevelOfDetailRepresentations      <br />&#160;&#160;&#160; Debug.Print oLOD.Name      <br />Next</font></p>
