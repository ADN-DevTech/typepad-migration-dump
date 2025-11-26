---
layout: "post"
title: "Change Ceiling Type Via API"
date: "2013-04-25 02:49:00"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2013/04/change-ceiling-type-via-api.html "
typepad_basename: "change-ceiling-type-via-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>For wall, floor, and roof, they have a dedicated property to change their type.</p>  <p>i.e. with walls:    <br />Dim newwalltype As WallType     <br />newwalltype = existingwall.RoofType.Duplicate(newname)     <br />newwalltype.SetCompoundStructure(newstructure)     <br />existingwall. WallType = newwalltype </p>  <p>but with ceilings:    <br />Dim newCeilingType As CeilingType     <br />newCeilingType = existingceiling.Duplicate(newname)     <br />newCeilingType.SetCompoundStructure(newstructure) </p>  <p>this works but now how can we set the CeilingType of the existing ceiling object in the new one (newCeilingType)? </p>  <p>For some elements, they don’t have dedicated shortcut property to change their type, as here mentioned Ceiling. Revit provide a more generic method, ChangeTypeId() within Element class, developers can use this method to change any element’s type, including Ceiling. Of cause, ChangeTypeId() method can also be used to change wall, floor, and roof type too. Pass in the target type’s Id, and that’s solution.</p>  <p>Here is the code snippet for changing the ceiling type.</p>  <p>Dim newCeilingType As CeilingType    <br />newCeilingType = existingceiling.Duplicate(newname)     <br />newCeilingType.SetCompoundStructure(newstructure) </p>  <p>existingCeiling.ChangeTypeId(newCeilingType.Id)</p>
