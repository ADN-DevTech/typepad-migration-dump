---
layout: "post"
title: "Detecting Ribbon Panel Visibility Change"
date: "2015-05-05 00:02:10"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/detecting-ribbon-panel-visibility-change.html "
typepad_basename: "detecting-ribbon-panel-visibility-change"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>I’ve received a query from an ADN partner on how to get notified when Ribbon panel visibility is changed.</p>
<p>Here is a self explanatory code, all we need is to listen to</p>
<p>IsVisibleChanged event.</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> TAB_ID = <span style="color: #a31515;">&quot;ID_CUSTOMCMDS&quot;</span>;</p>
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;addRibbon&quot;</span>)]</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> addRibbon()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonControl</span> rbnCtrl =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">RibbonServices</span>.RibbonPaletteSet.RibbonControl;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">RibbonTab</span> rbnTab = <span style="color: blue;">new</span>&#0160; <span style="color: #2b91af;">RibbonTab</span>();</p>
<p style="margin: 0px;">rbnTab.Title = <span style="color: #a31515;">&quot;Custom commands&quot;</span>;</p>
<p style="margin: 0px;">rbnTab.Id = TAB_ID;</p>
<p style="margin: 0px;">rbnCtrl.Tabs.Add(rbnTab);</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonPanelSource</span> rbnPnlSrc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> Autodesk.Windows.<span style="color: #2b91af;">RibbonPanelSource</span>();</p>
<p style="margin: 0px;">rbnPnlSrc.Title = <span style="color: #a31515;">&quot;Custom Panel&quot;</span>;</p>
<p style="margin: 0px;"><span style="color: green;">/*Ribbon Panel visibility */</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">RibbonPanel</span> rbnPnl = <span style="color: blue;">new</span> <span style="color: #2b91af;">RibbonPanel</span>();</p>
<p style="margin: 0px;">rbnPnl.IsVisibleChanged += rbnPnl_IsVisibleChanged;</p>
<p style="margin: 0px;">rbnPnl.Source = rbnPnlSrc;</p>
<p style="margin: 0px;">rbnTab.Panels.Add(rbnPnl);</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonButton</span> rbnBtn =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> Autodesk.Windows.<span style="color: #2b91af;">RibbonButton</span>();</p>
<p style="margin: 0px;">rbnBtn.Text = <span style="color: #a31515;">&quot;NETLOAD&quot;</span>;</p>
<p style="margin: 0px;">rbnBtn.CommandParameter = <span style="color: #a31515;">&quot;NETLOAD&quot;</span>;</p>
<p style="margin: 0px;">rbnBtn.ShowText = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Autodesk.Windows.<span style="color: #2b91af;">RibbonToolTip</span> rbnTT =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">RibbonToolTip</span>();</p>
<p style="margin: 0px;">rbnTT.Command = <span style="color: #a31515;">&quot;NETLOAD&quot;</span>;</p>
<p style="margin: 0px;">rbnTT.Title = <span style="color: #a31515;">&quot;Load a.NET assembly&quot;</span>;</p>
<p style="margin: 0px;">rbnTT.Content = <span style="color: #a31515;">&quot;Command to load a.NET assembly in AutoCAD&quot;</span>;</p>
<p style="margin: 0px;">rbnBtn.ToolTip = rbnTT;</p>
<p style="margin: 0px;">rbnPnlSrc.Items.Add(rbnBtn);</p>
<p style="margin: 0px;">rbnTab.IsActive = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">void</span> rbnPnl_IsVisibleChanged(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Application</span>.ShowAlertDialog(<span style="color: #a31515;">&quot;Event triggered, Panel visiblity changed&quot;</span>);</p>
<p style="margin: 0px;">}</p>
</div>
