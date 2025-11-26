---
layout: "post"
title: "Using LISP with AutoCAD Architecture and MEP"
date: "2016-04-13 14:17:23"
author: "Mikako Harada"
categories:
  - "AutoCAD Architecture"
  - "AutoCAD MEP"
original_url: "https://adndevblog.typepad.com/aec/2016/04/using-lisp-with-autocad-architecture-and-mep.html "
typepad_basename: "using-lisp-with-autocad-architecture-and-mep"
typepad_status: "Publish"
---

<p>Occasionally, we still seem to get questions about using LISP with AutoCAD Architecture (ACA) and MEP (AME). ACA and AME dropped DXF/LISP support since ADT 2004. <a href="http://adndevblog.typepad.com/aec/2012/07/accessing-and-manipulating-dxf-codes-of-autocad-architecture-and-mep-objects.html" title="LISP and ACA/AME">This blog post</a>&#0160;gives you a little bit of history and reasoning behind the decision.&#0160;</p>
<p>As new people joins, they sometimes assume whatever AutoCAD supports will be automatically extended to verticals. As&#0160;it&#39;s been a while since we had this discussion, I&#39;m bringing up this topic for the sake of people who are new to AutoCAD community. &#0160;&#0160;</p>
<p>The hard fact is that LISP&#0160;uses DXF code to function, and ACA/MEP do not implement DXF. If you try to use LISP to access AEC objects, it&#0160;simply does not return values. (Please see <a href="http://adndevblog.typepad.com/aec/2012/07/accessing-and-manipulating-dxf-codes-of-autocad-architecture-and-mep-objects.html" title="DXF code and ACA/AME">the earlier post</a>&#0160;for details.)&#0160;</p>
<p>You can still use LISP as a vanilla AutoCAD.&#0160;While AutoCAD is not adding any enhancement to LISP, the functionality it has already should remain. Same for VBA; ACA and AME do not support VBA/ActiveX. You can use it only to the level that access non-AEC objects. &#0160;</p>
<p>If you are trying to customize&#0160;ACA and AME, we recomend .NET API.</p>
<p>Mikako</p>
<p>&#0160;</p>
