---
layout: "post"
title: "Hotfix Resolves Export and Print Issue"
date: "2017-05-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2018"
  - "Export"
  - "Print"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/05/hotfix-resolves-export-and-print-issue.html "
typepad_basename: "hotfix-resolves-export-and-print-issue"
typepad_status: "Publish"
---

<p>Here is just a quick note to begin the week with the good news that the Revit 2018 issue reported in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-2018-dwf-shaded-views-and-windows-explorer-bug/m-p/7043102">Revit 2018, DWF, shaded views, and Windows explorer bug</a> is
resolved by the hotfix to handle 
the <a href="http://knowledge.autodesk.com/article/Product-crash-when-communicating-with-Licensing-Server-Hotfix">product crash when communicating with licensing server</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8facab3970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8facab3970b image-full img-responsive" alt="Hotfix May 20" title="Hotfix May 20" src="/assets/image_45e7f9.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>The problem can apparently also occur when exporting or printing views.</p>

<p>This affects several ADN cases and development database issues, e.g., </p>

<ul>
<li>12919761 <em>Revit 2018, DWF, Shaded Views, and Windows Explorer - Bug</em></li>
<li>12894987 <em>Revit crashes when exporting / printing Sheets via API while a modal dialog is open</em></li>
<li>REVIT-112605 <em>RVT 2018 crash involving DWF, shaded views and Windows explorer -- 12919761</em> </li>
<li>REVIT-112607 <em>Revit crashes when exporting / printing Sheets via API while a modal dialog is open -- 12894987</em></li>
<li>REVIT-112150</li>
<li>REVIT-112561</li>
</ul>

<p>To reproduce:</p>

<ol>
<li>Open the rac_basic_sample_project.rvt project that is installed with Revit 2018</li>
<li>Go to Add-Ins &gt; Export Crash Sample &gt; Open Window</li>
<li>Click Export or Print</li>
</ol>

<p>The sample project is specifically printing / exporting sheet A101, but I'm seeing the issue on just about every sheet in that project.</p>

<p>I've only seen the crash handling dialog once and I submitted a CER for it. Every other time I get the Windows dialog saying Revit has stopped working.</p>

<p>Please apply this fix if you have encountered anything similar.</p>
