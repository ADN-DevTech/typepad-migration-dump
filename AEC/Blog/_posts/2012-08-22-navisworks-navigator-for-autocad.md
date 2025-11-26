---
layout: "post"
title: "NavisWorks navigator for AutoCAD"
date: "2012-08-22 21:03:00"
author: "Xiaodong Liang"
categories:
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/08/navisworks-navigator-for-autocad.html "
typepad_basename: "navisworks-navigator-for-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue     <br /></strong>We can not find the Navigator ARX-Module of NavisWorks 2011 for AutoCAD. For 32 Bit and 64 Bit as well. Where can we find them? Is it a special download?&#160; Or do they not exist any more?</p>  <p><a name="section2"></a><strong>Solution     <br /></strong>This no longer exists in the same form. It’s now a NET DLL plug-in for AutoCAD, so you need to look for nwnavigator2011.dll in AutoCAD’s root. Navigator has been ported to a NET plug-in wrapping our ActiveX for AutoCAD 2008 onwards, as this enables 64-bit support.</p>  <p>Per AutoCAD 2012 and 2013, the navigator modules are installed under the AutoCAD entry point, and also note that the module name has a suffix of “_11”. Take AutoCAD 2013 for example,&#160; there should be a file <strong>nwnavigator2013_11.dll</strong> in the&#160; directory as&#160; </p>  <p>&quot;C:\Program Files\Autodesk\AutoCAD 2013&quot;.</p>  <p>Note: To get navigator, AutoCAD or its vertical products require Navisworks exporter. So you need to install AutoCAD firstly and then install Navisworks exporter . </p>
