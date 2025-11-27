---
layout: "post"
title: "Introducing new API Support for the Simplify Feature in Autodesk Inventor 2026: "
date: "2025-05-14 03:41:44"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/05/introducing-new-api-support-for-the-simplify-feature-in-autodesk-inventor-2026-.html "
typepad_basename: "introducing-new-api-support-for-the-simplify-feature-in-autodesk-inventor-2026-"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>The Simplify feature has long been a valuable tool within the Autodesk Inventor user interface, allowing users to reduce model complexity, enhance performance, and protect intellectual property. While the functionality has been available through the UI in earlier versions, Inventor 2026 marks the first release that introduces official API support for the Simplify feature.</p>
<p>This new API capability enables engineers and developers to automate the simplification process programmatically perfect for large-scale workflows, batch processing, or custom tool development.</p>
<p>In this article, we’ll revisit the benefits of the Simplify feature and walk through a sample VBA implementation using the new Inventor 2026 API.</p>
<hr />
<h3>✅ Why Use the Simplify Feature?</h3>
<p>Whether you’re managing large assemblies or sharing sensitive models with external teams, simplifying your design offers several key advantages:</p>
<ol>
<li><strong>Improve Performance</strong><br />By removing unnecessary detail or replacing geometry with simple envelopes, you can:
<ul>
<li>Reduce assembly load times</li>
<li>Lower memory consumption</li>
<li>Improve overall responsiveness in the graphics window</li>
</ul>
</li>
<li><strong>Protect Intellectual Property (IP)</strong><br />When sharing models outside your organization, the Simplify feature allows you to:
<ul>
<li>Strip internal or proprietary features</li>
<li>Replace complex geometry with abstracted shapes</li>
<li>Export only the essential form (e.g., via STEP or SAT formats)</li>
</ul>
</li>
<li><strong>Prepare for Simulation or Analysis</strong><br />Simplified parts are easier to mesh and simulate:
<ul>
<li>Fillets, threads, and small holes can be removed</li>
<li>Mesh generation becomes faster and more stable</li>
</ul>
</li>
<li><strong>Create Envelopes for Assembly Management</strong><br />Use simplified parts as envelopes to:
<ul>
<li>Keep large assemblies lightweight</li>
<li>Perform interference checks and space planning more effectively</li>
</ul>
</li>
</ol>
<hr />
<h3>🔧 VBA Sample – Using the Simplify API in Inventor 2026</h3>
<p>Starting with Inventor 2026, the Simplify feature can be accessed via the API. Here&#39;s a sample VBA procedure that shows how to automate simplification in a part document:</p>
<pre><code>
Sub PartSimplifyFeatureSample()
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument

    Dim oCompDef As PartComponentDefinition
    Set oCompDef = oDoc.ComponentDefinition

    Dim oSimplifyFeatures As SimplifyFeatures
    Set oSimplifyFeatures = oCompDef.Features.SimplifyFeatures

    Dim oSimplifyDef As SimplifyDefinition
    Set oSimplifyDef = oSimplifyFeatures.CreateDefinition()

    &#39; Configure the options for the simplify feature.
    oSimplifyDef.EnvelopesReplaceStyle = kEachBodyReplaceStyle
    oSimplifyDef.EnvelopeBoundingType = kOrientedMinimumBoundingBox
    oSimplifyDef.RemoveInternalBodies = False
    oSimplifyDef.RemoveBodiesBySize = True
    oSimplifyDef.RemoveBodiesSize = 10 &#39; in centimeters

    &#39; Create the Simplify feature.
    Dim oSimplifyFeature As SimplifyFeature
    Set oSimplifyFeature = oSimplifyFeatures.Add(oSimplifyDef)
End Sub
</code></pre>
<hr />
<h3>🔍 Key Parameters Explained</h3>
<table border="1" cellpadding="5" cellspacing="0">
<thead>
<tr>
<th>Property</th>
<th>Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td>EnvelopesReplaceStyle</td>
<td>Specifies how to replace geometry (e.g., per body or whole part)</td>
</tr>
<tr>
<td>EnvelopeBoundingType</td>
<td>Type of bounding geometry used (e.g., oriented bounding box)</td>
</tr>
<tr>
<td>RemoveInternalBodies</td>
<td>Removes fully enclosed bodies if set to True</td>
</tr>
<tr>
<td>RemoveBodiesBySize</td>
<td>Enables removal of bodies below a size threshold</td>
</tr>
<tr>
<td>RemoveBodiesSize</td>
<td>Sets the minimum diagonal size (in cm) for a body to remain in the model</td>
</tr>
</tbody>
</table>
<hr />
<h3>📝 Final Thoughts</h3>
<p>While the Simplify feature has been available through the Inventor UI in earlier versions, Inventor 2026 opens the door for automation through the API. This enhancement empowers developers to integrate simplification into custom workflows, boosting productivity and consistency across teams.</p>
<p>If you&#39;ve been manually simplifying your parts until now, it&#39;s time to explore the benefits of doing it programmatically in Inventor 2026.</p>
