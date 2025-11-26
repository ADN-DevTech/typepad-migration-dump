---
layout: "post"
title: "RevitAPI: How to resize cropbox of view?"
date: "2015-03-26 02:31:35"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/03/revitapi-how-to-resize-cropbox-of-view.html "
typepad_basename: "revitapi-how-to-resize-cropbox-of-view"
typepad_status: "Publish"
---

<a href="http://blog.csdn.net/lushibi/article/details/44200459">中文链接</a>

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>

<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>




<p>
<b>Question:</b>
  Why does nothing happen when setting the cropbox of view?
</p>
<p>
  What he did is, first create a view and then set its CropBox property.
</p>
<p>
  <pre name="code prettyprint" class="csharp">ViewFamilyType vTypeElev = Class1.getviewfamilytypes(
    ViewFamily.FloorPlan, RevitDoc).First&lt;ViewFamilyType&gt;();
var view = ViewPlan.Create(RevitDoc, vTypeElev.Id,
    RevitDoc.ActiveView.GenLevel.Id);
view.CropBoxActive = true;
BoundingBoxXYZ box = new BoundingBoxXYZ();
box.Min = new XYZ(100, 100, 0);
box.Max = new XYZ(200, 200, 100);
view.CropBox = box;</pre>
</p>
<p>
  and he said it works (note: above code should be included in transaction).
</p>
<p>
  However, when he change the CropBox seperately, there is nothing happen:
</p>
<p>
  <pre name="code prettyprint" class="csharp">var view = RevitDoc.ActiveView;
BoundingBoxXYZ box = new BoundingBoxXYZ();
box.Min = new XYZ(0, 0, 0);
box.Max = new XYZ(100, 100, 100);
view.CropBox = box;
</pre>
  <br />
  I tried his code, which works on my machine, I'm not sure why.
</p>
<p>
  Even thouogh, I suggested him another way: using method SetCropRegionShape of class ViewCropRegionShapeManager:
</p>
<p>
  <pre name="code prettyprint" class="csharp">double length = 100;
var view = RevitDoc.ActiveView;
List&lt;Curve&gt; nl = new List&lt;Curve&gt;();
XYZ p2 = new XYZ(0, 0, 0);
XYZ p3 = new XYZ(length, 0, 0);
XYZ p4 = new XYZ(length, length, 0);
XYZ p5 = new XYZ(0, length, 0);
nl.Add(Line.CreateBound(p2, p3));
nl.Add(Line.CreateBound(p3, p4));
nl.Add(Line.CreateBound(p4, p5));
nl.Add(Line.CreateBound(p5, p2));
CurveLoop cl = CurveLoop.Create(nl);
ViewCropRegionShapeManager vpcr = view.GetCropRegionShapeManager();
bool cropValid = vpcr.IsCropRegionShapeValid(cl);
if (cropValid)
{
    vpcr.SetCropRegionShape(cl);
}</pre>
  <br />
  which is much powerful, because it not only support rectangle but other shapes.
</p>
<p>
