---
layout: "post"
title: "Understanding BOM Delegation in Autodesk Inventor API"
date: "2025-06-13 11:24:24"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/06/understanding-bom-delegation-in-autodesk-inventor-api.html "
typepad_basename: "understanding-bom-delegation-in-autodesk-inventor-api"
typepad_status: "Publish"
---

<p>by&nbsp;<a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,&nbsp;&nbsp;</p>
<p>Bill of Materials (BOM) management is a cornerstone of effective digital design and manufacturing workflows. With the advent of <em>Model States</em> in Autodesk Inventor, the management of BOMs—especially across legacy designs and substitutes—has evolved significantly. One powerful yet often misunderstood concept introduced in this context is <strong>BOM Delegation</strong>.</p>
<h3>🔄 What is BOM Delegation?</h3>
<p><strong>BOM Delegation</strong> refers to the behavior where one model state defers its BOM and iProperties to another model state—typically the <strong>Primary</strong> or <strong>Master</strong>. This ensures that legacy Level of Detail (LOD) representations and substitute models maintain BOM consistency after migration to Inventor 2022 or newer.</p>
<h3>BOM Delegation is Triggered When:</h3>
<ul>
<li>The model state is a <strong>migrated non-Master Level of Detail (LOD)</strong></li>
<li>The model state is a <strong>migrated substitute LOD</strong></li>
<li>The model state is a <strong>substitute part file</strong></li>
</ul>
<h3>How It Works:</h3>
<ul>
<li>A <strong>substitute model state</strong> uses the BOM from the source model state.</li>
<li>A <strong>migrated non-Master LOD or substitute</strong> uses the BOM from the <strong>Primary</strong> model state.</li>
</ul>
<p><strong>Tip:</strong><br />You <em>can</em> edit the BOM of a <strong>migrated Master LOD</strong>.<br />You <em>cannot</em> edit the BOM of a <strong>migrated non-Master LOD</strong> directly. To make edits, <strong>copy the model state</strong>, edit the copy, and optionally remove the original.</p>
<h3>🛠️ BOM Delegation in the Inventor API</h3>
<p>To handle BOM delegation programmatically, use the read-only property:</p>
<pre><code>BOMDelegate As ModelState</code></pre>
<p>This returns the model state currently holding the BOM for the given model state.</p>
<h3>Delegation Behavior Summary</h3>
<table border="1" cellpadding="5">
<thead>
<tr>
<th>Model State Type</th>
<th>BOMDelegate Behavior</th>
</tr>
</thead>
<tbody>
<tr>
<td>Legacy LOD</td>
<td>Delegates to Master (Primary)</td>
</tr>
<tr>
<td>Legacy Substitute</td>
<td>Delegates to Master (Primary)</td>
</tr>
<tr>
<td>New Substitute (from model state)</td>
<td>Delegates to source state</td>
</tr>
<tr>
<td>New Substitute (from part/LOD)</td>
<td>Delegates to Master (Primary)</td>
</tr>
<tr>
<td>New Non-substitute</td>
<td>No delegation (owns its BOM)</td>
</tr>
</tbody>
</table>
<h3>💼 Real-World Use Case: Legacy Substitute BOM Access Failure</h3>
<p>A user attempted to access the <code>BOMStructure</code> property in a substituted model state migrated from Inventor 2021. Since the file wasn't saved post-migration, the call failed.</p>
<h3>❌ Problematic Code</h3>
<pre><code>Sub BOMStructureTest()
    Dim oAssemblyDoc As AssemblyDocument
    Set oAssemblyDoc = ThisApplication.ActiveDocument

    Dim oCompDef As AssemblyComponentDefinition
    Set oCompDef = oAssemblyDoc.ComponentDefinition

    Dim eBOMStructure As BOMStructureEnum
    eBOMStructure = oCompDef.BOMStructure ' Fails due to delegation
End Sub
</code></pre>
<h3>✅ Correct Code Using <code>BOMDelegate</code></h3>
<pre><code>Sub BOMStructureTest()
    Dim oAssemblyDoc As AssemblyDocument
    Set oAssemblyDoc = ThisApplication.ActiveDocument

    Dim oCompDef As AssemblyComponentDefinition
    Set oCompDef = oAssemblyDoc.ComponentDefinition

    Dim eBOMStructure As BOMStructureEnum
    eBOMStructure = oCompDef.ModelStates. _
        ActiveModelState.BOMDelegate.Document.ComponentDefinition.BOMStructure
End Sub
</code></pre>
<p>This correctly accesses the BOM from the delegated source model state.</p>
<h3>🧾 Managing BOMs in the Inventor UI</h3>
<p>Navigate to <strong>Assemble → Manage → Bill of Materials</strong> to access:</p>
<ul>
<li><strong>Model Data View</strong> – Browser-like, non-exportable</li>
<li><strong>Structured View</strong> – Exportable hierarchical BOM</li>
<li><strong>Parts Only View</strong> – Flat exportable BOM</li>
</ul>
<h3>UI Capabilities Include:</h3>
<ul>
<li>BOM renumbering and sorting</li>
<li>Add/Edit custom iProperties</li>
<li>Drag and reorder/remove columns</li>
<li>Multi-cell editing, Find/Replace, and Excel paste</li>
<li>QTY = 0 handling for suppressed components</li>
</ul>
<p><strong>To hide suppressed components:</strong> <br />Use <strong>BOM Settings → Hide Suppressed Components in BOM</strong></p>
<h3>📘 Understanding Level of Detail (LOD) Migration</h3>
<p>With Inventor 2022 and beyond, <strong>LODs are deprecated</strong> and replaced by <strong>Model States</strong>.</p>
<p><strong>When you open a legacy assembly:</strong></p>
<ul>
<li>System-defined LODs like <em>All Components Suppressed</em> are <strong>removed</strong></li>
<li>Primary/user-defined LODs and substitutes are <strong>converted to model states</strong></li>
<li>BOM in non-primary migrated states continues to <strong>delegate</strong> to the Primary</li>
</ul>
<p><strong>Tip:</strong> To modify BOM from a delegated state:</p>
<ol>
<li>Copy the model state</li>
<li>Edit the BOM in the new state</li>
<li>Optionally delete the old migrated LOD</li>
</ol>
<p>📄 <strong>Full Autodesk Help documentation:</strong><br /><a href="https://help.autodesk.com/view/INVNTOR/2026/ENU/?guid=GUID-42FE4328-3035-4CC7-BC0D-4012C670F16D" target="_blank" rel="noopener"> About Level of Detail Migration – Autodesk Help</a></p>
<h3>📚 Additional Resource</h3>
<p>To learn more about the enhancements made to the Inventor Apprentice Server in 2026 (including registry-free behavior), refer to this Autodesk Developer Blog post:</p>
<p>👉 <a href="https://adndevblog.typepad.com/manufacturing/2025/06/inventor-apprentice-server-enhancement-in-inventor-2026.html" target="_blank" rel="noopener"> Inventor Apprentice Server Enhancement in Inventor 2026</a></p>
<h3>🧠 Final Thoughts</h3>
<p>Understanding and applying BOM delegation principles—both through the UI and API—ensures your Inventor assemblies remain consistent, editable, and aligned with best practices in modern digital engineering.</p>
