---
layout: "post"
title: "Setting layer transparency"
date: "2012-04-03 03:50:34"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/setting-layer-transparency.html "
typepad_basename: "setting-layer-transparency"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Layers can be assigned a transparency value using the AutoCAD's layer dialog.</p>
<p>Here is sample code to show you how a similar result can be achieved using the AutoCAD .Net API.</p>
<p>But before trying this code, dont forget to set the "<span style="font-family: &quot;Arial&quot;,&quot;sans-serif&quot;; font-size: 10pt; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA;">TRANSPARENCYDISPLAY" system variable to 1.</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">"Test"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> TestMethod()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SetLayerTransparency(</span><span style="color: #a31515; line-height: 140%;">"Autodesk"</span><span style="line-height: 140%;">, 50);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> sets the layer transparency</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Can range from 0 (opaque) to 90 (almost transparent)</span></p>
<p style="margin: 0px;"><span style="color: gray; line-height: 140%;">///</span><span style="color: green; line-height: 140%;"> returns ObjectId of the layer</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> SetLayerTransparency</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> layerName, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Byte</span><span style="line-height: 140%;"> layerTransparency</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> activeDoc </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = activeDoc.Database;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> layerId = </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;">.Null;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> done = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">LayerTable</span><span style="line-height: 140%;"> lt </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= tr.GetObject</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">db.LayerTableId, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LayerTable</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (lt.Has(layerName))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">layerId = lt[layerName];</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">LayerTableRecord</span><span style="line-height: 140%;"> ltr </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">layerId, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LayerTableRecord</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// The color is being set here to ensure that </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// a regen will consider redrawing all the entities </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// belonging to this layer.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ltr.Color = ltr.Color;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Byte</span><span style="line-height: 140%;"> alpha = (</span><span style="color: #2b91af; line-height: 140%;">Byte</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(255 * (100 - layerTransparency) / 100);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Transparency</span><span style="line-height: 140%;"> trans = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Transparency</span><span style="line-height: 140%;">(alpha);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ltr.Transparency = trans;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">done = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (done)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">RefreshEntities(layerId);</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// (OR)</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//activeDoc.Editor.Regen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> layerId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Marks the entities referencing a </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// certain layer as "Modified"</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> RefreshEntities(</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> layerId)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> activeDoc = </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = activeDoc.Database;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;"> bt = tr.GetObject</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">db.BlockTableId, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> btr = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.GetObject</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">bt[</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace],</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> entityId </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> btr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= tr.GetObject</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">entityId, </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ent.LayerId.Equals(layerId))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ent.UpgradeOpen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ent.RecordGraphicsModified(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
