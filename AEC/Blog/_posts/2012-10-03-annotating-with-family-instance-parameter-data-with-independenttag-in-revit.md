---
layout: "post"
title: "Annotating with Family Instance Parameter data with IndependentTag in Revit"
date: "2012-10-03 13:55:05"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/10/annotating-with-family-instance-parameter-data-with-independenttag-in-revit.html "
typepad_basename: "annotating-with-family-instance-parameter-data-with-independenttag-in-revit"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>While creating IndependentTags for elements in a Revit model, you might have noticed that the IndependentTag.TagText() method is read-only. The label for this tag is picked up, by default, by the Type parameter. For instance, for any given wall, when Document.NewTag() method is used to create a new IndependentTag, it picks up the value of the <em>Type Mark</em> parameter which is contained in the Wall Type. </p>  <p>If you wish to use the Wall Instance parameter value instead (say <em>Mark</em> parameter) for the tag, you can do that from the Revit User Interface (UI). You can do this by editing the Tag family and select the Label in it. This brings up the parameters of the Label in the Family Editor mode and at this point, we can click the Edit button on the <em>Label</em> parameter of the Label. This brings up a dialog called <em>Edit Label</em> in which we can set the specific Instance parameter you want to use to set the Tag text (<em>Mark</em> parameter in this case), instead of the default Type parameter. The screenshot below shows the dialog I am referring to here:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c324dd32d970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_525617.jpg" width="471" height="278" /></a></p>  <p>After reloading the Tag family in the model, any new tags created will use this specific Instance parameter value for its text. </p>  <p>Using the API, it does not seem like we can edit the Label of a Tag family. So as an alternative, we can work with a model/template which already has the tag families configured (as per the specific Instance parameter you want to use) and also loaded in the model. Consequently any API calls to NewTag() should use the configured tags.</p>
