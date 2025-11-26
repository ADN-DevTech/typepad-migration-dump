---
layout: "post"
title: "Profile Geometry Points on Alignment Label Styles"
date: "2016-03-01 07:19:49"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2016/03/profile-geometry-points-on-alignment-label-styles.html "
typepad_basename: "profile-geometry-points-on-alignment-label-styles"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>The Civil 3D Styles collection is big, really big. Under <strong>CivilDocument.Styles</strong> we can read most of the information, but it can be tricky to dig it. This post show how to read Profile Geometry Points, as shown at the image below, for Alignment Label Set style.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c11d33970d-pi" style="display: inline;"><img alt="Style" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c11d33970d image-full img-responsive" src="/assets/image_cea689.jpg" title="Style" /></a></p>
<p>The basic idea for this code is to get the <strong>LabelSetStyles</strong>, then the collection of <strong>AlignmentLabelSetItem</strong>, filter by <strong>ProfileGeometryPoint</strong> and, finally, get the dictionary containing the <strong>ProfilePointType</strong> and a boolean that indicates if is checked or not. Note the dictionary&#0160;will only show checked items (true) and, if is not checked, it&#39;s not on the dictionary&#0160;(so no false items on the dictionary).</p>
<p></p>
<div>
<pre style="margin: 0; line-height: 125%;">[CommandMethod(&quot;labelStyleTest&quot;)]
<span style="color: #0000ff;">public</span> <span style="color: #0000ff;">static</span> <span style="color: #0000ff;">void</span> CmdLabelStyleTest()
{
  CivilDocument civilDoc = CivilApplication.ActiveDocument;
  Database db = Application.DocumentManager.MdiActiveDocument.Database;
  <span style="color: #0000ff;">using</span> (Transaction trans = db.TransactionManager.StartTransaction())
  {
    ObjectId alignLblSetStyleId = civilDoc.Styles.LabelSetStyles.
      AlignmentLabelSetStyles[<span style="color: #a31515;">&quot;Major and Minor only&quot;</span>]; <span style="color: #008000;">// or other name here</span>
    AlignmentLabelSetStyle alignlblsetstyle = trans.GetObject(
      alignLblSetStyleId, OpenMode.ForRead) <span style="color: #0000ff;">as</span> AlignmentLabelSetStyle;

    <span style="color: #0000ff;">foreach</span> (AlignmentLabelSetItem lblSetItem <span style="color: #0000ff;">in</span> alignlblsetstyle)
    {
      <span style="color: #008000;">// let&#39;s only treat Profile Geometry Point</span>
      <span style="color: #0000ff;">if</span> (lblSetItem.LabelStyleType !=
        LabelStyleType.AlignmentProfileGeometryPoint)
        <span style="color: #0000ff;">continue</span>; 

      <span style="color: #008000;">// this dictionary list should contain all marked (true)</span>
      Dictionary&lt;Autodesk.Civil.ProfilePointType, <span style="color: #2b91af;">bool</span>&gt; list =
        lblSetItem.GetLabeledProfileGeometryPoints();
    }
  }
}
</pre>
</div>
