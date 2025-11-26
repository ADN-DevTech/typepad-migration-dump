---
layout: "post"
title: "Missing object enabler for application error in Navisworks"
date: "2012-06-24 19:46:49"
author: "Mikako Harada"
categories:
  - "Mikako Harada"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/06/missing-object-enabler-for-application-error-in-navisworks.html "
typepad_basename: "missing-object-enabler-for-application-error-in-navisworks"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I created an ObjectDBX for a custom object. When the DWG is opened in Navisworks, the custom geometry is not shown, and&#0160;in [Home] &gt;&gt; [Project] &gt;&gt; [Scene Statistics], it reports an error:</p>
<p>&#0160;&#0160;&#0160; &quot;Missing object enabler for application &#39;MyCustomerObjectApp …&#39; &quot;</p>
<p>How can I make my OE to work with Navisworks?&#0160;</p>
<p><strong>Solution</strong></p>
<p>Navisworks uses RealDWG to read DWG files.&#0160; The same mechanism for registering and demand loading for AutoCAD and RealDWG application apply for Navisworks. Please take a look at &quot;Demand Loading on Detection of Custom Objects&quot; section of “ObjectARX Developer Guide” about how to register your OE for demand loading.</p>
<p>Assuming that your custom Object Enabler (OE) is correctly registered, and you still see an issue, here are a few points that you may want to check:</p>
<ul>
<li>Navisworks cache (.nwc) file - when a dwg file is opened, Navisworks creates a cached file with the same file name with the extension .nwc by default.&#0160; If you try to open a dwg file that has nwc present in the same folder and nwc is younger than dwg file, navisworks will go straight to nwc file. If you set the demand load after nwc is created, you will need to delete the nwc file to make sure Navisworks will open the dwg again. </li>
<li>Missing dependency - Another possibility of causing the failure of demand load is missing dlls that dbx is depending on.&#0160; You can check it using a tool like depends or Dependency Walker.</li>
<li>[Scene Statistics] ([Home] &gt;&gt; [Project] &gt;&gt; [Scene Statistic]) dialog shows you if there is missing dll or proxy objects in the dwg file. You may want to check there if the demand loading is really failing or not.</li>
<li>Rendering – Navisworks uses RealDWG to open the drawings. However, they then convert it to their own proprietary format which they then render themselves. Resulting render appearance may not be exactly the same as AutoCAD. e.g. ObjectARX SDK sample polysamp defines an entity using shell geometry. In AutoCAD, the entity is not rendered, while in Navisworks, it renders it.</li>
</ul>
<p>&#0160;</p>
