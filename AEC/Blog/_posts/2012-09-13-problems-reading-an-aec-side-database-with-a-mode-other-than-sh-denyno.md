---
layout: "post"
title: "Problems reading an AEC side database with a mode other than SH_DENYNO"
date: "2012-09-13 06:44:26"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Architecture"
  - "OMF"
original_url: "https://adndevblog.typepad.com/aec/2012/09/problems-reading-an-aec-side-database-with-a-mode-other-than-sh_denyno.html "
typepad_basename: "problems-reading-an-aec-side-database-with-a-mode-other-than-sh_denyno"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When a side database is read in using any mode other than SH_DENYNO, some OMF functionalities do not work properly. For example, when wall properties are queried, the following error may occur:</p>
<p>*Area Not Found*</p>
<p><strong>Solution</strong></p>
<p>The problem is due to the way that AutoCAD handles the reading of side databases, thus AutoCAD Architecture cannot do much in this regard.</p>
<p>You&#39;ll also find that some of the ACA/MEP entities do not behave as you&#39;d expect them in case of using RealDWG to open AutoCAD Architecture or MEP drawings.
E.g. the ACA/MEP entities might not call the drawing primitives of your AcGi implementation when you make them draw themselves.</p>
<p>If side databases are opened with SH_DENYNO, they are fully loaded and the relationship graph will be complete. </p>
<p>If they are loaded any other way, you need to call AecAppDbx::drawingPromoterAndIniter() or AcDbDatabase::closeInput() on it. That forces all objects to be pulled from the disk.</p>
<p>This is not ideal, but it is the only way that AutoCAD Architecture can guarantee that the relationship graph is complete. This function always needs to be called to make sure that the necessary promotion and cleanup takes place.</p>
<p>So, the solution is to either open the side database with SH_DENYNO, or call AecAppDbx::drawingPromoterAndIniter() or AcDbDatabase::closeInput() on the opened database.</p>
