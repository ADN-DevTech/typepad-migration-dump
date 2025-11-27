---
layout: "post"
title: "Use Thickness from Rule"
date: "2014-05-22 02:51:22"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/05/use-thickness-from-rule.html "
typepad_basename: "use-thickness-from-rule"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can find many settings in the <strong>Sheet Metal Defaults</strong> dialog, including <strong>Use Thickness from Rule</strong>.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dc8e260970d-pi" style="display: inline;"><img alt="UseThickness" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73dc8e260970d image-full img-responsive" src="/assets/image_5cafa8.jpg" title="UseThickness" /></a><br />This can be controlled from the <strong>SheetMetalComponentDefinition</strong> object&#39;s&#0160;<strong>UseSheetMetalStyleThickness</strong> property. Here is a VBA sample:</p>
<pre>Sub ChangeUseThicknessFromRule()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim cd As SheetMetalComponentDefinition
  Set cd = doc.ComponentDefinition
  
  cd.UseSheetMetalStyleThickness = False
End Sub</pre>
<p>&#0160;</p>
