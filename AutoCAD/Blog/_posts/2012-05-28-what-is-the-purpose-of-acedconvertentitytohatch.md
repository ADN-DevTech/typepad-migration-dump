---
layout: "post"
title: "What is the purpose of acedConvertEntityToHatch?"
date: "2012-05-28 07:25:20"
author: "Philippe Leefsma"
categories:
  - "2013"
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/what-is-the-purpose-of-acedconvertentitytohatch.html "
typepad_basename: "what-is-the-purpose-of-acedconvertentitytohatch"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>&#160;</p>  <p>Here is a common mistake programmers can do when using that API:</p>  <p><em>I am trying to use acedConvertEntityToHatch to create a hatch directly from some lines. Is there additional documentation describing how to use this function?</em></p>  <p>&#160;</p>  <p>This function is for converting old hatches (R13) that were anonymous block (*U and *X) references. The block reference must be database resident and have &quot;ACAD&quot; Xdata containing the old &quot;HATCH&quot; Xdata. A return value of&#160; eNotThatKindOfClass usually indicates a problem processing the Xdata.</p>  <p>Refer to the documentation in the ObjectARX Reference for acedConvertEntityToHatch:    <br />pHatch must be a newly created, and open for write but not added to the database yet. pEnt must be an AcDbBlockReference or AcDbSolid, otherwise you should get the &quot;eIIllegalEntityType&quot; error status.</p>
