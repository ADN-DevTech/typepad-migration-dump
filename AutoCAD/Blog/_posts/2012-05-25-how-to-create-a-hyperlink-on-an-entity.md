---
layout: "post"
title: "How to create a hyperlink on an entity with AutoCAD .NET API?"
date: "2012-05-25 13:42:27"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/how-to-create-a-hyperlink-on-an-entity.html "
typepad_basename: "how-to-create-a-hyperlink-on-an-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>Entities in AutoCAD can be associated with hyperlinks. Users can visit the referenced locations from the hyperlink on an entity. There are three types of hyperlink locations on AutoCAD entities. They are URLs, files, and DWG file targets (model, layout1, and so on). This article shows how to use .NET API to create a URL hyperlink on an entity. The code is in C#.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>[</span></span><span><span><span style="color: #2b91af;">CommandMethod</span></span><span style="color: #000000;">(</span><span><span style="color: #a31515;">&quot;createHLink&quot;</span></span><span style="color: #000000;">)]</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span><span style="color: #0000ff;"><span>static</span></span></span><span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">public</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">void</span></span><span style="color: #000000;"> CmdCreateHyperLink()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160; </span></span><span><span><span style="color: #2b91af;">Editor</span></span><span style="color: #000000;"> ed = </span><span><span style="color: #2b91af;">Application</span></span><span style="color: #000000;">.DocumentManager.</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; MdiActiveDocument.Editor;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160; </span></span><span><span><span style="color: #2b91af;">Database</span></span><span style="color: #000000;"> db = </span><span><span style="color: #2b91af;">Application</span></span><span style="color: #000000;">.DocumentManager.</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; MdiActiveDocument.Database;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160; </span></span><span><span><span style="color: #2b91af;">PromptEntityResult</span></span><span style="color: #000000;"> selectedEntity = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; ed.GetEntity(</span></span><span><span><span style="color: #a31515;">&quot;Please Select an Entity: &quot;</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160; </span></span><span><span><span style="color: #2b91af;">ObjectId</span></span><span style="color: #000000;"> objectId = selectedEntity.ObjectId;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160; </span></span><span><span style="color: #0000ff;">try</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span><span><span><span style="color: #0000ff;">using</span></span><span style="color: #000000;"> (</span><span><span style="color: #2b91af;">Transaction</span></span><span style="color: #000000;"> trans = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction())</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span style="color: #008000;">//Get the entity</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span><span style="color: #2b91af;">Entity</span></span><span style="color: #000000;"> ent = trans.GetObject(objectId,</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span><span style="color: #2b91af;">OpenMode</span></span><span style="color: #000000;">.ForWrite) </span><span><span style="color: #0000ff;">as</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">Entity</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span style="color: #008000;">//Get the hyperlink collection from the entity</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span><span style="color: #2b91af;">HyperLinkCollection</span></span><span style="color: #000000;"> linkCollection = ent.Hyperlinks;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span style="color: #008000;">//Create a new hyperlink</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span><span style="color: #2b91af;">HyperLink</span></span><span style="color: #000000;"> hyperLink = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">HyperLink</span></span><span style="color: #000000;">();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; hyperLink.Description = </span></span><span><span><span style="color: #a31515;">&quot;ADN DevBlog&quot;</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; hyperLink.Name = </span></span><span><span><span style="color: #a31515;">&quot;ADN DevBlog&quot;</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160; hyperLink.SubLocation = </span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span><span style="color: #a31515;">&quot;http://adndevblog.typepad.com/autocad/&quot;</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span style="color: #008000;">//Add the hyperlink to the collection</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160; linkCollection.Add(hyperLink);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160; trans.Commit();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; }</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160; }</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span>&#0160; </span></span><span><span><span style="color: #0000ff;">catch</span></span><span style="color: #000000;"> (System.</span><span><span style="color: #2b91af;">Exception</span></span><span style="color: #000000;"> ex)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; ed.WriteMessage(ex.Message);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160; }</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">}</span></span></p>
</div>
