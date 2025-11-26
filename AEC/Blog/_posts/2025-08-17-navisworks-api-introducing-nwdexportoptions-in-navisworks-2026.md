---
layout: "post"
title: "Navisworks API: Introducing NwdExportOptions in Navisworks 2026"
date: "2025-08-17 03:20:33"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2025/08/navisworks-api-introducing-nwdexportoptions-in-navisworks-2026.html "
typepad_basename: "navisworks-api-introducing-nwdexportoptions-in-navisworks-2026"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" rel="noopener" target="_blank">Naveen Kumar</a></p>
<p>With the release of Navisworks 2026, new and improved NWD export options have been introduced. One of the key enhancements is the ability to create an NWD file that includes only the visible items in a model or only the visible models in a federated file. This reduces file size and ensures that shared models remain clear, focused, and relevant for the intended audience.</p>
<h3 class="section-heading">How to Export NWD</h3>
<p>You can export an NWD file in two ways:</p>
<ul>
<li><strong>Output tab</strong> → <strong>Export Scene</strong> → <strong>Export NWD</strong></li>
<li><strong>Application</strong> → <strong>Export</strong> → <strong>Export NWD</strong></li>
</ul>
<h3 class="section-heading">Export Options Explained</h3>
<p>When exporting, Navisworks 2026 gives you configuration options:</p>
<ul>
<li><strong>Exclude hidden items</strong>
<ul>
<li><strong>Checked:</strong> Removes hidden objects completely from the exported NWD on publish.</li>
<li><strong>Unchecked:</strong> Hidden objects remain in the file but are displayed as hidden in the selection tree.</li>
<li>Useful when you want to share a clean, simplified view.</li>
</ul>
</li>
<li><strong>Embed ReCap and texture data</strong>
<ul>
<li><strong>Checked:</strong> Embeds externally referenced files, including textures and ReCap scans, directly into the NWD on publish.</li>
<li>Enables password protection for referenced files.</li>
<li>Any textures applied to the published file are saved in a folder with the same name as the Published file.</li>
<li>You can control ReCap file embedding via <strong>Options Editor</strong>(File Readers → ReCap page)</li>
</ul>
</li>
<li><strong>Prevent object property export</strong>
<ul>
<li><strong>Checked:</strong> Excludes object properties that come from native CAD packages in the published file.</li>
<li>Protects intellectual property and ensures sensitive design details are not shared.</li>
</ul>
</li>
<li><strong>Navisworks Version</strong>
<ul>
<li>Allows you to select the file format version you want to save in (e.g., Navisworks 2026).</li>
</ul>
</li>
</ul>
<h3 class="section-heading">Automating with the API</h3>
<p>You can also automate the export process using the Navisworks API. You can use either TryExportToNwd() or ExportToNwd(). <br /><br />Here’s a C# example:</p>
<pre class="prettyprint lang-cs">        Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;

        NwdExportOptions options = new NwdExportOptions
        {
            ExcludeHiddenItems = true,  
            PreventObjectPropertyExport = true,
            FileVersion = (int)DocumentFileVersion.Navisworks2026,
            EmbedXrefs = true
        };

        document.TryExportToNwd(@"C:\Exports\ProjectOutput.nwd", options);
      </pre>
