---
layout: "post"
title: "Road Widening using AutoCAD Civil 3D .NET API"
date: "2014-03-18 02:04:44"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/03/road-widening-using-autocad-civil-3d-net-api.html "
typepad_basename: "road-widening-using-autocad-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D, programmatically we can create Offset Alignment using overloaded <strong>Alignment.CreateOffsetAlignment()</strong> API. Once we have created an Offset Alignment, we can call <strong>OffsetAlignmentInfo.AddWidening()</strong> to create a road widening for a specific section of the road.</p>
<p>Here is a C# .NET code snippet demonstrating usage of OffsetAlignmentInfo.<strong>AddWidening()</strong> API :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> alignmentIdColl = civilDoc.GetAlignmentIds();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> alignmentId </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> alignmentIdColl)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; alignment = alignmentId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (alignment.Name.ToString() == </span><span style="color: #a31515; line-height: 140%;">&quot;Centerline (1)&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (alignment != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get a AlignmentStyle Object to use in OffSet Alignment creation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> offsetAlignmentStyleId = civilDoc.Styles.AlignmentStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Design Style&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (offsetAlignmentStyleId == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nOffset Alignment Style NOT found !&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Now create the OffSetAlignment</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// A negative value (offset &lt; 0) indicates the Offset Alignment is at the left of the parent alignment.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// A positive value (offset &gt; 0) indicates the Offset Alignment is at the right.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> offsetAlignmentId = </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">.CreateOffsetAlignment(</span><span style="color: #a31515; line-height: 140%;">&quot;Offset_Alignment&quot;</span><span style="line-height: 140%;">, alignment.ObjectId, -30.00, offsetAlignmentStyleId);</span></strong></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Open the Offset Alignment object to add road widening</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;"> offsetAlignment = offsetAlignmentId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">OffsetAlignmentInfo</span><span style="line-height: 140%;"> offsetInfo1 = offsetAlignment.OffsetAlignmentInfo;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Now use AddWidening( double startStation, double endStation, double offsetDistance )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// This method will create a specific region from startStation to endStation </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// with slim entry transition and exit transition.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; <span style="background-color: #ffff00;"><strong>offsetInfo1.AddWidening(200.00, 533.0, 40.0);</strong></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }&#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Following picture shows how it appears in AutoCAD Civil 3D :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcd87c87970b-pi" style="display: inline;"><img alt="Civil3D_Road_Widening_API" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcd87c87970b img-responsive" src="/assets/image_e346c6.jpg" title="Civil3D_Road_Widening_API" /></a></p>
<p>Hope this is useful to you!</p>
