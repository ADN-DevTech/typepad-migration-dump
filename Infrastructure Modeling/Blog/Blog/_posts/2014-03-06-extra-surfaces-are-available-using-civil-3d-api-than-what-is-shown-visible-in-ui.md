---
layout: "post"
title: "Extra Surfaces are available using Civil 3D API than what is shown / visible in UI"
date: "2014-03-06 01:32:24"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/03/extra-surfaces-are-available-using-civil-3d-api-than-what-is-shown-visible-in-ui.html "
typepad_basename: "extra-surfaces-are-available-using-civil-3d-api-than-what-is-shown-visible-in-ui"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Have you come across a situation where in you get more number of Civil 3D Surface objects using <a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">API</a> than what is shown / visible in AutoCAD Civil 3D User Interface (<a class="zem_slink" href="http://en.wikipedia.org/wiki/User_interface" rel="wikipedia" target="_blank" title="User interface">UI</a>) in Toolspace -&gt;Prospector Tab ?</p>
<p>I have come across few such DWG files where-in I got more number of Surfaces using API than what is shown in Civil 3D UI Toolspace. Interestingly all those files came to me from external sources and I have no idea how those DWG files were created.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d88ba97970d-pi" style="display: inline;"><img alt="Extra_Surfaces_In_API" class="asset  asset-image at-xid-6a0167607c2431970b01a73d88ba97970d img-responsive" src="/assets/image_230b09.jpg" title="Extra_Surfaces_In_API" /></a></p>
<p><br />&#0160;</p>
<p>AutoCAD Civil 3D UI only iterate and shows all the Surfaces in the layouts. However in the current API implementation, we find all the surfaces in the drawing, including all the blocks and that&#39;s why we see more number of Surfaces using API.</p>
<p>So, how do you get the actual surfaces those are visible in Civil 3D UI Toolspace ?</p>
<p>Here is the relevant code snippet that demonstrates how you can get the Surfaces which are available / shown in UI Toolspace :&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> civilDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> surfaceIds = civilDoc.GetSurfaceIds();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> surfaceVisibleinUI= </span><span style="color: blue; line-height: 140%;">new</span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// iterate through the list of all the surface objects</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceId </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> surfaceIds)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Autodesk.Civil.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Surface</span><span style="line-height: 140%;"> surface = surfaceId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> Autodesk.Civil.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Surface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> record = surface.OwnerId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (<span style="background-color: #ffff00;"><strong>record.IsLayout</strong></span>)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; surfaceVisibleinUI.Add(surfaceId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nTotal Number of Surfaces available in this DWG file&#0160; : &quot;</span><span style="line-height: 140%;"> + surfaceIds.Count.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n ------------------------------------------------------------- \n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nTotal Number of Surfaces Visible in UI&#0160; : &quot;</span><span style="line-height: 140%;"> + surfaceVisibleinUI.Count.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}&#0160; &#0160; &#0160;&#0160; </span></p>
</div>
