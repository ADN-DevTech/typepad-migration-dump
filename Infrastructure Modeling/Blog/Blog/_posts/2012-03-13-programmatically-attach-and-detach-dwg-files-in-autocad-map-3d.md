---
layout: "post"
title: "Programmatically Attach and Detach DWG files in AutoCAD Map 3D"
date: "2012-03-13 03:35:50"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/03/programmatically-attach-and-detach-dwg-files-in-autocad-map-3d.html "
typepad_basename: "programmatically-attach-and-detach-dwg-files-in-autocad-map-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Are you looking for a way to attach or detach dwg files in AutoCAD Map 3D using .NET API?</p>
<p><a style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016302c78076970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016302c78076970d" title="MapDWGAttach" src="/assets/image_a54b8e.jpg" border="0" alt="MapDWGAttach" /></a><br /><br /></p>
<p style="text-align: justify;">If the answer is ‘YES’, you are at the right place. <strong><em>DrawingSet</em></strong> class in Map 3D .NET API has the necessary API function to attach a DWG file using DrawingSet.AttachDrawing() as well as detach a DWG file using DrawingSet.DetachDrawing(). To access the <strong><em>DrawingSet</em></strong>&nbsp;object you need to make a reference to the <strong>ManagedMapApi</strong> assembly located in the Map 3D installation folder (For Map 3D 2012 release its here - &nbsp;C:\Program Files\Autodesk\AutoCAD Map 3D 2012\ManagedMapApi.dll)</p>
<p style="text-align: justify;">Here is the code snippet which demonstrates the Attach and Detach DWG files using Map 3D .NET API functions –</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AttachDWG()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">MapApplication</span><span style="line-height: 140%;"> mapApp = </span><span style="color: #2b91af; line-height: 140%;">HostMapApplicationServices</span><span style="line-height: 140%;">.Application;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">ProjectModel</span><span style="line-height: 140%;"> activeProject = mapApp.ActiveProject;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">ProjectCollection</span><span style="line-height: 140%;"> progList = mapApp.Projects;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DrawingSet</span><span style="line-height: 140%;"> dwgSet = activeProject.DrawingSet;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// String to indicate the DWG file path</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> dwgStr = </span><span style="color: #a31515; line-height: 140%;">@"C:\Data_set\DWGs_Map\Street_Centerlines.dwg"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">AttachedDrawing</span><span style="line-height: 140%;"> dwgAttached = dwgSet.AttachDrawing(dwgStr);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; dwgAttached.Preview();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">MapException</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ed.WriteMessage(ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> DetachDWG()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">MapApplication</span><span style="line-height: 140%;"> mapApp = </span><span style="color: #2b91af; line-height: 140%;">HostMapApplicationServices</span><span style="line-height: 140%;">.Application;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">ProjectModel</span><span style="line-height: 140%;"> activeProject = mapApp.ActiveProject;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">ProjectCollection</span><span style="line-height: 140%;"> progList = mapApp.Projects;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DrawingSet</span><span style="line-height: 140%;"> dwgSet = activeProject.DrawingSet;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Check how many DWG files are attached and then Detach each of them</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (dwgSet.DirectDrawingsCount &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; dwgSet.DetachDrawing(0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"attached number: "</span><span style="line-height: 140%;"> + dwgSet.DirectDrawingsCount.ToString() + </span><span style="color: #a31515; line-height: 140%;">"\n"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">MapException</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ed.WriteMessage(ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
</div>
<p>To know more about the AutoCAD Map 3D .NET API, refer to the <strong><a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=868205">Map 3D SDK</a></strong> documents and the API samples in the SDK.</p>
<p>&nbsp;</p>
