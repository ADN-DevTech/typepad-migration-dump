---
layout: "post"
title: "RevitAPI: how to get point clouds?"
date: "2016-04-01 00:13:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/04/revitapi-how-to-get-point-clouds.html "
typepad_basename: "revitapi-how-to-get-point-clouds"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/50109445">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>From Revit &gt; Insert &gt; Point Cloud, we can insert point clouds into Revit, then how to get those points information via API?</p>
<p>Inspect with RevitLookup, we found the inserted point cloud is an object of class PointCloudInstance, and there is a method named GetPoints, which is what we want.</p>
<p>&#0160;</p>
<p>GetPoints() has 3 arguments</p>
<pre class="csharp">public PointCollection GetPoints(
PointCloudFilter filter, double averageDistance, int numPoints);</pre>
<p><strong>filter</strong> specifies the conditions for filtering points.</p>
<p><strong>averageDistance</strong> stands for minimal distance between points, the smaller, the more points we will get.</p>
<p><strong>numPoints</strong> stands for the maximum number of points to be returned.</p>
<p>The key is how to create the filter, there is no constructor of class PointCloudFilter, no static Create function, and no relative creation functions&#0160;under&#0160;Document.Create nor Application.Create, fortunately, we found method CreateMultiPlaneFilter in class PointCloudFilterFactory.</p>
<pre class="csharp">public static PointCloudFilter CreateMultiPlaneFilter(IList&lt;Autodesk.Revit.DB.Plane&gt; planes);
public static PointCloudFilter CreateMultiPlaneFilter(IList&lt;Autodesk.Revit.DB.Plane&gt; planes, int exactPlaneCount);
</pre>
<p><br /> <strong>planes</strong>: defines a set of planes, all points returned should be in the positive direction of those planes, so we can use those planes to define a range to include all the points, and the range can be non-closure.</p>
<p><strong>exactPlaneCount</strong>: Exact number of planes to match. The greater, the more accurate (meaning less points are outside of the range defined by planes), the slower of the searching speed. If not specified, it is default to the number of first argument: planes.</p>
<p>Below example shows how to filter out points inside a box:</p>
<pre class="csharp prettyprint">if (pointCloudInstance != null)
{
    int SIZE = 100;
    List&lt;Plane&gt; planes = new List&lt;Plane&gt;();
    var min = new XYZ(-SIZE,-SIZE, -SIZE);
    var max = new XYZ(SIZE, SIZE, SIZE);

    //x planes
    planes.Add(new Plane(XYZ.BasisX, min));
    planes.Add(new Plane(-XYZ.BasisX, max));

    //y planes
    planes.Add(new Plane(XYZ.BasisY, min));
    planes.Add(new Plane(-XYZ.BasisY, max));

    //z planes
    planes.Add(new Plane(XYZ.BasisZ, min));
    planes.Add(new Plane(-XYZ.BasisZ, max));

    PointCloudFilter pcf = PointCloudFilterFactory.CreateMultiPlaneFilter(planes);
    var points = pointCloudInstance.GetPoints(pcf, 1, 100000);
}</pre>
<p>&#0160;</p>
