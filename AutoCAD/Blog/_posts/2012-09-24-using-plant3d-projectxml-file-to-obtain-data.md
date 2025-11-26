---
layout: "post"
title: "Using Plant3d Project.xml file to obtain data"
date: "2012-09-24 16:46:46"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2011"
  - "2012"
  - "2013"
  - "Fenton Webb"
  - "Plant3D"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/using-plant3d-projectxml-file-to-obtain-data.html "
typepad_basename: "using-plant3d-projectxml-file-to-obtain-data"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are new to Plant3d, it’s tempting to quickly obtain project data directly from the Project.xml file.</p>  <p>The Project.xml file is an internal project file that should not really be used to extract Plant data. You should always use the API's because the XML file format can change from release to release. </p>  <p>Take, for instance, obtaining the list of P&amp;ID drawings in AutoCAD P&amp;ID. In 2011, you could</p>  <p>1) Read the Project.xml…</p>  <p><strong>&lt;ProjectPart name=&quot;PnId&quot; fileName=&quot;</strong><a href="file:///C:/Ilya/Projects/PnID/Test"><strong>C:\Fenton\Projects\PnID\Test</strong></a><strong> ISO\PnIdPart.xml&quot; relativeFileName=&quot;PnIdPart.xml&quot; uncFileName=&quot;</strong><a href="file://\\Fenton\Projects\PnID\Test"><strong>\\Fenton\Projects\PnID\Test</strong></a><strong> ISO\PnIdPart.xml&quot; /&gt;     <br /></strong></p>  <p>2) Then Use the &quot;relativeFileName&quot; value to locate the &quot;PnIdPart.xml&quot; file.   <br />3) Open the &quot;PnIdPart.xml&quot; file and search for the XML block named &quot;&lt;PnpDrawingFiles&gt;&quot;    <br />4) Finally, inside the &quot;&lt;PnpDrawingFiles&gt;&quot; block, all the project drawing files are listed. </p>  <p>However, in P&amp;ID 2013 these elements don’t exist anymore. This is because we had issues with multi-user support, so we moved the file list into the DCF. </p>  <p>The correct way to obtain a list of P&amp;ID DWG files is to use the API</p>  <p><strong>Autodesk.ProcessPower.ProjectManager.Project.GetPnPDrawingFiles() </strong></p>
