---
layout: "post"
title: "AUTOCAD OEM 2023&ndash;Product Doesn&rsquo;t Start"
date: "2022-09-25 16:38:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2022/09/autocad-oem-2023product-doesnt-start.html "
typepad_basename: "autocad-oem-2023product-doesnt-start"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>
    <font size="2">By </font><a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">
        <font size="2">Madhukar Moogala</font>
    </a>
</p>
<p><br></p>
<p>
    <font size="2">We have moved to new installation tech called ODIS – On Demand Installation Services, this change is
        good in many aspects as its purpose is to create an installation, deployment, and update experience that's fast,
        easy, predictable, and painless for customers, as well as for the teams who develop and deliver Autodesk
        products.</font>
<p>
    <font size="2">First and foremost refer to this <a href="https://knowledge.autodesk.com/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/Unable-to-run-AutoCAD-when-opening-AutoCAD.html">blog</a> </font><a title="https://knowledge.autodesk.com/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/Unable-to-run-AutoCAD-when-opening-AutoCAD.html" href="https://knowledge.autodesk.com/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/Unable-to-run-AutoCAD-when-opening-AutoCAD.html">
        <font size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font>
    </a>
</p>
<p>
    <font size="2">If this doesn't solve or fix the problem.</font>
</p>
<p>
    <font size="2">Next, request these files from the target user -</font>
</p>
<pre>    <font size="2">%localappdata%\Autodesk\ODIS</font>
    <font size="2">%temp%\*.log</font>
</pre>
<p>
    <font size="2">Check for \ODIS\DDA.log, see if you find any errors, this should give some clue for you.</font>
</p>
<p>
    <font size="2">If this didn't solve the problem, contact A D N support and send those files, one of us can research
        this for you.</font></p>
