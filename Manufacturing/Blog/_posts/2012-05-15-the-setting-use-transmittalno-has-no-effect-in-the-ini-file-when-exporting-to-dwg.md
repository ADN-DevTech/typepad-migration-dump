---
layout: "post"
title: "The setting &quot;USE TRANSMITTAL=NO&quot; has no effect in the ini file when exporting to DWG"
date: "2012-05-15 02:54:59"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/the-setting-use-transmittalno-has-no-effect-in-the-ini-file-when-exporting-to-dwg.html "
typepad_basename: "the-setting-use-transmittalno-has-no-effect-in-the-ini-file-when-exporting-to-dwg"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p><b>Issue:</b></p>  <p>I'm trying to save my Inventor drawing as an AutoCAD DWG file and I'm using an ini file for that. I'm assigning this ini file for the DWG translator AddIn using the Export_Acad_IniFile option.</p>  <p>I found an existing ini file and just modified the </p>  <pre><font style="background-color: #ffffff" color="#000000"><em>USE TRANSMITTAL=Yes</em></font></pre>
value to 

<pre><em>USE TRANSMITTAL=NO</em></pre>

<p>but Inventor keeps omitting this setting and places the resulting DWG in a zip file.</p>

<p><a name="section2"></a></p>

<p><b>Solution:</b></p>

<p>The problem is that Inventor expects <b>No</b> instead of <b>NO</b>. Note that the correct string is <b>capital N + small o</b>.</p>

<p>Once you correct this, the DWG export works fine.</p>

<p>To avoid problems like this in the future the best thing is to set up the ini file through the user interface.</p>

<p>1) In the application menu select <b>Save As &gt; Save Copy As</b></p>

<p>2) In the dialog change the <b>Save as type</b> to <b>AutoCAD Drawings</b> then click <b>Options...</b></p>

<p>3) Change the setting as needed then click<b> Next &gt;</b> and <b>Save Cofiguration ...</b></p>

<p>4) Use the created ini file when exporting to DWG.</p>
