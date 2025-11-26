---
layout: "post"
title: "Identify Mechanical Desktop file(MDT) using .NET"
date: "2018-07-30 21:52:27"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "2017"
  - "AutoCAD"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2018/07/identify-mechanical-desktop-filemdt-using-net.html "
typepad_basename: "identify-mechanical-desktop-filemdt-using-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Recently, we had a customer query regarding distinguishing an AutoCAD Mechanical desktop file among a set of drawing files.</p>
<p>Answer: <br />Mechanical desktop should have entry named AmdtFileType with a value of 84&#0160;in the Custom tab of DWGPROPS(image)</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3852eb8200d-pi"><img alt="CustomProp" class="asset  asset-image at-xid-6a0167607c2431970b022ad3852eb8200d img-responsive" src="/assets/image_471956.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="CustomProp" /></a></p>
<p>One of the ways to distinguish a Autodesk Mechanical desktop file&#0160;could be to retrieve and check for the custom property as below.<br /><em>Note :</em> File to be checked is read as a side database in the code below.&#0160;</p>
<pre style="color: #d1d1d1; background: #000000;"><span style="color: #d2cd86;">[</span>CommandMethod<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">testMdtFile</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">]</span>
<span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">static</span> <span style="color: #e66170; font-weight: bold;">void</span> testMdtFile<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span>
<span style="color: #b060b0;">{</span>
    Editor ed <span style="color: #d2cd86;">=</span> Application<span style="color: #d2cd86;">.</span>DocumentManager<span style="color: #d2cd86;">.</span>MdiActiveDocument<span style="color: #d2cd86;">.</span>Editor<span style="color: #b060b0;">;</span>
    <span style="color: #e66170; font-weight: bold;">try</span>
    <span style="color: #b060b0;">{</span>
        <span style="color: #e66170; font-weight: bold;">using</span> <span style="color: #d2cd86;">(</span>Database db <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> Database<span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">false</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span>
        <span style="color: #b060b0;">{</span>
            db<span style="color: #d2cd86;">.</span>ReadDwgFile<span style="color: #d2cd86;">(</span><span style="color: #02d045;">@&quot;</span><span style="color: #00c4c4;">c:\temp\testMDT.dwg</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">,</span> FileOpenMode<span style="color: #d2cd86;">.</span>OpenForReadAndAllShare<span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">null</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            db<span style="color: #d2cd86;">.</span>CloseInput<span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">true</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            <span style="color: #e66170; font-weight: bold;">string</span> val <span style="color: #d2cd86;">=</span> GetCustomProperty<span style="color: #d2cd86;">(</span>db<span style="color: #d2cd86;">,</span> <span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">AmdtFileType</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            System<span style="color: #d2cd86;">.</span>Windows<span style="color: #d2cd86;">.</span>Forms<span style="color: #d2cd86;">.</span>MessageBox<span style="color: #d2cd86;">.</span>Show<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">Value of AmdtFileType is </span><span style="color: #02d045;">&quot;</span> <span style="color: #d2cd86;">+</span> val<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        <span style="color: #b060b0;">}</span>
    <span style="color: #b060b0;">}</span>
    <span style="color: #e66170; font-weight: bold;">catch</span> <span style="color: #d2cd86;">(</span>System<span style="color: #d2cd86;">.</span>Exception ex<span style="color: #d2cd86;">)</span>
    <span style="color: #b060b0;">{</span>
        ed<span style="color: #d2cd86;">.</span>WriteMessage<span style="color: #d2cd86;">(</span>ex<span style="color: #d2cd86;">.</span>ToString<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
    <span style="color: #b060b0;">}</span>
<span style="color: #b060b0;">}</span>
<span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">static</span> <span style="color: #e66170; font-weight: bold;">string</span> GetCustomProperty<span style="color: #d2cd86;">(</span>Database db<span style="color: #d2cd86;">,</span> <span style="color: #e66170; font-weight: bold;">string</span> key<span style="color: #d2cd86;">)</span>
<span style="color: #b060b0;">{</span>
    DatabaseSummaryInfoBuilder sumInfo <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> DatabaseSummaryInfoBuilder<span style="color: #d2cd86;">(</span>db<span style="color: #d2cd86;">.</span>SummaryInfo<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
    IDictionary custProps <span style="color: #d2cd86;">=</span> sumInfo<span style="color: #d2cd86;">.</span>CustomPropertyTable<span style="color: #b060b0;">;</span>
    <span style="color: #e66170; font-weight: bold;">return</span> <span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">string</span><span style="color: #d2cd86;">)</span>custProps<span style="color: #d2cd86;">[</span>key<span style="color: #d2cd86;">]</span><span style="color: #b060b0;">;</span>
<span style="color: #b060b0;">}</span>
</pre>
