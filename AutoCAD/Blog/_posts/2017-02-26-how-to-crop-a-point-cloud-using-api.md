---
layout: "post"
title: "How to Crop a Point Cloud using API"
date: "2017-02-26 23:56:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/02/how-to-crop-a-point-cloud-using-api.html "
typepad_basename: "how-to-crop-a-point-cloud-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>In this post we will discuss about cropping a point cloud, first we will look at how we can attach a point cloud from *.rcs which is an Autodesk Recap Scan File.</p> <p>Later, in the code we will crop the attached Point Cloud, I will add two types of cropping boundary namely, Rectangular and Circular.</p> <p>The core logic of cropping is to ensure giving the proper cropping plane and cropping points that fits within cropping boundary.</p> <p>The other elements in API used is self explanatory.</p><pre style="background: #ffffff; color: #000000"><span style="font-weight: bold; color: #800000">namespace</span> PointCloudData
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">class</span> Class1
<span style="color: #800080">{</span>
    <span style="color: #696969">//Get RCP file from Solution Directory</span>
    <span style="font-weight: bold; color: #800000">private</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">string</span> getPCFile<span style="color: #808030">(</span><span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        <span style="color: #696969">/*</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SolutionFolder</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │   </span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├───bin</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └───Debug</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; assembly.dll</span>
<span style="color: #696969"></span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; */</span>
        <span style="font-weight: bold; color: #800000">var</span> assemblyLoc <span style="color: #808030">=</span>
                Assembly<span style="color: #808030">.</span>GetExecutingAssembly<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">.</span>Location<span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">var</span> debugFolder <span style="color: #808030">=</span> 
                Path<span style="color: #808030">.</span>GetDirectoryName<span style="color: #808030">(</span>assemblyLoc<span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">var</span> binFolder <span style="color: #808030">=</span> 
                Path<span style="color: #808030">.</span>GetDirectoryName<span style="color: #808030">(</span>debugFolder<span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">var</span> solutionFolder <span style="color: #808030">=</span> 
                Path<span style="color: #808030">.</span>GetDirectoryName<span style="color: #808030">(</span>binFolder<span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">var</span> FilePath <span style="color: #808030">=</span> 
                solutionFolder <span style="color: #808030">+</span>
                       <span style="color: #800000">"</span><span style="color: #0000e6">\\MyOfficeRoom.rcs</span><span style="color: #800000">"</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">return</span> FilePath<span style="color: #800080">;</span>
    <span style="color: #800080">}</span>
    <span style="color: #696969">//Write to Editor</span>
    <span style="font-weight: bold; color: #800000">private</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> Report<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">string</span> v<span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        Editor ed <span style="color: #808030">=</span> Application<span style="color: #808030">.</span>DocumentManager<span style="color: #808030">.</span>MdiActiveDocument<span style="color: #808030">.</span>Editor<span style="color: #800080">;</span>
        ed<span style="color: #808030">.</span>WriteMessage<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">\n</span><span style="color: #800000">"</span> <span style="color: #808030">+</span> v<span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="color: #800080">}</span>

    <span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">static</span> ObjectId AttachPointCloudEx<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">string</span> filename<span style="color: #808030">,</span>
                                              <span style="font-weight: bold; color: #800000">double</span> scale<span style="color: #808030">,</span> 
                                              <span style="font-weight: bold; color: #800000">double</span> rotation<span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        Point3d location <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Point3d<span style="color: #808030">(</span><span style="color: #008c00">0</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        Database db <span style="color: #808030">=</span> HostApplicationServices<span style="color: #808030">.</span>WorkingDatabase<span style="color: #800080">;</span>
        ObjectId objid <span style="color: #808030">=</span> PointCloudEx<span style="color: #808030">.</span>AttachPointCloud<span style="color: #808030">(</span>filename<span style="color: #808030">,</span>
                                                        location<span style="color: #808030">,</span>
                                                        scale<span style="color: #808030">,</span>
                                                        rotation<span style="color: #808030">,</span>
                                                        db<span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>objid<span style="color: #808030">.</span>IsNull<span style="color: #808030">)</span>
            Report<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">\nFail to get object id of attach point cloud</span><span style="color: #800000">"</span><span style="color: #808030">+</span>
                <span style="color: #800000">"</span><span style="color: #0000e6">by PointCloudEx.AttacPointCloud API</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

        <span style="font-weight: bold; color: #800000">return</span> objid<span style="color: #800080">;</span>

    <span style="color: #800080">}</span>
    
    <span style="color: #696969">//Cropping Logic</span>
    <span style="color: #696969">//</span>
    <span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">static</span> ErrorStatus addcropping<span style="color: #808030">(</span>PointCloudEx pointcloud<span style="color: #808030">,</span>
                                          PointCloudCropType type<span style="color: #808030">,</span> 
                                          Point3d pt1<span style="color: #808030">,</span> Point3d pt2<span style="color: #808030">,</span>
                                          <span style="font-weight: bold; color: #800000">bool</span> bInside<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">bool</span> bInverted<span style="color: #808030">)</span>
    <span style="color: #800080">{</span>

        PointCloudCrop crop <span style="color: #808030">=</span> PointCloudCrop<span style="color: #808030">.</span>Create<span style="color: #808030">(</span>IntPtr<span style="color: #808030">.</span>Zero<span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>crop <span style="color: #808030">=</span><span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">)</span>
            Report<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">\nFail to create crop by pointcloudcrop.Create method</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        Point3dCollection points <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Point3dCollection<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        points<span style="color: #808030">.</span>Add<span style="color: #808030">(</span>pt1<span style="color: #808030">)</span><span style="color: #800080">;</span>
        points<span style="color: #808030">.</span>Add<span style="color: #808030">(</span>pt2<span style="color: #808030">)</span><span style="color: #800080">;</span>
        crop<span style="color: #808030">.</span>Vertices <span style="color: #808030">=</span> points<span style="color: #800080">;</span>
        <span style="color: #696969">//In SW Isometric View or OrthoGraphic View</span>
        Vector3d norm <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Vector3d<span style="color: #808030">(</span><span style="color: #008c00">0</span><span style="color: #808030">,</span> <span style="color: #008c00">1</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>           
        Plane cropPlane <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Plane<span style="color: #808030">(</span>pt1<span style="color: #808030">,</span> norm<span style="color: #808030">)</span><span style="color: #800080">;</span>
        crop<span style="color: #808030">.</span>CropPlane <span style="color: #808030">=</span> cropPlane<span style="color: #800080">;</span>
        crop<span style="color: #808030">.</span>CropType <span style="color: #808030">=</span> type<span style="color: #800080">;</span>
        crop<span style="color: #808030">.</span>Inside <span style="color: #808030">=</span> bInside<span style="color: #800080">;</span>
        crop<span style="color: #808030">.</span>Inverted <span style="color: #808030">=</span> bInverted<span style="color: #800080">;</span>
        pointcloud<span style="color: #808030">.</span>addCroppingBoundary<span style="color: #808030">(</span>crop<span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">return</span> ErrorStatus<span style="color: #808030">.</span>OK<span style="color: #800080">;</span>
    <span style="color: #800080">}</span>

       
    <span style="color: #808030">[</span>CommandMethod<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">CropPC</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #808030">]</span>
    <span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> CropPC<span style="color: #808030">(</span><span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        <span style="color: #696969">//attach point cloud into the drawing</span>
        Database db <span style="color: #808030">=</span> 
            HostApplicationServices<span style="color: #808030">.</span>WorkingDatabase<span style="color: #800080">;</span>
        ObjectId objid <span style="color: #808030">=</span> 
            AttachPointCloudEx<span style="color: #808030">(</span>getPCFile<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">,</span> <span style="color: #008000">1.0</span><span style="color: #808030">,</span> <span style="color: #008000">0.0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        Editor ed <span style="color: #808030">=</span> 
            Application<span style="color: #808030">.</span>DocumentManager<span style="color: #808030">.</span>MdiActiveDocument<span style="color: #808030">.</span>Editor<span style="color: #800080">;</span>
        <span style="color: #696969">//get the point cloud object</span>
        <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction t <span style="color: #808030">=</span> db<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
        <span style="color: #800080">{</span>
            ViewTableRecord activeVTR <span style="color: #808030">=</span> ed<span style="color: #808030">.</span>GetCurrentView<span style="color: #808030">(</span><span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">as</span> ViewTableRecord<span style="color: #800080">;</span>
            <span style="color: #696969">//Set VS Preset to Realistic, in Wireframe PC Attach is not supported.</span>
            DBDictionary dict <span style="color: #808030">=</span> t<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>db<span style="color: #808030">.</span>VisualStyleDictionaryId<span style="color: #808030">,</span>
                                OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">as</span> DBDictionary<span style="color: #800080">;</span>
            activeVTR<span style="color: #808030">.</span>VisualStyleId <span style="color: #808030">=</span> dict<span style="color: #808030">.</span>GetAt<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">Realistic</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            ed<span style="color: #808030">.</span>SetCurrentView<span style="color: #808030">(</span>activeVTR<span style="color: #808030">)</span><span style="color: #800080">;</span>
            ed<span style="color: #808030">.</span>UpdateTiledViewportsFromDatabase<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                

            PointCloudEx pcloudex <span style="color: #808030">=</span> 
                t<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>objid<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForWrite<span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">as</span> PointCloudEx<span style="color: #800080">;</span>

                             
            <span style="color: #696969">//Add two cropping to the point cloud</span>
            Point3d crop1_pt1 <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Point3d<span style="color: #808030">(</span><span style="color: #808030">-</span><span style="color: #008000">7.0</span><span style="color: #808030">,</span> <span style="color: #008000">12.0</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            Point3d crop1_pt2 <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Point3d<span style="color: #808030">(</span><span style="color: #808030">-</span><span style="color: #008000">4.0</span><span style="color: #808030">,</span> <span style="color: #008000">9.0</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            Point3d crop2_pt1 <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Point3d<span style="color: #808030">(</span><span style="color: #008000">3.5</span><span style="color: #808030">,</span> <span style="color: #008000">15.0</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            Point3d crop2_pt2 <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Point3d<span style="color: #808030">(</span><span style="color: #008000">6.0</span><span style="color: #808030">,</span> <span style="color: #008000">13.0</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #696969">/*Cropping PC in not supported in PerspectiveView*/</span>
            <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>activeVTR<span style="color: #808030">.</span>PerspectiveEnabled<span style="color: #808030">)</span>
            <span style="color: #800080">{</span>
                activeVTR<span style="color: #808030">.</span>PerspectiveEnabled <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #800080">;</span>
            <span style="color: #800080">}</span>
            <span style="color: #696969">//Rectangle - first corner point, 2nd, 3rd, 4th and first corner point again. </span>
            addcropping<span style="color: #808030">(</span>pcloudex<span style="color: #808030">,</span> 
                        PointCloudCropType<span style="color: #808030">.</span>Rectangular<span style="color: #808030">,</span> 
                        crop1_pt1<span style="color: #808030">,</span> crop1_pt2<span style="color: #808030">,</span>
                        <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #696969">//Circular - there are 2 points, center point and any point which is on the circle's circumference. </span>
            addcropping<span style="color: #808030">(</span>pcloudex<span style="color: #808030">,</span> 
                        PointCloudCropType<span style="color: #808030">.</span>Circular<span style="color: #808030">,</span> 
                        crop2_pt1<span style="color: #808030">,</span> crop2_pt2<span style="color: #808030">,</span>
                        <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

            <span style="color: #696969">//show Cropping</span>
            pcloudex<span style="color: #808030">.</span>ShowCropped <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #800080">;</span>
            <span style="color: #696969">//Now lets us traversing list of croppings</span>
            <span style="font-weight: bold; color: #800000">for</span> <span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">int</span> Index <span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #800080">;</span> Index <span style="color: #808030">&lt;</span> pcloudex<span style="color: #808030">.</span>getCroppingCount<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span> Index<span style="color: #808030">+</span><span style="color: #808030">+</span><span style="color: #808030">)</span>
            <span style="color: #800080">{</span>

                <span style="color: #696969">//Retrieving Croptype</span>
                PointCloudCrop tmpcrop <span style="color: #808030">=</span> pcloudex<span style="color: #808030">.</span>getPointCloudCropping<span style="color: #808030">(</span>Index<span style="color: #808030">)</span><span style="color: #800080">;</span>
                Report<span style="color: #808030">(</span>tmpcrop<span style="color: #808030">.</span>CropType<span style="color: #808030">.</span>ToString<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #800080">}</span>

            <span style="color: #696969">//Zoom the View to PC Extents</span>
            <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>ViewTableRecord vtr <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> ViewTableRecord<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
            <span style="color: #800080">{</span>
                vtr<span style="color: #808030">.</span>CenterPoint <span style="color: #808030">=</span> 
                    <span style="font-weight: bold; color: #800000">new</span> Point2d<span style="color: #808030">(</span>pcloudex<span style="color: #808030">.</span>Location<span style="color: #808030">.</span>X<span style="color: #808030">,</span> pcloudex<span style="color: #808030">.</span>Location<span style="color: #808030">.</span>Z<span style="color: #808030">)</span><span style="color: #800080">;</span>
                Extents3d extents <span style="color: #808030">=</span> 
                    pcloudex<span style="color: #808030">.</span>GeomExtents<span style="color: #800080">;</span>
                Point2d min2d <span style="color: #808030">=</span> 
                    <span style="font-weight: bold; color: #800000">new</span> Point2d<span style="color: #808030">(</span>extents<span style="color: #808030">.</span>MinPoint<span style="color: #808030">.</span>X<span style="color: #808030">,</span> extents<span style="color: #808030">.</span>MinPoint<span style="color: #808030">.</span>Y<span style="color: #808030">)</span><span style="color: #800080">;</span>
                Point2d max2d <span style="color: #808030">=</span>
                    <span style="font-weight: bold; color: #800000">new</span> Point2d<span style="color: #808030">(</span>extents<span style="color: #808030">.</span>MaxPoint<span style="color: #808030">.</span>X<span style="color: #808030">,</span> extents<span style="color: #808030">.</span>MaxPoint<span style="color: #808030">.</span>Y<span style="color: #808030">)</span><span style="color: #800080">;</span>
                vtr<span style="color: #808030">.</span>Height <span style="color: #808030">=</span> 
                    max2d<span style="color: #808030">.</span>Y <span style="color: #808030">-</span> min2d<span style="color: #808030">.</span>Y<span style="color: #800080">;</span>
                vtr<span style="color: #808030">.</span>Width <span style="color: #808030">=</span>
                    max2d<span style="color: #808030">.</span>X <span style="color: #808030">-</span> min2d<span style="color: #808030">.</span>X<span style="color: #800080">;</span>
                vtr<span style="color: #808030">.</span>Target <span style="color: #808030">=</span> 
                    activeVTR<span style="color: #808030">.</span>Target<span style="color: #800080">;</span>
                                       
                ed<span style="color: #808030">.</span>SetCurrentView<span style="color: #808030">(</span>vtr<span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #800080">}</span>
                
            t<span style="color: #808030">.</span>Commit<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

        <span style="color: #800080">}</span>
           
    <span style="color: #800080">}</span>

        

    <span style="color: #800080">}</span>
<span style="color: #800080">}</span>
</pre><iframe height="655" src="https://screencast.autodesk.com/Embed/Timeline/525f5997-5cfe-4ad6-be10-a876ba358647?t=0m00s" frameborder="0" width="696" webkitallowfullscreen allowfullscreen></iframe>
<p>&nbsp;</p>
<p>Source project can be downloaded from <a href="https://github.com/MadhukarMoogala/MyBlogs/blob/master/PointCloudData/PointCloudData.zip" target="_blank">here</a></p>
<p>Note: I intentionally didn’t upload Pointcloud file (*.rcs), as it is bulky size.</p>
