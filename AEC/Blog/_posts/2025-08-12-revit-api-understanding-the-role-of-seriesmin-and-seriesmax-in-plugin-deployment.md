---
layout: "post"
title: "Revit API: Understanding the Role of SeriesMin and SeriesMax in Plugin Deployment"
date: "2025-08-12 07:42:16"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2025/08/revit-api-understanding-the-role-of-seriesmin-and-seriesmax-in-plugin-deployment.html "
typepad_basename: "revit-api-understanding-the-role-of-seriesmin-and-seriesmax-in-plugin-deployment"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" rel="noopener" target="_blank">Naveen Kumar</a></p>
<p>If you’re developing a Revit plugin with the goal of ensuring compatibility across multiple Revit versions (e.g., 2023 through 2026), you’ve likely come across the <strong>SeriesMin</strong> and <strong>SeriesMax</strong> attributes in the <em>PackageContents.xml</em> file.</p>
<p>This article explains what these attributes are, why they are important, and the key limitations to be aware of when distributing your plugin.</p>
<h3 class="section-heading">What Is the SeriesMax Attribute?</h3>
<p>In a Revit plugin package, the <code>PackageContents.xml</code> file defines the plugin&#39;s metadata and how it integrates with Revit. Inside this file, the <code>&lt;RuntimeRequirements&gt;</code> element uses the <code>SeriesMin</code> and <code>SeriesMax</code> attributes to specify which Revit versions the plugin is intended to run on.</p>
<p>Example:</p>
<pre class="prettyprint lang-xml">&lt;RuntimeRequirements OS=&quot;Win64&quot; Platform=&quot;Revit&quot; SeriesMin=&quot;R2023&quot; SeriesMax=&quot;R2025&quot; /&gt;
</pre>
<ul>
<li>SeriesMin=&quot;R2023&quot;: Minimum supported Revit version.</li>
<li>SeriesMax=&quot;R2025&quot;: Maximum supported Revit version (inclusive? Not exactly—more on this below).</li>
</ul>
<h3 class="section-heading">What Actually Happens in Revit?</h3>
<p>Here&#39;s the tricky part: contrary to common assumptions, <strong>Revit doesn&#39;t use SeriesMax</strong> the way you might expect.</p>
<ul>
<li>Revit only checks the SeriesMin value.</li>
<li>If <em data-end="65" data-start="54">SeriesMin</em> matches the current Revit version (e.g., R2023), the plugin will load in that version.</li>
<li>If&#0160;<em data-end="65" data-start="54">SeriesMin </em>doesn’t match, the plugin won’t load in other versions(e.g., R2024,R2025,R2026), even if it’s compatible.</li>
<li>The SeriesMax attribute is effectively <strong>ignored by Revit</strong>.</li>
</ul>
<p>This is unlike other Autodesk products like Inventor, which support broader compatibility in a single XML entry without needing multiple version-specific elements.</p>
<h3 class="section-heading">What Works: One Entry Per Version</h3>
<p>To ensure your plugin loads across versions like Revit 2023, 2024, 2025 and 2026, you must create <strong>separate </strong>&lt;ComponentEntry&gt; and &lt;RuntimeRequirements&gt; elements for each Revit version, even if your plugin binary is the same.</p>
<p>Example of a working (but repetitive) PackageContents.xml:</p>
<pre class="prettyprint lang-xml">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;ApplicationPackage&gt;

  &lt;Components&gt;
    &lt;RuntimeRequirements OS=&quot;Win64&quot; Platform=&quot;Revit&quot; SeriesMin=&quot;R2023&quot; /&gt;
    &lt;ComponentEntry AppName=&quot;FileUpgrader&quot; Version=&quot;4.5.0&quot;
        AppType=&quot;ManagedPlugin&quot;
        ModuleName=&quot;./Contents/AddinFileName.addin&quot; /&gt;
  &lt;/Components&gt;

  &lt;Components&gt;
    &lt;RuntimeRequirements OS=&quot;Win64&quot; Platform=&quot;Revit&quot; SeriesMin=&quot;R2025&quot; /&gt;
    &lt;ComponentEntry AppName=&quot;FileUpgrader&quot; Version=&quot;4.5.0&quot;
        AppType=&quot;ManagedPlugin&quot;
        ModuleName=&quot;./Contents/AddinFileName.addin&quot; /&gt;
  &lt;/Components&gt;

&lt;/ApplicationPackage&gt;
</pre>
<p>This might seem redundant, but it is currently the only reliable way to ensure that your plugin loads across multiple Revit versions. With the above <em data-end="205" data-start="184">PackageContents.xml</em>, the plugin will load in both Revit 2023 and Revit 2025.</p>
<h3 class="section-heading">What Doesn&#39;t Work: Omitting SeriesMax in a Single Entry</h3>
<p>Some developers try to simplify the XML by removing SeriesMax and using just one ComponentEntry, hoping Revit will load it across all versions.</p>
<p>Unfortunately, this approach fails:</p>
<pre class="prettyprint lang-xml">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;ApplicationPackage&gt;
  &lt;Components&gt;
    &lt;RuntimeRequirements OS=&quot;Win64&quot; Platform=&quot;Revit&quot; SeriesMin=&quot;R2023&quot; /&gt;
    &lt;ComponentEntry AppName=&quot;FileUpgrader&quot; Version=&quot;4.5.0&quot;
        AppType=&quot;ManagedPlugin&quot;
        ModuleName=&quot;./Contents/AddinFileName.addin&quot; /&gt;
  &lt;/Components&gt;
&lt;/ApplicationPackage&gt;
</pre>
<p>This file <strong>only works in Revit 2023</strong>. Revit 2024 and 2025 will skip it because SeriesMin doesn’t match.</p>
<h3 class="section-heading">What About Using .NET Framework Across Versions?</h3>
<p>Plugins built with older Revit DLLs (for example, 2023) targeting the .NET Framework may still run on newer Revit versions such as 2024, 2025, and 2026—provided the Revit API method signatures remain unchanged between these versions.</p>
<p>That said, Autodesk recommends recompiling your plugin with the Revit API assemblies for the specific version you’re targeting—especially if you plan to share it publicly—to ensure long-term compatibility.</p>
<p>Also, while .NET Framework plugins might load in Revit versions based on .NET 8.0+ (such as Revit 2025), it’s not a best practice. For better stability and support, migrate and recompile your plugin using the latest .NET Core 8.0 SDKs.</p>
<h3 class="section-heading">Takeaway: Best Practice for Revit PackageContents.xml file</h3>
<ul>
<li>Add a separate ComponentEntry and RuntimeRequirements elements for each Revit version.</li>
<li>Match the SeriesMin value <strong>exactly</strong> to the Revit version you want to target.</li>
<li>Don’t rely on SeriesMax—Revit doesn&#39;t respect it.</li>
</ul>
<h3 class="section-heading">Final Thoughts</h3>
<p>Although repetitive, version-specific elements in <em data-end="109" data-start="88">PackageContents.xml</em> remain the most reliable way to register plugins across multiple Revit versions. Until Revit supports a more flexible system, maintain these elements and recompile as needed.</p>
