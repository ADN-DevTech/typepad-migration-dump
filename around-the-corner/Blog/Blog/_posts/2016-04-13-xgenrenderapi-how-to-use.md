---
layout: "post"
title: "XGenRenderAPI: How to use?"
date: "2016-04-13 02:31:48"
author: "Vijaya Prakash"
categories:
  - "Animation"
  - "Autodesk"
  - "C++"
  - "Maya"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/04/xgenrenderapi-how-to-use.html "
typepad_basename: "xgenrenderapi-how-to-use"
typepad_status: "Publish"
---

<p>Maya is using procedurals for rendering translators. One of the reason of using procedurals is because the translators of some renderers are not extensible. For example, to keep modules independent, Mentalray’s <span style="color: #0060bf;">Mayatomr.mll</span> and XGen’s<span style="color: #0060bf;"> libAdskXGen.dll</span> are not linked together. This means that the code using <span style="color: #0060bf;">XGenRenderAPI</span> doesn’t require a procedural. If third party renderer doesn’t support procedural, you can directly use <span style="color: #0060bf;">XGenRenderAPI</span> in the translator.</p>
<p>To do this, you can call&#0160;<span style="color: #0060bf;">XGenRenderAPI::PatchRenderer::init()</span> to initialize XGen primitive generation, and call <span style="color: #0060bf;">FaceRenderer::render()</span> to generate XGen hairs. You will also need to implement <span style="color: #0060bf;">ProceduralCallbacks::flush()</span> to collect XGen bplines and convert them to the renderer’s curve representation.</p>
<p>Here is the XGen RenderMan Documentation about arguments. In this documentation, we have clearly mentioned the list of arguments that can be used for the procedural, along with descriptions:</p>
<p><a href="https://knowledge.autodesk.com/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/Maya/files/GUID-3359F371-EAC1-4E56-BF7A-A544202CB41D-htm.html">https://knowledge.autodesk.com/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/Maya/files/GUID-3359F371-EAC1-4E56-BF7A-A544202CB41D-htm.html</a></p>
<p>The “-file” and “-geom” flags can be set to non-existence files. The API checks for the flags, but it will not actually load the files if the collection/description already exist in the scene.</p>
<p>You can enable debug info in the Maya GUI in the XGen Panel, Click Log -&gt; Debug Level -&gt; 5(high). The logs can be found in the window below titled “Log”(drag to expand the panel).</p>
