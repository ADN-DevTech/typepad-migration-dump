---
layout: "post"
title: "Revit API Error in Projects with Web References"
date: "2013-03-05 00:48:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/03/revit-api-error-in-projects-with-web-references.html "
typepad_basename: "revit-api-error-in-projects-with-web-references"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>We have so far received atleast a couple of queries stating something like -</p>  <p><em>While using Visual Studio 2010 and having referenced Revit API in my project, I also added some web reference to the project. I notice that the build process fails and the error mentioned below is thrown: </em></p>  <p><em>Error 1 An attempt was made to load an assembly with an incorrect format: C:\Program Files\Autodesk\Revit Structure 2013\Program\RevitAPI.dll. </em></p>  <p><em>SGEN </em></p>  <p><em>But when I try to debug, the project is working fine without any problem. How can I fix this?</em></p>  <p>The reported issue with the build error seems to be a generic issue with WebServices consumption in a .NET application with the project configured for release mode. </p>  <p>I found out (and tested) that under Build tab in Visual Studio project properties, you can set the <em>Generate Serialization Assembly</em> setting to Off (from Auto). This will disable the generation of the projectName.XMLSerializers.dll and this assembly will be generated dynamically at runtime (instead of compile time). </p>  <p>And this change of setting resolves the build error you have reported. </p>  <p>To read up more on this setting, you can find relevant information on MSDN – one such link is:</p>  <p><a href="http://msdn.microsoft.com/en-us/library/07bysfz2(v=vs.80).aspx">http://msdn.microsoft.com/en-us/library/07bysfz2(v=vs.80).aspx</a></p>
