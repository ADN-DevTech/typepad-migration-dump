---
layout: "post"
title: "Render Setup improvement in Maya 2017: Render Settings Changes"
date: "2016-11-25 14:30:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "Maya"
  - "Plug-in"
  - "Python"
  - "Rendering"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/render-setup-improvement-in-maya-2017-render-settings-changes.html "
typepad_basename: "render-setup-improvement-in-maya-2017-render-settings-changes"
typepad_status: "Publish"
---

<h3><font size="2"></font></h3>  <p>The 2<sup>nd</sup> main improvement is about changes to render settings window callbacks to support the new workspaces UI feature, as I mentioned before, this information is also provided by our engineering team. Let’s take a look at the improvement.</p>  <h3><font style="font-weight: bold" size="3">Introduction</font></h3>  <p>One of the changes introduced in Maya 2017 is the new workspaces feature. This feature includes a change that presents some problems for the Render Settings. Maya now retains the state of windows that were open in previous sessions of Maya. So, if you open the Render Settings and close Maya, then re-launch Maya, the Render Settings will still be open on startup. With the Render Settings this is a problem because by default they were being loaded prior to the renderer plugins which are needed to help build the UI. This was fixed in Maya 2017 by only loading the Render Settings window after all Maya plugins have been loaded. An additional problem is that renderers need to be able to load their renderer specific Render Settings information, including renderer specific tabs, once the Render Settings have been loaded. Previously this was done after users manually opened the Render Settings, but now the Render Settings can be open on startup so a different solution is required. This document showcases the new callback that is introduced to enable renderers to register their respective Render Settings information, including renderer specific tabs, after the Render Settings have been built.</p>  <h3><font style="font-weight: bold" size="3">Render Settings Built Callback</font></h3>  <p>Once the Render Settings window has been built, renderers may wish to setup renderer specific tabs or make other Render Settings related changes. Previously this was done after the Render Settings was launched manually by users, but with workspaces changes in Maya 2017 it is possible that the Render Settings window will be loaded on startup of Maya if the Render Settings window was not closed in the last session of Maya. As a result, a callback is now required in order to apply any renderer specific changes to the Render Settings such as adding a renderer specific tab.</p>  <pre class="brush:python;toolbar: false;">callbacks 
	–addCallback ”rendererRenderSettingsBuiltCallback”
	–hook ”renderSettingsBuilt”
	–owner ”renderer”</pre>

<p>The above function calls the “rendererRenderSettingsBuiltCallback” after the Render Settings have been built. Renderers can use this callback to create their renderer specific tabs or any other Render Settings specific operation knowing that these will be run after the Render Settings window has been created.</p>
