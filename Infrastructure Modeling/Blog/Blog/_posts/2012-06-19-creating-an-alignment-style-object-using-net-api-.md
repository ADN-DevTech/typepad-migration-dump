---
layout: "post"
title: "Creating an Alignment Style Object using .NET API "
date: "2012-06-19 04:02:47"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/creating-an-alignment-style-object-using-net-api-.html "
typepad_basename: "creating-an-alignment-style-object-using-net-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This sample Demonstrates creating Alignment Style using the new .NET API in AutoCAD Civil 3D 2013</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAlignmentSTName </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = </span><span style="color: #a31515; line-height: 140%;">&quot;Demo_Alignment_Style&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAlignmentStyle </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Civil.DatabaseServices.Styles.</span><span style="color: #2b91af; line-height: 140%;">AlignmentStyle</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> alignmentStyleId </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Check if a style by this name already exists.&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; alignmentStyleId = civilDoc.Styles.AlignmentStyles.Item(oAlignmentSTName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> (alignmentStyleId.IsValid) </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Style : &quot;</span><span style="line-height: 140%;"> + oAlignmentSTName.ToString + </span><span style="color: #a31515; line-height: 140%;">&quot; &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;Already exists in thsi Dwg !&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; alignmentStyleId = Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;">.Null</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> (alignmentStyleId.IsNull) </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; alignmentStyleId = civilDoc.Styles.AlignmentStyles.Add(oAlignmentSTName)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> (alignmentStyleId = </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Error setting an Alignment Style: &quot;</span><span style="line-height: 140%;"> &amp; Err.Description)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle = trans.GetObject(alignmentStyleId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Getting and setting style attributes for StyleBase objects </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39;requires using a GetDisplayStyle*() method rather than a property.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStyleModel(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Arrow).Visible = </span><span style="color: blue; line-height: 140%;">False</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStylePlan(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Arrow).Visible = </span><span style="color: blue; line-height: 140%;">False</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Display curves using a green color </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStyleModel(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Curve).Color = Autodesk.AutoCAD.Colors.</span><span style="color: #2b91af; line-height: 140%;">Color</span><span style="line-height: 140%;">.FromRgb(58, 191, 13)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStylePlan(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Curve).Color = Autodesk.AutoCAD.Colors.</span><span style="color: #2b91af; line-height: 140%;">Color</span><span style="line-height: 140%;">.FromRgb(58, 191, 13)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStyleModel(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Curve).Visible = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStylePlan(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Curve).Visible = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Display straight sections in blue.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStyleModel(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Line).Color = Autodesk.AutoCAD.Colors.</span><span style="color: #2b91af; line-height: 140%;">Color</span><span style="line-height: 140%;">.FromRgb(0, 0, 255) </span><span style="color: green; line-height: 140%;">&#39; blue</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStylePlan(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Line).Color = Autodesk.AutoCAD.Colors.</span><span style="color: #2b91af; line-height: 140%;">Color</span><span style="line-height: 140%;">.FromRgb(0, 0, 255) </span><span style="color: green; line-height: 140%;">&#39; blue</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStyleModel(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Line).Visible = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.GetDisplayStylePlan(</span><span style="color: #2b91af; line-height: 140%;">AlignmentDisplayStyleType</span><span style="line-height: 140%;">.Line).Visible = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.EnableRadiusSnap = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oAlignmentStyle.RadiusSnapValue = 0.05 </span><span style="color: green; line-height: 140%;">&#39; set a Radius Snap Value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit()</span></p>
</div>
<p>&#0160;</p>
<p>Once you build your application using the above code snippet and run the custom command in Civil 3D 2013, you should see the Alignment Style create (like below ) :</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016767ad41c9970b-pi" style="display: inline;"><img alt="Alignment_Style" class="asset  asset-image at-xid-6a0167607c2431970b016767ad41c9970b" src="/assets/image_007565.jpg" title="Alignment_Style" /></a><br /><br /></p>
<p>Hope this helps!</p>
