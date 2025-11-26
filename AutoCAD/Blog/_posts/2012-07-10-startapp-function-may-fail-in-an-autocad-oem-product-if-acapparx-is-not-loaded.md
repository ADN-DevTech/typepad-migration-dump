---
layout: "post"
title: "STARTAPP function may fail in an AutoCAD OEM product if AcApp.arx is not loaded"
date: "2012-07-10 16:35:23"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/startapp-function-may-fail-in-an-autocad-oem-product-if-acapparx-is-not-loaded.html "
typepad_basename: "startapp-function-may-fail-in-an-autocad-oem-product-if-acapparx-is-not-loaded"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>An error occurs when the (startapp) function is used in an AutoCAD OEM product: &quot;Error: no function definition: STARTAPP&quot;. When I load the .fas file in Standard AutoCAD, the function works correctly and no error occurs. What is the cause of this problem in the OEM version?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The (startapp) function depends on AcApp.arx, so it needs it loaded to work correctly. This module may not be loaded automatically when the project is run from the OEM Make wizard. If an installer is made for the project and the product is installed, then AcApp.arx is always loaded automatically. </p>  <p>The arxload function could be used to avoid this problem as in this example.</p>  <p>(defun c:test ()   <br />&#160; (arxload &quot;AcApp.arx&quot;)    <br />&#160; (startapp &quot;hh.exe&quot; (findfile &quot;c:/ARXSDK/docs/arxdoc.chm&quot;))    <br />)</p>  <p><strong>NOTE: </strong>You should always test your OEM application as an installed application, not just as a build from the OEMMakeWizard – that way you ensure that the product is fully installed and registered correctly.</p>
