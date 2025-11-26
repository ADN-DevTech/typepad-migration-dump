---
layout: "post"
title: "Set origin while creating a hatch using .NET"
date: "2018-05-04 04:00:00"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2018/05/set-origin-while-creating-a-hatch-using-net.html "
typepad_basename: "set-origin-while-creating-a-hatch-using-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>We had an issue raised by a customer regarding setting origin(image) during hatch creation.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03718bd200d-pi"><img alt="OriginHatchInUI" class="asset  asset-image at-xid-6a0167607c2431970b0224e03718bd200d img-responsive" src="/assets/image_754439.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="OriginHatchInUI" /></a></p>
<p>It was found that origin of hatch has to be set in a transaction other than the one in which it is created for it to work correctly. <br />Below code can be used for testing :&#0160;</p>
<pre style="background: #000; color: #f8f8f8;">[CommandMethod(<span style="color: #65b042;">&quot;setOrginHatch&quot;</span>)]
public <span style="color: #99cf50;">void</span> <span style="color: #89bdff;">setOriginHatch</span>()
{
    Document doc = Application<span style="color: #3e87e3;">.DocumentManager</span><span style="color: #3e87e3;">.MdiActiveDocument</span>;
    Database db = doc<span style="color: #3e87e3;">.Database</span>;
    Editor ed = doc<span style="color: #3e87e3;">.Editor</span>;

    ObjectId mHatchId;
    Hatch mHatch = new Hatch();
    using (Transaction tr1 = db<span style="color: #3e87e3;">.TransactionManager</span>.<span style="color: #dad085;">StartTransaction</span>())
    {
        BlockTable bt = (BlockTable)tr1.<span style="color: #dad085;">GetObject</span>(doc<span style="color: #3e87e3;">.Database</span><span style="color: #3e87e3;">.BlockTableId</span>, OpenMode<span style="color: #3e87e3;">.ForRead</span>);
        BlockTableRecord btr = (BlockTableRecord)tr1.<span style="color: #dad085;">GetObject</span>(db<span style="color: #3e87e3;">.CurrentSpaceId</span>, OpenMode<span style="color: #3e87e3;">.ForWrite</span>);

        Point2d pt = new Point2d(0, 0);
        Polyline mPolyline = new Polyline(4);
        mPolyline.<span style="color: #dad085;">AddVertexAt</span>(0, pt, 0.0, -1.0, -1.0);
        mPolyline<span style="color: #3e87e3;">.Normal</span> = Vector3d<span style="color: #3e87e3;">.ZAxis</span>;
        mPolyline.<span style="color: #dad085;">AddVertexAt</span>(1, new Point2d(pt<span style="color: #3e87e3;">.X</span> + 10, pt<span style="color: #3e87e3;">.Y</span>), 0.0, -1.0, -1.0);
        mPolyline.<span style="color: #dad085;">AddVertexAt</span>(2, new Point2d(pt<span style="color: #3e87e3;">.X</span> + 10, pt<span style="color: #3e87e3;">.Y</span> + 5), 0.0, -1.0, -1.0);
        mPolyline.<span style="color: #dad085;">AddVertexAt</span>(3, new Point2d(pt<span style="color: #3e87e3;">.X</span>, pt<span style="color: #3e87e3;">.Y</span> + 5), 0.0, -1.0, -1.0);
        mPolyline<span style="color: #3e87e3;">.Closed</span> = true;

        ObjectId mPlineId;
        mPlineId = btr.<span style="color: #dad085;">AppendEntity</span>(mPolyline);
        tr1.<span style="color: #dad085;">AddNewlyCreatedDBObject</span>(mPolyline, true);

        ObjectIdCollection ObjIds = new ObjectIdCollection();
        ObjIds.<span style="color: #dad085;">Add</span>(mPlineId);

        Vector3d normal = new Vector3d(0.0, 0.0, 1.0);
        mHatch<span style="color: #3e87e3;">.Normal</span> = normal;
        mHatch<span style="color: #3e87e3;">.Elevation</span> = 0.0;
        mHatch<span style="color: #3e87e3;">.PatternScale</span> = 2.0;
        mHatch.<span style="color: #dad085;">SetHatchPattern</span>(HatchPatternType<span style="color: #3e87e3;">.PreDefined</span>, &quot;NET&quot;);
        mHatch<span style="color: #3e87e3;">.ColorIndex</span> = 1;
        mHatch<span style="color: #3e87e3;">.PatternAngle</span> = 2;

        //trying to set origin here does not work 
        //Point2d setOrigin = new Point2d(6.698, 2.78);
        //mHatch<span style="color: #3e87e3;">.Origin</span> = setOrigin;

        btr.<span style="color: #dad085;">AppendEntity</span>(mHatch);
        tr1.<span style="color: #dad085;">AddNewlyCreatedDBObject</span>(mHatch, true);

        mHatch<span style="color: #3e87e3;">.Associative</span> = true;
        mHatch.<span style="color: #dad085;">AppendLoop</span>(HatchLoopTypes<span style="color: #3e87e3;">.Outermost</span>, ObjIds);
        mHatch.<span style="color: #dad085;">EvaluateHatch</span>(true);

        //get the ObjectId of hatch 
        mHatchId = mHatch<span style="color: #3e87e3;">.ObjectId</span>;

        tr1.<span style="color: #dad085;">Commit</span>();
    }
    //to set the origin use another transaction 
    using (Transaction tr2 = doc<span style="color: #3e87e3;">.TransactionManager</span>.<span style="color: #dad085;">StartTransaction</span>())
    {
        Entity ent = (Entity)tr2.<span style="color: #dad085;">GetObject</span>(mHatchId, OpenMode<span style="color: #3e87e3;">.ForWrite</span>);
        if (ent != null)
        {
            Hatch nHatch = ent as Hatch;
            String hatchName = nHatch<span style="color: #3e87e3;">.PatternName</span>;
            Point2d setOrigin = new Point2d(6.698, 2.78);
            nHatch<span style="color: #3e87e3;">.Origin</span> = setOrigin;
            nHatch.<span style="color: #dad085;">SetHatchPattern</span>(HatchPatternType<span style="color: #3e87e3;">.PreDefined</span>, hatchName);
            nHatch.<span style="color: #dad085;">EvaluateHatch</span>(true);
            nHatch.<span style="color: #dad085;">Draw</span>();
        }
        tr2.<span style="color: #dad085;">Commit</span>();
    }
}
</pre>
