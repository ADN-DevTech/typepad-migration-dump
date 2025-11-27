---
layout: "post"
title: "Working with PropertySets and Model States"
date: "2022-01-24 03:34:51"
author: "Sajith Subramanian"
categories:
  - "Inventor"
  - "Sajith Subramanian"
original_url: "https://adndevblog.typepad.com/manufacturing/2022/01/working-with-propertysets-and-model-states.html "
typepad_basename: "working-with-propertysets-and-model-states"
typepad_status: "Publish"
---

<p><p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>If you wish to change a Property's value on a document having Model States, the workflow via API is the similar to the Inventor UI: </p><ul><li>Activate the required Model State using ModelState.Activate.</li><li>Set the MemberEditScope to either kEditActiveMember or kEditAllMembers based on whether the updated value would be applied only to the active model state or all members.</li><li>Get the property from Factory document (and not the Model State document), and change its value.</li><li>To confirm whether the required Property has been correctly updated, activate the respective Model State and query the property from the Factory Document. </li></ul><p>Here is a sample VBA code that exhibits a similar workflow:</p><blockquote><p><font face="Courier New">Sub ModelStateProps()</font></p><font face="Courier New">
</font><p><font face="Courier New">Dim oPartDoc&nbsp; As PartDocument<br>
Set oPartDoc = ThisDocument</font></p><font face="Courier New">
</font><p><font face="Courier New">Dim oCompDef As PartComponentDefinition<br>
Set oCompDef = oPartDoc.ComponentDefinition</font></p><font face="Courier New">
</font><p><font face="Courier New">Dim oModelStates As ModelStates<br>
Set oModelStates = oCompDef.ModelStates</font></p><p><br></p><p><font face="Courier New">‘Fetch the required Model State from the collection</font></p><p><font face="Courier New">Dim oModelState As ModelState<br>
Set oModelState = oCompDef.ModelStates("Model State 1") </font></p><p><br></p><font face="Artifakt Element">
</font><p><font face="Courier New">'Activate the model state<br>
oModelState.Activate</font></p><font face="Courier New">
</font><p><font face="Courier New"><br></font></p><p><font face="Courier New">' Get the design tracking property set for our model state.<br>
Dim oSummaryInfo As Inventor.PropertySet<br>
Set oSummaryInfo = oPartDoc.PropertySets.Item("Inventor Summary Information")</font></p><p><font face="Courier New"><br></font></p><font face="Courier New">
</font><p><font face="Courier New">' Get the Comments property.<br>
Dim oCommentsProperty As Property<br>
Set oCommentsProperty = oSummaryInfo.Item("Comments")</font></p><p><font face="Courier New"><br></font></p><font face="Courier New">
</font><p><font face="Courier New">' Set the Member Edit Scope<br>
oModelStates.MemberEditScope = kEditActiveMember&nbsp; 'kEditAllMembers for all members</font></p><p><font face="Courier New"><br></font></p><font face="Courier New">
</font><p><font face="Courier New">'Set the value<br>
oCommentsProperty.Value = "Updating comments via API!"</font></p>
<p><font face="Courier New">End Sub</font></p></blockquote><p>A couple of things to remember:</p><ul><li>It is recommended to edit in the Factory Document context and activate the Model State you wish to edit.</li><li>It is not recommended to change the Model State Member Document directly. Also, the Member Document may not exist, if not already generated. Hence, your code would return a ‘Null’ in such cases, as shown below.</li></ul><p><p><p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f95372c200c-pi"><img width="608" height="637" title="modelstates" style="display: inline; background-image: none;" alt="modelstates" src="/assets/image_8dbbba.jpg" border="0"></a></p>
