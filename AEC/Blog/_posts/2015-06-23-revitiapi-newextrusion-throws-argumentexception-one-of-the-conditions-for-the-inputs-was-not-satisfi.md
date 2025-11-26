---
layout: "post"
title: "RevitiAPI: NewExtrusion throws ArgumentException: One of the conditions for the inputs was not satisfied"
date: "2015-06-23 02:27:00"
author: "Aaron Lu"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2015/06/revitiapi-newextrusion-throws-argumentexception-one-of-the-conditions-for-the-inputs-was-not-satisfi.html "
typepad_basename: "revitiapi-newextrusion-throws-argumentexception-one-of-the-conditions-for-the-inputs-was-not-satisfi"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/46313499">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>Sometimes calling Document.FamilyCreate.NewExtrusion will throw:</p>
<pre class="code-java">Autodesk.Revit.Exceptions.ArgumentException: One of the conditions for the inputs was not satisfied. Consult the documentation for requirements for each argument.</pre>
<p>This is probably caused by the profile and normal of sketch plane is not perpendicular.</p>
<p>For example:</p>
<pre class="csharp prettyprint">// Create a rectangle profile
CurveArrArray profile = new CurveArrArray();
CurveArray ca = new CurveArray();
XYZ[] points = new XYZ[] {
    new XYZ(10, 0, 0),
    new XYZ(20, 0, 0),
    new XYZ(20, 0, 10),
    new XYZ(10, 0, 10)
};
for (int ii = 0; ii &lt; points.Length; ii++)
{
    var point = points[ii];
    var point2 = points[ii == points.Length - 1 ? 0 : ii + 1];
    ca.Append(Line.CreateBound(point, point2));
}
profile.Append(ca);

// create the plane normal is perpendicular with profile
SketchPlane sketchplane = SketchPlane.Create(doc,
    new Plane(XYZ.BasisZ, XYZ.Zero));
Extrusion solid = doc.FamilyCreate.NewExtrusion(
    true, profile, sketchplane, 20);</pre>
<p><br /> This will throw, and notice that the normal of SketchPlane is the same as Z axis, but the profile is perpendicular to Y axis,</p>
<p>So the correct code of creating SketchPlane should be something like this:</p>
<pre class="csharp prettyprint">SketchPlane sketchplane = SketchPlane.Create(doc,
    new Plane(XYZ.BasisY, XYZ.Zero));
</pre>
<p>&#0160;</p>
