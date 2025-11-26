---
layout: "post"
title: "Acad::eWrongObjectType error with AcDbDictionary::setAt()"
date: "2013-01-08 17:44:49"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/acadewrongobjecttype-error-with-acdbdictionarysetat.html "
typepad_basename: "acadewrongobjecttype-error-with-acdbdictionarysetat"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>So what could cause the AcDbDictionary::setAt() to return Acad::eWrongObjectType error?</p>  <p>Unfortunately, this error is not documented for the AcDbDictionary::setAt method but there could be a couple of reasons why you could encounter this error.</p>  <p>If you are adding a custom object to the dictionary, you need to register that class with AutoCAD. If not, this could be the source of error.</p>  <p>In the acrxEntryPoint's kInitAppMsg handler of the application, add:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&lt;custom_object_name here&gt;::rxInit () ; </font></font></span></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">acrxBuildClassHierarchy () ;</font></font></span></p> </div>  <p>Within the kUnloadAppMsg handler:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#000000">deleteAcRxClass (&lt;custom_object_name here&gt;::desc ()) ;</font></font></span></p> </div>  <p>If you adding another object which isn't a custom object, or if you did what is explained above, then it means you are trying to add an object which can't sit into a AcDbDictionary object such as an entity (a graphical object).</p>
