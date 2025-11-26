---
layout: "post"
title: "Navisworks just displays basic geometry of custom entity"
date: "2013-01-17 01:56:00"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/01/navisworks-just-displays-basic-geometry-of-custom-entity.html "
typepad_basename: "navisworks-just-displays-basic-geometry-of-custom-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I created an ObjectDBX for a custom circle, which is derived from AcDbCircle.&#0160; I have a DWG which contains my custom circle. When I opened this DWG in Navisworks, it only displays the base geometry, circle. The custom geometry is missing.&#0160; What is going on? </p>
<p><strong>Solution     <br /></strong>When Navisworks displays AutoCAD entities, it checks if a given object is
a AutoCAD entity type. If a custom entity is derived from AutoCAD basic
geometry, such as AcDbCircle and AcDbLine, Navisworks identify them as AutoCAD
entity, and the custom geometry is ignored: e.g. a custom circle derived from
AcDbCircle will be identified as a Circle only. When an OE is <strong>NOT</strong> loaded
successfully, Navisworks uses proxy graphics, which may show the expected
graphics.&#0160;</p>
<p>If the custom entity derives from the abstract entity such as AcDbCurve, AcDbEntity, Navisworks can identify it as a proxy entity, displaying the custom geometry.</p>
<p>Sample DBX Project<strong>: </strong></p>
<p>We have attached sample projects that contains a DBX and an ARX modules. There are 4 types custom entities that are derived from AcDbEntity, AcDbCurve, AcDbLine and AcDbCircle.. ARX module has a command “<strong>OETest</strong>” which creates instances of each custom entity.</p>
<p>When the OE is not loaded correctly, Navisworks displays them and reports the error: Fig.1 and 2.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901cecd355970b-pi" style="display: inline;"><img alt="2013-6-3 16-45-58" class="asset  asset-image at-xid-6a0167607c2431970b01901cecd355970b" src="/assets/image_301040.jpg" title="2013-6-3 16-45-58" /></a><br /><br /></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 1: geometry displayed in Navisworks when OE is not loaded successfully</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f7de5b970d-pi" style="display: inline;"><img alt="clip_image004" class="asset  asset-image at-xid-6a0167607c2431970b017ee6f7de5b970d" src="/assets/image_273342.jpg" title="clip_image004" /></a></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 2: Scene statistics when OE is not loaded successfully</p>
<p>When the OE is loaded successfully, Navisworks displays them as below and will not report the error: Fig 3 &amp; 4.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019102e30414970c-pi" style="display: inline;"><img alt="2013-6-3 16-47-53" class="asset  asset-image at-xid-6a0167607c2431970b019102e30414970c" src="/assets/image_636169.jpg" title="2013-6-3 16-47-53" /></a><br /><br /></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 3: geometry displayed in Navisworks when OE is loaded successfully</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f7de8b970d-pi" style="display: inline;"><img alt="clip_image008" class="asset  asset-image at-xid-6a0167607c2431970b017ee6f7de8b970d" src="/assets/image_556482.jpg" title="clip_image008" /></a></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Figure 4: Scene statistics when OE is loaded successfully</p>
<p>Please refer to the sample for more details. It is written in VS2008 + SP1 and has been tested with AutoCAD 2011.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017ee6f7e0de970d"><a href="http://adndevblog.typepad.com/files/navisworks_display_basic_geometry_test_vs2008sp1_acad2011.zip">Download Navisworks_display_basic_geometry_test_VS2008SP1_Acad2011</a></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017ee6f7e0de970d">
<span class="asset  asset-generic at-xid-6a0167607c2431970b0191042ce8e5970c"><a href="http://adndevblog.typepad.com/files/navisworks_display_basic_geometry_test_vs2010_acad2014_64bits.zip">Download Navisworks_display_basic_geometry_test_vs2010_acad2014_64bits</a></span><br /></span></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
