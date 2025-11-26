---
layout: "post"
title: "Connecting CAO with Database"
date: "2021-02-04 08:07:36"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2021/02/connecting-cao-with-database.html "
typepad_basename: "connecting-cao-with-database"
typepad_status: "Publish"
---

<h5><span style="font-size: 10pt;">NOTE - This workflow describes how to connect to CAO with MS Access database (*.mdb/*.accdb)</span><br /><span style="font-size: 10pt;">From AutoCAD 2020, we have retired 32-bit AutoCAD, to connect to MS Access database from 64 Bit AutoCAD, you need install 64-bit MS Access DB Engine.&#0160;</span></h5>
<p><span style="font-size: 10pt;">(thanks for pointing out -Norman Yuan)</span></p>
<p><span style="font-size: 10pt;">About version of Microsoft Office and its bitness&#0160;</span></p>
<p><span style="font-size: 10pt;">Please refer <a href="https://support.microsoft.com/en-us/office/about-office-what-version-of-office-am-i-using-932788b8-a3ce-44bf-bb09-e334518b8b19">this blog on Microsoft</a></span></p>
<p><span style="font-size: 10pt;">I&#39;m using MS Access 64 Bit (MS Office 365)</span></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec36563200c-pi" style="float: left;"><img alt="2021-03-12_10-47-28" class="asset  asset-image at-xid-6a0167607c2431970b026bdec36563200c img-responsive" src="/assets/image_307119.jpg" style="margin: 0px 5px 5px 0px;" title="2021-03-12_10-47-28" /></a></p>
<h2>&#0160;</h2>
<h2>&#0160;</h2>
<h2>&#0160;</h2>
<h2>&#0160;</h2>
<h2>&#0160;</h2>
<h2>Step1: Launch AutoCAD 2021</h2>
<ul>
<li>Open the &quot;Floor Planning&quot; drawing from &quot;C:\~\AutoCAD 2021\Sample\Database Connectivity\Floor Plan Sample.dwg&quot;</li>
<li>Enter &quot;DBCONNECT&quot;</li>
<li>From DBCONNECT MANAGER, select <strong>Jet_dbSamples</strong> in &quot;Data Sources&quot; field.</li>
<li>
<div>Right Click and Select &quot;Configure&quot;</div>
<p><img alt="" src="/assets/image_855690.jpg" /></p>
</li>
</ul>
<h2>Step2: Configure DB</h2>
<ul>
<li>
<div style="text-align: justify;">In the &quot;Data Link Properties&quot;, select <strong>Microsoft OLE DB provider for ODBC Drivers </strong>and &quot;NEXT.&quot;</div>
<p style="text-align: justify;"><img alt="" src="/assets/image_961744.jpg" /></p>
</li>
<li>
<div>In Next page, check &quot;Use Connection String&quot; and click <strong>Build</strong>.</div>
<p><img alt="" src="/assets/image_467303.jpg" /></p>
</li>
<li>
<div>In the &quot;Select Data Source&quot; window, select &quot;Machine Data Source &quot;page and click <strong>New</strong>.</div>
<p><img alt="" src="/assets/image_90225.jpg" /></p>
</li>
<li>
<div>Create New Data Source -&gt; Check &quot;User Data Source&quot; -&gt; Next</div>
<p><img alt="" src="/assets/image_279032.jpg" /></p>
</li>
<li>
<div>Select &quot;Microsoft Access Driver (*.mdb,*.accdb) -&gt; Next -&gt; Finish</div>
<p><img alt="" src="/assets/image_242413.jpg" /></p>
</li>
</ul>
<h2>Step3: ODBC Microsoft Access Setup</h2>
<ul>
<li>In ODBC Microsoft Access Setup window, write Data Source name of your choice.</li>
<li>
<div>Select `Database` and pick the folder where db-samples.mdb is located.</div>
<ul>
<li>In my example, it is located at C:\~\AutoCAD 2021\Sample\Database Connectivity.</li>
</ul>
</li>
</ul>
<p style="margin-left: 36pt;"><img alt="" src="/assets/image_854059.jpg" /></p>
<ul>
<li>OK</li>
<li>OK</li>
<li>OK</li>
<li>OK</li>
</ul>
<h2>Step4: Testing Connection</h2>
<ul>
<li>
<div>Come Back to &quot;Data Link Properties&quot; and Select &quot;Test Connection&quot;</div>
<p><img alt="" src="/assets/image_708342.jpg" /></p>
</li>
</ul>
<p style="margin-left: 36pt;">&#0160;</p>
<p style="margin-left: 36pt;">&#0160;</p>
<p style="margin-left: 36pt;">&#0160;</p>
<p style="margin-left: 36pt;">&#0160;</p>
<p style="margin-left: 36pt;">&#0160;</p>
