---
layout: "post"
title: "Porting guide from Level of Details to Model states."
date: "2022-05-24 04:44:27"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2022/05/porting-guide-from-level-of-details-to-model-states.html "
typepad_basename: "porting-guide-from-level-of-details-to-model-states"
typepad_status: "Publish"
---

<p><span style="display: inline !important; float: none; background-color: #ffffff; color: #000000; cursor: text; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: none; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;">by </span><a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener" style="color: #0066cc; font-family: Georgia; font-size: 14px; font-style: normal; font-variant: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-decoration: underline; text-indent: 0px; text-transform: none; -webkit-text-stroke-width: 0px; white-space: normal; word-spacing: 0px;" target="_blank">Chandra shekar Gopal</a>,</p>
<h3>Introduction to Model States</h3>
<p>Model States were introduced in Inventor 2022. While product documentation covers the basics well, our ADN team has offered extensive support for developers transitioning to this feature. With Inventor 2023 now released, it’s a good time to review code migration requirements and discuss some known issues.</p>
<h3>Official Documentation</h3>
<p>These links, provided for Inventor 2023, also apply to Inventor 2022:</p>
<ul>
<li><a href="https://help.autodesk.com/view/INVNTOR/2023/ENU/?guid=GUID-23D2D59B-4E62-4B4C-AD4F-46FE9BC0CEB1">Introducing Model States (What&#39;s New in 2022)</a></li>
<li><a href="https://help.autodesk.com/view/INVNTOR/2023/ENU/?guid=GUID-378BFF19-FD92-4AF2-8DEB-9ACD41FC462E">About Model States</a></li>
</ul>
<h3>Key Observations</h3>
<p>“Design Options” and “varying configurations” are important facets of Model States. Compared to the older Level of Detail (LOD) feature, Model States offer improved flexibility—but can also present migration challenges.</p>
<h3>Developer Impact</h3>
<p>Inventor developers have generally experienced smooth transitions between versions. However, Model States represent the most disruptive change to date. We&#39;d love to hear how we can better support you—please leave your thoughts in the comments below.</p>
<h3>Conversion of Level of Detail into Model State</h3>
<p>Understanding how LODs convert to Model States is crucial. See the documentation here:</p>
<p><a href="https://help.autodesk.com/view/INVNTOR/2023/ENU/?guid=GUID-6F07C43B-2A43-4D9E-B62A-1E872C3F8E84">About Level of Detail Migration</a></p>
<h3>API Aspects</h3>
<p>Autodesk has updated the API documentation to help developers. Reference:</p>
<p><a href="https://help.autodesk.com/view/INVNTOR/2023/ENU/?guid=GUID-74A93E3E-C61D-41E1-9444-26EB7A620A25">Working with Model States</a></p>
<h4>Legacy API: Accessing LOD in Inventor 2021</h4>
<pre><code>Sub GetLods()
    Dim sFileName As String
    sFileName = &quot;C:\Temp\Assembly1.iam&quot;

    Dim oDoc As AssemblyDocument
    Set oDoc = ThisApplication.Documents.Open(sFileName)

    Dim oAssemblyCompDef As AssemblyComponentDefinition
    Set oAssemblyCompDef = oDoc.ComponentDefinition

    Dim oLODs As LevelOfDetailRepresentations
    Set oLODs = oAssemblyCompDef.RepresentationsManager.LevelOfDetailRepresentations

    Dim oLOD As LevelOfDetailRepresentation
    For Each oLOD In oLODs
        Debug.Print oLOD.Name
    Next

    oDoc.Close True
End Sub
</code></pre>
<h3>Different Types of Documents with Respect to Model State</h3>
<p>Parts or assemblies fall into one of three categories:</p>
<ul>
<li>Plain Document</li>
<li>Model State Document</li>
<li>iComponent (iPart/iAssembly) Document</li>
</ul>
<h4>Document Type Boolean Table</h4>
<table border="1" cellpadding="4" cellspacing="0">
<thead>
<tr>
<th style="width: 71.7688px;">Type</th>
<th style="width: 162.806px;">IsModelState<br />Factory</th>
<th style="width: 167.475px;">IsModelState<br />Member</th>
<th style="width: 112.438px;">IsiPartFactory</th>
<th style="width: 117.113px;">IsiPartMember</th>
<th style="width: 154.769px;">IsiAssembly<br />Factory</th>
<th style="width: 159.438px;">IsiAssembly<br />Member</th>
</tr>
</thead>
<tbody>
<tr>
<td style="width: 71.7688px;">Plain Document</td>
<td style="width: 162.806px;">&#0160;</td>
<td style="width: 167.475px;">&#0160;</td>
<td style="width: 112.438px;">&#0160;</td>
<td style="width: 117.113px;">&#0160;</td>
<td style="width: 154.769px;">&#0160;</td>
<td style="width: 159.438px;">&#0160;</td>
</tr>
<tr>
<td style="width: 71.7688px;">Model State Factory</td>
<td style="width: 162.806px;">True</td>
<td style="width: 167.475px;">&#0160;</td>
<td style="width: 112.438px;">&#0160;</td>
<td style="width: 117.113px;">&#0160;</td>
<td style="width: 154.769px;">&#0160;</td>
<td style="width: 159.438px;">&#0160;</td>
</tr>
<tr>
<td style="width: 71.7688px;">Model State Member</td>
<td style="width: 162.806px;">&#0160;</td>
<td style="width: 167.475px;">True</td>
<td style="width: 112.438px;">&#0160;</td>
<td style="width: 117.113px;">&#0160;</td>
<td style="width: 154.769px;">&#0160;</td>
<td style="width: 159.438px;">&#0160;</td>
</tr>
<tr>
<td style="width: 71.7688px;">iPart Factory</td>
<td style="width: 162.806px;">&#0160;</td>
<td style="width: 167.475px;">&#0160;</td>
<td style="width: 112.438px;">True</td>
<td style="width: 117.113px;">&#0160;</td>
<td style="width: 154.769px;">&#0160;</td>
<td style="width: 159.438px;">&#0160;</td>
</tr>
<tr>
<td style="width: 71.7688px;">iPart Member</td>
<td style="width: 162.806px;">&#0160;</td>
<td style="width: 167.475px;">&#0160;</td>
<td style="width: 112.438px;">&#0160;</td>
<td style="width: 117.113px;">True</td>
<td style="width: 154.769px;">&#0160;</td>
<td style="width: 159.438px;">&#0160;</td>
</tr>
<tr>
<td style="width: 71.7688px;">iAssembly Factory</td>
<td style="width: 162.806px;">&#0160;</td>
<td style="width: 167.475px;">&#0160;</td>
<td style="width: 112.438px;">&#0160;</td>
<td style="width: 117.113px;">&#0160;</td>
<td style="width: 154.769px;">True</td>
<td style="width: 159.438px;">&#0160;</td>
</tr>
<tr>
<td style="width: 71.7688px;">iAssembly Member</td>
<td style="width: 162.806px;">&#0160;</td>
<td style="width: 167.475px;">&#0160;</td>
<td style="width: 112.438px;">&#0160;</td>
<td style="width: 117.113px;">&#0160;</td>
<td style="width: 154.769px;">&#0160;</td>
<td style="width: 159.438px;">True</td>
</tr>
</tbody>
</table>
<h3>Factory vs Member Documents</h3>
<h4>Model State Factory Document</h4>
<ul>
<li>Fully editable</li>
<li>Drives all variations</li>
<li>Includes UI frame</li>
<li>Not stored on disk</li>
</ul>
<h4>Model State Member Document</h4>
<ul>
<li>Represents a variation</li>
<li>Non-modifiable (if factory exists)</li>
<li>No full UI</li>
<li>Multiple members per file</li>
</ul>
<h3>Accessing iProperties via Apprentice Server API</h3>
<p>In Inventor 2022, <code>ApprenticeServerDocument.PropertySets</code> returns only active model state properties. Use <code>FilePropertySets</code> (added in Inventor 2022.1) for editable iProperties.</p>
<h3>Behavioral Notes</h3>
<h4>IsModifiable in Inventor 2022</h4>
<p>When a Model State is added, <code>IsModifiable = True</code>, blocking property edits via Apprentice. Deleting the model state reverts this.</p>
<h4>DocumentDescriptor Behavior</h4>
<p>In Inventor 2022, suppressed occurrences were excluded from <code>ReferencedDocumentDescriptors.Count</code>. This is resolved in Inventor 2023.</p>
<h4>Checking Dirty States of Model State Members</h4>
<pre><code>Dim doc As AssemblyDocument
Set doc = ThisApplication.ActiveDocument

Dim oMs As ModelState
For Each oMs In doc.ComponentDefinition.ModelStates
    If oMs.ModelStateType = kSubstituteModelStateType Then
        Dim oMsDoc As AssemblyDocument
        Set oMsDoc = oMs.Document
        If Not oMsDoc Is Nothing Then
            Debug.Print oMs.Document.Dirty
        End If
    End If
Next
</code></pre>
<h4>Alternative for Substitute Model State</h4>
<pre><code>Dim oSub As AssemblyDocument
Set oSub = ThisApplication.Documents.ItemByName(&quot;D:\Temp\Assembly1.iam&lt;Substitute1&gt;&quot;)
Debug.Print oSub.Dirty
</code></pre>
<h4>Substitute LOD iProperties in Pre-2022 Files</h4>
<p>In older files, substitute LODs become substitute model states. iProperty access via API was fixed in Inventor 2022.3 and 2023:</p>
<pre><code>Sub GetPartNumber()
    Dim doc As Document
    Set doc = ThisApplication.ActiveDocument
    Debug.Print doc.FullDocumentName

    Dim propSet As PropertySet
    Set propSet = doc.PropertySets.Item(&quot;{32853F0F-3444-11D1-9E93-0060B03C1CA6}&quot;)

    Dim prop As Property
    Set prop = propSet.Item(&quot;Part Number&quot;)
    Debug.Print &quot;PartNumber=&quot; &amp; prop.Value
End Sub
</code></pre>
