---
layout: "post"
title: "Rebar.CreateFromCurves() Throws Exception With Two Curves "
date: "2013-03-04 23:48:44"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2013/03/rebarcreatefromcurves-throws-exception-with-two-curves-.html "
typepad_basename: "rebarcreatefromcurves-throws-exception-with-two-curves-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I&#39;m using Rebar.CreateCurves() to define a rebar whose curve is composed of two curves: an arc and a straight line.&#0160;As an example, the curve looks like the picture below: </p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c37507eb7970b-pi" style="display: inline;"><img alt="Rebar two curves" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017c37507eb7970b" height="262" src="/assets/image_809344.jpg" title="Rebar two curves" width="204" /></a></p>
<p>However, if I try to pass the curves with two segments in one curve array, it throws an error:</p>
<p>&#0160;&#0160;&#0160; &quot;Unable to create a RebarShape based on the given curves.&quot; </p>
<p>Could you tell what the part is that Revit does not like? </p>
<p><strong>Solution </strong></p>
<p>The basic problem is that you cannot create a Rebar that ends with an arc; if a Rebar shape includes any straight edges, then its first and last curves must be straight lines (see RebarShapeDefinitionBySegments).&#0160; The only exception is a Rebar that is completely made up of arcs (see RebarShapeDefinitionByArc). If you add straight line at the both ends of the arc, for example,&#0160;like the picture below, it should work. </p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8f37396970d-pi" style="display: inline;"><img alt="Rebar three curves" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017ee8f37396970d" height="297" src="/assets/image_416668.jpg" title="Rebar three curves" width="314" /></a></p>
<p>Note:&#0160;if your intention is to&#0160;create 180 degree hook, you should only include the straight line in the curves array, and then specify a RebarHookType as one of the arguments in the call to Rebar.CreateFromCurves().</p>
