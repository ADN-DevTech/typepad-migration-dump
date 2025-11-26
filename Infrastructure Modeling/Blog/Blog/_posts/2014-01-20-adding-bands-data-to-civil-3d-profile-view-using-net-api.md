---
layout: "post"
title: "Adding Bands data to Civil 3D Profile View using .NET API"
date: "2014-01-20 22:40:39"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/01/adding-bands-data-to-civil-3d-profile-view-using-net-api.html "
typepad_basename: "adding-bands-data-to-civil-3d-profile-view-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In this example we see how to add Profile View data bands to a Profile View in AutoCAD Civil 3D using .NET API.</p>
<p>Here we add the band for the top band. In AutoCAD Civil 3D &quot;Profile View Properties&quot; dialog box, you will notice &#39;Location&#39; drop down box under the &#39;List of Bands&#39; and it has two options -&#0160; &#39;Top of Profile View&#39; or &#39;Bottom of Profile View&#39;. In this example I am going to add a new band in the &#39;Top of Profile View&#39;.</p>
<p>&#0160;</p>
<p>Top of Profile View before adding the data band :</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51134ca06970c-pi" style="display: inline;"><img alt="Civil_3D_ProfileView_TopBand_Empty" class="asset  asset-image at-xid-6a0167607c2431970b01a51134ca06970c img-responsive" src="/assets/image_bf46ed.jpg" title="Civil_3D_ProfileView_TopBand_Empty" /></a></p>
<p>&#0160;</p>
<p>And After adding the data band :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0511a9c1970d-pi" style="display: inline;"><img alt="Civil_3D_ProfileView_TopBand_API" class="asset  asset-image at-xid-6a0167607c2431970b019b0511a9c1970d img-responsive" src="/assets/image_b954ee.jpg" title="Civil_3D_ProfileView_TopBand_API" /></a><br />&#0160;</p>
<p>And here is the C# .NET code snippet :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> civilDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//open the profile view</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ProfileView</span><span style="line-height: 140%;"> pv = trans.GetObject(pvId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">ProfileView</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//get the style of the first Bottom Bands&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> bandStyleId = pv.Bands.GetBottomBandItems()[0].BandStyleId;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//now access the collection of top band</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ProfileViewBandItemCollection</span><span style="line-height: 140%;"> topBandItems = pv.Bands.GetTopBandItems();&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//add a new one (using the above style)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; topBandItems.Add(bandStyleId);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//now Set the Top Bands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pv.Bands.SetTopBandItems(topBandItems);&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
