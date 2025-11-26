---
layout: "post"
title: "Obtaining the Nominal Diameter as a string distance in Plant3d"
date: "2013-02-08 12:13:54"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2013"
  - "Fenton Webb"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/obtaining-the-nominal-diameter-as-a-string-distance-in-plant3d.html "
typepad_basename: "obtaining-the-nominal-diameter-as-a-string-distance-in-plant3d"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>Obtaining the Nominal Diameter as a string in Plant3d is done using <span style="font-size: 9pt; font-family: &quot;Arial&quot;,&quot;sans-serif&quot;; color: black; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-gb; mso-fareast-language: en-gb; mso-bidi-language: ar-sa">NominalDiameterMap.csv Excel file found in Program Files\AutoCAD Plant3d 2013 folder. This spreadsheet mainly sets up a logical conversion map between imperial and metric sizes, because the math conversions are not always aligned to the required size strings. </span></p>  <p><span style="font-size: 9pt; font-family: &quot;Arial&quot;,&quot;sans-serif&quot;; color: black; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-gb; mso-fareast-language: en-gb; mso-bidi-language: ar-sa">Plant already has this map loaded, you can access it via:</span></p>  <p><span style="font-size: 9pt; font-family: &quot;Arial&quot;,&quot;sans-serif&quot;; color: black; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-gb; mso-fareast-language: en-gb; mso-bidi-language: ar-sa"><span lang="EN-US" style="font-size: 9.5pt; font-family: consolas; color: #2b91af; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-us; mso-fareast-language: en-gb; mso-bidi-language: ar-sa">NominalDiameterMap</span><span lang="EN-US" style="font-size: 9.5pt; font-family: consolas; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-us; mso-fareast-language: en-gb; mso-bidi-language: ar-sa"> DefaultMap;</span></span></p>  <p><span style="font-size: 9pt; font-family: &quot;Arial&quot;,&quot;sans-serif&quot;; color: black; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-gb; mso-fareast-language: en-gb; mso-bidi-language: ar-sa">You can convert the Nominal Diameter value to a string using this code:</span></p>  <p><font face="Consolas"><font color="#4bacc6">NominalDiameter</font> d = new NominalDiameter(0.125);</font></p>  <p><font face="Consolas"><font color="#4bacc6">string</font> nddDisplay = d.ToDisplayString(null);</font></p>  <p>&#160;</p>  <p><span style="font-size: 9pt; font-family: &quot;Arial&quot;,&quot;sans-serif&quot;; color: black; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: en-gb; mso-fareast-language: en-gb; mso-bidi-language: ar-sa">&#160;</span></p>
