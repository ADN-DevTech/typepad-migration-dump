---
layout: "post"
title: "Storing 64-bit integer in Result Buffer"
date: "2012-10-15 02:48:38"
author: "Marat Mirgaleev"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Marat Mirgaleev"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/storing-64-bit-integer-in-result-buffer.html "
typepad_basename: "storing-64-bit-integer-in-result-buffer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><b>Issue</b></p>  <p><em>I cannot find a DXF code to store a 64-bit integer in the Extended Data. Is there any?</em></p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>No, there is no DXF code to indicate a 64-bit value in Extended Data (XData). It is by design, because earlier AutoCAD versions wouldn't be able to read such a DWG file.</p>  <p>One workaround for this is to split your 64-bit value into two 32-bit values (high 32-bits and low 32-bits) and precede them with a code of your choosing to indicate to your application that they should be combined to 64-bit values.</p>  <p>Another workaround is using XRecords instead of XData, since there is support of kDxfInt64 in XRecords. When saving the XRecords back to 2007 and earlier formats, the 64-bit XRecord opcodes are converted into round-trip XData that is attached to the XRecord objects.</p>
