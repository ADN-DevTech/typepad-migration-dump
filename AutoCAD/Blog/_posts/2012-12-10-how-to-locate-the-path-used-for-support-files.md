---
layout: "post"
title: "How to locate the path used for support files"
date: "2012-12-10 08:47:58"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "Off-topic"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/how-to-locate-the-path-used-for-support-files.html "
typepad_basename: "how-to-locate-the-path-used-for-support-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>AutoCAD puts support files (i.e. acad.pgp) in the Users/username/... folder. My program searches for the acad.pgp and other support files to update. How can I easily locate this directory?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>There are a couple of means available to retrieve this path. One is to get the value from the Registry. It is stored in the <em><strong>RoamableRootFolder</strong></em> key in this location:&#160; <br /><strong><em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\&lt;Release&gt;\ACAD-&lt;Version&gt;:&lt;Language&gt;</em></strong> </p>  <p>You could use the value of CurVer key in this location    <br /><strong><em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\&lt;Release&gt; </em></strong>To first get the CurVer to get the correct location for different AutoCAD based products.</p>  <p>Another approach would be to use the sysvar <strong><em>roamablerootprefix</em></strong>. Which will also return this path.    </p>
