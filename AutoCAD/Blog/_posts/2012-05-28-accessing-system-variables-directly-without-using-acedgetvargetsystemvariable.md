---
layout: "post"
title: "Accessing system variables directly without using acedGetVar()/GetSystemVariable()"
date: "2012-05-28 04:17:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/accessing-system-variables-directly-without-using-acedgetvargetsystemvariable.html "
typepad_basename: "accessing-system-variables-directly-without-using-acedgetvargetsystemvariable"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I just realized that variables like EXTMIN, EXTMAX, etc. can also be accessed directly through AcDbDatabase, so I do not need to use acedGetVar()</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>Yes, that is correct.</p>
<p>acedGetVar() [aced = AutoCAD Editor] and Application.GetSystemVariable() enables you to access system variables that are also available for users through the AutoCAD Editor in the User Interface.</p>
<p>However, non-AutoCAD dependent system variables like EXTMIN, EXTMAX, UCSXDIR, etc. can also be found directly under AcDbDatabase/Database. For a complete list have a look at the AcDbDatabse methods or Database properties in the ObjectARX/.NET Reference Guide [arxdoc.chm].</p>
