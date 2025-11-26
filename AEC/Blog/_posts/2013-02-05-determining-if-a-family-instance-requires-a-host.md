---
layout: "post"
title: "Determining if a family instance requires a host"
date: "2013-02-05 23:12:19"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/02/determining-if-a-family-instance-requires-a-host.html "
typepad_basename: "determining-if-a-family-instance-requires-a-host"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Similar questions like that of the title of this post, has come up a couple of times in the recent past. This topic has been covered in <a href="http://adndevblog.typepad.com/aec/2012/10/determining-if-a-family-was-created-using-wall-floor-face-ceiling-or-roof-based-templates.html" target="_blank">this</a> blog-post too – though from a slightly different perspective. </p>  <p>As mentioned in <a href="http://adndevblog.typepad.com/aec/2012/10/determining-if-a-family-was-created-using-wall-floor-face-ceiling-or-roof-based-templates.html" target="_blank">this</a> blog-post, the key to finding out if a Family Instance requires a host or not is to find out the Host parameter of the Family itself. This blog-post contains a screenshot of a Outlet-Duplex family instance (which is wall based) and shows how Revit LookUp tool can be used to confirm that the value of the HOST parameter as 1 (which confirms that the Family is wall-based). Once a given family that needs to be determined, is loaded into a Revit model, there is no need to create an instance of the family and then edit it and then access the HOST parameter – the loaded Family itself will provide access to the value of this parameter directly to help determine if the family needs a host and if yes, of which type. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee844a97c970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_879089.jpg" width="505" height="298" /></a></p>
