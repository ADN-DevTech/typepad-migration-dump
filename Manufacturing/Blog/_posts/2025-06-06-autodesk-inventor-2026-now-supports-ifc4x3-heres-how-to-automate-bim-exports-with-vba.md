---
layout: "post"
title: "Autodesk Inventor 2026 API Now Supports IFC4x3: Here’s How to Automate BIM Exports with VBA"
date: "2025-06-06 10:57:52"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/06/autodesk-inventor-2026-now-supports-ifc4x3-heres-how-to-automate-bim-exports-with-vba.html "
typepad_basename: "autodesk-inventor-2026-now-supports-ifc4x3-heres-how-to-automate-bim-exports-with-vba"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>In the world of Building Information Modeling (BIM), interoperability is no longer a nice-to-have—it’s essential. With growing contractual requirements and cross-platform collaboration across tools like <strong>Revit</strong>, <strong>Navisworks</strong>, and <strong>Tekla</strong>, users of <strong>Autodesk Inventor</strong> have long requested better support for <strong>IFC</strong> (Industry Foundation Classes) exports.</p>
<h3>💡 The Problem with Legacy IFC Exports</h3>
<p>Until recently, Inventor users faced a major limitation when exporting assemblies to IFC:</p>
<ul>
<li>Entire assemblies were flattened into a single solid lump, losing structure and metadata.</li>
<li>No option to select IFC4 or newer versions during export.</li>
<li>External workarounds like BIM-Dex were used, often with limited results.</li>
</ul>
<h3>📣 The Big News: Autodesk Listened</h3>
<p>After years of feedback and feature requests from the community, Autodesk officially implemented full <strong>IFC4/IFC4x3</strong> export support in <strong>Inventor 2026</strong>.</p>
<h4>✅ Key Enhancements in Inventor 2026:</h4>
<ul>
<li>Support for <strong>IFC2x3</strong> and <strong>IFC4x3</strong> formats.</li>
<li>Ability to preserve the full assembly structure in the IFC file.</li>
<li>Seamless interoperability with Revit, Tekla, and other BIM platforms.</li>
<li>Works with the <strong>Revit Core Engine (RCE)</strong> for accurate geometry translation.</li>
</ul>
<h3>🧠 Automate IFC Exports Using VBA</h3>
<p>You can now automate IFC exports using the Inventor API and a simple VBA macro. Below is a sample script that demonstrates how to export your active assembly to IFC4x3 format:</p>
<pre><code>&#39; Ensure BIM Content add-in is loaded
Sub ExportToIFCFormatSample()
    Dim oBIMContent As ApplicationAddIn
    Set oBIMContent = ThisApplication.ApplicationAddIns.ItemById(&quot;{842004D5-C360-43A8-A00D-D7EB72DAAB69}&quot;)
    If Not oBIMContent.Activated Then
        oBIMContent.Activate
    End If

    &#39; Get active assembly
    Dim oDoc As AssemblyDocument
    Set oDoc = ThisApplication.ActiveDocument

    &#39; Access BIM component
    Dim oCompDef As AssemblyComponentDefinition
    Set oCompDef = oDoc.ComponentDefinition
    Dim oBIMComp As BIMComponent
    Set oBIMComp = oCompDef.BIMComponent

    &#39; Set IFC options
    Dim oOptions As NameValueMap
    Set oOptions = ThisApplication.TransientObjects.CreateNameValueMap
    oOptions.Value(&quot;IFCFileVersion&quot;) = &quot;IFC4x3&quot; &#39; Options: &quot;IFC2x3&quot; or &quot;IFC4x3&quot;

    &#39; Export to IFC
    oBIMComp.ExportBuildingComponentWithOptions &quot;C:\Temp\MyIFC.ifc&quot;, oOptions
End Sub
</code></pre>
<p><strong>💡 Note:</strong> The Revit Core Engine must be installed to enable this export functionality.</p>
<h3>📁 Use Case: Export Inventor Steel Structure to BIM</h3>
<p>This update is particularly beneficial for design workflows involving:</p>
<ul>
<li>Structural steel and piping assemblies for Revit or Tekla.</li>
<li>Facility management models requiring rich metadata and object hierarchy.</li>
<li>BIM coordination meetings where file fidelity matters.</li>
</ul>
<hr />
<h3>📌 Summary</h3>
<p>With Inventor 2026, Autodesk has finally delivered on years of community feedback by adding native support for IFC4 and IFC4x3 exports, complete with assembly structure. This is a major win for manufacturers, designers, and BIM professionals working in a connected design ecosystem.</p>
<ul>
<li>✅ Native support</li>
<li>✅ Full structure retention</li>
<li>✅ VBA-compatible automation</li>
</ul>
<hr />
<h3>🔗 Resources:</h3>
<ul>
<li><a href="https://help.autodesk.com/view/INVNTOR/2026/ENU/?guid=WhatsNew">Inventor 2026 – What’s New</a></li>
<li><a href="https://technical.buildingsmart.org/standards/ifc/ifc-schema-specifications/">IFC4x3 Schema Overview</a></li>
</ul>
