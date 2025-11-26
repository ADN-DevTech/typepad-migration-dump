---
layout: "post"
title: "Changing Properties of Elements in a Group"
date: "2012-08-09 22:28:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/08/changing-properties-of-elements-in-a-group.html "
typepad_basename: "changing-properties-of-elements-in-a-group"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Using the API currently, it is not possible to change any of the parameter values of the existing elements inside a Group, directly. Neither are there any current parameters that are writeable on elements which are part of Groups. </p>  <p>To store some additional information on elements that are part of a group, we can programmatically ungroup a group, add the desired information on all the elements (or the elements you wish to add to) by working on the parameter value and then regroup the elements to form a group again. If any information is meant to be added directly to a Group element, the Group.SetEntity() method could be used to add information to the entire Group element. </p>
