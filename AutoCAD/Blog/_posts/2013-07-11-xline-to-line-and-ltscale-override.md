---
layout: "post"
title: "Xline to Line and LTSCALE override"
date: "2013-07-11 08:53:01"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/xline-to-line-and-ltscale-override.html "
typepad_basename: "xline-to-line-and-ltscale-override"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Just a couple of utility functions here. </p>
<p>One converts all the Construction Lines (AcadXline/Xline/AcDbXline - in case someone is searching for a different name :)) to Line entities (AcadLine/Line/AcDbLine). You may need this if you have xline&#39;s in a block and you would like to snap to them outside the block. It does not seem possible, whereas line&#39;s are snappable even outside the block:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">&#39; Application Session Command with localized name</span></p>
<p style="margin: 0px; line-height: 120%;">&lt;<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;AcmUtilityConvertXlines&quot;</span>)&gt; _</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> AcmUtilConvertXlines() <span style="color: green;">&#39; This method can have any name</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Dim</span> doc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span> = <span style="color: #2b91af;">acApp</span>.DocumentManager.MdiActiveDocument</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Dim</span> db <span style="color: blue;">As</span> <span style="color: #2b91af;">Database</span> = doc.Database</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Using</span> tr <span style="color: blue;">As</span> <span style="color: #2b91af;">Transaction</span> = db.TransactionManager.StartTransaction()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; Just something crude to start with</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> length <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; length = db.Extmin.DistanceTo(db.Extmax) * 2</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> bt <span style="color: blue;">As</span> <span style="color: #2b91af;">BlockTable</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; bt = tr.GetObject(db.BlockTableId, <span style="color: #2b91af;">OpenMode</span>.ForRead)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> blockId <span style="color: blue;">In</span> bt</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> block <span style="color: blue;">As</span> <span style="color: #2b91af;">BlockTableRecord</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; block = tr.GetObject(blockId, <span style="color: #2b91af;">OpenMode</span>.ForRead)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> modified <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span> = <span style="color: blue;">False</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> entId <span style="color: blue;">In</span> block</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> entId.ObjectClass = <span style="color: #2b91af;">Xline</span>.GetClass(<span style="color: blue;">GetType</span>(<span style="color: #2b91af;">Xline</span>)) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> modified <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; block.UpgradeOpen()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; modified = <span style="color: blue;">True</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> xl <span style="color: blue;">As</span> <span style="color: #2b91af;">Xline</span> = tr.GetObject(entId, <span style="color: #2b91af;">OpenMode</span>.ForWrite)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Using</span> l <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">Line</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; l.StartPoint = xl.BasePoint.Add(xl.UnitDir.MultiplyBy(-length))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; l.EndPoint = xl.BasePoint.Add(xl.UnitDir.MultiplyBy(length))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; l.Layer = xl.Layer</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; l.Color = xl.Color</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; block.AppendEntity(l)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(l, <span style="color: blue;">True</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; xl.Erase()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; If the block got modified then let&#39;s update its references</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">If</span> modified <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> brId <span style="color: blue;">As</span> <span style="color: #2b91af;">ObjectId</span> <span style="color: blue;">In</span> block.GetBlockReferenceIds(<span style="color: blue;">True</span>, <span style="color: blue;">True</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> br <span style="color: blue;">As</span> <span style="color: #2b91af;">BlockReference</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; br = tr.GetObject(brId, <span style="color: #2b91af;">OpenMode</span>.ForWrite)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; br.RecordGraphicsModified(<span style="color: blue;">True</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; tr.Commit()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>The &#0160;other overrides the LTSCALE value before using the HATCH command and then sets it back afterwards. You may need this if you have dashed lines closing an area you want to hatch - the hatch preview looks OK but the created hatch might extend beyond the selected area. This can be prevented by using a smaller LTSCALE value:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Shared</span> ltscale <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">&#39; Application Session Command with localized name</span></p>
<p style="margin: 0px; line-height: 120%;">&lt;<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;AcmUtilityHatch&quot;</span>)&gt; _</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> AcmUtilHatch() <span style="color: green;">&#39; This method can have any name</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Dim</span> doc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span> = <span style="color: #2b91af;">acApp</span>.DocumentManager.MdiActiveDocument</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; ltscale = doc.Database.Ltscale</p>
<p style="margin: 0px; line-height: 120%;">&#0160; doc.Database.Ltscale = 0.01</p>
<p style="margin: 0px; line-height: 120%;">&#0160; doc.Editor.Regen()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; doc.SendStringToExecute(<span style="color: #a31515;">&quot;_.HATCH &quot;</span>, <span style="color: blue;">False</span>, <span style="color: blue;">False</span>, <span style="color: blue;">False</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">AddHandler</span> doc.CommandEnded, <span style="color: blue;">AddressOf</span> doc_CommandEnded</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">AddHandler</span> doc.CommandCancelled, <span style="color: blue;">AddressOf</span> doc_CommandEnded</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">AddHandler</span> doc.CommandFailed, <span style="color: blue;">AddressOf</span> doc_CommandEnded</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> doc_CommandEnded(sender <span style="color: blue;">As</span> <span style="color: blue;">Object</span>, e <span style="color: blue;">As</span> Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">CommandEventArgs</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">If</span> e.GlobalCommandName = <span style="color: #a31515;">&quot;HATCH&quot;</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">Dim</span> doc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span> = sender</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">RemoveHandler</span> doc.CommandEnded, <span style="color: blue;">AddressOf</span> doc_CommandEnded</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">RemoveHandler</span> doc.CommandCancelled, <span style="color: blue;">AddressOf</span> doc_CommandEnded</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">RemoveHandler</span> doc.CommandFailed, <span style="color: blue;">AddressOf</span> doc_CommandEnded</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; doc.Database.Ltscale = ltscale</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; doc.Editor.Regen()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>There might be nicer solutions to the problem. This is just a quick solution:&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b01901e3813c8970b"><a href="http://adndevblog.typepad.com/files/acmutility.zip">Download AcmUtility</a></span><br />The attachment also contains the compiled project for AutoCAD 2013/2014 and the bundle that can be simply placed in the appropriate folder on the system so that it will be loaded automatically into AutoCAD:&#0160;<a href="http://docs.autodesk.com/ACD/2013/ENU/index.html?url=files/GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008.htm,topicNumber=d30e503195" target="_self">http://docs.autodesk.com/ACD/2013/ENU/index.html?url=files/GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008.htm,topicNumber=d30e503195</a><a href="http://docs.autodesk.com/ACD/2013/ENU/index.html?url=files/GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008.htm,topicNumber=d30e503195" target="_self"></a>&#0160;&#0160;</p>
