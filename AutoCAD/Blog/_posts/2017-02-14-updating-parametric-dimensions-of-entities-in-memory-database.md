---
layout: "post"
title: "Updating Parametric Dimensions of Entities In-Memory Database"
date: "2017-02-14 01:43:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2017"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/02/updating-parametric-dimensions-of-entities-in-memory-database.html "
typepad_basename: "updating-parametric-dimensions-of-entities-in-memory-database"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>I have received a query from ADN&nbsp; on how to update the parameters of a drawing which is not an active document but read as in-memory database.</p> <p>For active drawing, we can use DIMREGEN in case if associated dimensions are updated, associated dimension may not act automatically in certain workflows, where a manual dimension regen is required.</p> <p>But, for in-memory database, after modifying Parameter values(AcDbAssocVariable), to take effect on underlying entities of dimensions it is preferable to always evaluate the top level network of the database by calling AcDbAssocManager::evaluateTopLevelNetwork(). Evaluation of the whole top level network guarantees that all actions that need to be evaluated, are evaluated. There is no performance penalty by always evaluating the whole top level network of the database even if it contains many sub networks and many actions.</p> <p>Following code assumes, user is trying to update dimensions on parametric rectangle of 20.0 and 30.0, test drawing and test project is appended at the bottom.</p><pre style="background: #ffffff; color: #000000"><span style="font-weight: bold; color: #800000">namespace</span> filer
<span style="color: #800080">{</span>
    <span style="color: #696969">///&lt;Summary&gt;</span>
    <span style="color: #696969">/// How to update Parameters on entities  of in-memory database</span>
    <span style="color: #696969">///&lt;/Summary&gt;</span>
<span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">class</span> TestFiler
<span style="color: #800080">{</span>
    <span style="color: #696969">///&lt;Summary&gt;</span>
    <span style="color: #696969">/// How to update Parameters on entities  of in-memory database</span>
    <span style="color: #696969">///&lt;/Summary&gt;</span>
    <span style="color: #696969">///</span>
<span style="color: #808030">[</span>CommandMethod<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">DIMUPDATE</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #808030">]</span>
<span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> testDimUpdate<span style="color: #808030">(</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
Editor ed <span style="color: #808030">=</span> Application<span style="color: #808030">.</span>DocumentManager<span style="color: #808030">.</span>MdiActiveDocument<span style="color: #808030">.</span>Editor<span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">try</span>
<span style="color: #800080">{</span>
    <span style="font-weight: bold; color: #800000">string</span> drawingFile <span style="color: #808030">=</span> getDrawingLocation<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Database db <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Database<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        db<span style="color: #808030">.</span>ReadDwgFile<span style="color: #808030">(</span>drawingFile<span style="color: #808030">,</span>
                        FileOpenMode<span style="color: #808030">.</span>OpenForReadAndWriteNoShare<span style="color: #808030">,</span>
                        <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        db<span style="color: #808030">.</span>CloseInput<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction tr <span style="color: #808030">=</span> db<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
        <span style="color: #800080">{</span>
            BlockTableRecord btr <span style="color: #808030">=</span> 
            <span style="color: #808030">(</span>BlockTableRecord<span style="color: #808030">)</span>tr<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>SymbolUtilityServices<span style="color: #808030">.</span>GetBlockModelSpaceId<span style="color: #808030">(</span>db<span style="color: #808030">)</span><span style="color: #808030">,</span>
                                            OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #696969">/*Width*/</span>
            ObjectId wActionId <span style="color: #808030">=</span> GetVariableByName<span style="color: #808030">(</span>btr<span style="color: #808030">.</span>ObjectId<span style="color: #808030">,</span> <span style="color: #800000">"</span><span style="color: #0000e6">Width</span><span style="color: #800000">"</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="font-weight: bold; color: #800000">string</span> name <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">string</span><span style="color: #808030">.</span>Empty<span style="color: #800080">;</span>
            ResultBuffer rbuf <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #800080">;</span>
            <span style="font-weight: bold; color: #800000">string</span> expression <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">string</span><span style="color: #808030">.</span>Empty<span style="color: #800080">;</span>
            GetVariableValue<span style="color: #808030">(</span>wActionId<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> name<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> rbuf<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> expression<span style="color: #808030">)</span><span style="color: #800080">;</span>
            expression <span style="color: #808030">=</span> <span style="color: #800000">"</span><span style="color: #0000e6">30</span><span style="color: #800000">"</span><span style="color: #800080">;</span><span style="color: #696969">//Width Value</span>
            SetVariableValue<span style="color: #808030">(</span>wActionId<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">,</span> expression<span style="color: #808030">)</span><span style="color: #800080">;</span>
                <span style="color: #696969">/*Check  if our values Got reflected in DB*/</span>
            GetVariableValue<span style="color: #808030">(</span>wActionId<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> name<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> rbuf<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> expression<span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>String<span style="color: #808030">.</span>Compare<span style="color: #808030">(</span>expression<span style="color: #808030">,</span> <span style="color: #800000">"</span><span style="color: #0000e6">30</span><span style="color: #800000">"</span><span style="color: #696969">/*hardCoded For Width*/</span><span style="color: #808030">)</span><span style="color: #808030">.</span>Equals<span style="color: #808030">(</span><span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
                Application<span style="color: #808030">.</span>ShowAlertDialog<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">Hey Success!</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="font-weight: bold; color: #800000">else</span> <span style="color: #696969">//Quit processing further</span>
                <span style="font-weight: bold; color: #800000">return</span><span style="color: #800080">;</span>
            <span style="color: #696969">/*Length*/</span>
            ObjectId lActionId <span style="color: #808030">=</span> GetVariableByName<span style="color: #808030">(</span>btr<span style="color: #808030">.</span>ObjectId<span style="color: #808030">,</span>
                                                    <span style="color: #800000">"</span><span style="color: #0000e6">Length</span><span style="color: #800000">"</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            rbuf <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #800080">;</span>
            GetVariableValue<span style="color: #808030">(</span>lActionId<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> name<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> rbuf<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> expression<span style="color: #808030">)</span><span style="color: #800080">;</span>
            expression <span style="color: #808030">=</span> <span style="color: #800000">"</span><span style="color: #0000e6">40</span><span style="color: #800000">"</span><span style="color: #800080">;</span><span style="color: #696969">//height Value</span>
            SetVariableValue<span style="color: #808030">(</span>lActionId<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">,</span> expression<span style="color: #808030">)</span><span style="color: #800080">;</span>
            tr<span style="color: #808030">.</span>Commit<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="color: #800080">}</span>

        <span style="font-weight: bold; color: #800000">bool</span> isEvaluationSuccess <span style="color: #808030">=</span> AssocManager<span style="color: #808030">.</span>EvaluateTopLevelNetwork<span style="color: #808030">(</span>db<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>isEvaluationSuccess<span style="color: #808030">)</span>
        <span style="color: #800080">{</span>
            <span style="color: #696969">//Now Check if the Values Updated in Entity</span>
            <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction tr <span style="color: #808030">=</span> db<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
            <span style="color: #800080">{</span>
                BlockTableRecord btr <span style="color: #808030">=</span> 
                <span style="color: #808030">(</span>BlockTableRecord<span style="color: #808030">)</span>tr<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>SymbolUtilityServices<span style="color: #808030">.</span>GetBlockModelSpaceId<span style="color: #808030">(</span>db<span style="color: #808030">)</span><span style="color: #808030">,</span>
                                                OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">)</span><span style="color: #800080">;</span>
                <span style="font-weight: bold; color: #800000">foreach</span> <span style="color: #808030">(</span>ObjectId id <span style="font-weight: bold; color: #800000">in</span> btr<span style="color: #808030">)</span>
                <span style="color: #800080">{</span>
                    Entity ent <span style="color: #808030">=</span> <span style="color: #808030">(</span>Entity<span style="color: #808030">)</span>tr<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>id<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">)</span><span style="color: #800080">;</span>
                    Polyline pline <span style="color: #808030">=</span> ent <span style="font-weight: bold; color: #800000">as</span> Polyline<span style="color: #800080">;</span>
                    <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>pline <span style="color: #808030">!</span><span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">)</span>
                    <span style="color: #800080">{</span>
                        <span style="color: #696969">//stream the points to Editor Console</span>
                        <span style="font-weight: bold; color: #800000">for</span> <span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">int</span> i <span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #800080">;</span> i <span style="color: #808030">&lt;</span> pline<span style="color: #808030">.</span>NumberOfVertices<span style="color: #800080">;</span> i<span style="color: #808030">+</span><span style="color: #808030">+</span><span style="color: #808030">)</span>
                            ed<span style="color: #808030">.</span>WriteMessage<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">\n</span><span style="color: #800000">"</span> <span style="color: #808030">+</span> 
                                            pline<span style="color: #808030">.</span>GetPoint3dAt<span style="color: #808030">(</span>i<span style="color: #808030">)</span><span style="color: #808030">.</span>ToString<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                    <span style="color: #800080">}</span>


                <span style="color: #800080">}</span>
                tr<span style="color: #808030">.</span>Commit<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #800080">}</span>
        <span style="color: #800080">}</span>
        <span style="font-weight: bold; color: #800000">else</span> <span style="font-weight: bold; color: #800000">return</span><span style="color: #800080">;</span> <span style="color: #696969">//We will quit</span>

        <span style="color: #696969">//Now save the in-memory DB to Disk.</span>
        db<span style="color: #808030">.</span>SaveAs<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">new</span> StringBuilder<span style="color: #808030">(</span>drawingFile<span style="color: #808030">)</span><span style="color: #808030">.</span>Replace<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">.dwg</span><span style="color: #800000">"</span><span style="color: #808030">,</span>
                                                        <span style="color: #800000">"</span><span style="color: #0000e6">_new.dwg</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #808030">.</span>ToString<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">,</span>
                                                        DwgVersion<span style="color: #808030">.</span>Current<span style="color: #808030">)</span><span style="color: #800080">;</span>


    <span style="color: #800080">}</span>
<span style="color: #800080">}</span>
<span style="font-weight: bold; color: #800000">catch</span> <span style="color: #808030">(</span>System<span style="color: #808030">.</span>Exception ex<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
    ed<span style="color: #808030">.</span>WriteMessage<span style="color: #808030">(</span>ex<span style="color: #808030">.</span>ToString<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

<span style="color: #800080">}</span>

<span style="font-weight: bold; color: #800000">private</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">string</span> getDrawingLocation<span style="color: #808030">(</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
    <span style="color: #696969">/*</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp; SolutionFolder</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp; │   </span>
<span style="color: #696969">&nbsp;&nbsp; |───bin</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp; └───Debug</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp; │</span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp; assembly.dll</span>
<span style="color: #696969"></span>
<span style="color: #696969">&nbsp;&nbsp;&nbsp; */</span>
    <span style="font-weight: bold; color: #800000">var</span> assemblyLoc <span style="color: #808030">=</span> Assembly<span style="color: #808030">.</span>GetExecutingAssembly<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">.</span>Location<span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">var</span> debugFolder <span style="color: #808030">=</span> Path<span style="color: #808030">.</span>GetDirectoryName<span style="color: #808030">(</span>assemblyLoc<span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">var</span> binFolder <span style="color: #808030">=</span> Path<span style="color: #808030">.</span>GetDirectoryName<span style="color: #808030">(</span>debugFolder<span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">var</span> solutionFolder <span style="color: #808030">=</span> Path<span style="color: #808030">.</span>GetDirectoryName<span style="color: #808030">(</span>binFolder<span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">var</span> drawingLocationFilePath <span style="color: #808030">=</span> solutionFolder <span style="color: #808030">+</span> 
                                <span style="color: #800000">"</span><span style="color: #0000e6">\\TestParameters.dwg</span><span style="color: #800000">"</span><span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">return</span> Path<span style="color: #808030">.</span>GetFullPath<span style="color: #808030">(</span>drawingLocationFilePath<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

<span style="color: #696969">///&lt;Summary&gt;</span>
<span style="color: #696969">/// Get Variable Name for Given variableId</span>
<span style="color: #696969">///&lt;/Summary&gt;</span>
<span style="color: #696969">///</span>
<span style="font-weight: bold; color: #800000">private</span> <span style="font-weight: bold; color: #800000">static</span> ObjectId GetVariableByName<span style="color: #808030">(</span>ObjectId btrId<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">string</span> name<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">bool</span> createIfDoesNotExist<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
    <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>name<span style="color: #808030">.</span>Length <span style="color: #808030">=</span><span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span>
        <span style="font-weight: bold; color: #800000">throw</span> <span style="font-weight: bold; color: #800000">new</span> Autodesk<span style="color: #808030">.</span>AutoCAD<span style="color: #808030">.</span>Runtime<span style="color: #808030">.</span>Exception<span style="color: #808030">(</span>
        ErrorStatus<span style="color: #808030">.</span>InvalidInput<span style="color: #808030">)</span><span style="color: #800080">;</span>

    ObjectId networkId <span style="color: #808030">=</span> AssocNetwork<span style="color: #808030">.</span>GetInstanceFromObject<span style="color: #808030">(</span>
    btrId<span style="color: #808030">,</span>
    createIfDoesNotExist<span style="color: #808030">,</span>
    <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">,</span>
    <span style="color: #800000">"</span><span style="color: #0000e6">ACAD_ASSOCNETWORK</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

    <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>networkId <span style="color: #808030">=</span><span style="color: #808030">=</span> ObjectId<span style="color: #808030">.</span>Null<span style="color: #808030">)</span>
        <span style="font-weight: bold; color: #800000">return</span> ObjectId<span style="color: #808030">.</span>Null<span style="color: #800080">;</span>

    <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction Tx <span style="color: #808030">=</span>
    btrId<span style="color: #808030">.</span>Database<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>AssocNetwork network <span style="color: #808030">=</span>
        Tx<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>networkId<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span>
        <span style="font-weight: bold; color: #800000">as</span> AssocNetwork<span style="color: #808030">)</span>
        <span style="color: #800080">{</span>
            <span style="font-weight: bold; color: #800000">foreach</span> <span style="color: #808030">(</span>ObjectId actionId <span style="font-weight: bold; color: #800000">in</span> network<span style="color: #808030">.</span>GetActions<span style="color: #808030">)</span>
            <span style="color: #800080">{</span>
                <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>actionId <span style="color: #808030">=</span><span style="color: #808030">=</span> ObjectId<span style="color: #808030">.</span>Null<span style="color: #808030">)</span>
                    <span style="font-weight: bold; color: #800000">continue</span><span style="color: #800080">;</span>

                <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>actionId<span style="color: #808030">.</span>ObjectClass<span style="color: #808030">.</span>IsDerivedFrom<span style="color: #808030">(</span>
                RXObject<span style="color: #808030">.</span>GetClass<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">typeof</span><span style="color: #808030">(</span>AssocVariable<span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
                <span style="color: #800080">{</span>
                    <span style="color: #696969">//Check if we found our guy</span>
                    AssocVariable <span style="font-weight: bold; color: #800000">var</span> <span style="color: #808030">=</span> Tx<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>actionId<span style="color: #808030">,</span> 
                                                    OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">,</span>
                                                    <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span>
                    <span style="font-weight: bold; color: #800000">as</span> AssocVariable<span style="color: #800080">;</span>

                    <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">var</span><span style="color: #808030">.</span>Name <span style="color: #808030">=</span><span style="color: #808030">=</span> name<span style="color: #808030">)</span>
                        <span style="font-weight: bold; color: #800000">return</span> actionId<span style="color: #800080">;</span>
                <span style="color: #800080">}</span>
            <span style="color: #800080">}</span>
        <span style="color: #800080">}</span>
    <span style="color: #800080">}</span>

    <span style="color: #696969">//If we don't want to create a new variable, returns an error</span>
    <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span><span style="color: #808030">!</span>createIfDoesNotExist<span style="color: #808030">)</span>
        <span style="font-weight: bold; color: #800000">return</span> ObjectId<span style="color: #808030">.</span>Null<span style="color: #800080">;</span>

    <span style="font-weight: bold; color: #800000">return</span> ObjectId<span style="color: #808030">.</span>Null<span style="color: #800080">;</span><span style="color: #696969">//We didnt find what we are looking</span>
<span style="color: #800080">}</span>
<span style="font-weight: bold; color: #800000">private</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> GetVariableValue<span style="color: #808030">(</span>ObjectId variableId<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">ref</span> <span style="font-weight: bold; color: #800000">string</span> name<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">ref</span> ResultBuffer value<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">ref</span> <span style="font-weight: bold; color: #800000">string</span> expression<span style="color: #808030">)</span>

<span style="color: #800080">{</span>
    <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction Tx <span style="color: #808030">=</span>
    variableId<span style="color: #808030">.</span>Database<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        AssocVariable <span style="font-weight: bold; color: #800000">assocV</span> <span style="color: #808030">=</span>
        Tx<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>variableId<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForRead<span style="color: #808030">)</span>
        <span style="font-weight: bold; color: #800000">as</span> AssocVariable<span style="color: #800080">;</span>

        name <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>Name<span style="color: #800080">;</span>

        <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>EvaluateExpression<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">ref</span> value<span style="color: #808030">)</span><span style="color: #800080">;</span>

        expression <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>Expression<span style="color: #800080">;</span>

        Tx<span style="color: #808030">.</span>Commit<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="color: #800080">}</span>
<span style="color: #800080">}</span>
<span style="color: #696969">///&lt;Summary&gt;</span>
<span style="color: #696969">/// Set Expression for Given variableId</span>
<span style="color: #696969">///&lt;/Summary&gt;</span>
<span style="color: #696969">///</span>
<span style="font-weight: bold; color: #800000">private</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> SetVariableValue<span style="color: #808030">(</span>ObjectId variableId<span style="color: #808030">,</span>
ResultBuffer value<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">string</span> expression<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
    <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction Tx <span style="color: #808030">=</span>
    variableId<span style="color: #808030">.</span>Database<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        AssocVariable <span style="font-weight: bold; color: #800000">assocV</span> <span style="color: #808030">=</span>
        Tx<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>variableId<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForWrite<span style="color: #808030">)</span>
        <span style="font-weight: bold; color: #800000">as</span> AssocVariable<span style="color: #800080">;</span>

        <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>expression <span style="color: #808030">!</span><span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">string</span><span style="color: #808030">.</span>Empty<span style="color: #808030">)</span>
        <span style="color: #800080">{</span>
            <span style="font-weight: bold; color: #800000">string</span> errMsg <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">string</span><span style="color: #808030">.</span>Empty<span style="color: #800080">;</span>

            <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>ValidateNameAndExpression<span style="color: #808030">(</span>
            <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>Name<span style="color: #808030">,</span>
            expression<span style="color: #808030">,</span>
            <span style="font-weight: bold; color: #800000">ref</span> errMsg<span style="color: #808030">)</span><span style="color: #800080">;</span>

            <strong><font color="#800000">assocV</font></strong><span style="color: #808030">.</span>SetExpression<span style="color: #808030">(</span>
            expression<span style="color: #808030">,</span> <span style="color: #800000">"</span><span style="color: #800000">"</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">ref</span> errMsg<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

            ResultBuffer evaluatedExpressionValue <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #800080">;</span>

            <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>EvaluateExpression<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">ref</span> evaluatedExpressionValue<span style="color: #808030">)</span><span style="color: #800080">;</span>

            <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>Value <span style="color: #808030">=</span> evaluatedExpressionValue<span style="color: #800080">;</span>
        <span style="color: #800080">}</span>
        <span style="font-weight: bold; color: #800000">else</span>
        <span style="color: #800080">{</span>
            <span style="font-weight: bold; color: #800000">assocV</span><span style="color: #808030">.</span>Value <span style="color: #808030">=</span> value<span style="color: #800080">;</span>
        <span style="color: #800080">}</span>

        Tx<span style="color: #808030">.</span>Commit<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="color: #800080">}</span>
<span style="color: #800080">}</span>
<span style="color: #800080">}</span>
<span style="color: #800080">}</span>
</pre>
<p>Sample Project with Drawings.</p>
<p><a href="https://github.com/MadhukarMoogala/MyBlogs/tree/master/Updateparameters" target="_blank">UpdateParameters</a></p>
