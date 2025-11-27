---
layout: "post"
title: "DISP_E_MEMBERNOTFOUND error from iLogic"
date: "2014-04-03 02:12:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/04/disp_e_membernotfound-error-from-ilogic.html "
typepad_basename: "disp_e_membernotfound-error-from-ilogic"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da033b9970d-pi" style="display: inline;"><img alt="ILogic_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73da033b9970d image-full img-responsive" src="/assets/image_d515a0.jpg" title="ILogic_error" /></a></p>
<p>If you have an <strong>iLogic Rule</strong> which is using code that works fine inside a <strong>.NET</strong> application or AddIn, but throws a&#0160;<strong>DISP_E_MEMBERNOTFOUND</strong> error from inside the <strong>iLogic Rule</strong>, then it could be that you just need to declare a variable using the specific type of the object. E.g. this code:</p>
<pre>PartDoc=ThisApplication.ActiveDocument
PartcompDef=PartDoc.ComponentDefinition
ExtFeature=PartcompDef.Features.ExtrudeFeatures.item(1)
<strong>ExtDef=ExtFeature.Definition</strong>
&#39; ...
ExtDef.Profile=ExtProfile</pre>
<p>would need to be changed to this:</p>
<pre>PartDoc=ThisApplication.ActiveDocument
PartcompDef=PartDoc.ComponentDefinition
ExtFeature=PartcompDef.Features.ExtrudeFeatures.item(1)
<strong>Dim ExtDef As ExtrudeDefinition =ExtFeature.Definition</strong>
&#39; ...
ExtDef.Profile=ExtProfile</pre>
<p>&#0160;</p>
