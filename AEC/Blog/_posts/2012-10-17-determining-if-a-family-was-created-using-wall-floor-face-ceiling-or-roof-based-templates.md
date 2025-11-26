---
layout: "post"
title: "Determining if a Family was created using Wall, Floor, Face, Ceiling or Roof based templates"
date: "2012-10-17 08:46:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/10/determining-if-a-family-was-created-using-wall-floor-face-ceiling-or-roof-based-templates.html "
typepad_basename: "determining-if-a-family-was-created-using-wall-floor-face-ceiling-or-roof-based-templates"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>
<p>Using the Revit API, we can determine if a family was created using a Wall, Floor, Face, Ceiling or Roof based template. This is particularly helpful in case of, say, a generic mass family which can be created with any of the specific templates – Wall, Face, Floor, Ceiling, Roof based etc.</p>
<p>The solution is to access the Family Document –&gt; Owner Family –&gt; Host parameter, to get this information. This parameter value is stored as an integer value and the integer values imply the following about the templates (hosting behavior):&#0160;</p>
<p>Wall based - 1</p>
<p>Floor based - 2</p>
<p>Ceiling based - 3</p>
<p>Roof based - 4</p>
<p>Face based - 5</p>
<p>The families created using line and pattern based templates, however return integer value of 0 and cannot be determined using this method. </p>
