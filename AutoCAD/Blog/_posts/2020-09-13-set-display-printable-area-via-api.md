---
layout: "post"
title: "Set &quot;Display printable area&quot; via API"
date: "2020-09-13 20:21:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2020/09/set-display-printable-area-via-api.html "
typepad_basename: "set-display-printable-area-via-api"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>When you make a new Layout a new Page Setup called *Layout1* </p><p>You see the printable area in Layout. This is a dashed rectangle. </p><p>In order to remove that rectangles you set the option via UI:<br>Go to TOOLS &gt; OPTIONS &gt; DISPLAY &gt; LAYOUT&nbsp; ELEMENTS &gt; Uncheck "Display printable area"</p><p>or to do the same in API</p><p><br></p>
<pre class="prettyprint">void turnOffPrintableArea() 
{

  AcApLayoutManager* pApLayoutMgr =
  (AcApLayoutManager*)acdbHostApplicationServices()-&gt;layoutManager();
	if (pApLayoutMgr != NULL) {
        pApLayoutMgr-&gt;setShowPaperMargins(false);
        pApLayoutMgr-&gt;updateCurrentPaper();
	}
}

</pre>
<p>Unfortunately, the interface AcApLayoutManager for application-specific routines that manipulate, access AcDbLayout objects  and controls layout related GUI attributes is not exposed to NET.</p><p><br></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4115494200d-pi"><img width="702" height="350" title="TurnOffDisplayArea" style="display: inline;" alt="TurnOffDisplayArea" src="/assets/image_433556.jpg"></a></p>
