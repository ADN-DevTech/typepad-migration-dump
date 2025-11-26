---
layout: "post"
title: "Workaround for MRenderItem::setMatrix may not working with transform matrix in Maya 2018 Update 2"
date: "2018-01-09 21:36:49"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2018/01/workaround-for-mrenderitemsetmatrix-may-not-working-with-transform-matrix-in-maya-2018-update-2.html "
typepad_basename: "workaround-for-mrenderitemsetmatrix-may-not-working-with-transform-matrix-in-maya-2018-update-2"
typepad_status: "Publish"
---

<p>I&#39;ve received a report of&#0160;MRenderItem::setMatrix is not working with transform matrix recently.&#0160; After doing some test, it seems to be only occurring with Maya 2018 Update 2 using OpenGL mode.<br /> <br /> Our engineers found it is an issue in code and will fix it in the future releases. Some tests in VP2 are being done in the wrong order, resulting in stale matrix data being used to render the item. We can mitigate this issue by manually querying the MDrawContext world matrix before calling MRenderItem::draw(), that will cause the stale data to be properly purged.<br /> <br /> e.g.</p>
<pre id="UHEACATBjKF">//Workaround<br />auto mat = context.getMatrix(MFrameContext::kWorldMtx, &amp;status);<br /><br /> self.drawRenderItem(context, *renderItem);</pre>
