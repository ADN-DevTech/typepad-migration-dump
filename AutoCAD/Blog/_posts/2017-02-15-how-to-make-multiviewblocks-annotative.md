---
layout: "post"
title: "How To Make MultiViewBlocks Annotative"
date: "2017-02-15 17:54:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2017"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/02/how-to-make-multiviewblocks-annotative.html "
typepad_basename: "how-to-make-multiviewblocks-annotative"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>Though the post is very much related to AutoCAD Architecture or MEP, I have taken little liberty to attract AutoCAD audience too <img class="wlEmoticon wlEmoticon-smile" style="border-top-style: none; border-bottom-style: none; border-right-style: none; border-left-style: none" alt="Smile" src="/assets/image_611766.jpg"></p> <p>There isn’t a direct API to make annotative MVBlock, however if the internal ACAD block is annotative, same behaviour can be exposed to MVBlocks, we will get all referenced blocks and set annotation on each block which is an ACAD block.</p> <pre style='color:#000000;background:#ffffff;'><span style='color:#808030; '>[</span>CommandMethod<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>MakeMVBlockAnnotative</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#808030; '>]</span>
        <span style='color:#800000; font-weight:bold; '>public</span> <span style='color:#800000; font-weight:bold; '>static</span> <span style='color:#800000; font-weight:bold; '>void</span> makeMVBlockAnnotative<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span>
        <span style='color:#800080; '>{</span>
            <span style='color:#696969; '>/*You need to include AecBaseMgd reference from OMF SDK or available with ACA\MEP installations*/</span>
            Document doc <span style='color:#808030; '>=</span> Application<span style='color:#808030; '>.</span>DocumentManager<span style='color:#808030; '>.</span>MdiActiveDocument<span style='color:#800080; '>;</span>
            Database db <span style='color:#808030; '>=</span> doc<span style='color:#808030; '>.</span>Database<span style='color:#800080; '>;</span>
            Editor ed <span style='color:#808030; '>=</span> doc<span style='color:#808030; '>.</span>Editor<span style='color:#800080; '>;</span>
            PromptEntityOptions peo <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>new</span> PromptEntityOptions<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>Select a MVBlockreference</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
            PromptEntityResult result <span style='color:#808030; '>=</span> ed<span style='color:#808030; '>.</span>GetEntity<span style='color:#808030; '>(</span>peo<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
            <span style='color:#800000; font-weight:bold; '>if</span> <span style='color:#808030; '>(</span>result<span style='color:#808030; '>.</span>Status <span style='color:#808030; '>!</span><span style='color:#808030; '>=</span> PromptStatus<span style='color:#808030; '>.</span>OK<span style='color:#808030; '>)</span> <span style='color:#800000; font-weight:bold; '>return</span><span style='color:#800080; '>;</span>
            <span style='color:#800000; font-weight:bold; '>try</span>
            <span style='color:#800080; '>{</span>
                <span style='color:#800000; font-weight:bold; '>using</span> <span style='color:#808030; '>(</span>Transaction tr <span style='color:#808030; '>=</span>
                    db<span style='color:#808030; '>.</span>TransactionManager<span style='color:#808030; '>.</span>StartTransaction<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#808030; '>)</span>
                <span style='color:#800080; '>{</span>
                    MultiViewBlockReference mvBref <span style='color:#808030; '>=</span> 
                        tr<span style='color:#808030; '>.</span>GetObject<span style='color:#808030; '>(</span>result<span style='color:#808030; '>.</span>ObjectId<span style='color:#808030; '>,</span> OpenMode<span style='color:#808030; '>.</span>ForWrite<span style='color:#808030; '>)</span> <span style='color:#800000; font-weight:bold; '>as</span> MultiViewBlockReference<span style='color:#800080; '>;</span>
                    MultiViewBlockDefinition mbBdef <span style='color:#808030; '>=</span> 
                        tr<span style='color:#808030; '>.</span>GetObject<span style='color:#808030; '>(</span>mvBref<span style='color:#808030; '>.</span>BlockDefId<span style='color:#808030; '>,</span> OpenMode<span style='color:#808030; '>.</span>ForWrite<span style='color:#808030; '>)</span> <span style='color:#800000; font-weight:bold; '>as</span> MultiViewBlockDefinition<span style='color:#800080; '>;</span>
                    <span style='color:#696969; '>/*Each MVBlockDefination has various internal ACAD Blocks,</span>
<span style='color:#696969; '>&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;*  each such block holds view dependent entities                     </span>
<span style='color:#696969; '>&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;* If a internal ACAD Block has annotative behavior, </span>
<span style='color:#696969; '>&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;* same can be exposed to mvblocks.                </span>
<span style='color:#696969; '>&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;</span>
<span style='color:#696969; '>&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;*/</span>

                    Autodesk<span style='color:#808030; '>.</span>AutoCAD<span style='color:#808030; '>.</span>DatabaseServices<span style='color:#808030; '>.</span>ObjectIdCollection BrefIds <span style='color:#808030; '>=</span>
                        mbBdef<span style='color:#808030; '>.</span>GetAllBlocksReferenced<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
                    <span style='color:#696969; '>/*Utility*/</span>
                    printHandlesToEditor<span style='color:#808030; '>(</span>BrefIds<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
                    <span style='color:#800000; font-weight:bold; '>foreach</span><span style='color:#808030; '>(</span>Autodesk<span style='color:#808030; '>.</span>AutoCAD<span style='color:#808030; '>.</span>DatabaseServices<span style='color:#808030; '>.</span>ObjectId id <span style='color:#800000; font-weight:bold; '>in</span> BrefIds<span style='color:#808030; '>)</span>
                    <span style='color:#800080; '>{</span>
                        BlockTableRecord btr <span style='color:#808030; '>=</span> tr<span style='color:#808030; '>.</span>GetObject<span style='color:#808030; '>(</span>id<span style='color:#808030; '>,</span> OpenMode<span style='color:#808030; '>.</span>ForWrite<span style='color:#808030; '>)</span> <span style='color:#800000; font-weight:bold; '>as</span> BlockTableRecord<span style='color:#800080; '>;</span>
                        btr<span style='color:#808030; '>.</span>Annotative <span style='color:#808030; '>=</span> AnnotativeStates<span style='color:#808030; '>.</span>True<span style='color:#800080; '>;</span>
                    <span style='color:#800080; '>}</span>
                    BrefIds<span style='color:#808030; '>.</span>Clear<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
                    
                    <span style='color:#800000; font-weight:bold; '>bool</span> status <span style='color:#808030; '>=</span> mvBref<span style='color:#808030; '>.</span>UpdateAnnotative<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
                    tr<span style='color:#808030; '>.</span>Commit<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

                <span style='color:#800080; '>}</span>
            <span style='color:#800080; '>}</span>
            <span style='color:#800000; font-weight:bold; '>catch</span> <span style='color:#808030; '>(</span>System<span style='color:#808030; '>.</span>Exception ex<span style='color:#808030; '>)</span>
            <span style='color:#800080; '>{</span>
                ed<span style='color:#808030; '>.</span>WriteMessage<span style='color:#808030; '>(</span>ex<span style='color:#808030; '>.</span>ToString<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
            <span style='color:#800080; '>}</span>
        <span style='color:#800080; '>}</span>
        <span style='color:#696969; '>/*Helper*/</span>
        <span style='color:#800000; font-weight:bold; '>private</span> <span style='color:#800000; font-weight:bold; '>static</span> <span style='color:#800000; font-weight:bold; '>void</span> printHandlesToEditor<span style='color:#808030; '>(</span>Autodesk<span style='color:#808030; '>.</span>AutoCAD<span style='color:#808030; '>.</span>DatabaseServices<span style='color:#808030; '>.</span>ObjectIdCollection ids<span style='color:#808030; '>)</span>
        <span style='color:#800080; '>{</span>
            Document doc <span style='color:#808030; '>=</span> Application<span style='color:#808030; '>.</span>DocumentManager<span style='color:#808030; '>.</span>MdiActiveDocument<span style='color:#800080; '>;</span>
            Database db <span style='color:#808030; '>=</span> doc<span style='color:#808030; '>.</span>Database<span style='color:#800080; '>;</span>
            Editor ed <span style='color:#808030; '>=</span> doc<span style='color:#808030; '>.</span>Editor<span style='color:#800080; '>;</span>
            <span style='color:#800000; font-weight:bold; '>foreach</span> <span style='color:#808030; '>(</span>Autodesk<span style='color:#808030; '>.</span>AutoCAD<span style='color:#808030; '>.</span>DatabaseServices<span style='color:#808030; '>.</span>ObjectId id <span style='color:#800000; font-weight:bold; '>in</span> ids<span style='color:#808030; '>)</span>
            <span style='color:#800080; '>{</span>
                Handle handle <span style='color:#808030; '>=</span> id<span style='color:#808030; '>.</span>Handle<span style='color:#800080; '>;</span>
                ed<span style='color:#808030; '>.</span>WriteMessage<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>\n</span><span style='color:#800000; '>"</span> <span style='color:#808030; '>+</span> handle<span style='color:#808030; '>.</span>Value<span style='color:#808030; '>.</span>ToString<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
            <span style='color:#800080; '>}</span>

        <span style='color:#800080; '>}</span>
</pre>
