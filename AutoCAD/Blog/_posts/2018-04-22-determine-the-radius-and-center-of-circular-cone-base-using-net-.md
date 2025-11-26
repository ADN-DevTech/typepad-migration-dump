---
layout: "post"
title: "Determine the radius and center of circular cone base using .NET "
date: "2018-04-22 23:04:37"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "AutoCAD"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2018/04/determine-the-radius-and-center-of-circular-cone-base-using-net-.html "
typepad_basename: "determine-the-radius-and-center-of-circular-cone-base-using-net-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e033ebde200d-pi"><img alt="CircularRadius" class="asset  asset-image at-xid-6a0167607c2431970b0224e033ebde200d img-responsive" src="/assets/image_628730.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="CircularRadius" /></a></p>
<p>We can make use of Brep support to determine the radius and center of a cone( properties shown up in&#0160; the image image)&#0160;with circular base.&#0160; &#0160;&#0160;</p>
<p>&#0160;Below .NET code snippet can be used :&#0160;</p>
<pre style="background: #000; color: #f8f8f8;">[CommandMethod(<span style="color: #65b042;">&quot;circularConeRadiusAndCenter&quot;</span>)]
<span style="color: #e28964;">public</span> <span style="color: #dad085;">void</span> circularConeRadiusAndCenter()
{
    Document doc <span style="color: #e28964;">=</span> Autodesk.AutoCAD.ApplicationServices.<span style="color: #9b859d;">Application</span>.DocumentManager.MdiActiveDocument;
    Database db <span style="color: #e28964;">=</span> doc.Database;
    Editor ed <span style="color: #e28964;">=</span> doc.Editor;

    <span style="color: #aeaeae; font-style: italic;">// Ask the user to select a solid</span>
    PromptEntityOptions peo <span style="color: #e28964;">=</span> <span style="color: #e28964;">new</span> PromptEntityOptions(<span style="color: #65b042;">&quot;Select a 3D solid&quot;</span>);
    peo.SetRejectMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span>A 3D solid must be selected.&quot;</span>);
    peo.AddAllowedClass(<span style="color: #dad085;">typeof</span>(Solid3d), <span style="color: #3387cc;">true</span>);
    PromptEntityResult per <span style="color: #e28964;">=</span> ed.GetEntity(peo);

    <span style="color: #e28964;">if</span> (per.Status <span style="color: #e28964;">!</span><span style="color: #e28964;">=</span> PromptStatus.OK)
        <span style="color: #e28964;">return</span>;

    Transaction tr <span style="color: #e28964;">=</span> db.TransactionManager.StartTransaction();
    using (tr)
    {
        Solid3d sol <span style="color: #e28964;">=</span> tr.GetObject(per.ObjectId, OpenMode.ForRead) as Solid3d;
        using (Brep brep <span style="color: #e28964;">=</span> <span style="color: #e28964;">new</span> Brep(sol))
        {
            <span style="color: #aeaeae; font-style: italic;">//check if base is a circular. If yes, then determine the center and radius</span>
            foreach (Complex cmp <span style="color: #dad085;">in</span> brep.Complexes)
            {
                <span style="color: #dad085;">int</span> edgeCnt <span style="color: #e28964;">=</span> <span style="color: #3387cc;">1</span>;
                foreach (Edge edge <span style="color: #dad085;">in</span> brep.Edges)
                {
                    ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> --&gt; Edge : {0}&quot;</span>, edgeCnt<span style="color: #e28964;">+</span><span style="color: #e28964;">+</span>);
                    Curve3d edgeCurve <span style="color: #e28964;">=</span> edge.Curve;
                    <span style="color: #e28964;">if</span> (edgeCurve is ExternalCurve3d)
                    {
                        ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> Edge type : {0}&quot;</span>, <span style="color: #65b042;">&quot;ExternalCurve3d&quot;</span>);
                        ExternalCurve3d extCurve3d <span style="color: #e28964;">=</span> edgeCurve as ExternalCurve3d;
                        Curve3d nativeCurve <span style="color: #e28964;">=</span> extCurve3d.NativeCurve;
                        <span style="color: #e28964;">if</span> (nativeCurve is CircularArc3d)
                        {
                            ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> Native curve type : {0}&quot;</span>, <span style="color: #65b042;">&quot;CircularArc3d&quot;</span>);
                            CircularArc3d circArc3d <span style="color: #e28964;">=</span> nativeCurve as CircularArc3d;
                            ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> radius is &quot;</span> <span style="color: #e28964;">+</span> circArc3d.Radius.ToString());                                    
                            ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> center is &quot;</span> <span style="color: #e28964;">+</span> PointToStr(circArc3d.Center));
                        }
                        <span style="color: #e28964;">else</span>
                        {
                            ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> Native curve type of cone is not Circular&quot;</span>);
                            <span style="color: #e28964;">return</span>;
                        }

                    }

                }
            }
        }
    }
}
<span style="color: #e28964;">private</span> string PointToStr(Point3d point)
{
    <span style="color: #e28964;">return</span> <span style="color: #65b042;">&quot;[&quot;</span> <span style="color: #e28964;">+</span>
        point.X.ToString(<span style="color: #65b042;">&quot;F2&quot;</span>) <span style="color: #e28964;">+</span> <span style="color: #65b042;">&quot;, &quot;</span> <span style="color: #e28964;">+</span>
        point.Y.ToString(<span style="color: #65b042;">&quot;F2&quot;</span>) <span style="color: #e28964;">+</span> <span style="color: #65b042;">&quot;, &quot;</span> <span style="color: #e28964;">+</span>
        point.Z.ToString(<span style="color: #65b042;">&quot;F2&quot;</span>)
        <span style="color: #e28964;">+</span> <span style="color: #65b042;">&quot;]&quot;</span>;
}
</pre>
