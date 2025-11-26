---
layout: "post"
title: "Changing the family category using the Revit API"
date: "2012-08-07 22:14:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/08/changing-the-family-category-using-the-revit-api.html "
typepad_basename: "changing-the-family-category-using-the-revit-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Is it possible to change the category of a family using the Revit API? </p>  <p>The category of a family is defined when it is created and cannot be changed later. If we use EditFamily() and access the OwnerFamily on the family document, we can see that the OwnerFamily’s Category is read-only. So the solution is to use the appropriate template for the category of family you wish to create at the time of family creation itself. </p>
