---
layout: "post"
title: "Creating new TextNoteType programmatically"
date: "2012-08-14 23:57:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/08/creating-new-textnotetype-programmatically.html "
typepad_basename: "creating-new-textnotetype-programmatically"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>An ADN partner had once, contacted us asking about how to create a new TextNoteType in a Revit document using the API. </p>  <p>To create a new TextNoteType, we can access one the of types of an existing TextNote and then use the TextNoteType.Duplicate() method to create a new type by specifying a name for this new type. Following this, we have to set the parameters of this new type, as was desired for the new TextNoteType. </p>
