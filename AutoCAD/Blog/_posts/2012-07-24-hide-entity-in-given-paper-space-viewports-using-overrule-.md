---
layout: "post"
title: "Hide entity in given paper space viewports using overrule "
date: "2012-07-24 03:27:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/hide-entity-in-given-paper-space-viewports-using-overrule-.html "
typepad_basename: "hide-entity-in-given-paper-space-viewports-using-overrule-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to hide some entities in specific viewports. I thought about using overrule but not sure how to get started.</p>
<p><strong>Solution</strong></p>
<p>If you only need to hide given entities in certain paper space viewports, i.e. you do not need this functionality for model space viewports, then you can use layers which are designed to solve per viewport visibility.</p>
<p>These are the things your AddIn needs to do:</p>
<ol>
<li>Create a clone of the layer that the entity you want to hide in a specific viewport is using. This is needed so that when the entity is drawing itself in the viewports where it is still visible, it will use the same color, line width, etc, that it would originally use&#0160;</li>
<li>For the same reason, you would also need to keep the clone layer properties in sync with the original layer&#0160;</li>
<li>Freeze the clone layer in the appropriate viewports&#0160;</li>
<li>Use overrule to make the entity use the previously created clone layer&#0160;</li>
</ol>
<p>When the layer is cloned, the program adds an extension dictionary entry to it with the ObjectId of the original layer. It also monitors the layers for changes in order to update the cloned layers.</p>
<p>It also adds an extension dictionary entry to the overruled entity with the ObjectId of the clone layer it should use - also the Overrule is filtered to entities that have this specific extension dictionary entry.</p>
<p>And this is the Overrule&#39;s implementation:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyOverrule</span><span style="line-height: 140%;"> : DrawableOverrule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> SetAttributes(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Drawable drawable, DrawableTraits traits)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ret = </span><span style="color: blue; line-height: 140%;">base</span><span style="line-height: 140%;">.SetAttributes(drawable, traits);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ObjectId id = HideControl.GetEntityLayerId(drawable);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!id.IsNull)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; traits.Layer = id;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> ret;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The attached project is just to show the concept, and is not fully implemented or tested.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017743711f8e970d"><a href="http://adndevblog.typepad.com/files/_hideobjectpervp_2011-02-15.zip">Download _hideobjectpervp_2011-02-15</a></span></p>
