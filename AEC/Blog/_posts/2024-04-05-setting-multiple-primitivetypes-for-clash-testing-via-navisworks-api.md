---
layout: "post"
title: "Setting multiple PrimitiveTypes for Clash Testing via Navisworks API"
date: "2024-04-05 03:22:29"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2024/04/setting-multiple-primitivetypes-for-clash-testing-via-navisworks-api.html "
typepad_basename: "setting-multiple-primitivetypes-for-clash-testing-via-navisworks-api"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p><span style="font-family: &#39;times new roman&#39;, times;">By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">I&#39;d like to delve into a specific aspect of clash testing: setting multiple PrimitiveTypes via Navisworks API. When dealing with clash testing in Navisworks, the ability to specify PrimitiveTypes for clash detection can significantly enhance the accuracy and relevance of clash results. By defining which geometric primitives (such as triangles, lines, or points) should be considered during clash tests, users can tailor the analysis to suit their specific project requirements.</span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">Here&#39;s a simple code snippet demonstrating how to set multiple PrimitiveTypes for clash testing using Navisworks API:</span></p>
<pre class="prettyprint"><span style="font-size: 10pt; font-family: &#39;times new roman&#39;, times;">Document doc = Autodesk.Navisworks.Api.Application.ActiveDocument;<br />DocumentClash documentClash = doc.GetClash();<br />DocumentClashTests oDCT = documentClash.TestsData;<br />ClashTest t = oDCT.Tests[0] as ClashTest;<br />ClashTest oCopyt = t.CreateCopy() as ClashTest;<br />oCopyt.SelectionA.PrimitiveTypes = PrimitiveTypes.Triangles | PrimitiveTypes.Lines;<br />oCopyt.SelectionB.PrimitiveTypes = PrimitiveTypes.Lines | PrimitiveTypes.Points;<br />oDCT.TestsEditTestFromCopy(t, oCopyt);</span></pre>
<p><span style="font-size: 10pt; font-family: &#39;times new roman&#39;, times;">Before running the sample code:<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab96f0200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Before" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab96f0200c img-responsive" src="/assets/image_35563.jpg" title="Before" /></a><br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afb440200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a><br />After running the sample code:<br /></span><br /><span style="font-family: &#39;times new roman&#39;, times;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afb451200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="After" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3afb451200d img-responsive" src="/assets/image_31897.jpg" title="After" /></a></span><br /><br /><br /><span style="font-family: &#39;times new roman&#39;, times;">If you&#39;re new to the Clash Detection API, my colleague Xiaodong has crafted an extensive guide on leveraging its capabilities. These resources offer valuable insights for optimizing clash detection workflows in Autodesk Navisworks.</span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;">I encourage you to explore the links provided below.</span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;"><a href="https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-1.html">https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-1.html</a></span></p>
<p><span style="font-family: &#39;times new roman&#39;, times;"><a href="https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-2.html">https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-2.html</a><br /><br />In addition, the Navisworks SDK includes helpful samples specifically focusing on clash detection. To explore these samples, go to &#39;<strong>NET\examples\PlugIns\ClashDetective</strong>&#39; in the Navisworks SDK.<br /></span></p>
<p>&#0160;</p>
