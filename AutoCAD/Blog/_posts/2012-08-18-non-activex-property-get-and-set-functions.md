---
layout: "post"
title: "Non-ActiveX property get and set functions"
date: "2012-08-18 06:11:15"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/non-activex-property-get-and-set-functions.html "
typepad_basename: "non-activex-property-get-and-set-functions"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>These were introduced in AutoCAD 2012 and you can find explanations in the <a href="http://docs.autodesk.com/ACDMAC/2012/ENU/filesALRMac/GUID-ED406069-97F9-4552-BBEF-9A02D06E6C1-203.htm">AutoCAD documentation</a> as well as in <a href="http://hyperpics.blogs.com/beyond_the_ui/2011/08/new-autolisp-functions-in-autocad-2012-for-mac.html">Lee' blog</a> and <a href="http://blog.jtbworld.com/2011/12/how-to-write-autolisp-that-works-on.html">Jimmy's blog</a>. Recently, I was asked by a developer to provide an&nbsp;explanation for the optional parameter marked as "[or collectionName index name val]" in the documentation.</p>
<p>To understand this parameter, you may try using the “dumpallproperties” on a polyline. The vertices of a polyline is a collection and each vertex has a set of properties that can be retrieved / set. In this context, the optional parameters are useful.</p>
<p>Try this in the AutoCAD command line :</p>
<p>&nbsp;(command "_pline" "0,0" "3,3" "5,2" "")</p>
<p>(setq e1 (entlast))</p>
<p>(dumpallproperties e1)</p>
<p>From what gets printed in the AutoCAD command window, we now have the name of the collection and the property names of each item in the collection. These can be used to either set / get the property as follows :</p>
<p>; Modify the EndWidth of a Vertex to 1.0</p>
<p>(setpropertyvalue e1 "Vertices" 0 "EndWidth" 1.0)</p>
<p>; Get the "EndWidth" of the first vertex.</p>
<p>(getpropertyvalue e1 "Vertices" 0 "EndWidth")</p>
<p>; Get the "EndWidth" of&nbsp;the second vertex.&nbsp;</p>
<p>(getpropertyvalue e1 "Vertices" 1 "EndWidth")</p>
<p>; Get the X coordinate of the third&nbsp;vertex.</p>
<p>(getpropertyvalue e1 "Vertices" 2 "Point/X")</p>
<p>; Get the&nbsp;Y coordinate of the third vertex.</p>
<p>(getpropertyvalue e1 "Vertices" 2 "Point/Y")</p>
<p>&nbsp;</p>
