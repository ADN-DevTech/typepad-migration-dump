---
layout: "post"
title: "get current UCS in a DBX enabler"
date: "2013-03-06 01:12:00"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2013/03/get-current-ucs-in-a-dbx-enabler.html "
typepad_basename: "get-current-ucs-in-a-dbx-enabler"
typepad_status: "Publish"
---

<p><strong>Issue     <br /></strong>How do I get the current UCS from within an ObjectDBX enabler and don&#39;t have access to acedGetCurrentUcs()?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>The global function acdbUcsMatrix() returns a matrix identical to acedGetCurrentUcs(). It can normally be used in place of acedGetCurrentUcs() and is available in ObjectDBX.</p>
<p>There are some posts which have lines of acdbUcsMatrix, e.g.</p>
<h5><a href="http://adndevblog.typepad.com/autocad/2013/01/set-oblique-angle-of-aligned-dimensions-to-a-certain-angle-relative-to-an-axis-in-the-current-ucs.html">Set oblique angle of aligned dimensions to a certain angle relative to an axis in the current UC</a></h5>
<p><a href="http://adndevblog.typepad.com/autocad/2012/09/ordinate-dimension-text-is-incorrect-in-rotated-ucs.html">Ordinate Dimension Text is incorrect in rotated UCS</a></p>
