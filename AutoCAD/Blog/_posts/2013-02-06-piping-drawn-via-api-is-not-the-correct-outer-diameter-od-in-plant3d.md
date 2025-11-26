---
layout: "post"
title: "Piping Drawn via API is not the correct Outer Diameter (OD) in Plant3d"
date: "2013-02-06 15:16:22"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "Fenton Webb"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/piping-drawn-via-api-is-not-the-correct-outer-diameter-od-in-plant3d.html "
typepad_basename: "piping-drawn-via-api-is-not-the-correct-outer-diameter-od-in-plant3d"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><strong>Issue</strong></p>  <p>Use the sample project CreatePipeline from the <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=15460551">Plant 3D 2013 SDK</a> to draw piping in a model. It draws a 6&quot; CS300 line. Next, put in a piece of pipe using the buttons on ribbon.     <br /></p>  <p>The pipe drawn through Plant 3D routing is the correct OD. However, the same Pipe drawn using the SDK is the nominal diameter. What is the problem here?</p>  <p><strong>Solution</strong></p>  <p>The managed Pipe entity has a property OuterDiameter that calls into the unmanaged C++ <strong>AcPpDb3dPipe::GetOuterDiameter() / <strong>AcPpDb3dPipe::SetOuterDiameter()</strong></strong>. You have to set this value using the Pipe’s SpecPart and MatchingPipeOD property. </p>  <p>For example:</p>  <p><strong>pipeEntity.OuterDiameter = pipeSpecPart.PropValue(“MatchingPipeOD”); </strong></p>
