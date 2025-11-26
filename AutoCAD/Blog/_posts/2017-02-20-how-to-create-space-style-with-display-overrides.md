---
layout: "post"
title: "How to create Space Style with Display Overrides"
date: "2017-02-20 20:51:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2017"
original_url: "https://adndevblog.typepad.com/autocad/2017/02/how-to-create-space-style-with-display-overrides.html "
typepad_basename: "how-to-create-space-style-with-display-overrides"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>We have received&nbsp; this query from one of our ADN publisher, I’m blogging it for benefit every developer.</p> <p>Thanks to Amod for helping me out. As I’m fairly new to OMF and &gt;NET API for ACA_MEP.</p> <p>The following code is self explanatory.</p><pre style="background: #ffffff; color: #000000"><span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> PrintToCmdLine<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">string</span> str<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
    Editor ed <span style="color: #808030">=</span> Application<span style="color: #808030">.</span>DocumentManager<span style="color: #808030">.</span>MdiActiveDocument<span style="color: #808030">.</span>Editor<span style="color: #800080">;</span>
    ed<span style="color: #808030">.</span>WriteMessage<span style="color: #808030">(</span>str<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>
<span style="color: #808030">[</span>CommandMethod<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">createSpaceStyle</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #808030">]</span>
<span style="font-weight: bold; color: #800000">public</span> <span style="font-weight: bold; color: #800000">static</span> <span style="font-weight: bold; color: #800000">void</span> createSpaceStyle<span style="color: #808030">(</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
    Document doc <span style="color: #808030">=</span> Application<span style="color: #808030">.</span>DocumentManager<span style="color: #808030">.</span>MdiActiveDocument<span style="color: #800080">;</span>
    Database db <span style="color: #808030">=</span> doc<span style="color: #808030">.</span>Database<span style="color: #800080">;</span>
    Editor ed <span style="color: #808030">=</span> doc<span style="color: #808030">.</span>Editor<span style="color: #800080">;</span>
    <span style="font-weight: bold; color: #800000">try</span>
    <span style="color: #800080">{</span>
        <span style="font-weight: bold; color: #800000">using</span> <span style="color: #808030">(</span>Transaction trans <span style="color: #808030">=</span> db<span style="color: #808030">.</span>TransactionManager<span style="color: #808030">.</span>StartTransaction<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
        <span style="color: #800080">{</span>
            Autodesk<span style="color: #808030">.</span>Aec<span style="color: #808030">.</span>Arch<span style="color: #808030">.</span>DatabaseServices<span style="color: #808030">.</span>DictionarySpaceStyle dict <span style="color: #808030">=</span>
             <span style="font-weight: bold; color: #800000">new</span> DictionarySpaceStyle<span style="color: #808030">(</span>db<span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #696969">// space style</span>
            <span style="font-weight: bold; color: #800000">string</span> stylename <span style="color: #808030">=</span> <span style="color: #800000">"</span><span style="color: #0000e6">My_SpaceStyle</span><span style="color: #800000">"</span><span style="color: #800080">;</span>
            Autodesk<span style="color: #808030">.</span>Aec<span style="color: #808030">.</span>Arch<span style="color: #808030">.</span>DatabaseServices<span style="color: #808030">.</span>SpaceStyle style <span style="color: #808030">=</span> 
            <span style="font-weight: bold; color: #800000">new</span> SpaceStyle<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #696969">//Append To DB</span>
            style<span style="color: #808030">.</span>SetToStandard<span style="color: #808030">(</span>db<span style="color: #808030">)</span><span style="color: #800080">;</span>
                   
            <span style="color: #696969">/*Check*/</span>
            <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>dict<span style="color: #808030">.</span>Records<span style="color: #808030">.</span>IndexOf<span style="color: #808030">(</span>style<span style="color: #808030">.</span>ObjectId<span style="color: #808030">)</span> <span style="color: #808030">=</span><span style="color: #808030">=</span> <span style="color: #808030">-</span><span style="color: #008c00">1</span><span style="color: #808030">)</span>
            <span style="color: #800080">{</span>
                dict<span style="color: #808030">.</span>AddNewRecord<span style="color: #808030">(</span>stylename<span style="color: #808030">,</span> style<span style="color: #808030">)</span><span style="color: #800080">;</span>
                trans<span style="color: #808030">.</span>AddNewlyCreatedDBObject<span style="color: #808030">(</span>style<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">true</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                <span style="color: #696969">/*How to Add overrides to Style */</span>
                DisplayRepresentationManager dispRepMg <span style="color: #808030">=</span> 
                 <span style="font-weight: bold; color: #800000">new</span> DisplayRepresentationManager<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                Autodesk<span style="color: #808030">.</span>AutoCAD<span style="color: #808030">.</span>DatabaseServices<span style="color: #808030">.</span>ObjectId dispRepsDictId <span style="color: #808030">=</span> 
                  dispRepMg<span style="color: #808030">.</span>DisplayRepresentationsDictionaryId<span style="color: #800080">;</span>
                DBDictionary dispRepsDict <span style="color: #808030">=</span> 
                  <span style="color: #808030">(</span>DBDictionary<span style="color: #808030">)</span>trans<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>dispRepsDictId<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForWrite<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                Autodesk<span style="color: #808030">.</span>AutoCAD<span style="color: #808030">.</span>DatabaseServices<span style="color: #808030">.</span>ObjectId dispRepId <span style="color: #808030">=</span> 
                   dispRepsDict<span style="color: #808030">.</span>GetAt<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">AecDbDispRepSpaceModel</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                DisplayRepresentation dispRep <span style="color: #808030">=</span> 
                trans<span style="color: #808030">.</span>GetObject<span style="color: #808030">(</span>dispRepId<span style="color: #808030">,</span> OpenMode<span style="color: #808030">.</span>ForWrite<span style="color: #808030">,</span> <span style="font-weight: bold; color: #800000">false</span><span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">as</span> DisplayRepresentation<span style="color: #800080">;</span>

                Autodesk<span style="color: #808030">.</span>AutoCAD<span style="color: #808030">.</span>DatabaseServices<span style="color: #808030">.</span>ObjectId DispPropId <span style="color: #808030">=</span> 
                  dispRep<span style="color: #808030">.</span>DefaultDisplayPropertiesId<span style="color: #800080">;</span>
                OverrideDisplayProperties ovr <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> OverrideDisplayProperties<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
                ovr<span style="color: #808030">.</span>SubSetDatabaseDefaults<span style="color: #808030">(</span>db<span style="color: #808030">)</span><span style="color: #800080">;</span>
                ovr<span style="color: #808030">.</span>SetToStandard<span style="color: #808030">(</span>db<span style="color: #808030">)</span><span style="color: #800080">;</span>
                ovr<span style="color: #808030">.</span>DisplayPropertyId <span style="color: #808030">=</span> DispPropId<span style="color: #800080">;</span>
                ovr<span style="color: #808030">.</span>ViewId <span style="color: #808030">=</span> dispRepId<span style="color: #800080">;</span>
                style<span style="color: #808030">.</span>Overrides<span style="color: #808030">.</span>Add<span style="color: #808030">(</span>ovr<span style="color: #808030">)</span><span style="color: #800080">;</span>
            <span style="color: #800080">}</span>
            <span style="color: #696969">/*Access Display Overides*/</span>
            <span style="font-weight: bold; color: #800000">for</span> <span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">int</span> c <span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #800080">;</span> c <span style="color: #808030">&lt;</span> style<span style="color: #808030">.</span>Overrides<span style="color: #808030">.</span>Count<span style="color: #800080">;</span> c<span style="color: #808030">+</span><span style="color: #808030">+</span><span style="color: #808030">)</span>
            <span style="color: #800080">{</span>
                Override ovr <span style="color: #808030">=</span> style<span style="color: #808030">.</span>Overrides<span style="color: #808030">[</span>c<span style="color: #808030">]</span><span style="color: #800080">;</span>
                OverrideDisplayProperties odp <span style="color: #808030">=</span> 
                            <span style="color: #808030">(</span>OverrideDisplayProperties<span style="color: #808030">)</span>ovr<span style="color: #800080">;</span>
                <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>odp <span style="color: #808030">!</span><span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">null</span><span style="color: #808030">)</span>
                <span style="color: #800080">{</span>
                    <span style="font-weight: bold; color: #800000">string</span> str <span style="color: #808030">=</span> <span style="color: #800000">"</span><span style="color: #0000e6">Space style </span><span style="color: #800000">"</span> <span style="color: #808030">+</span> 
                                style<span style="color: #808030">.</span>Name <span style="color: #808030">+</span>
                                <span style="color: #800000">"</span><span style="color: #0000e6"> has display overrides\n</span><span style="color: #800000">"</span><span style="color: #800080">;</span>
                    PrintToCmdLine<span style="color: #808030">(</span>str<span style="color: #808030">)</span><span style="color: #800080">;</span>
                <span style="color: #800080">}</span>
            <span style="color: #800080">}</span>                 
                    
            trans<span style="color: #808030">.</span>Commit<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
        <span style="color: #800080">}</span>
    <span style="color: #800080">}</span>
    <span style="font-weight: bold; color: #800000">catch</span> <span style="color: #808030">(</span>System<span style="color: #808030">.</span>Exception ex<span style="color: #808030">)</span>
    <span style="color: #800080">{</span>
        ed<span style="color: #808030">.</span>WriteMessage<span style="color: #808030">(</span>ex<span style="color: #808030">.</span>ToString<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
    <span style="color: #800080">}</span>

           
    <span style="color: #800080">}</span>
</pre>
<p>&nbsp;</p>
<p>Please find our style residing STYLEMANAGER:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb097ba1f7970d-pi"><img title="StyleOvveride" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="StyleOvveride" src="/assets/image_346176.jpg" width="244" height="153"></a></p>
