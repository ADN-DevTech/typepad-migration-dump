---
layout: "post"
title: "The BOM Pipeline"
date: "2012-11-20 08:27:44"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/11/the-bom-pipeline.html "
typepad_basename: "the-bom-pipeline"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>I want to explain a bit how bill of material data works in Vault.&#0160; It’s not very intuitive looking at the API.&#0160; I’ll also attempt to answer the question, “Why did they do it that way?” </p>
<p><strong>The Primary Workflow</strong></p>
<p>Let me know over the “primary workflow,” which I’m defining as the workflow most responsible for the architecture.&#0160; There are plenty of other valid workflows, but I’m not going to focus on them much here.</p>
<p>It’s best if I list out the sequence of events in the primary workflow:</p>
<ol>
<li>The CAD user adds a file to Vault. </li>
<li>The CAD plug-in uploads a BOM object along with the file.&#0160; I’ll refer to this object as the “file BOM”. </li>
<li>The “file BOM” object gets stored in the database and mostly stays dormant. </li>
<li>A Vault user selects the file and runs the “Assign Item” command. </li>
<li>Internally, the “file BOM” gets read and becomes the template for creating or updating Vault Items.&#0160; In other words, the “file BOM” fills out most the information in the Assign Item wizard. </li>
<li>When the Assign Item wizard is complete, the Item objects are created and hooked together to form an “Item BOM”.&#0160; The Item BOM is what user sees and interacts with in the Item Master. </li>
<li>The “file BOM” is still around in the database, but it goes back to being dormant.&#0160; It would only be used again if there is an update to the Item BOM or a need to re-create the Item BOM. </li>
</ol>
<p><strong>Impact</strong></p>
<p>There are some interesting consequences as a result of this workflow.</p>
<ul>
<li>The item structure has nothing to do with the file assembly structure.&#0160; The file BOM is the only factor.</li>
<li>The file BOM is uploaded, even if the product is Vault Basic.&#0160; The idea is that the product may get upgraded to Vault Professional in the future.&#0160; If the upgrade never happens, the BOM data stays dormant forever. </li>
<li>The file BOM is not static data.&#0160; It may get altered by the server.&#0160; For example, data gets “filled in” during an Assign Item operation.&#0160; This way things are optimized if the Items ever need to be updated. </li>
<li>Results from GetBOMByFileId are not guaranteed to be the same, even when called on the same file version. See previous bullet.</li>
<li>GetBOMByFileId is not actually used anywhere in the process.&#0160; It’s an API-only feature intended for testing and troubleshooting purposes.&#0160; And yes, I’m well aware that many people are using this function beyond it’s intended scope. </li>
</ul>
<p><strong>File BOM vs. Item BOM</strong></p>
<p>Here is a quick rundown of the two different BOM representations.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="156">&#0160;</td>
<td valign="top" width="156"><strong>File BOM</strong></td>
<td valign="top" width="156"><strong>Item BOM</strong></td>
</tr>
<tr>
<td valign="top" width="156">Purpose</td>
<td valign="top" width="156">A template for the Item BOM</td>
<td valign="top" width="156">Represent an accurate Bill of Materials</td>
</tr>
<tr>
<td valign="top" width="156">Visible to end user</td>
<td valign="top" width="156">No</td>
<td valign="top" width="156">Yes</td>
</tr>
<tr>
<td valign="top" width="156">File scope</td>
<td valign="top" width="156">Attached to 1 and only 1 file.</td>
<td valign="top" width="156">Can be associated with 0 to many files.</td>
</tr>
<tr>
<td valign="top" width="156">Can be directly edited</td>
<td valign="top" width="156">No</td>
<td valign="top" width="156">Yes</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
