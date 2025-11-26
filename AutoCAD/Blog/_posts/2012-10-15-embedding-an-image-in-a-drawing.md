---
layout: "post"
title: "Embedding an image in a drawing"
date: "2012-10-15 00:55:27"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/embedding-an-image-in-a-drawing.html "
typepad_basename: "embedding-an-image-in-a-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>How do I embed an image in my drawing file so that it I don't have to include a   <br />separate image file with the drawing?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>This can be done in two ways. The method you choose depends on your   <br />requirements.</p>  <p>* As an OLE Insert - </p>  <p>You can insert an image using Insert -&gt; OLE Object to create an AcDbOle2Frame   <br />that has its image data embedded in the drawing. A disadvantage to this approach    <br />is that you cannot create an AcDbOle2Frame programmatically - user interaction    <br />is required to create it.</p>  <p>* Using a custom object - (two options)</p>  <p>An AcDbRasterImageDef/AcDbRasterImage pair can be created and inserted   <br />programatically, but the AcDbRasterImageDef requires an external file to store    <br />its image data. To avoid this, you can either:</p>  <p>1. derive your own custom object from AcDbRasterImageDef, or    <br />2. store your image data in a separate custom object that creates a temporary    <br />file when it is opened, and sets this file to be the image file used by its    <br />associated AcDbRasterImageDef.</p>  <p>The attached sample project demonstrate the second approach. It takes the original image file    <br />and stores it in a custom object, AsdkBindObject. </p>  <p>When AsdkBindImage is opened, it creates a temporary file that is a copy of the original image file   <br />and calls the setActiveFileName() function of the AcDbRasterImageDef, so that    <br />this temporary file is used by the AcDbRasterImageDef, rather than the one    <br />specified by setSourceFileName.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:97de5252-2dfb-4e2e-8abb-300fc9893c0a" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/arxbindimage-1.zip" target="_blank">arxbindimage.zip</a></p></div>
