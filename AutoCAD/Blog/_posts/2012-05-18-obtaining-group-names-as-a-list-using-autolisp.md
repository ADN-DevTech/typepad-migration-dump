---
layout: "post"
title: "Obtaining Group names as a list using AutoLISP"
date: "2012-05-18 18:14:03"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/obtaining-group-names-as-a-list-using-autolisp.html "
typepad_basename: "obtaining-group-names-as-a-list-using-autolisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To obtain all the group names in an AutoLISP list variable, first get the "ACAD_GROUP" dictionary and then extract all the group names from it.</p>
<p>Here is a sample code :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(defun GetGroupList ()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (setq grp (dictsearch (namedobjdict) &quot;ACAD_GROUP&quot;))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (setq grpList (list))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (while (/= (assoc 3 grp) nil)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (setq grpList (cons (cdr (assoc 3 grp)) grpList ))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (setq grp (cdr (member (assoc 3 grp) grp)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; grpList</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
