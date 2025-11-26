---
layout: "post"
title: "How Get Adjacent Faces From a Selected Edge In Solid"
date: "2017-08-22 20:19:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/08/how-get-adjacent-faces-from-a-selected-edge-in-solid.html "
typepad_basename: "how-get-adjacent-faces-from-a-selected-edge-in-solid"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p><br></p><p>Using BREP API it is fairly straightforward to retrieve the adjacent faces from a particular edge in a 3dsolid.</p><p>We will use Boundary Loop to get all the loops consuming the edge, from Loop we can get associated Face.</p><p><br></p>
<pre style="background: rgb(0, 0, 0); color: rgb(209, 209, 209);"><span style="color: rgb(230, 97, 112); font-weight: bold;">static</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">public</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">void</span> GetAdjacentFaces<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span>
<span style="color: rgb(176, 96, 176);">{</span>
    Document doc <span style="color: rgb(210, 205, 134);">=</span> Application<span style="color: rgb(210, 205, 134);">.</span>DocumentManager<span style="color: rgb(210, 205, 134);">.</span>MdiActiveDocument<span style="color: rgb(176, 96, 176);">;</span>
    Database db <span style="color: rgb(210, 205, 134);">=</span> doc<span style="color: rgb(210, 205, 134);">.</span>Database<span style="color: rgb(176, 96, 176);">;</span>
    Editor ed <span style="color: rgb(210, 205, 134);">=</span> doc<span style="color: rgb(210, 205, 134);">.</span>Editor<span style="color: rgb(176, 96, 176);">;</span>
    PromptKeywordOptions pko <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> PromptKeywordOptions<span style="color: rgb(210, 205, 134);">(</span>
<span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nSpecify sub-entity selection type:</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    pko<span style="color: rgb(210, 205, 134);">.</span>AllowNone <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">false</span><span style="color: rgb(176, 96, 176);">;</span>
    pko<span style="color: rgb(210, 205, 134);">.</span>Keywords<span style="color: rgb(210, 205, 134);">.</span>Add<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">Edge</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    pko<span style="color: rgb(210, 205, 134);">.</span>Keywords<span style="color: rgb(210, 205, 134);">.</span>Default <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">Edge</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(176, 96, 176);">;</span>

    PromptResult pkr <span style="color: rgb(210, 205, 134);">=</span> ed<span style="color: rgb(210, 205, 134);">.</span>GetKeywords<span style="color: rgb(210, 205, 134);">(</span>pko<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

    <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>pkr<span style="color: rgb(210, 205, 134);">.</span>Status <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> PromptStatus<span style="color: rgb(210, 205, 134);">.</span>OK<span style="color: rgb(210, 205, 134);">)</span>
        <span style="color: rgb(230, 97, 112); font-weight: bold;">return</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(153, 153, 169);">//User selection is Edge</span>
    SubentityType subentityType <span style="color: rgb(210, 205, 134);">=</span> SubentityType<span style="color: rgb(210, 205, 134);">.</span>Edge<span style="color: rgb(176, 96, 176);">;</span>


    <span style="color: rgb(153, 153, 169);">//Enable force pick sub-selection</span>
    PromptSelectionOptions pso <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> PromptSelectionOptions<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    pso<span style="color: rgb(210, 205, 134);">.</span>MessageForAdding <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nSelect solid </span><span style="color: rgb(2, 208, 69);">"</span> <span style="color: rgb(210, 205, 134);">+</span>
        pkr<span style="color: rgb(210, 205, 134);">.</span>StringResult <span style="color: rgb(210, 205, 134);">+</span> <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">: </span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(176, 96, 176);">;</span>
    pso<span style="color: rgb(210, 205, 134);">.</span>SingleOnly <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">true</span><span style="color: rgb(176, 96, 176);">;</span>
    pso<span style="color: rgb(210, 205, 134);">.</span>SinglePickInSpace <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">true</span><span style="color: rgb(176, 96, 176);">;</span>
    pso<span style="color: rgb(210, 205, 134);">.</span>ForceSubSelections <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">true</span><span style="color: rgb(176, 96, 176);">;</span>

    PromptSelectionResult psr <span style="color: rgb(210, 205, 134);">=</span> ed<span style="color: rgb(210, 205, 134);">.</span>GetSelection<span style="color: rgb(210, 205, 134);">(</span>pso<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

    <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>psr<span style="color: rgb(210, 205, 134);">.</span>Status <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> PromptStatus<span style="color: rgb(210, 205, 134);">.</span>OK<span style="color: rgb(210, 205, 134);">)</span>
        <span style="color: rgb(230, 97, 112); font-weight: bold;">return</span><span style="color: rgb(176, 96, 176);">;</span>

    SelectionSet ss <span style="color: rgb(210, 205, 134);">=</span> psr<span style="color: rgb(210, 205, 134);">.</span>Value<span style="color: rgb(176, 96, 176);">;</span>

    SelectedObject so <span style="color: rgb(210, 205, 134);">=</span> ss<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(176, 96, 176);">;</span>

    <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">!</span>so<span style="color: rgb(210, 205, 134);">.</span>ObjectId<span style="color: rgb(210, 205, 134);">.</span>ObjectClass<span style="color: rgb(210, 205, 134);">.</span>IsDerivedFrom<span style="color: rgb(210, 205, 134);">(</span>
        RXClass<span style="color: rgb(210, 205, 134);">.</span>GetClass<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(230, 97, 112); font-weight: bold;">typeof</span><span style="color: rgb(210, 205, 134);">(</span>Solid3d<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span>
    <span style="color: rgb(176, 96, 176);">{</span>
        ed<span style="color: rgb(210, 205, 134);">.</span>WriteMessage<span style="color: rgb(210, 205, 134);">(</span>
            <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nYou didn't select a solid, please try again...</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
        <span style="color: rgb(230, 97, 112); font-weight: bold;">return</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(176, 96, 176);">}</span>
    <span style="color: rgb(153, 153, 169);">//To store adjacent Faces</span>
    List<span style="color: rgb(210, 205, 134);">&lt;</span>SubentityId<span style="color: rgb(210, 205, 134);">&gt;</span> faceIds <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> List<span style="color: rgb(210, 205, 134);">&lt;</span>SubentityId<span style="color: rgb(210, 205, 134);">&gt;</span><span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(230, 97, 112); font-weight: bold;">using</span> <span style="color: rgb(210, 205, 134);">(</span>Transaction Tx <span style="color: rgb(210, 205, 134);">=</span> db<span style="color: rgb(210, 205, 134);">.</span>TransactionManager<span style="color: rgb(210, 205, 134);">.</span>StartTransaction<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span>
    <span style="color: rgb(176, 96, 176);">{</span>
        Solid3d solid <span style="color: rgb(210, 205, 134);">=</span> Tx<span style="color: rgb(210, 205, 134);">.</span>GetObject<span style="color: rgb(210, 205, 134);">(</span>so<span style="color: rgb(210, 205, 134);">.</span>ObjectId<span style="color: rgb(210, 205, 134);">,</span> OpenMode<span style="color: rgb(210, 205, 134);">.</span>ForWrite<span style="color: rgb(210, 205, 134);">)</span>
            <span style="color: rgb(230, 97, 112); font-weight: bold;">as</span> Solid3d<span style="color: rgb(176, 96, 176);">;</span>

        SelectedSubObject<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(210, 205, 134);">]</span> sso <span style="color: rgb(210, 205, 134);">=</span> so<span style="color: rgb(210, 205, 134);">.</span>GetSubentities<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

        <span style="color: rgb(153, 153, 169);">//Checks that selected type matches keyword selection</span>
        <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>subentityType <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> sso<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(210, 205, 134);">.</span>FullSubentityPath<span style="color: rgb(210, 205, 134);">.</span>SubentId<span style="color: rgb(210, 205, 134);">.</span>Type<span style="color: rgb(210, 205, 134);">)</span>
        <span style="color: rgb(176, 96, 176);">{</span>
            ed<span style="color: rgb(210, 205, 134);">.</span>WriteMessage<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nInvalid Subentity Type: </span><span style="color: rgb(2, 208, 69);">"</span> <span style="color: rgb(210, 205, 134);">+</span>
                sso<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(210, 205, 134);">.</span>FullSubentityPath<span style="color: rgb(210, 205, 134);">.</span>SubentId<span style="color: rgb(210, 205, 134);">.</span>Type <span style="color: rgb(210, 205, 134);">+</span>
                <span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">, please try again...</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
            <span style="color: rgb(230, 97, 112); font-weight: bold;">return</span><span style="color: rgb(176, 96, 176);">;</span>
        <span style="color: rgb(176, 96, 176);">}</span>

        SubentityId subentityId <span style="color: rgb(210, 205, 134);">=</span> sso<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(210, 205, 134);">.</span>FullSubentityPath<span style="color: rgb(210, 205, 134);">.</span>SubentId<span style="color: rgb(176, 96, 176);">;</span>

        <span style="color: rgb(153, 153, 169);">//Creates subentity path to use with GetSubentity</span>
        FullSubentityPath subEntityPath <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> FullSubentityPath<span style="color: rgb(210, 205, 134);">(</span>
            <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> ObjectId<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(210, 205, 134);">]</span> <span style="color: rgb(176, 96, 176);">{</span> solid<span style="color: rgb(210, 205, 134);">.</span>ObjectId <span style="color: rgb(176, 96, 176);">}</span><span style="color: rgb(210, 205, 134);">,</span>
            subentityId<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

        <span style="color: rgb(153, 153, 169);">//Returns a non-database resident entity</span>
        <span style="color: rgb(153, 153, 169);">//that represents the subentity</span>
        <span style="color: rgb(230, 97, 112); font-weight: bold;">using</span> <span style="color: rgb(210, 205, 134);">(</span>Entity entity <span style="color: rgb(210, 205, 134);">=</span> solid<span style="color: rgb(210, 205, 134);">.</span>GetSubentity<span style="color: rgb(210, 205, 134);">(</span>subEntityPath<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span>
        <span style="color: rgb(176, 96, 176);">{</span>
            ed<span style="color: rgb(210, 205, 134);">.</span>WriteMessage<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nSubentity Entity Type: </span><span style="color: rgb(2, 208, 69);">"</span>
                <span style="color: rgb(210, 205, 134);">+</span> entity<span style="color: rgb(210, 205, 134);">.</span>ToString<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
        <span style="color: rgb(176, 96, 176);">}</span>

        <span style="color: rgb(153, 153, 169);">//Creates entity path to generate Brep object from it</span>
        FullSubentityPath entityPath <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> FullSubentityPath<span style="color: rgb(210, 205, 134);">(</span>
            <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> ObjectId<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(210, 205, 134);">]</span> <span style="color: rgb(176, 96, 176);">{</span> solid<span style="color: rgb(210, 205, 134);">.</span>ObjectId <span style="color: rgb(176, 96, 176);">}</span><span style="color: rgb(210, 205, 134);">,</span>
            <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> SubentityId<span style="color: rgb(210, 205, 134);">(</span>SubentityType<span style="color: rgb(210, 205, 134);">.</span>Null<span style="color: rgb(210, 205, 134);">,</span> IntPtr<span style="color: rgb(210, 205, 134);">.</span>Zero<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

        <span style="color: rgb(230, 97, 112); font-weight: bold;">using</span> <span style="color: rgb(210, 205, 134);">(</span>Edge edge <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">new</span> Edge<span style="color: rgb(210, 205, 134);">(</span>subEntityPath<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span>
        <span style="color: rgb(176, 96, 176);">{</span>
            <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>subentityType <span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(210, 205, 134);">=</span> SubentityType<span style="color: rgb(210, 205, 134);">.</span>Edge<span style="color: rgb(210, 205, 134);">)</span>
            <span style="color: rgb(176, 96, 176);">{</span>
                       
                    <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span>subentityId <span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(210, 205, 134);">=</span> edge<span style="color: rgb(210, 205, 134);">.</span>SubentityPath<span style="color: rgb(210, 205, 134);">.</span>SubentId<span style="color: rgb(210, 205, 134);">)</span>
                    <span style="color: rgb(176, 96, 176);">{</span>
                        ed<span style="color: rgb(210, 205, 134);">.</span>WriteMessage<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">\nCurve: </span><span style="color: rgb(2, 208, 69);">"</span> <span style="color: rgb(210, 205, 134);">+</span>  edge<span style="color: rgb(210, 205, 134);">.</span>Curve<span style="color: rgb(210, 205, 134);">.</span>ToString<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
                        <span style="color: rgb(153, 153, 169);">//Now color the adjacent faces.</span>
                        <span style="color: rgb(230, 97, 112); font-weight: bold;">foreach</span><span style="color: rgb(210, 205, 134);">(</span>BoundaryLoop loop <span style="color: rgb(230, 97, 112); font-weight: bold;">in</span> edge<span style="color: rgb(210, 205, 134);">.</span>Loops<span style="color: rgb(210, 205, 134);">)</span>
                        <span style="color: rgb(176, 96, 176);">{</span>
                        <span style="color: rgb(153, 153, 169);">//For simplest loop,outward loop is our interest</span>
                            <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span><span style="color: rgb(210, 205, 134);">(</span>loop<span style="color: rgb(210, 205, 134);">.</span>LoopType <span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(210, 205, 134);">=</span>LoopType<span style="color: rgb(210, 205, 134);">.</span>LoopExterior<span style="color: rgb(210, 205, 134);">)</span>
                            <span style="color: rgb(176, 96, 176);">{</span>
                            AcBr<span style="color: rgb(210, 205, 134);">.</span>Face face <span style="color: rgb(210, 205, 134);">=</span> loop<span style="color: rgb(210, 205, 134);">.</span>Face<span style="color: rgb(176, 96, 176);">;</span>
                            faceIds<span style="color: rgb(210, 205, 134);">.</span>Add<span style="color: rgb(210, 205, 134);">(</span>face<span style="color: rgb(210, 205, 134);">.</span>SubentityPath<span style="color: rgb(210, 205, 134);">.</span>SubentId<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
                            <span style="color: rgb(176, 96, 176);">}</span>

                        <span style="color: rgb(176, 96, 176);">}</span>
                        <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span><span style="color: rgb(210, 205, 134);">(</span>faceIds<span style="color: rgb(210, 205, 134);">.</span>Count <span style="color: rgb(210, 205, 134);">&gt;</span> <span style="color: rgb(0, 140, 0);">1</span><span style="color: rgb(210, 205, 134);">)</span>
                        <span style="color: rgb(230, 97, 112); font-weight: bold;">foreach</span> <span style="color: rgb(210, 205, 134);">(</span>SubentityId subentId <span style="color: rgb(230, 97, 112); font-weight: bold;">in</span> faceIds<span style="color: rgb(210, 205, 134);">)</span>
                        <span style="color: rgb(176, 96, 176);">{</span>
                            Color col <span style="color: rgb(210, 205, 134);">=</span> Color<span style="color: rgb(210, 205, 134);">.</span>FromColorIndex<span style="color: rgb(210, 205, 134);">(</span>ColorMethod<span style="color: rgb(210, 205, 134);">.</span>ByColor<span style="color: rgb(210, 205, 134);">,</span> <span style="color: rgb(0, 140, 0);">1</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
                            solid<span style="color: rgb(210, 205, 134);">.</span>SetSubentityColor<span style="color: rgb(210, 205, 134);">(</span>subentId<span style="color: rgb(210, 205, 134);">,</span> col<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
                        <span style="color: rgb(176, 96, 176);">}</span>


                <span style="color: rgb(176, 96, 176);">}</span>
                        
            <span style="color: rgb(176, 96, 176);">}</span>

        <span style="color: rgb(176, 96, 176);">}</span>

        Tx<span style="color: rgb(210, 205, 134);">.</span>Commit<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
    <span style="color: rgb(176, 96, 176);">}</span>


<span style="color: rgb(176, 96, 176);">}</span>
</pre>

<p>Demo Video</p>
<iframe width="400" height="470" src="https://screencast.autodesk.com/Embed/Timeline/583a9ecf-1bf4-4626-9ffd-4f60b8c384a9" frameborder="0" allowfullscreen="" webkitallowfullscreen=""></iframe>
