---
layout: "post"
title: "Export driven joint values"
date: "2021-03-19 09:46:32"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2021/03/export-driven-joint-values.html "
typepad_basename: "export-driven-joint-values"
typepad_status: "Publish"
---

<p>In certain workflows, it can come handy to get the values of joints at multiple states of the model.<br />This way you can use those values downstream to drive (or at least help set up) a real-life model.</p>
<p>In the user interface, you can drag components to drive the movement of the model, or use e.g. <a href="https://help.autodesk.com/view/fusion360/ENU/courses/AP-SOLIDWORKS-JOINTS-MOTION-STUDIES">Motion Study</a></p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340278801cc2a6200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="2021-03-18_17-50-47 (3)" class="asset  asset-image at-xid-6a00e553fcbfc688340278801cc2a6200d img-responsive" src="/assets/image_464325.jpg" title="2021-03-18_17-50-47 (3)" /></a></p>
<p>We can do the same using the <strong>API</strong> as well. As we keep driving the model, we can check how the change affects the various <strong>joints</strong> and record their values.<br />Then we can save the values in a <a href="https://en.wikipedia.org/wiki/Comma-separated_values">CSV</a> file which then could be opened in <a href="https://en.wikipedia.org/wiki/Microsoft_Excel">Excel</a> or a similar spreadsheet editor.</p>
<p>The code is made for this specific model but could easily be modified for others. It expects a <strong>list of components</strong> and a <strong>list of joints</strong> inside them that you want to monitor. In the case of both lists, you can specify the <strong>name</strong> that should be used in the <strong>CSV</strong> file.</p>
<p>After specifying where you want the <strong>CSV</strong> file to be saved, you can also provide the <strong>distance</strong> the main joint should be moved.&#0160;</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026bdec5009e200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Params" class="asset  asset-image at-xid-6a00e553fcbfc68834026bdec5009e200c img-responsive" src="/assets/image_388557.jpg" title="Params" /></a></p>
<p>The source code is here: <br /><a href="https://github.com/adamenagy/JointValueExporter/blob/main/JointValueExporter.py">https://github.com/adamenagy/JointValueExporter/blob/main/JointValueExporter.py</a>&#0160;</p>
<p>-Adam</p>
