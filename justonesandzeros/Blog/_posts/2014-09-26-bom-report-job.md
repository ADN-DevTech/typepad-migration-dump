---
layout: "post"
title: "BOM Report Job"
date: "2014-09-26 14:42:25"
author: "Doug Redmond"
categories:
  - "Sample Applications"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/09/bom-report-job.html "
typepad_basename: "bom-report-job"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/SampleApp4.png" /></p>
<p>To celebrate the release of Vault 2015 R2, I wrote an app that lets you generate BOM reports automatically through the Vault job server.&#0160;</p>
<p>If you are not already aware, Vault 2015 R2 has a BOM Report feature.&#0160; From any BOM view, you can run the Report command to generate a report of the BOM that you are viewing.&#0160; This report uses the same engine as File and Folder reports, which has a good deal of customization options.&#0160; There are also default templates to help you get started quickly.</p>
<p><img alt="" src="/assets/output.png" /></p>
<p>&#0160;</p>
<p>However, the out-of-the-box BOM Report feature is a manual process.&#0160; If you want to automate things, my plug-in hooks to job server and lets you trigger reports when items change state.&#0160; When the job runs, it generates the report, saves it to Vault as a PDF and attaches it to the item.</p>
<p><img alt="" src="/assets/ItemView.png" /></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Requirements:</strong></p>
<ul>
<li>Vault Professional 2015 R2</li>
<li>Must have job queue enabled.</li>
<li>Must have a “Quick Change” state that the plug-in can use to move the item to edit mode.</li>
<li>Must have a folder in Vault for storing the generated reports.</li>
</ul>
<p><a href="http://justonesandzeros.typepad.com/Apps/BOMReportJob/BOMReportJob-1.0.3.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/BOMReportJob/BOMReportJob-1.0.3.0-src.zip">Click here to download the source code</a></p>
<p>To configure BOM Reports, install the app and follow the readme.&#0160; You can find the readme in the install location (%ProgramData%\Autodesk\Vault 2015 R2\Extensions\BOMReportJob).&#0160; Administrative rights are required to configure the Vault job server.</p>
<hr noshade="noshade" style="color: #013181;" />
<p>All the apps on this blog have a <a href="http://justonesandzeros.typepad.com/blog/disclaimer.html">legal disclaimer</a> on them, but I want to make this one more obvious since it’s dealing with BOM data.&#0160; <strong>BOM Report Job is not an official product.</strong>&#0160; It’s technically classified as a sample app.&#0160; So please keep that in mind when dealing with these reports.&#0160; For example, if you are using the report to purchase $50,000 worth of parts, you may want to double check the numbers first before putting the order in.&#0160;</p>
<hr noshade="noshade" style="color: #013181;" />
