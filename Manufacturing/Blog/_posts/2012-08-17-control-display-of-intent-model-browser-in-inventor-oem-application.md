---
layout: "post"
title: "Control display of Intent Model Browser in Inventor OEM Application"
date: "2012-08-17 17:27:15"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/control-display-of-intent-model-browser-in-inventor-oem-application.html "
typepad_basename: "control-display-of-intent-model-browser-in-inventor-oem-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>In OEM the Intent Model browser is not displaying. Is there a way to enable the Intent Model Browser in Inventor OEM?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>There are settings in the intent.config file. This file will be in a directory similar to this:    <br />&quot;C:\Program Files\Autodesk\Inventor ETO Components 2013\Bin\intent.config&quot;</p>  <p>Set the value to zero for HideModelTree to enable the Intent Model Browser. </p>  <p>These entries may also be of interest.    <br />HideRibbonUI     <br />HideContextMenu</p>  <p>If these settings are commented out, the default behavior occurs: UI is visible in Inventor, and is not visible in OEM.</p>
