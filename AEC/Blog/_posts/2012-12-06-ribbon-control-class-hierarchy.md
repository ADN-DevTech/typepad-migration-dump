---
layout: "post"
title: "Ribbon Control Class Hierarchy"
date: "2012-12-06 16:54:38"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/12/ribbon-control-class-hierarchy.html "
typepad_basename: "ribbon-control-class-hierarchy"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>While preparing for my lecture at AU 2012 on Revit UI API, I was looking at the documentation we have on the Revit ribbon control classes and how best I could explain the various types of ribbon control classes the API exposes. I also wanted to highlight some of the properties and method that each of them expose uniquely, primarily because of their unique behavior. For example, a textbox would behave quite differently from a drop-down list and so the properties and methods would be quite drastically different. Also, some of the ribbon control classes derived from another class and so they inherited some behavior (in terms of properties and methods) from the parent (base) class. So to map this entire relationship out more visually, I created a class hierarchy diagram for the Revit ribbon control classes. The diagram is included below and you can click on it to get a detailed view.&#160; </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5fe5542970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; border-top: 0px; margin-right: auto; border-right: 0px; padding-top: 0px" title="Picture1" border="0" alt="Picture1" src="/assets/image_556132.jpg" width="481" height="262" /></a></p>  <p>For each of the classes, I have listed some of the unique properties and methods that further enhance the understanding of the class and its implementation. If you need to know more about any of these classes, their listed properties or methods, you can search for the details in the RevitAPI.chm file which is included in the Revit SDK. </p>
