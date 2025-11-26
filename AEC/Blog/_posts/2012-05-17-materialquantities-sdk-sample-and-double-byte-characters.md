---
layout: "post"
title: "MaterialQuantities SDK sample and double byte characters"
date: "2012-05-17 05:00:00"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/materialquantities-sdk-sample-and-double-byte-characters.html "
typepad_basename: "materialquantities-sdk-sample-and-double-byte-characters"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>If you are from a country of double byte characters, you may notice that&#0160;MaterialQuantities from Revit API SDK Sample does not work well with double byte characters.&#0160;We&#0160;found that, for example, Korean characters were corrupted if material names are in Korean characters. You may wonder how to make the sample work with double byte characters.</p>
<p>This problem is not Revit API itself as Revit is handling Korean as Unicode. The problem is that this sample saves file as a csv, which then Excel file picks up as ascii.&#0160;</p>
<p>Two options to fix this problem:</p>
<ol>
<li>If you import it as Unicode UTF-8 (Excel &gt;&gt; Data &gt;&gt; From Text &gt;&gt; File Origin Unicode UTF-8), it read as Unicode. You should be able to read it. You can do this manually.</li>
<li>Rewrite a code to write as an Excel sheet.&#0160; Fire Rating sample in the SDK is another sample that uses Excel and it uses workbook sheet.&#0160; You may convert the sample in a similar manner.</li>
</ol>
