---
layout: "post"
title: "'StationOffsetElevationToXYZ' is ambiguous because..."
date: "2013-10-31 01:27:23"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/10/stationoffsetelevationtoxyz-is-ambiguous-because-multiple-kinds-of-members-with-this-name-exist-in-c.html "
typepad_basename: "stationoffsetelevationtoxyz-is-ambiguous-because-multiple-kinds-of-members-with-this-name-exist-in-c"
typepad_status: "Publish"
---

<p>&#39;StationOffsetElevationToXYZ&#39;
is ambiguous because multiple kinds of members with this name exist in class
&#39;Autodesk.Civil.DatabaseServices.BaseBaseline&#39;</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b007d6411970c-pi" style="display: inline;"><img alt="StationOffsetElevationToXYZ_Issue" class="asset  asset-image at-xid-6a0167607c2431970b019b007d6411970c" src="/assets/image_df7c8a.jpg" title="StationOffsetElevationToXYZ_Issue" /></a><br /><br /></p>
<p>This issue
seems to be specific to VB.NET project (if you are using C# .NET, it works fine).</p>
<p>We have
noticed this in Civil 3D 2014 release and by the time we address this issue in
Civil 3D .NET API, here is a workaround with Reflection and .NET Extensions which you can
adopt to overcome this -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> TestCommand()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> db </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> civilDoc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> (civilDoc.CorridorCollection.Count = 0) </span><span style="color: blue; line-height: 140%;">Then</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> trans </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> = db.TransactionManager.StartOpenCloseTransaction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> corr </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Corridor</span><span style="line-height: 140%;"> = trans.GetObject(civilDoc.CorridorCollection.Item(0), </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> bl </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Baseline</span><span style="line-height: 140%;"> = corr.Baselines.Item(0)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> soe </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">(bl.StartStation, 0, 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> pointAtSOE </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> = bl.StationOffsetElevationToXYZ(soe)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
<p>&#0160;</p>
<p>&lt;&lt;&lt; &#0160;</p>
<p>And declare a
<strong>VB.NET Module</strong> as below:&#0160;</p>
<p>&gt;&gt;&gt;&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Module</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Utility</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &lt;System.Runtime.CompilerServices.</span><span style="color: #2b91af; line-height: 140%;">Extension</span><span style="line-height: 140%;">()&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Function</span><span style="line-height: 140%;"> StationOffsetElevationToXYZ(bl </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Baseline</span><span style="line-height: 140%;">, soe </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> args </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Object</span><span style="line-height: 140%;"> = {</span><span style="color: blue; line-height: 140%;">CType</span><span style="line-height: 140%;">(soe, </span><span style="color: blue; line-height: 140%;">Object</span><span style="line-height: 140%;">)}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> argsModifiers </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> System.Reflection.</span><span style="color: #2b91af; line-height: 140%;">ParameterModifier</span><span style="line-height: 140%;">(1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; dont pass by reference, which is the new override for this method</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; argsModifiers(0) = </span><span style="color: blue; line-height: 140%;">False</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> mods() </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Reflection.</span><span style="color: #2b91af; line-height: 140%;">ParameterModifier</span><span style="line-height: 140%;"> = {argsModifiers}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Invoke the method late bound.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Return</span><span style="line-height: 140%;"> bl.GetType().InvokeMember(</span><span style="color: #a31515; line-height: 140%;">&quot;StationOffsetElevationToXYZ&quot;</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; System.Reflection.</span><span style="color: #2b91af; line-height: 140%;">BindingFlags</span><span style="line-height: 140%;">.InvokeMethod, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">, bl, args, mods, </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Function</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Module</span></p>
</div>
<p>&#0160;</p>
