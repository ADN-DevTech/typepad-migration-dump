---
layout: "post"
title: "Explode Text Entity"
date: "2012-10-02 01:05:07"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/explode-text-entity.html "
typepad_basename: "explode-text-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>How to explode SHX based AcDbText entities to its constituent lines?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>You can “explode” or tessellate the text entity using the AcGiTextEngine::tessellate() method. But the method returns raw point information which can be used to create individual lines. The method does not honor the text style table properties and the text height, width are assumed to be of one unit.</p>  <p>To get the exact representation of an AcDbText entity, we need to transform this raw point information depending upon the attributes of the text and its text style. In the attached sample, the function fGeneratematrix() creates a transformation matrix which will place the lines drawn using the points exactly at the text location. This transformation matrix will take care of oblique angle, upside down, backward text properties.</p>  <p>NOTE: the true type fonts are not supported.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:06acaf6b-d7e4-46a1-9483-86b82ff165fa" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/_arxtesselatetext.zip" target="_blank">_ArxTesselateText.zip</a></p></div>
