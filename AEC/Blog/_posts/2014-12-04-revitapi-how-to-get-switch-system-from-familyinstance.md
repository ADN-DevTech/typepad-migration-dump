---
layout: "post"
title: "RevitAPI: How to get switch system from FamilyInstance?"
date: "2014-12-04 04:49:57"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
  - "Revit MEP"
original_url: "https://adndevblog.typepad.com/aec/2014/12/revitapi-how-to-get-switch-system-from-familyinstance.html "
typepad_basename: "revitapi-how-to-get-switch-system-from-familyinstance"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>A customer asked how to create switch system, and how to know a FamilyInstance belongs to which switch systems. Unfortunately, we don't have API to create switch system and no workaround as far as I know. And no direct way to get switch systems of FamilyInstance like electrical system (i.e. FamilyInstance.MEPModel.ElectricalSystems). The good news is we have a workaround: filtering all switch systems and see if any switch system contains that family instance. Code example:</p>
<p>
<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>
</p>
<pre class="prettyprint">Element element = null; // the element to find switch system
var categoryFilter =
    new ElementCategoryFilter(BuiltInCategory.OST_SwitchSystem);
FilteredElementCollector mepSystems =
    new FilteredElementCollector(RevitDoc);
mepSystems = mepSystems
    .WherePasses(categoryFilter).OfClass(typeof(MEPSystem));
foreach (MEPSystem mepSystem in mepSystems)
{
    foreach (Element member in mepSystem.Elements)
    {
        if (member.Id == element.Id)
        {
            //found
            break;
        }
    }
}
</pre>
<p>Further questions:</p>
<p>1) Are MEPSystems always SwitchSystems or are there other types of MepSystems as well?<br /> <strong>Answer</strong>: there are other types, MEPSystem is the base class of&nbsp;ElectricalSystem,MechanicalSystem and PipingSystem, that's why we need to use the combination of ElementClassFilter and ElementCategoryFilter.<br /> <br /> 2) Is it possible that an element in the MepSystem.Elements exists in another MepSystem as well? Or can an element only belong to one MepSystem?</p>
<p><strong>Answer</strong>: An element can belong to multiple systems when it has multiple connectors used in different systems.</p>
