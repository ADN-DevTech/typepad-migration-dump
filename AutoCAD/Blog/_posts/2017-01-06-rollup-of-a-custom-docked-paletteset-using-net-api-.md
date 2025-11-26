---
layout: "post"
title: "Rollup of a custom docked PaletteSet using .NET API "
date: "2017-01-06 00:52:46"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "2016"
  - "AutoCAD"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2017/01/rollup-of-a-custom-docked-paletteset-using-net-api-.html "
typepad_basename: "rollup-of-a-custom-docked-paletteset-using-net-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html">Deepak Nadig</a></p>
<p>This is my first post of 2017 and here is wishing you all a&#0160;<span style="color: #00bf00;">&quot;Happy New Year&quot; :-)</span></p>
<p>Recently, we had a query from an ADN partner:<br /><strong><span style="background-color: #80c0ff;"><em>How to roll up a docked and hidden custom PaletteSet using.NET &#0160;API ?</em></span></strong></p>
<p>The behaviour the ADN partner is expecting to be accomplished using .NET API can be seen in the below screencast:&#0160;&#0160;</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="590" src="https://screencast.autodesk.com/Embed/Timeline/074e2119-413c-4f51-867f-80152066e66a" webkitallowfullscreen="" width="640"></iframe>With the help of my colleague <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html">Madhu</a>, we figured out the answer is to set PaletteSet.RolledUp to false.</p>
<p>In the below code, command&#0160;MyPalette launches a docked palette and command&#0160;ExpandPalette rolls up the palette. Subsequently, screencast shows the testing of the code.</p>
<pre style="color: #d1d1d1; background: #000000;"><span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">class</span> MyCommands
<span style="color: #b060b0;">{</span>
    <span style="color: #e66170; font-weight: bold;">static</span> System<span style="color: #d2cd86;">.</span>Windows<span style="color: #d2cd86;">.</span>Forms<span style="color: #d2cd86;">.</span>Timer Clock<span style="color: #b060b0;">;</span>
    <span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">static</span> PaletteSet m_ps <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">null</span><span style="color: #b060b0;">;</span>
    <span style="color: #d2cd86;">[</span>CommandMethod<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">MyPalette</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">]</span>
    <span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">void</span> MyPalette<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span>
    <span style="color: #b060b0;">{</span>
        <span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span>m_ps <span style="color: #d2cd86;">=</span><span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">null</span><span style="color: #d2cd86;">)</span>
        <span style="color: #b060b0;">{</span>
            m_ps <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> PaletteSet<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">My Palette 1</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">,</span>
            <span style="color: #e66170; font-weight: bold;">new</span> Guid<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">170B0084-7B01-487E-9CBC-C7018588F26F</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            m_ps<span style="color: #d2cd86;">.</span>SetLocation<span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">new</span> System<span style="color: #d2cd86;">.</span>Drawing<span style="color: #d2cd86;">.</span>Point<span style="color: #d2cd86;">(</span><span style="color: #008c00;">312</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">763</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            m_ps<span style="color: #d2cd86;">.</span>SetSize<span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">new</span> System<span style="color: #d2cd86;">.</span>Drawing<span style="color: #d2cd86;">.</span>Size<span style="color: #d2cd86;">(</span><span style="color: #008c00;">909</span><span style="color: #d2cd86;">,</span> <span style="color: #008c00;">40</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            m_ps<span style="color: #d2cd86;">.</span>DockEnabled <span style="color: #d2cd86;">=</span> DockSides<span style="color: #d2cd86;">.</span>Bottom<span style="color: #b060b0;">;</span>
            <span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span>m_ps<span style="color: #d2cd86;">.</span>Dock <span style="color: #d2cd86;">=</span><span style="color: #d2cd86;">=</span> DockSides<span style="color: #d2cd86;">.</span>None<span style="color: #d2cd86;">)</span>
            <span style="color: #b060b0;">{</span>
                m_ps<span style="color: #d2cd86;">.</span>AutoRollUp <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
                m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #b060b0;">;</span>
                m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
            <span style="color: #b060b0;">}</span>
            <span style="color: #9999a9;">// If the palette is docked,</span>
            <span style="color: #9999a9;">// we need to undock it first.</span>
            <span style="color: #e66170; font-weight: bold;">else</span>
            <span style="color: #b060b0;">{</span>
                m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #b060b0;">;</span>
                m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
                CreateTimer<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            <span style="color: #b060b0;">}</span>
        <span style="color: #b060b0;">}</span>
        m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
    <span style="color: #b060b0;">}</span>
    <span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">static</span> <span style="color: #e66170; font-weight: bold;">void</span> CreateTimer<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span>
    <span style="color: #b060b0;">{</span>
        Clock <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> System<span style="color: #d2cd86;">.</span>Windows<span style="color: #d2cd86;">.</span>Forms<span style="color: #d2cd86;">.</span>Timer<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        Clock<span style="color: #d2cd86;">.</span>Interval <span style="color: #d2cd86;">=</span> <span style="color: #008c00;">500</span><span style="color: #b060b0;">;</span>
        Clock<span style="color: #d2cd86;">.</span>Start<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        Clock<span style="color: #d2cd86;">.</span>Tick <span style="color: #d2cd86;">+</span><span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">new</span> EventHandler<span style="color: #d2cd86;">(</span>Timer_Tick<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
    <span style="color: #b060b0;">}</span>
    <span style="color: #e66170; font-weight: bold;">static</span> <span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">void</span> Timer_Tick<span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">object</span> sender<span style="color: #d2cd86;">,</span>
    EventArgs eArgs<span style="color: #d2cd86;">)</span>
    <span style="color: #b060b0;">{</span>
        <span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span>sender <span style="color: #d2cd86;">=</span><span style="color: #d2cd86;">=</span> Clock<span style="color: #d2cd86;">)</span>
        <span style="color: #b060b0;">{</span>
            m_ps<span style="color: #d2cd86;">.</span>Dock <span style="color: #d2cd86;">=</span> DockSides<span style="color: #d2cd86;">.</span>None<span style="color: #b060b0;">;</span>
            m_ps<span style="color: #d2cd86;">.</span>AutoRollUp <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
            m_ps<span style="color: #d2cd86;">.</span>Dock <span style="color: #d2cd86;">=</span> DockSides<span style="color: #d2cd86;">.</span>Left<span style="color: #b060b0;">;</span>
            <span style="color: #9999a9;">// Note: we need to update the palette</span>
            <span style="color: #9999a9;">// window. I found turning it off and</span>
            <span style="color: #9999a9;">// on is the most robust way.</span>
            m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #b060b0;">;</span>
            m_ps<span style="color: #d2cd86;">.</span>Visible <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">true</span><span style="color: #b060b0;">;</span>
            <span style="color: #9999a9;">// Stop the clock and destroy it.</span>
            Clock<span style="color: #d2cd86;">.</span>Stop<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
            Clock<span style="color: #d2cd86;">.</span>Dispose<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
        <span style="color: #b060b0;">}</span>
    <span style="color: #b060b0;">}</span>
    <span style="color: #d2cd86;">[</span>CommandMethod<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">ExpandPalette</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">]</span>
    <span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">static</span> <span style="color: #e66170; font-weight: bold;">void</span> CheckPaletteSetState<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span>
    <span style="color: #b060b0;">{</span>
        <span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span>m_ps <span style="color: #d2cd86;">!</span><span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">null</span><span style="color: #d2cd86;">)</span>
        <span style="color: #b060b0;">{</span>
            m_ps<span style="color: #d2cd86;">.</span>RolledUp <span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">false</span><span style="color: #b060b0;">;</span>
        <span style="color: #b060b0;">}</span>
    <span style="color: #b060b0;">}</span>
<span style="color: #b060b0;">}</span>
</pre>
<p>&#0160;&#0160;Screencast :&#0160;</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="620" src="https://screencast.autodesk.com/Embed/Timeline/cf3585a7-9e33-4101-8db2-76dab7493cb8" webkitallowfullscreen="" width="640"></iframe></p>
<p>&#0160;</p>
