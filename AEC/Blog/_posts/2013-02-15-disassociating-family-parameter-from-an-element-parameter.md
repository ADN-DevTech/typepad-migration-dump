---
layout: "post"
title: "Disassociating Family Parameter from an Element Parameter"
date: "2013-02-15 13:17:21"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/02/disassociating-family-parameter-from-an-element-parameter.html "
typepad_basename: "disassociating-family-parameter-from-an-element-parameter"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Here is a typical query we have received in the past: </p>  <p><em>I am using the following code to associate/disassociate a family parameter from/to an element parameter.      <br />doc.FamilyManager.AssociateElementParameterToFamilyParameter(ep, fp)</em></p>  <p><em>I can associate fine. However, there are no instructions on how to disassociate. Can you let me know how to disassociate a Family parameter from an Element parameter.</em></p>  <p><strong>Resolution</strong>:</p>  <p>As per the Revit API chm file, the same AssociateElementParameterToFamilyParameter() method can help disassociate a element parameter from an existing family parameter. The only change would be that the value of the family parameter would be null (or Nothing in VB). </p>  <p>So to test this, calling the AssociateElementParameterToFamilyParameter() method once, it creates a column family and also associated the element parameter with family parameter, as shown below:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d4115654a970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_38111.jpg" width="482" height="253" /></a>&#160; <br />Next, if we add another line of code to call the same method and instead pass null as family parameter, the following dialog confirms that the disassociation works as expected: </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d41156557970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_435871.jpg" width="488" height="278" /></a></p>
