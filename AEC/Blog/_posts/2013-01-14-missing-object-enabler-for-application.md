---
layout: "post"
title: "Missing object enabler for application"
date: "2013-01-14 01:19:00"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/01/missing-object-enabler-for-application.html "
typepad_basename: "missing-object-enabler-for-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I created an ObjectDBX for a custom object. When the DWG is opened in Navisworks, the custom geometry is not shown and&#0160; [View]--&gt;[Scene Statistics] reports an error:</p>
<p>&#0160;&#0160; Missing object enabler for application &quot;MyCustomerObjectApp……”</p>
<p>How can I make my OE to work with Navisworks? </p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong>:<br />Basically, Navisworks uses RealDWG to read DWG files.&#0160; The same mechanism for AutoCAD or RealDWG applaications’ demand loading works for Navisworks. Please take a look at [Demand Loading on Detection of Custom Objects] section of “ObjectARX Developer Guide” about how to register your OE for demand loading.</p>
<p>Assuming that your custom Object Enabler (OE) is correctly registered, there are a few points that you may want to be aware of: </p>
<p>· Navisworks cach (.nwc) file - when a dwg file is opened, Navisworks creates a cached file with the same file name with the extension .nwc by default.&#0160; If you try to open a dwg file that has nwc is present in the same folder and nwc is younger than dwg file, navisworks will go straight to nwc.&#0160; If you set the demand load after nwc is created, you will need to delete the nwc file to make sure navisworks will open the dwg again. </p>
<p>· Missing dependency - Another possibility of causing the failure of demand load is missing dlls that dbx is depending on.&#0160; You can check it using a tool like depends or Dependency Walker.</p>
<p>· [Scene Statistics] – [View] &gt;&gt; [Scene Statistic] dialog shows you if there is missing dll or proxy objects in the dwg file. You may want to check here if the demand loading is really failing or not. </p>
<p>· Rendering – Navisworks uses RealDWG to open the drawings. However, they then convert it to their own proprietary format which they then render themselves. Resulting render appearance may not be exactly the same as AutoCAD. e.g. ObjectARX SDK sample <strong>polysamp</strong> defines an entity using shell geometry. In AutoCAD, the entity is not rendered, while in Navisworks, it renders it.</p>
<p>Here is more detailed description of how to define your OE.</p>
<p>1. Some of the built-in OEs used by Navisworks locate in &lt;Navisworks Installation path&gt;\lcdbx****, where **** means the corresponding version of DBX. e.g. AutoCAD 2010 DBX is put into &lt;Navisworks Installation path&gt;\lcdbx2010 (Fig. 1). Once your OE is available, you could put your OE under this path. </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f8342c8970c-pi"><img alt="clip_image001" border="0" height="225" src="/assets/image_288949.jpg" style="display: inline; border: 0px;" title="clip_image001" width="520" /></a></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #0000ff;">Figure 1: OE path</span></p>
<p>2. Registry: some options to let Navisworks know your OE</p>
<p>1) HKLM (or HKCU)\SOFTWARE\Autodesk\ObjectDBX\R18.0\Applications (Fig. 2)</p>
<p>The OE under this registry will be used for all Autodesk products.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c355457a8970b-pi"><img alt="clip_image003" border="0" height="199" src="/assets/image_184104.jpg" style="display: inline; border: 0px;" title="clip_image003" width="503" /></a></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #0000ff;"> Figure 2: registry of HKCU/HKLM ObjectDBX section </span></p>
<p>2) HKLM (or HKCU)\SOFTWARE\Autodesk\NavisWorks Manage\7.0\ObjectDBX\R***. Where *** means the corresponding version of DBX. e.g. AutoCAD 2010 corresponds to R18.0. </p>
<p>The OE under this registry is only used for Navisworks.</p>
<p>The key name must be the argument “APP” in <strong>ACRX_DXF_DEFINE_MEMBERS</strong> where the Custom Entity is defined. This name may not be the same as the DBX dll name. e.g. in the attached sample, The APP name is <strong>NWOEDBXAPP</strong>, while the DBX name is <strong>NWOEDBX</strong>. As shown in Fig. 3.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f83431a970c-pi"><img alt="clip_image005" border="0" height="309" src="/assets/image_900786.jpg" style="display: inline; border: 0px;" title="clip_image005" width="499" /></a></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 3: registry of HKCU/HKLM Host Application section</span></p>
<p>Location and order of search for demand loading setting is as follows: </p>
<p>· HKCU Host Application section </p>
<p>· HKLM Host Application section </p>
<p>· HKCU ObjectDBX section </p>
<p>· HKLM ObjectDBX section</p>
<p>3) HKLM (or HKCU)\SOFTWARE\Autodesk\Navisworks Manage\7.0\ObjectDBX Modules (Fig. 4)</p>
<p>The OE under this registry is only used for Navisworks, too. You can just create a string to specify the OE path. It forces to load all the time (as oppose to demand loading). This may be a tip while troubleshooting.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f834368970c-pi"><img alt="clip_image007" border="0" height="127" src="/assets/image_90209.jpg" style="display: inline; border: 0px;" title="clip_image007" width="504" /></a></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 4: Registry of ObjectDBX Modules</span></p>
<p>If any of them contains the correct OE, it will successfully identify your custom entity. Otherwise, it will fail to know it and pop out the error: “Missing object enabler for application.”</p>
<p>3. In [Tools] &gt;&gt; [Global Options…] &gt;&gt; [File Readers] &gt;&gt; [DWG/DXF/SAT] &gt;&gt; [DWG Loader Version], please ensure your [DWG Loader Version] is set to 2010 if you are opening a DWG of AutoCAD 2010. (Figure 5) </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f7b17b970d-pi"><img alt="clip_image009" border="0" height="350" src="/assets/image_172307.jpg" style="display: inline; border: 0px;" title="clip_image009" width="499" /></a></p>
<p><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 5: DWG Loader Version</span></p>
