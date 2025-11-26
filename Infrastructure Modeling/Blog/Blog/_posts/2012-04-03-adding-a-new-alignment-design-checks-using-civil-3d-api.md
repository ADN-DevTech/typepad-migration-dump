---
layout: "post"
title: "Adding a new Alignment Design Checks using Civil 3D API"
date: "2012-04-03 03:14:00"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/adding-a-new-alignment-design-checks-using-civil-3d-api.html "
typepad_basename: "adding-a-new-alignment-design-checks-using-civil-3d-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D's Toolspace -&gt; Settings Tabs when you expand the collection items under Alignment, you would see a set of object collection like the Alignment Styles, Design Checks, Label styles etc. Under the Design Checks collection you would see objects like "Design Check Sets", "Line", "Curve", "Spiral" etc. You intend to add a new Line Design Check item with a custom Expression or a new Curve Design Check item with a custom Expression / Formula.&nbsp;</p>
<p>The trick of adding a new Design Checks is exploring the <em><strong>AlignmentDesignCheckRoot</strong></em> Object.</p>
<p>Here is a VB.NET code snippet which demonstrates how to add new Alignment Design Checks using Civil 3D .NET API.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> db </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> = </span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> trans </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> = db.TransactionManager.StartTransaction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">' AlignmentDesignCheckRoot </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAlignmentDesignCheckRoot </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AlignmentDesignCheckRoot</span><span style="line-height: 140%;"> =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AlignmentDesignCheckRoot</span><span style="line-height: 140%;">(Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">' Add CurveDesignChecks</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; oAlignmentDesignCheckRoot.CurveDesignChecks.Add(</span><span style="color: #a31515; line-height: 140%;">"MY_TEST_Curve"</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">"Created using API"</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">"Radius&gt;200"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">' Add LineDesignChecks</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; oAlignmentDesignCheckRoot.LineDesignChecks.Add(</span><span style="color: #a31515; line-height: 140%;">"MY_TEST_Line"</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">"Created using API"</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">"{Design Speed}&lt;=65"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">' SpiralDesignChecks</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; oAlignmentDesignCheckRoot.SpiralDesignChecks.Add(</span><span style="color: #a31515; line-height: 140%;">"MY_TEST_Spiral"</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">"Created using API"</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">"A&lt;=100.05"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; trans.Commit()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 15px;"><br /></span></p>
</div>
<p><br />And the result of running the above code snippet –&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e996c696970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168e996c696970c" title="C3D_ALD" src="/assets/image_fc9ca0.jpg" border="0" alt="C3D_ALD" /></a><br /><br /></p>
<p><br />Hope this is useful to you !</p>
<p><br /><br /></p>
