---
layout: "post"
title: "Using CogoPoint.ApplyDescriptionKeys() to apply DescriptionKeys to a CogoPoint"
date: "2012-06-26 23:31:49"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/using-cogopointapplydescriptionkeys-to-apply-descriptionkeys-to-a-cogopoint.html "
typepad_basename: "using-cogopointapplydescriptionkeys-to-apply-descriptionkeys-to-a-cogopoint"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>As you know, we can use description keys to automatically control point appearance and some point properties when creating or importing a point into a drawing.</p>
<p>We use description keys to automatically control some drawing point properties, such as the appearance of a point in the drawing at the time of import points or when we create point in the drawing. Before you create drawing points using description keys, create a series of description keys. Then, when you create or import a drawing point, the <strong>raw description</strong> for the point specifies which description key is used to create the point in the drawing. The properties defined for that description key are applied to the point as it is added to the drawing.</p>
<p>AutoCAD Civil 3D provides a nice UI tool &quot;<strong>Apply Description Keys</strong>&quot; to apply&#0160; description keys to a single point or a collection of points.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017615dc2dec970c-pi" style="display: inline;"><img alt="Apply_Desc1" class="asset  asset-image at-xid-6a0167607c2431970b017615dc2dec970c" src="/assets/image_1d1a85.jpg" title="Apply_Desc1" /></a><br /><br /></p>
<p>&#0160;</p>
<p>The following C# code snippet demonstrates how to apply description keys to a selected point after updating it&#39;s RawDescription value.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//open the COGO Point </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;"> cogoPoint = trans.GetObject(per.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Change it&#39;s RawDescription</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cogoPoint.RawDescription = </span><span style="color: #a31515; line-height: 140%;">&quot;TREE&quot;</span><span style="line-height: 140%;">;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// ApplyDescriptionKeys() to apply DescriptionKeys to a CogoPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cogoPoint.ApplyDescriptionKeys();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
</div>
<p>&#0160;</p>
<p>Once you build your application using the above code snippet and run the custom command in Civil 3D 2013, you should see the Point appearance changed(like below ) :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016767e6fe65970b-pi" style="display: inline;"><img alt="Desc_applied" class="asset  asset-image at-xid-6a0167607c2431970b016767e6fe65970b" src="/assets/image_dc59e7.jpg" title="Desc_applied" /></a><br /><br /></p>
<p>Hope this helps!</p>
