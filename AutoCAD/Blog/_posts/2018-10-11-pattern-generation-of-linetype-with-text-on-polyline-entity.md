---
layout: "post"
title: "Pattern generation of linetype with text on polyline entity"
date: "2018-10-11 04:48:26"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "AutoCAD"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2018/10/pattern-generation-of-linetype-with-text-on-polyline-entity.html "
typepad_basename: "pattern-generation-of-linetype-with-text-on-polyline-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Generating linetype with text on a Polyline entity could result in&#0160;linetype pattern generated continuously across all vertices as below image. This is because the linetype generation property of Polyline is ON&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b7c358200b-pi" style="float: left;"><img alt="Overflow1" class="asset  asset-image at-xid-6a0167607c2431970b022ad3b7c358200b img-responsive" height="260" src="/assets/image_216988.jpg" style="margin: 0px 5px 5px 0px;" title="Overflow1" width="306" /></a></p>
<p>Interactively this can be changed by issuing PEDIT command, setting&#0160;Ltype gen as Off.</p>
<p><strong><em>Command: PEDIT</em></strong><br /><strong><em>Enter an option [Open/Join/Width/Edit vertex/Fit/Spline/Decurve/Ltype gen/Reverse/Undo]: L</em></strong><br /><strong><em>Enter polyline linetype generation option [ON/OFF] &lt;On&gt;: OFF</em></strong></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Via .NET API, Polyline entity has no property to change this. Thanks to Polyline2d.LinetypeGenerationOn property, we can convert the Polyline entity to Polyline2d and set this property false. Code sample shown below with the output image.&#0160;&#0160;</p>
<p>&#0160;</p>
<pre style="color: #d1d1d1; background: #000000;"><span style="color: #9999a9;">// code modified from the link </span>
<span style="color: #9999a9;">//</span><span style="color: #6070ec;">http://through-the-interface.typepad.com/through_the_interface/2008/01/creating-a-comp.html</span>
<span style="color: #d2cd86;">[</span>CommandMethod<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">CCL</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">]</span>
<span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">void</span> CreateComplexLinetype<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span>
<span style="color: #b060b0;">{</span>
    Document doc <span style="color: #d2cd86;">=</span>
        Application<span style="color: #d2cd86;">.</span>DocumentManager<span style="color: #d2cd86;">.</span>MdiActiveDocument<span style="color: #b060b0;">;</span>
    Database db <span style="color: #d2cd86;">=</span> doc<span style="color: #d2cd86;">.</span>Database<span style="color: #b060b0;">;</span>
    Editor ed <span style="color: #d2cd86;">=</span> doc<span style="color: #d2cd86;">.</span>Editor<span style="color: #b060b0;">;</span>
    Transaction tr <span style="color: #d2cd86;">=</span>
        db<span style="color: #d2cd86;">.</span>TransactionManager<span style="color: #d2cd86;">.</span>StartTransaction<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
    <span style="color: #e66170; font-weight: bold;">using</span> <span style="color: #d2cd86;">(</span>tr<span style="color: #d2cd86;">)</span>
    <span style="color: #b060b0;">{</span>
        TextStyleTable tt <span style="color: #d2cd86;">=</span>
            <span style="color: #d2cd86;">(</span>TextStyleTable<span style="color: #d2cd86;">)</span>tr<span style="color: #d2cd86;">.</span>GetObject<span style="color: #d2cd86;">(</span>
            db<span style="color: #d2cd86;">.</span>TextStyleTableId<span style="color: #d2cd86;">,</span>
            OpenMode<span style="color: #d2cd86;">.</span>ForRead
            <span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        LinetypeTable lt <span style="color: #d2cd86;">=</span>
            <span style="color: #d2cd86;">(</span>LinetypeTable<span style="color: #d2cd86;">)</span>tr<span style="color: #d2cd86;">.</span>GetObject<span style="color: #d2cd86;">(</span>
            db<span style="color: #d2cd86;">.</span>LinetypeTableId<span style="color: #d2cd86;">,</span>
            OpenMode<span style="color: #d2cd86;">.</span>ForWrite
            <span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        LinetypeTableRecord ltr <span style="color: #d2cd86;">=</span>
            <span style="color: #e66170; font-weight: bold;">new</span> LinetypeTableRecord<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>

        ltr<span style="color: #d2cd86;">.</span>Name <span style="color: #d2cd86;">=</span> <span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">COLD_WATER_SUPPLY</span><span style="color: #02d045;">&quot;</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>AsciiDescription <span style="color: #d2cd86;">=</span>
            <span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">Cold water supply ---- CW ---- CW ---- CW ----</span><span style="color: #02d045;">&quot;</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>PatternLength <span style="color: #d2cd86;">=</span> <span style="color: #009f00;">0.9</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>NumDashes <span style="color: #d2cd86;">=</span> <span style="color: #008c00;">3</span><span style="color: #b060b0;">;</span>
        <span style="color: #9999a9;">// Dash #1</span>
        ltr<span style="color: #d2cd86;">.</span>SetDashLengthAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #009f00;">0.5</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        <span style="color: #9999a9;">// Dash #2</span>
        ltr<span style="color: #d2cd86;">.</span>SetDashLengthAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span><span style="color: #d2cd86;">-</span><span style="color: #009f00;">0.2</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>SetShapeStyleAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> tt<span style="color: #d2cd86;">[</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">Standard</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">]</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>SetShapeNumberAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>SetShapeScaleAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> <span style="color: #009f00;">0.1</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>SetTextAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> <span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">CW</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>SetShapeRotationAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        ltr<span style="color: #d2cd86;">.</span>SetShapeOffsetAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">new</span> Vector2d<span style="color: #d2cd86;">(</span><span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #d2cd86;">-</span><span style="color: #009f00;">0.05</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        <span style="color: #9999a9;">// Dash #3</span>
        ltr<span style="color: #d2cd86;">.</span>SetDashLengthAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">2</span><span style="color: #d2cd86;">,</span> <span style="color: #d2cd86;">-</span><span style="color: #009f00;">0.2</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>

        <span style="color: #9999a9;">// Add the new linetype to the linetype table</span>
        ObjectId ltId <span style="color: #d2cd86;">=</span> lt<span style="color: #d2cd86;">.</span>Add<span style="color: #d2cd86;">(</span>ltr<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>                
        tr<span style="color: #d2cd86;">.</span>AddNewlyCreatedDBObject<span style="color: #d2cd86;">(</span>ltr<span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        <span style="color: #9999a9;">// Create a test line with this linetype</span>
        BlockTable bt <span style="color: #d2cd86;">=</span>
            <span style="color: #d2cd86;">(</span>BlockTable<span style="color: #d2cd86;">)</span>tr<span style="color: #d2cd86;">.</span>GetObject<span style="color: #d2cd86;">(</span>
            db<span style="color: #d2cd86;">.</span>BlockTableId<span style="color: #d2cd86;">,</span>
            OpenMode<span style="color: #d2cd86;">.</span>ForRead
            <span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        BlockTableRecord btr <span style="color: #d2cd86;">=</span>
            <span style="color: #d2cd86;">(</span>BlockTableRecord<span style="color: #d2cd86;">)</span>tr<span style="color: #d2cd86;">.</span>GetObject<span style="color: #d2cd86;">(</span>
            bt<span style="color: #d2cd86;">[</span>BlockTableRecord<span style="color: #d2cd86;">.</span>ModelSpace<span style="color: #d2cd86;">]</span><span style="color: #d2cd86;">,</span>
    OpenMode<span style="color: #d2cd86;">.</span>ForWrite
    <span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>

        <span style="color: #e66170; font-weight: bold;">using</span> <span style="color: #d2cd86;">(</span>Polyline acPoly <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> Polyline<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span>
        <span style="color: #b060b0;">{</span>
            acPoly<span style="color: #d2cd86;">.</span>SetDatabaseDefaults<span style="color: #d2cd86;">(</span>db<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            acPoly<span style="color: #d2cd86;">.</span>AddVertexAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">new</span> Point2d<span style="color: #d2cd86;">(</span><span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            acPoly<span style="color: #d2cd86;">.</span>AddVertexAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">1</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">new</span> Point2d<span style="color: #d2cd86;">(</span><span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">2</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            acPoly<span style="color: #d2cd86;">.</span>AddVertexAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">2</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">new</span> Point2d<span style="color: #d2cd86;">(</span><span style="color: #008c00;">2</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">2</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            acPoly<span style="color: #d2cd86;">.</span>AddVertexAt<span style="color: #d2cd86;">(</span><span style="color: #008c00;">3</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">new</span> Point2d<span style="color: #d2cd86;">(</span><span style="color: #008c00;">2</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">0</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            acPoly<span style="color: #d2cd86;">.</span>Closed <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
            btr<span style="color: #d2cd86;">.</span>AppendEntity<span style="color: #d2cd86;">(</span>acPoly<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            tr<span style="color: #d2cd86;">.</span>AddNewlyCreatedDBObject<span style="color: #d2cd86;">(</span>acPoly<span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            Polyline2d poly2 <span style="color: #d2cd86;">=</span> acPoly<span style="color: #d2cd86;">.</span>ConvertTo<span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">true</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            <span style="background-color: #0080ff;">poly2<span style="color: #d2cd86;">.</span>LinetypeGenerationOn <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #b060b0;">;</span></span>
            poly2<span style="color: #d2cd86;">.</span>LinetypeId <span style="color: #d2cd86;">=</span> ltId<span style="color: #b060b0;">;</span>
            tr<span style="color: #d2cd86;">.</span>AddNewlyCreatedDBObject<span style="color: #d2cd86;">(</span>poly2<span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        <span style="color: #b060b0;">}</span>
        tr<span style="color: #d2cd86;">.</span>Commit<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
    <span style="color: #b060b0;">}</span>
<span style="color: #b060b0;">}</span>
</pre>
<p>Result :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37201be200c-pi" style="float: left;"><img alt="LinetypeGenOff" class="asset  asset-image at-xid-6a0167607c2431970b022ad37201be200c img-responsive" src="/assets/image_22737.jpg" style="margin: 0px 5px 5px 0px;" title="LinetypeGenOff" /></a></p>
