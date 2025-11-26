---
layout: "post"
title: "Accessing Drawing Standards File (.dws)"
date: "2017-05-09 21:47:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2017"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/05/accessing-drawing-standards-file-dws.html "
typepad_basename: "accessing-drawing-standards-file-dws"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>I had a chance to explore on this, when I came across a query in <a href="https://forums.autodesk.com/t5/net/c-to-access-the-setup-for-the-layer-translator/m-p/7061366/highlight/true#M53141">forum</a>.</p> <p>When user invokes “<a href="https://www.youtube.com/watch?v=sMUiMWiL0Z4">LAYTRANS</a>”, a layer translator dialog gets popped up and user is allowed to translate the layers from a loaded drawing to current drawing.</p> <p>layers from loaded drawing are mapped to layers in current drawing or mapping can be set up by an user.</p> <p>This setup can be saved as standards file (.dws), for future use when user receives a drawing from a customer, user can check if drawing complies with said standards files.</p> <p>The standards file holds the layer translation mapping information in an extended data of each layers that has been mapped or translated.</p> <p>For example, the attached <a href="https://forums.autodesk.com/autodesk/attachments/autodesk/152/53230/1/MyStandard.dws">file</a>, has a mapping information between layers A,B,C wrt 1,2,3. Where A,B,C layers of standard drawing(source) which will be translated to layers 1,2,3 of current drawing (destination)</p> <p>Reading dws is same as like reading a drawing file, this is a file with different extension. (Thanks to <a href="http://blogs.autodesk.com/autocad/author/leeambrosius/">Lee</a> for the tip).</p><pre style="background: #000000; color: #d1d1d1"><span style="font-weight: bold; color: #e66170">public</span> <span style="font-weight: bold; color: #e66170">static</span> <span style="font-weight: bold; color: #e66170">void</span> tstreadDws<span style="color: #d2cd86">(</span><span style="color: #d2cd86">)</span>
        <span style="color: #b060b0">{</span>
            <span style="color: #9999a9">// save old database</span>
            Database oldDb <span style="color: #d2cd86">=</span> HostApplicationServices<span style="color: #d2cd86">.</span>WorkingDatabase<span style="color: #b060b0">;</span>
            Editor ed <span style="color: #d2cd86">=</span> Application<span style="color: #d2cd86">.</span>DocumentManager<span style="color: #d2cd86">.</span>MdiActiveDocument<span style="color: #d2cd86">.</span>Editor<span style="color: #b060b0">;</span>

            <span style="color: #9999a9">// when using ReadDwgFile, never specify True to buildDefaultDwg</span>
            <span style="color: #9999a9">// also, set noDocument=True because this drawing has no</span>
            <span style="color: #9999a9">// AutoCAD Document associated with it</span>
            <span style="font-weight: bold; color: #e66170">using</span> <span style="color: #d2cd86">(</span>Database db <span style="color: #d2cd86">=</span> <span style="font-weight: bold; color: #e66170">new</span> Database<span style="color: #d2cd86">(</span><span style="font-weight: bold; color: #e66170">false</span><span style="color: #d2cd86">,</span> <span style="font-weight: bold; color: #e66170">true</span><span style="color: #d2cd86">)</span><span style="color: #d2cd86">)</span>
            <span style="color: #b060b0">{</span>
                db<span style="color: #d2cd86">.</span>ReadDwgFile<span style="color: #d2cd86">(</span><span style="color: #02d045">"</span><span style="color: #00c4c4">D:\\Temp\\MyStandard.dws</span><span style="color: #02d045">"</span><span style="color: #d2cd86">,</span> FileOpenMode<span style="color: #d2cd86">.</span>OpenForReadAndWriteNoShare<span style="color: #d2cd86">,</span> <span style="font-weight: bold; color: #e66170">true</span><span style="color: #d2cd86">,</span> <span style="color: #02d045">"</span><span style="color: #02d045">"</span><span style="color: #d2cd86">)</span><span style="color: #b060b0">;</span>
                <span style="color: #9999a9">// closing the input makes sure the whole dwg is read from disk</span>
                <span style="color: #9999a9">// it also closes the file so you can SaveAs the same name</span>
                db<span style="color: #d2cd86">.</span>CloseInput<span style="color: #d2cd86">(</span><span style="font-weight: bold; color: #e66170">true</span><span style="color: #d2cd86">)</span><span style="color: #b060b0">;</span>
                <span style="font-weight: bold; color: #e66170">string</span> appName <span style="color: #d2cd86">=</span> <span style="color: #02d045">"</span><span style="color: #00c4c4">ACLAYTRANS</span><span style="color: #02d045">"</span><span style="color: #b060b0">;</span>               
                <span style="font-weight: bold; color: #e66170">string</span> msg <span style="color: #d2cd86">=</span> <span style="color: #02d045">"</span><span style="color: #00c4c4">LAYER TRANSLATION MAPPING:\n</span><span style="color: #02d045">"</span><span style="color: #b060b0">;</span>

                <span style="color: #9999a9">// ok time to set the working database</span>

                HostApplicationServices<span style="color: #d2cd86">.</span>WorkingDatabase <span style="color: #d2cd86">=</span> db<span style="color: #b060b0">;</span>
                <span style="font-weight: bold; color: #e66170">using</span> <span style="color: #d2cd86">(</span>Transaction t <span style="color: #d2cd86">=</span> db<span style="color: #d2cd86">.</span>TransactionManager<span style="color: #d2cd86">.</span>StartTransaction<span style="color: #d2cd86">(</span><span style="color: #d2cd86">)</span><span style="color: #d2cd86">)</span>
                <span style="color: #b060b0">{</span>
                    LayerTable lt <span style="color: #d2cd86">=</span> t<span style="color: #d2cd86">.</span>GetObject<span style="color: #d2cd86">(</span>db<span style="color: #d2cd86">.</span>LayerTableId<span style="color: #d2cd86">,</span> OpenMode<span style="color: #d2cd86">.</span>ForRead<span style="color: #d2cd86">)</span> <span style="font-weight: bold; color: #e66170">as</span> LayerTable<span style="color: #b060b0">;</span>
                    <span style="font-weight: bold; color: #e66170">foreach</span><span style="color: #d2cd86">(</span>ObjectId oId <span style="font-weight: bold; color: #e66170">in</span> lt<span style="color: #d2cd86">)</span>
                    <span style="color: #b060b0">{</span>
                        LayerTableRecord ltr <span style="color: #d2cd86">=</span> t<span style="color: #d2cd86">.</span>GetObject<span style="color: #d2cd86">(</span>oId<span style="color: #d2cd86">,</span> OpenMode<span style="color: #d2cd86">.</span>ForRead<span style="color: #d2cd86">)</span> <span style="font-weight: bold; color: #e66170">as</span> LayerTableRecord<span style="color: #b060b0">;</span>
                        
                        ResultBuffer rb <span style="color: #d2cd86">=</span> ltr<span style="color: #d2cd86">.</span>GetXDataForApplication<span style="color: #d2cd86">(</span>appName<span style="color: #d2cd86">)</span><span style="color: #b060b0">;</span>
                        <span style="font-weight: bold; color: #e66170">if</span> <span style="color: #d2cd86">(</span>rb <span style="color: #d2cd86">!</span><span style="color: #d2cd86">=</span> <span style="font-weight: bold; color: #e66170">null</span><span style="color: #d2cd86">)</span>
                        <span style="color: #b060b0">{</span>
                            <span style="font-weight: bold; color: #e66170">string</span> layerName <span style="color: #d2cd86">=</span> ltr<span style="color: #d2cd86">.</span>Name<span style="color: #b060b0">;</span>
                            <span style="color: #9999a9">// Get the values in the xdata</span>
                            <span style="font-weight: bold; color: #e66170">foreach</span> <span style="color: #d2cd86">(</span>TypedValue typeVal <span style="font-weight: bold; color: #e66170">in</span> rb<span style="color: #d2cd86">)</span>
                            <span style="color: #b060b0">{</span>
                             
                                <span style="font-weight: bold; color: #e66170">if</span><span style="color: #d2cd86">(</span>typeVal<span style="color: #d2cd86">.</span>TypeCode <span style="color: #d2cd86">=</span><span style="color: #d2cd86">=</span> <span style="color: #008c00">1000</span><span style="color: #d2cd86">)</span>
                                <span style="color: #b060b0">{</span>
                                    msg <span style="color: #d2cd86">=</span> msg <span style="color: #d2cd86">+</span> layerName <span style="color: #d2cd86">+</span> <span style="color: #02d045">"</span><span style="color: #00c4c4">:</span><span style="color: #02d045">"</span> <span style="color: #d2cd86">+</span> typeVal<span style="color: #d2cd86">.</span>Value <span style="color: #d2cd86">+</span><span style="color: #02d045">"</span><span style="color: #00c4c4">\n</span><span style="color: #02d045">"</span><span style="color: #b060b0">;</span>
                                <span style="color: #b060b0">}</span>
                            <span style="color: #b060b0">}</span>
                        <span style="color: #b060b0">}</span>
                        

                    <span style="color: #b060b0">}</span>
                    t<span style="color: #d2cd86">.</span>Commit<span style="color: #d2cd86">(</span><span style="color: #d2cd86">)</span><span style="color: #b060b0">;</span>
                <span style="color: #b060b0">}</span>
                ed<span style="color: #d2cd86">.</span>WriteMessage<span style="color: #d2cd86">(</span>msg<span style="color: #d2cd86">)</span><span style="color: #b060b0">;</span>
                <span style="color: #9999a9">// reset it back ASAP</span>
              HostApplicationServices<span style="color: #d2cd86">.</span>WorkingDatabase <span style="color: #d2cd86">=</span> oldDb<span style="color: #b060b0">;</span>
                
            <span style="color: #b060b0">}</span>

        <span style="color: #b060b0">}</span>
</pre>
<p>&nbsp;</p>
<p>Output:</p>
<p>LAYER TRANSLATION MAPPING:<br>A:1<br>B:2<br>C:3</p>
