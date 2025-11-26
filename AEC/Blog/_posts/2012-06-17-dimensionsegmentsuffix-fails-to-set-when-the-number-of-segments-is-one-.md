---
layout: "post"
title: "DimensionSegment.Suffix fails to set when the number of segments is one  "
date: "2012-06-17 12:08:11"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/dimensionsegmentsuffix-fails-to-set-when-the-number-of-segments-is-one-.html "
typepad_basename: "dimensionsegmentsuffix-fails-to-set-when-the-number-of-segments-is-one-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I&#39;m trying to add suffix to a dimension through the DimensionSegment.Suffix property. When&#0160;the dimension has multiple segments, it works. However, when the dimension is composed of a single segment, it fails. Is this a bug?</p>
<p><strong>Solution</strong></p>
<p>There is another property Suffix at the Dimension level (as opposed to DimensionSegment level). When there is only one segment in the given dimension, you will need to use Dimension level Suffix property. This is as designed (although one might question about design...).</p>
