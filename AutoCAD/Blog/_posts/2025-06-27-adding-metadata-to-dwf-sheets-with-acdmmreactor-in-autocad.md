---
layout: "post"
title: "Adding Metadata to DWF Sheets with AcDMMReactor in AutoCAD"
date: "2025-06-27 02:42:24"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "DWF"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2025/06/adding-metadata-to-dwf-sheets-with-acdmmreactor-in-autocad.html "
typepad_basename: "adding-metadata-to-dwf-sheets-with-acdmmreactor-in-autocad"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>When publishing to DWF or DWFx in AutoCAD, embedding metadata into sheets and entities can provide powerful downstream benefits, whether for facility management, digital twins, or simply making the file more intelligent. The <code>AcDMMReactor</code> API gives you fine-grained control over this.</p>
<p><em><span style="font-size: medium;">[Keywords]= AcDMMEPlotPropertyVec,AcDMMEPlotProperty,AcDMMSheetReactorInfo,AcDMMEPlotProperties,AcGlobAddDMMReactor</span></em>&#0160;</p>
<h3>What Is AcDMMReactor?</h3>
<p><code>AcDMMReactor</code> is part of the Autodesk DWF Publishing framework that lets you hook into the DWF publishing pipeline. You can inject custom properties into sheets (<code>OnBeginSheet</code>) or even individual drawing entities (<code>OnBeginEntity</code>).</p>
<h3>What You Can Do</h3>
<p>With this reactor, you can:</p>
<ul>
<li>Add <strong>custom properties</strong> like Layer Name, Block Name, or Area to entities.</li>
<li>Assign <strong>unique IDs</strong> to each entity for traceability.</li>
<li>Attach <strong>external resources</strong> (e.g., help documents) to sheets.</li>
<li>Dynamically associate metadata with drawing nodes for richer DWF output.</li>
</ul>
<h3>Sample Capabilities</h3>
<p>Here’s a glimpse of what’s possible inside <code>OnBeginEntity</code>:</p>
<pre class="prettyprint lang-cpp"><code>
AcDMMEPlotProperty prop(L&quot;Layer&quot;, layerName);
prop.SetCategory(L&quot;Display&quot;);
props.AddProperty(&amp;prop);
</code></pre>
<p>Want to include area? Use entity extents:</p>
<pre class="prettyprint lang-cpp"><code>
double area = (ext.maxPoint().x - ext.minPoint().x) * 
              (ext.maxPoint().y - ext.minPoint().y);
AcDMMEPlotProperty areaProp(L&quot;ApproxArea&quot;, std::to_wstring(area).c_str());
</code></pre>
<p>And here’s how to add properties to the sheet itself:</p>
<pre class="prettyprint lang-cpp"><code>
AcDMMEPlotProperty prop(_T(&quot;LayoutId&quot;), pInfo-&gt;UniqueLayoutId());
prop.SetCategory(_T(&quot;APS&quot;));
pInfo-&gt;AddPageProperties({ prop });
</code></pre>
<h3>Registering Your Reactor</h3>
<p>You’ll need to dynamically load the DWF publishing module and register your reactor:</p>
<pre class="prettyprint lang-cpp"><code>
if (!acrxServiceIsRegistered(&quot;AcEPlotX&quot;))
    acrxLoadModule(&quot;AcEPlotX.crx&quot;, false, false);
</code></pre>
<p>Then register your custom <code>AcDMMReactor</code>:</p>
<pre class="prettyprint lang-cpp"><code>
ADD_DMM_REACTOR pAdd = (ADD_DMM_REACTOR)GetProcAddress(hDmm, &quot;AcGlobAddDMMReactor&quot;);
pAdd(new TstMMReactor());
</code></pre>
<p>Here is Github Source <a href="https://github.com/MadhukarMoogala/inject-dwf-metadata">inject-dwf-metadata</a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d6f1ba200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DWFMetadtaProps" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d6f1ba200c img-responsive" src="/assets/image_369346.jpg" title="DWFMetadtaProps" /></a></p>
