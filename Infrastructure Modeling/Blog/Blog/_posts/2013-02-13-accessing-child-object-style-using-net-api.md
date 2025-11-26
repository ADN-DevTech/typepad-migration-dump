---
layout: "post"
title: "Accessing Child Object Style using .NET API"
date: "2013-02-13 20:51:02"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/accessing-child-object-style-using-net-api.html "
typepad_basename: "accessing-child-object-style-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In this
particular example we will see how to access Civil 3D Surface Spot Elevation
Child label style. Suppose there is a Surface Spot Elevation Child label style
named &quot;Foot Meter_Child&quot; in a Civil 3D DWG file and we try to use the
following VB.NET code snippet to find out the number of Spot Elevation label
style in the same DWG file, child label styles won&#39;t be shown or counted as
shown in the screenshot below -</p>
<p><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> surfaceSpotElevnLblStyle </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyleCollection</span><span style="line-height: 140%;"> = civilDoc.Styles.LabelStyles.SurfaceLabelStyles.SpotElevationLabelStyles</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;This DWG File has : &quot;</span><span style="line-height: 140%;"> + <span style="background-color: #ffff40;">surfaceSpotElevnLblStyle.Count.ToString</span> + </span><span style="color: #a31515; line-height: 140%;">&quot; Surface Spot Elevn Label Styles !&quot;</span><span style="line-height: 140%;">)</span></p>
</div>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36dcd36a970b-pi" style="display: inline;"><img alt="Child_Label_Style_01" class="asset  asset-image at-xid-6a0167607c2431970b017c36dcd36a970b" src="/assets/image_a0ba5b.jpg" title="Child_Label_Style_01" /></a></p>
<p>&#0160;</p>
<p>Above code
snippet shows the count of &#39;Spot Elevation&#39; labels as two omitting the child label
style.&#0160; So how do we access Child
Object Style using .NET API ?</p>
<p>We can access
child label style from the parent label style&#39;s ObjectId using [index]. Here is
the relevant VB.NET code snippet -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> surfaceSpotElevnLblStyle </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyleCollection</span><span style="line-height: 140%;"> = civilDoc.Styles.LabelStyles.SurfaceLabelStyles.SpotElevationLabelStyles</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39;MsgBox(&quot;This DWG File has : &quot; + surfaceSpotElevnLblStyle.Count.ToString + &quot; Surface Spot Elevn Label Styles !&quot;) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> surfaceLblStyleId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> osurfaceElevnLabelStyle </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;">&#0160; = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> surfaceLblStyleId </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> surfaceSpotElevnLblStyle </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; osurfaceElevnLabelStyle = trans.GetObject(surfaceLblStyleId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Style Name : &quot;</span><span style="line-height: 140%;"> + osurfaceElevnLabelStyle.Name.ToString + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;Children Style Count : &quot;</span><span style="line-height: 140%;"> + osurfaceElevnLabelStyle.ChildrenCount.ToString) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> (osurfaceElevnLabelStyle.ChildrenCount &gt;0) </span><span style="color: blue; line-height: 140%;">then</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="background-color: #ffff40;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> childLblStyleId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> =&#0160;&#0160; osurfaceElevnLabelStyle(0) </span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> childLblStyle </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LabelStyle</span><span style="line-height: 140%;"> = trans.GetObject(childLblStyleId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Child Style Name :&quot;</span><span style="line-height: 140%;"> + childLblStyle.Name.ToString)&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit()&#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Exception Message is : &quot;</span><span style="line-height: 140%;"> + ex.Message.ToString()) </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;"><br /></span></p>
</div>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee87faf6f970d-pi" style="display: inline;"><img alt="Child_Label_Style_02" class="asset  asset-image at-xid-6a0167607c2431970b017ee87faf6f970d" src="/assets/image_128598.jpg" title="Child_Label_Style_02" /></a><br /><br /></p>
<p>Hope this is
useful to you!</p>
