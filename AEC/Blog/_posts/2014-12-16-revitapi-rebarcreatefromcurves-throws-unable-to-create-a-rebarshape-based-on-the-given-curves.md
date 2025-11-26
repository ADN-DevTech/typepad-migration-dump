---
layout: "post"
title: "RevitAPI: Rebar.CreateFromCurves throws: Unable to create a RebarShape based on the given curves"
date: "2014-12-16 21:41:17"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/12/revitapi-rebarcreatefromcurves-throws-unable-to-create-a-rebarshape-based-on-the-given-curves.html "
typepad_basename: "revitapi-rebarcreatefromcurves-throws-unable-to-create-a-rebarshape-based-on-the-given-curves"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>
<p>We knew that Rebar has several creation method overloads, CreateFromCurves is one of them,</p>
<pre class="csharp prettyprint">public static Rebar CreateFromCurves(
    Document doc, RebarStyle style, RebarBarType barType, 
    RebarHookType startHook, RebarHookType endHook, Element host, 
    XYZ norm, IList&lt;Curve&gt; curves, 
    RebarHookOrientation startHookOrient, 
    RebarHookOrientation endHookOrient, 
    bool useExistingShapeIfPossible, bool createNewShape);</pre>
<p><br /> Let's take a look at argument norm and curves,</p>
<blockquote>
<p>norm<br /> &nbsp;&nbsp;&nbsp; Type: XYZ<br /> &nbsp;&nbsp;&nbsp; The normal to the plane that the rebar curves lie on. <br /> <br /> curves<br /> &nbsp;&nbsp;&nbsp; Type: IList&lt;Curve&gt;<br /> &nbsp;&nbsp;&nbsp; An array of curves that define the shape of the rebar curves. They must belong to the plane defined by the normal and origin. Bends and hooks should not be included in the array of curves.&nbsp;</p>
</blockquote>
<p>the RevitAPI.chm shows that the plane of the curves should be perpendicular to the normal.</p>
<p>A customer wants to create 2 rebars, and the plane of curves is truly perpendicular, but the fact is the first Rebar is created successfully, but the second one failed with an InvalidOperationException: Unable to create a RebarShape based on the given curves.</p>
<pre class="csharp prettyprint">RebarBarType bartype = new FilteredElementCollector(RevitDoc)
    .OfClass(typeof(RebarBarType))
    .FirstOrDefault(t =&gt; t.Name == "8 HPB300") as RebarBarType;
Curve
curve = Line.CreateBound(
    new XYZ(5.656152019023, 43.5912980314625, -10.0065608637852),
    new XYZ(5.65615201724766, 43.5912980188303, 7.70997373826235));
curves.Add(curve);
curve = Line.CreateBound(
    new XYZ(5.65615201724766, 43.5912980188303, 7.70997373826235), 
    new XYZ(5.65615201724766, 45.2478392340832, 7.70997373826235));
curves.Add(curve);
curve = Line.CreateBound(
    new XYZ(5.65615201724766, 45.2478392340832, 7.70997373826235), 
    new XYZ(5.656152019023, 45.2478392467154, -10.0065608637852));
curves.Add(curve);

// Create succesfully!
Rebar.CreateFromCurves(RevitDoc, RebarStyle.Standard, bartype, null, 
    null, ele, nml, curves, RebarHookOrientation.Left, 
    RebarHookOrientation.Left, false, true);

// Problem Curves
List&lt;Curve&gt; curves2 = new List&lt;Curve&gt;();
curve = Line.CreateBound(
    new XYZ(6.22700217574493, 43.5110702844365, -10.0065608637852), 
    new XYZ(6.22700217219427, 43.5110702591722, 7.70997373826235));
curves2.Add(curve);
curve = Line.CreateBound(
    new XYZ(6.22700217219427, 43.5110702591722, 7.70997373826235), 
    new XYZ(6.22700217219427, 45.1676114744251, 7.70997373826235));
curves2.Add(curve);
curve = Line.CreateBound(
    new XYZ(6.22700217219427, 45.1676114744251, 7.70997373826235), 
    new XYZ(6.22700217574493, 45.1676114996894, -10.0065608637852));
curves2.Add(curve);
//InvalidOperationException thrown
//  =&gt; Unable to create a RebarShape based on the given curves
Rebar.CreateFromCurves(RevitDoc, RebarStyle.Standard, bartype, null, 
    null, ele, nml, curves2, RebarHookOrientation.Left, 
    RebarHookOrientation.Left, false, true);</pre>
<p><br /> Seems the code is almost the same for the second Rebar creation comparing to the first one except the point positions. But why it throws while the first one doesn't?</p>
<p>I could not find the answer either :-(</p>
<p>My colleague Grzegorz said:</p>
<blockquote>The algorithm of creation of shape is quite complicated. Basically the problem can occur for shapes defined by segments (lines) forming RIGHT ANGLE between themselves. They have to be defined very precisely (different digits on 7 or 8 place after decimal separator can cause exception). The best solution for customer in this situation is to round all coordinates to 6 digits after decimal separator.</blockquote>
<p>So I changed the code like this, then the exception has gone.</p>
<pre class="csharp prettyprint">curve = Line.CreateBound(
    XYZ(6.22700217574493, 43.5110702844365, -10.0065608637852), 
    XYZ(6.22700217219427, 43.5110702591722, 7.70997373826235));
curves2.Add(curve);
curve = Line.CreateBound(
    XYZ(6.22700217219427, 43.5110702591722, 7.70997373826235), 
    XYZ(6.22700217219427, 45.1676114744251, 7.70997373826235));
curves2.Add(curve);
curve = Line.CreateBound(
    XYZ(6.22700217219427, 45.1676114744251, 7.70997373826235), 
    XYZ(6.22700217574493, 45.1676114996894, -10.0065608637852));
curves2.Add(curve);

....

private XYZ XYZ(double x, double y, double z)
{
    return new XYZ(
        Math.Round(x, 6), Math.Round(y, 6), Math.Round(z, 6));
}
</pre>


<a href="http://blog.csdn.net/lushibi/article/details/41978551">中文链接</a>
