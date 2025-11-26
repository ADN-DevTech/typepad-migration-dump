---
layout: "post"
title: "Editor.Snap Method to change the snap mode of point"
date: "2020-06-01 23:05:50"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2020/06/editorsnap-method-to-change-the-snap-mode-of-point.html "
typepad_basename: "editorsnap-method-to-change-the-snap-mode-of-point"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>In your workflow of using Editor.GetEntity you may sometimes want to retrieve <br />all the co-ordinates of pickpoint used to select the entity.</p>
<p>For example consider the following case by constructing a circle at point (0,0,5) and radius 5 on WCS XY-plane with top view.<br />On selecting a point on circle using Editor.GetEntity, PromptEntityResult.PickedPoint returns : <strong>(3.50064192661003 , 3.35618169844321 , 0)</strong></p>
<p>To get all the co-ordinates,it may be useful to change the point to nearest snap mode using<strong> Editor.Snap </strong>that returns the Z co-ordinate as well.&#0160;</p>
<p><span style="text-decoration: underline;"><strong>Code:</strong></span></p>
<pre style="background: #000; color: #f8f8f8;">[CommandMethod(<span style="color: #65b042;">&quot;getEntityPickPoint&quot;</span>)]
<span style="color: #e28964;">public</span> <span style="color: #e28964;">static</span> <span style="color: #dad085;">void</span> getEntityPickPoint()
{
    Document doc <span style="color: #e28964;">=</span> <span style="color: #9b859d;">Application</span>.DocumentManager.MdiActiveDocument;
    Database db <span style="color: #e28964;">=</span> doc.Database;
    Editor ed <span style="color: #e28964;">=</span> doc.Editor;

    PromptEntityOptions peo <span style="color: #e28964;">=</span> <span style="color: #e28964;">new</span> PromptEntityOptions(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span>Select Entity&quot;</span>);
    PromptEntityResult per <span style="color: #e28964;">=</span> ed.GetEntity(peo);
    <span style="color: #e28964;">if</span> (per.Status <span style="color: #e28964;">=</span><span style="color: #e28964;">=</span> PromptStatus.OK)
    {
        Point3d pickedPoint <span style="color: #e28964;">=</span> per.PickedPoint;
        Point3d pickedPtOsnap <span style="color: #e28964;">=</span> ed.Snap(<span style="color: #65b042;">&quot;_near&quot;</span>, pickedPoint);

        ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> Selected pick point: {0}&quot;</span>, pickedPoint.ToString());
        ed.WriteMessage(<span style="color: #65b042;">&quot;<span style="color: #ddf2a4;">\n</span> Selected Picked Point Osnap near: {0}&quot;</span>, pickedPtOsnap);
    }
}

</pre>
<p><span style="text-decoration: underline;"><strong>Result</strong></span>:</p>
<pre class="cscode"><code>Command: <strong>GETENTITYPICKPOINT</strong>
Select Entity:
Selected pick point: <strong>(3.50064192661003,3.35618169844321,0)</strong>
Selected Picked Point Osnap near: <strong>(3.60921996485993,3.46027907043009,5)
</strong></code></pre>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><strong>More about object osnaps : </strong></span><br /><a href="https://knowledge.autodesk.com/de/search-result/caas/CloudHelp/cloudhelp/2017/DEU/AutoCAD-AutoLISP/files/GUID-4EEE5488-01D8-454F-9386-79E493E55D6E-htm.html">https://knowledge.autodesk.com/de/search-result/caas/CloudHelp/cloudhelp/2017/DEU/AutoCAD-AutoLISP/files/GUID-4EEE5488-01D8-454F-9386-79E493E55D6E-htm.html</a></p>
