---
layout: "post"
title: "Accessing Inventor Apprentice Server with PowerShell"
date: "2025-06-03 12:35:22"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/06/accessing-inventor-apprentice-server-with-powershell.html "
typepad_basename: "accessing-inventor-apprentice-server-with-powershell"
typepad_status: "Publish"
---

<p>by&#0160;<a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<h3>📌 Issue</h3>
<p>Many developers and IT professionals seek lightweight ways to extract metadata from Autodesk Inventor files without launching the full Inventor application. A common scenario involves retrieving iProperties from part files using scripting languages such as PowerShell.</p>
<h3>✅ Solution</h3>
<p>Autodesk <strong>Inventor Apprentice Server</strong> provides a powerful COM interface that allows you to read Inventor file data—including iProperties—without opening the Inventor UI. This is ideal for batch processing, automation scripts, and headless environments.</p>
<p>Below is a <strong>PowerShell script example</strong> demonstrating how to use Apprentice Server to open a <code>.ipt</code> part file and display its <strong>Part Number</strong> iProperty.</p>
<h3>💻 PowerShell Script: Read Part Number from IPT File</h3>
<pre><code># Create the ApprenticeServer object
$apprentice = New-Object -ComObject Inventor.ApprenticeServer

# Open the specified Inventor part file
$apprenticeDoc = $apprentice.Open(&quot;C:\Temp\Part1.ipt&quot;)

# Retrieve and print the Part Number iProperty
Write-Host $apprenticeDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;).Item(&quot;Part Number&quot;).Value

# Clean up the COM object
[System.Runtime.Interopservices.Marshal]::ReleaseComObject($apprentice) | Out-Null
</code></pre>
<h3>🔍 Explanation</h3>
<ul>
<li><code>New-Object -ComObject Inventor.ApprenticeServer</code>: Creates an instance of the Apprentice Server.</li>
<li><code>.Open(&quot;C:\Temp\Part1.ipt&quot;)</code>: Opens the specified IPT file for read access.</li>
<li><code>.PropertySets.Item(&quot;Design Tracking Properties&quot;).Item(&quot;Part Number&quot;).Value</code>: Accesses the <strong>Part Number</strong> iProperty.</li>
<li><code>ReleaseComObject</code>: Properly releases the COM object to avoid memory leaks.</li>
</ul>
<h3>🛠️ Products and Versions Supported</h3>
<p>This approach applies to the following versions of Autodesk Inventor where <strong>Apprentice Server</strong> is available:</p>
<ul>
<li>Inventor 2021</li>
<li>Inventor 2022</li>
<li>Inventor 2023</li>
<li>Inventor 2024</li>
<li>Inventor 2025</li>
<li>Inventor 2026 <em>(Note: Requires separate installation of Apprentice Server).&#0160;</em></li>
</ul>
<h3>⚠️ Notes</h3>
<ul>
<li>In <strong>Inventor 2026</strong>, Apprentice Server is no longer registered by default and must be installed separately. Refer <a href="https://adndevblog.typepad.com/manufacturing/2025/06/inventor-apprentice-server-enhancement-in-inventor-2026.html">this blog</a> for more details on Inventor Apprentice Server Enhancement in Inventor 2026&#0160;</li>
<li>Running PowerShell as Administrator may be required depending on system policies.</li>
</ul>
<h3>📘 Additional Resources</h3>
<ul>
<li><a href="https://help.autodesk.com/" rel="noopener" target="_blank">Inventor API Help Documentation</a></li>
<li><a href="https://help.autodesk.com/view/INVNTOR/latest/ENU/" rel="noopener" target="_blank">What’s New in Inventor 2026</a></li>
<li><a href="https://knowledge.autodesk.com/" rel="noopener" target="_blank">Apprentice Server Standalone Installer for Inventor 2026</a></li>
<li><a href="https://forums.autodesk.com/t5/inventor-customization/bd-p/120" rel="noopener" target="_blank">Autodesk Inventor Customization Forum</a></li>
</ul>
