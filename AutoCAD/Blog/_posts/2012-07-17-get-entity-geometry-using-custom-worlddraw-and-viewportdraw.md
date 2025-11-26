---
layout: "post"
title: "Get entity geometry using custom WorldDraw and ViewportDraw"
date: "2012-07-17 06:19:12"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/get-entity-geometry-using-custom-worlddraw-and-viewportdraw.html "
typepad_basename: "get-entity-geometry-using-custom-worlddraw-and-viewportdraw"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When AutoCAD draws an entity, it is passing an implementation of WorldDraw and ViewportDraw classes to the entity which then can call the functions of those classes to draw itself. First WorldDraw is called, where the entity should draw geometry which is not viewport or view orientation specific. If the entity returns false from that function, then it means that it also wants to draw viewport specific geometry, so we could/should call ViewportDraw as well on the entity to gather the additional geometry.</p>
<p>We can create our own implementation of WorldDraw, ViewportDraw and the additionally needed classes so that we can collect the geometry the entity uses to represent itself.</p>
<p>If we are only interested in non-viewport specific geometry then we only need to implement our own Context, SubEntityTraits, WorldGeometry, and WorldDraw classes. Otherwise we also need Viewport, ViewportGeometry, and ViewportDraw classes. <br />Some entities draw all their geometry inside ViewportDraw(), but probably most draw it all inside WorldDraw(), while others may use both.</p>
<p>If you are interested in the solid representation of an entity it is a good idea to use an isometric view direction for the Viewport, like (1, 1, 1), that&#39;s what the sample uses too, because some entities have a non solid representation in views like Top.</p>
<p>ARX SDK contains the ArxDbg sample application, which among other things implements its own WorldDraw to collect the entity geometry. The attached sample also implements ViewportDraw - it simply lists the calls that the entity makes to draw itself and in case of solid entities which use Shell() to draw themselves creates lines based on the edges of the mesh facets. It draws all edges twice - it&#39;s not optimized. It&#39;s just a very basic sample. :)</p>
<p>You can use the attached classes like so:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[Autodesk.AutoCAD.Runtime.</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;GetGeometry&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> GetGeometry()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(</span><span style="color: #a31515; line-height: 140%;">&quot;Select entity&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = ed.Document.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent = (</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">)tr.GetObject(per.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!ent.WorldDraw(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyWorldDraw</span><span style="line-height: 140%;">()))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ent.ViewportDraw(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyViewportDraw</span><span style="line-height: 140%;">());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; tr.Commit(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01761686196e970c"><a href="http://adndevblog.typepad.com/files/mydraw.zip">Download MyDraw</a></span></p>
