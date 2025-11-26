---
layout: "post"
title: "Refresh Sections using ACA .NET API"
date: "2013-02-18 19:24:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/02/refresh-sections-using-aca-net-api.html "
typepad_basename: "refresh-sections-using-aca-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>In the recent past, we had received some queries around refreshing the sections directly using the API (without having to use SendCommand). </p>  <p>There isn’t any direct API which allows API users to the refresh on the sections/elevations. Sending the command using SendCommand would be one workaround for some API users. But if SendCommand is not an option (as in this case), the ACA 2013 .NET API provides a method called GenerateSection which can help generate 2D sections programmatically. The method signature is included below -   <br /><em>Autodesk.Aec.ApplicationServices.Utility.SectionUtilities.GenerateSection(Autodesk.AutoCAD.DatabaseServices.ObjectId, Autodesk.AutoCAD.Runtime.RXClass, string, string, bool, string)     <br /></em>&#160; <br />This API might help re-create sections and this might be closest to what we can do as work-around for now.     </p>
