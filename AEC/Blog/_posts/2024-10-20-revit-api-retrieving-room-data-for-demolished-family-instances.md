---
layout: "post"
title: "Revit API: Retrieving Room Data for Demolished Family Instances"
date: "2024-10-20 22:41:18"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2024/10/revit-api-retrieving-room-data-for-demolished-family-instances.html "
typepad_basename: "revit-api-retrieving-room-data-for-demolished-family-instances"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p>In Revit projects, it is important to track room data for family instances, especially when they are marked as &#39;Existing&#39; and later demolished during the &#39;New Construction&#39; phase.</p>
<p><strong>The Problem: Missing Room Data for Demolished Family Instances</strong></p>
<p>The issue comes from how the Revit API works. The `<span style="font-family: &#39;courier new&#39;, courier;">FamilyInstance.Room</span>` property gives room data based on the final phase of the project. If a family instance has been demolished and no longer exists in the final phase, the API might return incorrect or null values This can be problematic when accurate room data is needed from earlier phases, before the family instance was demolished.</p>
<p><strong>The Solution: Getting Room Data by Phase</strong></p>
<p>To fix this, the Revit API provides a method called `<span style="font-family: &#39;courier new&#39;, courier;">FamilyInstance.get_Room(Phase)</span>`. This allows you to retrieve room information for the specific phase you need, even if the family instance was demolished in a later phase. It ensures you’re getting the right data for each phase, just like in Revit schedules.</p>
<p>Another option is to use `<span style="font-family: &#39;courier new&#39;, courier;">Document.GetRoomAtPoint(XYZ, Phase)</span>`, which retrieves the room based on the family instance’s exact location during a specific phase. This method is useful for more complex cases, ensuring that you get accurate room data regardless of what happens to the family instances in later phases.</p>
<pre class="prettyprint">// Get the active document and UIDocument from the commandData
Document doc = commandData.Application.ActiveUIDocument.Document;
UIDocument uidoc = commandData.Application.ActiveUIDocument;

// Retrieve all project phases (assuming 3 phases: &quot;Existing&quot;, &quot;New Construction&quot;, &quot;Final Phase&quot;)
FilteredElementCollector phaseCollector = new FilteredElementCollector(doc)
.OfCategory(BuiltInCategory.OST_Phases)
.WhereElementIsNotElementType();

// Find the specific phases by name
Phase existingPhase=phaseCollector.FirstOrDefault(phase=&gt;phase.Name==&quot;Existing&quot;) as Phase;
Phase newConstructionPhase=phaseCollector.FirstOrDefault(phase=&gt;phase.Name==&quot;New Construction&quot;) as Phase;
Phase finalPhase=phaseCollector.FirstOrDefault(phase=&gt;phase.Name==&quot;Final Phase&quot;) as Phase;

// Let the user pick an element (family instance) from the Revit model
Autodesk.Revit.DB.Reference pickedReference = uidoc.Selection.PickObject(ObjectType.Element);
Element pickedElement = doc.GetElement(pickedReference);
FamilyInstance familyInstance = pickedElement as FamilyInstance;

if (familyInstance != null)
{<br />   // Room in which the family instance is located during the final phase of the project
    Room currentRoom = familyInstance.Room;

   // Access the room the family instance is located in, based on a specific phase
   // Modify this to set the desired phase: existingPhase, newConstructionPhase, or finalPhase
    Phase targetPhase = existingPhase;
    Room roomInSpecificPhase = familyInstance.get_Room(targetPhase);

   // Workaround: Get the family instance&#39;s location point <br />   //and find the corresponding room in the specified phase.
    LocationPoint familyLocation = familyInstance.Location as LocationPoint;
    if (familyLocation != null)
      {
        XYZ locationPoint = familyLocation.Point;
        Room roomAtPoint = doc.GetRoomAtPoint(locationPoint, targetPhase);
      }
}

</pre>
<p><strong>Automating Phase-Based View Schedule Creation for Revit Projects</strong></p>
<p>You can also use the Revit API to automate the creation of view schedules for different project phases. Instead of manually creating schedules for each phase, the API can automate this process, linking each schedule to the correct phase and organizing the data properly. This saves time and ensures accuracy across all phases of the project.</p>
<pre class="prettyprint">// Create a filtered collector to gather all Phase elements in the document
FilteredElementCollector collector = new FilteredElementCollector(doc)<br />.OfCategory(BuiltInCategory.OST_Phases)<br />.WhereElementIsNotElementType();
using (Transaction actrans = new Transaction(doc, &quot;Create View Schedules&quot;))
{
  actrans.Start();
  foreach (Element e in collector)
    {
      Phase phase = e as Phase;
      if (phase != null)
        {
          // Create a view schedule for the each phase
           CreateViewSchedule(doc ,phase);
        }
    }
  actrans.Commit();
}

private void CreateViewSchedule(Document doc , Phase phase)
{
  // Create a new view schedule in the document
  //with an InvalidElementId for a multi-category schedule
   ViewSchedule viewSchedule = ViewSchedule.CreateSchedule(doc, ElementId.InvalidElementId);
 
  // Set the name of the schedule
   viewSchedule.Name = &quot;API-&quot; + phase.Name;

  // Set the phase parameter of the view schedule to the required phase
   viewSchedule.get_Parameter(BuiltInParameter.VIEW_PHASE).Set(phase.Id);

   ScheduleDefinition definition = viewSchedule.Definition;

  // Loop through all schedulable fields and add them to the schedule definition
   foreach (SchedulableField sf in definition.GetSchedulableFields())
    {
      ScheduleField field = definition.AddField(sf);
    }
}

</pre>
