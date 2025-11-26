---
layout: "post"
title: "StationOffsetLabel.Create quick sample code"
date: "2016-05-05 11:22:40"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2016/05/stationoffsetlabelcreate-quick-sample-code.html "
typepad_basename: "stationoffsetlabelcreate-quick-sample-code"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>This is a quick sample code on how call StationOffsetLabel.Create method for all Alignments on each Station increment interval. Note it&#39;s using the first StationOffsetLabelStyle and first MarkerStyle from the respective style collections.</p>

<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #7d9029;">[CommandMethod(&quot;addStationOffsetLabel&quot;)]</span>
<span style="color: #008000; font-weight: bold;">public</span> <span style="color: #008000; font-weight: bold;">static</span> <span style="color: #008000; font-weight: bold;">void</span> <span style="color: #0000ff;">CmdAddStationOffsetLabel</span>()
{
  CivilDocument civilDoc = CivilApplication.ActiveDocument;
  Database db = Application.DocumentManager.MdiActiveDocument.Database;
  <span style="color: #008000; font-weight: bold;">using</span> (Transaction trans = db.TransactionManager.StartTransaction())
  {
    <span style="color: #008000; font-weight: bold;">foreach</span> (ObjectId alignId <span style="color: #008000; font-weight: bold;">in</span> civilDoc.GetSitelessAlignmentIds())
    {
      Alignment align = trans.GetObject(alignId, OpenMode.ForRead) <span style="color: #008000; font-weight: bold;">as</span> Alignment;

      <span style="color: #008000; font-weight: bold;">for</span> (<span style="color: #b00040;">double</span> s = align.StartingStation; s &lt; align.EndingStation; s+= align.StationIndexIncrement)
      {
        <span style="color: #b00040;">double</span> easting = <span style="color: #666666;">0</span>;
        <span style="color: #b00040;">double</span> northing = <span style="color: #666666;">0</span>;
        align.PointLocation(s, <span style="color: #666666;">0</span>, <span style="color: #008000; font-weight: bold;">ref</span> easting, <span style="color: #008000; font-weight: bold;">ref</span> northing);

        Point2d p = <span style="color: #008000; font-weight: bold;">new</span> Point2d(easting, northing);
        StationOffsetLabel.Create(
          alignId, 
          civilDoc.Styles.LabelStyles.AlignmentLabelStyles.StationOffsetLabelStyles[<span style="color: #666666;">0</span>], 
          civilDoc.Styles.MarkerStyles[<span style="color: #666666;">0</span>], 
          p);
      }
    }
    trans.Commit();
  }
}
</pre>
</div>
