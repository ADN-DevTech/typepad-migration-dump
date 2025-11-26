---
layout: "post"
title: "How do I turn off the \"Map Base\" Layer using API ?"
date: "2012-09-10 01:38:00"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/how-do-i-turn-off-the-map-base-layer-using-api-.html "
typepad_basename: "how-do-i-turn-off-the-map-base-layer-using-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>I want to switch
off the &quot;Map Base&quot; Layer using Map 3D API.</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3bf1d1ec970c-pi" style="display: inline;"><img alt="Map_Base" class="asset  asset-image at-xid-6a0167607c2431970b017d3bf1d1ec970c" src="/assets/image_d0b879.jpg" title="Map_Base" /></a></p>
<p>&#0160;</p>
<p>Here is the C# code snippet to switch off the &quot;Map Base&quot; Layer using Map 3D
managed API :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = transManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> managerId = </span><span style="color: #2b91af; line-height: 140%;">DisplayManager</span><span style="line-height: 140%;">.Create(proj).MapManagerId(proj, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">MapManager</span><span style="line-height: 140%;"> manager = trans.GetObject(managerId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MapManager</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (manager != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; currentmapId = manager.CurrentMapId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Map</span><span style="line-height: 140%;"> currentMap = trans.GetObject(currentmapId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Map</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">IEnumerator</span><span style="line-height: 140%;"> iterator = currentMap.NewIterator(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Type</span><span style="line-height: 140%;"> groupType = </span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(Autodesk.Gis.Map.DisplayManagement.</span><span style="color: #2b91af; line-height: 140%;">Group</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (iterator.MoveNext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> objId = (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;">)iterator.Current;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;"> obj = trans.GetObject(objId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite);&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (obj.GetType().ToString() == </span><span style="color: #a31515; line-height: 140%;">&quot;Autodesk.Gis.Map.DisplayManagement.BaseElement&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; Autodesk.Gis.Map.DisplayManagement.</span><span style="color: #2b91af; line-height: 140%;">BaseElement</span><span style="line-height: 140%;"> baseElement = obj </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> Autodesk.Gis.Map.DisplayManagement.</span><span style="color: #2b91af; line-height: 140%;">BaseElement</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; baseElement.SetVisible(0, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
