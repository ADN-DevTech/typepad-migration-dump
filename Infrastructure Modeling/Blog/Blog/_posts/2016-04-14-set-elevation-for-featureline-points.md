---
layout: "post"
title: "Set elevation for FeatureLine Points"
date: "2016-04-14 11:40:51"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2016/04/set-elevation-for-featureline-points.html "
typepad_basename: "set-elevation-for-featureline-points"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="http://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>Since <a href="http://adndevblog.typepad.com/infrastructure/2015/08/civil-3d-2016-sp-1-new-feature-line-api.html">Civil 3D 2016 SP1</a> we have the Feature Line API available for developers. It's possible to set new elevation for points on the feature line,&nbsp;but it may not be that direct to do so as we need the index of the point and, if we think in delta of elevation, we need the original point elevation.&nbsp;</p>
<p>This quick extension method aims to do that, just call <strong>FeatureLine.SetPointNewElevation(somePoint, elevationDelta)</strong></p>
<!-- HTML generated using hilite.me --><div><pre style="margin: 0; line-height: 125%"><span style="color: #0000aa">public</span> <span style="color: #0000aa">static</span> <span style="color: #0000aa">void</span> <span style="color: #00aa00">SetNewPointElevation</span>(<span style="color: #0000aa">this</span> FeatureLine fl,
  Point3d point, <span style="color: #00aaaa">double</span> elevationDelta)
{
  <span style="color: #aaaaaa; font-style: italic">// get all points on the Feature Line</span>
  Point3dCollection pointsOnFL = 
    fl.GetPoints(FeatureLinePointType.AllPoints);

  <span style="color: #aaaaaa; font-style: italic">// find the closest point and index</span>
  <span style="color: #00aaaa">double</span> distance = <span style="color: #00aaaa">double</span>.MaxValue;
  <span style="color: #00aaaa">int</span> index= <span style="color: #009999">0</span>;
  Point3d closestPointOnCurve = Point3d.Origin;
  <span style="color: #0000aa">for</span> (<span style="color: #00aaaa">int</span> i = <span style="color: #009999">0</span>; i &lt; pointsOnFL.Count; i++ )
  {
    Point3d p = pointsOnFL[i];
    <span style="color: #0000aa">if</span> (p.DistanceTo(point) &lt; distance){
      distance = p.DistanceTo(point);
      closestPointOnCurve = p;
      index = i;
    }
  }

  <span style="color: #aaaaaa; font-style: italic">// apply the delta</span>
  fl.SetPointElevation(index,
    closestPointOnCurve.Z + elevationDelta);
}
</pre></div>
