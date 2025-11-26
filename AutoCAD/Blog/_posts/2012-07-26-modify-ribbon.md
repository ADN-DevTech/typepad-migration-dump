---
layout: "post"
title: "Modify Ribbon"
date: "2012-07-26 04:51:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/modify-ribbon.html "
typepad_basename: "modify-ribbon"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to modify the Ribbon content - add/remove panels, etc. How could I do it?</p>
<p><strong>Solution</strong></p>
<p>This is only available through the .NET API&#39;s.</p>
<p>There are two sets of functionalities:</p>
<p><strong>1) Ribbon Runtime API</strong> - provided by <strong>AdWindows.dll</strong> under <strong>Autodesk.Windows</strong> namespace</p>
<p>Enables you to edit the Ribbon, but the changes will not be persisted in the CUIx file, so if AutoCAD is restarted, or even if the current workspace is changed (WSCURRENT system variable) or the menu file is reloaded then the changes will be gone and you need to redo them.</p>
<p>Here is a small sample that toggle&#39;s the visiblity of the <strong>Home</strong> tab:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;HideShowHomeTab&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static&#0160;</span><span style="color: blue; line-height: 140%;">public&#0160;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> HideShowHomeTab()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.Windows.RibbonControl rc = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Autodesk.Windows.ComponentManager.Ribbon;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.Windows.RibbonTab tab =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; rc.FindTab(</span><span style="color: #a31515; line-height: 140%;">&quot;ACAD.ID_TabHome3D&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tab != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; tab.IsVisible = !tab.IsVisible;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p><strong>2) CUI API</strong> - provided by <strong>AcCui.dll</strong> under <strong>Autodesk.AutoCAD.Customization</strong> namespace</p>
<p>You can use this to modify the user interface by modifying the CUIx file the same way as through the CUI Dialog and these changes will be persisted.</p>
<p>This sample as well toggles the visibility of the <strong>Home</strong> tab, but using the CUI API:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using&#0160;</span>acApp = Autodesk.AutoCAD.ApplicationServices.<span style="font-size: 8pt;">Application;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;HideShowHomeTabInCurrentWorkspace&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static&#0160;</span><span style="color: blue; line-height: 140%;">public&#0160;</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> HideShowHomeTabInCurrentWorkspace()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> menuName = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; (</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">)acApp</span><span style="line-height: 140%;">.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;MENUNAME&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> curWorkspace =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; (</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">)acApp</span><span style="line-height: 140%;">.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;WSCURRENT&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.AutoCAD.Customization.CustomizationSection cs =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Customization.CustomizationSection(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; menuName + </span><span style="color: #a31515; line-height: 140%;">&quot;.cuix&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.AutoCAD.Customization.WSRibbonRoot rr = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cs.getWorkspace(curWorkspace).WorkspaceRibbonRoot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Autodesk.AutoCAD.Customization.WSRibbonTabSourceReference tab = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; rr.FindTabReference(</span><span style="color: #a31515; line-height: 140%;">&quot;ACAD&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;ID_TabHome3D&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tab != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; tab.Show = !tab.Show;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cs.IsModified)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cs.Save();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Reload to see the changes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acApp.ReloadAllMenus();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Note: The above samples only work without modification in case of using vanilla AutoCAD (where the menu group is <strong>ACAD</strong>) and inside <strong>3D Modeling</strong> workspace (where the <strong>Home</strong> tab&#39;s id is <strong>ID_TabHome3D</strong>)</p>
