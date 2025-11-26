---
layout: "post"
title: "Linetype Preview image using AccoreConsole"
date: "2015-10-25 23:07:50"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/10/linetype-preview-image-using-accoreconsole.html "
typepad_basename: "linetype-preview-image-using-accoreconsole"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you need a preview image for a linetype to show in a picture box or for any other purpose, AccoreConsole should come in handy. To get this working, pass-in a blank drawing to AccoreConsole and have your plugin that loads in AccoreConsole load the linetype and create lines associated with that linetype. After the lines have been created, generate a preview image by plotting it as PNG.</p>
<p>Here is a sample code for the above steps and the script file that loads in AccoreConsole :</p>
<p></p>
<pre>
[CommandMethod("LTypePreview")]
public void LTypePreviewMethod()
{
    Document activeDoc = 
    Autodesk.AutoCAD.ApplicationServices.Core.Application.
    DocumentManager.MdiActiveDocument;
    Editor ed = activeDoc.Editor;

    String ltypeName = "BATTING";
    String linetypeFilePath = @"D:\Temp\acdb.lin";
    Database db = activeDoc.Database;

    using (Transaction trans 
                = db.TransactionManager.StartTransaction())
    {
        BlockTable bt = trans.GetObject(
            db.BlockTableId, 
            OpenMode.ForRead) as BlockTable;

        BlockTableRecord ms = trans.GetObject(
            bt[BlockTableRecord.ModelSpace], 
            OpenMode.ForWrite) as BlockTableRecord;

        LinetypeTable lineTypeTable = 
            trans.GetObject(
            db.LinetypeTableId, 
            OpenMode.ForRead) as LinetypeTable;

        bool isLinetypeLoaded 
                = lineTypeTable.Has(ltypeName);
        if (!isLinetypeLoaded)
        {// Not  loaded, try  loading the linetype
            db.LoadLineTypeFile(ltypeName, linetypeFilePath);
        }

        double ltScale = 30;
        ObjectId ltypeId = lineTypeTable[ltypeName];
        double incrx = 64.0;
        double incry = 48.0;
        for (int cnt = 0; cnt < 10; cnt++)
        {
            if (cnt == 0)
            {
                Line line = new Line(
                    new Point3d(0.0, 0.0, 0.0), 
                    new Point3d(640.0, 480.0, 0.0));
                line.LinetypeScale = ltScale;
                line.LinetypeId = ltypeId;
                ms.AppendEntity(line);
                trans.AddNewlyCreatedDBObject(line, true);
            }
            else
            {
                Line line1 = new Line(
                    new Point3d(cnt * incrx, 0.0, 0.0), 
                    new Point3d(640.0, (10 - cnt) * incry,
                    0.0));
                line1.LinetypeScale = ltScale;
                line1.LinetypeId = ltypeId;
                ms.AppendEntity(line1);
                trans.AddNewlyCreatedDBObject(line1, true);

                Line line2 = new Line(
                    new Point3d(0.0, cnt * incry, 0.0), 
                    new Point3d((10 - cnt) * incrx, 
                    480.0, 0.0));
                line2.LinetypeScale = ltScale;
                line2.LinetypeId = ltypeId;
                ms.AppendEntity(line2);
                trans.AddNewlyCreatedDBObject(line2, true);
            }
        }
        trans.Commit();
    }

    if (System.IO.File.Exists("D:\\Temp\\Test.png"))
    {
        System.IO.File.Delete("D:\\Temp\\Test.png");
    }

    string deviceName = "PublishToWeb PNG.pc3";
    string mediaName = "VGA (640.00 x 480.00 Pixels)";

    using (Transaction tr 
            = db.TransactionManager.StartTransaction())
    {
        BlockTableRecord btr 
            = tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead)
            as BlockTableRecord;
        Layout lo = 
            tr.GetObject(btr.LayoutId, OpenMode.ForRead) 
            as Layout;

        PlotInfo pi = new PlotInfo();
        pi.Layout = btr.LayoutId;

        PlotSettings ps = new PlotSettings(lo.ModelType);
        PlotConfigInfo pci = new PlotConfigInfo();

        ps.CopyFrom(lo);
        ps.PlotPlotStyles = true;

        Extents2d windowpos 
            = new Extents2d(0.0, 0.0, 640.0, 480.0);
        PlotSettingsValidator psv 
            = PlotSettingsValidator.Current;
        psv.SetPlotWindowArea(ps, windowpos);
        psv.SetPlotType(ps, PlotType.Window);
        psv.SetUseStandardScale(ps, true);
        ps.PrintLineweights = true;
        psv.SetPlotConfigurationName
            (ps, deviceName, 
            mediaName.Replace(" ", "_"));
        psv.SetPlotCentered(ps, true);
        psv.SetStdScaleType(ps, 
            StdScaleType.ScaleToFit);
        psv.RefreshLists(ps);
        pi.OverrideSettings = ps;
        PlotInfoValidator piv 
            = new PlotInfoValidator();
        piv.Validate(pi);
        using (var pe 
            = PlotFactory.CreatePublishEngine())
        {
            pe.BeginPlot(null, null);
            pe.BeginDocument(pi, activeDoc.Name, 
                null, 1, true, "D:\\Temp\\Test.png");
            PlotPageInfo ppi = new PlotPageInfo();
            pe.BeginPage(ppi, pi, true, null);
            pe.BeginGenerateGraphics(null);
            pe.EndGenerateGraphics(null);
            pe.EndPage(null);
            pe.EndDocument(null);
            pe.EndPlot(null);
        }
        tr.Commit();
    }
}
</pre>
<p></p>
<p>Here are a few sample linetype preview images generated using AccoreConsole :</p>
<p></p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0886d620970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0886d620970d img-responsive" alt="Preview1" title="Preview1" src="/assets/image_745510.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16cd4d7970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d16cd4d7970c img-responsive" alt="Preview2" title="Preview2" src="/assets/image_419299.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0886d63e970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0886d63e970d img-responsive" alt="Preview3" title="Preview3" src="/assets/image_668586.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
