---
layout: "post"
title: "Newly inserted block does not show in AutoCAD Mechanical"
date: "2014-12-23 11:37:49"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/12/newly-inserted-block-does-not-show-in-autocad-mechanical.html "
typepad_basename: "newly-inserted-block-does-not-show-in-autocad-mechanical"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p><strong>Issue:</strong> I have two commands inserting a block inside a transaction: the first inserts the block directly, the second does it through jigging. When run from a third command that combines the two then the first block does not appear until the second one&#39;s jig is finished when inside <strong>AutoCAD Mechanical</strong>. Inside plain <strong>AutoCAD</strong> it shows up fine.</p>
<p>I figured out that <strong>AutoCAD Mechanical</strong> wraps my command in a transaction. If I commit that then the first block appears fine, but it might cause other issues, so I&#39;d rather avoid doing that.</p>
<p><strong>Solution:</strong>&#0160;You can force a transaction to flush graphics to the UI:&#0160;<a href="http://adndevblog.typepad.com/autocad/2013/01/force-autocad-to-update-the-graphics-display-area.html" target="_self" title="">http://adndevblog.typepad.com/autocad/2013/01/force-autocad-to-update-the-graphics-display-area.html</a></p>
<p>In our case adding a <strong>TransactionManager.QueueForGraphicsFlush()</strong>&#0160;seems to be enough:</p>
<pre>using (var transaction = document.TransactionManager.StartTransaction())
{
  // Code of first command adding the block 
<br />  // Flush the graphics of the block to the UI
  <strong>transaction.TransactionManager.QueueForGraphicsFlush();</strong>

  transaction.Commit();
}</pre>
<p>You can also find information about this topic in &quot;Autodesk ObjectARX for AutoCAD 2015: Developer Guide &gt; Advanced Topics &gt; Advanced Topics &gt; Transaction Management&quot;</p>
