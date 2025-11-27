---
layout: "post"
title: "3D PDF Options"
date: "2016-04-04 02:36:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/04/3d-pdf-options.html "
typepad_basename: "3d-pdf-options"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Inventor 2017</strong> has now <strong>3D PDF</strong> support, and the export can also be automated using the <strong>API</strong>. There is a sample for it in the <strong>API Help</strong> file but it does not show all the available options. So here they are:</p>
<p><strong>1) Publish Options (Inventor.NameValueMap)</strong></p>
<p><strong>AttachedFiles</strong> (String array)<br />&#0160; - each string contains the full path of the file to be attached<br /> <strong>STEPFileOptions</strong> (Inventor.NameValueMap)<br />&#0160; - see&#0160;<strong>STEPFileOptions Options&#0160;</strong>below<br /> <strong>ExportAllProperties</strong> (Boolean)<br /> <strong>ExportProperties</strong> (String array)<br />&#0160; - each string needs to be in the format of &quot;&lt;InternalName of property set&gt;:&lt;name of property&gt;&quot;, <br />&#0160; &#0160; e.g.&#0160;&quot;{F29F85E0-4FF9-1068-AB91-08002B27B3D9}:Title&quot;<br /> <strong>ExportDesignViewRepresentations</strong> (String array)<br />&#0160; - list of the names of Design View Representations you want to export<br /> <strong>VisualizationQuality</strong> (Inventor.AccuracyEnum)<br /> <strong>GenerateAndAttachSTEPFile</strong> (Boolean)<br /> <strong>LimitToEntitiesInDVRs</strong> (Boolean)<br />&#0160; - Limit export to entities in selected Design View Representations<br /> <strong>FileOutputLocation</strong> (String)<br /> <strong>ExportTemplate</strong> (String)<br /> <strong>ViewPDFWhenFinished</strong> (Boolean)</p>
<p><strong>2) STEPFileOptions Options&#0160;<strong>(Inventor.NameValueMap)</strong></strong></p>
<p><strong>ApplicationProtocolType</strong> (Integer)<br />&#0160; - possible values: 2 (=eAP203), 4 (=eAP214IS), 5 (=<span class="s1">eAP242</span>)<br /> <strong>Author</strong> (String)<br /> <strong>Authorization</strong> (String)<br /> <strong>Description</strong> (String)<br /> <strong>ExportFitTolerance</strong> (Double)<br /> <strong>IncludeSketches</strong> (Boolean)<br /> <strong>Organization</strong> (String)</p>
