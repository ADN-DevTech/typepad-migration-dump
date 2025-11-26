---
layout: "post"
title: "Getting double numeric values to show up properly in the Properties Window (OPM)"
date: "2012-07-18 05:19:37"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/getting-double-numeric-values-to-show-up-properly-in-the-properties-window-opm.html "
typepad_basename: "getting-double-numeric-values-to-show-up-properly-in-the-properties-window-opm"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I've created a COM Wrapper for a Custom Object I've made. It seems that any/all <strong>double</strong> values that are added to the com wrapper automatically get displayed in the properties window as units of the type specified in the &quot;Drawing Units&quot; dialog. So if the double value is 14.500, it is automatically displayed by the Properties box as 1'-2½&quot; in the AutoCAD properties window.</p>  <p>Is there any way to override this feature of the properties window and just allow the double entered to not be changed and shown as is???</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>By default the OPM interprets double values as distance values.</p>  <p>Rather than returning a double variable type return one of the following (in your IDL and in your property functions) :</p>  <ul>   <li>ACAD_DISTANCE - distance value</li>    <li>ACAD_ANGLE - angle value</li>    <li>ACAD_NOUNITS - simple double/ads_real value</li> </ul>
