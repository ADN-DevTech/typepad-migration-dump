---
layout: "post"
title: "Avoiding regeneration during changing multiple Family Symbols"
date: "2012-07-13 14:38:37"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/07/avoiding-regeneration-during-changing-multiple-family-symbols.html "
typepad_basename: "avoiding-regeneration-during-changing-multiple-family-symbols"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>After every change of a Family Symbol for any given FamilyInstance, the Revit model does a regeneration – even if the regeneration is not explicitly called. This can slow down the process of updating the model. </p>  <p>This regeneration of the symbol after it has been changed is triggered by the product and not the API, and so there isn’t much the API can do to prevent this unfortunately. Having said that, Revit API exposes a method called <em>Element.ChangeTypeId(Document, ICollection&lt;ElementId&gt;), ElementId)</em> which changes the type of all elements in the given set. Using this API should help avoid the regeneration after each and every change of element type (symbol) and have the model regenerated only once.</p>
