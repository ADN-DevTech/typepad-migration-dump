---
layout: "post"
title: "How to get the picked element in the linked model"
date: "2014-02-27 20:54:19"
author: "Joe Ye"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2014/02/how-to-get-the-picked-element-in-the-linked-model.html "
typepad_basename: "how-to-get-the-picked-element-in-the-linked-model"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>In Revit 2014, developers can call the PickObject(ObjectType.LinkedElement) to pick element in the linked model. Many developers love that. One question is how to get the picked element. They use the ElementId in the returned Reference.ElementId cannot successfully get the target element.</p>  <p>Actually in along with addition of ObjectType.LinkedElement enum member, Reference class also has one new member, LinkedElementId. Through this property, you can get the right ElementId of the linked document.</p>  <p>Here is the minimum code fragment to show the idea.</p>  <p>&lt;code&gt;</p>  <blockquote>   <p>Document docLinked = null;</p> </blockquote>  <blockquote>   <p>Reference refElemLinked =uiDoc.Selection.PickObject(ObjectType.LinkedElement,”Please pick an element in the linked model”);</p>    <p>//Add code to get linked document here.</p>    <p>Element elem = linkedDoc.GetElement(refElemLinked.LinkedElementId);</p> </blockquote>  <p>&lt;code&gt;</p>
